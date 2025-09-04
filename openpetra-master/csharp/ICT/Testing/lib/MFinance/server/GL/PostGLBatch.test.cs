//
// DO NOT REMOVE COPYRIGHT NOTICES OR THIS FILE HEADER.
//
// @Authors:
//      peters, timop
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

using NUnit.Framework;

using Ict.Common;
using Ict.Common.Data;
using Ict.Common.DB;
using Ict.Common.Exceptions;
using Ict.Common.Remoting.Server;
using Ict.Common.Remoting.Shared;
using Ict.Common.Verification;
using Ict.Petra.Server.App.Core;
using Ict.Petra.Server.MFinance.Common;
using Ict.Petra.Shared.MFinance.GL.Data;
using Ict.Testing.NUnitPetraServer;


namespace Tests.MFinance.Server.GL
{
    /// This will test the business logic directly on the server
    [TestFixture]
    public class TGLBatchTest
    {
        Int32 FLedgerNumber = -1;

        /// <summary>
        /// open database connection or prepare other things for this test
        /// </summary>
        [OneTimeSetUp]
        public void Init()
        {
            //new TLogging("TestServer.log");
            TPetraServerConnector.Connect("../../etc/TestServer.config");
            FLedgerNumber = TAppSettingsManager.GetInt32("LedgerNumber", 43);
            TLogging.Log("Ledger Number = " + FLedgerNumber);
        }

        /// <summary>
        /// cleaning up everything that was set up for this test
        /// </summary>
        [OneTimeTearDown]
        public void TearDown()
        {
            TPetraServerConnector.Disconnect();
        }

        #region Test Argument Validation

        /// <summary>
        /// This will test argument validation for TGLPosting.PostGLBatch
        /// </summary>
        [Test]
        public void TestPostGLBatchArgumentValidation()
        {
            TVerificationResultCollection VerificationResult = null;

            string Message = "Validation failed for posting a GL Batch with ledger number less than 1.";

            // Post a GL Batch with ledger number less than 1
            try
            {
                TGLPosting.PostGLBatch(-1, 1, out VerificationResult);

                if ((VerificationResult.CountCriticalErrors != 1) ||
                    (!VerificationResult.BuildVerificationResultString().Contains("The Ledger number must be greater than 0")))
                {
                    Assert.Fail(Message);
                }
            }
            catch
            {
                Assert.Fail(Message);
            }

            Message = "Validation failed for posting a GL Batch with batch number less than 1.";

            // Post a GL Batch with batch number less than 1
            try
            {
                TGLPosting.PostGLBatch(1, -1, out VerificationResult);

                if ((VerificationResult.CountCriticalErrors != 1) ||
                    (!VerificationResult.BuildVerificationResultString().Contains("The Batch number must be greater than 0")))
                {
                    Assert.Fail(Message);
                }
            }
            catch
            {
                Assert.Fail(Message);
            }
        }

        /// <summary>
        /// This will test argument validation for TGLPosting.PostGLBatches
        /// </summary>
        [Test]
        public void TestPostGLBatchesArgumentValidation()
        {
            TVerificationResultCollection VerificationResult = null;

            List <Int32>BatchNumbers = new List <int>();

            string Message = "Validation failed for posting a GL Batch with ledger number less than 1.";

            // Post a GL Batch with ledger number less than 1
            try
            {
                TGLPosting.PostGLBatches(-1, BatchNumbers, out VerificationResult);
                if ((VerificationResult.CountCriticalErrors != 1) ||
                    (!VerificationResult.BuildVerificationResultString().Contains("The Ledger number must be greater than 0")))
                {
                    Assert.Fail(Message);
                }
            }
            catch
            {
                Assert.Fail(Message);
            }

            Message = "Validation failed for posting a GL Batch without any batch numbers.";

            // Post a GL Batch without any batch numbers
            try
            {
                TGLPosting.PostGLBatches(1, BatchNumbers, out VerificationResult);

                if ((!VerificationResult.HasCriticalOrNonCriticalErrors) ||
                    (!VerificationResult.BuildVerificationResultString().Contains("No GL Batches to post")))
                {
                    Assert.Fail(Message);
                }
            }
            catch
            {
                Assert.Fail(Message);
            }

            Message = "Validation failed for posting a GL Batch with batch number less than 1.";
            BatchNumbers.Add(-1);

            // Post a GL Batch with batch number less than 1
            try
            {
                TGLPosting.PostGLBatches(1, BatchNumbers, out VerificationResult);

                if ((VerificationResult.CountCriticalErrors != 1) ||
                    (!VerificationResult.BuildVerificationResultString().Contains("The Batch number must be greater than 0")))
                {
                    Assert.Fail(Message);
                }
            }
            catch
            {
                Assert.Fail(Message);
            }
        }

        /// <summary>
        /// This will test argument validation for TGLPosting.PrepareGLBatchForPosting
        /// </summary>
        [Test]
        public void TestPrepareGLBatchForPostingArgumentValidation()
        {
            TVerificationResultCollection VerificationResult = new TVerificationResultCollection();
            TDataBase db = DBAccess.Connect("TestPrepareGLBatchForPostingArgumentValidation");
            TDBTransaction Transaction = db.BeginTransaction(IsolationLevel.ReadCommitted);
            GLBatchTDS MainDS = null;
            int BatchPeriod = -1;

            string Message = "Validation failed for PrepareGLBatchForPosting with ledger number less than 1.";

            // Prepare GL Batch For Posting with ledger number less than 1
            try
            {
                TGLPosting.PrepareGLBatchForPosting(out MainDS, -1, 1, ref Transaction, out VerificationResult, null, ref BatchPeriod);
                Assert.Fail(Message);
            }
            catch (EFinanceSystemInvalidLedgerNumberException e)
            {
                Assert.AreEqual(-1, e.LedgerNumber, Message);
            }
            catch
            {
                Assert.Fail(Message);
            }

            // Prepare GL Batch For Posting with batch number less than 1
            Message = "Validation failed for PrepareGLBatchForPosting with batch number less than 1.";
            try
            {
                TGLPosting.PrepareGLBatchForPosting(out MainDS, 43, -1, ref Transaction, out VerificationResult, null, ref BatchPeriod);
                Assert.Fail(Message);
            }
            catch (EFinanceSystemInvalidBatchNumberException e)
            {
                Assert.AreEqual(43, e.LedgerNumber, Message);
                Assert.AreEqual(-1, e.BatchNumber, Message);
            }
            catch
            {
                Assert.Fail(Message);
            }
        }

        /// <summary>
        /// This will test argument validation for TGLPosting.LoadGLBatchData
        /// </summary>
        [Test]
        public void TestLoadGLBatchDataArgumentValidation()
        {
            TVerificationResultCollection VerificationResult = null;
            TDataBase db = DBAccess.Connect("TestLoadGLBatchDataArgumentValidation");
            TDBTransaction Transaction = db.BeginTransaction(IsolationLevel.ReadCommitted);

            // Load GL Batch with ledger number less than 1
            string Message = "Validation failed for LoadGLBatchData with ledger number less than 1.";

            try
            {
                TGLPosting.LoadGLBatchData(-1, 1, ref Transaction, ref VerificationResult);
                Assert.Fail(Message);
            }
            catch (EFinanceSystemInvalidLedgerNumberException e)
            {
                Assert.AreEqual(-1, e.LedgerNumber, Message);
            }
            catch
            {
                Assert.Fail(Message);
            }

            // Load GL Batch with batch number less than 1
            Message = "Validation failed for LoadGLBatchData with batch number less than 1.";
            try
            {
                TGLPosting.LoadGLBatchData(43, -1, ref Transaction, ref VerificationResult);
                Assert.Fail(Message);
            }
            catch (EFinanceSystemInvalidBatchNumberException e)
            {
                Assert.AreEqual(43, e.LedgerNumber, Message);
                Assert.AreEqual(-1, e.BatchNumber, Message);
            }
            catch
            {
                Assert.Fail(Message);
            }

            // Load GL Batch with null VerificationResult
            Message = "Validation failed for LoadGLBatchData with null VerificationResult.";
            VerificationResult = null;
            try
            {
                TGLPosting.LoadGLBatchData(1, 1, ref Transaction, ref VerificationResult);
                Assert.Fail(Message);
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("Function:Load GL Batch Data - Verifications collection must not be NULL!", e.Message,
                    Message);
            }
            catch
            {
                Assert.Fail(Message);
            }
        }

        #endregion
    }
}
