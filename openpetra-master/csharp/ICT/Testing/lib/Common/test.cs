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
using NUnit.Framework;
using System.Threading;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Globalization;

using Ict.Common;
using Ict.Common.Data;
using Ict.Common.Verification;
using Ict.Petra.Shared;

namespace Ict.Common.Testing
{
    ///  This is a testing program for Ict.Common.dll
    [TestFixture]
    public class TTestCommon
    {
        /***************************************************************************************************************************
        *  /// IMPORTANT NOTE ON THE USE OF DIFFERING CULTURES
        *  /// Written by AlanP April 2015
        *  ///
        *  /// I believe the following to be correct from my experience using Win7
        *  ///
        *  /// If you assign a new Culture to the CurrentThread and it is the Culture that you use for your working PC
        *  /// the values for the format information will be the values for your PC and not necessarily the values shipped by Microsoft
        *  /// when you installed Windows.  In my case, specifically for testing the GUI screens I have set my regional formats to use
        *  /// apostrophe/dot for currency group/decimal and space/comma for numeric group/decimal.  This is very helpful in checking we
        *  /// get the correct displays.  However, when I run the tests it means that my en-GB culture has these settings.  A similar
        *  /// situation would arise if your default culture is de-DE say.
        *  ///
        *  /// This means that these tests only work if we do not check for actual text in numeric/currency boxes in the en-GB culture.
        *  /// We can check for text in any other culture - and that is what these tests do.
        *  ///
        * *************************************************************************************************************************/

        /// init the test
        [SetUp]
        public void Init()
        {
            Catalog.Init();
            new TLogging("../../log/test.log");
        }

        #region Helper Methods for Test Cases of this Unit Test

        /// find matching quote in a string. considers nested quotes
        private static int FindMatchingQuote(String s)
        {
            string localstr;

            localstr = s.Substring(1);

            while (localstr.IndexOf("\"\"") != -1)
            {
                localstr = localstr.Replace("\"\"", "  ");
            }

            return localstr.IndexOf('"') + 1;
        }

        static Int16 tstNr;

        /// create a name for a test
        private static string FormatTestName(string s)
        {
            string ReturnValue;

            ReturnValue = CultureInfo.CurrentCulture.Name + ' ' + tstNr.ToString() + ' ' + s;
            tstNr++;
            return ReturnValue;
        }

        private string EvaluateErrCodeInfoResults(ErrCodeInfo AExpectedResult, ErrCodeInfo ATestResult)
        {
            StringCollection ErrCodeDifferences = new StringCollection();
            string ReturnValue = String.Empty;

            if (AExpectedResult.Category != ATestResult.Category)
            {
                ErrCodeDifferences.Add(String.Format(
                        "AExpectedResult.Category != ATestResult.Category ('{0}' vs. '{1}')",
                        AExpectedResult.Category, ATestResult.Category));
            }

            if (AExpectedResult.ErrorCode != ATestResult.ErrorCode)
            {
                ErrCodeDifferences.Add(String.Format(
                        "AExpectedResult.ErrorCode != ATestResult.ErrorCode ('{0}' vs. '{1}')",
                        AExpectedResult.ErrorCode, ATestResult.ErrorCode));
            }

            if (AExpectedResult.ErrorCodeConstantClass != ATestResult.ErrorCodeConstantClass)
            {
                ErrCodeDifferences.Add(String.Format(
                        "AExpectedResult.ErrorCodeConstantClass != ATestResult.ErrorCodeConstantClass ('{0}' vs. '{1}')",
                        AExpectedResult.ErrorCodeConstantClass, ATestResult.ErrorCodeConstantClass));
            }

            if (AExpectedResult.ErrorCodeConstantName != ATestResult.ErrorCodeConstantName)
            {
                ErrCodeDifferences.Add(String.Format(
                        "AExpectedResult.ErrorCodeConstantName != ATestResult.ErrorCodeConstantName ('{0}' vs. '{1}')",
                        AExpectedResult.ErrorCodeConstantName, ATestResult.ErrorCodeConstantName));
            }

            if (AExpectedResult.ErrorMessageText != ATestResult.ErrorMessageText)
            {
                ErrCodeDifferences.Add(String.Format(
                        "AExpectedResult.ErrorMessageText != ATestResult.ErrorMessageText ('{0}' vs. '{1}')",
                        AExpectedResult.ErrorMessageText, ATestResult.ErrorMessageText));
            }

            if (AExpectedResult.ErrorMessageTitle != ATestResult.ErrorMessageTitle)
            {
                ErrCodeDifferences.Add(String.Format(
                        "AExpectedResult.ErrorMessageTitle != ATestResult.ErrorMessageTitle ('{0}' vs. '{1}')",
                        AExpectedResult.ErrorMessageTitle, ATestResult.ErrorMessageTitle));
            }

            if (AExpectedResult.ShortDescription != ATestResult.ShortDescription)
            {
                ErrCodeDifferences.Add(String.Format(
                        "AExpectedResult.ShortDescription != ATestResult.ShortDescription ('{0}' vs. '{1}')",
                        AExpectedResult.ShortDescription, ATestResult.ShortDescription));
            }

            if (AExpectedResult.FullDescription != ATestResult.FullDescription)
            {
                ErrCodeDifferences.Add(String.Format(
                        "AExpectedResult.FullDescription != ATestResult.FullDescription ('{0}' vs. '{1}')",
                        AExpectedResult.FullDescription, ATestResult.FullDescription));
            }

            if (AExpectedResult.HelpID != ATestResult.HelpID)
            {
                ErrCodeDifferences.Add(String.Format(
                        "AExpectedResult.HelpID != ATestResult.HelpID ('{0}' vs. '{1}')",
                        AExpectedResult.HelpID, ATestResult.HelpID));
            }

            if (ErrCodeDifferences.Count > 0)
            {
                foreach (string ErrCodeDiff in ErrCodeDifferences)
                {
                    ReturnValue = ReturnValue + ErrCodeDiff + "; ";
                }

                // Strip off trailing "; " characters
                ReturnValue = ReturnValue.Substring(0, ReturnValue.Length - 2);
            }

            return ReturnValue;
        }

        #endregion


        /// test dates
        [Test]
        public void TestDates()
        {
            DateTime d;
            DateTime d1;
            DateTime d2;

            //TODO: Calendar vs Financial Date Handling - Check if this should use financial year start/end and not assume calendar
            d = new DateTime(2004, 1, 1);
            d1 = new DateTime(d.Year, d.Month, 1).AddMonths(-1);
            d2 = new DateTime(d.Year, d.Month, 1).AddDays(-1);
            Assert.AreEqual(new DateTime(2003, 12, 1), d1, "start date");
            Assert.AreEqual(new DateTime(2003, 12, 31), d2, "end date");
        }

        /// <summary>
        /// test the parsing of a date, needed eg. for gl batch import
        /// </summary>
        [Test]
        public void TestDateParsing()
        {
            // see https://sourceforge.net/apps/mantisbt/openpetraorg/view.php?id=840

            const string badDate = "Incorrect date for ";
            CultureInfo CultureInfoDate = StringHelper.GetCultureInfoForDateFormat("dd/MM/yyyy");   // actually it only matters that it does NOT start with M

            string sDate = "31/01/2012";

            Assert.AreEqual(new DateTime(2012, 01, 31), Convert.ToDateTime(sDate, CultureInfoDate), badDate + sDate);
            sDate = "31/1/12";
            Assert.AreEqual(new DateTime(2012, 01, 31), Convert.ToDateTime(sDate, CultureInfoDate), badDate + sDate);
            sDate = "31-1-12";
            Assert.AreEqual(new DateTime(2012, 01, 31), Convert.ToDateTime(sDate, CultureInfoDate), badDate + sDate);
            sDate = "31.1.12";
            Assert.AreEqual(new DateTime(2012, 01, 31), Convert.ToDateTime(sDate, CultureInfoDate), badDate + sDate);
            sDate = "31 1 12";
            Assert.AreEqual(new DateTime(2012, 01, 31), Convert.ToDateTime(sDate, CultureInfoDate), badDate + sDate);
            sDate = "5 1 2012";
            Assert.AreEqual(new DateTime(2012, 01, 5), Convert.ToDateTime(sDate, CultureInfoDate), badDate + sDate);
            sDate = "5.1.2012";
            Assert.AreEqual(new DateTime(2012, 01, 5), Convert.ToDateTime(sDate, CultureInfoDate), badDate + sDate);
            sDate = "5-1-2012";
            Assert.AreEqual(new DateTime(2012, 01, 5), Convert.ToDateTime(sDate, CultureInfoDate), badDate + sDate);
            sDate = "5/1/2012";
            Assert.AreEqual(new DateTime(2012, 01, 5), Convert.ToDateTime(sDate, CultureInfoDate), badDate + sDate);
            sDate = "5-Jan-2012";
            Assert.AreEqual(new DateTime(2012, 01, 5), Convert.ToDateTime(sDate, CultureInfoDate), badDate + sDate);
            sDate = "Jan-5-2012";
            Assert.AreEqual(new DateTime(2012, 01, 5), Convert.ToDateTime(sDate, CultureInfoDate), badDate + sDate);
            sDate = "2012-1-5";
            Assert.AreEqual(new DateTime(2012, 01, 5), Convert.ToDateTime(sDate, CultureInfoDate), badDate + sDate);
            sDate = "2012-01-05";
            Assert.AreEqual(new DateTime(2012, 01, 5), Convert.ToDateTime(sDate, CultureInfoDate), badDate + sDate);

            CultureInfoDate = StringHelper.GetCultureInfoForDateFormat("MM/dd/yyyy");   // actually it only matters that it starts with M

            sDate = "01/31/2012";
            Assert.AreEqual(new DateTime(2012, 01, 31), Convert.ToDateTime(sDate, CultureInfoDate), badDate + sDate);
            sDate = "1/31/12";
            Assert.AreEqual(new DateTime(2012, 01, 31), Convert.ToDateTime(sDate, CultureInfoDate), badDate + sDate);
            sDate = "1-31-12";
            Assert.AreEqual(new DateTime(2012, 01, 31), Convert.ToDateTime(sDate, CultureInfoDate), badDate + sDate);
            sDate = "1.31.12";
            Assert.AreEqual(new DateTime(2012, 01, 31), Convert.ToDateTime(sDate, CultureInfoDate), badDate + sDate);
            sDate = "1 31 12";
            Assert.AreEqual(new DateTime(2012, 01, 31), Convert.ToDateTime(sDate, CultureInfoDate), badDate + sDate);
            sDate = "1 5 2012";
            Assert.AreEqual(new DateTime(2012, 01, 5), Convert.ToDateTime(sDate, CultureInfoDate), badDate + sDate);
            sDate = "1.5.2012";
            Assert.AreEqual(new DateTime(2012, 01, 5), Convert.ToDateTime(sDate, CultureInfoDate), badDate + sDate);
            sDate = "1-5-2012";
            Assert.AreEqual(new DateTime(2012, 01, 5), Convert.ToDateTime(sDate, CultureInfoDate), badDate + sDate);
            sDate = "1/5/2012";
            Assert.AreEqual(new DateTime(2012, 01, 5), Convert.ToDateTime(sDate, CultureInfoDate), badDate + sDate);
            sDate = "5-Jan-2012";
            Assert.AreEqual(new DateTime(2012, 01, 5), Convert.ToDateTime(sDate, CultureInfoDate), badDate + sDate);
            sDate = "Jan-5-2012";
            Assert.AreEqual(new DateTime(2012, 01, 5), Convert.ToDateTime(sDate, CultureInfoDate), badDate + sDate);
            sDate = "2012-1-5";
            Assert.AreEqual(new DateTime(2012, 01, 5), Convert.ToDateTime(sDate, CultureInfoDate), badDate + sDate);
            sDate = "2012-01-05";
            Assert.AreEqual(new DateTime(2012, 01, 5), Convert.ToDateTime(sDate, CultureInfoDate), badDate + sDate);

            const string badLooksLike = "Incorrect LooksLike for ";
            const string badMonthFirst = "Incorrect month first for ";
            const string badDayFirst = "Incorrect day first for ";

            DateTime dtMonthFirst, dtDayFirst;
            sDate = "1/2/12";
            Assert.IsTrue(StringHelper.LooksLikeAmbiguousShortDate(sDate, false, out dtMonthFirst, out dtDayFirst), badLooksLike + sDate);
            Assert.AreEqual(dtMonthFirst, new DateTime(2012, 1, 2), badMonthFirst + sDate);
            Assert.AreEqual(dtDayFirst, new DateTime(2012, 2, 1), badDayFirst + sDate);
            sDate = "1-2-12";
            Assert.IsTrue(StringHelper.LooksLikeAmbiguousShortDate(sDate, false, out dtMonthFirst, out dtDayFirst), badLooksLike + sDate);
            Assert.AreEqual(dtMonthFirst, new DateTime(2012, 1, 2), badMonthFirst + sDate);
            Assert.AreEqual(dtDayFirst, new DateTime(2012, 2, 1), badDayFirst + sDate);
            sDate = "1.2.12";
            Assert.IsTrue(StringHelper.LooksLikeAmbiguousShortDate(sDate, false, out dtMonthFirst, out dtDayFirst), badLooksLike + sDate);
            Assert.AreEqual(dtMonthFirst, new DateTime(2012, 1, 2), badMonthFirst + sDate);
            Assert.AreEqual(dtDayFirst, new DateTime(2012, 2, 1), badDayFirst + sDate);
            sDate = "1 2 12";
            Assert.IsTrue(StringHelper.LooksLikeAmbiguousShortDate(sDate, false, out dtMonthFirst, out dtDayFirst), badLooksLike + sDate);
            Assert.AreEqual(dtMonthFirst, new DateTime(2012, 1, 2), badMonthFirst + sDate);
            Assert.AreEqual(dtDayFirst, new DateTime(2012, 2, 1), badDayFirst + sDate);
            sDate = "01/02/12";
            Assert.IsTrue(StringHelper.LooksLikeAmbiguousShortDate(sDate, false, out dtMonthFirst, out dtDayFirst), badLooksLike + sDate);
            Assert.AreEqual(dtMonthFirst, new DateTime(2012, 1, 2), badMonthFirst + sDate);
            Assert.AreEqual(dtDayFirst, new DateTime(2012, 2, 1), badDayFirst + sDate);
            sDate = "01-02-12";
            Assert.IsTrue(StringHelper.LooksLikeAmbiguousShortDate(sDate, false, out dtMonthFirst, out dtDayFirst), badLooksLike + sDate);
            Assert.AreEqual(dtMonthFirst, new DateTime(2012, 1, 2), badMonthFirst + sDate);
            Assert.AreEqual(dtDayFirst, new DateTime(2012, 2, 1), badDayFirst + sDate);
            sDate = "01.02.12";
            Assert.IsTrue(StringHelper.LooksLikeAmbiguousShortDate(sDate, false, out dtMonthFirst, out dtDayFirst), badLooksLike + sDate);
            Assert.AreEqual(dtMonthFirst, new DateTime(2012, 1, 2), badMonthFirst + sDate);
            Assert.AreEqual(dtDayFirst, new DateTime(2012, 2, 1), badDayFirst + sDate);
            sDate = "01 02 12";
            Assert.IsTrue(StringHelper.LooksLikeAmbiguousShortDate(sDate, false, out dtMonthFirst, out dtDayFirst), badLooksLike + sDate);
            Assert.AreEqual(dtMonthFirst, new DateTime(2012, 1, 2), badMonthFirst + sDate);
            Assert.AreEqual(dtDayFirst, new DateTime(2012, 2, 1), badDayFirst + sDate);
            sDate = "010212";
            Assert.IsFalse(StringHelper.LooksLikeAmbiguousShortDate(sDate, false, out dtMonthFirst, out dtDayFirst), badLooksLike + sDate);
            Assert.IsTrue(StringHelper.LooksLikeAmbiguousShortDate(sDate, true, out dtMonthFirst, out dtDayFirst), badLooksLike + sDate);
            Assert.AreEqual(dtMonthFirst, new DateTime(2012, 1, 2), badMonthFirst + sDate);
            Assert.AreEqual(dtDayFirst, new DateTime(2012, 2, 1), badDayFirst + sDate);
        }

        /// <summary>
        /// Test our unambiguous number parsing for 'LooksLikeFloat'
        /// </summary>
        [Test]
        public void TestNumberParsing()
        {
            const string badLooksLike = "Incorrect number evaluation for ";
            const string badDecimalSeparator = "Incorrect decimal separator for ";
            const string badDecSepNull = "Decimal separator is null for ";
            const string badConversion = "Incorrect float conversion for ";

            NumberFormatInfo dotFormat = new CultureInfo("en-GB").NumberFormat;
            NumberFormatInfo commaFormat = new CultureInfo("de-DE").NumberFormat;

            string s;
            bool? isDotDecimal;

            // All these should look like unambiguous dot numbers
            s = "1.234";
            Assert.IsTrue(StringHelper.LooksLikeFloat(s, out isDotDecimal), badLooksLike + s);
            Assert.IsNotNull(isDotDecimal, badDecSepNull + s);
            Assert.IsTrue(isDotDecimal.Value, badDecimalSeparator + s);
            Assert.Less(ConvertToFloat(s, dotFormat) - 1.234f, 0.001f, badConversion + s);
            s = "1234.567";
            Assert.IsTrue(StringHelper.LooksLikeFloat(s, out isDotDecimal), badLooksLike + s);
            Assert.IsNotNull(isDotDecimal, badDecSepNull + s);
            Assert.IsTrue(isDotDecimal.Value, badDecimalSeparator + s);
            Assert.Less(ConvertToFloat(s, dotFormat) - 1234.567f, 0.001f, badConversion + s);
            s = "1,234.567";
            Assert.IsTrue(StringHelper.LooksLikeFloat(s, out isDotDecimal), badLooksLike + s);
            Assert.IsNotNull(isDotDecimal, badDecSepNull + s);
            Assert.IsTrue(isDotDecimal.Value, badDecimalSeparator + s);
            Assert.Less(ConvertToFloat(s, dotFormat) - 1234.567f, 0.001f, badConversion + s);
            s = "1 234.567";
            Assert.IsTrue(StringHelper.LooksLikeFloat(s, out isDotDecimal), badLooksLike + s);
            Assert.IsNotNull(isDotDecimal, badDecSepNull + s);
            Assert.IsTrue(isDotDecimal.Value, badDecimalSeparator + s);
            Assert.Less(ConvertToFloat(s, dotFormat) - 1234.567f, 0.001f, badConversion + s);
            s = "1,234,567";
            Assert.IsTrue(StringHelper.LooksLikeFloat(s, out isDotDecimal), badLooksLike + s);
            Assert.IsNotNull(isDotDecimal, badDecSepNull + s);
            Assert.IsTrue(isDotDecimal.Value, badDecimalSeparator + s);
            Assert.Less(ConvertToFloat(s, dotFormat) - 1234567f, 0.1f, badConversion + s);
            s = "1,234,567.89";
            Assert.IsTrue(StringHelper.LooksLikeFloat(s, out isDotDecimal), badLooksLike + s);
            Assert.IsNotNull(isDotDecimal, badDecSepNull + s);
            Assert.IsTrue(isDotDecimal.Value, badDecimalSeparator + s);
            Assert.Less(ConvertToFloat(s, dotFormat) - 1234567.89f, 0.01f, badConversion + s);

            // All these should look like unambiguous comma numbers
            s = "1,234";
            Assert.IsTrue(StringHelper.LooksLikeFloat(s, out isDotDecimal), badLooksLike + s);
            Assert.IsNotNull(isDotDecimal, badDecSepNull + s);
            Assert.IsFalse(isDotDecimal.Value, badDecimalSeparator + s);    // This is the one that we say is comma but .NET can take as dot
            Assert.Less(ConvertToFloat(s, commaFormat) - 1.234f, 0.001f, badConversion + s);
            s = "1234,567";
            Assert.IsTrue(StringHelper.LooksLikeFloat(s, out isDotDecimal), badLooksLike + s);
            Assert.IsNotNull(isDotDecimal, badDecSepNull + s);
            Assert.IsFalse(isDotDecimal.Value, badDecimalSeparator + s);    // This is the one that we say is comma but .NET can take as dot
            Assert.Less(ConvertToFloat(s, commaFormat) - 1234.567f, 0.001f, badConversion + s);
            s = "1.234,567";
            Assert.IsTrue(StringHelper.LooksLikeFloat(s, out isDotDecimal), badLooksLike + s);
            Assert.IsNotNull(isDotDecimal, badDecSepNull + s);
            Assert.IsFalse(isDotDecimal.Value, badDecimalSeparator + s);    // This is the one that we say is comma but .NET can take as dot
            Assert.Less(ConvertToFloat(s, commaFormat) - 1234.567f, 0.001f, badConversion + s);
            s = "1 234,567";
            Assert.IsTrue(StringHelper.LooksLikeFloat(s, out isDotDecimal), badLooksLike + s);
            Assert.IsNotNull(isDotDecimal, badDecSepNull + s);
            Assert.IsFalse(isDotDecimal.Value, badDecimalSeparator + s);    // This is the one that we say is comma but .NET can take as dot
            Assert.Less(ConvertToFloat(s, commaFormat) - 1234.567f, 0.001f, badConversion + s);
            s = "1.234.567,89";
            Assert.IsTrue(StringHelper.LooksLikeFloat(s, out isDotDecimal), badLooksLike + s);
            Assert.IsNotNull(isDotDecimal, badDecSepNull + s);
            Assert.IsFalse(isDotDecimal.Value, badDecimalSeparator + s);
            Assert.Less(ConvertToFloat(s, commaFormat) - 1234567.89f, 0.01f, badConversion + s);
            s = "1 234 567,89";
            Assert.IsTrue(StringHelper.LooksLikeFloat(s, out isDotDecimal), badLooksLike + s);
            Assert.IsNotNull(isDotDecimal, badDecSepNull + s);
            Assert.IsFalse(isDotDecimal.Value, badDecimalSeparator + s);
            Assert.Less(ConvertToFloat(s, commaFormat) - 1234567.89f, 0.01f, badConversion + s);
            s = "1'234'567,89";
            Assert.IsTrue(StringHelper.LooksLikeFloat(s, out isDotDecimal), badLooksLike + s);
            Assert.IsNotNull(isDotDecimal, badDecSepNull + s);
            Assert.IsFalse(isDotDecimal.Value, badDecimalSeparator + s);
            Assert.Less(ConvertToFloat(s, commaFormat) - 1234567.89f, 0.01f, badConversion + s);
            s = "1.234.567";
            Assert.IsTrue(StringHelper.LooksLikeFloat(s, out isDotDecimal), badLooksLike + s);
            Assert.IsNotNull(isDotDecimal, badDecSepNull + s);
            Assert.IsFalse(isDotDecimal.Value, badDecimalSeparator + s);
            Assert.Less(ConvertToFloat(s, commaFormat) - 1234567f, 0.1f, badConversion + s);
            s = "1 234 567";
            Assert.IsTrue(StringHelper.LooksLikeFloat(s, out isDotDecimal), badLooksLike + s);
            Assert.IsNotNull(isDotDecimal, badDecSepNull + s);
            Assert.IsFalse(isDotDecimal.Value, badDecimalSeparator + s);
            Assert.Less(ConvertToFloat(s, commaFormat) - 1234567f, 0.1f, badConversion + s);
            s = "1'234'567";
            Assert.IsTrue(StringHelper.LooksLikeFloat(s, out isDotDecimal), badLooksLike + s);
            Assert.IsNotNull(isDotDecimal, badDecSepNull + s);
            Assert.IsFalse(isDotDecimal.Value, badDecimalSeparator + s);
            Assert.Less(ConvertToFloat(s, commaFormat) - 1234567f, 0.1f, badConversion + s);

            // This is ambiguous
            s = "1234";
            Assert.IsFalse(StringHelper.LooksLikeFloat(s, out isDotDecimal), badLooksLike + s);
            Assert.IsNull(isDotDecimal, badDecSepNull + s);

            // These are not valid numbers
            s = "1.234,567.89";
            Assert.IsFalse(StringHelper.LooksLikeFloat(s, out isDotDecimal), badLooksLike + s);
            Assert.IsNull(isDotDecimal, badDecSepNull + s);

            s = "a.bcd";
            Assert.IsFalse(StringHelper.LooksLikeFloat(s, out isDotDecimal), badLooksLike + s);
            Assert.IsNull(isDotDecimal, badDecSepNull + s);
        }

        private float ConvertToFloat(string AString, NumberFormatInfo ANumberFormatInfo)
        {
            // Importing in OP always translates to Invariant culture and removes the thousands separators
            if (ANumberFormatInfo.NumberDecimalSeparator == ",")
            {
                return Convert.ToSingle(AString.Replace(".", "").Replace(" ", "").Replace("'", "").Replace(",", "."), CultureInfo.InvariantCulture);
            }
            else
            {
                return Convert.ToSingle(AString.Replace(",", "").Replace(" ", "").Replace("'", ""), CultureInfo.InvariantCulture);
            }
        }

