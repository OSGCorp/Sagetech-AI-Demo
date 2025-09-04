# C# Finance GDPdU Export Subproject Of OpenPetra

The OpenPetra is a C# program that provides administrative and financial management capabilities for non-profit organizations. The program handles accounting, contact management, and data export functions while supporting international operations. This sub-project implements German tax authority digital data access (GDPdU) export functionality along with comprehensive financial data extraction capabilities. This provides these capabilities to the OpenPetra program:

- Standardized financial data export for German tax compliance
- Transaction data extraction with proper debit/credit indicators and tax calculations
- Accounts payable document export including payments and supplier details
- Cost center and account information formatting for audit purposes
- General ledger balance reporting with period start/end values
- Participant and gift transaction data extraction for conference/seminar reporting

## Identified Design Elements

1. **Command-Line Interface Architecture**: The main export tool connects to the Petra server and orchestrates the export of various financial data components
2. **Modular Export Components**: Separate specialized classes handle different aspects of financial data (transactions, accounts payable, balances, etc.)
3. **Configurable Filtering System**: Exports can be filtered by cost centers, accounts, transaction references, and financial years
4. **Database Query Optimization**: SQL queries are constructed to efficiently retrieve and join complex financial data from multiple tables
5. **Standardized Output Formatting**: All exports follow consistent CSV formatting with proper encoding for German tax authority requirements

## Overview
The architecture emphasizes compliance with German tax reporting requirements while providing flexible data extraction capabilities. The modular design separates concerns by financial data type, with each component handling its specific database interactions and formatting requirements. The system supports multi-year exports and implements sophisticated filtering to include only relevant financial information. All exports maintain consistent CSV formatting with appropriate character encoding to ensure compatibility with tax authority systems.

## Business Functions

### Financial Data Export
- `transactions.cs` : Exports financial transaction data in GDPdU format for tax reporting, focusing on GL transactions with cost center filtering.
- `accountspayable.cs` : Exports accounts payable data in GDPdU format for tax reporting in Germany.
- `accounts.cs` : Exports account and cost center data for the GDPdU (German tax audit) format in OpenPetra's finance system.
- `balances.cs` : Exports financial data in GDPdU format for tax authorities, focusing on GL balances for specified cost centers.
- `main.cs` : Command-line tool that exports financial data in GDPdU format for German tax authorities.
- `participants.cs` : Exports participant finance data for tax office according to GDPdU standards.

## Files
### accounts.cs

TGDPdUExportAccountsAndCostCentres implements functionality for exporting financial account and cost center data in a format suitable for German tax audits (GDPdU). The class provides methods to export cost centers and accounts to CSV files, filtering out certain types of cost centers like those related to personnel costs. Key methods include ExportCostCentres, ExportAccounts, WithoutPersonCostCentres, and GetDepartmentCostCentre. The code interacts with database tables to retrieve financial data and formats it according to specific requirements, handling character encoding and CSV formatting with appropriate separators.

 **Code Landmarks**
- `Line 46`: Recursive method to traverse cost center hierarchy to find department-level cost centers
- `Line 60`: Special filtering logic to exclude cost centers linked to personnel costs
- `Line 123`: Uses specific Windows-1252 encoding for German character compatibility in CSV output
- `Line 82`: Implements department categorization using application settings for summary cost centers
### accountspayable.cs

TGDPdUExportAccountsPayable implements functionality to export accounts payable data for tax office reporting according to GDPdU (German tax authority digital data access requirements). The main Export method retrieves posted or paid AP documents within a specified financial year and cost centers, along with their details, payments, and analysis attributes. It queries the database for invoice documents, their line items, payment information, and supplier details, then formats this data into a CSV file with proper encoding. The method handles both regular invoices and credit notes, tracking document IDs, dates, amounts, narratives, and payment information.

 **Code Landmarks**
- `Line 73`: Handles multiple document statuses (posted, partially paid, paid) in a single query
- `Line 147`: Special handling for multiple payments with concatenation of payment dates and IDs
- `Line 123`: Uses DataView sorting and FindRows for efficient relationship navigation between tables
- `Line 138`: Inverts amounts for credit notes by multiplying by -1
### balances.cs

TGDPdUExportBalances implements functionality for exporting financial data in the GDPdU format required by German tax authorities. The file contains a single class with a static method ExportGLBalances that generates a CSV file containing general ledger balances for specified cost centers in a given financial year. The method constructs an SQL query to retrieve cost center codes, account codes, start balances, and end balances from the database, filtering by ledger number, financial year, and cost centers while excluding specified accounts. The data is formatted and written to a balance.csv file with proper encoding.

 **Code Landmarks**
- `Line 52`: SQL query construction uses string formatting with numbered placeholders for database field names and parameters
- `Line 74`: Uses a delegate pattern with ReadTransaction for database access
- `Line 94`: Uses Windows-1252 encoding specifically for the output file, which is important for European character support
### main.cs

TGDPdUExport implements a command-line tool for exporting OpenPetra financial data in the GDPdU format required by German tax authorities. It connects to the Petra server and exports various financial data including GL transactions, balances, accounts payable, and participant information. The tool processes configuration settings to determine which cost centers and accounts to include/exclude, handles multiple financial years, and creates CSV files with proper formatting for tax reporting. Key functionality includes filtering data by cost centers, accounts, and transaction references, and organizing exports by financial year.

 **Code Landmarks**
- `Line 47`: Connects to Petra server using configuration file for database access
- `Line 58-65`: Configurable parameters allow flexible filtering of financial data through app settings
- `Line 78`: Special handling to exclude person cost centers from reporting
- `Line 83`: Sets culture-specific decimal and thousands separators for proper number formatting
- `Line 89`: Organizes exports by financial year in separate directories
### participants.cs

TGDPdUExportParticipants implements functionality to export posted invoices for conference and seminar participants to CSV format for tax reporting. The main Export method retrieves gift transactions from the database that match specified ledger number, financial year, and cost centers. It joins data from gift batches, gifts, gift details, and person tables to create comprehensive participant records. Each record includes transaction identifiers, amounts, dates, donor information, and cost center codes. The exported data is formatted according to GDPdU requirements and written to a CSV file with the specified separator and encoding.

 **Code Landmarks**
- `Line 54`: Uses database transaction pattern with delegate for complex multi-table query operations
- `Line 65`: Complex SQL query with string formatting to join multiple tables with specific filtering conditions
- `Line 146`: Creates unique transaction identifiers by combining batch, gift, and detail numbers with prefixes
### transactions.cs

TGDPdUExportTransactions implements functionality to export financial transactions in the GDPdU format required by tax authorities. The class provides methods to export GL transactions for specified financial years and cost centers, filtering by accounts and references. It handles transaction data retrieval, processes debit/credit indicators, calculates tax amounts based on analysis attributes, and exports the data to CSV files. The class also manages related transactions in journals and provides tax analysis attribute definitions. Key methods include ExportGLTransactions and GetTaxAnalysisAttributes, working with transaction tables, batches, and accounts.

 **Code Landmarks**
- `Line 51`: GetOtherTransactions method uses a sophisticated algorithm to find balancing transactions by tracking running balances until they reach zero
- `Line 95`: Complex SQL query construction with multiple joins and filters to retrieve financial transactions with specific criteria
- `Line 206`: Tax calculation logic applies different percentage rates based on analysis attribute values
- `Line 221`: Special handling for gift batches by enriching transaction narratives with account descriptions and batch information
- `Line 290`: Comprehensive list of tax codes with descriptions for German tax reporting requirements

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #