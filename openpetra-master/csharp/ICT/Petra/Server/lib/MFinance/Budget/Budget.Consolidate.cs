//
// DO NOT REMOVE COPYRIGHT NOTICES OR THIS FILE HEADER.
//
// @Authors:
//       timop, christophert
//
// Copyright 2004-2019 by OM International
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
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using Ict.Common.DB;
using Ict.Petra.Shared.MFinance.Account.Data;
using Ict.Petra.Shared.MFinance.GL.Data;
using Ict.Petra.Server.MFinance.Account.Data.Access;
using Ict.Petra.Server.MFinance.Common;
using Ict.Petra.Server.MFinance.GL.Data.Access;
using Ict.Petra.Server.App.Core.Security;

namespace Ict.Petra.Server.MFinance.Budget.WebConnectors
{
    /// <summary>
    /// maintain the budget
    /// </summary>
    public class TBudgetConsolidateWebConnector
    {
        private BudgetTDS FBudgetTDS = null;
        private GLPostingTDS FGLPostingDS = null;

        /// <summary>
        /// load budgets
        /// </summary>
        /// <param name="ALedgerNumber"></param>
        /// <returns></returns>
        private bool LoadBudgetForConsolidate(Int32 ALedgerNumber)
        {
            FBudgetTDS = new BudgetTDS();

            TDBTransaction Transaction = new TDBTransaction();
            TDataBase db = DBAccess.Connect("Budget");
            db.ReadTransaction(
                ref Transaction,
                delegate
                {
                    ALedgerAccess.LoadByPrimaryKey(FBudgetTDS, ALedgerNumber, Transaction);

                    string sqlLoadBudgetForThisAndNextYear =
                        string.Format("SELECT * FROM PUB_{0} WHERE {1}=? AND ({2} = ? OR {2} = ?)",
                            ABudgetTable.GetTableDBName(),
                            ABudgetTable.GetLedgerNumberDBName(),
                            ABudgetTable.GetYearDBName());

                    List <OdbcParameter>parameters = new List <OdbcParameter>();
                    OdbcParameter param = new OdbcParameter("ledgernumber", OdbcType.Int);
                    param.Value = ALedgerNumber;
                    parameters.Add(param);
                    param = new OdbcParameter("thisyear", OdbcType.Int);
                    param.Value = FBudgetTDS.ALedger[0].CurrentFinancialYear;
                    parameters.Add(param);
                    param = new OdbcParameter("nextyear", OdbcType.Int);
                    param.Value = FBudgetTDS.ALedger[0].CurrentFinancialYear + 1;
                    parameters.Add(param);

                    db.Select(FBudgetTDS, sqlLoadBudgetForThisAndNextYear, FBudgetTDS.ABudget.TableName, Transaction,
                        parameters.ToArray());

                    string sqlLoadBudgetPeriodForThisAndNextYear =
                        string.Format("SELECT {0}.* FROM PUB_{0}, PUB_{1} WHERE {0}.a_budget_sequence_i = {1}.a_budget_sequence_i AND " +
                            "{2}=? AND ({3} = ? OR {3} = ?)",
                            ABudgetPeriodTable.GetTableDBName(),
                            ABudgetTable.GetTableDBName(),
                            ABudgetTable.GetLedgerNumberDBName(),
                            ABudgetTable.GetYearDBName());

                    db.Select(FBudgetTDS,
                        sqlLoadBudgetPeriodForThisAndNextYear,
                        FBudgetTDS.ABudgetPeriod.TableName,
                        Transaction,
                        parameters.ToArray());

                    // Accept row changes here so that the Client gets 'unmodified' rows
                    FBudgetTDS.AcceptChanges();

                    FGLPostingDS = new GLPostingTDS();
                    AAccountAccess.LoadViaALedger(FGLPostingDS, ALedgerNumber, Transaction);
                    AAccountHierarchyDetailAccess.LoadViaALedger(FGLPostingDS, ALedgerNumber, Transaction);
                    ACostCentreAccess.LoadViaALedger(FGLPostingDS, ALedgerNumber, Transaction);
                    ALedgerAccess.LoadByPrimaryKey(FGLPostingDS, ALedgerNumber, Transaction);

                    // get the glm sequences for this year and next year
                    for (int i = 0; i <= 1; i++)
                    {
                        int Year = FGLPostingDS.ALedger[0].CurrentFinancialYear + i;

                        AGeneralLedgerMasterRow TemplateRow = (AGeneralLedgerMasterRow)FGLPostingDS.AGeneralLedgerMaster.NewRowTyped(false);

                        TemplateRow.LedgerNumber = ALedgerNumber;
                        TemplateRow.Year = Year;

                        FGLPostingDS.AGeneralLedgerMaster.Merge(AGeneralLedgerMasterAccess.LoadUsingTemplate(TemplateRow, Transaction));
                    }

                    string sqlLoadGLMPeriodForThisAndNextYear =
                        string.Format("SELECT {0}.* FROM PUB_{0}, PUB_{1} WHERE {0}.a_glm_sequence_i = {1}.a_glm_sequence_i AND " +
                            "{2}=? AND ({3} = ? OR {3} = ?)",
                            AGeneralLedgerMasterPeriodTable.GetTableDBName(),
                            AGeneralLedgerMasterTable.GetTableDBName(),
                            AGeneralLedgerMasterTable.GetLedgerNumberDBName(),
                            AGeneralLedgerMasterTable.GetYearDBName());

                    db.Select(FGLPostingDS,
                        sqlLoadGLMPeriodForThisAndNextYear,
                        FGLPostingDS.AGeneralLedgerMasterPeriod.TableName,
                        Transaction,
                        parameters.ToArray());
                });

            FGLPostingDS.AcceptChanges();

            return true;
        }

        /// <summary>
        /// Consolidate Budgets.
        /// </summary>
        /// <param name="ALedgerNumber"></param>
        /// <param name="AConsolidateAll"></param>
        [RequireModulePermission("FINANCE-3")]
        public static void ConsolidateBudgets(Int32 ALedgerNumber, bool AConsolidateAll)
        {
            TBudgetConsolidateWebConnector myObject = new TBudgetConsolidateWebConnector();
            myObject.LoadBudgetForConsolidate(ALedgerNumber);
            myObject.ConsolidateBudgetsInternal(ALedgerNumber, AConsolidateAll);
        }

        private void ConsolidateBudgetsInternal(Int32 ALedgerNumber, bool AConsolidateAll)
        {
            TDBTransaction Transaction = new TDBTransaction();
            Boolean SubmissionOK = false;
            TDataBase db = DBAccess.Connect("Budget");

            db.WriteTransaction(ref Transaction, ref SubmissionOK,
                delegate
                {
                    ALedgerRow LedgerRow = FBudgetTDS.ALedger[0];

                    // first clear the old budget from GLMPeriods
                    if (AConsolidateAll)
                    {
                        foreach (ABudgetRow BudgetRow in FBudgetTDS.ABudget.Rows)
                        {
                            BudgetRow.BudgetStatus = false;
                        }

                        foreach (AGeneralLedgerMasterRow GeneralLedgerMasterRow in FGLPostingDS.AGeneralLedgerMaster.Rows)
                        {
                            for (int Period = 1; Period <= LedgerRow.NumberOfAccountingPeriods; Period++)
                            {
                                ClearAllBudgetValues(GeneralLedgerMasterRow.GlmSequence, Period);
                            }
                        }
                    }
                    else
                    {
                        foreach (ABudgetRow BudgetRow in FBudgetTDS.ABudget.Rows)
                        {
                            if (!BudgetRow.BudgetStatus)
                            {
                                UnPostBudget(BudgetRow, ALedgerNumber);
                            }
                        }
                    }

                    foreach (ABudgetRow BudgetRow in FBudgetTDS.ABudget.Rows)
                    {
                        if (!BudgetRow.BudgetStatus || AConsolidateAll)
                        {
                            List <ABudgetPeriodRow>budgetPeriods = new List <ABudgetPeriodRow>();

                            FBudgetTDS.ABudgetPeriod.DefaultView.RowFilter = ABudgetPeriodTable.GetBudgetSequenceDBName() + " = " +
                                                                             BudgetRow.BudgetSequence.ToString();

                            foreach (DataRowView rv in FBudgetTDS.ABudgetPeriod.DefaultView)
                            {
                                budgetPeriods.Add((ABudgetPeriodRow)rv.Row);
                            }

                            PostBudget(ALedgerNumber, BudgetRow, budgetPeriods);
                        }
                    }

                    /*Consolidate_Budget*/
                    foreach (ABudgetRow BudgetRow in FBudgetTDS.ABudget.Rows)
                    {
                        BudgetRow.BudgetStatus = true;
                    }

                    ABudgetAccess.SubmitChanges(FBudgetTDS.ABudget, Transaction);

                    FGLPostingDS.ThrowAwayAfterSubmitChanges = true;
                    GLPostingTDSAccess.SubmitChanges(FGLPostingDS, Transaction.DataBaseObj);
                    FGLPostingDS.Clear();

                    SubmissionOK = true;
                });
        }

