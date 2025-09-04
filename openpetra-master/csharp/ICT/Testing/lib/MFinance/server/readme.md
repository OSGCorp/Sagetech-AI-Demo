# C# Finance Server Subproject Of Petra

The Petra is a C# program that provides administrative and financial management capabilities for non-profit organizations. The program handles accounting, contact management, and sponsorship tracking while supporting international operations. This sub-project implements the core financial server components along with API interfaces for client applications. This provides these capabilities to the Petra program:

- General Ledger (GL) management with period-end processing
- Accounts Payable (AP) operations with multi-currency support
- Gift processing with receipt generation and recurring gift management
- Bank statement import and reconciliation
- Financial reporting with customizable parameters
  - Trial Balance, Balance Sheet, Income/Expense Statements
  - Account Detail reports with filtering capabilities
- International Clearing House (ICH) operations for cross-border transactions

## Identified Design Elements

1. Multi-Currency Support: Comprehensive handling of foreign currencies with exchange rate management, revaluation processes, and proper accounting entries
2. Modular Financial Components: Clear separation between GL, AP, Gift, and ICH modules with well-defined interfaces
3. Flexible Import/Export: Support for multiple banking formats (CSV, CAMT.052, CAMT.053, MT940) and financial data interchange
4. Robust Testing Framework: Extensive unit tests with fixtures for validating financial calculations and data integrity
5. Templated Reporting: HTML and PDF report generation with customizable parameters and formatting

## Overview
The architecture follows standard accounting principles with a strong focus on data integrity and auditability. The General Ledger serves as the foundation with specialized modules for different financial functions. The system supports complex workflows including month-end and year-end processes, gift batch management, and international fund transfers. The codebase emphasizes validation at multiple levels to ensure financial data remains accurate and balanced. The reporting system provides both standard financial statements and specialized reports for non-profit operations, with support for cost center accounting across international operations.

## Business Functions

### Bank Import
- `BankImport/BankStatement2.csv` : CSV file containing bank statement data for a German association account with transaction details.
- `BankImport/camt_testfile.53.xml` : Test XML file for CAMT.053 bank statement format used in bank import testing.
- `BankImport/camt_testfile.52.xml` : Sample CAMT.052 XML test file for bank statement import testing in OpenPetra's finance module.
- `BankImport/GiftBatch.csv` : CSV test data file containing gift batch information for bank import testing.
- `BankImport/MatchingGiftsFromBankstatement.test.cs` : Test file for bank import gift matching functionality in OpenPetra's finance module.
- `BankImport/BankStatement.csv` : CSV file containing bank statement data with transaction dates, account numbers, amounts, and currency.

### Financial Reporting
- `Reporting/TestData/TrialBalance.Results.Expected.html` : HTML template for a Trial Balance report showing financial account balances with debits, credits, and end balances.
- `Reporting/TestData/IncExpStmt.xml` : XML configuration file for Income Expense Statement report parameters in OpenPetra's testing framework.
- `Reporting/TestData/AccountDetail.Parameters.Expected.xml` : XML configuration file defining column formatting and parameters for the Account Detail financial report in OpenPetra.
- `Reporting/TestData/TrialBalanceSelectedAccount.Results.Expected.html` : HTML template for a Trial Balance report showing selected accounts with their debit, credit, and balance values.
- `Reporting/TestData/BalanceSheetDetail.xml` : XML configuration file defining parameters for a Balance Sheet financial report in OpenPetra.
- `Reporting/TestData/AccountDetailSelectedAccount.Results.Expected.html` : HTML template for OpenPetra's Account Detail financial report showing transaction details for selected accounts.
- `Reporting/TestData/AccountDetail.Results.Expected.html` : HTML template for OpenPetra's Account Detail financial report showing transaction details for multiple accounts.
- `Reporting/IncExpStmt.test.cs` : Test suite for Income and Expense Statement reporting functionality in OpenPetra's financial module.
- `Reporting/TrialBalance.test.cs` : Test suite for the Trial Balance report in OpenPetra's finance module, verifying correct financial data reporting.
- `Reporting/BalanceSheet.test.cs` : Test fixture for validating the Balance Sheet report functionality in OpenPetra's financial reporting system.
- `Reporting/AccountDetail.test.cs` : Test suite for the Account Detail financial report in OpenPetra's MFinance module.

### Accounts Payable
- `AP/Test.AP.cs` : Test suite for Accounts Payable (AP) functionality in OpenPetra, including document posting, payment, and reversal operations.

### Ledger Management
- `CrossLedger/AvailableLedgers.test.cs` : Test class for validating the GetAvailableLedgers method in OpenPetra's financial module.

### Gift Processing
- `Gift/SingleGiftReceipt.test.cs` : Test class for generating single gift receipts in OpenPetra's finance module.
- `Gift/SetMotivationGroupAndDetail.test.cs` : Test suite for validating the TGuiTools.GetMotivationGroupAndDetailForPartner method and related gift transaction functionality.
- `Gift/test.cs` : Test class for OpenPetra's gift management functionality with database and web connector tests.
- `Gift/RevertAdjustGiftBatch.test.cs` : Test class for gift batch adjustment functionality in OpenPetra's financial module, verifying proper accounting and receipt generation.
- `Gift/RecurringGiftBatch.test.cs` : Test class for recurring gift batch functionality in OpenPetra's finance module.
- `Gift/PostGiftBatch.test.cs` : Test class for gift batch processing functionality in OpenPetra's finance module.
- `Gift/AnnualReceipts.test.cs` : Test class for annual gift receipt generation in OpenPetra's finance module.

### General Ledger
- `GL/Test.GL.PeriodEnd.Year.cs` : Test class for GL year-end financial operations in OpenPetra accounting system.
- `GL/Test.GL.PeriodEnd.Month.cs` : NUnit tests for GL month-end period closing operations in OpenPetra's financial module.
- `GL/Test.GL.CommonAccountingTool.cs` : Test class for validating general ledger accounting functionality in OpenPetra's finance module
- `GL/Test.GL.Import.cs` : Test class for GL batch import functionality in OpenPetra's finance module
- `GL/PostGLBatch.test.cs` : Test suite for GL batch posting functionality in OpenPetra's financial module, validating argument handling and business logic.
- `GL/Test.GL.PeriodEnd.cs` : Implements unit tests for the GL period end operations in OpenPetra's financial module.
- `GL/test-csv/glbatch-import2.csv` : CSV test data file for GL batch import testing with sample financial transactions.
- `GL/test-csv/glbatch-import3.csv` : Test CSV file for GL batch import containing sample financial transaction data.
- `GL/test-csv/gltransactions-import.csv` : Sample CSV file for testing GL transaction imports in OpenPetra financial module.
- `GL/test-csv/glbatch-import.csv` : CSV test data file for GL batch import testing with sample financial transactions.
- `GL/Test.GL.Revaluation.cs` : Unit test for the GL revaluation functionality in OpenPetra's finance module.
- `GL/Test.GL.Revaluation.Utils.cs` : Test fixture for GL revaluation functionality in OpenPetra's financial module.
- `GL/Test.GL.CommonTools.cs` : Test suite for GL common tools in OpenPetra's financial module, validating ledger flags, currency handling, and account hierarchy functionality.
- `GL/test-sql/gl-test-feesreceivable-data.sql` : SQL data file containing fees receivable records for GL testing in OpenPetra.
- `GL/test-sql/gl-test-account-data.sql` : SQL data file containing account records for GL testing in OpenPetra's finance module.
- `GL/test-sql/gl-test-costcentre-data.sql` : SQL script that populates the a_cost_centre table with test data for GL cost center testing.
- `GL/test-sql/gl-test-batch-data.sql` : SQL test data file for GL batch testing in OpenPetra's finance module.
- `GL/test-sql/gl-test-currency-data.sql` : SQL dump file providing test currency data for the GL module in OpenPetra's finance system.
- `GL/test-sql/gl-test-feespayable-data.sql` : SQL data file containing fee payable records for GL testing in OpenPetra's finance module.
- `GL/test-sql/gl-test-year-end.sql` : SQL script for setting up test data for GL year-end processing in OpenPetra.

### International Clearing House
- `ICH/ICHHOSAFileReports.test.cs` : Test suite for ICH HOSA file generation and reporting functionality in OpenPetra's finance module.
- `ICH/PerformStewardshipCalculation.test.cs` : Test suite for stewardship calculation functionality in OpenPetra's ICH (International Clearing House) financial module.

### Budget Management
- `Budget/Test.Budget.cs` : Implements automated testing for budget management functionality in OpenPetra's finance module.

## Files
### AP/Test.AP.cs

TestAP implements a test suite for the Accounts Payable module in OpenPetra. It tests key AP operations including posting documents, making payments, and reversing transactions. The class contains three main test cases: SimpleDocument_ExpectPostingAndPayingWorking (tests basic AP document posting and payment), ForeignCurrencySupplier_ExpectDocumentPostingAndPayingWorking (tests AP operations with foreign currency suppliers), and ForeignCurrencySupplier_ExpectDocumentPostingPayingAndReversingWorking (tests complete AP workflow including reversal). The file includes helper methods for creating, posting, and paying AP documents, as well as methods for retrieving ledger information and verifying account balances. Key functionality includes transaction handling, currency exchange calculations, and verification of proper accounting entries across multiple accounts.

 **Code Landmarks**
- `Line 74`: Uses a struct (AAPInfos) to encapsulate related AP data rather than passing multiple parameters between methods
- `Line 103`: Test implements the AAA pattern (Arrange-Act-Assert) for clear test structure
- `Line 306`: Helper methods use delegate pattern with database transactions for clean resource management
- `Line 362`: Document counter ensures unique document codes across test runs
- `Line 453`: Foreign currency tests verify proper exchange rate calculations and forex gain/loss accounting
### BankImport/BankStatement.csv

This CSV file contains sample bank statement data for testing the bank import functionality in OpenPetra. Each line represents a transaction with six fields: transaction date, value date, account number, amount (using comma as decimal separator), currency code (EUR), and an empty field. The file includes three sample transactions, with one containing an invalid date (39.09.2010), likely for testing error handling.

 **Code Landmarks**
- `Line 3`: Contains an invalid date (39.09.2010) which appears to be intentionally included for testing error handling in the import process
### BankImport/BankStatement2.csv

This CSV file contains bank statement data for a German association account (Verein). It includes account holder information, account number, statement period (July 2019), and balance information. The main section contains transaction records with booking date, value date, transaction description, and amount in EUR. Transactions shown are SEPA transfers for sponsorships and donations with specific reference information.

 **Code Landmarks**
- `Line 5`: Uses German number format with comma as decimal separator (1.234,56)
- `Line 10-12`: Transaction descriptions contain structured payment information with SVWZ+ and EREF+ tags following German SEPA standards
### BankImport/GiftBatch.csv

This CSV file contains test data for gift batch imports in the OpenPetra financial system. It represents a single gift batch with multiple transactions. The file has a header row (B) with batch information including name, account code, date, and currency, followed by transaction rows (T) containing donor information, recipient details, amounts, and various gift attributes. This structured data is used for testing the bank import functionality in the MFinance module.

 **Code Landmarks**
- `Line 1`: Header row defines batch metadata with ID 'NUnit Gift Batch', account code, date and currency information
- `Line 2-5`: Transaction rows follow a consistent format with donor ID, name, recipient account, amount, and multiple gift classification attributes
### BankImport/MatchingGiftsFromBankstatement.test.cs

This test file verifies the functionality of matching gifts from bank statements in OpenPetra's finance module. It contains test cases for multiple gift scenarios, CAMT file imports, and MT940 file parsing. The file includes tests for importing gift batches, posting them, importing bank statements in various formats (CSV, CAMT.53, CAMT.52, MT940), and creating gift batches from imported statements. Key methods include TestMultipleGifts(), TestImportCAMT(), ImportCAMTFile53(), ImportCAMTFile52(), and ImportMT940File(). The tests verify proper data handling, date formatting, and transaction matching between bank statements and gifts.

 **Code Landmarks**
- `Line 63`: Test setup connects to a test server configuration and uses a configurable ledger number
- `Line 91`: Demonstrates date replacement in test data to ensure tests use current year
- `Line 143`: Shows gift batch reversal process to prevent training confusion in repeated test runs
- `Line 212`: Implementation of CAMT format parsing for bank statement imports
- `Line 282`: Swift MT940 format parsing implementation for bank statement imports
### BankImport/camt_testfile.52.xml

This XML file serves as a test fixture for OpenPetra's bank import functionality, specifically for the CAMT.052 format (ISO 20022 standard). It contains a sample bank account report with account information, balances, and two transaction entries - one credit donation from 'Mr Faithful Donor' and one debit payment to 'The Hosting Company'. The file includes standard CAMT elements like message headers, account details with IBAN, opening and closing balances, and transaction details with remittance information, providing a comprehensive test case for the bank import module.

 **Code Landmarks**
- `Line 3`: Uses CAMT.052.001.02 XML schema, an ISO 20022 standard format for bank account reporting
- `Line 52`: Contains both opening (PRCD) and closing (CLBD) balance elements for testing balance reconciliation
- `Line 67`: Includes a credit transaction with donor information for testing donation import functionality
- `Line 113`: Contains a debit transaction with mandate ID for testing payment processing
### BankImport/camt_testfile.53.xml

This XML file serves as a test fixture for the bank import functionality in OpenPetra, specifically for the CAMT.053 format (ISO 20022 bank statement). It contains sample bank statement data structured according to the CAMT.053 standard, including account information, balances, and transaction entries. The file includes two sample transactions: a credit entry representing a donation and a debit entry for a hosting service payment. Each transaction contains detailed information such as amounts, dates, account details, and remittance information that would be processed by the bank import module during testing.

 **Code Landmarks**
- `Line 4`: Uses the ISO 20022 XML schema for CAMT.053.001.02 bank statement format
- `Line 53`: Contains balance information with both previous (PRCD) and closing (CLBD) balance types
- `Line 71`: First transaction entry shows credit donation with donor details and remittance information
- `Line 108`: Second transaction demonstrates debit entry with mandate ID for direct debit payment
### Budget/Test.Budget.cs

TestBudget implements NUnit tests for budget functionality in OpenPetra's finance module. The file tests budget consolidation and autogeneration processes. Key functionality includes importing budgets from CSV files, consolidating budgets, updating budget values, and testing budget autogeneration for future periods. The class contains test methods T0_Consolidation, T1_AutoGenerationLoadData, T2_AutoGenerationGenBudget, and T4_AutoGenerationSetBudgetAmount that verify budget data manipulation and consolidation works correctly. Important methods include Init() for test setup, TearDownTest() for cleanup, and LoadData() which prepares test data.

 **Code Landmarks**
- `Line 102`: Tests budget consolidation by importing from CSV, verifying values, and checking consolidated results
- `Line 214`: Tests unposting and reposting budgets to verify changes propagate correctly
- `Line 247`: Demonstrates how budget values are modified and marked for consolidation
- `Line 282`: LoadData method creates a reusable dataset for multiple test cases
### CrossLedger/AvailableLedgers.test.cs

TAvailableLedgersTest implements unit tests for the GetAvailableLedgers method in OpenPetra's financial module. It verifies that the method correctly returns a subset of ledger data with appropriate filtering capabilities. The test creates temporary test ledgers, tests retrieval of all ledgers, in-use ledgers, and currency-specific ledgers, then cleans up afterward. Key methods include Init() for test setup, TearDown() for cleanup, and test methods TestInitialisation(), TestGetAvailableLedgers(), TestGetAvailableInUseLedgers(), and TestGetAvailableJPYLedgers(). Helper methods manage test data creation and removal.

 **Code Landmarks**
- `Line 36`: Test focuses specifically on validating a SELECT statement that returns only selected columns from ledgers
- `Line 61`: Uses a list of test ledger numbers (9997-9999) to avoid conflicts with existing data
- `Line 138`: Tests filter ledgers by status using DataView filtering capabilities
- `Line 155`: Tests currency-based filtering to verify multi-currency support
### GL/PostGLBatch.test.cs

This test file verifies the General Ledger batch posting functionality in OpenPetra's financial module. It focuses on testing argument validation for various GL posting methods including PostGLBatch, PostGLBatches, PrepareGLBatchForPosting, and LoadGLBatchData. The tests ensure proper validation of ledger numbers, batch numbers, and other parameters. The TGLBatchTest class contains multiple test methods that validate error handling when invalid parameters are provided, such as negative ledger or batch numbers. Each test verifies that appropriate exceptions are thrown or error messages are generated when validation fails.

 **Code Landmarks**
- `Line 38`: Uses NUnit testing framework for automated server-side business logic testing
- `Line 54`: Implements test fixture setup that connects to a test server using configuration file
- `Line 82`: Tests verify both exception throwing and error message content in verification results
- `Line 169`: Tests database transaction handling during GL batch preparation
### GL/Test.GL.CommonAccountingTool.cs

TestCommonAccountingTool implements unit tests for the TCommonAccountingTool class which handles general ledger accounting operations. The file contains three test methods: Test_01_BaseCurrencyAccounting verifies correct debit/credit operations in base currency, Test_02_ForeignCurrencyAccounting tests foreign currency transactions, and Test_03_ForeignCurrencyAccountingWithWrongForeignValue validates error handling for invalid currency operations. The tests verify that account balances are properly updated in both base and foreign currencies, comparing GLM values before and after transactions.

 **Code Landmarks**
- `Line 53`: Test validates that accounting operations match Petra's legacy behavior with identical database entries
- `Line 108`: Tests succeed by fortunate accident as GLMInfo initially returns empty data with 0 values for non-existent rows
- `Line 161`: PrepareTestCaseData method conditionally loads test data only when needed
- `Line 209`: Test specifically validates exception handling for currency mismatches
### GL/Test.GL.CommonTools.cs

This test file validates the functionality of common tools used in OpenPetra's General Ledger (GL) module. It contains test fixtures for TLedgerInitFlagHandler (managing boolean flags in database), TLedgerInfo (retrieving ledger information), TAccountPeriodInfo (handling accounting periods), FormatConverter (currency format validation), TCurrencyInfo (currency handling and conversion), TGetAccountHierarchyDetailInfo (account hierarchy navigation), and TAccountPropertyHandler (special account code management). The tests verify database interactions, currency conversions, rounding rules, and account relationships within the financial system.

 **Code Landmarks**
- `Line 48`: Tests database persistence of boolean flags with component-based values that can be individually added or removed
- `Line 106`: Currency format validation with custom exception handling for invalid formats
- `Line 151`: Currency conversion implementation with precise rounding rules based on currency-specific decimal places
- `Line 183`: Account hierarchy navigation showing parent-child relationships between financial accounts
### GL/Test.GL.Import.cs

TestGLImport implements unit tests for the General Ledger batch import functionality in OpenPetra. The file contains a test fixture that verifies the ability to import GL batches from CSV files with specific formatting requirements. The main test method Test_01_GL_Batch_Import loads test data, modifies dates to use the current year, and calls the ImportGLBatches method from TGLTransactionWebConnector. The class also includes setup and teardown methods that establish database connections, prepare test data by creating a new ledger, and disconnect from the server.

 **Code Landmarks**
- `Line 75`: Dynamically updates test data dates to use current year, ensuring tests remain valid over time
- `Line 81`: Uses verification result collection pattern to validate import success with detailed error reporting
- `Line 107`: Creates a new ledger for each test run to ensure clean test environment
### GL/Test.GL.PeriodEnd.Month.cs

TestGLPeriodicEndMonth implements NUnit tests for the GL.PeriodEnd.Month routines in OpenPetra's financial module. It tests critical month-end operations including detection of unposted batches, suspended accounts, unposted gift batches, and currency revaluation requirements. The class contains test methods like Test_PEMM_02_UnpostedBatches, Test_PEMM_03_SuspensedAccounts, Test_PEMM_04_UnpostedGifts, Test_PEMM_05_Revaluation, and Test_SwitchToNextMonth. It also includes helper methods for test data management and a ChangeSuspenseAccount class for manipulating suspense accounts during testing.

 **Code Landmarks**
- `Line 73`: Tests verify that month-end operations properly detect unposted batches as a critical error condition
- `Line 123`: Tests suspended accounts handling with different severity levels depending on period (status for regular periods, critical for year-end)
- `Line 240`: Tests foreign currency revaluation requirements for month-end processing
- `Line 304`: Demonstrates month-end period advancement through multiple accounting periods
- `Line 450`: ChangeSuspenseAccount helper class provides database manipulation for testing suspense account conditions
### GL/Test.GL.PeriodEnd.Year.cs

TestGLPeriodicEndYear implements unit tests for the General Ledger year-end processing in OpenPetra's financial module. It tests the year-end closing process, including reallocation of income and expense accounts, verification of account balances before and after year-end, and proper initialization of the new financial year. The class contains three main test methods: Test_YearEnd (verifies a single year-end process), Test_2YearEnds (tests consecutive year-end operations), and Test_TAccountPeriodToNewYear (tests calendar year transitions including leap years). It also includes helper methods to verify account balances and period entries across cost centers.

 **Code Landmarks**
- `Line 123`: Creates test accounting transactions with specific patterns to verify year-end processing
- `Line 193`: Demonstrates the reallocation process that moves income/expense balances during year-end
- `Line 229`: Shows how year-end processing resets income/expense accounts while preserving balance sheet accounts
- `Line 318`: Implementation of consecutive year-end operations to test multi-year functionality
- `Line 417`: Tests leap year handling in the accounting period system
### GL/Test.GL.PeriodEnd.cs

TestGLPeriodicEnd implements unit tests for the General Ledger period end operations in OpenPetra. The file contains a test fixture class with methods to verify the functionality of TPeriodEndOperations and AbstractPeriodEndOperation classes. It includes two helper classes: TestOperation (which extends AbstractPeriodEndOperation) and TestOperations (which extends TPeriodEndOperations). The tests verify that operations run correctly in both information mode and normal mode, with proper initialization, job counting, and operation execution. The fixture includes standard NUnit setup and teardown methods to connect and disconnect from the Petra server.

 **Code Landmarks**
- `Line 41`: TestOperation class extends AbstractPeriodEndOperation to create a testable implementation with counters to verify operation execution
- `Line 76`: TestOperations class demonstrates how period end operations are sequenced and executed in both info and normal modes
- `Line 119`: Test_AbstractPeriodEndOperation method shows the verification pattern used for testing period end operations
### GL/Test.GL.Revaluation.Utils.cs

TestGLRevaluation implements a test fixture for testing General Ledger revaluation functionality in OpenPetra's financial module. The class sets up the testing environment by connecting to the Petra server, loading test data, and disconnecting after tests complete. It includes methods for initializing the test environment (Init), tearing it down (TearDownTest), and loading specific test data (LoadTestTata). The LoadTestTata method ensures that currency test data exists in the database, loading it from an SQL file if necessary.

 **Code Landmarks**
- `Line 46`: Uses OneTimeSetUp attribute to establish database connection once before all tests run
- `Line 61`: Uses OneTimeTearDown attribute to properly disconnect from database after all tests complete
- `Line 75`: Conditionally loads test data only if required currency records don't already exist
### GL/Test.GL.Revaluation.cs

TestGLRevaluation implements a test fixture for validating the mathematical calculations in the General Ledger revaluation functionality. The file contains a single test method, Test_05_AccountDelta, which verifies the CLSRevaluation.AccountDelta method using multiple test cases with different parameters. Each test case provides input values for base currency amount, foreign currency amount, exchange rate, and currency digits, along with the expected result. The method tests various scenarios of currency conversion calculations to ensure accurate financial revaluation.

 **Code Landmarks**
- `Line 40`: Uses NUnit's TestCase attribute for parameterized testing with multiple currency conversion scenarios
- `Line 44`: Comment explains that negative values indicate excess currency units in the ratio
### GL/test-csv/glbatch-import.csv

This CSV file contains test data for GL (General Ledger) batch import functionality in OpenPetra. It includes two sample batches with financial transactions in a semicolon-delimited format. Each batch starts with a 'B' record (batch header), followed by a 'J' record (journal header), and multiple 'T' records (transaction details). The transactions include account numbers, descriptions, references, dates, debit and credit amounts, and various optional fields. The file demonstrates balanced transactions with matching debits and credits within each batch.

 **Code Landmarks**
- `Line 1`: Uses semicolon as delimiter rather than comma, which is important for financial data containing commas in numbers
- `Line 3-4`: Demonstrates balanced transaction with equal debit (2269,98) and credit amounts using European number format with comma as decimal separator
- `Line 7-12`: Shows multiple transactions that balance within a batch with special account notation '{ledgernumber}00' as a placeholder
### GL/test-csv/glbatch-import2.csv

This CSV file contains test data for the General Ledger batch import functionality in OpenPetra. It includes two sample batches with multiple financial transactions. Each batch starts with a B (batch) record followed by a J (journal) record and multiple T (transaction) records. The data includes account codes, descriptions, dates, and monetary amounts in EUR currency. The file demonstrates various transaction types with debit/credit entries that balance within each batch.

 **Code Landmarks**
- `Line 1-2`: Batch header format with batch description, sequence number, and effective date
- `Line 3`: Journal record format showing ledger type, currency code, and transaction date
- `Line 4-5`: Transaction records with balanced debit/credit entries totaling 2269.98
- `Line 9-14`: More complex batch with multiple transactions including donor references and transaction codes
### GL/test-csv/glbatch-import3.csv

This test CSV file contains sample data for testing the GL batch import functionality in OpenPetra. It includes two batches of financial transactions with various accounting entries. Each batch starts with a 'B' record containing batch description and date, followed by a 'J' record with journal details, and multiple 'T' records representing individual transactions. The transactions include ledger numbers, account codes, descriptions, references, dates, and monetary values. The file uses a semicolon-delimited format with values enclosed in double quotes.

 **Code Landmarks**
- `Line 1-4`: First batch with two balanced transactions totaling 2269.98 EUR
- `Line 5-12`: Second batch with placeholder {ledgernumber} that likely gets replaced during testing
- `Line 7-12`: Multiple transaction entries with donor references and specific account codes
### GL/test-csv/gltransactions-import.csv

This is a test CSV file containing sample general ledger transaction data for import testing in OpenPetra's financial module. Each line represents a transaction record with fields separated by semicolons, including ledger numbers, account codes, descriptions, dates, and monetary values. The file demonstrates various transaction types with placeholder values like '{ledgernumber}00' that would be replaced during actual testing. The data structure follows a specific format required by OpenPetra's GL transaction import functionality.

 **Code Landmarks**
- `Line 1`: Each record starts with 'T' indicating a transaction line type
- `Line 1-7`: The file uses a semicolon-delimited format with 18 fields per record
- `Line 1`: Contains placeholder '{ledgernumber}00' that would be dynamically replaced during testing
### GL/test-sql/gl-test-account-data.sql

This SQL file contains test data for the a_account table in OpenPetra's General Ledger module. It provides sample account records with properties like account codes, descriptions, types, and currency settings. The file includes two test accounts: a GBP petty cash asset account (6001) and a JPY petty cash asset account (6002). Each record contains complete field definitions including ledger number, descriptions, posting status, and system flags needed for GL testing.

 **Code Landmarks**
- `Line 8`: Uses {ledgernumber} placeholder that gets replaced during test execution rather than hardcoded ledger values
- `Line 8`: Sample accounts are configured as foreign currency accounts with specific currency codes (GBP, JPY)
### GL/test-sql/gl-test-batch-data.sql

This SQL file contains test data for the General Ledger batch functionality in OpenPetra's finance module. It populates the a_batch table with four test records having different batch statuses (Unposted, Posted, Cancelled, and HasTransactions). Each record includes fields for ledger number, batch number, description, financial totals, period information, dates, and system tracking fields. The data is designed to support testing of GL periodic end-month operations.

 **Code Landmarks**
- `Line 1`: Uses PostgreSQL's COPY command for efficient bulk data insertion
- `Line 2`: Contains placeholder {ledgernumber} that gets replaced at runtime with actual ledger number values
- `Line 3`: Includes four different batch statuses to test various batch processing scenarios
### GL/test-sql/gl-test-costcentre-data.sql

This SQL script provides test data for the a_cost_centre table in the GL module. It inserts four cost center records with different configurations: two local cost centers (4301, 4302) and two foreign cost centers (4303, 4304). Each record includes settings for ledger number, cost center code, reporting hierarchy, name, posting flags, activity status, and accounting parameters like clearing and retained earnings accounts. The script uses {ledgernumber} as a placeholder that gets replaced during test execution.

 **Code Landmarks**
- `Line 7`: Uses {ledgernumber} placeholder that gets dynamically replaced during test execution
### GL/test-sql/gl-test-currency-data.sql

This SQL file contains a minimal PostgreSQL database dump for testing currency functionality in OpenPetra's GL (General Ledger) module. It defines a single record in the a_currency table with a 'Damaged Currency' entry using code 'DMG'. The table structure includes fields for currency code, name, symbol, country code, display format, EMU status, and standard system fields for tracking creation and modification metadata.

 **Code Landmarks**
- `Line 7`: Creates test data with a deliberately nonsensical display format ('nonsense') to likely test system handling of invalid currency formats
### GL/test-sql/gl-test-feespayable-data.sql

This SQL file provides test data for fees payable functionality in OpenPetra's finance module. It contains INSERT statements for the a_fees_payable table with sample fee configurations including fixed fees, minimum percentage fees, and maximum percentage fees. Each record includes ledger number, fee code, charge options, amounts, account codes, descriptions, and audit fields. The data represents different fee calculation methods that would be used in financial transactions testing.

 **Code Landmarks**
- `Line 7`: Uses a {ledgernumber} placeholder that gets replaced during test execution rather than hardcoded values
- `Line 8-10`: Demonstrates three different fee calculation methods: Fixed, Minimum, and Maximum percentage-based charges
### GL/test-sql/gl-test-feesreceivable-data.sql

This SQL data file provides test data for the fees receivable functionality in OpenPetra's General Ledger module. It contains a PostgreSQL COPY statement that inserts a single record into the a_fees_receivable table with fields for ledger number, fee code, charge options, amounts, account codes, descriptions, and audit fields. The sample record represents a 'Home Office Admin Due2' fixed fee of 10.00 with associated account codes and cost centers.

 **Code Landmarks**
- `Line 6`: Uses a placeholder {ledgernumber} that likely gets replaced during test execution with an actual ledger number value
### GL/test-sql/gl-test-year-end.sql

This SQL script creates test data for GL year-end processing in OpenPetra's finance module. It contains PostgreSQL commands to populate the a_cost_centre table with three test cost centers (4301, 4302, 4303) for year-end testing. Each cost center is configured with the same properties including active status, clearing account (8500), retained earnings account (9700), and rollup style. The script uses a placeholder {ledgernumber} that gets replaced during test execution.

 **Code Landmarks**
- `Line 7`: Uses {ledgernumber} placeholders that get substituted at runtime with actual ledger numbers
### Gift/AnnualReceipts.test.cs

TGiftAnnualReceiptTest implements unit tests for generating annual gift receipts in OpenPetra's finance module. It tests the creation of receipts by importing and posting sample gift batches, then generating annual receipts in HTML and PDF formats. The class includes setup and teardown methods for database connections, and a test method that verifies receipt generation against expected output. Key functionality includes importing gift data, posting gift batches, and generating localized receipts with proper formatting. Important methods include Init(), TearDown(), ImportAndPostGiftBatch(), and TestAnnualReceipt().

 **Code Landmarks**
- `Line 82`: Imports gift batch data with dynamic replacement of ledger number and year values to ensure test data is current
- `Line 111`: Database transaction to set send_mail flag for specific partner, showing how partner communication preferences affect receipt generation
- `Line 166`: Localization handling with Catalog.Init for German language receipts
- `Line 173`: Dynamic date formatting in expected output to handle test execution on different dates
### Gift/PostGiftBatch.test.cs

TGiftBatchTest implements unit tests for the gift processing functionality in OpenPetra's finance module. It tests critical operations including importing and posting gift batches, processing admin fees, handling recipient ledger numbers, and cost center assignments. The class verifies that gift transactions are properly processed with correct recipient information, cost centers are properly assigned based on motivation details, and admin fees are calculated correctly. It also tests argument validation for various gift processing methods and recurring gift batch functionality. The tests ensure that the gift processing system maintains data integrity when handling donations.

 **Code Landmarks**
- `Line 73`: Test setup connects to a test database configuration specified in an external config file
- `Line 143`: Tests gift batch posting with sample data that uses string replacement for dynamic values
- `Line 192`: Tests automatic cost center correction based on motivation detail during posting
- `Line 426`: Implementation of recurring gift batch testing with proper partner relationship setup
- `Line 689`: Comprehensive argument validation testing for finance system API methods
### Gift/RecurringGiftBatch.test.cs

TRecurringGiftBatchTest implements unit tests for recurring gift batch operations in OpenPetra's finance module. It tests the creation, modification, and deletion of recurring gift batches with their associated gift details. Key functionality includes testing deletion of both saved and unsaved gift batches, verifying proper database operations, and ensuring data integrity during these operations. The class uses NUnit for testing and connects to a test server to perform database operations. Important methods include TestDeleteSavedGiftBatch() and TestDeleteUnsavedGiftBatch(), which verify different deletion scenarios.

 **Code Landmarks**
- `Line 88`: Creates a recurring gift batch with detailed transaction data for testing deletion scenarios
- `Line 114`: Tests deletion behavior with database persistence, verifying rows remain until SubmitChanges()
- `Line 176`: Demonstrates different behavior when deleting unsaved vs. saved records
### Gift/RevertAdjustGiftBatch.test.cs

TRevertAdjustGiftBatchTest implements a test fixture for OpenPetra's gift batch adjustment functionality. It tests the process of importing, posting, and adjusting gift batches, focusing on the financial implications of these operations. Key functionality includes importing a sample gift batch, posting it, creating an adjustment batch that modifies gift amounts and recipients, and verifying that the financial ledger reflects these changes correctly. The class also tests that gift receipts are properly updated after adjustments. Important methods include ImportAndPostGiftBatch(), TestAdjustGiftBatch(), Init() for setup, and TearDown() for cleanup.

 **Code Landmarks**
- `Line 77`: Imports gift batch from CSV file with dynamic replacement of ledger number and year values
- `Line 157`: Tests gift receipt generation before and after adjustments to verify proper receipt updates
- `Line 172`: Demonstrates the adjustment workflow by modifying gift amount and recipient
- `Line 208`: Verifies financial integrity by checking account balances after adjustments
### Gift/SetMotivationGroupAndDetail.test.cs

SetMotivationGroupAndDetailTest implements a test suite for validating gift motivation group and detail functionality in OpenPetra. It tests the TGuiTools.GetMotivationGroupAndDetailForPartner method with various partner scenarios including null partners, invalid partners, regular persons, and units with/without key ministries. The class also tests related gift transaction functionality such as GetRecipientFundNumber, CheckCostCentreLinkForRecipient, and KeyMinistryExists. Helper methods create test partners and units for testing purposes. The tests verify that motivation details are properly assigned based on partner types, especially the KEYMIN designation for unit partners.

 **Code Landmarks**
- `Line 100`: Tests verify that motivation details are automatically set to 'KEYMIN' for unit-type partners
- `Line 179`: Test for GetRecipientFundNumber verifies proper ledger number assignment for gift recipients
- `Line 196`: Tests for recipient ledger functionality temporarily disabled with TODO comments
- `Line 247`: Helper method creates test partners with specific attributes needed for testing
### Gift/SingleGiftReceipt.test.cs

TGiftSingleGiftReceiptTest implements a test fixture for generating single gift receipts in OpenPetra's finance module. It tests the functionality of creating gift receipts by importing a test gift batch, generating a receipt using an HTML template, and comparing the output with an expected result. The class includes setup and teardown methods for database connections and a TestSingleReceipt method that tests the receipt generation process. Important methods include Init(), TearDown(), and TestSingleReceipt(), with FLedgerNumber as a key variable.

 **Code Landmarks**
- `Line 50`: Test fixture is currently disabled with the //[Test] comment, suggesting it may be under development or maintenance
- `Line 71`: Uses template-based receipt generation with HTML files that can be customized
- `Line 84`: Contains a TODO comment about calendar vs financial date handling that needs resolution
- `Line 89`: Demonstrates integration with a localization system by specifying 'de-DE' culture for receipt generation
### Gift/test.cs

TGiftTest implements a test fixture for OpenPetra's gift management functionality. It contains setup and teardown methods for database connections and two test methods: TestSimpleDatabaseAccess verifies the number of ledgers in the database, and TestSimpleWebConnector tests the TReceiptingWebConnector's CreateAnnualGiftReceipts method with invalid parameters to ensure proper error handling. The class demonstrates both direct database access and web connector usage patterns for testing the MFinance.Gift module.

 **Code Landmarks**
- `Line 47`: Uses TPetraServerConnector to establish connection with test configuration file
- `Line 72`: Demonstrates transaction management pattern with try/catch/finally and rollback for database tests
- `Line 92`: Tests web connector with intentionally invalid parameters to verify error handling
### ICH/ICHHOSAFileReports.test.cs

TICHHOSAFileReportsTest implements unit tests for the International Clearing House (ICH) HOSA file generation and reporting functionality. It tests file header replacement, HOSA file generation, and gift export operations. The class verifies that the system can properly generate financial reports, replace headers in CSV files, and export gift transactions for the HOSA process. Key methods include TestFileHeaderReplace(), TestGenerateHOSAFiles(), and TestExportGifts(). Important variables include FLedgerNumber, MainFeesPayableCode, and MainFeesReceivableCode.

 **Code Landmarks**
- `Line 72`: Test method verifies file header replacement functionality in CSV files for financial reports
- `Line 106`: Explicit test for HOSA file generation that creates and verifies financial clearing house files
- `Line 146`: Test method for exporting gifts as part of the HOSA process, including stewardship calculation
### ICH/PerformStewardshipCalculation.test.cs

TStewardshipCalculationTest implements unit tests for the stewardship calculation functionality in OpenPetra's financial module. The test class verifies the correct calculation and distribution of administrative fees between different entities in the International Clearing House (ICH) system. Key tests include importing and posting gift batches, processing admin fees, and performing stewardship calculations that distribute funds between home office, GIF (Global Impact Fund), receiving fields, and clearing house according to defined percentages. The class includes helper methods for importing test data and validating financial calculations across different accounts.

 **Code Landmarks**
- `Line 72`: ImportAndPostGiftBatch method demonstrates how test data is prepared by replacing placeholder values in CSV files with test-specific values
- `Line 175`: ImportAdminFees method conditionally loads test data only when needed, improving test efficiency
- `Line 223`: CalculateAdminFee test verifies the precise percentage-based fee calculation (1% of 200)
- `Line 268`: Test runs an initial empty calculation to clear previous data before running the actual test, ensuring test isolation
- `Line 307`: Multiple assertions verify the correct distribution of funds between different entities with specific percentages
### Reporting/AccountDetail.test.cs

TAccountDetailTest implements unit tests for the Account Detail report in OpenPetra's financial module. The file contains test methods that verify the report's functionality with different parameter configurations. It tests both standard account detail reporting and reporting with selected accounts. Each test method sets up specific parameters (ledger number, period range, account codes), calculates the report using TReportTestingTools, and validates the results against expected output files. The class includes Init and TearDown methods to manage database connections for testing.

 **Code Landmarks**
- `Line 80`: Uses a JSON configuration file to define the report structure being tested
- `Line 84`: Explicitly casts parameter values as strings using TVariant to ensure proper type handling
- `Line 88`: Uses TReportTestingTools to calculate and validate reports against expected results
### Reporting/BalanceSheet.test.cs

TBalanceSheetTest implements a test fixture for validating the Balance Sheet report in OpenPetra's financial reporting system. The class tests the business logic directly on the server by connecting to a test database, executing the balance sheet report, and comparing the results against expected values. The main test method TestBalanceSheet is currently ignored due to issues with summary calculations. The fixture includes standard NUnit setup and teardown methods for database connection management. Key methods include Init(), TearDown(), and TestBalanceSheet().

 **Code Landmarks**
- `Line 73`: The test is marked with [Ignore] attribute indicating known issues with balance sheet summary calculations that need fixing.
- `Line 51`: Uses TPetraServerConnector to establish connection to test server using configuration file.
- `Line 83`: Demonstrates report testing pattern using JSON test definition and CSV result validation.
### Reporting/IncExpStmt.test.cs

TIncExpStatementTest implements a test fixture for validating the Income and Expense Statement report in OpenPetra's financial module. The class tests the business logic directly on the server by connecting to a test database, executing the report with specific parameters, and validating the results against expected output. The test focuses on a specific ledger (43) with defined period parameters and cost center options. Key methods include Init() for setup, TearDown() for cleanup, and TestIncExpStatement() which executes the actual report test using the TReportTestingTools utility class.

 **Code Landmarks**
- `Line 76`: Test is currently ignored with a note that the Income/Expense statement needs fixing to calculate summaries in glm
- `Line 82`: Uses JSON test data file and CSV results file for test validation
- `Line 86`: Retrieves standard cost center dynamically using TGLTransactionWebConnector
### Reporting/TestData/AccountDetail.Parameters.Expected.xml

This XML file contains parameter definitions for the Account Detail financial report in OpenPetra. It specifies formatting rules for report columns including alignment, captions, number formats, positions, and widths. The file defines parameters for filtering financial data by account codes, cost centers, date ranges, and periods. It also includes configuration for report headers, currency settings, and calculation methods. These parameters control how financial transaction data is displayed in the report, with specific formatting for debits, credits, balances, and transaction details across different report levels and sections.

 **Code Landmarks**
- `Line 103`: Uses composite string construction with multiple data types to create a formatted date range header
- `Line 106-108`: Dynamic report header construction using variables like CurrentPeriod
- `Line 34-46`: Sophisticated number formatting with different formats for positive/negative values and credit/debit indicators
### Reporting/TestData/AccountDetail.Results.Expected.html

This file is an HTML template for the Account Detail financial report in OpenPetra. It displays detailed transaction information for multiple accounts (10100-0100 through 10500-0100) with their respective cost centers. The report shows transaction dates, references, narratives, and monetary values (debits, credits, and balances). Each account section includes a header with account number and description, followed by chronologically ordered transactions, and concludes with a footer showing net balance information. The layout uses a responsive grid system with columns for different data elements and includes styling for currency formatting and visual separation between accounts.

 **Code Landmarks**
- `Line 1-7`: Basic HTML structure with UTF-8 charset and external CSS reference for report styling
- `Line 8-21`: Header section with placeholders for report title, ledger name, and filtering criteria marked with TODO comments
- `Line 23-30`: Column headings define the report's data structure: transaction date, reference, narrative, debit, credit, and end balance
- `Line 35-40`: Account header pattern showing account number and description in a consistent format
- `Line 61-67`: Footer row pattern showing account totals with net balance calculation
### Reporting/TestData/AccountDetailSelectedAccount.Results.Expected.html

This HTML file serves as an expected results template for testing the Account Detail report in OpenPetra's financial module. It displays financial transactions for multiple accounts (10100-0100 through 10500-0100) with their respective cost centers. The report includes transaction dates, references, narratives, and monetary values (debits, credits, and end balances). Each account section shows individual transactions and concludes with a net balance summary. The file uses a structured HTML layout with CSS classes for formatting columns and rows, creating a standardized financial report presentation.

 **Code Landmarks**
- `Line 6`: Links to an external CSS file for report styling that would be served from the OpenPetra application
- `Line 16`: Contains placeholder tags with 'getLedgerName' function call that would be populated by the reporting engine
- `Line 20`: Multiple visibility:hidden elements suggest conditional display of report parameters based on user selections
### Reporting/TestData/BalanceSheetDetail.xml

This XML file contains configuration parameters for generating a Balance Sheet report in OpenPetra's finance module. It defines report settings including XML source files, report name, display columns, accounting periods, calculation types, and formatting options. The file specifies five calculation columns: Actual Selected Year, Actual Previous Year, Actual End of Previous Year, Variance, and Variance Percentage. Additional parameters control currency format, account hierarchy, ledger number, cost center options, and report depth level. These parameters determine how financial data is retrieved, calculated, and displayed in the final balance sheet report.

 **Code Landmarks**
- `Line 3`: References external XML files that provide structure and data definitions for the report
- `Line 6`: MaxDisplayColumns parameter limits the report to 5 columns of financial data
- `Line 19-28`: Defines column-specific calculation types with variance calculations between columns
- `Line 32`: Specifies 'detail' depth parameter controlling the granularity of financial information displayed
### Reporting/TestData/IncExpStmt.xml

This XML file defines test parameters for the Income Expense Statement report in OpenPetra's testing framework. It configures report settings including XML source files, display columns, period settings, account hierarchy, currency format, and calculation types. The file specifies parameters for multiple report columns with different calculation methods (Actual, Budget, Variance) and time frames (period-specific or year-to-date). It includes settings for cost center handling, ledger number, and display depth, providing a comprehensive test configuration for financial reporting functionality.

 **Code Landmarks**
- `Line 3`: References multiple XML source files that provide the report structure and definitions
- `Line 16-42`: Configures 7 different report columns with various calculation types and comparison settings
- `Line 23-25`: Sets up variance percentage calculation using FirstColumn and SecondColumn parameters to define comparison sources
- `Line 43`: Uses 'mixed' YTD setting to combine period-specific and year-to-date calculations in the same report
### Reporting/TestData/TrialBalance.Results.Expected.html

This file is an expected results HTML template for testing the Trial Balance report functionality in OpenPetra's financial reporting system. It displays a structured financial report with account codes, names, debit amounts, credit amounts, and end balances for different cost centers (Guyana, Nigeria, Cambodia, El Salvador, and Tunisia). The template includes placeholder sections for report title, ledger name, descriptions, and period information, with a formatted table showing financial data with proper column alignment and styling through CSS references.

 **Code Landmarks**
- `Line 8`: References an external CSS file for report styling
- `Line 16`: Uses placeholder comments with 'TODO' markers for dynamic content insertion
- `Line 20`: Uses hidden visibility elements to conditionally display cost centre information
- `Line 37`: Structured div elements with specific IDs (acccc0-4) for each account entry, suggesting programmatic generation
### Reporting/TestData/TrialBalanceSelectedAccount.Results.Expected.html

This HTML file serves as an expected results template for testing the Trial Balance report in OpenPetra's financial module. It displays financial data for selected accounts across different cost centers (10100-10500), showing account codes, names, debit amounts, credit amounts, and end balances. The template includes a header section with placeholders for report title, ledger name, and period information, followed by a structured table displaying the financial data for five different geographical support gift accounts. The file uses CSS classes for formatting and layout of the financial information.

 **Code Landmarks**
- `Line 7`: References an external CSS file for report styling
- `Line 16`: Uses placeholder comments (<!-- TODO -->) for dynamic content insertion
- `Line 20`: Hidden visibility elements used to store filter criteria information
- `Line 35`: Structured HTML with column classes for tabular financial data presentation
- `Line 44`: Uses unique ID attributes (acccc0-4) for each account row, likely for DOM manipulation
### Reporting/TrialBalance.test.cs

TTrialBalanceTest implements unit tests for the Trial Balance report in OpenPetra's finance module. It tests the report generation functionality with different parameter combinations. The class contains setup and teardown methods to establish and close database connections, plus two test methods: TestTrialBalance (testing basic trial balance reporting with period and account filters) and TestTrialBalanceSelectedAccount (testing the report with specific account selection). Both tests use TReportTestingTools to generate HTML reports and verify the results against expected outputs.

 **Code Landmarks**
- `Line 82`: Uses a JSON configuration file to define the report structure
- `Line 87`: Explicitly converts parameter values to string variants to ensure proper type handling
- `Line 91`: Uses TReportTestingTools to abstract report calculation and testing
- `Line 116`: Demonstrates parameter switching between range-based and list-based account selection

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #