//
// DO NOT REMOVE COPYRIGHT NOTICES OR THIS FILE HEADER.
//
// @Authors:
//       wolfgangu, timop
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

using System;
using System.Data.Odbc;
using NUnit.Framework;
using Ict.Testing.NUnitTools;
using Ict.Testing.NUnitPetraServer;
using Ict.Petra.Server.MFinance.GL;
using Ict.Petra.Server.MFinance.Common;
using Ict.Common;
using Ict.Common.Verification;

using Ict.Petra.Server.MFinance.Account.Data.Access;
using Ict.Petra.Shared.MFinance.Account.Data;
using Ict.Petra.Server.MFinance.GL.WebConnectors;
using Ict.Petra.Shared.MCommon.Data;
using Ict.Petra.Server.MCommon.Data.Access;
using Ict.Petra.Server.MFinance.Cacheable;
using Ict.Petra.Server.MFinance.Setup.WebConnectors;

using Ict.Common.DB;
using Ict.Petra.Server.MFinance.Gift.Data.Access;
using Ict.Petra.Server.MPartner.Partner.Data.Access;
using Ict.Petra.Shared;
using Ict.Petra.Shared.MFinance;
using Ict.Petra.Shared.MFinance.Gift.Data;
using Ict.Petra.Shared.MFinance.GL.Data;
using Ict.Petra.Shared.MPartner.Partner.Data;
using System.Collections.Generic;
using System.Data;

namespace Ict.Testing.Petra.Server.MFinance.GL
{
    /// <summary>
    /// Test of the GL.PeriodEnd.Year routines ...
    /// </summary>
    [TestFixture]
    public class TestGLPeriodicEndYear
    {
        private int intLedgerNumber = 43;

        /// <summary>
        /// Test_YearEnd
        /// </summary>
        [Test]
        public void Test_YearEnd()
        {
            intLedgerNumber = CommonNUnitFunctions.CreateNewLedger();
            TDataBase db = DBAccess.Connect("Test_YearEnd");

            TLedgerInfo LedgerInfo = new TLedgerInfo(intLedgerNumber, db);
            Assert.AreEqual(0, LedgerInfo.CurrentFinancialYear, "Before YearEnd, we should be in year 0");

            TAccountPeriodInfo periodInfo = new TAccountPeriodInfo(intLedgerNumber, 1);
            Assert.AreEqual(new DateTime(DateTime.Now.Year,
                    1,
                    1), periodInfo.PeriodStartDate, "Calendar from base database should start with January 1st of this year");

            CommonNUnitFunctions.LoadTestDataBase("csharp\\ICT\\Testing\\lib\\MFinance\\server\\GL\\test-sql\\gl-test-year-end.sql", intLedgerNumber);

            TDBTransaction TestBatchTransaction = db.BeginTransaction(IsolationLevel.Serializable, -1, "Test_YearEnd.PrepareBatches");

            TCommonAccountingTool commonAccountingTool =
                new TCommonAccountingTool(intLedgerNumber, "NUNIT", db);
            commonAccountingTool.AddBaseCurrencyJournal();
            commonAccountingTool.JournalDescription = "Test Data accounts";
            string strAccountGift = "0200";
            string strAccountBank = "6200";
            string strAccountExpense = "4100";

            // Accounting of some gifts ...
            commonAccountingTool.AddBaseCurrencyTransaction(
                strAccountBank, "4301", "Gift Example", "Debit", MFinanceConstants.IS_DEBIT, 100);
            commonAccountingTool.AddBaseCurrencyTransaction(
                strAccountBank, "4302", "Gift Example", "Debit", MFinanceConstants.IS_DEBIT, 200);
            commonAccountingTool.AddBaseCurrencyTransaction(
                strAccountBank, "4303", "Gift Example", "Debit", MFinanceConstants.IS_DEBIT, 300);

            commonAccountingTool.AddBaseCurrencyTransaction(
                strAccountGift, "4301", "Gift Example", "Credit", MFinanceConstants.IS_CREDIT, 100);
            commonAccountingTool.AddBaseCurrencyTransaction(
                strAccountGift, "4302", "Gift Example", "Credit", MFinanceConstants.IS_CREDIT, 200);
            commonAccountingTool.AddBaseCurrencyTransaction(
                strAccountGift, "4303", "Gift Example", "Credit", MFinanceConstants.IS_CREDIT, 300);


            // Accounting of some expenses ...

            commonAccountingTool.AddBaseCurrencyTransaction(
                strAccountExpense, "4301", "Expense Example", "Debit", MFinanceConstants.IS_DEBIT, 150);
            commonAccountingTool.AddBaseCurrencyTransaction(
                strAccountExpense, "4302", "Expense Example", "Debit", MFinanceConstants.IS_DEBIT, 150);
            commonAccountingTool.AddBaseCurrencyTransaction(
                strAccountExpense, "4303", "Expense Example", "Debit", MFinanceConstants.IS_DEBIT, 200);

            commonAccountingTool.AddBaseCurrencyTransaction(
                strAccountBank, "4301", "Expense Example", "Credit", MFinanceConstants.IS_CREDIT, 150);
            commonAccountingTool.AddBaseCurrencyTransaction(
                strAccountBank, "4302", "Expense Example", "Credit", MFinanceConstants.IS_CREDIT, 150);
            commonAccountingTool.AddBaseCurrencyTransaction(
                strAccountBank, "4303", "Expense Example", "Credit", MFinanceConstants.IS_CREDIT, 200);

            TVerificationResultCollection verificationResult = new TVerificationResultCollection();

            commonAccountingTool.CloseSaveAndPost(verificationResult, db); // returns true if posting seemed to work

            TestBatchTransaction.Commit();

            bool blnLoop = true;

            while (blnLoop)
            {
                TDBTransaction PeriodEndTransaction = db.BeginTransaction(IsolationLevel.Serializable, -1, "Test_2YearEnds.PeriodEnd");
                TLedgerInfo LedgerInfo2 = new TLedgerInfo(intLedgerNumber, db);

                if (LedgerInfo2.ProvisionalYearEndFlag)
                {
                    blnLoop = false;
                }
                else
                {
                    TVerificationResultCollection VerificationResult;
                    List <Int32>glBatchNumbers;
                    Boolean stewardshipBatch;

                    TPeriodIntervalConnector.PeriodMonthEnd(
                        intLedgerNumber, false,
                        out glBatchNumbers,
                        out stewardshipBatch,
                        out VerificationResult,
                        db);
                    CommonNUnitFunctions.EnsureNullOrOnlyNonCriticalVerificationResults(VerificationResult,
                        "Running MonthEnd gave critical error");
                }

                PeriodEndTransaction.Commit();
            }

            // check before year end that income and expense accounts are not 0
            int intYear = 0;
            CheckGLMEntry(intLedgerNumber, intYear, strAccountBank,
                -50, 0, 50, 0, 100, 0);
            CheckGLMEntry(intLedgerNumber, intYear, strAccountExpense,
                150, 0, 150, 0, 200, 0);
            CheckGLMEntry(intLedgerNumber, intYear, strAccountGift,
                100, 0, 200, 0, 300, 0);

            // test that we cannot post to period 12 anymore, all periods are closed?
            LedgerInfo = new TLedgerInfo(intLedgerNumber, db);
            Assert.AreEqual(true, LedgerInfo.ProvisionalYearEndFlag, "Provisional YearEnd flag should be set");


            List <Int32>glBatches = new List <int>();
            TDBTransaction transaction = new TDBTransaction();
            bool SubmissionOK = false;

            db.WriteTransaction(
                ref transaction,
                ref SubmissionOK,
                delegate
                {
                    //
                    // Reallocation is never called explicitly like this - it's not really appropriate
                    // because I'm about to call it again as part of YearEnd, below.
                    // But a tweak in the reallocation code means that it should now cope with being called twice.
                    TReallocation reallocation = new TReallocation(LedgerInfo, glBatches, transaction);
                    reallocation.VerificationResultCollection = verificationResult;
                    reallocation.IsInInfoMode = false;
                    reallocation.RunOperation();
                    SubmissionOK = true;
                });

            // check amounts after reallocation
            CheckGLMEntry(intLedgerNumber, intYear, strAccountBank,
                -50, 0, 50, 0, 100, 0);
            CheckGLMEntry(intLedgerNumber, intYear, strAccountExpense,
                0, -150, 0, -150, 0, -200);
            CheckGLMEntry(intLedgerNumber, intYear, strAccountGift,
                0, -100, 0, -200, 0, -300);

            // first run in info mode
            TPeriodIntervalConnector.PeriodYearEnd(intLedgerNumber, true,
                out glBatches,
                out verificationResult, db);
            CommonNUnitFunctions.EnsureNullOrOnlyNonCriticalVerificationResults(verificationResult,
                "YearEnd test should not have critical errors");

            transaction = db.BeginTransaction(IsolationLevel.Serializable);
            // now run for real
            TPeriodIntervalConnector.PeriodYearEnd(intLedgerNumber, false,
                out glBatches,
                out verificationResult, db);
            CommonNUnitFunctions.EnsureNullOrOnlyNonCriticalVerificationResults(verificationResult,
                "YearEnd should not have critical errors");

            transaction.Commit();
            transaction = db.BeginTransaction(IsolationLevel.ReadCommitted);

            ++intYear;
            // check after year end that income and expense accounts are 0, bank account remains
            CheckGLMEntry(intLedgerNumber, intYear, strAccountBank,
                -50, 0, 50, 0, 100, 0);
            CheckGLMEntry(intLedgerNumber, intYear, strAccountExpense,
                0, 0, 0, 0, 0, 0);
            CheckGLMEntry(intLedgerNumber, intYear, strAccountGift,
                0, 0, 0, 0, 0, 0);

            // also check the glm period records
            CheckGLMPeriodEntry(intLedgerNumber, intYear, 1, strAccountBank,
                -50, 50, 100, db);
            CheckGLMPeriodEntry(intLedgerNumber, intYear, 1, strAccountExpense,
                0, 0, 0, db);
            CheckGLMPeriodEntry(intLedgerNumber, intYear, 1, strAccountGift,
                0, 0, 0, db);

            // 9700 is the account that the expenses and income from last year is moved to
            TGlmInfo glmInfo = new TGlmInfo(intLedgerNumber, intYear, "9700", db);
            glmInfo.Reset();
            Assert.IsTrue(glmInfo.MoveNext(), "9700 account not found");

            Assert.AreEqual(100, glmInfo.YtdActualBase);
            Assert.AreEqual(0, glmInfo.ClosingPeriodActualBase);

            LedgerInfo = new TLedgerInfo(intLedgerNumber, db);
            Assert.AreEqual(1, LedgerInfo.CurrentFinancialYear, "After YearEnd, we are in a new financial year");
            Assert.AreEqual(1, LedgerInfo.CurrentPeriod, "After YearEnd, we are in Period 1");
            Assert.AreEqual(false, LedgerInfo.ProvisionalYearEndFlag, "After YearEnd, ProvisionalYearEnd flag should not be set");

            periodInfo = new TAccountPeriodInfo(intLedgerNumber, 1, db);
            Assert.AreEqual(new DateTime(DateTime.Now.Year + 1,
                    1,
                    1), periodInfo.PeriodStartDate, "new Calendar should start with January 1st of next year");
            transaction.Rollback();
        }

        /// <summary>
        /// test 2 consecutive year ends
        /// </summary>
        [Test]
        public void Test_2YearEnds()
        {
            intLedgerNumber = CommonNUnitFunctions.CreateNewLedger();
            CommonNUnitFunctions.LoadTestDataBase("csharp\\ICT\\Testing\\lib\\MFinance\\server\\GL\\test-sql\\gl-test-year-end.sql", intLedgerNumber);
            TDataBase db = DBAccess.Connect("Test_2YearEnds");

            for (int countYear = 0; countYear < 2; countYear++)
            {
                TLogging.Log("preparing year number " + countYear.ToString());

                TDBTransaction TestBatchTransaction = db.BeginTransaction(IsolationLevel.Serializable, -1, "Test_2YearEnds.PrepareBatches");

                // accounting one gift
                string strAccountGift = "0200";
                string strAccountBank = "6200";
                TCommonAccountingTool commonAccountingTool =
                    new TCommonAccountingTool(intLedgerNumber, "NUNIT", db);
                commonAccountingTool.AddBaseCurrencyJournal();
                commonAccountingTool.JournalDescription = "Test Data accounts";
                commonAccountingTool.AddBaseCurrencyTransaction(
                    strAccountBank, "4301", "Gift Example", "Debit", MFinanceConstants.IS_DEBIT, 100);
                commonAccountingTool.AddBaseCurrencyTransaction(
                    strAccountGift, "4301", "Gift Example", "Credit", MFinanceConstants.IS_CREDIT, 100);
                TVerificationResultCollection verificationResult = new TVerificationResultCollection();
                Boolean PostedOk = commonAccountingTool.CloseSaveAndPost(verificationResult, db); // returns true if posting seemed to work
                Assert.AreEqual(true, PostedOk, "Test batch can't be posted");

                TestBatchTransaction.Commit();

                bool blnLoop = true;

                while (blnLoop)
                {
                    TDBTransaction PeriodEndTransaction = db.BeginTransaction(IsolationLevel.Serializable, -1, "Test_2YearEnds.PeriodEnd");
                    TLedgerInfo LedgerInfo = new TLedgerInfo(intLedgerNumber, db);

                    if (LedgerInfo.ProvisionalYearEndFlag)
                    {
                        blnLoop = false;
                    }
                    else
                    {
                        List <Int32>glBatchNumbers;
                        Boolean stewardshipBatch;
                        TVerificationResultCollection VerificationResult;

                        TPeriodIntervalConnector.PeriodMonthEnd(
                            intLedgerNumber,
                            false,
                            out glBatchNumbers,
                            out stewardshipBatch,
                            out VerificationResult,
                            db);
                        CommonNUnitFunctions.EnsureNullOrOnlyNonCriticalVerificationResults(VerificationResult,
                            "MonthEnd gave critical error at Period" + LedgerInfo.CurrentPeriod + ":\r\n");
                    }

                    PeriodEndTransaction.Commit();
                }

                TDBTransaction transaction = new TDBTransaction();
                bool SubmissionOK = false;

                db.WriteTransaction(
                    ref transaction,
                    ref SubmissionOK,
                    delegate
                    {
                        TLedgerInfo LedgerInfo = new TLedgerInfo(intLedgerNumber, db);

                        TLogging.Log("Closing year number " + countYear.ToString());
                        List <Int32>glBatches = new List <int>();
                        TReallocation reallocation = new TReallocation(LedgerInfo, glBatches, transaction);
                        verificationResult = new TVerificationResultCollection();
                        reallocation.VerificationResultCollection = verificationResult;
                        reallocation.IsInInfoMode = false;
                        //                Assert.AreEqual(1, reallocation.GetJobSize(), "Check 1 reallocation job is required"); // No job size is published by Reallocation
                        reallocation.RunOperation();

                        TYearEnd YearEndOperator = new TYearEnd(LedgerInfo);
                        TGlmNewYearInit glmNewYearInit = new TGlmNewYearInit(LedgerInfo, countYear, YearEndOperator, transaction);
                        glmNewYearInit.VerificationResultCollection = verificationResult;
                        glmNewYearInit.IsInInfoMode = false;
                        //              Assert.Greater(glmNewYearInit.GetJobSize(), 0, "Check that NewYearInit has work to do"); // in this version, GetJobSize returns 0
                        glmNewYearInit.RunOperation();
                        YearEndOperator.SetNextPeriod(transaction);
                        SubmissionOK = true;
                    });
            }

            TLedgerInfo LedgerInfo2 = new TLedgerInfo(intLedgerNumber, db);
            Assert.AreEqual(2, LedgerInfo2.CurrentFinancialYear, "After YearEnd, Ledger is in year 2");

            TAccountPeriodInfo periodInfo = new TAccountPeriodInfo(intLedgerNumber, 1);
            Assert.AreEqual(new DateTime(DateTime.Now.Year + 2,
                    1,
                    1), periodInfo.PeriodStartDate, "new Calendar should start with January 1st of next year");
        } // Test_2YearEnds

        void CheckGLMEntry(int ALedgerNumber, int AYear, string AAccount,
            decimal cc1Base, decimal cc1Closing,
            decimal cc2Base, decimal cc2Closing,
            decimal cc3Base, decimal cc3Closing)
        {
            TGlmInfo glmInfo = new TGlmInfo(ALedgerNumber, AYear, AAccount);

            glmInfo.Reset();
            int intCnt = 0;
            bool blnFnd1 = false;
            bool blnFnd2 = false;
            bool blnFnd3 = false;

            TCacheable cache = new Ict.Petra.Server.MFinance.Cacheable.TCacheable();
            Type dummy;
            ACostCentreTable costcentres = (ACostCentreTable)cache.GetCacheableTable(TCacheableFinanceTablesEnum.CostCentreList,
                string.Empty,
                false,
                ALedgerNumber,
                out dummy);

            while (glmInfo.MoveNext())
            {
//              TLogging.Log("glmInfo.CostCentreCode: " + glmInfo.CostCentreCode);

                if (glmInfo.CostCentreCode.Equals("4301"))
                {
                    Assert.AreEqual(cc1Base, glmInfo.YtdActualBase, "CheckGLMEntry (" + ALedgerNumber + ", " + AYear + ", 4301, " + AAccount + ")");
                    Assert.AreEqual(cc1Closing,
                        glmInfo.ClosingPeriodActualBase,
                        "CheckGLMEntry (" + ALedgerNumber + ", " + AYear + ", 4301, " + AAccount + ")");
                    blnFnd1 = true;
                }

                if (glmInfo.CostCentreCode.Equals("4302"))
                {
                    Assert.AreEqual(cc2Base, glmInfo.YtdActualBase, "CheckGLMEntry (" + ALedgerNumber + ", " + AYear + ", 4302, " + AAccount + ")");
                    Assert.AreEqual(cc2Closing,
                        glmInfo.ClosingPeriodActualBase,
                        "CheckGLMEntry (" + ALedgerNumber + ", " + AYear + ", 4302, " + AAccount + ")");
                    blnFnd2 = true;
                }

                if (glmInfo.CostCentreCode.Equals("4303"))
                {
                    Assert.AreEqual(cc3Base, glmInfo.YtdActualBase, "CheckGLMEntry (" + ALedgerNumber + ", " + AYear + ", 4303, " + AAccount + ")");
                    Assert.AreEqual(cc3Closing,
                        glmInfo.ClosingPeriodActualBase,
                        "CheckGLMEntry (" + ALedgerNumber + ", " + AYear + ", 4303, " + AAccount + ")");
                    blnFnd3 = true;
                }

                if (((ACostCentreRow)costcentres.Rows.Find(new object[] { ALedgerNumber, glmInfo.CostCentreCode })).PostingCostCentreFlag)
                {
                    ++intCnt;
                }
            }

            Assert.IsTrue(blnFnd1, "CheckGLMEntry (" + ALedgerNumber + ", " + AYear + ", 4301, " + AAccount + ")");
            Assert.IsTrue(blnFnd2, "CheckGLMEntry (" + ALedgerNumber + ", " + AYear + ", 4302, " + AAccount + ")");
            Assert.IsTrue(blnFnd3, "CheckGLMEntry (" + ALedgerNumber + ", " + AYear + ", 4303, " + AAccount + ")");

            Assert.AreEqual(3, intCnt, "CheckGLMEntry expects 3 posting cost centres ...");
        }

        void CheckGLMPeriodEntry(int ALedgerNumber, int AYear, int APeriodNr, string AAccount,
            decimal cc1Base,
            decimal cc2Base,
            decimal cc3Base,
            TDataBase ADataBase)
        {
            TGlmInfo glmInfo = new TGlmInfo(ALedgerNumber, AYear, AAccount, ADataBase);

            glmInfo.Reset();
            int intCnt = 0;
            bool blnFnd1 = false;
            bool blnFnd2 = false;
            bool blnFnd3 = false;

            TCacheable cache = new Ict.Petra.Server.MFinance.Cacheable.TCacheable();
            Type dummy;
            ACostCentreTable costcentres = (ACostCentreTable)cache.GetCacheableTable(TCacheableFinanceTablesEnum.CostCentreList,
                string.Empty,
                false,
                ALedgerNumber,
                out dummy);

            while (glmInfo.MoveNext())
            {
//              TLogging.Log("glmInfo.CostCentreCode: " + glmInfo.CostCentreCode);

                TGlmpInfo glmpInfo = new TGlmpInfo(ALedgerNumber, ADataBase);
                glmpInfo.LoadBySequence(glmInfo.GlmSequence, APeriodNr);

                Assert.AreEqual(true,
                    glmpInfo.RowExists,
                    "we cannot find a glm period record for " + glmInfo.AccountCode + " / " + glmInfo.CostCentreCode);

                if (glmInfo.CostCentreCode.Equals("4301"))
                {
                    Assert.AreEqual(cc1Base, glmpInfo.ActualBase);
                    blnFnd1 = true;
                }

                if (glmInfo.CostCentreCode.Equals("4302"))
                {
                    Assert.AreEqual(cc2Base, glmpInfo.ActualBase);
                    blnFnd2 = true;
                }

                if (glmInfo.CostCentreCode.Equals("4303"))
                {
                    Assert.AreEqual(cc3Base, glmpInfo.ActualBase);
                    blnFnd3 = true;
                }

                if (((ACostCentreRow)costcentres.Rows.Find(new object[] { ALedgerNumber, glmInfo.CostCentreCode })).PostingCostCentreFlag)
                {
                    ++intCnt;
                }
            }

            Assert.AreEqual(3, intCnt, "3 posting cost centres ...");
            Assert.IsTrue(blnFnd1);
            Assert.IsTrue(blnFnd2);
            Assert.IsTrue(blnFnd3);
        }

        /// <summary>
        /// Test of TAccountPeriodToNewYear
        /// </summary>
        [Test]
        public void Test_TAccountPeriodToNewYear()
        {
            // create new ledger which is in year 2010
            int intLedgerNumber2010 = CommonNUnitFunctions.CreateNewLedger(new DateTime(2010, 1, 1));

            TDBTransaction transaction = new TDBTransaction();
            TDataBase db = DBAccess.Connect("Test_TAccountPeriodToNewYear");
            bool SubmissionOK = false;
            TAccountPeriodToNewYear accountPeriodToNewYear = null;

            db.WriteTransaction(
                ref transaction,
                ref SubmissionOK,
                delegate
                {
                    // We are in 2010 and this and 2011 is not a leap year
                    TVerificationResultCollection verificationResult = new TVerificationResultCollection();
                    accountPeriodToNewYear = new TAccountPeriodToNewYear(intLedgerNumber2010, transaction);

                    accountPeriodToNewYear.VerificationResultCollection = verificationResult;
                    accountPeriodToNewYear.IsInInfoMode = false;

                    // RunEndOfPeriodOperation ...
                    accountPeriodToNewYear.RunOperation();
                    SubmissionOK = true;
                });

            TAccountPeriodInfo accountPeriodInfo = new TAccountPeriodInfo(intLedgerNumber2010);
            accountPeriodInfo.AccountingPeriodNumber = 2;
            Assert.AreEqual(2011, accountPeriodInfo.PeriodStartDate.Year, "Test of the year");
            Assert.AreEqual(28, accountPeriodInfo.PeriodEndDate.Day, "Test of the Feb. 28th");

            SubmissionOK = false;
            db.WriteTransaction(
                ref transaction,
                ref SubmissionOK,
                delegate
                {
                    // Switch to 2012 - this is a leap year ...
                    accountPeriodToNewYear = new TAccountPeriodToNewYear(intLedgerNumber2010, transaction);
                    accountPeriodToNewYear.IsInInfoMode = false;
                    accountPeriodToNewYear.RunOperation();
                    SubmissionOK = true;
                });

            accountPeriodInfo = new TAccountPeriodInfo(intLedgerNumber2010);
            accountPeriodInfo.AccountingPeriodNumber = 2;
            Assert.AreEqual(29, accountPeriodInfo.PeriodEndDate.Day, "Test of the Feb. 29th");
        }

        /// <summary>
        /// TestFixtureSetUp
        /// </summary>
        [OneTimeSetUp]
        public void Init()
        {
            TPetraServerConnector.Connect();
            System.Diagnostics.Debug.WriteLine("Init: " + this.ToString());
        }

        /// <summary>
        /// TearDown the test
        /// </summary>
        [OneTimeTearDown]
        public void TearDownTest()
        {
            TPetraServerConnector.Disconnect();
            System.Diagnostics.Debug.WriteLine("TearDown: " + this.ToString());
        }

        private const string strTestDataBatchDescription = "TestGLPeriodicEndMonth-TESTDATA";

        private void LoadTestTata_GetBatchInfo()
        {
            ABatchRow template = new ABatchTable().NewRowTyped(false);

            template.BatchDescription = strTestDataBatchDescription;

            TDBTransaction transaction = new TDBTransaction();
            TDataBase db = DBAccess.Connect("GetBatchInfo");
            ABatchTable batches = null;
            db.ReadTransaction(ref transaction,
                delegate
                {
                    batches = ABatchAccess.LoadUsingTemplate(template, transaction);
                });

            if (batches.Rows.Count == 0)
            {
                CommonNUnitFunctions.LoadTestDataBase("csharp\\ICT\\Testing\\lib\\MFinance\\server\\GL\\" +
                    "test-sql\\gl-test-batch-data.sql", intLedgerNumber);
            }
        }

        private void UnloadTestData_GetBatchInfo()
        {
            OdbcParameter[] ParametersArray;
            ParametersArray = new OdbcParameter[1];
            ParametersArray[0] = new OdbcParameter("", OdbcType.VarChar);
            ParametersArray[0].Value = strTestDataBatchDescription;

            TDBTransaction transaction = new TDBTransaction();
            bool SubmissionOK = true;
            TDataBase db = DBAccess.Connect("UnloadTestData_GetBatchInfo");
            db.WriteTransaction(ref transaction, ref SubmissionOK,
                delegate
                {
                    string strSQL = "DELETE FROM PUB_" + ABatchTable.GetTableDBName() + " ";
                    strSQL += "WHERE " + ABatchTable.GetBatchDescriptionDBName() + " = ? ";
                    db.ExecuteNonQuery(
                        strSQL, transaction, ParametersArray);
                });
            db.CloseDBConnection();
        }
    }
}