        /// test formatting currency values
        [Test]
        public void TestStringHelperFormatCurrency()
        {
            String DecimalSeparator;
            String ThousandsOperator;
            ArrayList cultures;
            TVariant v;

            cultures = new ArrayList();
            cultures.Add("en-GB");
            cultures.Add("en-US");
            cultures.Add("de-DE");

            /* opposite meaning of , and . */
            cultures.Add("ru-RU");

            /* funny thousand separator: space */
            foreach (string s in cultures)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(s, false);

                /* Console.WriteLine('currently testing: ' + CultureInfo.CurrentCulture.Name); */
                DecimalSeparator = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
                ThousandsOperator = CultureInfo.CurrentCulture.NumberFormat.CurrencyGroupSeparator;
                tstNr = 1;
                Assert.AreEqual("2" + DecimalSeparator + "23",
                    StringHelper.FormatCurrency(new TVariant(2.23), "Currency"), FormatTestName("variant parameter; positive; format: Currency"));
                Assert.AreEqual("0" + DecimalSeparator + "00",
                    StringHelper.FormatCurrency(new TVariant(0.00), "Currency"), FormatTestName("variant parameter; 0; format: Currency"));
                Assert.AreEqual("(2" + DecimalSeparator + "23)",
                    StringHelper.FormatCurrency(new TVariant(-2.23), "Currency"), FormatTestName("variant parameter; negative; format: Currency"));
                Assert.AreEqual('2' + DecimalSeparator + "23", StringHelper.FormatCurrency(new TVariant(
                            2.23), "#,##0.00;-#,##0.00;0.00; "), FormatTestName("variant parameter; positive; format: Access Style"));
                Assert.AreEqual('0' + DecimalSeparator + "00", StringHelper.FormatCurrency(new TVariant(
                            0.00), "#,##0.00;-#,##0.00;0.00; "), FormatTestName("variant parameter; 0; format: Access Style"));
                Assert.AreEqual("-2" + DecimalSeparator + "23", StringHelper.FormatCurrency(new TVariant(
                            -2.23), "#,##0.00;-#,##0.00; ; "), FormatTestName("variant parameter; negative; format: Access Style"));
                Assert.AreEqual("",
                    StringHelper.FormatCurrency(new TVariant(-2.23),
                        "#,##0.00; ; ; "), FormatTestName("variant parameter; negative; format: Access Style; only display positive values"));
                Assert.AreEqual("-2" + DecimalSeparator + "23",
                    StringHelper.FormatCurrency(new TVariant(-2.23),
                        ";-#,##0.00; ; "), FormatTestName("variant parameter; negative; format: Access Style; only display negative values"));
                Assert.AreEqual('2' + DecimalSeparator + "00",
                    StringHelper.FormatCurrency(new TVariant(-2.00),
                        ";#,##0.00; ; "),
                    FormatTestName(
                        "variant parameter; negative; format: Access Style; only display negative values, but without sign; also test if 0 are added"));
                Assert.AreEqual('2' + DecimalSeparator + "00",
                    StringHelper.FormatCurrency(new TVariant(-2),
                        ";#,##0.00; ; "),
                    FormatTestName(
                        "variant parameter; negative; format: Access Style; only display negative values, but without sign; also test if 0 are added; integer value"));
                Assert.AreEqual('2' + DecimalSeparator + "23",
                    StringHelper.FormatCurrency(new TVariant(2.23),
                        "->>>,>>>,>>>,>>9.99"), FormatTestName("variant parameter; positive; format: Progress Style"));
                Assert.AreEqual('0' + DecimalSeparator + "00",
                    StringHelper.FormatCurrency(new TVariant(0.00),
                        "->>>,>>>,>>>,>>9.99"), FormatTestName("variant parameter; 0; format: Progress Style"));
                Assert.AreEqual("-2" + DecimalSeparator + "23", StringHelper.FormatCurrency(new TVariant(
                            -2.23), "->>>,>>>,>>>,>>9.99"), FormatTestName("variant parameter; negative; format: Progress Style"));
                Assert.AreEqual("2",
                    StringHelper.FormatCurrency(new TVariant(2.23),
                        "->>>,>>>,>>>,>>9"), FormatTestName("variant parameter; positive; format: Progress Style, no decimals"));
                Assert.AreEqual("0",
                    StringHelper.FormatCurrency(new TVariant(0.00),
                        "->>>,>>>,>>>,>>9"), FormatTestName("variant parameter; 0; format: Progress Style, no decimals"));
                Assert.AreEqual("-2",
                    StringHelper.FormatCurrency(new TVariant(-2.23),
                        "->>>,>>>,>>>,>>9"), FormatTestName("variant parameter; negative; format: Progress Style, no decimals"));
                Assert.AreEqual("002" + DecimalSeparator + "23", StringHelper.FormatCurrency(new TVariant(
                            2.23), "->>>,>>>,>>>,999.99"), FormatTestName("variant parameter; positive; format: Progress Style, leading zeros"));
                Assert.AreEqual("000" + DecimalSeparator + "00", StringHelper.FormatCurrency(new TVariant(
                            0.00), "->>>,>>>,>>>,999.99"), FormatTestName("variant parameter; 0; format: Progress Style, leading zeros"));
                Assert.AreEqual("-002" + DecimalSeparator + "23", StringHelper.FormatCurrency(new TVariant(
                            -2.23), "->>>,>>>,>>>,999.99"), FormatTestName("variant parameter; negative; format: Progress Style, leading zeros"));
                Assert.AreEqual("2" + ThousandsOperator + "345" + DecimalSeparator + "23",
                    StringHelper.FormatCurrency(new TVariant(2345.23),
                        "->>>,>>>,>>>,>>9.99"), FormatTestName("variant parameter; positive; format: Progress Style, thousand separator"));
                Assert.AreEqual('2' + DecimalSeparator + "23",
                    StringHelper.FormatCurrency(2.23M, "->>>,>>>,>>>,>>9.99"), FormatTestName("decimal parameter"));
                Assert.AreEqual("223", StringHelper.FormatCurrency(223234, "#;(#);0;"), FormatTestName("just display the thousands"));
                Assert.AreEqual("0027045678", StringHelper.FormatCurrency(27045678, "0000000000;;;"), FormatTestName("partner key"));
                Assert.AreEqual("3%", StringHelper.FormatCurrency(3.0M, "0%;-0%;0;"), FormatTestName("percentage"));
                Assert.AreEqual("0029112233", StringHelper.FormatCurrency(29112233, "partnerkey"), FormatTestName("partnerkey"));
                Assert.AreEqual("31" + DecimalSeparator + "31%",
                    StringHelper.FormatCurrency(31.3053682781472M, "percentage2decimals"), FormatTestName("percentage2decimals"));
                Assert.AreEqual("31" + DecimalSeparator + "31%", StringHelper.FormatCurrency(new TVariant(
                            31.3053682781472), "percentage2decimals"), FormatTestName("percentage2decimals TVariant"));
                Assert.AreEqual("31" + DecimalSeparator + "31%",
                    StringHelper.FormatCurrency(TVariant.DecodeFromString(new TVariant(
                                31.3053682781472).EncodeToString()), "percentage2decimals"), FormatTestName("percentage2decimals TVariant Encoded"));
                Assert.AreEqual("31" + DecimalSeparator + "31%",
                    StringHelper.FormatCurrency(TVariant.DecodeFromString(new TVariant(31.3053682781472M,
                                "percentage2decimals").EncodeToString()),
                        "percentage2decimals"), FormatTestName("percentage2decimals TVariant Encoded 2"));
                v = new TVariant(31.3053682781472);
                v.FormatString = "percentage2decimals";
                Assert.AreEqual("eDecimal:percentage2decimals:4629504895489138888", v.EncodeToString(),
                    FormatTestName("percentage2decimals encodetostring"));
                Assert.AreEqual("31" + DecimalSeparator + "31%", StringHelper.FormatCurrency(TVariant.DecodeFromString(
                            v.EncodeToString()), ""), FormatTestName("percentage2decimals TVariant Encoded 3"));
                v = new TVariant(31);
                v.FormatString = "percentage2decimals";
                Assert.AreEqual("eInteger:percentage2decimals:31", v.EncodeToString(), FormatTestName("percentage2decimals encodetostring integer"));
            }
        }

        /// test formatting currency using currency code
        [Test]
        public void TestStringHelperCurrencyFromCode()
        {
            DataTable Tbl = new DataTable();

            Tbl.Columns.Add("a_display_format_c", typeof(String));
            Tbl.Columns.Add("a_currency_code_c", typeof(String));

            DataRow Row = Tbl.NewRow();
            Row["a_currency_code_c"] = "TRL";
            Row["a_display_format_c"] = "->>,>>>,>>>,>>>,>>9";
            Tbl.Rows.Add(Row);

            Row = Tbl.NewRow();
            Row["a_currency_code_c"] = "EUR";
            Row["a_display_format_c"] = "->>>,>>>,>>>,>>9.99";
            Tbl.Rows.Add(Row);

            // See the IMPORTANT NOTE at the top of this class concerning the use of Cultures and a PC's local regional settings.
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
            StringHelper myStringHelper = new StringHelper();
            myStringHelper.CurrencyFormatTable = Tbl;
            String TrlRes = myStringHelper.FormatUsingCurrencyCode(1234.56M, "TRL");
            String EurRes = myStringHelper.FormatUsingCurrencyCode(1234.56M, "EUR");

            NumberFormatInfo nfi = Thread.CurrentThread.CurrentCulture.NumberFormat;
            String expectedTrl = "1" + nfi.CurrencyGroupSeparator + "235";
            String expectedEur = "1" + nfi.CurrencyGroupSeparator + "234" + nfi.CurrencyDecimalSeparator + "56";
            Assert.AreEqual(expectedTrl, TrlRes);
            Assert.AreEqual(expectedEur, EurRes);

            // Now try a different culture with default culture settings
            Thread.CurrentThread.CurrentCulture = new CultureInfo("de-DE");
            TrlRes = myStringHelper.FormatUsingCurrencyCode(1234.56M, "TRL");
            EurRes = myStringHelper.FormatUsingCurrencyCode(1234.56M, "EUR");
            Assert.AreEqual("1.235", TrlRes);
            Assert.AreEqual("1.234,56", EurRes);
        }

        /// test csv operations (comma separated value lists)
        [Test]
        public void TestStringHelperCSVList()
        {
            const String EMPTY = "";
            const String UMLAUT = "B�cker";
            const String QUOTEKOMMA = "test\",";
            const String QUOTE = "test\"";
            const String SIMPLE = "test";
            const String KOMMA = "test, hallo";
            const String QUOTES = "test\"tst";
            const String DATE = "24/03/1999";
            const String ACCOUNTCODE = "0400";

            String[] myTest;
            Int16 i;
            string s;
            string s2;
            string longStringBug672;
            Assert.AreEqual("eString:test", StringHelper.AddCSV("", "eString:test", "|"), "Add to empty csv string");
            Assert.AreEqual("a, test ", StringHelper.AddCSV(StringHelper.AddCSV("", "a", ","), " test "), "add string with spaces");
            s = " test ,a";
            Assert.AreEqual(" test ", StringHelper.GetNextCSV(ref s), "get string with spaces");

            s = "shortdesc=\"Equipment, General\",longdesc=Equipment";
            Assert.AreEqual("shortdesc=\"Equipment, General\"", StringHelper.GetNextCSV(ref s), "get string with quotes that are not at the start");
            Assert.AreEqual("longdesc=Equipment", StringHelper.GetNextCSV(ref s), "after string with quotes that are not at the start");

            // Tests on what might be CSV Import files
            // Simple headers (keep spaces)
            s = "Col 1, Col 2, Col 3";
            Assert.AreEqual("Col 1", StringHelper.GetNextCSV(ref s, ",", false), "Typical CSV File import column 1 (keep spaces)");
            Assert.AreEqual(" Col 2", StringHelper.GetNextCSV(ref s, ",", false), "Typical CSV File import column 2 (keep spaces)");
            Assert.AreEqual(" Col 3", StringHelper.GetNextCSV(ref s, ",", false), "Typical CSV File import column 3 (keep spaces)");

            // Same simple headers (remove spaces)
            s = "Col 1, Col 2, Col 3";
            Assert.AreEqual("Col 1", StringHelper.GetNextCSV(ref s, ",", false, true), "Typical CSV File import column 1 (remove spaces)");
            Assert.AreEqual("Col 2", StringHelper.GetNextCSV(ref s, ",", false, true), "Typical CSV File import column 2 (remove spaces)");
            Assert.AreEqual("Col 3", StringHelper.GetNextCSV(ref s, ",", false, true), "Typical CSV File import column 3 (remove spaces)");

            // Quoted headers (keep spaces)
            s = "Col 1, \"Col 2\" xx , \"Col \"\"3\"\" \" ";
            Assert.AreEqual("Col 1", StringHelper.GetNextCSV(ref s, ",", false), "Typical CSV File import with quotes column 1 (keep spaces)");
            Assert.AreEqual(" \"Col 2\" xx ", StringHelper.GetNextCSV(ref s,
                    ",",
                    false), "Typical CSV File with quotes import column 2 (keep spaces)");
            Assert.AreEqual(" \"Col \"3\" \" ", StringHelper.GetNextCSV(ref s,
                    ",",
                    false), "Typical CSV File with quotes import column 3 (keep spaces)");

            // Same quoted headers (remove spaces)
            s = "Col 1, \" Col 2 \" , \"Col \"\"3\"\" \" ";
            Assert.AreEqual("Col 1", StringHelper.GetNextCSV(ref s, ",", false, true), "Typical CSV File import with quotes column 1 (remove spaces)");
            Assert.AreEqual(" Col 2 ", StringHelper.GetNextCSV(ref s,
                    ",",
                    false,
                    true), "Typical CSV File import with quotes column 2 (remove spaces)");
            Assert.AreEqual("Col \"3\" ", StringHelper.GetNextCSV(ref s,
                    ",",
                    false,
                    true), "Typical CSV File import with quotes column 3 (remove spaces)");

            /* ,B�cker,"test"",","test""",test,"test, hallo","test""tst","24/03/1999","0400" */
            myTest = new String[] {
                EMPTY, UMLAUT, QUOTEKOMMA, QUOTE, SIMPLE, KOMMA, QUOTES, DATE, ACCOUNTCODE
            };
            s = "";

            for (i = 0; i <= myTest.Length - 1; i += 1)
            {
                s = StringHelper.AddCSV(s, myTest[i]);
            }

            Assert.AreEqual(" ," + UMLAUT + ",\"" +
                QUOTEKOMMA.Replace("\"", "\"\"") + "\",\"" +
                QUOTE.Replace("\"", "\"\"") + "\"," + SIMPLE + ",\"" + KOMMA + "\",\"" +
                QUOTES.Replace("\"", "\"\"") + "\"," + DATE + ",\"" + ACCOUNTCODE + "\"", s, "full list not correctly built");
            i = 0;

            while (s.Length > 0)
            {
                Assert.AreEqual(myTest[i].Trim(), StringHelper.GetNextCSV(ref s).Trim(), "problem with reading one element");
                i++;
            }

            s = "eDateTime:\"2004-03-29T00:00:00\"";
            Assert.AreEqual("eDateTime", StringHelper.GetNextCSV(ref s, ":"), "read CSV 1");
            Assert.AreEqual("2004-03-29T00:00:00", StringHelper.GetNextCSV(ref s, ":"), "read CSV 2");
            s = "";
            s = StringHelper.AddCSV(s, "test", ",");
            s = StringHelper.AddCSV(s, "\" )  ))", ",");
            Assert.AreEqual("test,\"\"\" )  ))\"", s, "quote at beginning of csv value");
            s2 = "test";
            s2 = StringHelper.AddCSV(s2, s);
            Assert.AreEqual("test,\"test,\"\"\"\"\"\" )  ))\"\"\"", s2, "cascading csv lists");
            Assert.AreEqual("test", StringHelper.GetNextCSV(ref s2, ","), "cascading csv lists 2");
            Assert.AreEqual(s, StringHelper.GetNextCSV(ref s2, ","), "cascading csv lists 3");
            Assert.AreEqual(1, FindMatchingQuote("\"\""), "Test 1");
            Assert.AreEqual(4, FindMatchingQuote("\"   \""), "Test 2");
            Assert.AreEqual(8, FindMatchingQuote("\" \"\" \"\" \""), "Test 3");
            Assert.AreEqual(8, FindMatchingQuote("\" \"\" \"\" \""), "Test 3");
            Assert.AreEqual(19, FindMatchingQuote("\"eString:\"\" test \"\"\"|\"eDateTime:\"\"2004-03-29T00:00:00\"\"\""), "Test 4");
            s = "\"eComposite::eString:test|\"\"eString:\"\"\"\" test \"\"\"\"\"\"|\"\"eDateTime:\"\"\"\"2004-03-29T00:00:00\"\"\"\"\"\"\"";
            Assert.AreEqual("eComposite::eString:test|\"eString:\"\" test \"\"\"|\"eDateTime:\"\"2004-03-29T00:00:00\"\"\"",
                StringHelper.GetNextCSV(ref s,
                    "|"),
                "split composite CSV 1a");
            longStringBug672 =
                "Actual vs Budget with % Variance\\, (Quarter)3 even longer and longer and longer and longer\\, testActual vs Budget with % VarianceActual vs Budget with % Variance,"
                +
                "Actual vs Budget with % Variance\\, (Quarter)3 even longer and longer and longer and longer\\, testActual vs Budget with % Variance,"
                +
                "Actual vs Budget with % Variance\\, (Quarter)3 even longer and longer and longer and longer\\, test," + "My office budget report," +
                "Actual vs Budget with % Variance (Month)";
            Assert.AreEqual(5, StringHelper.StrSplit(longStringBug672, ",").Count, "Test bug 672, strsplit, number of elements");
            Assert.AreEqual(159, StringHelper.StrSplit(longStringBug672, ",")[0].Length, "Test bug 672, strsplit, length of first element");

            string testAccountCodeLeadingZero = StringHelper.AddCSV("test", "0100");
            Assert.AreEqual("0100", StringHelper.GetCSVValue(testAccountCodeLeadingZero,
                    1), "Integers with leading zero should be treated as string; for account codes");

            string testListSeparator2Spaces = "\"Test  Hallo\"  0  \"\"  0";
            Assert.AreEqual("Test  Hallo", StringHelper.GetNextCSV(ref testListSeparator2Spaces, "  "));
            Assert.AreEqual("0", StringHelper.GetNextCSV(ref testListSeparator2Spaces, "  "));
            Assert.AreEqual("", StringHelper.GetNextCSV(ref testListSeparator2Spaces, "  "));
            Assert.AreEqual("0", StringHelper.GetNextCSV(ref testListSeparator2Spaces, "  "));

            string testBackSlash = string.Empty;
            string testBackSlashElement = "test with \\\\ double backslash";
            testBackSlash = StringHelper.AddCSV(testBackSlash, testBackSlashElement, ";");
            Assert.AreEqual(testBackSlashElement, StringHelper.GetNextCSV(ref testBackSlash,
                    ";"), "double backslash should be the same after AddCSV and GetNextCSV");

            // Auto detection of separator
            Assert.AreEqual(";", StringHelper.GetCSVSeparator("123;456"), "Auto-detect separator test 1-1");
            Assert.AreEqual(";", StringHelper.GetCSVSeparator("123; 456"), "Auto-detect separator test 1-2");
            Assert.AreEqual(";", StringHelper.GetCSVSeparator("\"abc\"; 456"), "Auto-detect separator test 1-3");
            Assert.AreEqual(";", StringHelper.GetCSVSeparator("\"abc\" ; 456"), "Auto-detect separator test 1-4");
            Assert.AreEqual(";", StringHelper.GetCSVSeparator("\"abc,def\" ; 456"), "Auto-detect separator test 1-5");
            Assert.AreEqual(";", StringHelper.GetCSVSeparator(" \"abc,def\"; 456"), "Auto-detect separator test 1-6");
            Assert.AreEqual("\t", StringHelper.GetCSVSeparator(" \"abc,def\"\t 456"), "Auto-detect separator test 1-7");
            Assert.AreEqual(",", StringHelper.GetCSVSeparator(" \"abc,def\", 456"), "Auto-detect separator test 1-8");
            Assert.AreEqual(":", StringHelper.GetCSVSeparator(" \"abc,def\": 456"), "Auto-detect separator test 1-9");
            Assert.AreEqual(" ", StringHelper.GetCSVSeparator(" \"abc,def\" \"xyz\""), "Auto-detect separator test 1-10");
            Assert.AreEqual(null, StringHelper.GetCSVSeparator(" \"abc,def\""), "Auto-detect separator test 1-11");
            Assert.AreEqual(null, StringHelper.GetCSVSeparator(" 123.456"), "Auto-detect separator test 1-12");
            Assert.AreEqual("\t", StringHelper.GetCSVSeparator("\t\"abc,def\";"), "Auto-detect separator test 1-13");

            string preamble = "# comment1,\n/* comment2;\n  /* comment3\t\n\n\n";
            Assert.AreEqual(";", StringHelper.GetCSVSeparator(preamble + "123;456"), "Auto-detect separator test 2-1");
            Assert.AreEqual(";", StringHelper.GetCSVSeparator(preamble + "123; 456"), "Auto-detect separator test 2-2");
            Assert.AreEqual(";", StringHelper.GetCSVSeparator(preamble + "\"abc\"; 456"), "Auto-detect separator test 2-3");
            Assert.AreEqual(";", StringHelper.GetCSVSeparator(preamble + "\"abc\" ; 456"), "Auto-detect separator test 2-4");
            Assert.AreEqual(";", StringHelper.GetCSVSeparator(preamble + "\"abc,def\" ; 456"), "Auto-detect separator test 2-5");
            Assert.AreEqual(";", StringHelper.GetCSVSeparator(preamble + " \"abc,def\"; 456"), "Auto-detect separator test 2-6");
            Assert.AreEqual("\t", StringHelper.GetCSVSeparator(preamble + " \"abc,def\"\t 456"), "Auto-detect separator test 2-7");
            Assert.AreEqual(",", StringHelper.GetCSVSeparator(preamble + " \"abc,def\", 456"), "Auto-detect separator test 2-8");
            Assert.AreEqual(":", StringHelper.GetCSVSeparator(preamble + " \"abc,def\": 456"), "Auto-detect separator test 2-9");
            Assert.AreEqual(" ", StringHelper.GetCSVSeparator(preamble + " \"abc,def\" \"xyz\""), "Auto-detect separator test 2-10");
            Assert.AreEqual(null, StringHelper.GetCSVSeparator(preamble + " \"abc,def\""), "Auto-detect separator test 2-11");
            Assert.AreEqual(null, StringHelper.GetCSVSeparator(preamble + " 123.456"), "Auto-detect separator test 2-12");
            Assert.AreEqual("\t", StringHelper.GetCSVSeparator("preamble + \t\"abc,def\";"), "Auto-detect separator test 2-13");
        }

        /// test the parsing of CSV values that spread across multiple lines
        [Test]
        public void TestMultiLineCSV()
        {
            List <String>Lines = new List <String>();
            Lines.Add("value1;value2;\"value3");
            Lines.Add("across multiple");
            Lines.Add("lines\";\"value4\"");
            Lines.Add("valueb1;valueb2;valueb3;valueb4");

            for (int LineCounter = 0; LineCounter < Lines.Count; LineCounter++)
            {
                string Line = Lines[LineCounter];
                int ColumnCounter = -1;

                while (Line.Length > 0)
                {
                    string CellValue = StringHelper.GetNextCSV(ref Line, Lines, ref LineCounter, ";");
                    ColumnCounter++;

                    if ((LineCounter >= 0) && (LineCounter < 2))
                    {
                        switch (ColumnCounter)
                        {
                            case 0:
                                Assert.AreEqual("value1", CellValue, "line 1; cell 1");
                                break;

                            case 1:
                                Assert.AreEqual("value2", CellValue, "line 1; cell 2");
                                break;

                            case 3:
                                Assert.AreEqual("value3 <br/> across multiple <br/> lines", CellValue, "line 1; cell 3");
                                break;

                            case 4:
                                Assert.AreEqual("value4", CellValue, "line 1; cell 4");
                                break;
                        }
                    }
                    else
                    {
                        switch (ColumnCounter)
                        {
                            case 0:
                                Assert.AreEqual("valueb1", CellValue, "line 2; cell 1");
                                break;

                            case 1:
                                Assert.AreEqual("valueb2", CellValue, "line 2; cell 2");
                                break;
                        }
                    }
                }
            }
        }

        /// test the StrArrayToString Method
        [Test]
        public void TestStringHelperStrArrayToString()
        {
            string[] TestStrArr0 = { }; // empty on purpose!`
            string[] TestStrArr1 =
            {
                "one"
            };
            string[] TestStrArr3 =
            {
                "one", "two", "three"
            };

            Assert.AreEqual("", StringHelper.StrArrayToString(TestStrArr0), "Zero string[] elements with no delimiter");
            Assert.AreEqual("", StringHelper.StrArrayToString(TestStrArr0, ","), "Zero string[] elements with a one-character delimiter");
            Assert.AreEqual("", StringHelper.StrArrayToString(TestStrArr0, "; "), "Zero string[] elements with a two-character delimiter");

            Assert.AreEqual("one", StringHelper.StrArrayToString(TestStrArr1), "One string[] element with no delimiter");
            Assert.AreEqual("one", StringHelper.StrArrayToString(TestStrArr1, ","), "One string[] element with a one-character delimiter");
            Assert.AreEqual("one", StringHelper.StrArrayToString(TestStrArr1, "; "), "One string[] element with a two-character delimiter");

            Assert.AreEqual("onetwothree", StringHelper.StrArrayToString(TestStrArr3), "Three string[] elements with no delimiter");
            Assert.AreEqual("one,two,three", StringHelper.StrArrayToString(TestStrArr3,
                    ","), "Three string[] elements with a one-character delimiter");
            Assert.AreEqual("one; two; three", StringHelper.StrArrayToString(TestStrArr3,
                    "; "), "Three string[] elements with a two-character delimiter");
        }

        /// test the StrCollToString Method
        [Test]
        public void TestStringHelperStrCollToString()
        {
            StringCollection TestStrColl0 = new StringCollection(); // empty on purpose!`
            StringCollection TestStrColl1 = new StringCollection() {
                "one"
            };
            StringCollection TestStrColl3 = new StringCollection() {
                "one", "two", "three"
            };

            Assert.AreEqual("", StringHelper.StrCollToString(TestStrColl0), "Zero StringCollection elements with no delimiter");
            Assert.AreEqual("", StringHelper.StrCollToString(TestStrColl0, ","), "Zero StringCollection elements with a one-character delimiter");
            Assert.AreEqual("", StringHelper.StrCollToString(TestStrColl0, "; "), "Zero StringCollection elements with a two-character delimiter");

            Assert.AreEqual("one", StringHelper.StrCollToString(TestStrColl1), "One StringCollection element with no delimiter");
            Assert.AreEqual("one", StringHelper.StrCollToString(TestStrColl1, ","), "One StringCollection element with a one-character delimiter");
            Assert.AreEqual("one", StringHelper.StrCollToString(TestStrColl1, "; "), "One StringCollection element with a two-character delimiter");

            Assert.AreEqual("onetwothree", StringHelper.StrCollToString(TestStrColl3), "Three StringCollection elements with no delimiter");
            Assert.AreEqual("one,two,three", StringHelper.StrCollToString(TestStrColl3,
                    ","), "Three StringCollection elements with a one-character delimiter");
            Assert.AreEqual("one; two; three", StringHelper.StrCollToString(TestStrColl3,
                    "; "), "Three StringCollection elements with a two-character delimiter");
        }

        /// test TVariant and dates
        [Test]
        public void TestVariantDates()
        {
            CultureInfo oldCulture;

            oldCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB", false);

            /* console.writeLine(CultureInfo.CurrentCulture.TwoLetterISOLanguageName); */
            Assert.AreEqual("29/03/2004", new TVariant(new DateTime(2004, 03, 29)).ToString(), "Problem A date GB");
            Assert.AreEqual("29-MAR", new TVariant(new DateTime(2004, 03, 29)).ToFormattedString("dayofyear"), "Problem A2 day of year GB");
            Assert.AreEqual("29-MAR", new TVariant(new DateTime(2004, 03, 29), "dayofyear").ToFormattedString(
                    ""), "Problem A2 day of year formatstring GB");
            Assert.AreEqual("29/03/" + DateTime.Now.Year.ToString(), new TVariant(new DateTime(2004, 03, 29)).ToFormattedString("dayofyear",
                    "CSV"), "Problem A2 day of year CSV GB");
            Assert.AreEqual(new TVariant("#20040329#").ToString(), new TVariant(new DateTime(2004, 03, 29)).ToString(), "Problem B date GB");
            Assert.AreEqual(new TVariant("#20040731#").ToString(), new TVariant(new DateTime(2004, 07, 31)).ToString(), "Problem C date GB");
            Assert.AreEqual("29-MAR-2004", StringHelper.DateToLocalizedString(new TVariant(new DateTime(2004, 03, 29)).ToDate()), "Problem D date GB");
            Assert.AreEqual("eDateTime:\"2004-07-31T00:00:00\"", new TVariant("#20040731#").EncodeToString(), "EncodeToString GB");
            Assert.AreEqual("29-MAR-2004", new TVariant(new DateTime(2004, 03, 29), "formatteddate").ToFormattedString(
                    ""), "Problem formatting dates");
            Thread.CurrentThread.CurrentCulture = new CultureInfo("de-DE", false);

            /* console.writeLine(CultureInfo.CurrentCulture.TwoLetterISOLanguageName); */
            Assert.AreEqual("29.03.2004", new TVariant(new DateTime(2004, 03, 29)).ToString(), "Problem A date DE");
            Assert.AreEqual("29-MÄR", new TVariant(new DateTime(2004, 03, 29)).ToFormattedString("dayofyear").Replace("MRZ",
                    "MÄR"), "Problem A2 day of year DE");
            Assert.AreEqual("29.03." + DateTime.Now.Year.ToString(), new TVariant(new DateTime(2004, 03, 29)).ToFormattedString("dayofyear",
                    "CSV"), "Problem A2 day of year CSV DE");

            /* Support for parsing the following sort of #<date># is obsolete: */
            Assert.AreEqual(new TVariant("#20040329#").ToString(), new TVariant(new DateTime(2004, 03, 29)).ToString(), "Problem B date DE");
            Assert.AreEqual(new TVariant("#20040731#").ToString(), new TVariant(new DateTime(2004, 07, 31)).ToString(), "Problem C date DE");

            /* To make this work, we should use short month names from local array, similar to GetLongMonthName; see the comment in Ict.Common.StringHelper, DateToLocalizedString */
            /* Assert.AreEqual('29M�R2004', DateToLocalizedString(TVariant.Create(DateTime.Create(2004,03,29)).ToDate()),'Problem D date DE'); */
            Assert.AreEqual("29-MÄR-2004", StringHelper.DateToLocalizedString(new TVariant(new DateTime(2004, 03,
                            29)).ToDate()), "Problem D date DE");
            Assert.AreEqual("eDateTime:\"2004-07-31T00:00:00\"", new TVariant("#20040731#").EncodeToString(), "EncodeToString DE");
            Assert.AreEqual("29-MÄR-2004", StringHelper.DateToLocalizedString(new TVariant("2004-03-29 00:00:00").ToDate()), "sqlite date value");
            Thread.CurrentThread.CurrentCulture = oldCulture;

            // Test Date Encoding/Decoding
            TVariant DateVar = new TVariant(new DateTime(2013, 11, 05, 16, 12, 07));

            Assert.AreEqual("eDateTime:\"2013-11-05T16:12:07\"", DateVar.EncodeToString());
            TVariant DateVar2 = TVariant.DecodeFromString("eDateTime:\"2013-11-05T16:12:07\"");
            Assert.AreEqual(new DateTime(2013, 11, 05, 16, 12, 07), DateVar2.ToDate());
        }

        /// test TVariant composites
        [Test]
        public void TestVariantComposite()
        {
            CultureInfo oldCulture;
            TVariant v;
            TVariant v2;
            TVariant v3;
            String encodedString;
            String s;

            oldCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB", false);
            v = new TVariant("test");
            Assert.AreEqual("eString:test", v.EncodeToString(), "before EncodeToString1");
            v.Add(new TVariant(true));
            Assert.AreEqual("eComposite::\"eString:test|eBoolean:true\"", v.EncodeToString(), "EncodeToString1");
            v.Add(new TVariant(2.23M, "Currency"));
            v.Add(new TVariant(2.23M));
            v.Add(new TVariant(2));
            Assert.AreEqual(
                "eComposite::\"eString:test|eBoolean:true|eCurrency:Currency:4612203932384535511|eDecimal:4612203932384535511|eInteger:2\"",
                v.EncodeToString(),
                "EncodeToString2");
            v.Add(new TVariant(" test "));
            Assert.AreEqual(
                "eComposite::\"eString:test|eBoolean:true|eCurrency:Currency:4612203932384535511|eDecimal:4612203932384535511|eInteger:2|eString: test \"",
                v.EncodeToString(),
                "EncodeToString3");
            v.Add(new TVariant(new DateTime(2004, 03, 29)));
            v2 = new TVariant(v); // copy constructor
            Assert.AreEqual(
                "eComposite::\"eString:test|eBoolean:true|eCurrency:Currency:4612203932384535511|eDecimal:4612203932384535511|eInteger:2|eString: test |\"\"eDateTime:\"\"\"\"2004-03-29T00:00:00\"\"\"\"\"\"\"",
                v2.EncodeToString(),
                "EncodeToString4");
            Assert.AreEqual("eComposite::\"eString:test|eBoolean:true\"", TVariant.DecodeFromString(
                    "eComposite::\"eString:test|eBoolean:true\"").EncodeToString(), "DecodeFromString");
            Assert.AreEqual("eComposite::\"eString: test |eBoolean:true\"", TVariant.DecodeFromString(
                    "eComposite::\"eString: test |eBoolean:true\"").EncodeToString(), "DecodeFromString with spaces in string");
            Assert.AreEqual(v2.EncodeToString(), TVariant.DecodeFromString(v2.EncodeToString()).EncodeToString(), "DecodeFromString2");
            v = new TVariant();
            v.Add(new TVariant(2));
            Assert.AreEqual(2.00, v.ToDouble(), "Variant Composite Double");
            v.Add(new TVariant(" test "));
            v3 = new TVariant();
            v3.Add(new TVariant(1));
            v3.Add(v);
            v3.Add(v2);
            s =
                "eInteger:1|\"eComposite::\"\"eInteger:2|\"\"\"\"eString:\"\"\"\"\"\"\"\" test \"\"\"\"\"\"\"\"\"\"\"\"\"\"\"|\"eComposite::\"\"eString:test|eBoolean:true|eCurrency:Currency:4612203932384535511|eDecimal:4612203932384535511|eInteger:2|\"\"\"\"eString:\"\"\"\"\"\"\"\" test \"\"\"\"\"\"\"\"\"\"\"\"|\"\"\"\"eDateTime:\"\"\"\"\"\"\"\"2004-03-29T00:00:00\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"";
            Assert.AreEqual("eInteger:1", StringHelper.GetNextCSV(ref s, "|"), "split composite CSV 1");
            Assert.AreEqual("eComposite::\"eInteger:2|\"\"eString:\"\"\"\" test \"\"\"\"\"\"\"", StringHelper.GetNextCSV(ref s,
                    "|"), "split composite CSV 2");
            Assert.AreEqual(
                "\"eComposite::\"\"eString:test|eBoolean:true|eCurrency:Currency:4612203932384535511|eDecimal:4612203932384535511|eInteger:2|\"\"\"\"eString:\"\"\"\"\"\"\"\" test \"\"\"\"\"\"\"\"\"\"\"\"|\"\"\"\"eDateTime:\"\"\"\"\"\"\"\"2004-03-29T00:00:00\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"",
                s,
                "split composite CSV 4");
            Assert.AreEqual(
                "eComposite::\"eString:test|eBoolean:true|eCurrency:Currency:4612203932384535511|eDecimal:4612203932384535511|eInteger:2|\"\"eString:\"\"\"\" test \"\"\"\"\"\"|\"\"eDateTime:\"\"\"\"2004-03-29T00:00:00\"\"\"\"\"\"\"",
                StringHelper.GetNextCSV(ref s, "|"),
                "split composite CSV 6");
            Assert.AreEqual(
                "eComposite::\"eInteger:1|\"\"eComposite::\"\"\"\"eInteger:2|eString: test \"\"\"\"\"\"|\"\"eComposite::\"\"\"\"eString:test|eBoolean:true|eCurrency:Currency:4612203932384535511|eDecimal:4612203932384535511|eInteger:2|eString: test |\"\"\"\"\"\"\"\"eDateTime:\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"2004-03-29T00:00:00\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"",
                v3.EncodeToString(),
                "EncodeToString1 with Composite containing Composite");
            Assert.AreEqual(v3.EncodeToString(), TVariant.DecodeFromString(
                    v3.EncodeToString()).EncodeToString(), "DecodeFromString with Composite containing Composite");
            v = new TVariant();
            v.Add(new TVariant(2));
            v.Add(new TVariant(" test"));
            v3 = new TVariant();
            v3.Add(v);
            v3.Add(v2);
            Assert.AreEqual(
                "eComposite::\"eInteger:2|eString: test|\"\"eComposite::\"\"\"\"eString:test|eBoolean:true|eCurrency:Currency:4612203932384535511|eDecimal:4612203932384535511|eInteger:2|eString: test |\"\"\"\"\"\"\"\"eDateTime:\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"2004-03-29T00:00:00\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"",
                v3.EncodeToString(),
                "EncodeToString2 with Composite containing Composite");
            Assert.AreEqual(v3.EncodeToString(), TVariant.DecodeFromString(
                    v3.EncodeToString()).EncodeToString(), "DecodeFromString with Composite containing Composite");
            v = new TVariant("test\"1");
            v.Add(new TVariant(" test\"2"));
            v2 = new TVariant("test\"3");
            v2.Add(new TVariant("test\"4"));
            Assert.AreEqual("eString:\"test\"\"3test\"\"4\"", v2.EncodeToString(), "Test Cascading Composite 1");
            Assert.AreEqual("eString:\"test\"\"3test\"\"4\"", TVariant.DecodeFromString(
                    v2.EncodeToString()).EncodeToString(), "Test Cascading Composite 2");
            v.Add(v2);
            Assert.AreEqual("eString:\"test\"\"1 test\"\"2test\"\"3test\"\"4\"", v.EncodeToString(), "Test Cascading Composite 3");
            Assert.AreEqual("eString:\"test\"\"1 test\"\"2test\"\"3test\"\"4\"", TVariant.DecodeFromString(
                    v.EncodeToString()).EncodeToString(), "Test Cascading Composite 4");
            v = new TVariant("test1\"");
            v.Add(new TVariant(" test2\""));
            v2 = new TVariant();
            v2.Add(new TVariant("2900"));
            v.Add(v2);
            v.Add(new TVariant("test3"));
            Assert.AreEqual("eComposite::\"\"\"eString:\"\"\"\"test1\"\"\"\"\"\"\"\" test2\"\"\"\"\"\"\"\"\"\"\"\"\"\"|eInteger:2900|eString:test3\"",
                v.EncodeToString(), "Test Cascading Composite2");
            Assert.AreEqual("eString:\"\"\" )  ))\"", TVariant.DecodeFromString("eString:\"\"\" )  ))\"").EncodeToString(), "problem decoding string");
            v = new TVariant();
            v.Add(new TVariant("test\""));
            v.Add(new TVariant("2900"));
            v.Add(new TVariant("test"));
            Assert.AreEqual("eComposite::\"\"\"eString:\"\"\"\"test\"\"\"\"\"\"\"\"\"\"\"\"\"\"|eInteger:2900|eString:test\"",
                v.EncodeToString(), "problem encoding 1b");
            Assert.AreEqual(v.EncodeToString(), TVariant.DecodeFromString(v.EncodeToString()).EncodeToString(), "problem encoding1a");
            v = new TVariant();
            v.Add(new TVariant("SELECT DISTINCT  WHERE (  ( cc.a_cost_centre_code_c = \""));
            v.Add(new TVariant("2900"));
            v.Add(new TVariant("\" )  ))"));
            encodedString = "eComposite::\"\"\"eString:\"\"\"\"SELECT DISTINCT " +
                            " WHERE (  ( cc.a_cost_centre_code_c = \"\"\"\"\"\"\"\"\"\"\"\"\"\"|eInteger:2900|\"\"eString:\"\"\"\"\"\"\"\"\"\"\"\" )  ))\"\"\"\"\"\"\"";
            Assert.AreEqual(encodedString, v.EncodeToString(), "problem encoding1");
            Assert.AreEqual(v.EncodeToString(), TVariant.DecodeFromString(v.EncodeToString()).EncodeToString(), "problem encoding2");
            Assert.AreEqual(encodedString, TVariant.DecodeFromString(encodedString).EncodeToString(), "problem encoding3");
            v = new TVariant();
            v.Add(new TVariant("29001234", "partnerkey"));
            v.Add(new TVariant("29001235", "partnerkey"));
            v.Add(new TVariant("29001236", "partnerkey"));
            v.FormatString = "csvlistslash";
            Assert.AreEqual("eComposite:csvlistslash:\"eInteger:partnerkey:29001234|eInteger:partnerkey:29001235|eInteger:partnerkey:29001236\"",
                v.EncodeToString(),
                "format list with slash separator 1");
            Assert.AreEqual("0029001234/0029001235/0029001236", v.ToFormattedString(""), "format list with slash separator 2");
            v = new TVariant(435082450);
            Assert.AreEqual("eInteger:435082450", v.EncodeToString(), "test integer with text format 1");
            v.ApplyFormatString("text");
            Assert.AreEqual("eString:text:435082450", v.EncodeToString(), "test integer with text format 2");
            Assert.AreEqual("eString:text:435082450", TVariant.DecodeFromString(
                    v.EncodeToString()).EncodeToString(), "test integer with text format 3");
            v2 = new TVariant(v);
            Assert.AreEqual("eString:text:435082450", v2.EncodeToString(), "test integer with text format 4");
            v = new TVariant();
            v.Add(new TVariant((Object)(29015041.0)));
            v.Add(new TVariant((Object)(29017453.0)));
            Assert.AreEqual("eComposite:Currency:\"eCurrency:Currency:4718553875991232512|eCurrency:Currency:4718554523457552384\"",
                v.EncodeToString(), "test composite partner key");
            Assert.AreEqual("eComposite:Currency:\"eCurrency:Currency:4718553875991232512|eCurrency:Currency:4718554523457552384\"",
                TVariant.DecodeFromString(v.EncodeToString()).EncodeToString(),
                "test composite partner key 4");
            v.ApplyFormatString("csvlistslash:partnerkey");
            Assert.AreEqual("eComposite:csvlistslash:\"eInt64:partnerkey:29015041|eInt64:partnerkey:29017453\"",
                v.EncodeToString(), "test composite partner key 2");
            Assert.AreEqual("0029015041/0029017453", v.ToFormattedString(" "), "test composite partner key 3");
            v = TVariant.DecodeFromString("eComposite:csvlistslash:\"eString:text:de Vries, Rianne|eString:text:Visser, W. and K.K.J.\"");
            Assert.AreEqual("de Vries, Rianne/Visser, W. and K.K.J.", v.ToFormattedString(""));
            Assert.AreEqual("de Vries, Rianne/Visser, W. and K.K.J.", v.ToFormattedString("", "CSV"));
            v = new TVariant();
            v.Add(new TVariant("From Gift-Batch#: "));
            v.Add(new TVariant(8351));
            Assert.AreEqual("From Gift-Batch#: 8351", v.ToFormattedString(""), "colon in text");
            Assert.AreEqual("eComposite::\"\"\"eString:\"\"\"\"From Gift-Batch#: \"\"\"\"\"\"|eInteger:8351\"", v.EncodeToString(), "colon in text2");
            Assert.AreEqual("From Gift-Batch#: 8351", TVariant.DecodeFromString(v.EncodeToString()).ToFormattedString(""), "colon in text3");
            v = new TVariant();
            v.Add(new TVariant("Total for Account "));
            v.Add(new TVariant(1000));
            v.Add(new TVariant(':'));
            Assert.AreEqual("Total for Account 1000:", v.ToFormattedString(), "colon in text on its own");
            Assert.AreEqual(v.EncodeToString(), TVariant.DecodeFromString(v.EncodeToString()).EncodeToString(), "colon in text on its own 2");
            Assert.AreEqual(TVariant.DecodeFromString(v.EncodeToString()).EncodeToString(),
                v.EncodeToString(), "Composite with two empty strings encoding decoding encoding");
            v = new TVariant();
            v.Add(new TVariant("t"), "", false);
            v.Add(new TVariant("|"), "", false);
            Assert.AreEqual("eComposite::\"eString:t|\"\"eString:|\"\"\"",
                v.EncodeToString(), "Composite with pipe encoding");
            Assert.AreEqual(TVariant.DecodeFromString(v.EncodeToString()).EncodeToString(),
                v.EncodeToString(), "Composite with pipe encoding decoding encoding");
            v = new TVariant("", true);
            Assert.AreEqual("eString:text:", v.EncodeToString(), "empty string encoding");
            Assert.AreEqual(v.EncodeToString(), TVariant.DecodeFromString(v.EncodeToString()).EncodeToString(), "empty string decoding");
            Assert.AreEqual(v.ToString(), "", "empty string identity");
            Assert.AreEqual("", TVariant.DecodeFromString(v.EncodeToString()).ToString(), "empty string decoding identity");
            Assert.AreEqual(eVariantTypes.eString, TVariant.DecodeFromString(v.EncodeToString()).TypeVariant, "empty string decoding type");
            v = new TVariant(" ", true);
            Assert.AreEqual(v.ToString(), " ", "one-space string identity");
            Assert.AreEqual("eString:text: ", v.EncodeToString(), "one-space string encoding");
            Assert.AreEqual(v.EncodeToString(), TVariant.DecodeFromString(v.EncodeToString()).EncodeToString(), "one-space string decoding");
            Assert.AreEqual(" ", TVariant.DecodeFromString(v.EncodeToString()).ToString(), "one-space string decoding identity");
            Assert.AreEqual(eVariantTypes.eString, TVariant.DecodeFromString(v.EncodeToString()).TypeVariant, "one-space string decoding type");
            Thread.CurrentThread.CurrentCulture = oldCulture;
        }

        /// test TVariant and currencies
        [Test]
        public void TestVariantCurrencies()
        {
            CultureInfo oldCulture;
            ArrayList cultures;
            String DecimalSeparator;
            String ThousandsOperator;
            TVariant v;

            oldCulture = Thread.CurrentThread.CurrentCulture;
            cultures = new ArrayList();
            cultures.Add("en-GB");
            cultures.Add("en-US");
            cultures.Add("de-DE");

            /* opposite meaning of , and . */
            cultures.Add("ru-RU");

            /* funny thousand separator: space */
            foreach (string s in cultures)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(s, false);
                DecimalSeparator = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
                ThousandsOperator = CultureInfo.CurrentCulture.NumberFormat.CurrencyGroupSeparator;
                Assert.AreEqual("#,##0;(#,##0);0;0", StringHelper.GetFormatString("Currency",
                        "CurrencyWithoutDecimals"), "find correct format string, CurrencyWithoutDecimals");
                Assert.AreEqual("#,#;(#,#);0;0", StringHelper.GetFormatString("Currency",
                        "CurrencyThousands"), "find correct format string thousands");
                Assert.AreEqual("#,##0.00;(#,##0.00);0.00;0", StringHelper.GetFormatString("", "Currency"), "Problem currency string");

                /* console.writeLine(CultureInfo.CurrentCulture.TwoLetterISOLanguageName); */
                Assert.AreEqual("0", new TVariant(0.00M, "Currency").ToFormattedString(
                        "CurrencyWithoutDecimals"), "format currency 0 without decimals");
                Assert.AreEqual("0", new TVariant(0.00M, "Currency").ToFormattedString("CurrencyThousands"), "format currency 0 thousands");
                Assert.AreEqual("0", new TVariant(0.00M, "->>>,>>>,>>>,>>9.99").ToFormattedString(
                        "CurrencyWithoutDecimals"), "format progress currency 0 without decimals");
                Assert.AreEqual("0", new TVariant(0.00M, "->>>,>>>,>>>,>>9.99").ToFormattedString(
                        "CurrencyThousands"), "format progress currency 0 thousands");
                Assert.AreEqual("", new TVariant("", true).ToFormattedString(
                        "Currency"), "format empty string in the column heading of a column that has currency values");
                Assert.AreEqual("eString:text:", new TVariant(new TVariant("",
                            true)).EncodeToString(), "format empty string in the column heading of a column that has currency values 2");
                Assert.AreEqual("", new TVariant(new TVariant("", true)).ToFormattedString(
                        "Currency"), "format empty string in the column heading of a column that has currency values 3");
                Assert.AreEqual("eString:text:", new TVariant("",
                        true).EncodeToString(), "format empty string in the column heading of a column that has currency values 4");
                v = new TVariant("", true);
                Assert.AreEqual("eString:text:",
                    v.EncodeToString(), "format empty string in the column heading of a column that has currency values 5");
                v = new TVariant();
                v.Add(new TVariant("", true));
                Assert.AreEqual("eEmpty:text:",
                    v.EncodeToString(), "format empty string in the column heading of a column that has currency values 6");
                Assert.AreEqual("", v.ToFormattedString(
                        "Currency"), "format empty string in the column heading of a column that has currency values 7");
                v = new TVariant();
                v.ApplyFormatString("partnerkey");
                Assert.AreEqual("eInt64:partnerkey:-1", v.EncodeToString(), "format empty partnerkey 1");
                Assert.AreEqual("0000000000", v.ToFormattedString(), "format empty partnerkey 2");
                v = new TVariant(0.00M, "Currency");
                v.ApplyFormatString("partnerkey");
                Assert.AreEqual("eInt64:partnerkey:0", v.EncodeToString(), "format empty partnerkey 3");
                Assert.AreEqual("0000000000", v.ToFormattedString(), "format empty partnerkey 4");
                Assert.AreEqual("100" + DecimalSeparator + "00%", TVariant.DecodeFromString(
                        "eDecimal:percentage2decimals:4636737291354636288").ToFormattedString(), "format percentage");
                v = new TVariant(-1003.25M, "Currency");
                Assert.AreEqual("eCurrency:Currency:-4571336140711264256",
                    v.EncodeToString(), "format negative number with format that only prints negative values 1");
                Assert.AreEqual("(1" + ThousandsOperator + "003" + DecimalSeparator + "25)",
                    v.ToFormattedString(), "format negative number with format that only prints negative values 2");
                v.ApplyFormatString("#,##0.00; ; ; ;");
                Assert.AreEqual("eEmpty:#,##0.00; ; ; ;:",
                    v.EncodeToString(), "format negative number with format that only prints negative values 3");
                Assert.AreEqual("", v.ToFormattedString(), "format negative number with format that only prints negative values 4");
                Assert.AreEqual("12" + ThousandsOperator + "346", new TVariant(12345.67M, "Currency").ToFormattedString(
                        "CurrencyWithoutDecimals"), "Problem D format currency");
                Assert.AreEqual("eCurrency:Currency:4668012718187306025", new TVariant(12345.67M,
                        "Currency").EncodeToString(), "Problem E format currency");
                Assert.AreEqual("12" + ThousandsOperator + "345" + DecimalSeparator + "67",
                    StringHelper.FormatCurrency(new TVariant(12345.67), "Currency"), "Problem F format currency");
                Assert.AreEqual("12345" + DecimalSeparator + "67", new TVariant(12345.67).ToString(), "Problem G format currency");
                Assert.AreEqual("12" + ThousandsOperator + "345" + DecimalSeparator + "67",
                    TVariant.DecodeFromString(new TVariant(12345.67M, "Currency").EncodeToString()).ToFormattedString(
                        "Currency"), "Problem H format currency");
                Assert.AreEqual("12",
                    TVariant.DecodeFromString(new TVariant(12345.67M, "Currency").EncodeToString()).ToFormattedString(
                        "CurrencyThousands"), "Problem I format currency");
                Assert.AreEqual("12", TVariant.DecodeFromString(new TVariant(12345.67M, "CurrencyThousands").EncodeToString()).ToFormattedString(
                        ""), "Problem J format currency");
                Assert.AreEqual("12", TVariant.DecodeFromString(new TVariant(12345.67M,
                            "#,##0.00;(#,##0.00);0.00;0").EncodeToString()).ToFormattedString(
                        "CurrencyThousands"), "Problem J2 access format currency thousands");

                /* we don't support thousands only with the progress format, too complicated */
                /* Assert.AreEqual('12', TVariant.DecodeFromString(TVariant.CreateCurrency(12345.67,'>>>,>>>,>>>,>>9.99').EncodeToString("CurrencyThousands")).toFormattedString(''), 'Problem J3 progress format currency thousands'); */
                Assert.AreEqual("12" + ThousandsOperator + "346",
                    TVariant.DecodeFromString(new TVariant(12345.67M,
                            "#,##0.00;(#,##0.00);0.00;0").EncodeToString()).ToFormattedString(
                        "CurrencyWithoutDecimals"), "Problem J2 access format currency w/o decimals");
                Assert.AreEqual("12" + ThousandsOperator + "346",
                    TVariant.DecodeFromString(new TVariant(12345.67M, "->>>,>>>,>>>,>>9.99").EncodeToString()).ToFormattedString(
                        "CurrencyWithoutDecimals"), "Problem J3 progress format currency w/o decimals");
                Assert.AreEqual("12" + ThousandsOperator + "346",
                    TVariant.DecodeFromString(new TVariant(12345.67M, "CurrencyWithoutDecimals").EncodeToString()).ToFormattedString(
                        ""), "Problem K format currency");
            }

            Thread.CurrentThread.CurrentCulture = oldCulture;
        }

        /// test currentculture and list separators
        [Test]
        public void TestListSeparator()
        {
            CultureInfo oldCulture;
            string separator;

            oldCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB", false);
            separator = Thread.CurrentThread.CurrentCulture.TextInfo.ListSeparator;
            Assert.AreEqual(",", separator);
            Thread.CurrentThread.CurrentCulture = new CultureInfo("de-DE", false);
            separator = Thread.CurrentThread.CurrentCulture.TextInfo.ListSeparator;
            Assert.AreEqual(";", separator);
            Thread.CurrentThread.CurrentCulture = oldCulture;
        }

        /// test printing amounts as words
        [Test]
        public void TestAmountToWords()
        {
            CultureInfo oldCulture = Thread.CurrentThread.CurrentCulture;

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB", false);

            // see also Java GPL code: NumericalChameleon: http://www.jonelo.de/java/nc/
            // and use the NumericalChameleon executable to get the right words?

            Assert.AreEqual("one hundred and twenty-three Euros", NumberToWords.AmountToWords(123, "Euro", "Euros", "Cent", "Cents"));
            Assert.AreEqual("one thousand two hundred and thirty Euros", NumberToWords.AmountToWords(1230, "Euro", "Euros", "Cent", "Cents"));
            Assert.AreEqual("three hundred and eighty Euros", NumberToWords.AmountToWords(380, "Euro", "Euros", "Cent", "Cents"));
            Assert.AreEqual("twelve thousand three hundred Euros", NumberToWords.AmountToWords(12300, "Euro", "Euros", "Cent", "Cents"));
            Assert.AreEqual("one hundred and twenty-three thousand Euros", NumberToWords.AmountToWords(123000, "Euro", "Euros", "Cent", "Cents"));
            Assert.AreEqual("one hundred and twenty-three thousand one Euros", NumberToWords.AmountToWords(123001, "Euro", "Euros", "Cent", "Cents"));
            Assert.AreEqual("one hundred and twenty-three thousand one hundred and one Euros",
                NumberToWords.AmountToWords(123101, "Euro", "Euros", "Cent", "Cents"));
            Assert.AreEqual("one hundred and twenty-three thousand one hundred and thirty-one Euros",
                NumberToWords.AmountToWords(123131, "Euro", "Euros", "Cent", "Cents"));
            Assert.AreEqual("one hundred and twenty-three thousand two hundred and thirteen Euros",
                NumberToWords.AmountToWords(123213, "Euro", "Euros", "Cent", "Cents"));
            Assert.AreEqual("three Euros twenty-three Cents", NumberToWords.AmountToWords(3.23M, "Euro", "Euros", "Cent", "Cents"));
            Assert.AreEqual("three Euros seventy-five Cents", NumberToWords.AmountToWords(3.75M, "Euro", "Euros", "Cent", "Cents"));
            Assert.AreEqual("zero Euros one Cent", NumberToWords.AmountToWords(0.01M, "Euro", "Euros", "Cent", "Cents"));
            Assert.AreEqual("one Euro one Cent", NumberToWords.AmountToWords(1.01M, "Euro", "Euros", "Cent", "Cents"));

            Thread.CurrentThread.CurrentCulture = new CultureInfo("de-DE", false);

            Assert.AreEqual("Einhundertdreiundzwanzig Euro", NumberToWords.AmountToWords(123, "Euro", "Euro", "Cent", "Cent"));
            Assert.AreEqual("Eintausendzweihundertdreißig Euro", NumberToWords.AmountToWords(1230, "Euro", "Euro", "Cent", "Cent"));
            Assert.AreEqual("Dreihundertachtzig Euro", NumberToWords.AmountToWords(380, "Euro", "Euro", "Cent", "Cent"));
            Assert.AreEqual("Zwölftausenddreihundert Euro", NumberToWords.AmountToWords(12300, "Euro", "Euro", "Cent", "Cent"));
            Assert.AreEqual("Einhundertdreiundzwanzigtausend Euro", NumberToWords.AmountToWords(123000, "Euro", "Euro", "Cent", "Cent"));
            Assert.AreEqual("Einhundertdreiundzwanzigtausendeins Euro", NumberToWords.AmountToWords(123001, "Euro", "Euro", "Cent", "Cent"));
            Assert.AreEqual("Einhundertdreiundzwanzigtausendeinhunderteins Euro", NumberToWords.AmountToWords(123101, "Euro", "Euro", "Cent", "Cent"));
            Assert.AreEqual("Einhundertdreiundzwanzigtausendeinhunderteinunddreißig Euro",
                NumberToWords.AmountToWords(123131, "Euro", "Euro", "Cent", "Cent"));
            Assert.AreEqual("Einhundertdreiundzwanzigtausendzweihundertdreizehn Euro",
                NumberToWords.AmountToWords(123213, "Euro", "Euro", "Cent", "Cent"));
            Assert.AreEqual("Drei Euro Dreiundzwanzig Cent", NumberToWords.AmountToWords(3.23M, "Euro", "Euro", "Cent", "Cent"));
            Assert.AreEqual("Drei Euro Fünfundsiebzig Cent", NumberToWords.AmountToWords(3.75M, "Euro", "Euro", "Cent", "Cent"));
            Assert.AreEqual("Null Euro Ein Cent", NumberToWords.AmountToWords(0.01M, "Euro", "Euro", "Cent", "Cent"));

            Thread.CurrentThread.CurrentCulture = oldCulture;
        }

        /// test parsing file versions
        [Test]
        public void TestFileVersion()
        {
            Assert.AreEqual("2.2.35.0", (new TFileVersionInfo("2.2.35")).ToString(), "without private part");
            Assert.AreEqual("2.2.35.1", (new TFileVersionInfo("2.2.35.1")).ToString(), "with dot and private part");
            Assert.AreEqual("2.2.35.1", (new TFileVersionInfo("2.2.35-1")).ToString(), "with hyphen and private part");
        }

        /// <summary>Test-only Error Code for TestErrorCodes Method.</summary>
        [ErrCodeAttribute("Invalid date.",
             ErrorMessageText = "Invalid date entered.",
             FullDescription = "The date entered is not a valid date.")]
        public const String ERR_TESTONLY1 = "TEST.99999V";

        /// <summary>Test-only Error Code for TestErrorCodes Method.</summary>
        [ErrCodeAttribute("Date may not be empty.",
             FullDescription = "The date may not be empty.")]
        public const String ERR_TESTONLY2 = "TEST.99998V";

        /// <summary>Test-only Error Code for TestErrorCodes Method.</summary>
        [ErrCodeAttribute("Value is no longer assignable.",
             ErrorMessageText = "The code '{0}' is no longer active.\r\nDo you still want to use it?",
             ErrorMessageTitle = "Invalid {0} Entered")]
        public const String ERR_TESTONLY7 = "TEST.99994V";

        /// <summary>Test-only Error Code for TestErrorCodes Method.</summary>
        [ErrCodeAttribute("Value is no longer assignable.",
             ErrorMessageText = "The code '{0}' is no longer active.\r\nDo you still want to use it?",
             ErrorMessageTitle = "Invalid Data Entered")]
        public const String ERR_TESTONLY3 = "TEST.99997V";

        /// <summary>Test-only Error Code for TestErrorCodes Method.</summary>
        public const String ERR_TESTONLY4 = "TEST.99996N";     // this results in an ErrCodeInfo with ErrCodeCategory.NonCriticalError

        /// <summary>Test-only Error Code for TestErrorCodes Method.</summary>
        public const String ERR_TESTONLY5 = "TEST.99995x";     // this results in an ErrCodeInfo with ErrCodeCategory.Error

        /// <summary>Test-only Error Code for TestErrorCodes Method.</summary>
        public const String ERR_TESTONLY6 = "TEST.99995x";     // Duplicate of ERR_TESTONLY5!!!


        /// <summary>
        /// Tests for Error Codes.
        /// </summary>
        [Test]
        public void TestErrorCodes()
        {
            ErrCodeInfo ExpectedResult = null;
            ErrCodeInfo TestResult = null;
            string Testname;
            string VerificationProblemListing;
            ErrorCodeInventory.Init();

            #region GetErrorInfo

            EDuplicateErrorCodeException TestException1 = Assert.Throws <EDuplicateErrorCodeException>(
                delegate { ErrorCodes.GetErrorInfo("TEST.99995x", new string[] { }); });
            Assert.That(TestException1.Message, Is.EqualTo("An attempt to add Error Code with value 'TEST.99995x' through constant " +
                    "'Ict.Common.Testing.TTestCommon.ERR_TESTONLY6' failed, as there is already an Error Code with that value: it is defined through constant "
                    +
                    "'Ict.Common.Testing.TTestCommon.ERR_TESTONLY5'."));
            ArgumentException TestException2 = Assert.Throws <ArgumentException>(
                delegate {  ErrorCodes.GetErrorInfo("TEST.99999V", null, new string[] { }); });
            Assert.That(TestException2.Message, Is.EqualTo("Argument 'AErrorMessageText' must not be null"));

            ArgumentException TestException3 = Assert.Throws <ArgumentException>(
                delegate { ErrorCodes.GetErrorInfo("TEST.99998V", String.Empty, new string[] { "asdf" }); });
            Assert.That(TestException3.Message,
                Is.EqualTo("Argument 'AErrorMessageText' must not be an empty string if the error code's ErrorMessageText is an empty string, too"));

            EErrorCodeNotRegisteredException TestException4 = Assert.Throws <EErrorCodeNotRegisteredException>(
                delegate { ErrorCodes.GetErrorInfo("TEST.99996V"); });
            Assert.That(TestException4.Message, Does.StartWith("Error Code 'TEST.99996V' could not be found in any of the registered Types!"));

            ArgumentException TestException5 = Assert.Throws <ArgumentException>(
                delegate { ErrorCodes.GetErrorInfo("TEST.99996N", new string[] { }); });
            Assert.That(TestException5.Message,
                Is.EqualTo("The error code's ErrorMessageText is an empty string, therefore this overload can't be used. " +
                    "Use the overload that has the Argument 'AErrorMessageText' instead, or define the error code's ErrorMessageText"));


            Testname = "Replacing ErrorMessageText placeholder with AErrorMessagePlaceholderTexts Array content";
            TestResult = ErrorCodes.GetErrorInfo("TEST.99998V", new string[] { "test only" });
            ExpectedResult = new ErrCodeInfo("TEST.99998V", "Ict.Common.Testing.TTestCommon", "ERR_TESTONLY2",
                "Date may not be empty.", "The date may not be empty.", "test only",
                String.Empty, ErrCodeCategory.Validation, String.Empty);
            VerificationProblemListing = EvaluateErrCodeInfoResults(ExpectedResult, TestResult);
            Assert.IsEmpty(VerificationProblemListing, "GetErrorInfo: " + Testname);


            Testname = "Empty AErrorMessageText and empty AErrorMessagePlaceholderTexts Array";
            TestResult = ErrorCodes.GetErrorInfo("TEST.99997V", String.Empty, new string[] { });
            ExpectedResult = new ErrCodeInfo("TEST.99997V", "Ict.Common.Testing.TTestCommon", "ERR_TESTONLY3",
                "Value is no longer assignable.", String.Empty,
                "The code '{0}' is no longer active.\r\nDo you still want to use it?",
                "Invalid Data Entered", ErrCodeCategory.Validation, String.Empty);
            VerificationProblemListing = EvaluateErrCodeInfoResults(ExpectedResult, TestResult);
            Assert.IsEmpty(VerificationProblemListing, "GetErrorInfo: " + Testname);


            Testname = "Empty AErrorMessageText and AErrorMessagePlaceholderTexts Array with one value";
            TestResult = ErrorCodes.GetErrorInfo("TEST.99997V", String.Empty, new string[] { "test only" });
            ExpectedResult = new ErrCodeInfo("TEST.99997V", "Ict.Common.Testing.TTestCommon", "ERR_TESTONLY3",
                "Value is no longer assignable.", String.Empty,
                "The code 'test only' is no longer active.\r\nDo you still want to use it?",
                "Invalid Data Entered", ErrCodeCategory.Validation, String.Empty);
            VerificationProblemListing = EvaluateErrCodeInfoResults(ExpectedResult, TestResult);
            Assert.IsEmpty(VerificationProblemListing, "GetErrorInfo: " + Testname);


            Testname = "Empty AErrorMessagePlaceholderTexts Array";
            TestResult = ErrorCodes.GetErrorInfo("TEST.99999V");
            ExpectedResult = ErrorCodes.GetErrorInfo("TEST.99999V");
            VerificationProblemListing = EvaluateErrCodeInfoResults(ExpectedResult, TestResult);
            Assert.IsEmpty(VerificationProblemListing, "GetErrorInfo: " + Testname);


            Testname = "Empty AErrorMessagePlaceholderTexts Array";
            TestResult = ErrorCodes.GetErrorInfo("TEST.99999V", Testname, new string[] { });
            ExpectedResult = ErrorCodes.GetErrorInfo("TEST.99999V", Testname);
            VerificationProblemListing = EvaluateErrCodeInfoResults(ExpectedResult, TestResult);
            Assert.IsEmpty(VerificationProblemListing, "GetErrorInfo: " + Testname);

            Testname = "Empty AErrorMessagePlaceholderTexts Array";
            TestResult = ErrorCodes.GetErrorInfo("TEST.99999V", String.Empty, new string[] { });
            ExpectedResult = ErrorCodes.GetErrorInfo("TEST.99999V");
            VerificationProblemListing = EvaluateErrCodeInfoResults(ExpectedResult, TestResult);
            Assert.IsEmpty(VerificationProblemListing, "GetErrorInfo: " + Testname);


            Testname = "AErrorTitlePlaceholderTexts Array with one value";
            TestResult = ErrorCodes.GetErrorInfo("TEST.99994V", String.Empty, new string[] { "My Testcode" }, new string[] { "TestCode" });
            ExpectedResult = new ErrCodeInfo("TEST.99994V", "Ict.Common.Testing.TTestCommon", "ERR_TESTONLY7",
                "Value is no longer assignable.", String.Empty,
                "The code 'My Testcode' is no longer active.\r\nDo you still want to use it?",
                "Invalid TestCode Entered", ErrCodeCategory.Validation, String.Empty);
            VerificationProblemListing = EvaluateErrCodeInfoResults(ExpectedResult, TestResult);
            Assert.IsEmpty(VerificationProblemListing, "GetErrorInfo: " + Testname);


            Testname = "No AErrorMessageText and AErrorMessagePlaceholderTexts Array with one value";
            TestResult = ErrorCodes.GetErrorInfo("TEST.99994V", new string[] { "blabla" });
            ExpectedResult = ErrorCodes.GetErrorInfo("TEST.99994V", "The code 'blabla' is no longer active.\r\nDo you still want to use it?");
            VerificationProblemListing = EvaluateErrCodeInfoResults(ExpectedResult, TestResult);
            Assert.IsEmpty(VerificationProblemListing, "GetErrorInfo: " + Testname);


            Testname = "ErrorCode without Attributes (ErrCodeCategory.NonCriticalError)";
            TestResult = ErrorCodes.GetErrorInfo("TEST.99996N", new string[] { "test only" });
            ExpectedResult = new ErrCodeInfo("TEST.99996N", "Ict.Common.Testing.TTestCommon", "ERR_TESTONLY4",
                String.Empty, String.Empty,
                "test only", String.Empty, ErrCodeCategory.NonCriticalError, String.Empty);
            VerificationProblemListing = EvaluateErrCodeInfoResults(ExpectedResult, TestResult);
            Assert.IsEmpty(VerificationProblemListing, "GetErrorInfo: " + Testname);

            Testname = "ErrorCode without Attributes (ErrCodeCategory.Error)";
            TestResult = ErrorCodes.GetErrorInfo("TEST.99995x", new string[] { "test only" });
            ExpectedResult = new ErrCodeInfo("TEST.99995x", "Ict.Common.Testing.TTestCommon", "ERR_TESTONLY5",
                String.Empty, String.Empty,
                "test only", String.Empty, ErrCodeCategory.Error, String.Empty);
            VerificationProblemListing = EvaluateErrCodeInfoResults(ExpectedResult, TestResult);
            Assert.IsEmpty(VerificationProblemListing, "GetErrorInfo: " + Testname);

            ErrorCodeInventory.RegisteredTypes.Add(new TErrorCodesTest().GetType());
            ErrorCodes.GetErrorInfo("TEST.88888V");

            // The following test is needed to test-cover the if-branch in Method ErrorCodes.GetErrorInfo that performs a recursive Method call!
            TErrorCodesTest2.TestAddingOnTheFlyInUnregisteredClass();

            #endregion


            #region This Test Case's Private Methods ('self-test')

            Testname = "EvaluateErrCodeInfoResults (identical values)";
            TestResult = new ErrCodeInfo("TEST.99995x", "Ict.Common.Testing.TTestCommon", "ERR_TESTONLY5",
                "ShortDesc1", "FullDesc1",
                "ErrorMessageText1", "ErrorMessageTitleText1", ErrCodeCategory.Error, "HelpID1");
            ExpectedResult = new ErrCodeInfo("TEST.99995x", "Ict.Common.Testing.TTestCommon", "ERR_TESTONLY5",
                "ShortDesc1", "FullDesc1",
                "ErrorMessageText1", "ErrorMessageTitleText1", ErrCodeCategory.Error, "HelpID1");
            VerificationProblemListing = EvaluateErrCodeInfoResults(ExpectedResult, TestResult);
            Assert.IsEmpty(VerificationProblemListing, "EvaluateErrCodeInfoResults: " + Testname);

            Testname = "EvaluateErrCodeInfoResults (non-identical values)";
            TestResult = new ErrCodeInfo("TEST.99995x", "Ict.Common.Testing.TTestCommon", "ERR_TESTONLY5",
                "ShortDesc1", "FullDesc1",
                "ErrorMessageText1", "ErrorMessageTitleText1", ErrCodeCategory.Error, "HelpID1");
            ExpectedResult = new ErrCodeInfo("TEST.99995x2", "Ict.Common.Testing.TTestCommon2", "ERR_TESTONLY52",
                "ShortDesc2", "FullDesc2",
                "ErrorMessageText2", "ErrorMessageTitleText2", ErrCodeCategory.NonCriticalError, "HelpID2");
            VerificationProblemListing = EvaluateErrCodeInfoResults(ExpectedResult, TestResult);

            Assert.IsTrue(String.Compare(
                    VerificationProblemListing,
                    "AExpectedResult.Category != ATestResult.Category ('NonCriticalError' vs. 'Error'); " +
                    "AExpectedResult.ErrorCode != ATestResult.ErrorCode ('TEST.99995x2' vs. 'TEST.99995x'); " +
                    "AExpectedResult.ErrorCodeConstantClass != ATestResult.ErrorCodeConstantClass ('Ict.Common.Testing.TTestCommon2' vs. 'Ict.Common.Testing.TTestCommon'); "
                    +
                    "AExpectedResult.ErrorCodeConstantName != ATestResult.ErrorCodeConstantName ('ERR_TESTONLY52' vs. 'ERR_TESTONLY5'); " +
                    "AExpectedResult.ErrorMessageText != ATestResult.ErrorMessageText ('ErrorMessageText2' vs. 'ErrorMessageText1'); " +
                    "AExpectedResult.ErrorMessageTitle != ATestResult.ErrorMessageTitle ('ErrorMessageTitleText2' vs. 'ErrorMessageTitleText1'); " +
                    "AExpectedResult.ShortDescription != ATestResult.ShortDescription ('ShortDesc2' vs. 'ShortDesc1'); " +
                    "AExpectedResult.FullDescription != ATestResult.FullDescription ('FullDesc2' vs. 'FullDesc1'); " +
                    "AExpectedResult.HelpID != ATestResult.HelpID ('HelpID2' vs. 'HelpID1')") == 0,
                "EvaluateErrCodeInfoResults: " + Testname);

            #endregion
        }

        /// <summary>
        /// Tests for the uniquness of Error Codes across OpenPetra.
        /// </summary>
        /// <remarks>Will throw an Ict.Common.EDuplicateErrorCodeException in case a duplicate Error Code exists!!!</remarks>
        [Test]
        public void TestErrorCodesUniqueAcrossOpenPetra()
        {
            // The following calls each add Error Codes to the central Error Code repository (held in memory).
            // The ErrorCodeInventory.BuildErrorCodeInventory Method checks that each added Error Code isn't
            // already held in it and throws an Ict.Common.EDuplicateErrorCodeException if an Error Code that
            // is to be added already exists.
            // Thus the uniqueness of all Error codes across OpenPetra can be checked by making a call to
            // the ErrorCodeInventory.BuildErrorCodeInventory Method with all Classes that hold Error Codes.
            // Note: We mustn't add run ErrorCodeInventory.BuildErrorCodeInventory for Ict.Common.CommonErrorCodes()
            // as this already happened through the 'TestErrorCodes' Unit Test!
            ErrorCodeInventory.BuildErrorCodeInventory(new Ict.Petra.Shared.PetraErrorCodes().GetType());
            ErrorCodeInventory.BuildErrorCodeInventory(new Ict.Common.Verification.TStringChecks().GetType());
        }

        /// <summary>
        /// Tests the Catalog.GetPluralString Method to ensure that it always returns either the correct
        /// singluar or plural string - irrespective of the language resource file being loaded, or not.
        /// </summary>
        [Test]
        public void TestGettext_GetPluralString_ExpectSingularPluralToWork()
        {
            const string SINGULARTEXT = "*** DO NOT TRANSLATE! ***   test   *** DO NOT TRANSLATE! ***";
            const string PLURALTEXT = "*** DO NOT TRANSLATE! ***   testS   *** DO NOT TRANSLATE! ***";

            string GettextResult;

            //
            // With non-initialised Catalog!
            //

            // Zero number
            GettextResult = Catalog.GetPluralString(SINGULARTEXT, PLURALTEXT, 0, false);
            Assert.IsTrue(String.Compare(GettextResult, SINGULARTEXT) == 0);

            GettextResult = Catalog.GetPluralString(SINGULARTEXT, PLURALTEXT, 0, true);
            Assert.IsTrue(String.Compare(GettextResult, PLURALTEXT) == 0);

            // Positive numbers
            GettextResult = Catalog.GetPluralString(SINGULARTEXT, PLURALTEXT, 1, false);
            Assert.IsTrue(String.Compare(GettextResult, SINGULARTEXT) == 0);

            GettextResult = Catalog.GetPluralString(SINGULARTEXT, PLURALTEXT, 1, true);
            Assert.IsTrue(String.Compare(GettextResult, SINGULARTEXT) == 0);

            GettextResult = Catalog.GetPluralString(SINGULARTEXT, PLURALTEXT, 2, false);
            Assert.IsTrue(String.Compare(GettextResult, PLURALTEXT) == 0);

            GettextResult = Catalog.GetPluralString(SINGULARTEXT, PLURALTEXT, 2, true);
            Assert.IsTrue(String.Compare(GettextResult, PLURALTEXT) == 0);

            // Negative numbers
            GettextResult = Catalog.GetPluralString(SINGULARTEXT, PLURALTEXT, -1, false);
            Assert.IsTrue(String.Compare(GettextResult, SINGULARTEXT) == 0);

            GettextResult = Catalog.GetPluralString(SINGULARTEXT, PLURALTEXT, -1, true);
            Assert.IsTrue(String.Compare(GettextResult, SINGULARTEXT) == 0);

            GettextResult = Catalog.GetPluralString(SINGULARTEXT, PLURALTEXT, -2, false);
            Assert.IsTrue(String.Compare(GettextResult, PLURALTEXT) == 0);

            GettextResult = Catalog.GetPluralString(SINGULARTEXT, PLURALTEXT, -2, true);
            Assert.IsTrue(String.Compare(GettextResult, PLURALTEXT) == 0);


            //
            // And now the same with a Catalog that is initialised
            // (That will be different to the tests above ONLY IF the resource file for "de-DE" exists: \bin\de-DE\OpenPetra.resources.dll!)
            //
            Catalog.Init("de-DE", "de-DE");

            if (Catalog.ResourceManagerInitialisedWithGivenLanguage)
            {
                // Zero number
                GettextResult = Catalog.GetPluralString(SINGULARTEXT, PLURALTEXT, 0, false);
                Assert.IsTrue(String.Compare(GettextResult, SINGULARTEXT) == 0);

                GettextResult = Catalog.GetPluralString(SINGULARTEXT, PLURALTEXT, 0, true);
                Assert.IsTrue(String.Compare(GettextResult, PLURALTEXT) == 0);

                // Positive numbers
                GettextResult = Catalog.GetPluralString(SINGULARTEXT, PLURALTEXT, 1, false);
                Assert.IsTrue(String.Compare(GettextResult, SINGULARTEXT) == 0);

                GettextResult = Catalog.GetPluralString(SINGULARTEXT, PLURALTEXT, 1, true);
                Assert.IsTrue(String.Compare(GettextResult, SINGULARTEXT) == 0);

                GettextResult = Catalog.GetPluralString(SINGULARTEXT, PLURALTEXT, 2, false);
                Assert.IsTrue(String.Compare(GettextResult, PLURALTEXT) == 0);

                GettextResult = Catalog.GetPluralString(SINGULARTEXT, PLURALTEXT, 2, true);
                Assert.IsTrue(String.Compare(GettextResult, PLURALTEXT) == 0);

                // Negative numbers
                GettextResult = Catalog.GetPluralString(SINGULARTEXT, PLURALTEXT, -1, false);
                Assert.IsTrue(String.Compare(GettextResult, SINGULARTEXT) == 0);

                GettextResult = Catalog.GetPluralString(SINGULARTEXT, PLURALTEXT, -1, true);
                Assert.IsTrue(String.Compare(GettextResult, SINGULARTEXT) == 0);

                GettextResult = Catalog.GetPluralString(SINGULARTEXT, PLURALTEXT, -2, false);
                Assert.IsTrue(String.Compare(GettextResult, PLURALTEXT) == 0);

                GettextResult = Catalog.GetPluralString(SINGULARTEXT, PLURALTEXT, -2, true);
                Assert.IsTrue(String.Compare(GettextResult, PLURALTEXT) == 0);
            }
        }
    }

    /// <summary>
    /// Needed for testing error codes.
    /// </summary>
    public class TErrorCodesTest
    {
        /// <summary>Test-only Error Code for TestErrorCodes Method.</summary>
        [ErrCodeAttribute("Invalid date.",
             ErrorMessageText = "Invalid date entered.",
             FullDescription = "The date entered is not a valid date.")]
        public const String ERR_TESTONLY_OTHERCLASS1 = "TEST.88888V";
    }

    /// <summary>
    /// Needed for testing error codes.
    /// </summary>
    public class TErrorCodesTest2
    {
        /// <summary>Test-only Error Code for TestErrorCodes Method.</summary>
        [ErrCodeAttribute("Invalid date.",
             ErrorMessageText = "Invalid date entered.",
             FullDescription = "The date entered is not a valid date.")]
        public const String ERR_TESTONLY_OTHERCLASS1 = "TEST.77777V";

        /// <summary>
        /// Test for adding on the fly in Unregistered Class.
        /// </summary>
        public static void TestAddingOnTheFlyInUnregisteredClass()
        {
            ErrorCodes.GetErrorInfo("TEST.77777V");
        }
    }
}
