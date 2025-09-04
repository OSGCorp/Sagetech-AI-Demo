﻿//
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
using System.Globalization;
using System.Threading;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System.IO;
using Ict.Common;
using Ict.Common.IO;
using Ict.Common.Printing;
using Ict.Petra.Shared.MReporting;

namespace Tests.Reporting
{
    /// <summary>
    /// This is a test for the parameter list which is used for reporting.
    /// </summary>
    [TestFixture]
    public class TParameterListTest
    {
        /// <summary>
        /// ...
        /// </summary>
        [SetUp]
        public void Init()
        {
        }

        /// <summary>
        /// ...
        /// </summary>
        [TearDown]
        public void TearDown()
        {
        }

        /// <summary>
        /// ...
        /// </summary>
        [Test]
        public void TestGeneralParametersProcessing()
        {
            TParameterList parameters = new TParameterList();

            TVariant value = new TVariant();

            value.ApplyFormatString("Currency");
            Assert.AreEqual("0", value.ToFormattedString(), "null value for currency should be 0");
            value = new TVariant(value.ToFormattedString());
            parameters.Add("amountdue", value, -1, 2);
            parameters.Save("testDebug.json");
            Assert.AreEqual(true, parameters.Exists("amountdue", -1, 1, eParameterFit.eBestFitEvenLowerLevel), "can find added parameter");
            Assert.AreEqual("0", parameters.Get("amountdue", -1, 2,
                    eParameterFit.eBestFit).ToFormattedString(), "currency parameter is stored not correctly");
            //Assert.AreEqual("0", parameters.Get("amountdue", -1, 1, eParameterFit.eBestFit).ToFormattedString(), "currency parameter is stored not correctly");
            Assert.AreEqual("0", parameters.Get("amountdue", -1, 1,
                    eParameterFit.eBestFitEvenLowerLevel).ToFormattedString(), "currency parameter cannot be accessed from level up");

            // this test only works with english culture settings
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB", false);
            parameters.Add("IntegerList", "300,400");
            parameters.Save("test.json");
            parameters.Load(Path.GetFullPath("test.json"));
            Assert.AreEqual("eString:300,400", parameters.Get(
                    "IntegerList").EncodeToString(), "integers separated by comma should be treated as string");
            parameters.Save("test2.json");
        }
    }
}
