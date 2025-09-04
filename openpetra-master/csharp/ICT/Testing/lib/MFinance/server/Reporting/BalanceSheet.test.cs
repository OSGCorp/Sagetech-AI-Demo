//
// DO NOT REMOVE COPYRIGHT NOTICES OR THIS FILE HEADER.
//
// @Authors:
//       timop
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
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Globalization;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System.IO;
using System.Collections;
using System.Threading;
using Ict.Testing.NUnitPetraServer;
using Ict.Testing.NUnitTools;
using Ict.Common;
using Ict.Common.Verification;
using Ict.Common.DB;
using Ict.Common.IO;
using Ict.Common.Remoting.Server;
using Ict.Common.Remoting.Shared;
using Ict.Petra.Server.App.Core;
using Ict.Petra.Server.MFinance.Common;
using Ict.Petra.Shared.MFinance;
using Ict.Common.Data;
using Ict.Petra.Server.MReporting.WebConnectors;
using Ict.Petra.Server.MFinance.GL.WebConnectors;
using Ict.Petra.Shared.MReporting;
using Tests.MReporting.Tools;

namespace Tests.MFinance.Server.Reporting
{
    /// This will test the business logic directly on the server
    [TestFixture]
    public class TBalanceSheetTest
    {
        /// <summary>
        /// open database connection or prepare other things for this test
        /// </summary>
        [OneTimeSetUp]
        public void Init()
        {
            //new TLogging("TestServer.log");
            TPetraServerConnector.Connect("../../etc/TestServer.config");
        }

        /// <summary>
        /// cleaning up everything that was set up for this test
        /// </summary>
        [OneTimeTearDown]
        public void TearDown()
        {
            TPetraServerConnector.Disconnect();
        }

        /// <summary>
        /// Test the standard balance sheet
        /// </summary>
        [Test]
        [Ignore("Balance sheet needs fixing to calculate the summaries in glm")]
        public void TestBalanceSheet()
        {
            string testFile = "../../csharp/ICT/Testing/lib/MFinance/server/Reporting/TestData/BalanceSheet.json";
            string resultFile = "../../csharp/ICT/Testing/lib/MFinance/server/Reporting/TestData/BalanceSheet.Results.csv";

            int LedgerNumber = 43;
            TParameterList SpecificParameters = new TParameterList();
            SpecificParameters.Add("param_start_period_i", 1);
            SpecificParameters.Add("param_end_period_i", 1);

            TReportTestingTools.CalculateReport(testFile, resultFile, SpecificParameters, LedgerNumber);

            TReportTestingTools.TestResult(resultFile, LedgerNumber);
        }
    }
}
