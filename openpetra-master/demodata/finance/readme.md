# Demo Finance Data Subproject Of Petra

The Demo Finance Data subproject provides essential test and reference data for OpenPetra's financial modules. This collection of structured data files serves as the foundation for testing financial operations, currency conversions, and accounting functionality within the system. The subproject implements comprehensive sample datasets for general ledger transactions, currency exchange rates, and account hierarchies, enabling developers to validate financial processing capabilities across the application.

## Key Components

- **General Ledger Transaction Data**: CSV files containing balanced accounting entries with corresponding debits and credits
- **Currency Exchange Reference Data**: Historical exchange rate information for multiple currency pairs
- **Chart of Accounts Framework**: YAML-defined hierarchical structure of all accounting elements
- **Batch Export Test Data**: Sample financial transaction batches for testing export functionality

## Identified Design Elements

1. **Standardized Data Formats**: Consistent CSV structures with semicolon or pipe delimiters for easy parsing and integration
2. **Complete Accounting Framework**: Comprehensive chart of accounts supporting assets, liabilities, equity, income, and expense tracking
3. **Multi-currency Support**: Reference exchange rate data enabling currency conversion operations
4. **Transaction Batch Processing**: Sample batch headers and transaction records demonstrating the batch processing model
5. **Financial Test Scenarios**: Varied transaction samples covering different financial scenarios including donations and cross-currency operations

## Architecture Integration

The Demo Finance Data subproject integrates with OpenPetra's finance module by providing the reference and test data necessary for development, testing, and demonstration. The data structures align with the application's data models, ensuring developers can effectively implement and validate financial processing features while maintaining accounting integrity and multi-currency support.

## Sub-Projects

### demodata/finance/bankstatements

This subproject focuses on data mapping and transformation, enabling the system to correctly interpret CSV-formatted bank statements for integration into OpenPetra's accounting and financial management systems.

The subproject implements CSV column mapping configuration along with field transformation rules that standardize external banking data into OpenPetra's internal data model. This provides these capabilities to the Petra program:

- Structured mapping of external bank statement formats to internal data structures
- Configuration-based field transformation without code changes
- Support for international banking standards (IBAN/BIC)
- Flexible handling of various CSV formats from different financial institutions

#### Identified Design Elements

1. **Declarative Data Mapping**: The configuration defines explicit mappings between external CSV columns and internal data fields, allowing for adaptation to different bank statement formats
2. **Field Selection Logic**: The configuration specifies which fields are relevant for processing and which should be ignored (marked as "unused")
3. **Financial Data Normalization**: Ensures transaction dates, amounts, and currency information are correctly interpreted regardless of source format
4. **Banking Identifier Support**: Properly handles international banking identifiers like IBAN and BIC codes for cross-border transaction processing

#### Overview
The architecture emphasizes configuration over code, allowing OpenPetra to adapt to different bank statement formats without requiring programming changes. The mapping system provides a standardized approach to importing financial data while maintaining the integrity of transaction information. This design supports OpenPetra's international focus, particularly for organizations dealing with multiple currencies and financial institutions across different countries.

  *For additional detailed information, see the summary for demodata/finance/bankstatements.*

### demodata/finance/skr49

This subproject provides a comprehensive accounting structure specifically designed for German associations, foundations, and charitable organizations, enabling proper financial reporting and tax compliance.

The subproject consists of three key components:
- A YAML-based account definition file that structures the hierarchical chart of accounts
- A Python conversion utility that transforms GnuCash XML account data to OpenPetra format
- Documentation for implementation and usage

#### Key Technical Features

- Hierarchical account structure with 10 main classes (0-9) covering all financial aspects of German non-profits
- Account metadata including active status, account type, debit/credit nature, and validity periods
- Multilingual support with both short and long descriptions for financial reporting
- Specialized account categorization for tax-exempt vs. taxable operations
- Data conversion pipeline from industry-standard formats (GnuCash) to OpenPetra's internal format

#### Identified Design Elements

1. **Standardized Financial Data Model**: Implements the German SKR49 standard while maintaining compatibility with OpenPetra's financial engine
2. **Data Transformation Pipeline**: Provides tools to convert between different accounting system formats
3. **Locale-Aware Processing**: Handles German-specific accounting requirements and character encoding
4. **Hierarchical Account Organization**: Maintains proper parent-child relationships for financial reporting and analysis

#### Overview
The architecture emphasizes standards compliance for German financial reporting while providing the flexibility needed for various non-profit operational models. The conversion tools enable data migration from other systems, and the comprehensive account structure supports both basic and advanced accounting needs for German charitable organizations.

  *For additional detailed information, see the summary for demodata/finance/skr49.*

## Business Functions

### Financial Data
- `gltransactions.csv` : Sample GL transactions data file containing financial entries for OpenPetra accounting system.
- `glbatch.csv` : Sample GL (General Ledger) batch data in CSV format for OpenPetra's finance module.

### Exchange Rate Data
- `USD_HKD.csv` : CSV file containing USD to HKD currency exchange rates for specific dates.
- `multiplecurrencies.csv` : CSV file containing exchange rate data between EUR and other currencies for different dates.

### Batch Export Testing
- `TestBatchExport2.csv` : CSV file containing test batch export data for financial transactions in the OpenPetra system.
- `TestBatchExport.csv` : CSV file containing test batch export data for financial transactions in OpenPetra.

### Account Configuration
- `accountshierarchy.yml` : YAML configuration file defining the accounting chart of accounts hierarchy for OpenPetra's financial system.

## Files
### TestBatchExport.csv

This CSV file contains sample financial transaction data for testing batch export functionality in OpenPetra. It includes two batches (marked with 'B') containing gift transactions (marked with 'T'). Each batch record includes batch number, description, account code, amount, date, currency, and exchange rate. Transaction records contain donor information, recipient details, amounts, gift types, and various classification flags. The file demonstrates different transaction scenarios including donations to different field locations with varying donor information.

 **Code Landmarks**
- `Line 1`: Uses semicolon-delimited format with letter codes 'B' for batch headers and 'T' for transactions
- `Line 2`: Contains donor-recipient relationship with cost center codes (43000000, 73000000) representing different countries
- `Line 4`: Includes donor reference codes (A1000, A1001) and different gift types (PERSONAL, UNDESIG)
### TestBatchExport2.csv

This CSV file contains sample financial transaction data for testing batch export functionality in OpenPetra. It includes a batch header line (B) with batch number, description, bank account, date, and currency information. The transaction lines (T) represent summarized gift data with account codes, country information, and monetary amounts. The data appears to show transactions between Germany and Kenya with different currency values.

 **Code Landmarks**
- `Line 1`: Batch header line format shows standard OpenPetra financial batch structure with type, description, account, date and currency fields
- `Line 2-4`: Transaction lines demonstrate the format for recording international financial transfers between different country accounts
### USD_HKD.csv

This CSV file contains historical exchange rate data between US Dollar (USD) and Hong Kong Dollar (HKD). The file stores exchange rates for specific dates in a simple two-column format with date and rate values separated by a pipe character. The data spans from April 2009 to December 2011, with most entries from late 2011. This data is likely used by OpenPetra's finance module for currency conversion calculations.

 **Code Landmarks**
- `Line 1`: Uses pipe character as delimiter instead of comma, despite being a CSV file
- `Line all`: Date format follows ISO 8601 standard (YYYY-MM-DD) for international compatibility
### accountshierarchy.yml

This YAML file defines a comprehensive chart of accounts hierarchy for OpenPetra's financial system. It structures all accounting elements in a tree format starting with the root node 'BAL SHT' (Balance Sheet). The hierarchy organizes accounts into major categories like Assets, Liabilities, and Equity, with subcategories for cash, investments, debtors, fixed assets, creditors, and income/expense accounts. Each account entry contains attributes like active status, account type (Asset, Liability, Equity, Income, Expense), debit/credit nature, cost center validity, and descriptions. The file provides a complete accounting framework that supports financial operations including banking, investments, receivables, payables, and detailed income and expense tracking.

 **Code Landmarks**
- `Line 1`: Uses hierarchical indentation to represent parent-child relationships in the chart of accounts
- `Line 6`: Includes specialized account attributes like 'bankaccount=true' to designate accounts for specific functionality
- `Line 25`: Implements contra accounts (like accumulated depreciation) by changing the debit/credit nature
- `Line 32`: Contains a comprehensive structure for international clearing house (ICH) operations
- `Line 73`: Provides detailed categorization of income and expense accounts for non-profit financial reporting
### glbatch.csv

This CSV file contains sample General Ledger batch data for OpenPetra's finance module. It includes two batches with transaction records in a semicolon-delimited format. Each batch starts with a 'B' record (batch header) followed by a 'J' record (journal details) and multiple 'T' records (transactions). The transactions include account codes, descriptions, references, dates, debit/credit amounts, and additional transaction attributes. The data demonstrates balanced accounting entries with matching debits and credits within each batch.

 **Code Landmarks**
- `Line 1`: Uses a specific format where first character indicates record type (B=Batch, J=Journal, T=Transaction)
- `Line 3-4`: Demonstrates balanced accounting with equal debit/credit amounts (2269,98) across transaction pairs
- `Line 8-13`: Shows more complex batch with multiple related transactions including donor references and transaction codes
### gltransactions.csv

This CSV file contains sample general ledger transaction data for OpenPetra's finance module. Each line represents a financial transaction with fields separated by semicolons, including transaction type, cost center codes, description, reference, date, debit amount, credit amount, and several additional fields for transaction metadata. The data demonstrates balanced accounting entries with corresponding debits and credits, and includes examples of donor-related transactions with references to specific accounts.

 **Code Landmarks**
- `Line 1-2`: Demonstrates balanced accounting entries with matching debit (2269.98) and credit amounts across two consecutive transactions
- `Line 3-8`: Shows pattern of related transactions with donor returns ('LS-Retour') and corresponding entries with the same reference code 'E125'
- `Line all`: Uses semicolon-delimited format with quotes around each field, following a specific structure for OpenPetra's GL transaction import format
### multiplecurrencies.csv

This CSV file stores historical currency exchange rate data for OpenPetra's financial system. It contains exchange rates with EUR as the base currency paired with other currencies (HKD, ALL, USD) across different dates from 2011-2012. Each record follows the format of base currency code, foreign currency code, date (YYYY-MM-DD), and exchange rate value. The file serves as reference data for currency conversion operations in the finance module.

 **Code Landmarks**
- `Line 1`: Uses pipe-delimited format rather than comma-separated values despite being a CSV file

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #