# C# Finance Sample Data Subproject Of OpenPetra

The OpenPetra is a C# program that provides administrative and financial management capabilities for non-profit organizations. The program handles accounting, contact management, and data exchange while supporting international operations. This sub-project implements sample financial data templates and test datasets along with import/export configurations for the finance module. This provides these capabilities to the OpenPetra program:

- Financial document generation with HTML templates for receipts and statements
- Sample data for testing budget imports and gift processing
- Standardized CSV formats for financial data exchange
- Configuration files for data import mapping and transformation

## Identified Design Elements

1. Templated Financial Documents: HTML templates with placeholders enable dynamic generation of tax-compliant financial documents like donation receipts
2. Structured Data Import: YAML configuration files define mappings between external data formats and internal financial structures
3. Multi-currency Support: Sample data demonstrates international financial operations with different currencies (e.g., KES)
4. Budget Modeling: CSV files provide examples of various budget types (Same, Split, Adhoc, Inflation-based) for testing budget functionality

## Overview
The architecture emphasizes standardized data formats for financial information exchange, templated document generation for regulatory compliance, and comprehensive test datasets covering various financial scenarios. The templates support internationalization with multi-language capabilities, particularly for tax documentation. The sample data demonstrates the system's ability to handle complex financial operations including budget planning, gift processing, and general ledger transactions across different accounting periods and currencies.

## Business Functions

### Financial Reports and Receipts
- `AnnualReceiptTemplate.html` : HTML template for annual donation receipts with placeholders for donor information and donation details.
- `AnnualReceiptExpected.html` : HTML template for annual donation receipt with German tax information and donor details.
- `SingleGiftReceiptExpected.html` : HTML template for a single gift receipt document with German tax donation confirmation.
- `SingleGiftReceiptTemplate.html` : HTML template for generating single gift receipts with donor and charity information in a German tax donation format.

### Budget Management
- `BudgetImport-01.csv` : Sample CSV data for budget import testing in OpenPetra's finance module.
- `BudgetImport-02.csv` : Sample CSV data file for budget import testing in OpenPetra's finance module.
- `BudgetImport-03.csv` : Sample CSV file containing budget import data for financial testing
- `BudgetImport-04.csv` : Sample CSV file for budget import testing with various budget types and monthly data values.
- `BudgetImport-All.csv` : CSV data file containing budget import sample data for OpenPetra financial testing.

### Data Import Configuration
- `importBookkeepingDataDescription.yml` : YAML configuration file for importing bookkeeping data into OpenPetra with Kenya Shilling currency settings.

### Financial Transactions
- `sampleGiftBatch.csv` : Sample CSV file containing gift batch data for testing OpenPetra's financial module.
- `SampleFileWithGLTransactionsToImport.csv` : Sample CSV file containing financial transaction data for testing GL import functionality in OpenPetra.

## Files
### AnnualReceiptExpected.html

This HTML file serves as a template for generating annual donation receipts in OpenPetra. It contains a structured layout for a German tax donation confirmation document with placeholders for dynamic content. The template includes sections for donor information, charity details, donation amounts, legal tax statements, and an itemized list of donations. The document features two pages: the main receipt with formal tax declarations and an appendix listing individual donations with their dates, amounts, and purposes.

 **Code Landmarks**
- `Line 18`: Placeholder comment for logo image that would be replaced in actual receipts
- `Line 102`: #TODAY# placeholder that would be dynamically replaced with current date during receipt generation
- `Line 147`: #THISYEAR# placeholder for dynamic insertion of the current year in donation details
### AnnualReceiptTemplate.html

This HTML template file provides a layout for annual donation receipts. It contains placeholders (prefixed with #) for donor information (name, address), donation amounts, dates, and other receipt details. The template includes sections for the recipient's address, charity contact information, donation summary, legal confirmations, and an itemized list of donations. The document is structured with tables for layout and includes different font sizes for various sections. The template appears to be designed for German tax receipts, containing German legal text and tax information.

 **Code Landmarks**
- `Line 33-39`: Address placeholders using # prefix notation that will be replaced with donor data during processing
- `Line 85-91`: Donation amount section with placeholders for both numeric and text representation of amounts
- `Line 139-149`: Conditional display elements for different donation types (Gift vs Gift In Kind)
### BudgetImport-01.csv

This CSV file contains sample budget data for testing the budget import functionality in OpenPetra's finance module. It includes multiple budget entries organized by cost center, account, budget type, and year. The data spans two fiscal years (2012 and 2013) for cost center 4300, with various accounts and budget types including 'Same', 'Split', 'Adhoc', 'Inf.Base', and 'Inf. n'. Each entry contains monthly budget values across 12 periods, with empty columns reserved for potential additional periods.

 **Code Landmarks**
- `Line 1`: Header defines the structure with cost centre, account, budget type, year and 20 data columns for monthly values
- `Line 2-25`: Data for fiscal year 2012 with consistent cost center (4300) but varying account codes and budget types
- `Line 26-49`: Data for fiscal year 2013 with identical structure to 2012 data, suggesting testing for multi-year budget imports
### BudgetImport-02.csv

This CSV file contains sample budget data for testing OpenPetra's budget import functionality. It includes 25 budget entries for cost centre 4300 across various accounts for the year 2012. Each row represents a budget line with different budget types (Same, Split, Adhoc, Inf.Base, Inf. n) and monthly budget values spread across 12 months (Data[1] through Data[12]). The file structure demonstrates various budget allocation patterns including flat allocations, split allocations, and inflation-based calculations that would be processed by OpenPetra's finance module.

 **Code Landmarks**
- `Line 1`: Header defines the CSV structure with Cost Centre, Account, Budget Type, Year, and 20 data columns for monthly values
- `Line 2-26`: Demonstrates five different budget types (Same, Split, Adhoc, Inf.Base, Inf. n) that represent different allocation patterns
### BudgetImport-03.csv

This CSV file contains sample budget import data for testing OpenPetra's financial module. It includes budget data for cost center 4300 with different budget types (Same, Split, Adhoc, Inf.Base, Inf. n) for the year 2012. The file structure has columns for Cost Centre, Account, Budget Type, Year, and 20 data columns representing monthly budget values. Each row represents a different account with its corresponding budget allocation across months.

 **Code Landmarks**
- `Line 1`: Header row defines the structure for budget import with cost center, account, type, year and 20 data columns
- `Line 2-6`: Demonstrates five different budget types (Same, Split, Adhoc, Inf.Base, Inf. n) with varying monthly distribution patterns
### BudgetImport-04.csv

This CSV file contains sample data for testing budget import functionality in OpenPetra. It includes five budget records for cost centre 4300 with different accounts and budget types (Same, Split, Adhoc, Inf.Base, Inf. n). Each record contains monthly budget values for 2013, with 12 months of data per record. The file structure includes headers for cost centre, account, budget type, year, and 20 data columns representing monthly budget values.

 **Code Landmarks**
- `Line 2`: Same budget type with consistent values of 100 across all months
- `Line 3`: Split budget type with equal values for 11 months and a different value for month 12
- `Line 4`: Adhoc budget type with unique values for each month
- `Line 5`: Inflation-based budget with stepped increases
- `Line 6`: Another inflation pattern with a mid-year step increase from 200 to 300
### BudgetImport-All.csv

This CSV file contains sample budget import data for testing OpenPetra's financial module. It includes budget line items with account codes, cost centers, descriptions, and monthly budget values. The data represents different budget scenarios including adhoc budgets, split budgets, and inflation-based budgets across different time periods. Each row contains an account code, cost center, description, budget period indicator, and up to 12 monthly budget values.

 **Code Landmarks**
- `Line 1`: Uses account code 4300 consistently across all test records
- `Line 1-5`: Demonstrates different budget allocation patterns - monthly breakdowns vs. single values
- `Line 4`: Shows inflation-based budget with consistent monthly values of 3 for most periods
### SampleFileWithGLTransactionsToImport.csv

This CSV file provides sample financial data for testing the General Ledger import functionality in OpenPetra. It contains a structured format with sections for bookkeeping summary, cash flow information, and transaction details. The file includes headers and data rows representing financial transactions with dates, amounts, and categories. It demonstrates the expected format for importing financial data with transaction numbers, descriptions, dates, and monetary values in Kenyan Shillings (KES).

 **Code Landmarks**
- `Line 1-13`: Demonstrates the specific CSV structure expected by OpenPetra's GL import functionality, including headers and summary sections
- `Line 8`: Shows totals row with sum calculations that would need to be validated during import
- `Line 9`: Column headers define the expected data structure for transaction processing
- `Line 12`: Example of transaction with ID number 1, showing the complete data format expected for processing
### SingleGiftReceiptExpected.html

This HTML file serves as a template for generating single gift receipts in OpenPetra. It contains a structured layout for a donation confirmation document that follows German tax regulations. The template includes placeholders for dynamic content such as dates (#TODAY#, #THISYEAR#), donor information, and donation amounts. The document is formatted with tables for layout control and includes the charity's contact information, the donor's address, donation details, and required legal disclaimers in German. The file demonstrates how OpenPetra formats financial documents for printing or electronic distribution.

 **Code Landmarks**
- `Line 4`: Character encoding explicitly set to UTF-8 to support international characters in the German text
- `Line 16`: Placeholder comment for logo image integration
- `Line 30`: Address formatting follows a specific structure for postal mail compatibility
- `Line 74`: Dynamic date placeholders (#TODAY#, #THISYEAR#) that will be replaced at runtime
### SingleGiftReceiptTemplate.html

This HTML template file provides a structure for generating single gift receipts for charitable donations. It creates a formatted document with placeholders for donor information (name, address), donation details (amount, date), and charity information. The template is designed for German tax donation receipts with specific legal text and formatting. Key placeholders include #NAME, #STREETNAME, #POSTALCODE, #CITY, #OVERALLAMOUNT, #TOTALAMOUNTINWORDS, and #DONATIONDATE. The document includes both the donor's address and the charity's contact information, along with required legal notices and disclaimers.

 **Code Landmarks**
- `Line 1`: Uses HTML 4.01 Transitional DOCTYPE which indicates backward compatibility requirements
- `Line 5`: Contains a TODO comment about potentially using CSS for positioning and font sizes
- `Line 16`: Placeholder for logo image that's currently commented out
- `Line 30-36`: Uses template variables with # prefix for dynamic content insertion
- `Line 52-58`: Implements a structured table for donation amount details with specific German tax requirements
### importBookkeepingDataDescription.yml

This YAML configuration file defines parameters for importing bookkeeping data into OpenPetra's finance module. It specifies Kenya Shilling (KES) as the currency, sets default cost centers and accounts, and defines formatting rules including separators and date format. The file maps CSV columns to OpenPetra financial concepts, indicating which columns represent transaction references, narratives, dates, and accounts. It includes specific account codes and credit/debit designations for donations income and food expenses.

 **Code Landmarks**
- `Line 3-5`: Defines default financial accounts and cost centers for imported transactions
- `Line 6-9`: Specifies localization settings for date formats and number separators critical for accurate data parsing
- `Line 13-17`: Maps source columns to OpenPetra financial fields with account codes and transaction types
### sampleGiftBatch.csv

This CSV file provides sample gift batch data for testing OpenPetra's financial module. It contains two lines: a batch header (B) with information about the gift batch including date, currency, and ledger number; and a transaction line (T) with donor information, gift amount, and various allocation details. The file demonstrates the expected format for importing gift batches into the system.

 **Code Landmarks**
- `Line 1`: Uses placeholder {thisyear} that likely gets replaced with the current year during processing
- `Line 1`: Uses placeholder {ledgernumber} that gets replaced with an actual ledger number during import
- `Line 2`: Contains multiple yes/no flags that control gift processing behavior

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #