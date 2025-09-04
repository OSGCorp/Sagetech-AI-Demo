## C# Finance GDPdU Income Tax Export

The C# Finance GDPdU Income Tax Export is a specialized data export module within OpenPetra that enables non-profit organizations to comply with German tax authority (GDPdU) requirements. This subproject implements standardized financial data extraction and formatting capabilities to generate properly structured CSV files for tax reporting purposes. The module provides comprehensive export functionality for financial transactions, account structures, balances, and worker information.

### Key Technical Features:

- Standardized CSV export with configurable separators and proper encoding for German tax compliance
- Hierarchical cost center processing with filtering capabilities for personnel-related costs
- Transaction relationship tracking to ensure balanced financial reporting
- Comprehensive database querying across multiple financial tables (GeneralLedgerMaster, AccountTable, etc.)
- Worker data extraction based on commitment periods and site affiliations

### Identified Functional Areas:

1. **Transaction Export**: Retrieves and processes GL transactions with related entries and analysis attributes
2. **Account and Cost Center Export**: Manages hierarchical cost center structures and account information
3. **Balance Export**: Calculates and formats start/end balances for specified financial periods
4. **Worker Data Export**: Extracts staff information based on employment periods and office assignments
5. **Main Orchestration**: Coordinates the overall export process with configurable parameters

The architecture follows a modular approach with specialized classes for each export type, allowing for independent maintenance and extension. The system integrates with OpenPetra's database layer through TDBTransaction to ensure data consistency while providing flexible filtering options to meet specific reporting requirements.

## Business Functions

### Financial Data Export
- `transactions.cs` : Exports financial transaction data for tax authorities according to GDPdU standards, focusing on GL transactions for specified cost centers.
- `accounts.cs` : Exports account and cost center data for German tax reporting (GDPdU) in the OpenPetra finance system.
- `balances.cs` : Exports financial balance data in GDPdU format for tax reporting in the OpenPetra finance system.

### Personnel Data Export
- `workers.cs` : Exports worker data for tax office reporting according to GDPdU standards in OpenPetra's finance module.

### Export Orchestration
- `main.cs` : Tool for exporting financial data to German tax authorities in GDPdU format from OpenPetra.

## Files
### accounts.cs

TGDPdUExportAccountsAndCostCentres implements functionality for exporting financial accounts and cost centers data for German tax reporting (GDPdU). The class provides methods to export cost centers and accounts to CSV files, filtering out cost centers linked to personnel costs. Key methods include GetDepartmentCostCentre for navigating cost center hierarchies, WithoutPersonCostCentres for filtering cost centers not linked to personnel, ExportCostCentres for writing cost center data to CSV, and ExportAccounts for exporting account information. The code handles database access, data filtering, and CSV file generation with proper encoding.

 **Code Landmarks**
- `Line 45`: Recursive method to traverse cost center hierarchy to find department-level cost centers
- `Line 60`: Specialized filtering to exclude cost centers related to personnel costs based on naming conventions
- `Line 91`: Uses StringHelper.StrMerge to properly format CSV data with appropriate separators
### balances.cs

TGDPdUExportBalances implements functionality to export general ledger balances in a format compliant with GDPdU (German tax authority requirements). The class provides a static method ExportGLBalances that queries financial data from the database for a specific ledger, financial year, and cost centers, while excluding specified accounts. The method formats the data with start and end balances and writes it to a CSV file with configurable separators and encoding. The implementation uses SQL queries to retrieve data from multiple database tables including GeneralLedgerMaster and AccountTable, and handles database transactions through the TDBTransaction class.

 **Code Landmarks**
- `Line 52`: SQL query construction using string formatting with multiple table joins and parameter substitution
- `Line 76`: Database transaction pattern using delegate for data retrieval
- `Line 104`: Uses Windows-1252 encoding specifically for European character compatibility in CSV output
### main.cs

TGDPdUExport implements a tool for exporting financial data in the German GDPdU format for tax authorities. It connects to the OpenPetra server, retrieves financial data based on configurable parameters (ledger number, financial years, cost centers, accounts), and exports this data to CSV files. The main functionality includes exporting worker data, GL transactions, GL balances, cost centers, and accounts. Key methods include Main(), which orchestrates the export process by calling specialized export methods from other classes like TGDPdUExportWorkers, TGDPdUExportTransactions, TGDPdUExportBalances, and TGDPdUExportAccountsAndCostCentres.

 **Code Landmarks**
- `Line 48`: Uses TPetraServerConnector to establish connection with the OpenPetra server using a configuration file
- `Line 52`: Configurable export parameters retrieved from application settings with fallback default values
- `Line 70`: Filters cost centers and accounts based on configurable inclusion/exclusion rules
- `Line 79`: Sets culture-specific formatting for decimal and thousands separators
- `Line 84`: Organizes exported data in year-specific subdirectories
### transactions.cs

TGDPdUExportTransactions implements functionality for exporting financial transaction data to comply with German tax authority (GDPdU) requirements. The class provides methods to export GL transactions for specified cost centers, financial years, and ledger numbers while filtering out ignored accounts and references. The main method ExportGLTransactions retrieves transaction data from the database, processes it to include related transactions and analysis attributes, and exports it to a CSV file. The class handles transaction relationships by finding balancing entries through the GetOtherTransactions method, and tracks involved cost centers and accounts during processing.

 **Code Landmarks**
- `Line 45`: GetOtherTransactions method uses a sophisticated algorithm to find balancing transactions in the same journal by tracking running balance
- `Line 93`: Complex SQL query construction with multiple table joins and parameter substitution for filtering transactions
- `Line 188`: Special handling for gift batches with narrative enrichment from account descriptions
- `Line 207`: Custom transaction ID formatting combines batch, journal and transaction numbers for traceability
### workers.cs

TGDPdUExportWorkers implements functionality to export worker data for tax reporting according to GDPdU standards. The main Export method retrieves worker information from the database based on commitment periods within a specified year and site key. It queries staff data, partner information, and person details for workers associated with home or receiving field offices, then formats and exports this data to a CSV file named 'angestellte.csv'. The method handles date filtering to include workers active during the specified year and formats personal information with appropriate separators.

 **Code Landmarks**
- `Line 71`: SQL query uses complex date logic to find workers active during the specified year
- `Line 57`: Uses StringBuilder for efficient string concatenation when building CSV content
- `Line 143`: Exports data with specific encoding (1252) for compatibility with German tax systems

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #