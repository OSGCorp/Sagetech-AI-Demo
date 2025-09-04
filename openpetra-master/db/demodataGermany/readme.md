# Database Germany Demo Data Subproject Of OpenPetra

The Database Germany Demo Data subproject provides a comprehensive set of sample data files tailored for German implementations of OpenPetra. This subproject implements localized financial structures and partner data samples along with country-specific configurations for accounting, banking, and organizational management. This provides these capabilities to the OpenPetra program:

- German-specific chart of accounts with hierarchical structure
- Localized financial transaction types and accounting periods
- Sample partner data including organizations, families, and banks
- Currency configuration with EUR as base and exchange rates to USD

## Identified Design Elements

1. **Modular Data Organization**: Data is organized into distinct CSV files by functional domain (accounting, partners, publications) for maintainability
2. **Dynamic Date Handling**: Uses template variables like `${datetime::get-year(datetime::now())}` to ensure demo data remains relevant regardless of installation date
3. **Hierarchical Account Structure**: Implements a comprehensive chart of accounts with parent-child relationships for financial reporting
4. **Internationalization Support**: Includes multi-currency handling with EUR base and exchange rates to USD

## Overview
The architecture emphasizes a complete demonstration environment for German non-profit organizations using OpenPetra. The data structure follows standard accounting principles with localized adaptations for German requirements. The subproject includes initialization scripts that establish proper permissions, default site settings, and sample organizational structures. The demo data covers all major functional areas of OpenPetra including partner management, accounting, gift processing, publications, and supplier management, providing a realistic testing and training environment for German implementations.

## Business Functions

### Partner Management
- `p_partner.csv` : CSV file containing demo partner data for German locale with sample donors, organizations, and banks.
- `p_partner_location.csv` : CSV data file containing partner location records for the OpenPetra demo database with German sample data.
- `p_location.csv` : CSV data file containing location records for the German demo database in OpenPetra.
- `p_organisation.csv` : Demo data CSV file containing test company records for the German OpenPetra database.
- `p_family.csv` : CSV data file containing a single demo donor record for the German version of OpenPetra.

### Banking
- `p_bank.csv` : CSV file containing a single bank record for demo data in the German localization of OpenPetra.

### Accounting Configuration
- `a_valid_ledger_number.csv` : CSV file containing valid ledger number data for the German demo database in OpenPetra.
- `a_corporate_exchange_rate.csv` : Demo exchange rate data file for EUR to USD currency conversion in the OpenPetra system.
- `a_motivation_group.csv` : CSV data file containing motivation group definitions for the German demo database.
- `a_account_hierarchy_detail.csv` : CSV file defining the account hierarchy structure for the German demo data in OpenPetra's accounting system.
- `a_accounting_system_parameter.csv` : CSV file containing a single record of accounting system parameters for the German demo data.
- `a_cost_centre_types.csv` : CSV file containing cost center type data for German demo data in OpenPetra.
- `a_ledger.csv` : CSV file containing a single ledger record for the Germany demo database in OpenPetra.
- `a_transaction_type.csv` : CSV file containing transaction type definitions for the German demo data in OpenPetra's accounting system.
- `a_account_hierarchy.csv` : CSV file containing a single account hierarchy record for the German demo database.
- `a_account_property.csv` : CSV file containing demo account property data for German bank accounts in the OpenPetra system.
- `a_accounting_period.csv` : CSV file defining accounting periods for German demo data in OpenPetra.
- `a_system_interface.csv` : CSV file defining system interface records for the AP, GL, and GR modules in the OpenPetra demo data for Germany.
- `a_account.csv` : CSV file containing German account structure data for OpenPetra's accounting system.

### Database Initialization
- `patch-0.0.3-0.sql` : SQL patch script that adds table access permission for DEMO user to the p_partner_type table in OpenPetra.
- `init.sql` : SQL initialization script for OpenPetra demo database with German localization, setting up users, permissions, and sample data.

### Human Resources
- `um_job.csv` : Demo data CSV file containing job position records for the German demo database.

### Supplier Management
- `a_ap_supplier.csv` : CSV file containing demo supplier data for the German instance of OpenPetra.

### Publications
- `p_publication.csv` : Demo data CSV file containing publication types for the German OpenPetra instance.

### Fees Management
- `a_fees_payable.csv` : CSV data file containing fee definitions for the German demo database in OpenPetra.
- `a_motivation_detail_fee.csv` : CSV file containing motivation detail fee data for the German demo database in OpenPetra.
- `a_fees_receivable.csv` : CSV data file containing fee receivable configurations for the German demo dataset in OpenPetra.

## Files
### a_account.csv

This CSV file defines the account structure for the German implementation of OpenPetra's accounting system. It contains comprehensive account definitions with over 180 entries covering income, expense, asset, liability, and equity accounts. Each record includes account codes, descriptions in multiple languages, account types, and various configuration flags that determine account behavior. The file organizes accounts into hierarchical structures with parent-child relationships (indicated by 'S' suffix for summary accounts), and includes standard accounting categories like income (gifts, sales, interest), expenses (ministry, administration, personnel), assets (cash, receivables, equipment), and liabilities. The data includes configuration for account visibility, posting permissions, and reporting structures.

 **Code Landmarks**
- `Line 1-180`: Hierarchical account structure with summary accounts (marked with 'S' suffix) that aggregate child accounts
- `Line 1-43`: Income accounts (0100-3740) organized by source type including support gifts, fund gifts, and various income categories
- `Line 44-125`: Expense accounts (4100-5601) categorized by function including ministry, administration, personnel, and financial expenses
- `Line 126-156`: Asset and liability accounts (6000-9800) following standard accounting structure with proper classification
### a_account_hierarchy.csv

This CSV file contains a single record for the account hierarchy table in the German demo database. The record has the ID 43, type 'STANDARD', account hierarchy number 43, a date of June 15, 2009, and four empty fields marked with question marks. This appears to be part of the demo data setup for the German version of OpenPetra's accounting system.
### a_account_hierarchy_detail.csv

This CSV file contains the account hierarchy detail data for the German demo implementation of OpenPetra. It defines the relationships between different account codes in a standard chart of accounts, organizing them into a hierarchical structure. Each row represents an account relationship with fields for ledger number (43), account system (STANDARD), account code, parent account code, sort order, creation date, and placeholder fields. The file establishes the financial reporting structure by defining how accounts roll up into summary accounts, with categories like GIFT, CASH, ASSETS, LIABILITIES, INCOME, and EXPENSES forming the backbone of the accounting system.

 **Code Landmarks**
- `Line 1`: Uses ledger number 43 consistently for all German demo accounts
- `Line 2`: Implements parent-child relationships between accounts using account codes as identifiers
- `Line 43`: Establishes a multi-level hierarchy with summary accounts (ending with 'S') that aggregate detail accounts
### a_account_property.csv

This CSV file contains sample account property data for the German demo database in OpenPetra. It defines three account properties with their respective values: two bank accounts (6200 and 6210) and a special ICH (International Clearing House) account (8500). Each record includes account ID, property code, property name, value, and date fields, with some fields left empty (marked with '?').

 **Code Landmarks**
- `Line 3`: Special account property 'Is_Special_Account' with value 'ICH_ACCT' identifies an International Clearing House account, which is important for currency exchange operations
### a_accounting_period.csv

This CSV file contains demo accounting period data for the German version of OpenPetra. It defines monthly accounting periods for two consecutive years, with each record including a ledger ID (43), period number, month name, start date, end date, and creation date. The dates are dynamically calculated using datetime functions to ensure the periods are relevant to the current year and next year. February's end date is specially handled to account for leap years. Each record contains placeholder values (?) for unspecified fields.

 **Code Landmarks**
- `Line 2`: Uses dynamic date calculation with ${datetime::get-days-in-month()} to handle February's variable length in leap years
- `Line 1`: Template uses ${datetime::get-year(datetime::now())} to dynamically set accounting periods relative to current year
- `Line 13`: Transitions from current year to next year's accounting periods at line 13
### a_accounting_system_parameter.csv

This CSV file contains a single record of accounting system parameters for the German demo database in OpenPetra. The record includes various numeric parameters (20,11,2,2,43) followed by several empty fields and an 8, then a date (2009-6-15) and four placeholder values marked with question marks. These parameters likely configure the accounting system's behavior for the German localization of OpenPetra.

 **Code Landmarks**
- `Line 1`: The file uses a specific format where empty fields are represented by consecutive commas, indicating null or default values in the database schema.
### a_ap_supplier.csv

This CSV file contains demo supplier data for the German instance of OpenPetra. It includes records with supplier IDs, account codes, payment types, currencies, and other financial parameters. Each row represents a supplier with attributes like payment terms, currency, account codes for various financial operations, creation dates, and status indicators. The file serves as sample data for testing and demonstrating the supplier management functionality in OpenPetra.

 **Code Landmarks**
- `Line 1-2`: The file uses a specific format with supplier ID as the first field, followed by account codes, payment method (CASH), and currency designations (EUR, GBP)
### a_corporate_exchange_rate.csv

This CSV file contains sample exchange rate data for the OpenPetra system's German demo environment. It defines monthly EUR to USD exchange rates for a two-year period, alternating between 1.20 and 1.35 rates. Each record includes the source currency (EUR), target currency (USD), exchange rate value, effective date (using dynamic date expressions based on the current year), a status flag (0), creation date (2014-05-21), and a source identifier (DEMO). The file uses template variables like ${datetime::get-year(datetime::now())} to ensure the demo data remains relevant regardless of when the system is installed.

 **Code Landmarks**
- `Line 1-24`: Uses dynamic date expressions with ${datetime::get-year(datetime::now())} to generate relevant dates based on the current year
- `Line 1-24`: Alternates exchange rates between 1.20 and 1.35 to simulate realistic currency fluctuations
### a_cost_centre_types.csv

This CSV file contains demo data for cost center types in the German localization of OpenPetra. It defines two basic cost center types - 'Local' and 'Foreign' - both with ID 43. The file uses a simple comma-separated format with nine fields per record, where most fields contain placeholder values (?) indicating default or undefined values. Only the ID, name, and a boolean value (false) are explicitly defined for each record.

 **Code Landmarks**
- `Line 1-2`: Both cost center types share the same ID (43), which may indicate a parent-child relationship or categorization system in the database schema.
### a_fees_payable.csv

This CSV file contains demo data for fees payable in the German version of OpenPetra. It defines four different fee types (GIF, GIF2, GIF3, GIF4) with various calculation methods (Percentage, Fixed, Minimum, Maximum) and their associated values. Each record includes fee code, description, calculation method, percentage rate, fixed amount, account codes, and fee description. The data represents different fee structures that would be used in the accounting system for the Global Impact Fund.

 **Code Landmarks**
- `Line 1-4`: The file uses a specific format with 13 fields per record, with question marks (?) representing null or empty values
### a_fees_receivable.csv

This CSV file contains configuration data for fees receivable in the German demo dataset. It defines two fee types: a percentage-based 'HO_ADMIN' fee at 7% and a fixed 'HO_ADMIN2' fee of 10.00. Each record includes the fee code, type, rate, amount, account codes for debiting and crediting, description, and several placeholder fields. These configurations likely support OpenPetra's accounting functionality for non-profit organizations.

 **Code Landmarks**
- `Line 1-2`: The file uses a combination of percentage-based and fixed-amount fee structures, demonstrating OpenPetra's flexibility in handling different fee calculation methods.
### a_ledger.csv

This CSV file contains a single record for the Germany ledger in the OpenPetra demo database. It defines ledger ID 43 with country 'Germany', currency EUR, and country code DE. The record includes various financial configuration flags and settings such as fiscal year settings, reporting periods, and transaction-related parameters. This data serves as the foundation for the German version of the demo database, establishing the basic accounting structure for the application.

 **Code Landmarks**
- `Line 1`: The ledger ID (43) and country code (DE) are key identifiers for the German implementation of the accounting system.
- `Line 1`: The record uses EUR as currency with numerous boolean flags (mostly false) controlling ledger behavior and capabilities.
- `Line 1`: Contains timestamp '2009-06-15' which likely represents the ledger creation or initialization date.
### a_motivation_detail_fee.csv

This CSV file contains motivation detail fee data for the German demo database in OpenPetra. It defines relationships between gift types (GIFT) and their categories (FIELD, SUPPORT) with corresponding cost centers (GIF, HO_ADMIN). The file uses a simple comma-separated format with nine columns, though only the first four contain actual data while the remaining columns are placeholders marked with question marks.

 **Code Landmarks**
- `Line 1-4`: The file uses a consistent pattern of gift type, category, and cost center mappings that likely define how donations are categorized in the German implementation of OpenPetra.
### a_motivation_group.csv

This CSV file defines motivation groups for the German demo database in OpenPetra. It contains two records for standard gift categories: 'GIFT' (Standard Gifts) and 'DONATION' (Donations). Each record includes fields for ID, code, description, active status, a text description, tax-deductible status, creation date, and several placeholder fields marked with question marks. These motivation groups likely categorize different types of financial contributions in the system.

 **Code Landmarks**
- `Line 1-2`: The file uses a simple CSV structure with question marks (?) as placeholders for optional or undefined fields
### a_system_interface.csv

This CSV file contains system interface configuration data for the German demo database in OpenPetra. It defines three interface records (all with ID 43) for the Accounts Payable (AP), General Ledger (GL), and Gift Receiving (GR) modules. Each record is marked as active (true) with an effective date of June 15, 2009. The remaining fields are left undefined with placeholder question marks, suggesting these are optional configuration parameters.

 **Code Landmarks**
- `Line 1-3`: All three interface records share the same ID (43), suggesting they may be part of a related configuration group or subsystem.
### a_transaction_type.csv

This CSV file defines transaction types for the German demo database in OpenPetra. It contains six predefined transaction types including AP invoices, GL allocations, reallocations, foreign exchange revaluations, standard journals, and gift processing. Each record includes ledger code, transaction type code, debit/credit account codes, description, active status, and date fields. These transaction types form the foundation for financial operations in the German implementation of OpenPetra.

 **Code Landmarks**
- `Line 1`: Uses ledger code 43 consistently across all transaction types, indicating a specific accounting entity for German operations
- `Line 4`: Foreign Exchange Revaluation transaction uses specific account code 5003 rather than the generic BAL SHT used by other transaction types
- `Line 5`: Standard Journal is the only transaction type marked as inactive (false), suggesting it may be a template
### a_valid_ledger_number.csv

This CSV file contains ledger configuration data for the German demo database in OpenPetra. It defines four ledger entries with ID 43, each with different account numbers (4000000, 35000000, 73000000, 95000000), a common base account (4000000), and different cost center codes (0400, 3500, 7300, 9500). All entries share the same date (2009-6-15) and have undefined values for the last four fields. This data likely represents financial account structures used in the German implementation of OpenPetra's accounting system.

 **Code Landmarks**
- `Line 1-4`: Uses a consistent pattern of ledger ID 43 with varying account numbers and cost centers, suggesting a standardized accounting structure for the German implementation
### init.sql

This SQL initialization script sets up a demo database for OpenPetra with German localization. It creates a DEMO user with comprehensive permissions across multiple modules, establishes the German site (43000000) as the default, and configures essential organizational structures including the main organization, International Clearing House, and Global Impact Fund. The script imports data for the accounting system including ledgers, cost centers, accounts, accounting periods, and transaction types. It also sets up sample partners (donors and suppliers), banking details, and a demo conference for online registration testing. The file uses placeholders for password hashes and paths to CSV files that are replaced during the build process.

 **Code Landmarks**
- `Line 2`: Uses placeholders for password hashes that get replaced during build process
- `Line 73`: Imports data from CSV files with paths containing placeholders that are replaced at build time
- `Line 143`: Creates sample bank account matching a specific format in demo bank statement files
### p_bank.csv

This CSV file contains a single record of demo bank data for the German localization of OpenPetra. The record includes fields for bank ID (43005004), bank name (Test Bank), account number (3020010020), and several placeholder fields marked with question marks. This data is likely used for testing or demonstration purposes in the German version of the OpenPetra non-profit management system.

 **Code Landmarks**
- `Line 1`: Uses a specific format with question mark placeholders for optional or undefined fields in the bank record structure
### p_family.csv

This CSV file contains sample data for the p_family table in the German demo database for OpenPetra. It includes a single record with ID 43005001 representing a test donor named 'Mr Test Donor' with minimal personal information. The record includes fields for family key, family name, title, first name, family category, date entered, and other demographic data, most of which are marked with placeholder values.

 **Code Landmarks**
- `Line 1`: Uses a specific ID range (43005001) that appears to be designated for demo data in the system
### p_location.csv

This file contains demo location data for the German version of OpenPetra. It consists of comma-separated values representing location records with fields for address information including street, city, postal code, and country. The file appears to contain a single sample record with placeholder values like 'Somewhere 5' for street address and 'Anywhere' for city. The record includes a postal code '12311', country code '99', and is marked as created by 'DEMO' on '2012-04-02'.

 **Code Landmarks**
- `Line 1`: Uses question marks as placeholders for empty or null values in the CSV format
### p_organisation.csv

This CSV file contains demo organization data for the German version of OpenPetra. It includes two test company records with identifiers, names, types, and creation dates. Each record contains fields for organization ID, name, type, various boolean flags, creation date, and several placeholder fields marked with question marks. The data appears to be sample entries for testing the system with different currency configurations (EUR and GBP).

 **Code Landmarks**
- `Line 1-2`: Contains test organizations specifically labeled for different currencies (EUR and GBP), suggesting OpenPetra's multi-currency support capabilities
### p_partner.csv

This CSV file contains sample partner data for the German demo environment in OpenPetra. It defines four demo partners: a test donor family, two test companies (one using EUR and one using GBP currencies), and a test bank. Each record includes partner key, class, category, short name, status, creation date, and various flags for communication preferences. The data serves as initial test data for the partner management module.

 **Code Landmarks**
- `Line 1`: Partner records use a specific ID range (43005001-43005004) likely reserved for demo data
### p_partner_location.csv

This CSV file contains sample partner location data for the OpenPetra demo database with German data. Each row represents a location record with fields for partner ID, location type (HOME/BUSINESS), dates, and system flags. The file includes four sample records (43005001-43005004) with minimal data populated, primarily showing partner IDs, creation dates, location types, and the 'DEMO' user identifier. Question marks indicate null or empty values in the dataset.

 **Code Landmarks**
- `Line 1-4`: The file uses a consistent pattern of question marks (?) to represent null values in the CSV format rather than leaving fields empty
### p_publication.csv

This CSV file contains demo data for publications in the German OpenPetra installation. It defines two publication types: ANNUALREPORT (sent to all supporters annually) and NEWSUPDATES (quarterly updates via letter or email). Each record includes fields for publication code, description, frequency, and several placeholder fields marked with question marks. This data would be imported into the p_publication database table during system setup.

 **Code Landmarks**
- `Line 1`: Uses a specific CSV format with question marks as placeholders for optional or undefined fields
### patch-0.0.3-0.sql

This SQL patch file (version 0.0.3-0) contains a single INSERT statement that grants the DEMO user access permission to the p_partner_type table in the OpenPetra database. This is part of the demo data for Germany, ensuring that the demonstration user has appropriate access rights to partner type information in the system.

 **Code Landmarks**
- `Line 1`: Part of OpenPetra's incremental database patching system, indicated by the version numbering in the filename
### um_job.csv

This CSV file contains sample job position data for the German demo database in OpenPetra. Each record represents a job position with fields including job code, status, ID, term type, start/end dates, various boolean flags for job requirements, notice periods, and other employment conditions. The data includes positions like webmaster, accountant, manager, receptionist, and system administrator with their respective employment terms and conditions.

 **Code Landmarks**
- `Line 1`: Uses numeric partner key (1000000) as the first field for all records, likely representing the organization ID
- `Line all`: Question marks (?) are used as placeholders for null/empty values, particularly for open-ended employment dates
- `Line all`: Boolean values are represented as 'true'/'false' or as binary 0/1 values depending on the field

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #