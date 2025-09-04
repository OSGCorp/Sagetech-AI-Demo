# C# Petra SQL Subproject Of OpenPetra

The OpenPetra is a C# program that helps non-profit organizations manage administration tasks and reduce operational costs. The program handles financial transactions and maintains comprehensive partner records. This sub-project implements SQL database queries and reference data structures that power OpenPetra's data access layer. This provides these capabilities to the OpenPetra program:

- Structured data retrieval through parameterized SQL queries
- Financial transaction management (gifts, donations, accounting)
- Partner relationship and contact management
- Reference data configuration through YAML templates
- International Clearing House (ICH) transaction support
- Conference and personnel management data access

## Identified Design Elements

1. **Modular Query Organization**: Queries are organized by functional domain (Finance, Partner, Gift, Personnel, Conference) for maintainability
2. **Parameterized Queries**: SQL uses placeholders (? or ##placeholder##) for dynamic parameter substitution
3. **Conditional Query Building**: Many queries contain preprocessor directives for conditional inclusion of SQL clauses
4. **Hierarchical Reference Data**: YAML configuration files define standard hierarchies for accounts, cost centers, and motivations
5. **Extract Pattern**: Common pattern of extracting partner information based on various criteria for reporting and processing

## Overview
The architecture emphasizes reusable SQL components that support OpenPetra's business logic. Queries are designed to handle complex relationships between partners, gifts, financial transactions, and organizational structures. The reference data files provide standardized templates for accounting structures and financial categorization. The SQL components support both operational queries and reporting functions, with special attention to international financial operations through the ICH subsystem.

## Business Functions

### Partner Management
- `Partner.Queries.ExtractByPartnerCity.sql` : SQL query to extract partner data filtered by city with active status and valid mailing address.
- `Partner.Queries.ExtractFamilyForPersons.sql` : SQL query to extract family partner records for persons in an extract table.
- `Partner.CommonAddressTools.GetBestAddress.sql` : SQL query that retrieves the best address for each partner from a list of donors.
- `Partner.Queries.ExtractPartnerByGeneralCriteria.sql` : SQL query for extracting partner data based on various filtering criteria in OpenPetra.
- `Partner.Queries.ExtractPartnerByEventRole.sql` : SQL query to extract partners by event role based on short-term application criteria.
- `Partner.Queries.ExtractPartnerByField.WorkerField.sql` : SQL query for extracting partner data based on worker field criteria
- `Partner.Queries.ExtractPartnerByField.Commitment.sql` : SQL query for extracting partner data based on commitment criteria in OpenPetra.
- `DataCheck.MPartner.CountryCode99.sql` : SQL query to identify partner locations with invalid country codes in the OpenPetra system.
- `Partner.Queries.ExtractByPartnerRelationship.sql` : SQL query for extracting partner relationships in OpenPetra's partner management system.
- `Partner.CommonAddressTools.GetBestAddressSanitized.sql` : SQL query that retrieves the best address for partners from the OpenPetra database based on specific criteria.
- `Partner.Queries.ExtractPartnerByCommitment.sql` : SQL query to extract partner data based on commitment criteria from OpenPetra's database.
- `Partner.Queries.ExtractFromExtractByPartnerRelationship.sql` : SQL query to extract partner relationships from a database based on specified criteria.
- `Partner.Queries.ExtractFamilyMembers.sql` : SQL query to extract family members from a partner database based on an extract ID.
- `Partner.Queries.ExtractPartnerByContactLog.sql` : SQL query for extracting partners based on contact log criteria with flexible filtering options.
- `Partner.Queries.ExtractPartnerByEvent.sql` : SQL query to extract partners by event based on short-term application status and filtering criteria.
- `Partner.Queries.ExtractPartnerBySubscription.sql` : SQL query to extract partners based on their publication subscriptions with various filtering options.
- `Partner.Queries.ExtractByPartnerSpecialType.sql` : SQL query to extract partners by special type with filtering options

### Financial Management
- `Finance.TypeAheadMotivationDetail.sql` : SQL query for retrieving motivation detail records with type-ahead filtering capabilities.
- `Gift.GetGiftsToAdjustField.sql` : SQL query to retrieve gift details for adjustment based on specific criteria in the OpenPetra system.
- `ICH.HOSAReportGiftSummary.sql` : SQL query for HOSA report gift summary with conditional filtering options for personal HOSA and period-based reporting.
- `BankImport.GetBankAccountByDate.sql` : SQL query to retrieve bank account details for gifts on a specific date in the OpenPetra system.
- `Finance.TypeAheadCostCentreCode.sql` : SQL query for cost centre code type-ahead search in OpenPetra's finance module.
- `Gift.ReceiptPrinting.GetDonationsOfDonor.sql` : SQL query to retrieve donor gift details for receipt printing in OpenPetra.
- `ICH.HOSAExportGLTrans.sql` : SQL query for exporting GL transactions from OpenPetra's International Clearing House system.
- `Gift.ReceiptPrinting.GetDonors.sql` : SQL query for retrieving donor information for gift receipt printing in OpenPetra.
- `Gift.GetDonationsOfDonorAndOrRecipientTemplate.sql` : SQL query template for retrieving donor gift information with filtering options.
- `BankImport.GetDonationsByDate.sql` : SQL query to retrieve donation details by date for bank import matching
- `Finance.TypeAheadAccountCode.sql` : SQL query for account code type-ahead search functionality in OpenPetra's finance module.
- `Finance.TypeAheadMotivationGroup.sql` : SQL query for retrieving motivation group codes and descriptions with filtering and sorting capabilities.

### Gift Management
- `Gift.Queries.ExtractRecipientByField.sql` : SQL query to extract distinct gift recipients by field with filtering options
- `ICH.HOSAExportGifts.sql` : SQL query for selecting gift transactions to export from the general ledger for the ICH HOSA export process.
- `Gift.Queries.ExtractDonorByMiscellaneous.sql` : SQL query for extracting donor information based on various gift-related criteria in OpenPetra.
- `Gift.Queries.ExtractDonorByMotivation.sql` : SQL query to extract donors by motivation criteria from the OpenPetra gift database.
- `Gift.Queries.ExtractDonorByAmount.sql` : SQL query to extract donor information based on gift amounts with various filtering options.
- `ICH.HOSAExportGiftsInner.sql` : SQL query for exporting gift transactions in OpenPetra's International Clearing House system.
- `Gift.Queries.ExtractDonorByField.sql` : SQL query to extract donor information from gift records with various filtering options.

### Personnel Management
- `Personnel.Reports.AllCommitments.sql` : SQL query to retrieve all active staff commitments with their personal and assignment details.
- `Personnel.Reports.Birthday.sql` : SQL query for generating birthday reports from the Personnel database with various filtering options.
- `Personnel.Reports.GetFamilyKeyOfPerson.sql` : SQL query to retrieve family keys associated with persons based on different filtering criteria.

### Conference Management
- `Conference.ReportHeadsetsForSession.sql` : SQL query to retrieve headset rental information for conference sessions.
- `Conference.GetAllParticipantsWithRoleAndCountry.sql` : SQL query to retrieve conference participants with their roles and countries.

### System Configuration
- `DefaultMotivations.yml` : YAML configuration file defining default gift and donation motivations with their descriptions and account codes.
- `DefaultAccountHierarchy.yml` : YAML file defining the default account hierarchy structure for OpenPetra's accounting system.
- `DefaultAdminGrantsPayableReceivable.yml` : YAML configuration file defining default administrative grants for payable and receivable fees in OpenPetra.
- `DefaultCostCentreHierarchy.yml` : YAML template defining the default cost centre hierarchy structure for OpenPetra ledgers.

## Files
### BankImport.GetBankAccountByDate.sql

This SQL file contains a query that retrieves bank account information for donors who made gifts on a specific date. It joins multiple tables including gift batches, gifts, banking details, partner banking details, and bank information to fetch comprehensive banking information. The query filters by ledger number and effective date, excluding cancelled batches. It returns banking details along with partner keys and bank sort codes. The file includes commented-out code related to banking details usage types that appears to be alternative filtering logic for different types of banking relationships.

 **Code Landmarks**
- `Line 3`: The query joins five different tables to correlate gift transactions with banking information.
- `Line 7`: Uses parameterized queries with '?' placeholders for ledger number and date values.
- `Line 10`: Contains extensive commented-out code suggesting alternative filtering logic for banking details usage types.
### BankImport.GetDonationsByDate.sql

This SQL query retrieves donation details for bank import matching. It joins multiple tables (gift batches, gifts, gift details, and partner information) to fetch comprehensive donation data for a specific ledger and date. The query includes donor information, recipient details, and flags for matching status. It filters out cancelled batches and modified details, ensuring only valid donations are returned for the bank import reconciliation process.

 **Code Landmarks**
- `Line 1`: Uses table aliasing with the same table (PUB_p_partner) to get both donor and recipient names in a single query
- `Line 2`: Includes a hardcoded 'false' as AlreadyMatched column to indicate these donations haven't been matched to bank statements yet
- `Line 8`: Filters by effective date using a parameterized query (?) for security and flexibility
### Conference.GetAllParticipantsWithRoleAndCountry.sql

This SQL query retrieves conference participant information by joining multiple tables. It selects participant partner keys, short names, fellowship groups, roles, and countries from short-term applications, general applications, units, and partner tables. The query filters by a specified confirmation option code parameter and only includes approved applications ('A' status). It establishes relationships between participants, their applications, and registration offices to provide a comprehensive view of conference participants with their roles and countries.

 **Code Landmarks**
- `Line 1`: Uses parameterized query with '?' placeholder for confirmation option code to prevent SQL injection
### Conference.ReportHeadsetsForSession.sql

This SQL file defines a query for retrieving headset rental information for conference sessions in OpenPetra. It joins multiple tables to fetch data about partners who have rented or returned headsets for a specific session. The query returns partner keys, session names, rental status, short names, fellowship groups, roles, and countries. It filters results based on headset rental status codes, a specific session parameter, and a confirmation option parameter.

 **Code Landmarks**
- `Line 4-10`: Selects fields from multiple tables to create a comprehensive view of headset rental information including participant details.
- `Line 12`: Uses specific contact codes 'HEADSET_OUT' and 'HEADSET_RETURN' to track the rental status of headsets.
- `Line 15-16`: Uses parameterized queries (?) for session name and confirmation option to make the query reusable.
### DataCheck.MPartner.CountryCode99.sql

This SQL file contains a query that identifies partner locations with invalid country codes (specifically '99') that have the mail flag enabled. The query joins three tables (PUB_p_partner_location, PUB_p_location, and PUB_p_partner) to retrieve partner keys, location keys, partner names, street names, and cities where the send_mail flag is true but the country code is invalid. This helps administrators identify and fix data integrity issues in the partner location records.

 **Code Landmarks**
- `Line 3`: Uses multiple table joins to correlate partner data with location information for comprehensive error reporting
- `Line 12`: Specifically targets country code '99' which appears to be a placeholder for invalid or missing country information
### DefaultAccountHierarchy.yml

This YAML file defines the default account hierarchy structure for OpenPetra's accounting system. It establishes a comprehensive chart of accounts organized in a hierarchical tree structure starting with a root node 'BAL SHT' (Balance Sheet). The hierarchy includes major sections for Assets, Liabilities, and Equity, with detailed sub-accounts for cash, investments, debtors, creditors, income, and expenses. Each account entry contains attributes such as active status, account type (Asset, Liability, Equity, Income, Expense), debit/credit nature, valid cost centers, and descriptions. The file serves as a template for creating standardized accounting structures in OpenPetra implementations.

 **Code Landmarks**
- `Line 1`: Uses hierarchical indentation to represent parent-child relationships between accounts
- `Line 3`: Implements account attributes through key-value pairs including type, debitcredit, validcc, and descriptions
- `Line 6`: Special account types are marked with flags like 'bankaccount=true' for specialized processing
### DefaultAdminGrantsPayableReceivable.yml

This YAML configuration file defines default administrative grants for payable and receivable fees in OpenPetra. It contains commented-out examples of fee configurations for organizations (ABC and XYZ) with parameters including fee type (payable), charge option (percentage), percentage rate (1%), cost center code, account code, description, and debit account code. These configurations would likely be used to set up default administrative fee structures in the system.

 **Code Landmarks**
- `Line 2-3`: Contains commented example configurations that serve as templates for fee setup with complete parameter sets
### DefaultCostCentreHierarchy.yml

This YAML configuration file defines the default cost centre hierarchy structure for OpenPetra ledgers. It establishes a simple three-level hierarchy template with placeholders that get populated with actual ledger information during system operation. The file uses special placeholder variables (enclosed in {#}) that are replaced at runtime with actual ledger numbers, names, and formatted values to create the appropriate cost centre structure for each ledger in the system.

 **Code Landmarks**
- `Line 2`: Uses placeholder variables with {#} syntax for dynamic content insertion at runtime
- `Line 2-4`: Implements a three-level hierarchical structure for cost centres with root node, summary level, and general level
### DefaultMotivations.yml

DefaultMotivations.yml defines standard categories for gifts and donations in OpenPetra. It organizes motivations into two main groups: GIFT and DONATIONS, each containing identical subcategories (FIELD, KEYMIN, PERSONAL, SUPPORT, UNDESIG). Each motivation includes a description and associated account code for financial tracking. This configuration file provides the default motivation structure used by the system for categorizing financial contributions.

 **Code Landmarks**
- `Line 1-14`: Uses a hierarchical YAML structure to define financial motivation categories that likely populate database tables or application dropdown menus
### Finance.TypeAheadAccountCode.sql

This SQL file contains a parameterized query for the Finance module's type-ahead functionality when searching for account codes. It retrieves account codes and their short descriptions from the PUB_a_account table based on matching criteria. The query filters by ledger number and searches across account code, short description, and long description fields. It includes an optional filter for posting status and orders results by account code.

 **Code Landmarks**
- `Line 4`: Uses parameterized queries (?) for security against SQL injection attacks
- `Line 5`: Implements flexible search using LIKE operator across multiple fields for better user experience
- `Line 6`: Contains conditional logic (NOT ? OR) to optionally filter by posting status
### Finance.TypeAheadCostCentreCode.sql

This SQL query retrieves cost centre codes and names from the PUB_a_cost_centre table based on a specified ledger number. It filters results where either the code or name matches a search pattern and optionally restricts to posting cost centres only. Results are ordered by cost centre code for consistent display in type-ahead interfaces.

 **Code Landmarks**
- `Line 3`: Uses parameterized queries (?) for security against SQL injection attacks
- `Line 4`: Implements flexible search with LIKE operator on both code and name fields
- `Line 5`: Contains conditional filtering logic for posting cost centres using boolean parameter
### Finance.TypeAheadMotivationDetail.sql

This SQL file contains a query that retrieves motivation detail records from the PUB_a_motivation_detail table for a specific ledger. It filters records where either the motivation detail code or description matches a pattern (used for type-ahead functionality). The query returns motivation group code, detail code, description, and boolean flags for membership, sponsorship, and worker support. Results are ordered by motivation group code and detail code.

 **Code Landmarks**
- `Line 3`: Uses parameterized queries (?) for security against SQL injection when filtering by ledger number and search patterns
### Finance.TypeAheadMotivationGroup.sql

This SQL query retrieves motivation group codes and descriptions from the PUB_a_motivation_group table for a specific ledger. It filters records based on the ledger number and searches for partial matches in either the motivation group code or description fields using LIKE operators with wildcards. Results are sorted alphabetically by motivation group code to facilitate typeahead functionality in the Finance module of OpenPetra.

 **Code Landmarks**
- `Line 1-7`: Implements typeahead functionality by using LIKE operators with wildcards for partial text matching on two different fields.
### Gift.GetDonationsOfDonorAndOrRecipientTemplate.sql

This SQL query template retrieves donation details from the OpenPetra gift system. It joins multiple tables (a_gift_batch, a_gift, a_gift_detail, p_partner) to extract comprehensive information about donations, including amounts, dates, receipt numbers, and recipient details. The query supports filtering by ledger number, donor key, recipient key, and date range through parameterized inputs. Results are ordered by date entered in descending order. The query only returns posted gift batches and includes fields for both financial data and descriptive information about each donation.

 **Code Landmarks**
- `Line 28-31`: Uses boolean parameter pattern (? OR field = ?) to make filters optional
- `Line 30`: Date range filtering with timestamp casting to ensure full day coverage
- `Line 7-8`: Tracks both local and international gift amounts for multi-currency support
### Gift.GetGiftsToAdjustField.sql

This SQL query retrieves gift details from the OpenPetra database that need adjustment. It joins three tables (PUB_a_gift_batch, PUB_a_gift, PUB_a_gift_detail) and filters by ledger number, date range, posted batch status, recipient key, recipient ledger number, and only includes unmodified details. The query uses parameterized inputs (indicated by ? placeholders) for ledger number, date range boundaries, recipient key, and recipient ledger number.

 **Code Landmarks**
- `Line 1-15`: Uses parameterized queries (? placeholders) for security against SQL injection when filtering data
### Gift.Queries.ExtractDonorByAmount.sql

This SQL query file extracts donor information from the OpenPetra database based on gift amounts. It joins multiple tables (gift_batch, gift, gift_detail, and partner) to retrieve donor keys, names, ledger numbers, batch information, gift transaction details, and gift amounts. The query includes parameterized filters for date ranges, first-time gifts, active partners, family class partners, and solicitation preferences. It contains placeholder sections (##address_filter_fields##, ##address_filter_tables##, ##address_filter_where_clause##) that can be dynamically replaced to include address filtering functionality. Results are ordered by donor key and gift transaction details.

 **Code Landmarks**
- `Line 9`: Uses placeholder tokens (##address_filter_fields##) that can be replaced dynamically before query execution
- `Line 14`: Implements parameterized queries with ? placeholders for secure and flexible filtering
- `Line 20`: Filters for first-time gifts with conditional logic (NOT ? OR pub_a_gift.a_first_time_gift_l)
### Gift.Queries.ExtractDonorByField.sql

This SQL file defines a query for extracting donor information from the gift system. It joins gift, gift detail, and partner tables to retrieve donor keys and names based on various filtering criteria. The query supports filtering by ledger number, date ranges, first-time gifts, receipt preferences, partner status, partner class, solicitation preferences, and receipt letter frequency. The query includes placeholder sections for additional address filtering that can be dynamically replaced during execution. Results are ordered by partner short name with optional address-based ordering.

 **Code Landmarks**
- `Line 3`: Uses placeholder tokens (##address_filter_fields##) that can be replaced dynamically at runtime
- `Line 7`: Complex conditional logic using parameter placeholders (?) for flexible query execution
- `Line 22`: Additional placeholder sections for address filtering that can be injected into the query
### Gift.Queries.ExtractDonorByMiscellaneous.sql

This SQL file contains a query for extracting donor information from the OpenPetra database based on various gift-related criteria. It performs a complex SELECT operation joining multiple tables including gift batches, gifts, gift details, motivation details, and partner information. The query filters results based on numerous conditional parameters including ledger numbers, recipient keys, mailing codes, receipt requirements, date ranges, gift types, and donor characteristics. The query includes placeholder sections for address filtering that can be dynamically replaced during execution. Results are returned with donor keys and names, ordered by partner short name.

 **Code Landmarks**
- `Line 3`: Uses ##address_filter_fields## placeholder for dynamic SQL generation
- `Line 6`: Multiple joined tables with complex conditional logic using parameter placeholders
- `Line 10`: Custom comparison operator placeholder ##equals_or_like_mailing_code## for flexible matching
- `Line 22`: Receipt letter code placeholder allows for custom receipt filtering logic
- `Line 37`: Address filtering placeholders at multiple points enable customizable location-based filtering
### Gift.Queries.ExtractDonorByMotivation.sql

This SQL file contains a query for extracting donor information based on gift motivation criteria. It selects distinct partner keys and names from multiple tables including gifts, gift details, motivation details, and partners. The query filters results based on parameters for ledger number, mailing code, motivation groups, date ranges, and various partner attributes. It includes placeholder sections for address filtering that can be dynamically replaced during query execution. Results are ordered by partner short name with optional address-based ordering.

 **Code Landmarks**
- `Line 5-6`: Uses placeholder syntax (##address_filter_fields## and ##address_filter_tables##) for dynamic SQL generation
- `Line 9-10`: Uses parameter binding with question marks for secure query execution
- `Line 11`: Uses dynamic IN clause (##motivation_group_detail_pairs##) for flexible motivation filtering
- `Line 24-25`: Contains placeholder sections for address filtering in WHERE and ORDER BY clauses
### Gift.Queries.ExtractRecipientByField.sql

This SQL query extracts distinct gift recipients from the OpenPetra database. It joins the gift, gift detail, and partner tables to retrieve recipient partner keys and short names. The query includes multiple parameterized filters for ledger numbers, date ranges, and other criteria. Results are ordered by partner short name. The query supports the gift management functionality in OpenPetra, allowing users to retrieve recipient information based on specified filtering conditions.

 **Code Landmarks**
- `Line 3`: Uses parameterized queries (?) for secure and flexible filtering conditions
- `Line 4`: Implements conditional filtering with boolean parameters to toggle filter application
- `Line 9-10`: Date range filtering with optional date boundaries controlled by boolean parameters
### Gift.ReceiptPrinting.GetDonationsOfDonor.sql

This SQL query retrieves donation details for a specific donor within a date range for receipt printing purposes. It joins multiple tables including gift batches, gifts, gift details, motivation details, accounts, cost centers, and partner information. The query returns comprehensive donation information including transaction amounts, tax-deductible portions, currencies, gift types, comments, account descriptions, recipient details, and motivation descriptions. It filters for posted batches, specific ledger numbers, date ranges, donor keys, printable receipts, and receipt-eligible motivation details, excluding modified details and ordering results chronologically by date entered.

 **Code Landmarks**
- `Line 1-26`: Comprehensive selection of financial fields including both base currency and transaction currency amounts
- `Line 27-37`: Complex join across 7 tables including self-joins on p_partner table for different roles (GiftDestination and Recipient)
- `Line 42`: Filters for donations marked for receipt printing with a_print_receipt_l = 1
- `Line 46`: Filters for motivation details that are receipt-eligible with a_receipt_l = 1
- `Line 55`: Excludes modified/adjusted gift details with a_modified_detail_l = 0 condition
### Gift.ReceiptPrinting.GetDonors.sql

This SQL file implements a query to retrieve distinct donor information for gift receipt printing. It selects donor keys, names, status codes, and partner types from gift records within a specified date range and ledger. The query filters for posted gift batches and can be configured with conditional clauses for different printing scenarios. Key conditional blocks include BYEXTRACT (filtering by extract lists), VIAEMAIL (selecting donors with email subscriptions), and VIAPRINT (selecting donors without email subscriptions). The query supports filtering by receipt letter frequency and orders results by partner name.

 **Code Landmarks**
- `Line 4`: Uses conditional SQL compilation with {#IFDEF} directives to modify query structure based on runtime parameters
- `Line 20`: Implements email-based filtering using EXISTS subquery to check subscription records
- `Line 29`: Uses NOT EXISTS pattern to identify donors without email subscriptions for print-only receipts
- `Line 38`: Conditional join to m_extract tables when filtering by extract lists
### ICH.HOSAExportGLTrans.sql

This SQL file contains a query for retrieving General Ledger transactions that need to be exported from the International Clearing House (ICH) system. The query joins the a_transaction and a_journal tables to filter transactions based on specific criteria including ledger number, account code, cost center, transaction status, and ICH number. It excludes system-generated transactions with specific narratives and filters by journal period. The query returns transaction details including amounts in various currencies, transaction dates, and narrative information.

 **Code Landmarks**
- `Line 15-16`: Uses complex filtering logic for ICH numbers with mathematical operations to determine eligible transactions
- `Line 12-13`: Implements exclusion logic for system-generated transactions based on narrative text pattern matching
### ICH.HOSAExportGifts.sql

This SQL file contains a query for selecting gift transactions to export from the general ledger system. It joins the a_general_ledger_master table with a_cost_centre and a_account tables to retrieve transaction details including sequence, ledger number, year, account code, cost center code, and account type. The query filters for active posting accounts and cost centers, and accepts parameters for ledger number, year, and cost center code. Results are ordered by account code in ascending order.

 **Code Landmarks**
- `Line 1`: References gi3200.p line 170, indicating this SQL is part of a larger process related to gift exports
- `Line 3`: Query joins three tables (a_general_ledger_master, a_cost_centre, and a_account) to gather complete transaction information
- `Line 19`: Uses parameterized queries (?) for ledger number, year, and cost center code to prevent SQL injection
### ICH.HOSAExportGiftsInner.sql

This SQL file contains a query for exporting gift transactions in OpenPetra's International Clearing House (ICH) system. The query joins multiple tables (a_gift_detail, a_gift_batch, a_motivation_detail, and a_gift) to retrieve comprehensive gift transaction information. It filters transactions based on ledger number, cost center code, ICH number, batch status, date range, and account code. The query returns details including gift amounts, motivation codes, recipient keys, and batch descriptions, ordered by recipient, motivation group, and detail codes.

 **Code Landmarks**
- `Line 1`: Part of the HOSA (Home Office System Administration) export functionality for ICH
- `Line 29`: Uses parameterized queries (?) for secure database access and filtering
- `Line 36`: Orders results by recipient key and motivation codes for organized reporting
### ICH.HOSAReportGiftSummary.sql

This SQL file defines a query used for HOSA (Home Office Supporter Account) report gift summaries in OpenPetra's finance module. It retrieves gift amounts, recipient keys, and recipient names by joining multiple tables including gift details, batches, motivation details, and partner information. The query supports conditional filtering through preprocessor directives that modify the SQL based on report requirements, including options for personal HOSA reports, ICH number filtering, and period-based vs date-based reporting. Parameters include ledger number, cost center code, ICH number, batch status, and date/period ranges.

 **Code Landmarks**
- `Line 7-10`: Uses preprocessor directives ({#IFDEF}, {#ENDIF}) to conditionally include SQL clauses for different report types
- `Line 31-33`: Implements conditional table joining for personal HOSA reports with partner key relationships
- `Line 42-49`: Supports two different date filtering methods: direct date comparison or period-based filtering
### Partner.CommonAddressTools.GetBestAddress.sql

This SQL file implements a query to find the best address for each partner in a given donor list. It uses Common Table Expressions (CTEs) to first categorize addresses as current, future, or expired, then selects the optimal address based on priority rules. The query prioritizes addresses marked for sending mail, then selects based on date state (current > future > expired). For current addresses, it selects the most recent; for future addresses, the soonest starting; and for expired addresses, the most recently active. The final result joins with the location table to return complete address information.

 **Code Landmarks**
- `Line 13`: Implements a sophisticated prioritization algorithm for addresses using a CASE statement to categorize by date state
- `Line 29`: Uses window functions with row_number() and complex ordering criteria to select the optimal address per partner
- `Line 1`: Uses Common Table Expressions (CTEs) for improved query readability and maintainability
### Partner.CommonAddressTools.GetBestAddressSanitized.sql

This SQL query retrieves the best address for each partner from a list of donor keys. It uses Common Table Expressions (CTEs) to first categorize addresses as current, future, or expired based on effective dates, then selects the optimal address using a prioritization algorithm. The query prioritizes addresses marked for mail sending, then by date state (current > future > expired), and finally by the most recent effective date for current addresses, earliest effective date for future addresses, or most recent expiration date for expired addresses. The final result joins with the location table to return complete address details including geographic coordinates.

 **Code Landmarks**
- `Line 13`: Implements a date-based state categorization system that prioritizes current, future, and expired addresses in that order
- `Line 32`: Uses window functions with complex ordering logic to determine the optimal address for each partner
- `Line 41`: Joins with location table to retrieve complete address details after determining the best address record
### Partner.Queries.ExtractByPartnerCity.sql

This SQL query extracts partner keys and short names from the OpenPetra database by filtering partners based on city name. It joins partner, partner_location, and location tables to find partners with valid mailing addresses in specified cities. The query includes parameters for city name pattern matching and date validation to ensure only current addresses are returned. It filters out deleted partners and only includes those with 'ACTIVE' status, ordering results by partner short name for easy readability.

 **Code Landmarks**
- `Line 4-6`: Three-table join structure connects partners to their locations through the partner_location junction table
- `Line 10-11`: Date range validation using parameters ensures only currently valid addresses are returned
- `Line 8`: LIKE operator with parameter allows flexible city name pattern matching
### Partner.Queries.ExtractByPartnerRelationship.sql

This SQL file implements a query to extract partner relationships from the OpenPetra database. It performs a UNION of two SELECT statements to retrieve both direct and reciprocal relationships. The query joins partner and partner relationship tables to extract partner keys and short names based on specified relationship criteria. It includes filters for active partners, solicitation preferences, and specific relationship types. Parameters are used for filtering active status, solicitation preferences, partner keys, and relationship names.

 **Code Landmarks**
- `Line 1-9`: Uses parameterized queries with ? placeholders for secure and flexible filtering
- `Line 11-21`: Implements a UNION operation to combine both direct and reciprocal relationships in a single result set
- `Line 5-6`: Conditional filtering using boolean parameters with NOT ? OR syntax for optional criteria application
### Partner.Queries.ExtractByPartnerSpecialType.sql

This SQL query extracts distinct partner keys and short names from the partner database filtered by partner type. It supports filtering by creation date, active status, family class, and no-solicitation flag. The query includes placeholder sections (##address_filter_fields##, ##address_filter_tables##, etc.) that allow for dynamic inclusion of address-related filtering criteria. Results are ordered by partner short name and key, with optional address-based ordering.

 **Code Landmarks**
- `Line 3`: Uses placeholder syntax (##address_filter_fields##) for dynamic SQL generation
- `Line 5`: Implements parameterized queries with (?) placeholders for security
- `Line 6`: Uses boolean parameter pattern (NOT ? OR condition) for optional filtering
### Partner.Queries.ExtractFamilyForPersons.sql

This SQL query retrieves distinct family partner records for persons included in an extract table. It joins multiple tables (m_extract, p_person, p_family, and p_partner) to find the family partners associated with persons in the extract, excluding merged partners. The query returns partner keys and short names, ordered alphabetically by partner short name. The parameterized query uses a placeholder for the extract ID.

 **Code Landmarks**
- `Line 1-11`: Uses a parameterized query with '?' placeholder for the extract ID to prevent SQL injection
### Partner.Queries.ExtractFamilyMembers.sql

This SQL query extracts family members from the OpenPetra database. It joins multiple tables to find individual person records that belong to families included in a specific extract. The query filters for partners with class 'FAMILY', excludes merged partners, and returns distinct partner keys and short names of family members, ordered alphabetically by name. It uses a parameterized query with a placeholder for the extract ID.

 **Code Landmarks**
- `Line 3-6`: Uses multiple table joins to connect extract data with family relationships and individual person records
- `Line 8`: Parameterized query using '?' placeholder for extract ID to prevent SQL injection
- `Line 10`: Filters specifically for partner class 'FAMILY' to ensure only family groups are processed
- `Line 12`: Excludes merged partners with status code check to prevent duplicate or obsolete records
### Partner.Queries.ExtractFromExtractByPartnerRelationship.sql

This SQL file implements a query that extracts partners related to partners in a specified extract. The query performs a UNION of two SELECT statements to find both direct and reciprocal relationships. It joins partner tables with extract tables to filter partners based on relationship types, active status, and solicitation preferences. The query includes parameters for filtering by active status, solicitation preferences, extract name, and relationship types. Results are ordered by partner short name and include partner keys and short names.

 **Code Landmarks**
- `Line 3`: Uses JOIN operations across multiple tables to establish relationship connections between partners
- `Line 11`: Implements a subquery to filter partners based on their presence in a named extract
- `Line 20`: UNION operation combines direct relationships with reciprocal relationships for comprehensive results
- `Line 1`: Parameterized query with multiple boolean and string parameters (indicated by ? placeholders)
### Partner.Queries.ExtractPartnerByCommitment.sql

This SQL query file extracts partner information based on commitment criteria from OpenPetra's database. It joins the partner and staff data tables to filter partners by commitment dates, home/receiving field offices, commitment status, partner status, and solicitation preferences. The query includes placeholder parameters (?) for dynamic filtering and special placeholders (##address_filter_fields##) that are likely replaced during query execution to include additional address filtering capabilities. The query returns partner keys and short names, ordered by partner short name.

 **Code Landmarks**
- `Line 4`: Uses special placeholders (##address_filter_fields##) that get replaced during runtime to dynamically extend the query with address filtering capabilities
- `Line 7`: Implements flexible date range filtering with nullable parameters using OR conditions with parameter pairs
- `Line 11`: Complex condition to find commitments valid on a specific date using both start and end dates
- `Line 16`: Handles NULL values in commitment status with special parameter to include/exclude partners with no commitment status
### Partner.Queries.ExtractPartnerByContactLog.sql

This SQL file defines a query for extracting partner information based on contact log records. It joins partner data with contact logs and contact attributes to filter partners by various criteria including contactor, contact code, mailing code, contact attributes, date ranges, partner status, partner class, and solicitation preferences. The query includes placeholder parameters (?) for dynamic filtering and special placeholders (##address_filter_fields##) for additional filtering options that can be injected at runtime.

 **Code Landmarks**
- `Line 3`: Uses placeholder syntax (##address_filter_fields##) for dynamic SQL generation that can inject additional fields at runtime
- `Line 9`: Implements flexible filtering with boolean parameters using (NOT ? OR condition) pattern to conditionally apply filters
- `Line 17`: Uses complex string concatenation and IN clause for attribute filtering
- `Line 22`: Contains business logic filters for active partners, family class partners, and solicitation preferences
### Partner.Queries.ExtractPartnerByEvent.sql

This SQL query extracts distinct partner keys and names from the OpenPetra database based on short-term application events. It joins partner, short-term application, and general application tables with configurable address filtering. The query filters by confirmation options, application statuses (Active, Hold, Enquiry, Completed, Rejected), and optional partner status conditions. It includes placeholder variables for dynamic filtering and ordering, allowing customization through ##address_filter_fields##, ##address_filter_tables##, ##address_filter_where_clause##, and ##address_filter_order_by_clause## tokens.

 **Code Landmarks**
- `Line 3`: Uses placeholder tokens (##address_filter_fields##) for dynamic SQL generation
- `Line 7`: Implements parameterized queries with question mark placeholders for security
- `Line 13-17`: Boolean parameter flags (?) control which application statuses to include in results
- `Line 18-19`: Optional filtering for active partners and those allowing solicitations
### Partner.Queries.ExtractPartnerByEventRole.sql

This SQL query extracts distinct partner keys and short names from the partner database based on their event roles. It joins the partner table with short-term and general application tables, filtering by confirmed options, congress codes, and application statuses. The query includes parameters for filtering specific confirmation options, congress codes, and application status types (Active, Hold, Enquiry, Cancelled, or Rejected), while excluding deleted records. Results are ordered by partner short name.

 **Code Landmarks**
- `Line 3-4`: Uses parameterized queries with multiple placeholders (?) for dynamic filtering of confirmed options and congress codes
- `Line 11-15`: Implements conditional filtering with boolean parameters to selectively include different application statuses using LIKE operators with wildcards
### Partner.Queries.ExtractPartnerByField.Commitment.sql

This SQL file contains a query for extracting partner information based on commitment criteria. It selects partner keys and names from pub_p_partner and pub_pm_staff_data tables, filtering by commitment dates and status. The query includes placeholders (##) for dynamic parts that get replaced during execution, such as address filtering fields, tables, and join conditions. It supports filtering by active status, no solicitations flag, and commitment date ranges, with results ordered by partner name.

 **Code Landmarks**
- `Line 3`: Uses placeholders (##address_filter_fields##) for dynamic SQL generation
- `Line 5`: Dynamic field selection with ##sending_or_receiving_field## placeholder
- `Line 9`: Conditional join with ##join_for_person_or_family## placeholder
- `Line 12`: Address filtering with placeholder clauses for flexible query construction
### Partner.Queries.ExtractPartnerByField.WorkerField.sql

This SQL file contains a query for extracting partner data based on worker field criteria. It selects partner keys and names from the partner database tables with various filtering options. The query includes placeholders for dynamic filtering by address, person/family type, and worker type. It supports filtering for active partners, excluding those with no solicitations, and can exclude family members already in an extract. Results are ordered by partner short name with optional address-based ordering.

 **Code Landmarks**
- `Line 1-3`: Uses dynamic SQL placeholders (##address_filter_fields##) for flexible column selection
- `Line 5-6`: Implements parameterized queries with (?) placeholders for security against SQL injection
- `Line 7-8`: Contains conditional filtering logic using boolean parameters (NOT ?)
- `Line 9`: Uses LIKE operator with placeholder (##worker_type##) for flexible worker type filtering
### Partner.Queries.ExtractPartnerByGeneralCriteria.sql

This SQL file defines a parameterized query for extracting partner information from the OpenPetra database. It selects partner keys and short names with optional address fields based on multiple filtering criteria including partner class, status code, language code, solicitation preferences, creation/modification dates and users. The query uses placeholders (##partner_specific_tables##, ##address_filter_tables##, etc.) that get replaced at runtime to customize the query based on specific filtering requirements. The structure allows for flexible partner data extraction with conditional filtering and customizable sorting.

 **Code Landmarks**
- `Line 3`: Uses ##address_filter_fields## placeholder for dynamic field selection based on address filtering requirements
- `Line 5`: Employs boolean parameter pairs (? OR field = ?) pattern for optional filtering throughout the query
- `Line 17`: Contains multiple placeholders (##partner_specific_where_clause##, ##address_filter_where_clause##) for runtime query customization
### Partner.Queries.ExtractPartnerBySubscription.sql

This SQL file contains a query to extract partners based on their publication subscriptions. It selects partner keys and names from partners who have subscriptions to specified publications. The query implements complex filtering logic including subscription status (active, expired, cancelled), gratis subscriptions, partner status, partner class, solicitation preferences, publication copies, and date ranges for subscription start and expiry. It includes placeholders for additional address filtering that can be injected at runtime, with customizable WHERE clauses and ORDER BY options.

 **Code Landmarks**
- `Line 3`: Uses ##address_filter_fields## placeholder for dynamic field selection in the query
- `Line 4`: Uses ##address_filter_tables## placeholder to dynamically join additional tables
- `Line 30`: Uses ##address_filter_where_clause## and ##address_filter_order_by_clause## for dynamic filtering and sorting
- `Line 6`: Implements complex conditional logic for subscription status filtering with nested AND/OR conditions
### Personnel.Reports.AllCommitments.sql

This SQL file defines a query to retrieve information about active staff commitments in the Personnel module. It joins multiple tables (pm_staff_data, p_person, p_partner, p_unit) to collect staff member details including personal information, commitment dates, and receiving field country. The query filters for active partners and uses a subquery to ensure only current commitments (based on date parameters) are returned. The query returns fields like partner key, gender, name, commitment dates, and receiving country code.

 **Code Landmarks**
- `Line 19`: Uses a correlated subquery with date parameters to filter for current commitments only
- `Line 1`: Query designed to return distinct records to avoid duplicates when multiple commitments exist
### Personnel.Reports.Birthday.sql

This SQL file defines a query for retrieving birthday information from the Personnel database in OpenPetra. It selects key person data including partner key, date of birth, gender, name fields, and calculates the day of year for birthdays. The query supports multiple filtering options through conditional SQL blocks including: filtering by a specific partner, by extract list, by staff status within date ranges, by family key, by partner types, and with or without date ranges. Results are ordered by the day of year of the birth date to facilitate chronological birthday reports.

 **Code Landmarks**
- `Line 9-11`: Conditional SQL block for including partner type codes when needed
- `Line 21-25`: Conditional filtering by extract lists, allowing report generation for specific groups
- `Line 27-36`: Staff-specific filtering with date range parameters for active staff members
- `Line 46-55`: Date range filtering using DAYOFYEAR function to compare birthdays regardless of year
### Personnel.Reports.GetFamilyKeyOfPerson.sql

This SQL query retrieves family keys from the PUB_p_person table using different filtering options controlled by preprocessor directives. It supports three query modes: ONEPARTNER (filters by specific partner key), BYEXTRACT (joins with extract tables to filter by extract name), and BYSTAFF (joins with staff data to filter by commitment dates). The query ensures only records with non-null family keys are returned. The conditional sections are controlled by preprocessor directives that determine which filtering logic is applied.

 **Code Landmarks**
- `Line 3-22`: Uses preprocessor directives ({#IFDEF}) to conditionally include different WHERE clauses based on the query context

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #