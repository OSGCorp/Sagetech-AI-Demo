# C# Shared Finance Module Of OpenPetra

The OpenPetra Finance Module is a C# implementation that provides comprehensive financial management capabilities for non-profit organizations. The module handles core accounting functions, gift processing, tax calculations, and international currency operations. This sub-project implements cross-cutting financial services and data structures that serve as the foundation for OpenPetra's financial operations, providing these capabilities:

- General Ledger management with batch/journal transaction processing
- Gift processing with tax deductibility calculations
- Bank import and reconciliation functionality
- Multi-currency support with exchange rate management
- Accounts payable operations with supplier payment processing

## Identified Design Elements

1. **Typed Dataset Architecture**: Strongly-typed datasets (TDS) define the data structures for different financial subsystems, ensuring type safety and clear data relationships
2. **Financial Constants System**: Comprehensive enumeration system standardizes transaction types, account codes, and financial identifiers across the application
3. **Cross-Currency Operations**: Built-in exchange rate management with optimization algorithms for finding best available rates
4. **Financial Validation Framework**: Robust validation for banking information including IBAN/BIC verification according to international standards
5. **Tax Calculation Engine**: Specialized services for calculating tax-deductible portions of financial transactions

## Overview
The architecture emphasizes data integrity through strong typing and validation, while supporting international financial operations with multi-currency capabilities. The module implements financial industry standards for banking information validation and follows accounting principles for transaction processing. The codebase separates concerns between data definition (TypedDataSets), business logic (Routines classes), and system constants, creating a maintainable structure that can accommodate country-specific financial requirements and complex non-profit accounting scenarios.

## Business Functions

### Finance Constants and Types
- `Constants.cs` : Defines constants and enumerations used throughout the finance module of OpenPetra for accounting, gift processing, and financial operations.
- `Types.cs` : Defines financial batch filtering options for OpenPetra's finance module.

### Financial Data Processing
- `TaxDeductibility.cs` : Utility class for calculating tax-deductible and non-deductible amounts for gift transactions in OpenPetra's finance module.
- `CommonRoutines.cs` : Provides financial validation and utility functions for banking operations in OpenPetra.
- `GLRoutines.cs` : Provides shared GL accounting routines for updating batch totals and transaction calculations in OpenPetra's financial system.

### Gift Management
- `data/Finance.Gift.TypedDataSets.xml` : XML definition of typed datasets for financial gift processing in OpenPetra.

### Bank Import
- `data/Finance.BankImport.TypedDataSets.xml` : XML schema definition for bank import data structures in OpenPetra's finance module.

### Cross-Ledger Operations
- `data/Finance.CrossLedger.TypedDataSets.xml` : XML definition for ExchangeRateTDS typed dataset used in OpenPetra's cross-ledger financial operations.

### General Ledger
- `data/Finance.GL.TypedDataSets.xml` : XML definition file for General Ledger typed datasets used in OpenPetra's financial module.

### Accounts Payable
- `data/Finance.AP.TypedDataSets.xml` : Defines the AccountsPayableTDS typed dataset structure for accounts payable functionality in OpenPetra.

## Files
### CommonRoutines.cs

CommonRoutines implements financial validation and utility functions for the OpenPetra Finance Module. It provides methods for validating banking information including BIC (Bank Identifier Code) and IBAN (International Bank Account Number) validation according to international standards. The class also includes country-specific account number validation (currently supporting Netherlands), and functions for finding optimal currency exchange rates from rate tables. Key methods include CheckBIC(), CheckIBAN(), CheckAccountNumberIsNotIBAN(), and GetBestExchangeRate(). The validation logic implements detailed checks against formatting rules, checksums, and country-specific requirements for financial data integrity.

 **Code Landmarks**
- `Line 72`: Implements BIC validation with detailed structure checking for international bank codes
- `Line 173`: IBAN validation with comprehensive country-specific length validation against a reference table
- `Line 335`: Modulo-11 checksum algorithm for Dutch bank account validation
- `Line 467`: Currency exchange rate selection algorithm that handles bidirectional rate lookups
### Constants.cs

MFinanceConstants.cs defines a comprehensive set of constants and enumerations used throughout the OpenPetra finance module. It includes constants for currency types, gift processing, accounting batch statuses, transaction types, account types, and financial system identifiers. The file defines standard account codes, ICH (International Clearing House) accounts, budget types, and ledger initialization flags. It also implements several enumerations including CommonAccountingSubSystemsEnum, CommonAccountingTransactionTypesEnum, TLedgerInitialisationArrayEnum, TMOPTypeEnum, GiftAdjustmentFunctionEnum, and CCRollupStyleEnum that standardize financial operations across the system.

 **Code Landmarks**
- `Line 31`: Defines MAX_PERIODS constant which limits the number of accounting periods in the system to 20
- `Line 70-87`: Defines batch status constants that control the workflow of financial batches through the system
- `Line 166-195`: Defines standard account types that implement the accounting equation (Assets, Liabilities, Income, Expense, Equity)
- `Line 271-306`: Defines ICH (International Clearing House) account constants for handling multi-currency transactions
- `Line 467`: Implements GiftAdjustmentFunctionEnum which defines the different ways gifts can be reversed or adjusted
### GLRoutines.cs

GLRoutines implements utility functions for OpenPetra's financial system that are used on both server and client sides. It handles General Ledger batch and journal management, focusing on updating transaction numbers, calculating totals, and managing financial data integrity. Key functionality includes updating batch and journal last transaction numbers, calculating transaction amounts in base currency, and updating batch totals. The class provides methods for both standard and recurring batches, with functions like UpdateBatchLastJournal, UpdateJournalLastTransaction, UpdateBatchTotals, and currency conversion utilities. It also offers dataset manipulation methods to extract or remove specific batch data from larger datasets. Important constants include DECIMALS for rounding precision, while key methods handle validation, transaction calculations, and dataset filtering.

 **Code Landmarks**
- `Line 46`: Implements shared financial routines used by both client and server components, promoting code reuse
- `Line 71`: Robust argument validation pattern used throughout the class to ensure data integrity
- `Line 301`: Methods handle both standard and recurring batches with parallel implementation structures
- `Line 585`: Dataset manipulation methods allow filtering financial data by batch for efficient processing
- `Line 700`: Currency calculation methods ensure consistent decimal precision across the system
### TaxDeductibility.cs

TaxDeductibility provides static methods for calculating tax-deductible and non-deductible amounts for gift transactions in OpenPetra's finance module. The main functionality includes updating tax deductibility amounts for gift details across transaction, base, and international currency values. The class ensures percentages are constrained between 0-100% and calculates the appropriate splits of gift amounts. Key methods include UpdateTaxDeductibiltyAmounts which processes a gift detail record, and the private CalculateTaxDeductibilityAmounts which performs the actual calculations based on gift amount and deductible percentage.

 **Code Landmarks**
- `Line 42`: Ensures tax deductible percentage is constrained between 0 and 100%
- `Line 47`: Updates three different currency representations (transaction, base, and international) with the same percentage calculation
### Types.cs

This file defines the TFinanceBatchFilterEnum enumeration used throughout OpenPetra's finance module to filter batches in batch screens. The enumeration provides five filtering options: fbfNone (no batches), fbfReadyForPosting (only batches ready for posting), fbfEditing (unposted and not cancelled batches), fbfAllCurrent (all batches in current and forward posting periods), and fbfAll (all batches including previous periods). Each option has a specific integer value that appears to use bitwise logic for combining filter criteria.

 **Code Landmarks**
- `Line 34`: The enumeration uses bitwise values (1, 3, 7, 15) suggesting filters can be combined through bitwise operations
### data/Finance.AP.TypedDataSets.xml

This XML file defines the AccountsPayableTDS typed dataset structure for the accounts payable module in OpenPetra. It imports dependencies from Finance and Partner modules and specifies tables related to suppliers, documents, payments, and analysis attributes. The file extends standard database tables with custom fields to support accounts payable operations, including fields for tracking payment status, due dates, outstanding amounts, exchange rates, and payment processing details. Notable custom fields include payment flags, document tracking fields, and supplier information needed for payment processing. The dataset integrates accounts payable data with partner information through related tables.

 **Code Landmarks**
- `Line 7`: Defines custom fields that extend the database schema for runtime functionality
- `Line 13`: Implements payment tracking with custom Boolean fields for payment processing logic
- `Line 23`: Creates fields for check printing functionality including amount-to-words conversion
### data/Finance.BankImport.TypedDataSets.xml

This XML file defines the BankImportTDS typed dataset structure for OpenPetra's finance module, specifically for bank import functionality. It establishes database tables and their relationships for managing imported bank transactions, including gift records, banking details, electronic payment statements, and transaction matching. The file extends standard database tables with custom fields to support additional functionality like transaction matching, donor information tracking, and gift processing. Key tables include a_gift, a_gift_detail, p_banking_details, a_ep_statement, a_ep_match, a_ep_transaction, and custom TransactionDetail that joins data from multiple tables.

 **Code Landmarks**
- `Line 5-7`: Imports other data modules showing dependencies on Gift, Account and Partner subsystems
- `Line 11-16`: Custom fields in gift tables to track donation amounts, donor information and matching status
- `Line 30-36`: Transaction table with custom fields for tracking gift relationships across multiple subsystems
- `Line 37-43`: Custom TransactionDetail table that joins fields from multiple physical database tables
### data/Finance.CrossLedger.TypedDataSets.xml

This XML file defines the ExchangeRateTDS typed dataset structure for OpenPetra's financial cross-ledger operations. It imports data modules from MFinance Gift and Account namespaces and defines four related tables: a_corporate_exchange_rate, a_daily_exchange_rate (with usage tracking fields), ADailyExchangeRateUsage (tracking which ledgers/batches use specific exchange rates), and ALedgerInfo (containing period date information). The dataset includes custom fields for tracking exchange rate usage in journals and gift batches, with table source indicators and appropriate primary keys for each table. This structure supports currency conversion operations across the financial system.

 **Code Landmarks**
- `Line 8`: Custom fields track usage counts of exchange rates in different financial contexts
- `Line 15`: ADailyExchangeRateUsage table implements a relationship tracking which financial records reference specific exchange rates
- `Line 24`: ALedgerInfo table extends standard ledger data with custom date fields for period tracking
### data/Finance.GL.TypedDataSets.xml

This XML file defines the structure of typed datasets used in OpenPetra's General Ledger (GL) financial module. It specifies several key datasets including GLBatchTDS for transaction batches, GLPostingTDS for posting operations, GLReportingTDS for financial reporting, GLSetupTDS for system configuration, GLStewardshipCalculationTDS for fee calculations, BudgetTDS for budget management, SuspenseAccountTDS for suspense accounts, and CorporateExchangeSetupTDS for exchange rates. Each dataset defines database tables with their fields, custom fields, and relationships needed for financial operations. The file establishes the data structure foundation for OpenPetra's accounting functionality.

 **Code Landmarks**
- `Line 10`: GLBatchTDS includes custom fields for transaction currency and journal totals that extend the database schema
- `Line 49`: GLReportingTDS includes analysis type fields that enable multi-dimensional financial reporting capabilities
- `Line 76`: GLSetupTDS contains flag fields to identify special account types (bank, cash, suspense)
- `Line 129`: BudgetTDS defines 14 period amount fields allowing for flexible fiscal year structures
- `Line 149`: Custom relation defined between suspense accounts and standard accounts for descriptive linking
### data/Finance.Gift.TypedDataSets.xml

This XML file defines the GiftBatchTDS typed dataset structure for OpenPetra's financial gift processing system. It specifies database tables and their relationships for managing gift transactions, including regular and recurring gifts. The dataset includes tables for ledgers, gift batches, individual gifts, gift details, recurring gifts, banking information, and partner data. Custom fields extend the base tables with additional information like donor names, gift totals, recipient details, and payment methods. The structure supports comprehensive gift tracking with connections to partner records, motivation details, and financial processing requirements.

 **Code Landmarks**
- `Line 6`: GiftBatchTDS combines multiple related tables into a single typed dataset for efficient gift processing
- `Line 10`: Custom fields extend database tables with calculated or derived values not stored in the database
- `Line 24`: Fields from a_motivation_detail are included directly in gift detail tables for simplified data access
- `Line 73`: Multiple instances of p_partner table with different names allow different partner roles within the same dataset

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #