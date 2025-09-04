# C# Finance Module Of Petra

The C# Finance Module is a comprehensive financial management component of the OpenPetra application that implements core accounting, gift processing, and financial reporting functionality. The module provides a robust multi-currency accounting system with support for general ledger operations, accounts payable, gift management, budgeting, and international clearing house capabilities. This sub-project implements both the business logic and data access layers for financial operations along with specialized import/export functionality for various financial formats. This provides these capabilities to the Petra program:

- Multi-currency accounting with exchange rate management
- Complete gift processing with receipting and tax deductibility tracking
- General ledger operations with batch processing and period-end procedures
- Accounts payable with supplier management and payment processing
- Budget creation, management and forecasting
- International Clearing House (ICH) for cross-ledger transactions

## Identified Design Elements

1. **Modular Financial Components**: The finance module is organized into distinct functional areas (GL, Gift, AP, Budget, ICH) that share common validation and data access patterns
2. **Transaction-Based Processing**: Financial operations use batch/journal/transaction hierarchies with validation at each level to maintain accounting integrity
3. **Multi-Currency Support**: Comprehensive handling of base, foreign, and international currencies with exchange rate management
4. **Period Management**: Structured accounting periods with month-end and year-end procedures to maintain financial data integrity
5. **Import/Export Framework**: Standardized approaches for importing and exporting financial data in various formats (CSV, MT940, CAMT, SEPA)

## Overview
The architecture emphasizes accounting integrity through comprehensive validation at multiple levels. The module maintains a clear separation between different financial subsystems while providing integration points through the general ledger. The design supports both interactive operations through web connectors and batch processing for imports and period-end procedures. Data access is optimized through caching mechanisms for frequently accessed financial data, and the module includes extensive reporting capabilities for financial analysis and compliance requirements.

## Business Functions

### Account Management
- `Common/Common.Tools.Accounts.cs` : Financial account management utilities for OpenPetra, handling account hierarchies and special account properties.
- `Common/Common.Tools.GLM.cs` : Provides tools for accessing and managing General Ledger Master data in the OpenPetra financial module.
- `setup/GL.Setup.cs` : Server-side implementation for General Ledger setup operations in OpenPetra finance module.
- `setup/GL.Setup.Validation.cs` : Validation module for General Ledger setup in OpenPetra's finance module.

### Accounts Payable
- `AP/AP.EditSupplier.cs` : Server-side connector for managing supplier data in the Accounts Payable module of OpenPetra.
- `AP/AP.EditTransaction.cs` : Manages AP transaction editing, posting, and payment processing for OpenPetra's finance module
- `AP/AP.EditTransaction.Validation.cs` : Validates accounts payable transaction details in OpenPetra's finance module.
- `AP/AP.Find.cs` : Implements server-side search functionality for Accounts Payable, finding suppliers, invoices, and transactions.
- `AP/AP.Remittance.cs` : Handles accounts payable remittance advice and cheque generation for financial documents.
- `GL/Reporting.APReports.UIConnectors.cs` : Provides server-side connectors for Accounts Payable reporting in OpenPetra's finance module.
- `validation/AP.Validation.cs` : Validates accounts payable document details in OpenPetra's finance module.

### Bank Import
- `BankImport/Connector.cs` : Implements bank statement import functionality for OpenPetra's finance module, handling CSV, MT940, and CAMT formats.
- `BankImport/Import.cs` : Handles bank statement import functionality for OpenPetra's finance module.
- `BankImport/ImportFromCAMT.cs` : Imports bank statements from CAMT Swift files into OpenPetra's financial system.
- `BankImport/ImportFromCSV.cs` : Imports bank statements from CSV files into OpenPetra's financial system with configurable column mappings and formatting options.
- `BankImport/ImportFromMT940.cs` : Imports bank statements from MT940 Swift files into OpenPetra's financial system.
- `BankImport/Matching.cs` : Implements bank import matching logic to train the system by comparing bank statements with existing gift batches.
- `BankImport/ParseCAMT.cs` : Parses ISO 20022 CAMT.053 bank statement files for OpenPetra's financial system, extracting transaction data from XML format.
- `BankImport/ParseMT940.cs` : Parser for Swift MT940 bank statement files that processes and converts German bank statement data into structured formats.

### Budget Management
- `Budget/Budget.AutoGenerate.cs` : Server-side component for automatically generating budget data for financial planning in OpenPetra.
- `Budget/Budget.Consolidate.cs` : Server-side component for consolidating and managing budget data in OpenPetra's financial module.
- `Budget/Budget.Maintain.cs` : Server-side implementation for budget management in OpenPetra's finance module.

### Currency and Exchange Rates
- `Common/Common.CrossLedger.cs` : Provides server-side functionality for cross-ledger exchange rate operations in OpenPetra's finance module.
- `Common/Common.Tools.ExchangeRates.cs` : Provides exchange rate handling and currency conversion tools for OpenPetra's financial system.
- `GL/GL.Revaluation.cs` : Handles foreign currency account revaluation in OpenPetra's financial system by creating adjustment transactions at period end.
- `setup/CorporateExchangeRates.Setup.cs` : Manages corporate exchange rates for financial transactions in OpenPetra, including saving rates and updating transaction amounts.
- `validation/GL.Setup.Validation.cs` : Validates financial data in OpenPetra's GL module, ensuring data integrity for exchange rates and accounting periods.

### Data Access and Caching
- `data/Cacheable.ManualCode.cs` : Provides cacheable financial data tables for client-side use in OpenPetra's finance module.
- `data/DataAggregates.cs` : Provides data aggregation classes for financial ledger information in OpenPetra.

### Financial Reporting
- `GL/Reporting.APReports.UIConnectors.cs` : Provides server-side connectors for Accounts Payable reporting in OpenPetra's finance module.
- `GL/Reporting.FDReports.UIConnectors.cs` : Provides financial development reporting functionality for OpenPetra, handling donor and gift data analysis.
- `GL/Reporting.GiftReports.UIConnectors.cs` : Provides server-side data access for gift reporting in OpenPetra's financial module.
- `GL/Reporting.UIConnectors.cs` : Finance reporting web connector that provides data for financial reports in OpenPetra.
- `queries/ReportFinance.cs` : Provides financial reporting functionality for OpenPetra, including gift analysis and reporting across different time periods.

### General Ledger
- `Common/Common.AccountingTool.cs` : Provides a tool for creating accounting batches, journals, and transactions in OpenPetra's financial system.
- `Common/Common.Budget.Maintain.cs` : Server-side utility class for budget maintenance operations in OpenPetra's finance module.
- `Common/Common.Posting.cs` : Provides methods for posting, validating, and managing GL batches in the OpenPetra financial system.
- `GL/GL.AccountingPeriods.cs` : Provides server-side functionality for managing accounting periods in OpenPetra's financial system.
- `GL/GL.Exporting.cs` : Exports GL batch data to CSV format with options for summarization and currency handling.
- `GL/GL.Importing.cs` : Implements GL batch and transaction importing functionality for OpenPetra's financial system
- `GL/GL.PeriodEnd.Month.cs` : Implements month-end closing procedures for OpenPetra's financial ledger system, including period validation and stewardship calculation.
- `GL/GL.PeriodEnd.Year.cs` : Implements year-end financial processing for OpenPetra's general ledger module.
- `GL/GL.TransactionFind.cs` : Server-side implementation for finding GL transactions with filtering capabilities for the OpenPetra financial system.
- `GL/GL.Transactions.cs` : GL transaction management for OpenPetra's financial system
- `connect/GL.GLTransactionFind.cs` : Server-side connector for GL Transaction Find functionality in OpenPetra's finance module.
- `validation/GL.Validation.cs` : Server-side validation for General Ledger financial data in OpenPetra

### Gift Management
- `Gift/Gift.Adjustment.cs` : Implements gift adjustment functionality for reversing, adjusting, and field-changing gifts in OpenPetra's finance module.
- `Gift/Gift.Batch.cs` : Provides functionality for creating and managing gift batches in the OpenPetra financial system.
- `Gift/Gift.cs` : Server-side business logic for handling gift transactions in OpenPetra's finance module.
- `Gift/Gift.Exporting.SEPA.cs` : Exports recurring gift batches to SEPA Direct Debit files for electronic payment processing in OpenPetra.
- `Gift/Gift.Exporting.cs` : Exports gift batch data to Excel with options for filtering, summarizing, and formatting financial information.
- `Gift/Gift.GiftDetailFind.cs` : Gift detail search implementation for the OpenPetra financial system
- `Gift/Gift.Importing.cs` : Implements gift batch and transaction importing functionality for the OpenPetra financial system.
- `Gift/Gift.Receipting.cs` : Implements gift receipt generation and handling for OpenPetra's finance module.
- `Gift/Gift.Setup.cs` : Server-side implementation for managing gift motivation groups and details in OpenPetra's finance module.
- `Gift/Gift.TaxDeductiblePct.cs` : Manages tax deductibility percentages for gift recipients in the OpenPetra financial system.
- `Gift/Gift.Transactions.cs` : Server-side implementation for gift transaction processing in OpenPetra's finance module
- `Gift/Gift.Transactions.Validation.cs` : Validates gift transactions in OpenPetra's finance module with methods for batch and detail validation.
- `Gift/Gift.gui.tools.cs` : Server-side utility for validating gift recipient keys and determining appropriate motivation codes in OpenPetra's finance module.
- `connect/Gift.GiftDetailFind.cs` : Connector class for gift detail search functionality in OpenPetra's finance module.
- `validation/Gift.Validation.cs` : Provides validation functions for gift-related data in OpenPetra's finance module

### International Clearing House
- `ICH/ICH.GenHOSAFilesReports.cs` : Generates Home Office Statement of Accounts (HOSA) files and reports for ICH (International Clearing House) transactions.
- `ICH/StewardshipCalculation.cs` : Implements ICH Stewardship Calculation functionality for OpenPetra's financial system.

### Partner and Cost Center Management
- `Common/PartnerCostCentreLink.cs` : Provides functionality to check partner-cost centre relationships in the MFinance module.

### Period End Processing
- `Common/Common.PeriodEnd.cs` : Implements period-end operations framework for financial processing in OpenPetra's accounting system.
- `GL/GL.PeriodEnd.Month.cs` : Implements month-end closing procedures for OpenPetra's financial ledger system, including period validation and stewardship calculation.
- `GL/GL.PeriodEnd.Year.cs` : Implements year-end financial processing for OpenPetra's general ledger module.

### Query and Extract
- `queries/ExtractDonorByAmount.cs` : Extracts donor data based on gift amounts, filtering by various criteria like date range, minimum/maximum amounts, and number of gifts.
- `queries/ExtractDonorByField.cs` : Extracts donor information based on field/ledger criteria for financial reporting in OpenPetra.
- `queries/ExtractDonorByMiscellaneous.cs` : Server-side implementation of donor extraction by miscellaneous criteria for OpenPetra's financial reporting system.
- `queries/ExtractDonorByMotivation.cs` : Extracts donor data based on gift motivation details for financial reporting.
- `queries/ExtractRecipientByField.cs` : Extracts financial data for recipients who have given to specific ledgers within a date range.

### System Tools and Utilities
- `Common/Common.FinancialYear.cs` : Financial year management module for OpenPetra that handles period validation, date calculations, and ledger period operations.
- `Common/Common.Import.cs` : Utility class for parsing and validating data from import files in OpenPetra's finance module.
- `Common/Common.Tools.Ledger.cs` : Provides financial ledger information and flag management for OpenPetra's accounting system
- `Common/ServerLookups.cs` : Server-side finance lookup functions for retrieving accounting data like period dates, currencies, and account information.
- `validation/Finance.Checks.Validation.cs` : Empty validation file for finance checks in OpenPetra's financial module.
- `validation/Helper.cs` : Helper class providing validation functions for financial data in OpenPetra, particularly for date range validation in accounting periods.

## Files
### AP/AP.EditSupplier.cs

TSupplierEditUIConnector implements a server-side UIConnector for the Accounts Payable module that handles supplier data operations. It provides functionality to check if a supplier exists for a partner, retrieve supplier data as a typed dataset, and save supplier information. The connector manages database transactions, handles data validation, and ensures proper data formatting before submission. Key methods include CanFindSupplier(), GetData(), and SubmitChanges(), which work with the AccountsPayableTDS dataset to facilitate communication between the client UI and server-side business logic.

 **Code Landmarks**
- `Line 72`: Uses database connection pooling with optional database parameter
- `Line 137`: Automatically removes empty tables from dataset before sending to client to optimize network transfer
- `Line 153`: Ensures discount fields are never null by setting default values of 0
### AP/AP.EditTransaction.Validation.cs

TAPTransactionWebConnector implements validation functionality for accounts payable (AP) transactions in OpenPetra's finance module. The file contains a single static partial method ValidateApDocumentDetailManual that validates AP document details submitted through the web interface. It iterates through each row in the submitted data table and calls TFinanceValidation_AP.ValidateApDocumentDetailManual to perform the actual validation, collecting any validation errors in the provided TVerificationResultCollection. The method is designed to work within OpenPetra's financial transaction processing system.

 **Code Landmarks**
- `Line 37`: Uses partial methods pattern allowing implementation in separate files without modifying the core class definition
- `Line 40`: Implements row-by-row validation with counter-based error reporting for better error traceability
### AP/AP.EditTransaction.cs

TAPTransactionWebConnector implements server-side functionality for Accounts Payable (AP) document management in OpenPetra's finance module. It handles creating, loading, editing, approving, posting, and paying AP documents (invoices and credit notes). Key features include document validation, GL batch creation for posting, payment processing, and payment reversal. The class manages currency exchange rates, analysis attributes, and supplier defaults. Important methods include LoadAApDocument, CreateAApDocument, SaveAApDocument, PostAPDocuments, CreatePaymentTableEntries, and PostAPPayments. The connector ensures proper accounting entries are created in the General Ledger when AP transactions are processed.

 **Code Landmarks**
- `Line 182`: Creates AP document with default values from supplier settings including credit terms and exchange rates
- `Line 324`: Implements document balance validation to ensure total amount matches sum of detail lines
- `Line 343`: Validates required analysis attributes are present before allowing document posting
- `Line 1033`: Handles complex payment reversal by creating counterbalancing documents and GL entries
- `Line 500`: Creates GL batch with transactions for AP posting with proper currency handling and exchange rate calculations
### AP/AP.Find.cs

TFindUIConnector implements server-side search functionality for the Accounts Payable module in OpenPetra. It provides methods to find suppliers, invoices, and supplier transactions by executing SQL queries asynchronously using a paged data approach. The class handles search criteria preparation, query execution in separate threads, and post-processing of results including calculation of outstanding amounts, due dates, and discount information. Key methods include FindSupplier, FindInvoices, FindSupplierTransactions, PerformSearch, GetDataPagedResult, and the static GetPartPaidAmount method that calculates amounts already paid for invoices.

 **Code Landmarks**
- `Line 161`: Uses threaded asynchronous query execution to prevent UI blocking during database operations
- `Line 196`: Static GetPartPaidAmount method provides reusable invoice payment calculation used by multiple components
- `Line 258`: Post-processes query results to calculate business-specific values like outstanding amounts and discount dates
### AP/AP.Remittance.cs

TRemittanceWebConnector implements server-side functionality for generating remittance advice and cheque form data in the OpenPetra financial system. It provides methods to create formatted data for templated documents, supporting both standalone remittance advices and combined remittance-cheque outputs. The class processes payment information, retrieves associated invoice details, and formats data for document generation. Key methods include CreateRemittanceAdviceFormData, CreateRemittanceAdviceAndChequeFormData, and the private CreateFormDataInternal helper. The implementation handles multiple invoices per payment and supports progress tracking during document generation.

 **Code Landmarks**
- `Line 44`: Web connector class with finance module permission requirements for secure access
- `Line 79`: Progress tracking implementation provides real-time feedback during potentially lengthy document generation operations
- `Line 113`: Method supports dual-purpose document generation for both remittance advice and cheque printing
- `Line 164`: Handles complex financial relationships where a single payment may cover multiple invoices
- `Line 178`: Supports partial payment scenarios where an invoice may be only partially paid in a transaction
### BankImport/Connector.cs

TBankImportWebConnector implements server-side functionality for importing and processing bank statements in OpenPetra's finance module. It provides methods for importing bank statements from various formats (CSV, MT940, CAMT), saving and retrieving import settings, matching transactions with donors, creating gift and GL batches, and managing transaction details. Key functionality includes parsing bank files, training the system to recognize recurring transactions, matching transactions with donors based on IBAN numbers, and converting bank transactions into gift or GL batches. The class handles verification of data integrity, supports multiple currencies, and maintains transaction history. Important methods include ImportFromCSVFile, GetBankStatementTransactionsAndMatches, TrainBankStatement, CreateGiftBatch, and CreateGLBatch.

 **Code Landmarks**
- `Line 168`: Uses a thread to perform bank statement training operations asynchronously to prevent UI blocking
- `Line 357`: Implements a custom MatchUpdate struct to efficiently track and apply changes to transaction matches
- `Line 1035`: Creates gift batches from bank transactions with automatic donor matching and cost center identification
- `Line 1206`: Implements GL batch creation with automatic balancing of debits and credits
### BankImport/Import.cs

TBankStatementImport implements functionality for importing bank statements into the OpenPetra system. The class provides a static method StoreNewBankStatement that processes and stores bank statement data from a BankImportTDS dataset into the database. The method initializes a progress tracker, submits changes to the database while preserving statement keys, and then calls a training function to match imported statements with existing gift batches. It returns a TSubmitChangesResult indicating success or failure and outputs the first statement key for reference.

 **Code Landmarks**
- `Line 57`: Uses DomainManager to retrieve client ID for progress tracking
- `Line 59`: Implements progress tracking for user feedback during potentially lengthy import operations
- `Line 70`: Sets DontThrowAwayAfterSubmitChanges flag to preserve statement keys after database submission
- `Line 82`: Calls TBankImportMatching.Train() to automatically match imported statements with existing gift batches
### BankImport/ImportFromCAMT.cs

TBankStatementImportCAMT implements functionality to import bank statements from CAMT Swift files into OpenPetra's financial system. The class provides two main methods: ImportFromZipFile for processing multiple CAMT files from a zip archive, and ImportFromFile for handling individual CAMT files. It parses transaction data using TCAMTParser, processes transaction details including amounts, dates, and account information, and stores them in a BankImportTDS dataset. The code handles special transaction types, marks potential gifts, and organizes transactions by amount and account name to match paper statement ordering. Key functionality includes multi-year statement parsing and proper formatting of bank account identifiers from IBAN or traditional formats.

 **Code Landmarks**
- `Line 53`: Uses SharpZipLib to extract and process multiple CAMT files from a single ZIP archive
- `Line 137`: Handles multi-year statements by recursively calling itself with a flag to parse previous year data
- `Line 164`: Intelligently parses IBAN codes to extract branch and account information
- `Line 183`: Identifies potential gift transactions using specific transaction type codes
- `Line 227`: Implements custom sorting logic to match paper statement ordering
### BankImport/ImportFromCSV.cs

TBankStatementImportCSV implements functionality to import bank statements from CSV files into OpenPetra's financial system. It parses CSV content with configurable separators, date formats, number formats, and column mappings. The class provides methods to process statement data, map columns to transaction fields, handle currency codes, and store transactions in the database. Key functionality includes parsing CSV lines, mapping data to transaction fields, handling different date and number formats, and organizing transactions by month. Important methods include ImportBankStatement and ImportBankStatementHelper, which process the CSV content and return a BankImportTDS dataset containing statement and transaction data.

 **Code Landmarks**
- `Line 76`: Flexible column mapping system allows customization of CSV import format through AColumnMeaning parameter
- `Line 143`: Dynamic bank account detection based on filename patterns using 'BankNameFor' configuration parameters
- `Line 249`: Transactions are organized by month, with the month containing the most transactions being selected for import
- `Line 170`: Currency code determination with fallback logic that checks account settings then ledger settings
- `Line 199`: Conditional parsing based on AStartAfterLine parameter allows skipping header rows
### BankImport/ImportFromMT940.cs

TBankStatementImportMT940 implements functionality for importing bank statements from MT940 Swift files into OpenPetra's financial system. The main method ImportFromFile processes file content, parses transactions using TSwiftParser, and organizes them into statements by year. It populates BankImportTDS datasets with transaction details including dates, amounts, account information, and transaction types. The code identifies potential gift transactions using specific transaction type codes and sorts transactions by amount and account name to match paper statement order. Finally, it stores the imported data through TBankStatementImport.StoreNewBankStatement.

 **Code Landmarks**
- `Line 76`: Uses regex pattern matching to identify IBAN numbers and extract banking details
- `Line 101`: Transaction type codes are analyzed to flag potential donations with a special suffix
- `Line 139`: Implements custom sorting of transactions to match physical paper statement order
- `Line 142`: Uses a bidirectional counting system to number transactions based on whether amounts are positive or negative
### BankImport/Matching.cs

TBankImportMatching implements logic for training the bank import system to recognize recurring transactions. It analyzes historical bank statements and matches them with existing gift batches to create a knowledge base for future automatic matching. The class compares transaction details like amounts, dates, and donor information with gift records to establish reliable patterns. Key functionality includes finding appropriate gift batches for statements, matching transactions to gifts based on bank account numbers and transaction details, and storing these matches for future reference. Important methods include Train(), FindGiftBatch(), GetGiftsByDate(), MatchDonorsWithKnownBankaccount(), MatchTransactionsToGiftBatch(), and CalculateMatchText() which generates unique identifiers for transactions.

 **Code Landmarks**
- `Line 72`: Uses a sophisticated algorithm to find the best matching gift batch by counting matches between transactions and gifts
- `Line 245`: Implements donor identification by IBAN with fallback to description field for testing purposes
- `Line 464`: Uses word matching algorithm to compare donor names and descriptions for fuzzy matching
- `Line 694`: Implements special text normalization to handle bank formatting inconsistencies in transaction descriptions
- `Line 749`: Uses MD5 hashing to create compact unique identifiers when match text exceeds database field length
### BankImport/ParseCAMT.cs

TCAMTParser implements functionality to parse bank statement files in ISO 20022 CAMT.053 format. The class processes XML-structured bank statement data, extracting account information, balances, and transaction details. Key functionality includes parsing statement metadata (account codes, bank codes, currencies), processing balance information, and extracting individual transactions with their amounts, dates, and descriptions. The class handles different CAMT versions (52 and 53) and supports configuration options for specific accounts. Important classes include TCAMTParser, TTransaction (storing individual transaction data), and TStatement (representing a complete bank statement with transactions). The code manages cultural settings for proper decimal parsing and includes error handling for verification results.

 **Code Landmarks**
- `Line 77`: Handles multiple CAMT format versions (52 and 53) with different XML namespaces
- `Line 111`: Uses configuration settings to adjust statement IDs with DiffElctrncSeqNb parameter
- `Line 137`: Supports balance adjustments through configuration with DiffBalanceFor parameter
- `Line 162`: Filters transactions by year to handle statements that span multiple years
- `Line 233`: Special handling for SEPA direct debit batches with multiple transaction details
### BankImport/ParseMT940.cs

TSwiftParser implements functionality for parsing Swift MT940 bank statement files from German banks. It processes structured financial data including transaction details, account information, and balances. The class handles various Swift tags (like 20, 25, 60, 61, 86) to extract information about bank accounts, transactions, dates, and amounts. Key functionality includes parsing file content, handling different Swift data tags, and exporting the parsed data to XML format. Important classes include TSwiftParser, TStatement, and TTransaction, with methods like ProcessFileContent, HandleSwiftData, and ExportToXML.

 **Code Landmarks**
- `Line 78`: Handles special date parsing with fallback for invalid dates like February 30th by using the last day of the month
- `Line 173`: Character encoding handling for German special characters (umlauts) with specific character replacements
- `Line 203`: Balance validation that throws exceptions when calculated balance doesn't match the reported balance
- `Line 267`: Custom line reading implementation that handles different line endings and character encoding issues
### Budget/Budget.AutoGenerate.cs

TBudgetAutoGenerateWebConnector implements server-side functionality for budget auto-generation in OpenPetra's finance module. It provides methods to load budget data and generate budgets for the next financial year based on different forecast types and budget models. The class handles various budget calculation methods including ADHOC, INFLATE_BASE, SAME/SPLIT, and INFLATE_N, applying different formulas to calculate budget amounts based on historical data. Key methods include LoadBudgetForAutoGenerate, GenBudgetForNextYear, SetBudgetPeriodBaseAmount, and GetBudgetPeriodAmount, all working with budget data tables and requiring appropriate finance permissions.

 **Code Landmarks**
- `Line 56`: Permission system integration requiring FINANCE-1 module access for budget data loading
- `Line 93`: Comprehensive data validation ensuring ledger data exists before proceeding with operations
- `Line 158`: Budget generation logic with multiple calculation methods based on budget type codes
- `Line 185`: Complex period-based calculations for different budget forecasting approaches
- `Line 289`: Specialized inflation-based budget calculations with period change detection
### Budget/Budget.Consolidate.cs

TBudgetConsolidateWebConnector implements functionality for consolidating budgets in OpenPetra's financial system. It handles loading budget data for specified ledgers, posting budgets to general ledger master periods, and unposting budgets when needed. Key operations include clearing old budget values, calculating and applying budget amounts across accounting periods, and updating budget statuses. The class manages transactions with the database to ensure data integrity when consolidating budgets. Important methods include ConsolidateBudgets, LoadBudgetForConsolidate, PostBudget, UnPostBudget, and AddBudgetValue. The class works with BudgetTDS and GLPostingTDS datasets to manage budget and general ledger data.

 **Code Landmarks**
- `Line 76`: Custom SQL queries with parameterized statements to load budget data for current and next financial year
- `Line 179`: Transaction management pattern with delegate functions for database operations
- `Line 214`: Budget consolidation logic that handles both complete reconsolidation and selective updates
### Budget/Budget.Maintain.cs

TBudgetMaintainWebConnector implements server-side functionality for managing financial budgets in OpenPetra. It provides methods for loading, saving, importing, and exporting budget data. Key functionality includes retrieving budgets for specific years, importing budgets from CSV files with different budget types (SAME, SPLIT, INFLATE_BASE, INFLATE_N, ADHOC), validating budget data, and exporting budgets to CSV format. The class handles budget periods, budget revisions, and performs calculations based on budget types. Important methods include LoadAllBudgetsForExport, LoadBudgetsForYear, SaveBudget, ImportBudgets, and ExportBudgets, with helper methods for validation and data formatting.

 **Code Landmarks**
- `Line 75`: Custom SQL query with dynamic columns to load budget data with period amounts
- `Line 226`: Budget import implementation with transaction handling and detailed validation
- `Line 348`: GetBudgetYearNumber calculates financial year numbers based on ledger settings
- `Line 456`: ProcessBudgetTypeImportDetails handles different budget calculation methods based on type
- `Line 599`: Budget export formats data differently based on budget type (SAME, SPLIT, etc.)
### Common/Common.AccountingTool.cs

TCommonAccountingTool implements a class for creating accounting batches with journals and transactions that are ready to post. It manages the creation of base and foreign currency journals, handles transaction entries with proper currency conversions, and maintains accounting integrity through checksums. Key functionality includes batch initialization, journal creation, transaction addition with proper currency handling, and batch posting. Important methods include AddBaseCurrencyJournal(), AddForeignCurrencyJournal(), AddBaseCurrencyTransaction(), AddForeignCurrencyTransaction(), and CloseSaveAndPost(). The class validates accounting rules and maintains proper relationships between batches, journals, and transactions.

 **Code Landmarks**
- `Line 74`: Creates a new batch with proper initialization of the accounting data structure
- `Line 181`: Handles currency exchange rates when adding journals with different currencies
- `Line 268`: Maintains batch control totals by tracking journal debits and credits
- `Line 339`: Validates foreign currency transactions against account settings to prevent currency mismatches
- `Line 399`: Calculates international currency amounts using exchange rate tools
### Common/Common.Budget.Maintain.cs

TCommonBudgetMaintain implements static methods for budget-related operations in OpenPetra's finance module. The class provides functionality to retrieve GLM sequence numbers for budgets, get actual financial values considering forwarding periods, and calculate budget values across accounting periods. Key methods include GetGLMSequenceForBudget which retrieves sequence numbers by ledger and account information, GetActual which calculates actual financial values with support for different currencies and year-to-date options, and GetBudget which retrieves budget values across accounting periods. The class handles complex financial calculations including forwarding periods and different currency types (base, international, or transaction).

 **Code Landmarks**
- `Line 77`: GetActual method handles complex financial period calculations across fiscal years
- `Line 107`: Private GetActualInternal method contains core logic for retrieving actual amounts with currency selection
- `Line 214`: Logic for handling forwarding periods in income/expense accounts with special correction calculations
- `Line 288`: CalculateBudget helper function demonstrates period-based budget calculation with currency selection
### Common/Common.CrossLedger.cs

TCrossLedger implements server-side functionality for managing exchange rates between different currencies in OpenPetra's finance module. The class provides methods to load, clean, and manipulate daily exchange rate data used across ledgers. Key functionality includes DoDailyExchangeRateClean() which purges aged exchange rates, and LoadDailyExchangeRateData() which retrieves exchange rate information from multiple sources including journals, gift batches, and the daily exchange rate table. The code handles complex SQL queries to aggregate exchange rates, resolve primary key conflicts, and track usage of rates across the system. It maintains three tables: daily exchange rates, rate usage details, and corporate exchange rates, supporting OpenPetra's multi-currency financial operations.

 **Code Landmarks**
- `Line 67`: Implements automatic cleaning of aged exchange rates to prevent database bloat while preserving work-in-progress rates
- `Line 153`: Complex SQL query combines data from multiple financial tables to create a comprehensive view of exchange rates
- `Line 403`: Resolves potential primary key conflicts by adjusting time values when multiple rates exist for the same currency pair and date
- `Line 436`: Modifies related records in the usage table when adjusting time values to maintain referential integrity
### Common/Common.FinancialYear.cs

TFinancialYear implements server-side functionality for managing financial years and periods in OpenPetra. It provides methods to determine valid posting periods for dates, validate if dates fall within open accounting periods, and retrieve start/end dates for specific periods. Key functionality includes determining the financial period for a given date, fixing dates that fall outside valid posting periods, and validating if dates are within allowed posting periods. Important methods include GetLedgerDatePostingPeriod(), IsValidPostingPeriod(), GetStartAndEndDateOfPeriod(), and IsInValidPostingPeriod(). The class performs extensive validation of input parameters and handles exceptions for invalid ledger numbers or missing data.

 **Code Landmarks**
- `Line 57`: Comprehensive argument validation pattern used throughout the class to ensure data integrity
- `Line 81`: Handles complex date calculations for financial periods with forward posting capabilities
- `Line 179`: Date fixing logic that adjusts dates to fall within valid posting periods when requested
- `Line 229`: Handles year calculations to support multi-year operations in the financial system
### Common/Common.Import.cs

TCommonImport provides static methods for parsing and validating data from import files in OpenPetra's finance module. It implements functionality to convert CSV-formatted strings into various data types including strings, booleans, integers, decimals, and dates. Each import method handles data validation, error reporting, and supports default values. The class also includes helper methods for fixing account codes by padding with leading zeros when needed. Key methods include ImportString, ImportBoolean, ImportInt32, ImportInt64, ImportDecimal, ImportDate, and FixAccountCodes. These methods consistently handle delimiters, culture-specific formatting, and add validation messages to a collection when parsing errors occur.

 **Code Landmarks**
- `Line 73`: Handles special case for empty strings, allowing them to be treated as either empty text or null values
- `Line 177`: Implements robust decimal parsing that handles different number formats and thousands separators across cultures
- `Line 235`: Smart date parsing that can handle both formatted dates and integer date representations
- `Line 301`: Intelligent account code fixing that attempts to recover from Excel's leading zero removal
### Common/Common.PeriodEnd.cs

This file implements abstract classes for managing financial period-end operations in OpenPetra. TPeriodEndOperations serves as a base class for month-end and year-end processes, handling verification, error checking, and operation sequencing. AbstractPeriodEndOperation provides a framework for specific period-end tasks with methods for determining job size, running operations, and verifying completion. The file includes error handling mechanisms, verification result collection, and status tracking through enumerations like TPeriodEndErrorAndStatusCodes and TCarryForwardENum. Key methods include SetNextPeriod, RunPeriodEndCheck, RunPeriodEndSequence, GetJobSize, and RunOperation.

 **Code Landmarks**
- `Line 70`: WasCancelled property provides a cancellation mechanism for period-end operations, though implementation is currently stubbed
- `Line 103`: RunPeriodEndSequence method implements a verification pattern by running operations and then verifying their success
- `Line 144`: System verifies operation success by checking if any items remain to be processed after completion
- `Line 175`: DoExecuteableCode property implements a guard condition combining multiple state flags to control execution flow
### Common/Common.Posting.cs

TGLPosting implements core functionality for General Ledger batch posting in OpenPetra's financial system. It handles the entire posting workflow including validation, transaction processing, and summarization of financial data. Key functionality includes validating batch transactions, calculating account balances, updating general ledger master records, and managing batch statuses. The class provides methods for creating, posting, canceling, and reversing GL batches, as well as creating journals and transactions. Important methods include PostGLBatch, ReverseBatch, ValidateGLBatchAndTransactions, SummarizeDataSimple, and CreateATransaction. Helper classes TAmount and TAccountTreeElement support the posting process by tracking financial amounts and account hierarchies.

 **Code Landmarks**
- `Line 1006`: SummarizeDataSimple method replaces a more complex summarization approach for better performance, focusing only on posting levels
- `Line 1073`: The code maintains both simple and complex summarization methods, with comments noting the performance benefits of the simpler approach
- `Line 402`: Contains logic to detect and correct small currency rounding discrepancies during transaction posting
- `Line 1382`: Implements batch reversal functionality that creates mirror transactions with opposite debit/credit indicators
- `Line 1214`: Uses transaction-based approach to ensure database consistency during posting operations
### Common/Common.Tools.Accounts.cs

This file implements utility classes for OpenPetra's financial account management system. TGetAccountHierarchyDetailInfo manages account hierarchies, providing methods to retrieve parent-child relationships between accounts. THandleAccountPropertyInfo handles special account codes defined in the TAccountPropertyEnum enumeration, retrieving account codes from database properties or configuration settings. TAccountInfo provides access to account details like type, currency settings, and posting status. TAccountPeriodInfo manages accounting period data, including start and end dates. Together, these classes form the foundation for account navigation, special account identification, and period management in the financial module.

 **Code Landmarks**
- `Line 73`: Implements hierarchical account navigation with configurable depth traversal
- `Line 173`: Fallback mechanism for special accounts using configuration parameters when database properties aren't available
- `Line 353`: Account selection system with both direct code access and sequential traversal capabilities
### Common/Common.Tools.ExchangeRates.cs

This file implements currency management and exchange rate functionality for OpenPetra's financial module. The TCurrencyInfo class manages currency information by loading the complete currency table and providing methods for currency conversion and proper rounding based on currency-specific digit requirements. The FormatConverter class parses display format strings to determine decimal places. TExchangeRateTools offers static methods to retrieve daily and corporate exchange rates from the database with various filtering options. TCorporateExchangeRateCache implements a caching mechanism to optimize database access when repeatedly requesting corporate exchange rates. The file provides comprehensive currency handling capabilities including conversion between currencies, proper rounding, and exchange rate retrieval with date constraints.

 **Code Landmarks**
- `Line 72`: TCurrencyInfo implements a database-efficient approach by loading the entire currency table once and then switching between currencies without additional queries
- `Line 196`: The RoundBaseCurrencyValue and RoundForeignCurrencyValue methods ensure proper decimal rounding based on currency-specific digit requirements
- `Line 258`: FormatConverter uses regex to parse legacy Petra format strings to determine decimal places for currencies
- `Line 380`: GetDailyExchangeRate includes parameters to control how old an exchange rate can be and whether unique rates are required
- `Line 467`: GetCorporateExchangeRate intelligently tries both currency direction combinations when searching for exchange rates
### Common/Common.Tools.GLM.cs

This file implements three utility classes for accessing General Ledger Master (GLM) data in OpenPetra's financial module. TGlmpInfo handles General Ledger Master Period information, allowing loading by sequence, period, cost center, account, and year. TGet_GLM_Info provides read-only access to GLM data with methods to check existence and retrieve financial values like YtdActual and YtdForeign. TGlmInfo offers an iterator-based approach to GLM data, with methods to navigate through records and access account information, cost centers, and financial values. All classes support optional database connection parameters for transaction management.

 **Code Landmarks**
- `Line 167`: Constructor has a warning about not returning useful values because Year isn't specified, only used in tests
- `Line 196`: GLMExists property provides a safety check before accessing potentially non-existent data
- `Line 211`: Financial methods include null checks and default values to prevent errors
- `Line 302`: Iterator pattern implementation for navigating through GLM records
### Common/Common.Tools.Ledger.cs

This file implements two key classes for OpenPetra's financial management system. TLedgerInfo retrieves and manages ledger data including currency settings, accounting periods, and bank accounts. It provides methods to access ledger properties like base currency, revaluation accounts, and year-end flags. TLedgerInitFlag manages persistent flags for ledgers, allowing the system to track initialization states and store configuration values. The file supports both simple boolean flags and more complex string-based values with component management capabilities, enabling the financial system to maintain state across operations.

 **Code Landmarks**
- `Line 75`: Dictionary caching of ledger data improves performance by avoiding repeated database queries
- `Line 217`: GetLedgerBaseCurrency method uses dictionary lookup pattern for efficient currency code retrieval
- `Line 425`: Implements fallback logic to find default bank accounts through multiple strategies
- `Line 579`: CSV-based component storage in flags provides a simple but effective way to store multiple values
### Common/PartnerCostCentreLink.cs

PartnerCostCentreLink.cs implements a Common class in the MFinance module that handles business logic for financial data operations. The file focuses on partner-cost centre relationships, providing functionality to check if a partner (such as a worker) is linked to a specific cost centre. The HasPartnerCostCentreLink method queries the database to determine if a partner has an associated cost centre, returning both a boolean result and the actual cost centre code if found. The method uses database transactions to ensure data consistency while performing the query against the AValidLedgerNumber table.

 **Code Landmarks**
- `Line 54`: Uses a StringCollection to specify required database columns for optimized data retrieval
- `Line 59`: Implements proper transaction management with isolation level specification and commit handling
- `Line 76`: Includes detailed transaction logging at level 7 for debugging purposes
### Common/ServerLookups.cs

TFinanceServerLookupWebConnector implements server-side lookup functionality for the MFinance module. It provides methods to retrieve financial data such as current period dates, posting range dates, suspense account information, foreign currency data, and partner keys associated with cost centers. The class includes type-ahead search functionality for account codes, cost center codes, and motivation groups/details to support UI autocomplete features. Key methods include GetCurrentPeriodDates, GetCurrentPostingRangeDates, GetLedgerBaseCurrency, GetForeignCurrencyAccountActuals, and various TypeAhead methods. All methods require appropriate module permissions and handle database connections and transactions.

 **Code Landmarks**
- `Line 63`: Database transaction pattern with delegate for encapsulating database operations
- `Line 107`: Comprehensive argument validation with custom exceptions for finance-specific errors
- `Line 269`: Optional database parameter allows reusing existing connections
- `Line 348`: SQL file-based queries with parameterization for type-ahead functionality
### GL/GL.AccountingPeriods.cs

TAccountingPeriodsWebConnector implements server-side methods for managing accounting periods in the OpenPetra financial system. It provides functionality to retrieve current period dates, posting range dates, period information for specific dates, and available financial years and periods. Key methods include GetCurrentPeriodDates, GetRealPeriod, GetPeriodStartDate, GetPeriodEndDate, GetAccountingYearByDate, GetAccountingYearPeriodByDate, GetAvailableGLYears, GetAvailablePeriods, and GetFirstDayOfAccountingPeriod. The class handles date calculations across financial years, supports different financial year configurations, and provides data for UI components like year/period selection dropdowns. All methods require the FINANCE-1 permission for access.

 **Code Landmarks**
- `Line 117`: Implements caching for financial data through TCacheable to improve performance when retrieving accounting period information
- `Line 170`: GetRealPeriod handles complex financial year calculations to support organizations with different financial year configurations
- `Line 359`: Uses database transactions to ensure data consistency when retrieving financial period information
- `Line 426`: FindFinancialYearByDate algorithm handles year determination by finding earliest start date rather than assuming primary key order
- `Line 602`: DecrementYear method accounts for leap years when calculating previous year dates
### GL/GL.Exporting.cs

TGLExporting implements functionality for exporting General Ledger batch data to CSV format. It provides methods to export batch information, journal entries, and transactions with various formatting options. The class supports summarizing transactions by account and cost center, handling different currencies, and including analysis attributes. Key functionality includes formatting batch lines, journal lines, transaction lines, and handling date/currency formatting. The file defines three classes: TGLExporting (main export functionality), AJournalSummaryRow (for summarizing journals), and ATransactionSummaryRow (for summarizing transactions). Important methods include ExportAllGLBatchData and various Write* methods for formatting output lines.

 **Code Landmarks**
- `Line 84`: Uses TProgressTracker to provide real-time progress updates to client during potentially lengthy export operations
- `Line 147`: Implements special handling for accounts that shouldn't be summarized with a unique key generation approach
- `Line 243`: Uses a sophisticated approach to handle analysis attributes with a configurable maximum number of values
### GL/GL.Importing.cs

TGLImporting implements functionality for importing General Ledger batches and transactions into OpenPetra's financial system. The file provides two main methods: ImportGLBatches for importing complete GL batches with journals and transactions from CSV data, and ImportGLTransactions for adding transactions to existing journals. It handles data parsing, validation, exchange rate management, and database operations. The class processes CSV data with configurable delimiters, validates account codes, cost centers, and analysis attributes, manages transaction amounts in multiple currencies, and updates batch totals. Key functionality includes parsing CSV files, validating financial data against ledger rules, handling exchange rates, and maintaining data integrity through transaction management.

 **Code Landmarks**
- `Line 72`: UpdateDailyExchangeRateTable dynamically adds exchange rates to the system when importing transactions with foreign currencies
- `Line 306`: Implements batch validation with both standard validation and additional manual validation checks
- `Line 1017`: ImportGLTransactionsInner handles the core transaction parsing logic shared between batch and transaction imports
- `Line 1223`: MakeFriendlyFKExceptions translates database foreign key errors into user-friendly messages
### GL/GL.PeriodEnd.Month.cs

GL.PeriodEnd.Month.cs implements the month-end closing process for OpenPetra's financial ledger system. The TPeriodIntervalConnector class provides the public API method PeriodMonthEnd that clients call to execute month-end operations. The TMonthEnd class handles the core functionality, performing validation checks through RunMonthEndChecks to verify no unposted batches exist, suspense accounts are properly managed, and foreign currency accounts are revalued. The process advances the current period, generates required batches, and updates the ledger status. Supporting classes include GetUnpostedGiftInfo, GetSuspenseAccountInfo, and GetBatchInfo to validate financial data integrity before closing the period.

 **Code Landmarks**
- `Line 71`: PeriodMonthEnd is the main entry point with permission requirement 'FINANCE-2' for month-end operations
- `Line 173`: StewardshipCalculationDelegate uses dependency injection to allow customization of stewardship calculation logic
- `Line 247`: NoteForexRevalRequired marks foreign accounts with non-zero balances for revaluation in the next period
- `Line 304`: Month-end validation includes checking for unposted batches, suspense accounts, and required revaluations
- `Line 456`: Year-end requires suspense accounts to have zero balances, enforced with critical verification errors
### GL/GL.PeriodEnd.Month.cs

GL.PeriodEnd.Month.cs implements the month-end closing process for OpenPetra's financial ledger system. The TPeriodIntervalConnector class provides the public API method PeriodMonthEnd that clients call to execute month-end operations. The TMonthEnd class handles the core functionality, performing validation checks through RunMonthEndChecks to verify no unposted batches exist, suspense accounts are properly managed, and foreign currency accounts are revalued. The process advances the current period, generates required batches, and updates the ledger status. Supporting classes include GetUnpostedGiftInfo, GetSuspenseAccountInfo, and GetBatchInfo to validate financial data integrity before closing the period.

 **Code Landmarks**
- `Line 71`: PeriodMonthEnd is the main entry point with permission requirement 'FINANCE-2' for month-end operations
- `Line 173`: StewardshipCalculationDelegate uses dependency injection to allow customization of stewardship calculation logic
- `Line 247`: NoteForexRevalRequired marks foreign accounts with non-zero balances for revaluation in the next period
- `Line 304`: Month-end validation includes checking for unposted batches, suspense accounts, and required revaluations
- `Line 456`: Year-end requires suspense accounts to have zero balances, enforced with critical verification errors
### GL/GL.PeriodEnd.Year.cs

GL.PeriodEnd.Year.cs implements the year-end financial processing functionality for OpenPetra's general ledger module. The file contains several classes that work together to perform year-end operations: TPeriodIntervalConnector provides web-facing methods to execute or cancel year-end operations; TYearEnd orchestrates the entire year-end process; and supporting classes handle specific tasks like TReallocation (reallocating income/expense accounts), TGlmNewYearInit (initializing next year's general ledger), TAccountPeriodToNewYear (updating accounting periods), TResetForwardPeriodBatches and TResetForwardPeriodICH (adjusting forward-posted transactions). The year-end process includes archiving old data, reallocating income/expense accounts to equity accounts, initializing the database for the next year, and rebasing forward-posted transactions.

 **Code Landmarks**
- `Line 156`: The TYearEnd.RunYearEnd method orchestrates the entire year-end process through a sequence of operations in a database transaction
- `Line 276`: TReallocation.RunOperation implements complex business logic for reallocating income/expense accounts based on cost center rollup styles
- `Line 521`: TGlmNewYearInit.RunOperation carefully transfers financial data from the old year to the new year while maintaining accounting integrity
- `Line 694`: Forward-posted transactions are rebased to appear in the correct periods of the new financial year
### GL/GL.PeriodEnd.Year.cs

GL.PeriodEnd.Year.cs implements the year-end financial processing functionality for OpenPetra's general ledger module. The file contains several classes that work together to perform year-end operations: TPeriodIntervalConnector provides web-facing methods to execute or cancel year-end operations; TYearEnd orchestrates the entire year-end process; and supporting classes handle specific tasks like TReallocation (reallocating income/expense accounts), TGlmNewYearInit (initializing next year's general ledger), TAccountPeriodToNewYear (updating accounting periods), TResetForwardPeriodBatches and TResetForwardPeriodICH (adjusting forward-posted transactions). The year-end process includes archiving old data, reallocating income/expense accounts to equity accounts, initializing the database for the next year, and rebasing forward-posted transactions.

 **Code Landmarks**
- `Line 156`: The TYearEnd.RunYearEnd method orchestrates the entire year-end process through a sequence of operations in a database transaction
- `Line 276`: TReallocation.RunOperation implements complex business logic for reallocating income/expense accounts based on cost center rollup styles
- `Line 521`: TGlmNewYearInit.RunOperation carefully transfers financial data from the old year to the new year while maintaining accounting integrity
- `Line 694`: Forward-posted transactions are rebased to appear in the correct periods of the new financial year
### GL/GL.Revaluation.cs

This file implements currency revaluation functionality for OpenPetra's financial system. It contains two main classes: TRevaluationWebConnector (public interface) and CLSRevaluation (implementation). The code creates accounting batches with transactions to adjust foreign currency accounts based on new exchange rates. Key functionality includes calculating value differences, generating appropriate debit/credit transactions, and posting the revaluation batch. Important methods include Revaluate(), RunRevaluation(), RevaluateAccount(), and AccountDelta(). The implementation handles multiple foreign accounts simultaneously and maintains proper accounting balance by creating offsetting entries.

 **Code Landmarks**
- `Line 181`: AccountDelta calculates the difference between current and revalued amounts, which determines if revaluation is needed
- `Line 233`: Creates paired transactions to maintain accounting balance - one for the foreign account and one for the revaluation account
- `Line 245`: Initializes batch and journal with specific transaction type REVAL and accounting subsystem GL
- `Line 319`: Makes AccountDelta public to enable unit testing of the calculation logic
### GL/GL.TransactionFind.cs

TGLTransactionFind implements server-side functionality for the GL Transaction Find screen in OpenPetra's financial module. It provides methods to search for financial transactions based on various criteria such as ledger number, batch number, transaction status, descriptions, dates, and amounts. The class uses a paged data set approach to efficiently handle large result sets, executing queries asynchronously in a separate thread. Key methods include PerformSearch() to initiate queries based on criteria, GetDataPagedResult() to retrieve paginated results, and BuildCustomWhereCriteria() to construct SQL WHERE clauses from search parameters.

 **Code Landmarks**
- `Line 67`: Uses TPagedDataSet for efficient handling of large query results with pagination
- `Line 106`: Implements asynchronous database querying using a dedicated thread for better UI responsiveness
- `Line 174`: Dynamic SQL query construction with parameter binding for secure database access
- `Line 182`: Uses TDynamicSearchHelper to build complex WHERE clauses from user criteria
### GL/GL.Transactions.cs

TGLTransactionWebConnector implements server-side functionality for OpenPetra's General Ledger (GL) module, handling batch creation, loading, saving, and manipulation of financial transactions. It provides methods for creating, loading, and managing GL batches, journals, and transactions, with support for recurring transactions. Key features include batch validation, transaction posting, batch reversal, exchange rate calculations, and import/export capabilities. The class enforces data integrity through extensive validation and provides transaction analysis functionality. Important methods include CreateABatch, LoadABatch, SaveGLBatchTDS, PostGLBatch, and ReverseBatch.

 **Code Landmarks**
- `Line 1271`: Implements cascading delete functionality for recurring GL batches to ensure data integrity across multiple related tables
- `Line 1054`: Validation logic for transaction account and cost center codes ensures financial data integrity before saving
- `Line 2245`: Complex SQL queries validate journal numbering sequence integrity in recurring batches
- `Line 2391`: Implements transaction renumbering after deletion to maintain continuous sequence
- `Line 1767`: Supports batch reversal with automatic posting option for streamlined correction workflows
### GL/Reporting.APReports.UIConnectors.cs

TFinanceReportingWebConnector implements server-side methods for generating Accounts Payable reports in OpenPetra. The class provides four key reporting functions: APAgedSupplierList for aging analysis of supplier accounts, APCurrentPayable for current payable documents, APPaymentReport for payment history, and APAccountDetail for GL account transactions related to AP. Each method accepts parameters like ledger number and date ranges, executes database queries within transactions, and returns DataSets or DataTables with formatted report data. The methods handle currency conversions, calculate aging periods, and format data for client-side reporting consumption.

 **Code Landmarks**
- `Line 64`: Complex SQL query joins multiple tables to retrieve AP document data with partner information
- `Line 114`: Date-based aging calculation logic that categorizes payables into different aging buckets
- `Line 331`: Sophisticated reference number formatting that extracts and transforms AP document references
### GL/Reporting.APReports.UIConnectors.cs

TFinanceReportingWebConnector implements server-side methods for generating Accounts Payable reports in OpenPetra. The class provides four key reporting functions: APAgedSupplierList for aging analysis of supplier accounts, APCurrentPayable for current payable documents, APPaymentReport for payment history, and APAccountDetail for GL account transactions related to AP. Each method accepts parameters like ledger number and date ranges, executes database queries within transactions, and returns DataSets or DataTables with formatted report data. The methods handle currency conversions, calculate aging periods, and format data for client-side reporting consumption.

 **Code Landmarks**
- `Line 64`: Complex SQL query joins multiple tables to retrieve AP document data with partner information
- `Line 114`: Date-based aging calculation logic that categorizes payables into different aging buckets
- `Line 331`: Sophisticated reference number formatting that extracts and transforms AP document references
### GL/Reporting.FDReports.UIConnectors.cs

TFinanceReportingWebConnector implements server-side methods for generating financial development reports in OpenPetra. The file contains several specialized reporting functions that query the database for donor and gift information. Key functionality includes SYBUNTTable for tracking donors who gave in one period but not another, DonorReportShort and DonorReportDetail for basic and detailed donor information, GiftsOverMinimum for identifying donors whose gifts exceed a threshold, TopDonorReport for analyzing top/middle/bottom donors by percentage contribution, and NewDonorReport for tracking first-time donors. Each method processes parameters like date ranges, minimum amounts, and motivation codes to generate DataTables or DataSets containing donor information, gift details, and addresses for client-side reporting.

 **Code Landmarks**
- `Line 50`: Uses NoRemoting attribute to indicate methods are only available for internal server calls, not client remoting
- `Line 124`: Complex SQL query with Common Table Expressions (WITH clause) for efficient data aggregation
- `Line 265`: Uses TAddressTools.GetBestAddressForPartners to efficiently retrieve address data for multiple partners in a single operation
- `Line 418`: Implements percentage-based donor segmentation with cumulative totals calculation in SQL
- `Line 597`: Uses LINQ GroupBy to identify and consolidate duplicate donor entries when grouping is requested
### GL/Reporting.GiftReports.UIConnectors.cs

TFinanceReportingWebConnector implements server-side methods for gift reporting in OpenPetra's financial system. It provides data access for various gift reports including gift batch details, gift statements for donors and recipients, motivation responses, and monthly giving summaries. Key functionality includes retrieving gift transaction data with donor and recipient information, calculating gift totals by period, handling tax deduction percentages, and supporting different report formats (detailed, brief, totals). The class connects to the database through a reporting adapter and processes parameters to filter results by date range, ledger, motivation codes, and other criteria. Important methods include GiftBatchDetailTable, GiftStatementRecipientTable, GiftStatementDonorTable, and MotivationResponse.

 **Code Landmarks**
- `Line 89`: Implements database access with transaction handling pattern for safe data retrieval
- `Line 143`: Uses complex SQL case statements to handle restricted gifts based on user permissions
- `Line 300`: Implements distinct row filtering in C# rather than SQL for performance optimization
- `Line 490`: Caches donor addresses in memory to avoid redundant database lookups
- `Line 632`: Uses SQL CASE expressions to calculate monthly gift totals in a single query
### GL/Reporting.UIConnectors.cs

TFinanceReportingWebConnector implements server-side functionality for generating financial reports in OpenPetra. It provides methods to retrieve ledger details, financial years, cost centers, and account hierarchies. The class contains numerous methods for generating different financial reports including balance sheets, income/expense statements, gift summaries, trial balances, and executive summaries. Key functionality includes retrieving period balances, calculating account hierarchies, summarizing financial data across periods, and formatting data for client-side reporting. The connector handles different currency options and supports various filtering mechanisms for customized reports. Important methods include GetLedgerPeriodDetails, GetReportingCostCentres, GetReportingAccounts, BalanceSheetTable, IncomeExpenseTable, and various specialized report generators.

 **Code Landmarks**
- `Line 1069`: AddTotalsToParentAccountRow uses recursion to build account hierarchies, updating parent accounts with child values
- `Line 1651`: GetActualsAndBudget method calculates financial metrics with complex SQL queries that handle different accounting periods
- `Line 1389`: IncomeExpenseTable method builds complex financial reports with support for cost center breakdowns and period-by-period analysis
- `Line 2078`: TotalGiftsThroughField processes gift data with period-by-period tracking and tax deduction handling
- `Line 2385`: AccountAndCostCentreFilters method builds dynamic SQL filters based on user parameters
### Gift/Gift.Adjustment.cs

TAdjustmentWebConnector implements server-side functionality for adjusting and reversing gifts in OpenPetra's finance system. It provides methods for retrieving gifts that need adjustment, checking if gifts have been previously reversed, and performing various gift adjustment operations. Key functionality includes GetGiftsForReverseAdjust for retrieving gift data needed for reversal or adjustment, GetGiftsForFieldChangeAdjustment for field-specific adjustments, CheckGiftsNotPreviouslyReversed to validate gift status, ReversedGiftReset to reset reversal flags, and GiftRevertAdjust to perform the actual adjustment operations. The class handles various adjustment types including gift reversals, batch reversals, field adjustments, and tax deductible percentage changes. It manages the creation of new gift batches when needed and handles the duplication of gift details with appropriate modifications based on the adjustment type.

 **Code Landmarks**
- `Line 73`: Implements permission-based security through RequireModulePermission attribute requiring FINANCE-1 permission for all public methods
- `Line 290`: Uses database transactions to ensure data integrity when performing gift adjustments
- `Line 387`: Implements a private helper method to create new gift batches based on existing ones
- `Line 435`: AddDuplicateGiftDetailToGift handles complex logic for creating reversed or adjusted gift details with proper accounting
### Gift/Gift.Batch.cs

TGiftBatchFunctions implements server-side functionality for creating gift batches in OpenPetra's financial system. It provides methods to create new gift batches with consecutive batch numbers in a ledger. The class offers two key methods: CreateANewGiftBatchRow for standard gift batches and CreateANewRecurringGiftBatchRow for recurring gift batches. Both methods handle validation, set default values for batch properties, and update the ledger's batch counters. The functions manage batch dates, periods, currency settings, and bank account information while ensuring proper validation of input parameters.

 **Code Landmarks**
- `Line 63`: Creates gift batch rows with validation and automatic batch numbering from ledger
- `Line 81`: Handles date validation to ensure effective dates fit within open financial periods
- `Line 152`: Creates recurring gift batches with similar validation but different numbering sequence
### Gift/Gift.Exporting.SEPA.cs

TGiftExportingSEPA implements functionality for exporting recurring gift batches to SEPA Direct Debit files. The class provides methods to convert gift information into standardized SEPA XML format for direct debit collection. Key functionality includes retrieving system defaults for creditor information, validating required SEPA settings, processing recurring gift details, calculating sequence types, generating payment references, and retrieving donor banking details. The main method ExportRecurringGiftBatch handles the entire export process, including transaction management and error handling. Important methods include ProcessPmtInf, CalculateSequenceType, GenerateReference, and GetBankingDetailsOfPartner.

 **Code Landmarks**
- `Line 62`: Uses base64 encoding for the final XML document to ensure safe transmission of the SEPA file content
- `Line 145`: Implements sequence type determination (OOFF for one-time payments, RCUR for recurring) based on gift start/end dates
- `Line 178`: Generates payment references from motivation details with intelligent truncation to stay within SEPA's 140 character limit
- `Line 235`: Retrieves banking details from partner records with support for multiple accounts and main account identification
- `Line 285`: Validates SEPA mandate reference and date before adding payments to the SEPA file
### Gift/Gift.Exporting.cs

TGiftExporting implements functionality for exporting gift batch data from OpenPetra to Excel format. The class provides methods to export gift batches with various filtering options including date ranges, batch numbers, and transaction status. Key features include support for summarized or detailed exports, currency conversion options, confidential gift handling, and customizable formatting. The export process retrieves gift batch records, gift records, and gift details from the database, formats them according to user preferences, and outputs them as CSV data that gets converted to Excel. Important methods include ExportAllGiftBatchData which handles the main export logic, and various helper methods for writing different data types to the output stream. The class also includes AGiftSummaryRow to support gift summarization functionality.

 **Code Landmarks**
- `Line 71`: Uses StringWriter to build CSV data that later gets converted to Excel format
- `Line 148`: Implements complex SQL filtering with multiple optional parameters for flexible data selection
- `Line 248`: Uses SortedDictionary for efficient summarization of gift data by multiple key attributes
- `Line 346`: Special handling for confidential gifts with security restrictions
- `Line 398`: Implements custom formatting for different data types (currency, dates, etc.) in CSV output
### Gift/Gift.GiftDetailFind.cs

TGiftDetailFind implements a search functionality for gift details in OpenPetra's financial module. It provides methods to search gift transactions based on various criteria like ledger number, batch number, transaction number, receipt number, motivation codes, comments, donor/recipient partners, dates, and amounts. The class uses a paged data set approach to handle potentially large result sets, executing searches asynchronously in a separate thread. Key methods include PerformSearch() to execute queries based on criteria, GetDataPagedResult() to retrieve paginated results, and BuildCustomWhereCriteria() to construct SQL WHERE clauses from search parameters.

 **Code Landmarks**
- `Line 71`: Uses TPagedDataSet for handling large result sets with pagination
- `Line 145`: Executes search query in a separate thread for asynchronous operation
- `Line 181`: Dynamically adds a 'BatchPosted' column to results for UI display purposes
- `Line 204`: Uses TDynamicSearchHelper to build SQL WHERE clauses from various criteria types
### Gift/Gift.Importing.cs

TGiftImporting implements functionality for importing gift batches and transactions into OpenPetra's financial system. The class handles parsing and validating CSV data files containing gift batch information and gift transactions. Key functionality includes importing complete gift batches with header and transaction data, importing transactions into existing batches, handling ESR payment format (Swiss payment system), and managing exchange rates. The class validates imported data against database constraints, performs business rule validation, and handles data transformations. Important methods include ImportGiftBatches(), ImportGiftTransactions(), ParseBatchLine(), ParseTransactionLine(), and ParseEsrTransactionLine(). The class maintains state through FMainDS (dataset), FLedgerNumber, and culture-specific formatting information.

 **Code Landmarks**
- `Line 90`: UpdateDailyExchangeRateTable method dynamically manages exchange rates during import
- `Line 214`: ImportGiftBatches method handles complete gift batch imports with validation and database transactions
- `Line 1032`: ParseEsrTransactionLine handles specialized Swiss payment format (ESR) imports
- `Line 1186`: MakeFriendlyFKExceptions converts database foreign key errors into user-friendly messages
- `Line 882`: GetEsrDefaults method creates and manages a custom table for ESR payment defaults
### Gift/Gift.Receipting.cs

TReceiptingWebConnector implements functionality for creating and managing gift receipts in OpenPetra's finance system. It provides methods for generating annual gift receipts in PDF and HTML formats, handling individual receipts, and managing receipt templates. Key features include creating receipts based on donor information, formatting receipt letters with gift details, supporting email delivery, handling tax-deductible amounts, and managing receipt printing status. The class works with donor data, gift transactions, and receipt templates stored in the database, supporting customization through HTML templates with logo and signature images.

 **Code Landmarks**
- `Line 72`: Main method for creating annual gift receipts with support for multiple output formats and delivery methods
- `Line 372`: Conditional section replacement in HTML templates using #if/#endif syntax for dynamic content generation
- `Line 408`: Complex receipt formatting logic handling different gift types, tax deductions, and currency formatting
- `Line 1000`: Template storage system for HTML, logos and signatures using the PForm table
### Gift/Gift.Setup.cs

TGiftSetupWebConnector implements server-side functionality for managing gift motivation groups and details in OpenPetra's finance system. It provides methods to load, create, update, and delete motivation groups and details, as well as manage default motivation settings. Key methods include LoadMotivationDetails for retrieving motivation data, MaintainMotivationGroups and MaintainMotivationDetails for CRUD operations, and LoadDefaultMotivation/SetDefaultMotivationDetail for managing default motivation settings. The class uses database transactions to ensure data integrity and implements permission checks through RequireModulePermission attributes.

 **Code Landmarks**
- `Line 47`: RequireModulePermission attributes enforce security by requiring specific permissions to access methods
- `Line 52`: Uses transaction pattern with delegates for database operations to ensure data integrity
- `Line 92`: Implements CRUD operations for motivation groups with comprehensive error handling
- `Line 182`: Maintains relationships between motivation details and their associated fees when deleting records
### Gift/Gift.TaxDeductiblePct.cs

TTaxDeductibleWebConnector implements functionality to manage tax deductibility percentages for gift recipients in the OpenPetra financial system. It provides methods to check if a partner is receiving gifts with different tax deductible percentages, update tax deductible percentages for unposted gifts, and retrieve gifts that need tax deductible percentage adjustments. Key methods include IsPartnerARecipient, which checks if a partner has received gifts with different tax percentages; UpdateUnpostedGiftsTaxDeductiblePct, which updates percentages for unposted gifts; and GetGiftsForTaxDeductiblePctAdjustment, which retrieves posted gifts needing adjustment. The class interacts with database tables for gift details, gift batches, and partners.

 **Code Landmarks**
- `Line 54`: Uses SQL query with conditional aggregation to count different types of gifts (posted vs unposted) in a single database call
- `Line 106`: Implements transaction management pattern with delegate for database operations
- `Line 125`: Updates tax deductibility amounts for each gift detail row using a helper method
- `Line 169`: Joins multiple tables to retrieve comprehensive gift information including donor names
### Gift/Gift.Transactions.Validation.cs

TGiftTransactionWebConnector implements validation methods for gift transactions in OpenPetra's finance module. The file contains four static partial methods that validate different aspects of gift transactions: ValidateGiftBatchManual, ValidateGiftDetailManual, ValidateRecurringGiftBatchManual, and ValidateRecurringGiftDetailManual. Each method iterates through rows in a submitted data table, calling appropriate validation functions from TFinanceValidation_Gift class. For gift details, it also retrieves recipient partner information. All methods collect validation results in a TVerificationResultCollection parameter that's passed by reference.

 **Code Landmarks**
- `Line 53`: Retrieves partner information to populate the RecipientClass field before validation
- `Line 33`: Uses partial methods pattern allowing implementation in separate files without modifying the base class
### Gift/Gift.Transactions.cs

TGiftTransactionWebConnector implements server-side functionality for managing gift transactions in OpenPetra's finance module. It provides methods for creating, loading, modifying, and posting gift batches and recurring gifts. Key functionality includes batch creation, transaction management, recipient validation, cost center determination, fee calculation, and GL batch generation for posting. The class handles both regular and recurring gifts, supports importing/exporting gift data, and provides partner-related lookups for gift processing. Important methods include CreateAGiftBatch, LoadGiftTransactionsForBatch, PostGiftBatch, and RetrieveCostCentreCodeForRecipient.

 **Code Landmarks**
- `Line 1066`: PrepareGiftBatchForPosting performs comprehensive validation of gift batches before posting, checking batch status, periods, exchange rates and recipient validity
- `Line 1369`: CreateGLBatchAndTransactionsForPostingGifts converts gift transactions into corresponding GL transactions for accounting
- `Line 1576`: PostGiftBatches handles the complex transaction of posting multiple gift batches with proper validation and GL batch generation
- `Line 1030`: LoadGiftDonorRelatedData efficiently loads all donor information needed for gift processing in a single database operation
- `Line 2310`: GetRecipientFundNumberInner implements recursive logic to determine the appropriate ledger number for gift recipients
### Gift/Gift.cs

TGift implements business functions for the Gift subsystem of OpenPetra's Finance module. It provides methods to retrieve and process gift information, particularly focusing on retrieving details about a donor's last gift. Key functionality includes retrieving gift details with proper security checks for restricted or confidential gifts, calculating tax deductibility amounts, and formatting gift information for display. The class enforces security by checking user permissions against restricted gifts and handles split gifts differently from regular gifts. Important methods include GetLastGiftDetails(), GiftRestricted(), and SetDefaultTaxDeductibilityData(), which manage gift access permissions and tax calculations.

 **Code Landmarks**
- `Line 67`: Implements security check for restricted gifts by verifying user group membership
- `Line 189`: Handles split gifts differently from regular gifts with special display formatting
- `Line 380`: Implements comprehensive security model for confidential gifts requiring specific group membership
- `Line 460`: Reusable method to determine if a gift is restricted based on user permissions
- `Line 504`: Calculates tax deductibility amounts based on recipient configuration and motivation details
### Gift/Gift.gui.tools.cs

TGuiTools implements a server-side utility class for the OpenPetra finance system that validates gift recipient partner keys and determines appropriate motivation codes. The primary functionality is determining whether a partner key is valid and selecting the correct motivation group and detail codes based on partner type. The class contains one main static method, GetMotivationGroupAndDetailForPartner, which checks if a partner exists, determines if it's a unit type partner, and assigns appropriate motivation codes based on unit type (KEYMIN, field, etc.) or partner-specific motivation details.

 **Code Landmarks**
- `Line 73`: Implements a complex business rule for determining motivation codes based on partner types
- `Line 94`: Uses database transactions to ensure data consistency during partner validation
- `Line 125`: Prioritizes partner-specific motivation details over default type-based assignments
### ICH/ICH.GenHOSAFilesReports.cs

TGenHOSAFilesReportsWebConnector implements functionality to generate Home Office Statement of Accounts (HOSA) files and reports for ICH transactions. It processes financial data from OpenPetra's accounting system, focusing on transactions between different cost centers and ledgers. The class provides methods to export financial data in a specific format for reporting purposes. Key functionality includes generating HOSA files with transaction details, exporting gift transactions, and formatting account information. Important methods include GenerateHOSAFiles(), ExportGifts(), ReplaceHeaderInFile(), and ConvertAccount(). The class handles both base and international currency transactions, and supports filtering by ICH number, ledger, period, and cost center.

 **Code Landmarks**
- `Line 73`: Main entry point that generates HOSA files with comprehensive transaction data filtering
- `Line 304`: Custom file header replacement function for adding metadata to exported CSV files
- `Line 352`: Gift export function with complex SQL query that handles both base and international currency amounts
- `Line 614`: Account code conversion function that maps specific account codes to standardized formats
- `Line 631`: Commented out incomplete GenerateHOSAReports method showing planned but unimplemented functionality
### ICH/StewardshipCalculation.cs

TStewardshipCalculationWebConnector implements the International Clearing House (ICH) Stewardship Calculation process for OpenPetra's financial system. It provides functionality to generate admin fee batches and ICH stewardship batches for financial transactions. The class processes transactions across cost centers, calculates fees, and creates appropriate journal entries to balance accounts. Key methods include PerformStewardshipCalculation, GenerateAdminFeeBatch, GenerateICHStewardshipBatch, and BuildChildAccountList. The implementation handles both ICH and non-ICH funds, creates transaction records, updates gift details with ICH numbers, and maintains stewardship records for reporting purposes.

 **Code Landmarks**
- `Line 73`: Main entry point for stewardship calculation that coordinates the generation of admin fee and ICH stewardship batches
- `Line 151`: GenerateAdminFeeBatch creates GL transactions for admin fees charged in the specified period
- `Line 409`: GenerateICHStewardshipBatch processes foreign cost centers and creates balancing transactions for ICH settlement
- `Line 657`: BuildChildAccountList recursively builds a CSV list of account codes using account hierarchy relationships
- `Line 693`: SelectedPeriodRequiresStewardshipRun checks if foreign transactions exist that need stewardship processing
### connect/GL.GLTransactionFind.cs

TGLTransactionFindUIConnector implements a UI connector for the GL Transaction Find screen in OpenPetra's finance module. It serves as an intermediary between the client UI and server-side business logic, handling data retrieval requests. The class encapsulates a TGLTransactionFind object and exposes methods to perform transaction searches and retrieve paginated results. Key functionality includes executing find queries based on criteria and returning data in pages of specified size. Important methods are PerformSearch() which executes queries using criteria data, and GetDataPagedResult() which returns paginated search results with information about total records and pages.

 **Code Landmarks**
- `Line 33`: The UIConnector pattern separates client UI from server business logic, allowing efficient data transfer with only necessary data sent to clients
- `Line 72`: The connector implements pagination for search results, allowing efficient transfer of large result sets
- `Line 92`: The GetDataPagedResult method supports non-sequential page requests, providing flexibility for UI navigation
### connect/Gift.GiftDetailFind.cs

TGiftDetailFindUIConnector implements a UI connector class that facilitates gift detail searching in OpenPetra's finance module. It acts as an intermediary between the client UI and server-side business logic, wrapping a TGiftDetailFind instance. The connector provides methods to perform searches based on criteria data and retrieve paginated results. Key functionality includes executing search queries with PerformSearch() and retrieving paginated data with GetDataPagedResult(). The class follows OpenPetra's UIConnector pattern, handling data retrieval while ensuring efficient data transfer between server and client.

 **Code Landmarks**
- `Line 73`: The connector implements a pagination system that allows retrieving search results in pages of configurable size rather than all at once.
- `Line 84`: The comment indicates pages can be requested in any order, suggesting a stateful search result cache on the server side.
### data/Cacheable.ManualCode.cs

TCacheable implements functionality for retrieving cacheable financial data tables that can be stored on the client side. The class provides methods to fetch various financial data including accounting periods, ledger details, cost centers, accounts, and fee information. Key methods include GetCacheableTable for retrieving specified tables, and specialized methods for different financial entities like GetAccountListTable and GetLedgerDetailsTable. The file also contains validation logic for fees payable and receivable to prevent concurrent editing conflicts by checking for duplicate fee codes across tables.

 **Code Landmarks**
- `Line 67`: Uses aggregates to build specialized data tables with specific fields for client-side caching
- `Line 112`: Implements special handling for bank account flags by processing account properties
- `Line 177`: Contains concurrency control to prevent duplicate fee codes between payable and receivable tables
### data/DataAggregates.cs

This file implements two data aggregate classes for the OpenPetra financial module. TALedgerNameAggregate retrieves ledger names by joining the a_ledger and p_partner tables, providing a single GetData method that returns ledger numbers and names in a DataTable. TCostCentresLinkedToPartner retrieves cost centers linked to partners, including partner keys and classes, with its GetData method accepting a ledger number parameter. Both classes use SQL queries to aggregate data from multiple tables and return structured results for financial reporting purposes.

 **Code Landmarks**
- `Line 45`: Uses string formatting to build SQL queries dynamically rather than parameterized queries
- `Line 74`: Implements a complex join across multiple tables including a LEFT OUTER JOIN for optional unit data
### queries/ExtractDonorByAmount.cs

QueryDonorByAmount implements a server-side extract query that identifies donors based on their gift amounts and other criteria. The class extends ExtractQueryBase and overrides methods for special processing of donor data. It retrieves gift details from the database using SQL queries, processes the records to filter donors based on parameters like minimum/maximum gift amounts, date ranges, number of gifts, and whether to include only new donors. The class handles both base currency and international currency amounts, and supports filtering by address information. Key methods include CalculateExtract, RunSpecialTreatment, RetrieveParameters, and ProcessGiftDetailRecords.

 **Code Landmarks**
- `Line 67`: Implements special treatment flag to indicate this extract requires post-processing beyond a single query
- `Line 117`: Creates an extract from filtered partner keys with support for address filtering
- `Line 197`: Complex algorithm to process gift records with logic for handling reversed gifts and different currencies
- `Line 226`: Implements filtering logic that distinguishes between per-gift and cumulative amount criteria
- `Line 248`: Handles edge cases for partner transitions in the data to ensure accurate gift counting
### queries/ExtractDonorByField.cs

QueryDonorByField implements a server-side component for extracting donor information based on specific financial criteria. It extends ExtractQueryBase to generate extracts of donors who have contributed to particular ledgers. The class provides the CalculateExtract method that processes parameters and executes SQL queries using a template file. The RetrieveParameters method handles parameter processing, including options for ledger selection, date ranges, donor types, and receipt preferences. The implementation supports filtering by active status, family-only records, solicitation preferences, and receipt letter frequency.

 **Code Landmarks**
- `Line 33`: Class is designed as an example implementation for more complex reports and extracts
- `Line 53`: Uses SQL template files rather than hardcoded queries for better maintainability
- `Line 74`: Handles empty parameter lists with dummy values to prevent SQL query failures
### queries/ExtractDonorByMiscellaneous.cs

QueryDonorByMiscellaneous implements a server-side extract query for retrieving donors based on various financial criteria. The class extends ExtractQueryBase and provides functionality to filter donors by ledger number, recipient key, mailing code, date range, payment methods, and other parameters. The main method CalculateExtract processes parameters from the client, builds SQL queries with appropriate filters, and returns donor data. The RetrieveParameters method handles parameter validation and SQL statement preparation, converting client parameters into database query parameters with proper formatting and validation.

 **Code Landmarks**
- `Line 53`: Uses SQL file externalization pattern to separate query logic from code
- `Line 70`: Comprehensive parameter handling with null checks and type conversions for SQL injection prevention
- `Line 85`: Dynamic SQL modification using placeholders for conditional query parts
- `Line 184`: Commented code shows planned but unimplemented receipt letter code functionality
### queries/ExtractDonorByMotivation.cs

QueryDonorByMotivation implements a server-side extract query that retrieves donors who have made gifts with specific motivation details. The class extends ExtractQueryBase and provides functionality to generate SQL queries with parameters from client requests. The main method CalculateExtract processes parameters like ledger number, motivation details, date ranges, and donor filters to build a parameterized SQL query. The RetrieveParameters method handles parameter processing, including special handling for motivation group-detail pairs that are formatted into SQL-compatible strings. The class supports filtering by mailing codes, active status, family-only records, and receipt preferences.

 **Code Landmarks**
- `Line 77`: Processes motivation group and detail codes as pairs from a comma-separated string, demonstrating custom parameter formatting for SQL queries
- `Line 53`: Uses SQL template files rather than hardcoded queries, improving maintainability
- `Line 57`: Implements a factory pattern where a static method creates and delegates to an instance method
### queries/ExtractRecipientByField.cs

QueryRecipientByField implements a server-side query class for extracting financial recipient data based on ledger contributions. It inherits from ExtractQueryBase and provides functionality to generate extracts of recipients who have given to particular ledgers within specified date ranges. The class contains two main methods: CalculateExtract, which initializes the extraction process using SQL from a file, and RetrieveParameters, which processes client parameters and builds SQL parameter lists for filtering by ledgers and date ranges. The implementation handles both all-ledger and specific-ledger scenarios with appropriate parameter handling.

 **Code Landmarks**
- `Line 43`: Class is explicitly marked as an example for more complex reports and extracts
- `Line 51`: Uses external SQL file 'Gift.Queries.ExtractRecipientByField.sql' rather than embedding SQL in code
- `Line 83`: Handles empty parameter lists with dummy values to prevent SQL query failures
### queries/ReportFinance.cs

QueryFinanceReport implements server-side financial reporting functionality for OpenPetra's MFinance module. It provides methods to analyze and summarize gift data for reporting purposes. Key functionality includes calculating total gifts through field by month or year, selecting gift recipients based on various criteria, and retrieving donor information for specific recipients. The class supports filtering by ledger, date ranges, partner types, and currencies. It handles both base and international currency calculations, and supports tax-deductible percentage tracking when enabled. The methods generate DataTables with aggregated gift information that can be used in financial reports.

 **Code Landmarks**
- `Line 72`: Contains commented-out legacy code for HOSA gift calculation that was previously implemented but no longer used
- `Line 229`: Uses SQL query with CASE statements to calculate totals across four different date periods in a single query
- `Line 323`: Stores SQL query in session variable to avoid redundant query building between related methods
- `Line 144`: Conditionally includes tax deductible amount calculations based on system configuration
### setup/CorporateExchangeRates.Setup.cs

TCorporateExchangeRatesSetupWebConnector implements functionality for managing corporate exchange rates in the OpenPetra financial system. It provides methods to save exchange rate changes and update related financial transactions. The class handles recalculating international currency amounts in transactions when exchange rates are modified, updates general ledger master records to maintain accounting integrity, and validates if exchange rates can be deleted. Key methods include SaveCorporateExchangeSetupTDS which processes rate changes and updates transaction amounts, and CanDeleteCorporateExchangeRate which checks if a rate can be safely removed based on existing financial data.

 **Code Landmarks**
- `Line 77`: Updates international amounts in transactions when exchange rates change using SQL queries
- `Line 107`: Identifies and corrects entries in general ledger master tables after modifying transaction exchange rates
- `Line 226`: Recalculates account balances based on debit/credit indicators to maintain accounting integrity
- `Line 288`: Validates if exchange rates can be deleted by checking for dependent financial transactions
### setup/GL.Setup.Validation.cs

This file is part of the General Ledger (GL) setup validation framework in OpenPetra's finance module. It defines a partial class TGLSetupWebConnector that would contain methods for validating GL Setup DataTables. Currently, the file is a placeholder with no implemented validation methods, only containing the class declaration and namespace setup. The file is structured to be extended with specific validation logic for GL setup data in the future.

 **Code Landmarks**
- `Line 29`: The file is structured as a partial class, indicating that implementation is split across multiple files
### setup/GL.Setup.cs

TGLSetupWebConnector implements server-side functionality for managing General Ledger setup in OpenPetra. It provides methods for loading and saving ledger information, managing account hierarchies, cost center structures, and subsystem activation. Key functionality includes creating new ledgers, importing/exporting account and cost center hierarchies, managing analysis attributes, renaming accounts and cost centers, and handling currency settings. The class supports operations like activating gift processing and accounts payable subsystems, validating account changes, and managing ledger settings. Important methods include LoadLedgerInfo, CreateNewLedger, ImportAccountHierarchy, SaveGLSetupTDS, and various subsystem activation methods.

 **Code Landmarks**
- `Line 1073`: ImportDefaultAccountHierarchy loads account structure from YAML file for new ledgers
- `Line 1166`: ImportDefaultCostCentreHierarchy creates standard cost center structure from template
- `Line 1311`: SetupILTCostCentreHierarchy creates Inter-Ledger Transfer structure for cross-ledger operations
- `Line 2704`: RenameAccountCode updates all database references when changing account codes
- `Line 3020`: RenameCostCentreCode handles complex database updates when renaming cost centers
### validation/AP.Validation.cs

TFinanceValidation_AP implements validation functionality for MFinance Accounts Payable data tables. The class contains a single static method ValidateApDocumentDetailManual that validates AP document detail rows. This method checks that the Amount field in AP document details is either positive or zero, ensuring financial data integrity. The validation skips deleted rows and uses TNumericalChecks to perform the validation, adding any validation failures to a collection of verification results. The class is part of OpenPetra's server-side finance validation framework.

 **Code Landmarks**
- `Line 43`: Uses a partial class pattern allowing for extension in other files
- `Line 55`: Validation skips deleted rows, preventing unnecessary validation of data being removed
- `Line 74`: Uses Auto_Add_Or_AddOrRemove method for efficient management of verification results
### validation/Finance.Checks.Validation.cs

This file appears to be a placeholder for finance check validation functionality in OpenPetra's financial module. The file contains only namespace declarations and imports but lacks any actual implementation code. It imports data handling, verification, and finance-related modules that would typically be used for validating financial checks. Despite the imports suggesting validation functionality for finance checks, the file contains no classes, methods, or variables.
### validation/GL.Setup.Validation.cs

TFinanceValidation_GLSetup provides validation functions for MFinance GL DataTables in OpenPetra. It implements validation for daily exchange rates, corporate exchange rates, administrative grants (payable and receivable), and accounting periods. The class ensures data integrity by validating currency rates, dates, time values, and financial amounts. Key validations include checking that exchange rates are positive, dates fall within acceptable ranges, and accounting period end dates occur after start dates. The validation methods use TVerificationResultCollection to track and report validation errors, with different severity levels for critical and non-critical issues.

 **Code Landmarks**
- `Line 54`: Comprehensive validation for exchange rates with multiple date range checks and currency validations
- `Line 203`: Corporate exchange rate validation includes special handling for accounting period start dates
- `Line 271`: Conditional validation logic that changes based on the ChargeOption (Percentage vs Fixed amount)
### validation/GL.Validation.cs

TFinanceValidation_GL provides comprehensive validation functions for General Ledger financial data in OpenPetra's server-side implementation. The class contains methods to validate GL batches, journals, transactions, and recurring entries against business rules and data integrity requirements. Key functionality includes validating effective dates against accounting periods, verifying transaction amounts, ensuring narrative and reference fields are populated, validating currency exchange rates, and checking that cost centers and account codes exist and are active for posting. The class supports both normal user operations and data import scenarios with specialized validation paths for each context.

 **Code Landmarks**
- `Line 74`: Validates batch dates against accounting period ranges using specialized date validation helpers
- `Line 183`: Handles currency validation including exchange rate verification against corporate exchange rate tables
- `Line 347`: Validates GL transactions with different logic paths for importing vs. normal user operations
- `Line 378`: Validates cost centre codes against reference tables, checking both existence and posting status
- `Line 400`: Validates account codes against reference tables, ensuring they exist and are valid for posting
### validation/Gift.Validation.cs

TFinanceValidation_Gift implements validation logic for gift-related data in OpenPetra's finance module. It contains methods to validate gift batches, gift details, recurring gifts, and gift motivation setup data. Key functionality includes validating bank accounts, cost centers, currencies, exchange rates, donors, recipients, motivation groups/details, and ensuring proper data relationships. The class performs comprehensive validation for both manual data entry and importing scenarios, checking for valid codes, active accounts, proper relationships between entities, and appropriate values for financial transactions. Important methods include ValidateGiftBatchManual, ValidateGiftDetailManual, ValidateGiftManual, ValidateRecurringGiftBatchManual, and ValidateGiftMotivationSetupManual.

 **Code Landmarks**
- `Line 77`: Comprehensive validation of gift batch data with optional reference tables for importing scenarios
- `Line 254`: Validation includes checking corporate exchange rates for international currency conversions
- `Line 352`: Gift detail validation includes complex business rules for motivation groups and recipient types
- `Line 445`: Validation ensures proper relationships between recipient partners and their ledger numbers
- `Line 1006`: Special validation rules for recurring gifts that differ from standard gift validation
### validation/Helper.cs

TFinanceValidationHelper implements a static class that provides helper functions for validating finance data in OpenPetra. It primarily manages delegate functions for validating date ranges in accounting periods. The class defines three key delegate types: TGetValidPostingDateRange, TGetValidPeriodDates, and TGetFirstDayOfAccountingPeriod. These delegates are stored as thread-static fields and exposed through properties. The class provides public methods that invoke these delegates to validate posting date ranges, period dates, and determine the first day of accounting periods for specified ledgers. If delegates aren't initialized before calling these methods, the class throws InvalidOperationExceptions.

 **Code Landmarks**
- `Line 43`: Uses ThreadStatic attribute to ensure delegate references are thread-specific, preventing cross-thread interference
- `Line 53`: Implements delegate pattern to allow external code to provide the actual validation implementation
- `Line 149`: Optional database parameter allows flexibility in data source for accounting period calculations

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #