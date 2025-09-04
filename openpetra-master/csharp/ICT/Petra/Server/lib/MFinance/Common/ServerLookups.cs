//
// DO NOT REMOVE COPYRIGHT NOTICES OR THIS FILE HEADER.
//
// @Authors:
//       christiank, timop
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
//
using System;
using System.Data;
using System.Data.Odbc;

using Ict.Common;
using Ict.Common.DB;
using Ict.Common.Data;
using Ict.Common.Exceptions;

using Ict.Petra.Shared;
using Ict.Petra.Shared.MFinance;
using Ict.Petra.Shared.MFinance.GL.Data;
using Ict.Petra.Shared.MFinance.Account.Data;
using Ict.Petra.Shared.MSysMan;

using Ict.Petra.Server.App.Core.Security;
using Ict.Petra.Server.MCommon;
using Ict.Petra.Server.MFinance.Account.Data.Access;
using Ict.Petra.Server.MSysMan.Common.WebConnectors;

namespace Ict.Petra.Server.MFinance.Common.ServerLookups.WebConnectors
{
    /// <summary>
    /// Performs server-side lookups for the Client in the MFinance.Common.ServerLookups
    /// sub-namespace.
    ///
    /// </summary>
    public class TFinanceServerLookupWebConnector
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="ALedgerNumber"></param>
        /// <param name="AStartDateCurrentPeriod"></param>
        /// <param name="AEndDateCurrentPeriod"></param>
        /// <returns></returns>
        [RequireModulePermission("FINANCE-1")]
        public static Boolean GetCurrentPeriodDates(Int32 ALedgerNumber,
            out DateTime AStartDateCurrentPeriod,
            out DateTime AEndDateCurrentPeriod)
        {
            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();
            TDBTransaction Transaction = new TDBTransaction();
            TDataBase db = DBAccess.Connect("GetCurrentPeriodDates");

            db.ReadTransaction(
                ref Transaction,
                delegate
                {
                    ALedgerTable ledgerTable = ALedgerAccess.LoadByPrimaryKey(ALedgerNumber, Transaction);
                    AAccountingPeriodTable accountingPeriodTable = AAccountingPeriodAccess.LoadByPrimaryKey(ALedgerNumber,
                        ledgerTable[0].CurrentPeriod,
                        Transaction);
                    startDate = accountingPeriodTable[0].PeriodStartDate;
                    endDate = accountingPeriodTable[0].PeriodEndDate;
                });

            db.CloseDBConnection();

            AStartDateCurrentPeriod = startDate;
            AEndDateCurrentPeriod = endDate;
            return true;
        }

        /// <summary>
        /// Returns starting and ending posting dates for the ledger
        /// </summary>
        /// <param name="ALedgerNumber"></param>
        /// <param name="AStartDateCurrentPeriod"></param>
        /// <param name="AEndDateLastForwardingPeriod"></param>
        /// <returns>true if good data was returned</returns>
        [RequireModulePermission("FINANCE-1")]
        public static Boolean GetCurrentPostingRangeDates(Int32 ALedgerNumber,
            out DateTime AStartDateCurrentPeriod,
            out DateTime AEndDateLastForwardingPeriod)
        {
            Boolean dataIsOk = false;

            #region Validate Arguments

            if (ALedgerNumber <= 0)
            {
                throw new EFinanceSystemInvalidLedgerNumberException(String.Format(Catalog.GetString(
                            "Function:{0} - The Ledger number must be greater than 0!"),
                        Utilities.GetMethodName(true)), ALedgerNumber);
            }

            #endregion Validate Arguments

            DateTime StartDateCurrentPeriod = new DateTime();
            DateTime EndDateLastForwardingPeriod = new DateTime();

            TDBTransaction Transaction = new TDBTransaction();
            TDataBase db = DBAccess.Connect("GetCurrentPostingRangeDates");

            db.ReadTransaction(
                ref Transaction,
                delegate
                {
                    ALedgerTable ledgerTable = ALedgerAccess.LoadByPrimaryKey(ALedgerNumber, Transaction);

                    #region Validate Data

                    if ((ledgerTable == null) || (ledgerTable.Count == 0))
                    {
                        throw new EFinanceSystemDataTableReturnedNoDataException(String.Format(Catalog.GetString(
                                    "Function:{0} - Ledger data for Ledger number {1} does not exist or could not be accessed!"),
                                Utilities.GetMethodName(true),
                                ALedgerNumber));
                    }

                    #endregion Validate Data

                    int firstPostingPeriod = -1;
                    int lastPostingPeriod = -1;

                    // If final month end has been run but year end has not yet been run
                    // then we cannot post to the current period as it is actually closed.
                    if (ledgerTable[0].ProvisionalYearEndFlag)
                    {
                        firstPostingPeriod = ledgerTable[0].CurrentPeriod + 1;
                    }
                    else
                    {
                        firstPostingPeriod = ledgerTable[0].CurrentPeriod;
                    }

                    AAccountingPeriodTable accountingPeriodTable = AAccountingPeriodAccess.LoadByPrimaryKey(ALedgerNumber,
                        firstPostingPeriod,
                        Transaction);

                    #region Validate Data 2

                    if ((accountingPeriodTable == null) || (accountingPeriodTable.Count == 0))
                    {
                        throw new EFinanceSystemDataTableReturnedNoDataException(String.Format(Catalog.GetString(
                                    "Function:{0} - Accounting Period data for Ledger number {1} and posting period {2} does not exist or could not be accessed!"),
                                Utilities.GetMethodName(true),
                                ALedgerNumber,
                                firstPostingPeriod));
                    }

                    #endregion Validate Data 2

                    StartDateCurrentPeriod = accountingPeriodTable[0].PeriodStartDate;

                    lastPostingPeriod = ledgerTable[0].CurrentPeriod + ledgerTable[0].NumberFwdPostingPeriods;
                    accountingPeriodTable = AAccountingPeriodAccess.LoadByPrimaryKey(ALedgerNumber,
                        lastPostingPeriod,
                        Transaction);

                    #region Validate Data 3

                    if ((accountingPeriodTable == null) || (accountingPeriodTable.Count == 0))
                    {
                        throw new EFinanceSystemDataTableReturnedNoDataException(String.Format(Catalog.GetString(
                                    "Function:{0} - Accounting Period data for Ledger number {1} and posting period {2} does not exist or could not be accessed!"),
                                Utilities.GetMethodName(true),
                                ALedgerNumber,
                                lastPostingPeriod));
                    }

                    #endregion Validate Data 3

                    EndDateLastForwardingPeriod = accountingPeriodTable[0].PeriodEndDate;
                    dataIsOk = true;
                });

            AStartDateCurrentPeriod = StartDateCurrentPeriod;
            AEndDateLastForwardingPeriod = EndDateLastForwardingPeriod;

            db.CloseDBConnection();

            return dataIsOk;
        }

        /// <summary>
        /// return information if ledger with given number has suspense accounts set up
        /// </summary>
        /// <param name="ALedgerNumber"></param>
        /// <returns></returns>
        [RequireModulePermission("FINANCE-1")]
        public static Boolean HasSuspenseAccounts(Int32 ALedgerNumber)
        {
            Boolean ReturnValue = false;
            TDBTransaction transaction = new TDBTransaction();
            TDataBase db = DBAccess.Connect("HasSuspenseAccounts");

            db.ReadTransaction(
                ref transaction,
                delegate
                {
                    ReturnValue = (ASuspenseAccountAccess.CountViaALedger(ALedgerNumber, transaction) > 0);
                });

            db.CloseDBConnection();

            return ReturnValue;
        }

        /// <summary>
        /// return partner key associated with cost centre code in a_valid_ledger_number table
        /// returns false if cost centre type is not "Foreign" or if cost centre cannot be found in a_valid_ledger_number table
        /// </summary>
        /// <param name="ALedgerNumber"></param>
        /// <param name="ACostCentreCode"></param>
        /// <param name="APartnerKey"></param>
        /// <returns></returns>
        [RequireModulePermission("FINANCE-1")]
        public static Boolean GetPartnerKeyForForeignCostCentreCode(Int32 ALedgerNumber, String ACostCentreCode, out Int64 APartnerKey)
        {
            Boolean ReturnValue = false;

            Int64 PartnerKey = 0;

            TDBTransaction transaction = new TDBTransaction();
            TDataBase db = DBAccess.Connect("GetPartnerKeyForForeignCostCentreCode");

            db.ReadTransaction(ref transaction,
                delegate
                {
                    ACostCentreTable CostCentreTable;
                    CostCentreTable = ACostCentreAccess.LoadByPrimaryKey(ALedgerNumber, ACostCentreCode, transaction);

                    if (CostCentreTable.Count > 0)
                    {
                        ACostCentreRow CostCentreRow = (ACostCentreRow)CostCentreTable.Rows[0];

                        if (CostCentreRow.CostCentreType == MFinanceConstants.FOREIGN_CC_TYPE)
                        {
                            AValidLedgerNumberTable ValidLedgerNumberTable;
                            AValidLedgerNumberRow ValidLedgerNumberRow;
                            ValidLedgerNumberTable = AValidLedgerNumberAccess.LoadViaACostCentre(ALedgerNumber, ACostCentreCode, transaction);

                            if (ValidLedgerNumberTable.Count > 0)
                            {
                                ValidLedgerNumberRow = (AValidLedgerNumberRow)ValidLedgerNumberTable.Rows[0];
                                PartnerKey = ValidLedgerNumberRow.PartnerKey;
                                ReturnValue = true;
                            }
                        }
                    }
                });

            db.CloseDBConnection();

            APartnerKey = PartnerKey;
            return ReturnValue;
        }

        /// <summary>
        /// return ledger's base currency
        /// </summary>
        [RequireModulePermission("FINANCE-1")]
        public static string GetLedgerBaseCurrency(Int32 ALedgerNumber, TDataBase ADataBase = null)
        {
            string ReturnValue = "";
            TDBTransaction ReadTransaction = new TDBTransaction();
            TDataBase db = DBAccess.Connect("GetLedgerBaseCurrency", ADataBase);

            // Automatic handling of a Read-only DB Transaction - and also the automatic establishment and closing of a DB
            // Connection where a DB Transaction can be exectued (only if that should be needed).
            db.ReadTransaction(ref ReadTransaction,
                delegate
                {
                    ReturnValue = ((ALedgerRow)ALedgerAccess.LoadByPrimaryKey(ALedgerNumber, ReadTransaction).Rows[0]).BaseCurrency;
                });

            if (ADataBase == null)
            {
                db.CloseDBConnection();
            }

            return ReturnValue;
        }

        /// <summary>
        /// Get Foreign Currency Accounts' YTD Actuals
        /// </summary>
        /// <param name="AForeignCurrencyAccounts">DataTable containing rows of Foreign Currency Accounts</param>
        /// <param name="ALedgerNumber"></param>
        /// <param name="AYear"></param>
        [RequireModulePermission("FINANCE-1")]
        public static void GetForeignCurrencyAccountActuals(ref DataTable AForeignCurrencyAccounts, Int32 ALedgerNumber, Int32 AYear)
        {
            DataTable ForeignCurrencyAccounts = AForeignCurrencyAccounts;

            TDBTransaction Transaction = new TDBTransaction();
            TDataBase db = DBAccess.Connect("GetForeignCurrencyAccountActuals");

            db.ReadTransaction(ref Transaction,
                delegate
                {
                    AGeneralLedgerMasterTable glmTbl = new AGeneralLedgerMasterTable();
                    AGeneralLedgerMasterRow GLMTemplateRow = glmTbl.NewRowTyped(false);

                    foreach (DataRow ForeignCurrencyAccountRow in ForeignCurrencyAccounts.Rows)
                    {
                        GLMTemplateRow.LedgerNumber = ALedgerNumber;
                        GLMTemplateRow.Year = AYear;
                        GLMTemplateRow.AccountCode = ForeignCurrencyAccountRow[AAccountTable.GetAccountCodeDBName()].ToString();

                        glmTbl = AGeneralLedgerMasterAccess.LoadUsingTemplate(GLMTemplateRow, Transaction);
                        Decimal YtdActual = 0;
                        Decimal YtdActualForeign = 0;

                        if (glmTbl != null)
                        {
                            //
                            // I need to sum up all the GLM entries for this account:
                            foreach (AGeneralLedgerMasterRow glmRow in glmTbl.Rows)
                            {
                                if (!glmRow.IsYtdActualForeignNull())
                                {
                                    YtdActualForeign += glmRow.YtdActualForeign;
                                }

                                YtdActual += glmRow.YtdActualBase;
                            }
                        }

                        ForeignCurrencyAccountRow[AGeneralLedgerMasterTable.GetYtdActualBaseDBName()] = YtdActual;
                        ForeignCurrencyAccountRow[AGeneralLedgerMasterTable.GetYtdActualForeignDBName()] = YtdActualForeign;
                    }
                });

            db.CloseDBConnection();
        }

        /// <summary>
        /// Returns CurrencyLanguageRow for a corresponding currency code
        /// </summary>
        /// <param name="ACurrencyCode">Currency Code</param>
        /// <returns></returns>
        [RequireModulePermission("USER")]
        public static ACurrencyLanguageRow GetCurrencyLanguage(string ACurrencyCode)
        {
            ACurrencyLanguageRow ReturnValue = null;
            string Language = TUserDefaults.GetStringDefault(MSysManConstants.USERDEFAULT_UILANGUAGE);

            if (Language.Length > 2)
            {
                // need to get the two digit language code of p_language: de-DE => DE, en-EN => EN
                Language = Language.Substring(Language.Length - 2).ToUpper();
            }

            TDBTransaction Transaction = new TDBTransaction();
            TDataBase db = DBAccess.Connect("GetCurrencyLanguage");

            db.ReadTransaction(
                ref Transaction,
                delegate
                {
                    ACurrencyLanguageTable CurrencyLanguageTable = ACurrencyLanguageAccess.LoadByPrimaryKey(ACurrencyCode, Language, Transaction);

                    if ((CurrencyLanguageTable != null) && (CurrencyLanguageTable.Rows.Count > 0))
                    {
                        ReturnValue = CurrencyLanguageTable[0];
                    }
                });

            db.CloseDBConnection();

            return ReturnValue;
        }

        /// <summary>
        /// Returns a list of possible candidates for the account code
        /// </summary>
        [RequireModulePermission("FINANCE-1")]
        public static bool TypeAheadAccountCode(Int32 ALedgerNumber, string ASearch,
                bool APostingOnly,
                bool AExcludePosting,
                bool AActiveOnly,
                bool ABankAccountOnly,
                Int32 ALimit,
                out DataTable AResult)
        {
            DataTable result = new DataTable();
            TDBTransaction Transaction = new TDBTransaction();
            TDataBase db = DBAccess.Connect("TypeAheadAccountCode");
            db.ReadTransaction(
                ref Transaction,
                delegate
                {
                    string SqlStmt = TDataBase.ReadSqlFile("Finance.TypeAheadAccountCode.sql");

                    OdbcParameter[] parameters = new OdbcParameter[5];
                    parameters[0] = new OdbcParameter("LedgerNumber", OdbcType.Int);
                    parameters[0].Value = ALedgerNumber;
                    parameters[1] = new OdbcParameter("AccountCode", OdbcType.VarChar);
                    parameters[1].Value = "%" + ASearch + "%";
                    parameters[2] = new OdbcParameter("ShortDesc", OdbcType.VarChar);
                    parameters[2].Value = "%" + ASearch + "%";
                    parameters[3] = new OdbcParameter("LongDesc", OdbcType.VarChar);
                    parameters[3].Value = "%" + ASearch + "%";
                    parameters[4] = new OdbcParameter("PostingOnly", OdbcType.TinyInt);
                    parameters[4].Value = APostingOnly;

                    SqlStmt += " LIMIT " + ALimit.ToString();

                    result = db.SelectDT(SqlStmt, "Search", Transaction, parameters);
                });

            db.CloseDBConnection();

            AResult = result;
            return result.Rows.Count > 0;
        }

        /// <summary>
        /// Returns a list of possible candidates for the cost centre code
        /// </summary>
        [RequireModulePermission("FINANCE-1")]
        public static bool TypeAheadCostCentreCode(Int32 ALedgerNumber, string ASearch,
                bool APostingOnly,
                bool AExcludePosting,
                bool AActiveOnly,
                bool ALocalOnly,
                bool AIndicateInactive,
                Int32 ALimit,
                out DataTable AResult)
        {
            TDBTransaction Transaction = new TDBTransaction();
            TDataBase db = DBAccess.Connect("TypeAheadCostCentreCode");
            DataTable result = new DataTable();

            db.ReadTransaction(
                ref Transaction,
                delegate
                {
                    string SqlStmt = TDataBase.ReadSqlFile("Finance.TypeAheadCostCentreCode.sql");

                    OdbcParameter[] parameters = new OdbcParameter[4];
                    parameters[0] = new OdbcParameter("LedgerNumber", OdbcType.Int);
                    parameters[0].Value = ALedgerNumber;
                    parameters[1] = new OdbcParameter("CostCentreCode", OdbcType.VarChar);
                    parameters[1].Value = "%" + ASearch + "%";
                    parameters[2] = new OdbcParameter("CostCentreName", OdbcType.VarChar);
                    parameters[2].Value = "%" + ASearch + "%";
                    parameters[3] = new OdbcParameter("PostingOnly", OdbcType.TinyInt);
                    parameters[3].Value = APostingOnly;

                    SqlStmt += " LIMIT " + ALimit.ToString();

                    result = db.SelectDT(SqlStmt, "Search", Transaction, parameters);
                });

            db.CloseDBConnection();

            AResult = result;
            return result.Rows.Count > 0;
        }

        /// <summary>
        /// Returns a list of possible candidates for the motivation group
        /// </summary>
        [RequireModulePermission("OR(FINANCE-1,SPONSORADMIN)")]
        public static bool TypeAheadMotivationGroup(Int32 ALedgerNumber, string ASearch,
                Int32 ALimit,
                out DataTable AResult)
        {
            TDBTransaction Transaction = new TDBTransaction();
            TDataBase db = DBAccess.Connect("TypeAheadMotivationGroup");
            DataTable result = new DataTable();

            db.ReadTransaction(
                ref Transaction,
                delegate
                {
                    string SqlStmt = TDataBase.ReadSqlFile("Finance.TypeAheadMotivationGroup.sql");

                    OdbcParameter[] parameters = new OdbcParameter[3];
                    parameters[0] = new OdbcParameter("LedgerNumber", OdbcType.Int);
                    parameters[0].Value = ALedgerNumber;
                    parameters[1] = new OdbcParameter("MotivationGroupCode", OdbcType.VarChar);
                    parameters[1].Value = "%" + ASearch + "%";
                    parameters[2] = new OdbcParameter("DescGroup", OdbcType.VarChar);
                    parameters[2].Value = "%" + ASearch + "%";

                    SqlStmt += " LIMIT " + ALimit.ToString();

                    result = db.SelectDT(SqlStmt, "Search", Transaction, parameters);
                });

            db.CloseDBConnection();

            AResult = result;
            return result.Rows.Count > 0;
        }

        /// <summary>
        /// Returns a list of possible candidates for the motivation detail
        /// </summary>
        [RequireModulePermission("OR(FINANCE-1,SPONSORADMIN)")]
        public static bool TypeAheadMotivationDetail(Int32 ALedgerNumber, string ASearch,
                Int32 ALimit,
                out DataTable AResult)
        {
            TDBTransaction Transaction = new TDBTransaction();
            TDataBase db = DBAccess.Connect("TypeAheadMotivationDetail");
            DataTable result = new DataTable();

            db.ReadTransaction(
                ref Transaction,
                delegate
                {
                    string SqlStmt = TDataBase.ReadSqlFile("Finance.TypeAheadMotivationDetail.sql");

                    OdbcParameter[] parameters = new OdbcParameter[3];
                    parameters[0] = new OdbcParameter("LedgerNumber", OdbcType.Int);
                    parameters[0].Value = ALedgerNumber;
                    parameters[1] = new OdbcParameter("MotivationDetailCode", OdbcType.VarChar);
                    parameters[1].Value = "%" + ASearch + "%";
                    parameters[2] = new OdbcParameter("DescDetail", OdbcType.VarChar);
                    parameters[2].Value = "%" + ASearch + "%";

                    SqlStmt += " LIMIT " + ALimit.ToString();

                    result = db.SelectDT(SqlStmt, "Search", Transaction, parameters);
                });

            db.CloseDBConnection();

            AResult = result;
            return result.Rows.Count > 0;
        }
    }
}