        /// <summary>
        /// Return the budget amount from the temp table APeriodDataTable.
        ///   if the record is not already in the temp table, it is fetched
        /// </summary>
        /// <param name="APeriodDataTable"></param>
        /// <param name="AGLMSequence"></param>
        /// <param name="APeriodNumber"></param>
        /// <returns></returns>
        [RequireModulePermission("FINANCE-3")]
        public static decimal GetBudgetValue(ref DataTable APeriodDataTable, int AGLMSequence, int APeriodNumber)
        {
            decimal GetBudgetValue = 0;

            DataRow TempRow = (DataRow)APeriodDataTable.Rows.Find(new object[] { AGLMSequence, APeriodNumber });

            if (TempRow == null)
            {
                AGeneralLedgerMasterPeriodTable GeneralLedgerMasterPeriodTable = null;
                AGeneralLedgerMasterPeriodRow GeneralLedgerMasterPeriodRow = null;

                TDataBase db = DBAccess.Connect("Budget");
                TDBTransaction transaction = new TDBTransaction();
                db.ReadTransaction(
                    ref transaction,
                    delegate
                    {
                        GeneralLedgerMasterPeriodTable = AGeneralLedgerMasterPeriodAccess.LoadByPrimaryKey(AGLMSequence, APeriodNumber, transaction);
                    });

                if (GeneralLedgerMasterPeriodTable.Count > 0)
                {
                    GeneralLedgerMasterPeriodRow = (AGeneralLedgerMasterPeriodRow)GeneralLedgerMasterPeriodTable.Rows[0];

                    DataRow DR = (DataRow)APeriodDataTable.NewRow();
                    DR["GLMSequence"] = AGLMSequence;
                    DR["PeriodNumber"] = APeriodNumber;
                    DR["BudgetBase"] = GeneralLedgerMasterPeriodRow.BudgetBase;

                    APeriodDataTable.Rows.Add(DR);
                }
            }
            else
            {
                //Set to budget base
                GetBudgetValue = Convert.ToDecimal(TempRow["BudgetBase"]);
            }

            return GetBudgetValue;
        }

        /// <summary>
        /// Unpost a budget
        /// </summary>
        /// <param name="ABudgetRow"></param>
        /// <param name="ALedgerNumber"></param>
        /// <returns>true if it seemed to go OK</returns>
        private bool UnPostBudget(ABudgetRow ABudgetRow, int ALedgerNumber)
        {
            /* post the negative budget, which will result in an empty a_glm_period.budget */

            // get the current budget value for each GLM Period, and unpost that budget

            FGLPostingDS.AGeneralLedgerMaster.DefaultView.Sort = String.Format("{0},{1},{2},{3}",
                AGeneralLedgerMasterTable.GetLedgerNumberDBName(),
                AGeneralLedgerMasterTable.GetYearDBName(),
                AGeneralLedgerMasterTable.GetAccountCodeDBName(),
                AGeneralLedgerMasterTable.GetCostCentreCodeDBName());

            int glmIndex = FGLPostingDS.AGeneralLedgerMaster.DefaultView.Find(
                new object[] { ALedgerNumber, ABudgetRow.Year, ABudgetRow.AccountCode, ABudgetRow.CostCentreCode });

            if (glmIndex != -1)
            {
                AGeneralLedgerMasterRow glmRow = (AGeneralLedgerMasterRow)FGLPostingDS.AGeneralLedgerMaster.DefaultView[glmIndex].Row;

                List <ABudgetPeriodRow>budgetPeriods = new List <ABudgetPeriodRow>();

                for (int Period = 1; Period <= FGLPostingDS.ALedger[0].NumberOfAccountingPeriods; Period++)
                {
                    AGeneralLedgerMasterPeriodRow GeneralLedgerMasterPeriodRow =
                        (AGeneralLedgerMasterPeriodRow)FGLPostingDS.AGeneralLedgerMasterPeriod.Rows.Find(
                            new object[] { glmRow.GlmSequence, Period });

                    ABudgetPeriodRow budgetPeriodRow = FBudgetTDS.ABudgetPeriod.NewRowTyped(true);
                    budgetPeriodRow.PeriodNumber = Period;
                    budgetPeriodRow.BudgetSequence = ABudgetRow.BudgetSequence;

                    // use negative amount for unposting
                    budgetPeriodRow.BudgetBase = -1 * GeneralLedgerMasterPeriodRow.BudgetBase;

                    // do not add to the budgetperiod table, but to our local list
                    budgetPeriods.Add(budgetPeriodRow);
                }

                PostBudget(ALedgerNumber, ABudgetRow, budgetPeriods);
            }

            ABudgetRow.BudgetStatus = false;                 //i.e. unposted

            return true;
        }

        /// <summary>
        /// Post a budget
        /// </summary>
        private void PostBudget(int ALedgerNumber, ABudgetRow ABudgetRow, List <ABudgetPeriodRow>ABudgetPeriodRows)
        {
            FGLPostingDS.AGeneralLedgerMaster.DefaultView.Sort = String.Format("{0},{1},{2},{3}",
                AGeneralLedgerMasterTable.GetLedgerNumberDBName(),
                AGeneralLedgerMasterTable.GetYearDBName(),
                AGeneralLedgerMasterTable.GetAccountCodeDBName(),
                AGeneralLedgerMasterTable.GetCostCentreCodeDBName());

            int glmRowIndex = FGLPostingDS.AGeneralLedgerMaster.DefaultView.Find(new object[] {
                    ALedgerNumber,
                    ABudgetRow.Year,
                    ABudgetRow.AccountCode,
                    ABudgetRow.CostCentreCode
                });

            if (glmRowIndex == -1)
            {
                TGLPosting.CreateGLMYear(ref FGLPostingDS,
                    ALedgerNumber,
                    ABudgetRow.Year,
                    ABudgetRow.AccountCode,
                    ABudgetRow.CostCentreCode);
                glmRowIndex = FGLPostingDS.AGeneralLedgerMaster.DefaultView.Find(new object[] {
                        ALedgerNumber,
                        ABudgetRow.Year,
                        ABudgetRow.AccountCode,
                        ABudgetRow.CostCentreCode
                    });
            }

            int GLMSequence = ((AGeneralLedgerMasterRow)FGLPostingDS.AGeneralLedgerMaster.DefaultView[glmRowIndex].Row).GlmSequence;

            /* Update totals for the General Ledger Master period record. */
            foreach (ABudgetPeriodRow BPR in ABudgetPeriodRows)
            {
                AddBudgetValue(GLMSequence, BPR.PeriodNumber, BPR.BudgetBase);
            }
        }

        /// <summary>
        /// Write a budget value to the temporary table
        /// </summary>
        /// <param name="AGLMSequence"></param>
        /// <param name="APeriodNumber"></param>
        /// <param name="APeriodAmount"></param>
        private void AddBudgetValue(int AGLMSequence, int APeriodNumber, decimal APeriodAmount)
        {
            AGeneralLedgerMasterPeriodRow GeneralLedgerMasterPeriodRow =
                (AGeneralLedgerMasterPeriodRow)FGLPostingDS.AGeneralLedgerMasterPeriod.Rows.Find(
                    new object[] { AGLMSequence, APeriodNumber });

            if (GeneralLedgerMasterPeriodRow != null)
            {
                GeneralLedgerMasterPeriodRow.BudgetBase += APeriodAmount;
            }
            else
            {
                throw new Exception("AddBudgetValue: cannot find glmp record for " + AGLMSequence.ToString() + " " + APeriodNumber.ToString());
            }
        }

        /// <summary>
        /// Reset the budget amount in the temp table wtPeriodData.
        ///   if the record is not already in the temp table, it is created empty
        /// </summary>
        /// <param name="AGLMSequence"></param>
        /// <param name="APeriodNumber"></param>
        private void ClearAllBudgetValues(int AGLMSequence, int APeriodNumber)
        {
            AGeneralLedgerMasterPeriodRow GeneralLedgerMasterPeriodRow =
                (AGeneralLedgerMasterPeriodRow)FGLPostingDS.AGeneralLedgerMasterPeriod.Rows.Find(new object[] { AGLMSequence, APeriodNumber });

            if (GeneralLedgerMasterPeriodRow != null)
            {
                GeneralLedgerMasterPeriodRow.BudgetBase = 0;
            }
        }
    }
}
