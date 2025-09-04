﻿//
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
using System.Data;
//using System.Data.Odbc;
//using System.Text.RegularExpressions;
using System.Collections.Generic;
using Ict.Common;
using Ict.Common.DB;
using Ict.Common.Exceptions;
//using Ict.Common.Verification;
using Ict.Petra.Shared.MFinance.Account.Data;
using Ict.Petra.Server.MFinance.Account.Data.Access;
//using Ict.Petra.Shared.MCommon.Data;
//using Ict.Petra.Server.MCommon.Data.Access;

namespace Ict.Petra.Server.MFinance.Common
{
    /// <summary>
    /// This object handles the table AccountHierarchyDetailInfo and provides some standard
    /// procedures.
    /// </summary>
    public class TGetAccountHierarchyDetailInfo
    {
        private const string STANDARD = "STANDARD";
        AAccountHierarchyDetailTable FHierarchyDetailTable;
        AAccountTable FAccountTable;


        /// <summary>
        /// ...
        /// </summary>
        /// <param name="ALedgerNumber"></param>
        public TGetAccountHierarchyDetailInfo(Int32 ALedgerNumber)
        {
            #region Validate Arguments

            if (ALedgerNumber <= 0)
            {
                throw new EFinanceSystemInvalidLedgerNumberException(String.Format(Catalog.GetString(
                            "Function:{0} - The Ledger number must be greater than 0!"),
                        Utilities.GetMethodName(true)), ALedgerNumber);
            }

            #endregion Validate Arguments

            TDBTransaction Transaction = new TDBTransaction();
            TDataBase db = DBAccess.Connect("TGetAccountHierarchyDetailInfo");

            try
            {
                db.ReadTransaction(
                    ref Transaction,
                    delegate
                    {
                        FHierarchyDetailTable = AAccountHierarchyDetailAccess.LoadViaALedger(ALedgerNumber, Transaction);
                        FAccountTable = AAccountAccess.LoadViaALedger(ALedgerNumber, Transaction);
                        FAccountTable.DefaultView.Sort = "a_account_code_c";
                    });
            }
            catch (Exception ex)
            {
                TLogging.LogException(ex, Utilities.GetMethodSignature());
                throw;
            }

            db.CloseDBConnection();
        }

        /// <summary>
        /// The data were read only one time (in the constructor)
        /// and the results are put into a list for a more specific use.
        /// </summary>
        /// <param name="AAccountCode"></param>
        /// <param name="OnlyPosting">Don't show me all the summary accounts</param>
        /// <param name="AChildLevel">-1 = all levels</param>
        /// <returns></returns>
        public List <String>GetChildren(string AAccountCode, Boolean OnlyPosting = false, int AChildLevel = -1)
        {
            List <String>help = new List <String>();
            GetChildrenIntern(help, AAccountCode, OnlyPosting, --AChildLevel);
            return help;
        }

        private void GetChildrenIntern(IList <String>help, string AAccountCode, Boolean OnlyPosting, int AChildLevel)
        {
            if (FHierarchyDetailTable.Rows.Count > 0)
            {
                FHierarchyDetailTable.DefaultView.Sort =
                    AAccountHierarchyDetailTable.GetReportOrderDBName() + ", " +
                    AAccountHierarchyDetailTable.GetReportingAccountCodeDBName();

                foreach (DataRowView rv in FHierarchyDetailTable.DefaultView)
                {
                    AAccountHierarchyDetailRow Row = (AAccountHierarchyDetailRow)rv.Row;

                    if (Row.AccountCodeToReportTo.Equals(AAccountCode))
                    {
                        if (Row.AccountHierarchyCode.Equals(STANDARD))
                        {
                            Boolean includeThis = true;

                            if (OnlyPosting)
                            {
                                Int32 pos = FAccountTable.DefaultView.Find(Row.ReportingAccountCode);

                                if (pos >= 0)
                                {
                                    AAccountRow account = (AAccountRow)FAccountTable.DefaultView[pos].Row;

                                    if (!account.PostingStatus)
                                    {
                                        includeThis = false;
                                    }
                                }
                            }

                            if (includeThis)
                            {
                                help.Add(Row.ReportingAccountCode);
                            }

                            if (AChildLevel != 0)
                            {
                                GetChildrenIntern(help, Row.ReportingAccountCode, OnlyPosting, --AChildLevel);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// If you only want to know, that the account code has no children, you can avoid the
        /// handling of the list.
        /// </summary>
        /// <param name="AAccountCode"></param>
        /// <returns></returns>
        public bool HasNoChildren(string AAccountCode)
        {
            if (FHierarchyDetailTable.Rows.Count > 0)
            {
                for (int i = 0; i < FHierarchyDetailTable.Rows.Count; ++i)
                {
                    AAccountHierarchyDetailRow Row = (AAccountHierarchyDetailRow)FHierarchyDetailTable.Rows[i];

                    if (Row.AccountCodeToReportTo.Equals(AAccountCode))
                    {
                        if (Row.AccountHierarchyCode.Equals(STANDARD))
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Gets the parent account of an inserted account ...
        /// </summary>
        /// <param name="AAccountCode"></param>
        /// <returns></returns>
        public string GetParentAccount(string AAccountCode)
        {
            if (FHierarchyDetailTable.Rows.Count > 0)
            {
                for (int i = 0; i < FHierarchyDetailTable.Rows.Count; ++i)
                {
                    AAccountHierarchyDetailRow Row = (AAccountHierarchyDetailRow)FHierarchyDetailTable.Rows[i];

                    if (Row.ReportingAccountCode.Equals(AAccountCode))
                    {
                        if (Row.AccountHierarchyCode.Equals(STANDARD))
                        {
                            return Row.AccountCodeToReportTo;
                        }
                    }
                }
            }

            return String.Empty;
        }
    }

    /// <summary>
    /// A Enum-list of some special account codes ...
    /// https://sourceforge.net/apps/mediawiki/openpetraorg/index.php?title=Data_Conversion_from_Petra_to_Openpetra
    /// </summary>
    public enum TAccountPropertyEnum
    {
        /// <summary>
        ///
        /// </summary>
        GIFT_HEADING,               // GIFT

        /// <summary>
        ///
        /// </summary>
        INTER_LEDGER_HEADING,       // ILT

        /// <summary>
        ///
        /// </summary>
        BANK_HEADING,               // CASH

        /// <summary>
        ///
        /// </summary>
        BALANCE_SHEET_HEADING,      // BAL

        //PROFIT_AND_LOSS_HEADING,    // PL
        //INCOME_HEADING,             // INC
        //EXPENSE_HEADING,            // EXP

        /// <summary>
        ///
        /// </summary>
        DEBTOR_HEADING,             // DRS


        /// <summary>
        ///
        /// </summary>
        CREDITOR_HEADING,           // CRS

        /// <summary>
        ///
        /// </summary>
        TOTAL_ASSET_HEADING,        // ASSETS

        /// <summary>
        ///
        /// </summary>
        TOTAL_LIABILITY_HEADING,    // LIABS

        /// <summary>
        ///
        /// </summary>
        EQUITY_HEADING,             // RET EARN


        /// <summary>
        ///
        /// </summary>
        EARNINGS_BF_ACCT,           // 9700

        /// <summary>
        ///
        /// </summary>
        DIRECT_XFER_ACCT,           // 5501

        /// <summary>
        ///
        /// </summary>
        ICH_SETTLEMENT_ACCT,        // 5601

        /// <summary>
        ///
        /// </summary>
        ICH_ACCT,                   // 8500

        /// <summary>
        ///
        /// </summary>
        INTERNAL_XFER_ACCT,         // 9800

        /// <summary>
        ///
        /// </summary>
        ADMIN_FEE_INCOME_ACCT,      // 3400

        /// <summary>
        ///
        /// </summary>
        ADMIN_FEE_EXPENSE_ACCT,     // 4900

        /// <summary>
        ///
        /// </summary>
        FUND_TRANSFER_INCOME_ACCT,  // 3300

        /// <summary>
        ///
        /// </summary>
        FUND_TRANSFER_EXPENSE_ACCT, // 4800
    }

    /// <summary>
    /// A handler to the special accounts in TAccountPropertyEnum
    /// </summary>
    public class THandleAccountPropertyInfo
    {
        AAccountPropertyTable FPropertyCodeTable;
        /// <summary>
        /// The constructor needs a ledgerinfo (for the ledger number)
        /// </summary>
        /// <param name="ALedgerNumber"></param>
        public THandleAccountPropertyInfo(Int32 ALedgerNumber)
        {
            #region Validate Arguments

            if (ALedgerNumber <= 0)
            {
                throw new EFinanceSystemInvalidLedgerNumberException(String.Format(Catalog.GetString(
                            "Function:{0} - The Ledger number must be greater than 0!"),
                        Utilities.GetMethodName(true)), ALedgerNumber);
            }

            #endregion Validate Arguments

            TDBTransaction Transaction = new TDBTransaction();
            TDataBase db = DBAccess.Connect("THandleAccountPropertyInfo");

            try
            {
                db.ReadTransaction(
                    ref Transaction,
                    delegate
                    {
                        FPropertyCodeTable = AAccountPropertyAccess.LoadViaALedger(ALedgerNumber, Transaction);
                    });
            }
            catch (Exception ex)
            {
                TLogging.LogException(ex, Utilities.GetMethodSignature());
                throw;
            }

            db.CloseDBConnection();
        }

        /// <summary>
        /// Get access on a special account...
        /// </summary>
        /// <param name="AEnum"></param>
        /// <returns></returns>
        public string GetAccountCode(TAccountPropertyEnum AEnum)
        {
            foreach (AAccountPropertyRow row in FPropertyCodeTable.Rows)
            {
                if (row.PropertyCode.Equals("Is_Special_Account"))
                {
                    if (row.PropertyValue == AEnum.ToString())
                    {
                        return row.AccountCode;
                    }
                }
            }

            // if special account flag is not set in the property table, then use config parameters and hardcoded defaults
            switch (AEnum)
            {
                case TAccountPropertyEnum.GIFT_HEADING:
                    return TAppSettingsManager.GetValue(AEnum.ToString(), "GIFT");

                case TAccountPropertyEnum.INTER_LEDGER_HEADING:
                    return TAppSettingsManager.GetValue(AEnum.ToString(), "ILT");

                case TAccountPropertyEnum.BANK_HEADING:
                    return TAppSettingsManager.GetValue(AEnum.ToString(), "CASH");

                case TAccountPropertyEnum.BALANCE_SHEET_HEADING:
                    return TAppSettingsManager.GetValue(AEnum.ToString(), "BAL");

                case TAccountPropertyEnum.DEBTOR_HEADING:
                    return TAppSettingsManager.GetValue(AEnum.ToString(), "DRS");

                case TAccountPropertyEnum.CREDITOR_HEADING:
                    return TAppSettingsManager.GetValue(AEnum.ToString(), "CRS");

                case TAccountPropertyEnum.TOTAL_ASSET_HEADING:
                    return TAppSettingsManager.GetValue(AEnum.ToString(), "ASSETS");

                case TAccountPropertyEnum.TOTAL_LIABILITY_HEADING:
                    return TAppSettingsManager.GetValue(AEnum.ToString(), "LIABS");

                case TAccountPropertyEnum.EQUITY_HEADING:
                    return TAppSettingsManager.GetValue(AEnum.ToString(), "RET EARN");

                case TAccountPropertyEnum.EARNINGS_BF_ACCT:
                    return TAppSettingsManager.GetValue(AEnum.ToString(), "9700");

                case TAccountPropertyEnum.DIRECT_XFER_ACCT:
                    return TAppSettingsManager.GetValue(AEnum.ToString(), "5501");

                case TAccountPropertyEnum.ICH_SETTLEMENT_ACCT:
                    return TAppSettingsManager.GetValue(AEnum.ToString(), "5601");

                case TAccountPropertyEnum.ICH_ACCT:
                    return TAppSettingsManager.GetValue(AEnum.ToString(), "8500");

                case TAccountPropertyEnum.INTERNAL_XFER_ACCT:
                    return TAppSettingsManager.GetValue(AEnum.ToString(), "9800");

                case TAccountPropertyEnum.ADMIN_FEE_INCOME_ACCT:
                    return TAppSettingsManager.GetValue(AEnum.ToString(), "3400");

                case TAccountPropertyEnum.ADMIN_FEE_EXPENSE_ACCT:
                    return TAppSettingsManager.GetValue(AEnum.ToString(), "4900");

                case TAccountPropertyEnum.FUND_TRANSFER_INCOME_ACCT:
                    return TAppSettingsManager.GetValue(AEnum.ToString(), "3300");

                case TAccountPropertyEnum.FUND_TRANSFER_EXPENSE_ACCT:
                    return TAppSettingsManager.GetValue(AEnum.ToString(), "4800");
            }

            TLogging.Log("THandleAccountPropertyInfo: cannot find Account code for " + AEnum.ToString());
            return String.Empty;
        }

        /// <summary>
        /// </summary>
        /// <param name="APropertyCode"></param>
        /// <returns></returns>
        public string GetAccountCode(string APropertyCode)
        {
            foreach (AAccountPropertyRow row in FPropertyCodeTable.Rows)
            {
                if (row.PropertyCode == APropertyCode)
                {
                    return row.PropertyValue;
                }
            }

            return String.Empty;
        }
    }


    /// <summary>
    /// TAccountInfo uses the LedgerNumber.
    /// All Accounts are load in both contructors. You can define an initial account code in the
    /// second constructor or you can set the value later (or change) by using SetAccountRowTo.
    /// Then you can read the values for the selected Account.
    /// </summary>
    public class TAccountInfo
    {
        AAccountTable FAccountTable;
        AAccountRow FAccountRow = null;
        Int32 FLedgerNumber;
        TDBTransaction FTransaction;
        THandleAccountPropertyInfo FAccountPropertyHandler = null;

        int FRowIdx;

        /// <summary>
        /// This mininmal constructor defines the result collection for the error messages and
        /// Ledger Info to select the ledger ...
        /// </summary>
        public TAccountInfo(Int32 ALedgerNumber, TDBTransaction ATransaction = null)
        {
            FLedgerNumber = ALedgerNumber;
            FTransaction = ATransaction;
            LoadData();
        }

        /// <summary>
        /// The Constructor defines a first value of a specific accounting code too.
        /// </summary>
        public TAccountInfo(Int32 ALedgerNumber, String AAccountCode, TDBTransaction ATransaction = null)
        {
            FLedgerNumber = ALedgerNumber;
            FTransaction = ATransaction;
            LoadData();
            AccountCode = AAccountCode;
        }

        private void LoadData()
        {
            FAccountRow = null;

            if (FTransaction != null)
            {
                FAccountTable = AAccountAccess.LoadViaALedger(FLedgerNumber, FTransaction);
            }
            else
            {
                TDBTransaction Transaction = new TDBTransaction();
                TDataBase db = DBAccess.Connect("TAccountInfo.LoadData");

                db.ReadTransaction(
                    ref Transaction,
                    delegate
                    {
                        FAccountTable = AAccountAccess.LoadViaALedger(FLedgerNumber, Transaction);
                    });

                db.CloseDBConnection();
            }

            #region Validate Data

            if ((FAccountTable == null) || (FAccountTable.Count == 0))
            {
                throw new EFinanceSystemDataTableReturnedNoDataException(String.Format(Catalog.GetString(
                            "Function:{0} - Account data for Ledger {1} does not exist or could not be accessed!"),
                        Utilities.GetMethodName(true),
                        FLedgerNumber));
            }

            #endregion Validate Data
        }

        /// <summary>
        /// TAccountPropertyEnum defines a set of special accounts and here one of them
        /// can be selected. ..
        /// </summary>
        /// <param name="AENum"></param>
        public void SetSpecialAccountCode(TAccountPropertyEnum AENum)
        {
            FAccountRow = null;

            if (FAccountPropertyHandler == null)
            {
                FAccountPropertyHandler = new THandleAccountPropertyInfo(FLedgerNumber);
            }

            string account = FAccountPropertyHandler.GetAccountCode(AENum);

            if (!account.Equals(string.Empty))
            {
                AccountCode = account;
            }
        }

        /// <summary>
        /// The Account code can be read and the result is the account code of the row
        /// which was selected before. <br />
        /// The Account can be written and this will change the selected row without any
        /// database request. The result may be invalid.
        ///
        /// After this is set, the FRowIdx is invalid.
        /// </summary>
        public string AccountCode
        {
            get
            {
                if (FAccountRow == null)
                {
                    return String.Empty;
                }
                else
                {
                    return FAccountRow.AccountCode;
                }
            }
            set
            {
                if ((FAccountRow == null) || (FAccountRow.AccountCode != value))
                {
                    Reset();

                    if (value != "")
                    {
                        FAccountRow = (AAccountRow)FAccountTable.Rows.Find(new Object[] { FLedgerNumber, value });
                    }
                }
            }
        }

        /// <summary>
        /// If there's no current row, this will raise an exception.
        /// </summary>
        public string AccountType
        {
            get
            {
                return FAccountRow.AccountType;
            }
        }

        /// <summary>
        /// Reset the row list
        /// </summary>
        public void Reset()
        {
            FRowIdx = -1;
            FAccountRow = null;
        }

        /// <summary>
        /// Move to the next row ...
        /// </summary>
        /// <returns></returns>
        public bool MoveNext()
        {
            ++FRowIdx;

            if (FRowIdx < FAccountTable.Rows.Count)
            {
                FAccountRow = (AAccountRow)FAccountTable[FRowIdx];
                return true;
            }
            else
            {
                FAccountRow = null;
                return false;
            }
        }

        /// <summary>
        /// Informs that a row selection is valid and the row properties will not produce
        /// an exception.
        /// </summary>
        public bool IsValid
        {
            get
            {
                return !(FAccountRow == null);
            }
        }

        /// <summary>
        /// If there's no current row, this will raise an exception.
        /// </summary>
        public bool ForeignCurrencyFlag
        {
            get
            {
                return FAccountRow.ForeignCurrencyFlag;
            }
        }

        /// <summary>
        /// If there's no current row, this will raise an exception.
        /// </summary>
        public string ForeignCurrencyCode
        {
            get
            {
                return FAccountRow.ForeignCurrencyCode;
            }
        }

        /// <summary>
        /// If there's no current row, this will raise an exception.
        /// </summary>
        public bool PostingStatus
        {
            get
            {
                return FAccountRow.PostingStatus;
            }
        }

        /// <summary>
        /// If there's no current row, this will raise an exception.
        /// </summary>
        public bool DebitCreditIndicator
        {
            get
            {
                return FAccountRow.DebitCreditIndicator;
            }
        }
    }

    /// <summary>
    /// Gets the specific date informations of an accounting intervall. This routine is either used by
    /// GL.PeriodEnd.Month and GL.Revaluation but in different senses. On time the dataset holds exact
    /// one row (Contructor with two parameters) and on time it holds a set of rows (Constructor with
    /// one parameter.
    /// </summary>
    public class TAccountPeriodInfo
    {
        private int FLedgerNumber;
        private AAccountingPeriodTable FPeriodTable = null;
        private AAccountingPeriodRow FPeriodRow = null;
        private TDataBase FDataBase = null;


        /// <summary>
        ///
        /// </summary>
        protected void LoadTableData(int ALedgerNumber)
        {
            FLedgerNumber = ALedgerNumber;
            LoadData();
        }

        /// <summary>
        /// Constructor needs a valid ledger number.
        /// </summary>
        /// <param name="ALedgerNumber">Ledger number</param>
        /// <param name="ADataBase"></param>
        public TAccountPeriodInfo(int ALedgerNumber, TDataBase ADataBase = null)
        {
            FLedgerNumber = ALedgerNumber;
            FDataBase = ADataBase;
            LoadData();
        }

        /// <summary>
        /// Constructor to adress a record by its primary key
        /// </summary>
        /// <param name="ALedgerNumber">the ledger number</param>
        /// <param name="ACurrentPeriod">the current accounting period</param>
        /// <param name="ADataBase"></param>
        public TAccountPeriodInfo(int ALedgerNumber, int ACurrentPeriod, TDataBase ADataBase = null)
        {
            FLedgerNumber = ALedgerNumber;
            FDataBase = ADataBase;
            LoadData();
            AccountingPeriodNumber = ACurrentPeriod;
        }

        private void LoadData()
        {
            TDBTransaction Transaction = new TDBTransaction();
            TDataBase db = DBAccess.Connect("TAccountPeriodInfo.LoadData", FDataBase);

            try
            {
                db.ReadTransaction(
                    ref Transaction,
                    delegate
                    {
                        FPeriodTable = AAccountingPeriodAccess.LoadViaALedger(FLedgerNumber, Transaction);

                        #region Validate Data

                        if ((FPeriodTable == null) || (FPeriodTable.Count == 0))
                        {
                            throw new EFinanceSystemDataTableReturnedNoDataException(String.Format(Catalog.GetString(
                                        "Function:{0} - Accounting Period data for Ledger {1} does not exist or could not be accessed!"),
                                    Utilities.GetMethodName(true),
                                    FLedgerNumber));
                        }

                        #endregion Validate Data
                    });
            }
            catch (Exception ex)
            {
                TLogging.LogException(ex, Utilities.GetMethodSignature());
                throw;
            }

            if (FDataBase == null)
            {
                db.CloseDBConnection();
            }
        }

        /// <summary>
        ///
        /// </summary>
        public int AccountingPeriodNumber
        {
            set
            {
                FPeriodRow = null;
                AAccountingPeriodRow periodRowH;

                for (int i = 0; i < FPeriodTable.Rows.Count; ++i)
                {
                    periodRowH = FPeriodTable[i];

                    if (periodRowH.AccountingPeriodNumber == value)
                    {
                        FPeriodRow = periodRowH;
                    }
                }
            }
        }


        /// <summary>
        ///
        /// </summary>
        public DateTime PeriodEndDate
        {
            get
            {
                return FPeriodRow.PeriodEndDate;
            }
        }


        /// <summary>
        ///
        /// </summary>
        public DateTime PeriodStartDate
        {
            get
            {
                return FPeriodRow.PeriodStartDate;
            }
        }


        /// <summary>
        /// Returns the number of accounting periods in the table
        /// </summary>
        public int Rows
        {
            get
            {
                return FPeriodTable.Rows.Count;
            }
        }
    }


    /// <summary>
    /// Different Account Types ...
    /// </summary>
    public enum TAccountTypeEnum
    {
        /// <summary>
        ///
        /// </summary>
        Income,

        /// <summary>
        ///
        /// </summary>
        Expense,

        /// <summary>
        ///
        /// </summary>
        Asset,

        /// <summary>
        ///
        /// </summary>
        Equity,

        /// <summary>
        ///
        /// </summary>
        Liability
    }
}
