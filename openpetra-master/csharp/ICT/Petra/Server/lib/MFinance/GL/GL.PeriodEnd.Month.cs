//
// DO NOT REMOVE COPYRIGHT NOTICES OR THIS FILE HEADER.
//
// @Authors:
//       wolfgangu, timop
//       Tim Ingham
//
// Copyright 2004-2020 by OM International
//
// This file is part of OpenPetra.org.
//
// OpenPetra.org is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// OpenPetra.org is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with OpenPetra.org.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Data;
using System.Data.Odbc;
using System.Collections;
using Ict.Petra.Server.App.Core.Security;
using Ict.Petra.Server.MFinance.Cacheable.WebConnectors;
using Ict.Petra.Server.MFinance.Account.Data.Access;
using Ict.Petra.Shared.MFinance.Account.Data;
using Ict.Petra.Server.MFinance.GL.WebConnectors;
using Ict.Petra.Shared.MCommon.Data;
using Ict.Petra.Server.MCommon.Data.Access;
using Ict.Common;
using Ict.Common.DB;
using Ict.Common.Verification;
using Ict.Common.Remoting.Shared;
using Ict.Petra.Server.MFinance.Gift.Data.Access;
using Ict.Petra.Server.MFinance.GL;
using Ict.Petra.Server.MFinance.Common;
using Ict.Petra.Server.MPartner.Partner.Data.Access;
using Ict.Petra.Shared;
using Ict.Petra.Shared.MFinance;
using Ict.Petra.Shared.MFinance.Gift.Data;
using Ict.Petra.Shared.MFinance.GL.Data;
using Ict.Petra.Shared.MPartner.Partner.Data;
using System.Collections.Generic;

namespace Ict.Petra.Server.MFinance.GL.WebConnectors
{
    /// <summary>
    /// Routines for running the period month end check.
    /// </summary>
    public partial class TPeriodIntervalConnector
    {
        /// <summary>
        /// MonthEnd master routine ...
        /// </summary>
        /// <param name="ALedgerNumber"></param>
        /// <param name="AInfoMode"></param>
        /// <param name="AglBatchNumbers"></param>
        /// <param name="AStewardshipBatch">True if Stewardship Batch was generated</param>
        /// <param name="AVerificationResults"></param>
        /// <param name="ADataBase"></param>
        /// <returns>true if there's no problem</returns>
        [RequireModulePermission("FINANCE-2")]
        public static bool PeriodMonthEnd(
            Int32 ALedgerNumber,
            bool AInfoMode,
            out List <Int32>AglBatchNumbers,
            out Boolean AStewardshipBatch,
            out TVerificationResultCollection AVerificationResults,
            TDataBase ADataBase = null)
        {
            AglBatchNumbers = new List <int>();
            AStewardshipBatch = false;
            try
            {
                TLedgerInfo ledgerInfo = new TLedgerInfo(ALedgerNumber, ADataBase);
                Int32 PeriodClosing = ledgerInfo.CurrentPeriod;
                bool succeeded = new TMonthEnd(ADataBase, ledgerInfo).RunMonthEnd(AInfoMode,
                    out AglBatchNumbers,
                    out AStewardshipBatch,
                    out AVerificationResults);

                if (succeeded && !AInfoMode)
                {
                    TDBTransaction Transaction = new TDBTransaction();
                    TDataBase db = DBAccess.Connect("PeriodMonthEnd", ADataBase);
                    AAccountingPeriodTable PeriodTbl = null;

                    db.ReadTransaction(
                        ref Transaction,
                        delegate
                        {
                            PeriodTbl = AAccountingPeriodAccess.LoadByPrimaryKey(ledgerInfo.LedgerNumber, PeriodClosing, Transaction);
                        });

                    if (ADataBase == null)
                    {
                        db.CloseDBConnection();
                    }

                    if (succeeded && PeriodTbl.Rows.Count > 0)
                    {
                        AVerificationResults.Add(
                            new TVerificationResult(
                                Catalog.GetString("Month End"),
                                String.Format(Catalog.GetString("The period {0} - {1} has been closed."),
                                    PeriodTbl[0].PeriodStartDate.ToShortDateString(), PeriodTbl[0].PeriodEndDate.ToShortDateString()),
                                TResultSeverity.Resv_Status));
                    }
                }

                return succeeded;
            }
            catch (Exception e)
            {
                TLogging.Log("TPeriodIntervallConnector.TPeriodMonthEnd() throws " + e.ToString());
                AVerificationResults = new TVerificationResultCollection();
                AVerificationResults.Add(
                    new TVerificationResult(
                        Catalog.GetString("Month End"),
                        Catalog.GetString("Uncaught Exception: ") + e.Message,
                        TResultSeverity.Resv_Critical));


                return false;
            }
        }
    }
}

namespace Ict.Petra.Server.MFinance.GL
{
    /// <summary>
    /// Main Class to serve a
    /// Ict.Petra.Server.MFinance.GL.WebConnectors.TPeriodMonthEnd request ...
    /// </summary>
    public class TMonthEnd : TPeriodEndOperations
    {
        TLedgerInfo FledgerInfo;
        TDataBase FDataBase;

        /// <summary>
        ///
        /// </summary>
        [NoRemoting]
        public delegate bool StewardshipCalculation(int ALedgerNumber,
            int APeriodNumber,
            out List <Int32>AglBatchNumbers,
            out TVerificationResultCollection AVerificationResult,
            TDataBase ADataBase = null);
        [ThreadStatic]
        private static StewardshipCalculation FStewardshipCalculationDelegate;

        /// <summary>
        ///
        /// </summary>
        [NoRemoting]
        public static StewardshipCalculation StewardshipCalculationDelegate
        {
            get
            {
                return FStewardshipCalculationDelegate;
            }
            set
            {
                FStewardshipCalculationDelegate = value;
            }
        }

        /// <summary>Constructor</summary>
        /// <param name="ADataBase"></param>
        /// <param name="ALedgerInfo"></param>
        public TMonthEnd(TDataBase ADataBase, TLedgerInfo ALedgerInfo)
        {
            FledgerInfo = ALedgerInfo;
            FDataBase = ADataBase;
        }

        /// <summary>
        /// Go to next month - unless you're already at the last month!
        /// </summary>
        public override void SetNextPeriod(TDBTransaction ATransaction)
        {
            if (FledgerInfo.CurrentPeriod == FledgerInfo.NumberOfAccountingPeriods)
            {
                // Set the YearEndFlag to Switch between the months...
                FledgerInfo.ProvisionalYearEndFlag = true;
            }
            else
            {
                // Conventional Month->Month Switch ...
                FledgerInfo.CurrentPeriod = FledgerInfo.CurrentPeriod + 1;
            }

            TCacheableTablesManager.GCacheableTablesManager.MarkCachedTableNeedsRefreshing(
                TCacheableFinanceTablesEnum.LedgerDetails.ToString());
        }

        /// <summary>
        /// Any foreign account that has a non-zero opening balance should be marked
        /// for revaluation.
        /// </summary>
        private void NoteForexRevalRequired(Int32 ALedgerNumber, Int32 AYear, Int32 ABatchPeriod)
        {
            TDBTransaction transaction = new TDBTransaction();
            TDataBase db = DBAccess.Connect("NoteForexRevalRequired", FDataBase);
            Boolean submissionOK = true;

            if (ABatchPeriod == FledgerInfo.NumberOfAccountingPeriods)
            {
                if (FledgerInfo.NumberFwdPostingPeriods == 0)
                {
                    return;
                }
                else
                {
                    // If we're closing the final period, SetNextPeriod() doesn't update FledgerInfo.CurrentPeriod. But we still want to
                    // check the next period for foreign forward-posted amounts, so we have to increment the period locally here.
                    ABatchPeriod++;
                }
            }

            db.WriteTransaction(
                ref transaction,
                ref submissionOK,
                delegate
                {
                    string strSQL = "SELECT Account.a_account_code_c," +
                                    "SUM (GLMP.a_actual_foreign_n) AS Balance" +
                                    " FROM PUB_a_account Account, PUB_a_general_ledger_master GLM, PUB_a_general_ledger_master_period GLMP" +
                                    " WHERE Account.a_ledger_number_i=" + ALedgerNumber +
                                    " AND Account.a_foreign_currency_flag_l=true" +
                                    " AND GLM.a_ledger_number_i=" + ALedgerNumber +
                                    " AND GLM.a_account_code_c=Account.a_account_code_c" +
                                    " AND GLM.a_year_i= " + AYear +
                                    " AND GLMP.a_glm_sequence_i=GLM.a_glm_sequence_i" +
                                    " AND GLMP.a_period_number_i=" + ABatchPeriod +
                                    " GROUP BY Account.a_account_code_c";
                    DataTable Balance = db.SelectDT(strSQL, "Balance", transaction);

                    foreach (DataRow Row in Balance.Rows)
                    {
                        if (Convert.ToDecimal(Row["Balance"]) != 0)
                        {
                            TLedgerInitFlag flag = new TLedgerInitFlag(ALedgerNumber, "", transaction.DataBaseObj);
                            flag.SetFlagComponent(MFinanceConstants.LEDGER_INIT_FLAG_REVAL,
                                Row["a_account_code_c"].ToString());
                        }
                    }
                });

            if (FDataBase == null)
            {
                db.CloseDBConnection();
            }
        }

        /// <summary>
        /// Main Entry point. The parameters are the same as in
        /// Ict.Petra.Server.MFinance.GL.WebConnectors.TPeriodMonthEnd
        /// </summary>
        /// <param name="AInfoMode"></param>
        /// <param name="AglBatchNumbers">The Client should print the generated Batches</param>
        /// <param name="AStewardshipBatch">True if Stewardship Batch was generated</param>
        /// <param name="AVRCollection"></param>
        /// <returns>true if it went OK</returns>
        public bool RunMonthEnd(
            bool AInfoMode,
            out List <Int32>AglBatchNumbers,
            out Boolean AStewardshipBatch,
            out TVerificationResultCollection AVRCollection)
        {
            FInfoMode = AInfoMode;
            FverificationResults = new TVerificationResultCollection();
            AVRCollection = FverificationResults;
            AStewardshipBatch = false;
            WasCancelled = false;
            AglBatchNumbers = new List <int>();

            if (FledgerInfo.ProvisionalYearEndFlag)
            {
                // we want to run a month end, but the provisional year end flag has been set
                TVerificationResult tvt =
                    new TVerificationResult(Catalog.GetString("Year End is required!"),
                        Catalog.GetString("In this situation you cannot run a month end routine"), "",
                        TPeriodEndErrorAndStatusCodes.PEEC_03.ToString(),
                        TResultSeverity.Resv_Critical);
                FverificationResults.Add(tvt);
                FHasCriticalErrors = true;
                return false;
            }

            TDBTransaction Transaction = new TDBTransaction();
            TDataBase db = DBAccess.Connect("RunMonthEnd", FDataBase);

            if (AInfoMode)
            {
                AAccountingPeriodTable PeriodTbl = null;
                
                db.ReadTransaction(
                    ref Transaction,
                    delegate
                    {
                        PeriodTbl = AAccountingPeriodAccess.LoadByPrimaryKey(FledgerInfo.LedgerNumber, FledgerInfo.CurrentPeriod, Transaction);
                    });

                if (PeriodTbl.Rows.Count > 0)
                {
                    FverificationResults.Add(
                        new TVerificationResult(
                            Catalog.GetString("Month End"),
                            String.Format(Catalog.GetString("Current period is {0} - {1}"),
                                PeriodTbl[0].PeriodStartDate.ToShortDateString(), PeriodTbl[0].PeriodEndDate.ToShortDateString()),
                            TResultSeverity.Resv_Status));
                }
            }

            RunPeriodEndCheck(new RunMonthEndChecks(db, FledgerInfo), FverificationResults);

            if (!AInfoMode)
            {
                TVerificationResultCollection IchVerificationResults;

                if (!StewardshipCalculationDelegate(FledgerInfo.LedgerNumber, FledgerInfo.CurrentPeriod,
                        out AglBatchNumbers,
                        out IchVerificationResults, db))
                {
                    FHasCriticalErrors = true;
                }

                // Merge VerificationResults:
                FverificationResults.AddCollection(IchVerificationResults);

                if (AglBatchNumbers.Count > 0)
                {
                    AStewardshipBatch = true;
                }
            }

            // RunPeriodEndSequence(new RunMonthlyAdminFees(), "Example");

            if (TPeriodEndOperations.WasCancelled)
            {
                FverificationResults.Add(new TVerificationResult(Catalog.GetString("Month End"),
                        Catalog.GetString("Process was cancelled by user."), "",
                        TPeriodEndErrorAndStatusCodes.PEEC_12.ToString(),
                        TResultSeverity.Resv_Critical));
                FHasCriticalErrors = true;
            }

            if (!FInfoMode)
            {
                if (!FHasCriticalErrors)
                {
                    SetNextPeriod(Transaction);
                    NoteForexRevalRequired(FledgerInfo.LedgerNumber, FledgerInfo.CurrentFinancialYear, FledgerInfo.CurrentPeriod);
                    // refresh cached ledger table, so that the client will know the current period
                    TCacheableTablesManager.GCacheableTablesManager.MarkCachedTableNeedsRefreshing(
                        TCacheableFinanceTablesEnum.LedgerDetails.ToString());
                }
            }

            if (FDataBase == null)
            {
                db.CloseDBConnection();
            }

            return !FHasCriticalErrors;
        }
    }

    class RunMonthEndChecks : AbstractPeriodEndOperation
    {
        TDataBase FDataBase = null;
        TLedgerInfo FledgerInfo;

        GetSuspenseAccountInfo suspenseAccountInfo = null;

        public RunMonthEndChecks(TDataBase ADataBase, TLedgerInfo ALedgerInfo)
        {
            FledgerInfo = ALedgerInfo;
            FDataBase = ADataBase;
        }

        public override int GetJobSize()
        {
            return 0;
        }

        public override AbstractPeriodEndOperation GetActualizedClone()
        {
            return new RunMonthEndChecks(FDataBase, FledgerInfo);
        }

        public override Int32 RunOperation()
        {
            CheckIfRevaluationIsDone();
            CheckForUnpostedBatches();
            CheckForUnpostedGiftBatches();

            if (FInfoMode)
            {
                CheckForSuspenseAccounts();
            }

            CheckForSuspenseAccountsZero();
            return 0;
        }

        private void CheckIfRevaluationIsDone()
        {
            if (!FInfoMode)
            {
                return;
            }

            TLedgerInitFlag flag = new TLedgerInitFlag(FledgerInfo.LedgerNumber, "", null);
            String RevalAccounts = flag.GetFlagValue(MFinanceConstants.LEDGER_INIT_FLAG_REVAL);

            if (RevalAccounts == "")
            {
                return; // Revaluation has been performed.
            }

            TVerificationResult tvr;

            if (FledgerInfo.CurrentPeriod < FledgerInfo.NumberOfAccountingPeriods)
            {
                tvr = new TVerificationResult(
                    Catalog.GetString("Currency revaluation"),
                    String.Format(
                        Catalog.GetString("Before proceeding you may want to revalue foreign currency accounts {0}."),
                        RevalAccounts),
                    "",
                    TPeriodEndErrorAndStatusCodes.PEEC_05.ToString(), TResultSeverity.Resv_Status);
                // Error is non-critical - the user can choose to continue.
            }
            else
            {
                tvr = new TVerificationResult(
                    Catalog.GetString("Currency revaluation"),
                    String.Format(
                        Catalog.GetString("The foreign currency accounts {0} need to be revalued."),
                        RevalAccounts),
                    "",
                    TPeriodEndErrorAndStatusCodes.PEEC_05.ToString(), TResultSeverity.Resv_Critical);
                // Error is critical - the user nust do a reval.
            }

            FverificationResults.Add(tvr);
        }

        private void CheckForUnpostedBatches()
        {
            GetBatchInfo getBatchInfo = new GetBatchInfo(
                FledgerInfo.LedgerNumber, FledgerInfo.CurrentFinancialYear, FledgerInfo.CurrentPeriod);

            if (getBatchInfo.NumberOfBatches > 0)
            {
                TVerificationResult tvr = new TVerificationResult(
                    Catalog.GetString("Unposted Batches found"),
                    String.Format(Catalog.GetString(
                            "Please post or cancel the batches {0} first!"),
                        getBatchInfo.ToString()),
                    "", TPeriodEndErrorAndStatusCodes.PEEC_06.ToString(), TResultSeverity.Resv_Critical);
                FverificationResults.Add(tvr);
                FHasCriticalErrors = true;
            }
        }

        private void CheckForSuspenseAccounts()
        {
            if (suspenseAccountInfo == null)
            {
                suspenseAccountInfo =
                    new GetSuspenseAccountInfo(FledgerInfo.LedgerNumber);
            }

            if (suspenseAccountInfo.RowCount != 0)
            {
                TLogging.LogAtLevel(1, String.Format("MonthEnd: {0} suspense accounts in use.", suspenseAccountInfo.RowCount));
                TVerificationResult tvr = new TVerificationResult(
                    Catalog.GetString("Suspense Accounts found"),
                    String.Format(
                        Catalog.GetString(
                            "Have you checked the details of suspense account {0}?"),
                        suspenseAccountInfo.ToString()),
                    "", TPeriodEndErrorAndStatusCodes.PEEC_07.ToString(), TResultSeverity.Resv_Status);
                FverificationResults.Add(tvr);
            }

/*
 *          else
 *          {
 *              TLogging.LogAtLevel(1, "MonthEnd: No suspense accounts used.");
 *          }
 */
        }

        private void CheckForUnpostedGiftBatches()
        {
            TAccountPeriodInfo getAccountingPeriodInfo =
                new TAccountPeriodInfo(FledgerInfo.LedgerNumber, FledgerInfo.CurrentPeriod);
            GetUnpostedGiftInfo getUnpostedGiftInfo = new GetUnpostedGiftInfo(
                FledgerInfo.LedgerNumber, getAccountingPeriodInfo.PeriodEndDate);

            if (getUnpostedGiftInfo.HasRows)
            {
                TVerificationResult tvr = new TVerificationResult(
                    Catalog.GetString("Unposted Gift Batches found"),
                    String.Format(
                        "Please post or cancel the gift batches {0} first!",
                        getUnpostedGiftInfo.ToString()),
                    "", TPeriodEndErrorAndStatusCodes.PEEC_08.ToString(), TResultSeverity.Resv_Critical);
                FverificationResults.Add(tvr);
                FHasCriticalErrors = true;
            }
        }

        private void CheckForSuspenseAccountsZero()
        {
            if (FledgerInfo.CurrentPeriod == FledgerInfo.NumberOfAccountingPeriods)
            {
                // This means: The last accounting period of the year is running!

                if (suspenseAccountInfo == null)
                {
                    suspenseAccountInfo =
                        new GetSuspenseAccountInfo(FledgerInfo.LedgerNumber);
                }

                if (suspenseAccountInfo.RowCount > 0)
                {
                    ASuspenseAccountRow aSuspenseAccountRow;

                    for (int i = 0; i < suspenseAccountInfo.RowCount; ++i)
                    {
                        aSuspenseAccountRow = suspenseAccountInfo.Row(i);
                        TGet_GLM_Info get_GLM_Info = new TGet_GLM_Info(FledgerInfo.LedgerNumber,
                            aSuspenseAccountRow.SuspenseAccountCode,
                            FledgerInfo.CurrentFinancialYear, FDataBase);

                        if (get_GLM_Info.GLMExists)
                        {
                            TGlmpInfo get_GLMp_Info = new TGlmpInfo(FledgerInfo.LedgerNumber, FDataBase);
                            get_GLMp_Info.LoadBySequence(get_GLM_Info.Sequence, FledgerInfo.CurrentPeriod);

                            if (get_GLMp_Info.RowExists && (get_GLMp_Info.ActualBase != 0))
                            {
                                TVerificationResult tvr = new TVerificationResult(
                                    Catalog.GetString("Non Zero Suspense Account found"),
                                    String.Format(Catalog.GetString("Suspense account {0} has the balance value {1:F2}. It is required to be zero."),
                                        aSuspenseAccountRow.SuspenseAccountCode,
                                        get_GLMp_Info.ActualBase), "",
                                    TPeriodEndErrorAndStatusCodes.PEEC_07.ToString(), TResultSeverity.Resv_Critical);

                                FHasCriticalErrors = true;
                                FverificationResults.Add(tvr);
                            }
                        }
                    }
                }
            }
        } // CheckFor SuspenseAccountsZero
    } // Run MonthEndChecks

    /// <summary>
    /// Example ....
    /// </summary>
    class RunMonthlyAdminFees : AbstractPeriodEndOperation
    {
        public override int GetJobSize()
        {
            return 0;
        }

        public override AbstractPeriodEndOperation GetActualizedClone()
        {
            // TODO: Some Code
            return new RunMonthlyAdminFees();
        }

        public override Int32 RunOperation()
        {
            // TODO: Some Code
            return 0;
        }
    }

    /// <summary>
    /// Routine to find unposted gifts batches.
    /// </summary>
    public class GetUnpostedGiftInfo
    {
        DataTable FDataTable;

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public Boolean HasRows
        {
            get
            {
                return FDataTable.Rows.Count > 0;
            }
        }

        /// <summary>
        /// Direct access to the unposted gifts
        /// </summary>
        /// <param name="ALedgerNumber"></param>
        /// <param name="ADateEndOfPeriod"></param>
        public GetUnpostedGiftInfo(int ALedgerNumber, DateTime ADateEndOfPeriod)
        {
            OdbcParameter[] ParametersArray;
            ParametersArray = new OdbcParameter[3];
            ParametersArray[0] = new OdbcParameter("", OdbcType.Int);
            ParametersArray[0].Value = ALedgerNumber;
            ParametersArray[1] = new OdbcParameter("", OdbcType.Date);
            ParametersArray[1].Value = ADateEndOfPeriod;
            ParametersArray[2] = new OdbcParameter("", OdbcType.VarChar);
            ParametersArray[2].Value = MFinanceConstants.BATCH_UNPOSTED;

            TDBTransaction transaction = new TDBTransaction();
            TDataBase db = DBAccess.Connect("GetUnpostedGiftInfo");

            db.ReadTransaction(
                ref transaction,
                delegate
                {
                    string strSQL = "SELECT * FROM PUB_" + AGiftBatchTable.GetTableDBName() +
                                    " WHERE " + AGiftBatchTable.GetLedgerNumberDBName() + " = ?" +
                                    " AND " + AGiftBatchTable.GetGlEffectiveDateDBName() + " <= ?" +
                                    " AND " + AGiftBatchTable.GetBatchStatusDBName() + " = ? " +
                                    " ORDER BY " + AGiftBatchTable.GetBatchNumberDBName();

                    FDataTable = db.SelectDT(
                        strSQL, AAccountingPeriodTable.GetTableDBName(), transaction, ParametersArray);
                });

            db.CloseDBConnection();
        }

        /// <summary>
        /// Creates a comma separated list of the batch numbers
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string strH;

            if (FDataTable.Rows.Count == 0)
            {
                strH = "-";
            }
            else
            {
                int ih = Convert.ToInt32(FDataTable.Rows[0][AGiftBatchTable.GetBatchNumberDBName()]);
                strH = ih.ToString();

                for (int i = 1; i < FDataTable.Rows.Count; ++i)
                {
                    strH += (", " + Convert.ToString(FDataTable.Rows[i][AGiftBatchTable.GetBatchNumberDBName()]));
                }
            }

            return "(" + strH + ")";
        }
    }

    /// <summary>
    /// Routine to read the a_suspense_account entries
    /// </summary>
    public class GetSuspenseAccountInfo
    {
        ASuspenseAccountTable FSuspenseAccountTable;

        /// <summary>
        /// Constructor to define ...
        /// </summary>
        /// <param name="ALedgerNumber">the ledger Number</param>
        public GetSuspenseAccountInfo(int ALedgerNumber)
        {
            TDBTransaction Transaction = new TDBTransaction();
            TDataBase db = DBAccess.Connect("GetSuspenseAccountInfo");

            db.ReadTransaction(
                ref Transaction,
                delegate
                {
                    FSuspenseAccountTable = ASuspenseAccountAccess.LoadViaALedger(ALedgerNumber, Transaction);
                });

            db.CloseDBConnection();
        }

        /// <summary>
        /// In case of an error message you need the number of entries.
        /// </summary>
        public int RowCount
        {
            get
            {
                return FSuspenseAccountTable.Rows.Count;
            }
        }

        /// <summary>
        /// Gives direct access to the selected suspense account row ...
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ASuspenseAccountRow Row(int index)
        {
            return FSuspenseAccountTable[index];
        }

        /// <summary>
        /// Produces a comma separated list of suspense account codes
        /// for use in the status message(s).
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string strH;

            if (RowCount == 0)
            {
                strH = "-";
            }
            else
            {
                strH = FSuspenseAccountTable[0].SuspenseAccountCode;

                for (int i = 1; i < RowCount; ++i)
                {
                    strH += ", " + FSuspenseAccountTable[i].SuspenseAccountCode;
                }
            }

            return "(" + strH + ")";
        }
    }

    /// <summary>
    /// GetBatchInfo is a class to check for a list of batches in ledgerNum and actual period which are
    /// not posted or not cancelled, that means. This routine is looking for open tasks.
    /// </summary>
    public class GetBatchInfo
    {
        ABatchTable Fbatches = new ABatchTable();

        /// <summary>
        /// The contructor gets the root information
        /// </summary>
        public GetBatchInfo(Int32 ALedgerNumber, Int32 AYear, Int32 ABatchPeriod)
        {
            TDBTransaction transaction = new TDBTransaction();
            TDataBase db = DBAccess.Connect("GetBatchInfo");

            db.ReadTransaction(
                ref transaction,
                delegate
                {
                    string strSQL = "SELECT * FROM PUB_a_batch" +
                                    " WHERE a_ledger_number_i=" + ALedgerNumber +
                                    " AND a_batch_year_i=" + AYear +
                                    " AND a_batch_period_i=" + ABatchPeriod +
                                    " AND a_batch_status_c <> '" + MFinanceConstants.BATCH_POSTED + "'" +
                                    " AND a_batch_status_c <> '" + MFinanceConstants.BATCH_CANCELLED + "'" +
                                    " ORDER BY a_batch_number_i";
                    db.SelectDT(Fbatches, strSQL, transaction);
                });

            db.CloseDBConnection();
        }

        /// <summary>
        /// For a first overview you can read the number of rows and rows = 0 means that the
        /// batching jobs are done.
        /// </summary>
        public int NumberOfBatches
        {
            get
            {
                return Fbatches.Rows.Count;
            }
        }

        /// <summary>
        /// In case of an error you can create a string for the error message ...
        /// </summary>
        public override string ToString()
        {
            if (Fbatches.Rows.Count == 0)
            {
                return " - ";
            }

            string strList = "";

            foreach (ABatchRow Row in Fbatches.Rows)
            {
                if (strList != "")
                {
                    strList += ", ";
                }

                strList += String.Format("{0}: {1}", Row.BatchNumber, Row.BatchDescription);
            }

            return "(" + strList + ")";
        }
    }
}
