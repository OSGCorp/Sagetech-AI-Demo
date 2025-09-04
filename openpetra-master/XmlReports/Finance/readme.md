# XML Finance Section Of Petra

The XML Finance Section is a critical component of OpenPetra that implements the financial reporting infrastructure through XML-based templates and HTML renderers. This sub-project provides a comprehensive framework for generating financial reports essential to non-profit organizations' accounting and administrative needs. The architecture follows a template-driven approach where report definitions are stored as XML files that define both data retrieval logic and presentation structure.

This provides these capabilities to the Petra program:

- Standardized financial report generation (Trial Balance, Balance Sheet, Income/Expense Statements)
- Donor and recipient gift tracking and reporting
- Hierarchical data presentation with multiple detail levels
- Multi-currency support (Base, International, Transaction)
- Configurable filtering and parameter-based report customization

## Identified Design Elements

1. **Hierarchical Report Structure**: Reports are organized in nested levels (main, summary, detail) allowing for proper grouping and totaling of financial data
2. **Reusable Calculation Components**: Common SQL queries and calculations are defined in shared files (accountdetailcommon.xml, finance.xml) for reuse across multiple reports
3. **Parameterized Reporting**: All reports support extensive filtering options including date ranges, account codes, cost centers, and currencies
4. **Multiple Output Formats**: Support for both XML-based and HTML-based report rendering to accommodate different use cases
5. **Specialized Financial Reporting**: Purpose-built templates for specific financial needs like donor statements, gift summaries, and tax reporting

## Overview
The architecture emphasizes a clear separation between data retrieval (SQL queries) and presentation logic. Reports are highly configurable through parameters while maintaining consistent formatting and calculation methods. The template-based approach ensures consistency across the financial reporting system while allowing for specialized reports to address unique organizational needs.

## Sub-Projects

### XmlReports/Finance/AccountsPayable

The XML Accounts Payable Section implements standardized report templates for accounts payable functionality, providing structured financial reporting capabilities. This sub-project delivers templated XML definitions that power OpenPetra's AP reporting engine, enabling consistent financial data presentation across the application.

This provides these capabilities to the OpenPetra program:

- Structured financial data presentation with hierarchical organization
- Standardized calculation queries for retrieving AP transaction data
- Flexible parameter-based filtering for date ranges and payment criteria
- Multi-currency support with appropriate currency-based grouping
- Aging analysis for outstanding supplier invoices

#### Identified Design Elements

1. **Hierarchical Report Structure**: Reports implement nested levels (main, supplier, payment, document, detail) to organize financial data logically
2. **Parameterized Queries**: All reports include calculation sections that define database queries with configurable parameters
3. **Multi-Currency Support**: Reports handle both original and base currencies with appropriate conversion and display
4. **Aging Analysis**: Specialized templates for analyzing overdue payments with configurable aging periods
5. **Consistent Formatting**: Standardized headers, footers, and calculation sections across all report templates

#### Overview
The architecture emphasizes a template-based approach to financial reporting, with XML definitions that separate the presentation logic from the application code. Each report template defines both the data retrieval logic (through calculation sections) and the presentation structure (through nested levels). This separation enables consistent reporting while allowing for customization of individual reports. The templates focus on accounts payable workflows including payment tracking, supplier aging analysis, and detailed transaction reporting.

  *For additional detailed information, see the summary for XmlReports/Finance/AccountsPayable.*

### XmlReports/Finance/AccountsReceivable

The program handles contact management, accounting workflows, and financial reporting with internationalization support. This sub-project implements XML-based reporting templates for accounts receivable operations, specifically focusing on conference payment tracking and reconciliation. This provides these capabilities to the OpenPetra program:

- Participant payment status tracking for conference management
- Financial reconciliation between registrations and received payments
- Customizable reporting with multiple parameter inputs
- Data extraction for financial follow-up activities

#### Identified Design Elements

1. XML-Based Report Templates: The system uses structured XML definitions to create consistent, reusable report templates that separate presentation from data logic
2. Parameterized Queries: Reports accept multiple input parameters (ledger number, conference key, date ranges, motivation codes) to filter and customize output
3. Complex Data Relationships: The architecture handles relationship mapping between participant registrations and corresponding payment records
4. Formatted Output Generation: Templates define precise presentation formatting for tabular data with consistent column structures

#### Overview
The architecture follows a template-based approach for report generation, allowing for consistent styling and presentation while maintaining separation between data retrieval logic and presentation. The XML templates serve as declarative definitions that can be modified independently of application code. This design supports financial reconciliation workflows by identifying discrepancies between expected and actual payments, enabling conference organizers to follow up with participants who have outstanding balances.

  *For additional detailed information, see the summary for XmlReports/Finance/AccountsReceivable.*

## Business Functions

### Financial Reporting
- `hosa.xml` : XML definition for the HOSA (Home Office Statement of Accounts) financial report that shows account details with special handling for income accounts.
- `incomeexpensestatement.xml` : XML report definition for generating Income Expense Statements with various filtering and display options.
- `fieldgifttotalsreport.xml` : XML report definition for Field Gift Totals Report showing gifts from donors over a specified period.
- `accountdetail.xml` : XML configuration file defining Account Detail financial report with parameters, calculations, and layout for OpenPetra accounting system.
- `trialbalance.html` : HTML template for generating Trial Balance financial reports in OpenPetra.
- `accountdetail.html` : HTML template for Account Detail financial report in OpenPetra with SQL queries for transaction data.
- `trialbalance.xml` : XML report definition for Trial Balance and Surplus Deficit financial reports showing account/cost center credits, debits and balances.
- `balancesheet.xml` : XML definition for a Balance Sheet report showing year-to-date financial data with multiple detail levels and cost center breakdown options.

### Donor Management
- `donorgiftstatement.xml` : XML report definition for generating donor gift statements with various filtering options and output formats.
- `donorgiftstofieldsreport.xml` : XML report definition for displaying donor gifts to fields with filtering options for dates, amounts, and fields.
- `recipientgiftstatement.xml` : XML report definition for generating recipient gift statements showing donors and gift details for financial reporting.
- `totalgivingforrecipients.xml` : XML report definition for showing total giving for recipients over the last 4 years in OpenPetra's finance module.
- `oneyearmonthlygiving.xml` : XML report definition for generating monthly giving summaries for recipients, showing donor contributions throughout a year.

### Field Management
- `fieldleadergiftsummary.xml` : XML report definition for Field Leader Gift Summary that displays gift information for local cost centers sorted alphabetically.
- `fieldleadergiftsummary_2.xml` : XML report definition for Field Leader Gift Summary showing income posted to local cost centers in account order with recipient details.
- `totalgiftsthroughfieldTaxdeduct.xml` : XML report definition for the Total Gifts through Field report showing gift summaries with tax deduction information.
- `totalgiftsthroughfield.xml` : XML report definition for generating a summary of gifts processed through a field, showing worker and field gifts by month and year.

### Accounts Payable
- `currentaccountspayable.xml` : XML report definition for the Current Accounts Payable report showing unpaid invoices as of a specific date.

### Common Components
- `accountdetailcommon.xml` : XML file defining common calculations for account detail financial reports in OpenPetra.
- `finance.xml` : XML configuration file defining common finance report calculations for OpenPetra.

## Files
### accountdetail.html

This HTML template defines the Account Detail financial report for OpenPetra. It displays transaction details with optional analysis attributes for specified accounts and cost centers. The file contains two SQL queries: SelectTransactions retrieves transaction data including account codes, cost centers, dates, amounts, and narratives; SelectBalances fetches account balances with descriptive information. The template provides conditional visibility based on parameters and formats the report with transaction dates, references, narratives, debits, credits, and ending balances. It supports filtering by account codes, cost centers, date ranges, and reference numbers.

 **Code Landmarks**
- `Line 17`: Conditional visibility controls which title displays based on whether analysis attributes are included
- `Line 29`: Dynamic display of reference or analysis type information based on sort parameter and filter values
- `Line 46`: SQL query with multiple conditional filters using template parameters for flexible report generation
- `Line 113`: Template structure for displaying account/cost center combinations with transaction details and totals
### accountdetail.xml

This XML file defines the Account Detail report for OpenPetra's financial reporting system. It specifies report parameters including ledger number, date ranges, account hierarchies, cost centers, and sorting options. The report structure includes multiple calculation definitions for transaction amounts, balances, and analysis attributes. The file organizes data through a hierarchical level structure that handles different sorting methods (by Account, Cost Centre, Reference, or Analysis Type) and provides appropriate headers, details, and footers for each level. Key functionality includes displaying transaction details with debits/credits, calculating net balances, supporting different currency options (Base, International, Transaction), and optional display of analysis attributes. The report can filter by account codes, cost centers, references, and date ranges, making it a comprehensive financial reporting tool.

 **Code Landmarks**
- `Line 3`: Report structure follows a hierarchical XML schema defined in reports.dtd
- `Line 7`: Extensive parameterization allows for flexible report configuration with multiple filtering options
- `Line 118`: Calculations section defines formulas and formatting for financial data display
- `Line 207`: Multi-level report structure supports different sorting methods with appropriate headers and footers
- `Line 387`: Conditional rendering based on report parameters allows customized output formats
### accountdetailcommon.xml

This XML file defines reusable calculations for various account detail financial reports in OpenPetra. It contains SQL queries that retrieve financial data from the database, including account details, cost centers, transactions, references, and analysis attributes. The calculations support filtering by account ranges, account lists, cost center ranges, and cost center lists. Key functionality includes selecting accounts sorted by account code or cost center, retrieving transactions for specific account-cost center combinations, and getting transaction details by reference or analysis attribute. These calculations serve as building blocks that can be incorporated into different financial reports requiring account detail information.

 **Code Landmarks**
- `Line 16`: Provides modular SQL query components that can be reused across multiple financial reports
- `Line 30`: Implements conditional query segments based on parameter values for flexible reporting
- `Line 132`: Uses UNION queries to handle both direct account codes and property-based account hierarchies
- `Line 344`: Filters out system-generated year-end transactions with specific narrative patterns
### balancesheet.xml

This XML file defines a Balance Sheet report for OpenPetra's financial reporting system. It displays year-to-date financial data starting with the "BAL SHT" account using a selected accounting hierarchy. The report offers three columns (Actual End of Previous Year, Actual Previous Year, Actual Selected Year) and three detail levels (detail, standard, summary). It includes parameters for ledger selection, year, accounting hierarchy, periods, cost center options, and currency selection. The report structure is organized in hierarchical levels (main, summaryBreakDown, summaryCostCentre, summaryAccount, subSummaryAccount, detailAccount, detailCostCentre) to display financial data with appropriate grouping, indentation, and calculations.

 **Code Landmarks**
- `Line 77`: Implements a flexible calculation system that dynamically generates report columns based on selected parameters
- `Line 112`: Uses a switch-case structure to handle different cost center reporting options with conditional logic
- `Line 151`: Hierarchical level structure allows for nested reporting with different detail levels
- `Line 217`: Conditional display logic based on report parameters controls what information is shown
- `Line 254`: Uses function calls to calculate totals and retrieve data from the financial system
### currentaccountspayable.xml

This XML file defines the Current Accounts Payable report structure for OpenPetra's finance module. It displays invoices that have been posted but not yet paid as of a specified date. The report includes SQL queries to retrieve AP document data, calculate payment amounts, and determine outstanding invoices. Key calculations include selecting posted AP documents, determining amounts paid, and identifying outstanding invoices. The report presents data with fields for AP reference numbers, supplier information, due dates, posting dates, batch numbers, narratives, and financial amounts. The report structure includes multiple levels for organizing data presentation and calculating totals.

 **Code Landmarks**
- `Line 39`: SQL query joins multiple transaction tables to handle data across different fiscal years
- `Line 94`: Complex calculation logic to determine outstanding invoices with special handling for credit notes and reversals
- `Line 106`: Financial calculations adjust amounts based on debit/credit indicators to ensure proper reporting
### donorgiftstatement.xml

This XML file defines a report template for generating donor gift statements in OpenPetra. It allows users to view gifts from donors within specified date ranges, amount thresholds, and motivation groups. The report supports two main formats: 'Complete' showing detailed gift information and 'Totals' displaying monthly summaries across multiple years. Key calculations include SelectDonors, SelectSingleGift, SelectTotalPreviousYear, SelectTotalThisYear, and various monthly sum calculations. The report structure includes multiple nested levels for organizing data hierarchically from donors down to individual gifts. The template includes comprehensive SQL queries that join gift, gift detail, batch, and partner tables to retrieve the necessary information with various filtering options based on user parameters.

 **Code Landmarks**
- `Line 59`: Conditional field display based on donor selection type (All Donors, One Donor, or Extract)
- `Line 102`: SQL query uses complex joins across multiple tables with conditional filtering based on report parameters
- `Line 397`: Dynamic date range formatting in SQL queries based on month number to handle single/double digit months
- `Line 624`: Report implements a nested level structure that organizes data hierarchically from donors to individual gifts
- `Line 654`: Uses calculation functions to dynamically generate monthly summaries across multiple years
### donorgiftstofieldsreport.xml

This XML file defines a report that displays gifts from donors to specific fields within a specified period. It supports three report types: Complete, Summary, and Tax Claim. The file contains SQL queries to retrieve donor information, gift details, and recipient data from the OpenPetra database. Key functionality includes filtering by ledger number, date range, gift amount range, specific donors or donor extracts, and selected fields. The report can display data in either base or international currency. The report structure is organized in hierarchical levels, starting with donors and then showing their gifts to recipients with details like gift date, amount, motivation, and receipt number. It also includes calculations for totals and formatting of addresses.

 **Code Landmarks**
- `Line 11`: Report parameters define filtering options including ledger number, date ranges, and amount thresholds
- `Line 99`: SelectDonors calculation retrieves donor information with conditional query segments based on parameter selections
- `Line 233`: Conditional SQL queries handle different currency display options (Base vs International)
- `Line 365`: MakeDonorAddress function builds formatted address strings from partner data
- `Line 475`: Switch statement controls which report type (Complete, Summary, Tax Claim) is generated
### fieldgifttotalsreport.xml

This XML file defines the Field Gift Totals Report for OpenPetra's finance module. It generates a report showing gifts from donors for specified periods, categorized by fields. The report includes parameters for ledger number, currency, date range, and amount limits. It contains calculations to retrieve field information and gift data, with SQL queries that extract gift amounts and counts for both worker and field categories. The report is organized in hierarchical levels, displaying monthly and yearly totals with averages. Key calculations include SelectFields, SelectMonthWorker, SelectMonthField, and various formatting functions to present monetary values and counts properly.

 **Code Landmarks**
- `Line 60`: Uses CSV function to dynamically build SQL WHERE clause from selected fields
- `Line 74`: Complex date handling in SQL queries with conditional formatting based on month number
- `Line 245`: Hierarchical report structure with multiple nested levels for years and months
- `Line 307`: Dynamic calculation of averages using getSumLowerReport function
### fieldleadergiftsummary.xml

This XML file defines the Field Leader Gift Summary report for OpenPetra's financial reporting system. It displays gift information for local cost centers, showing total gifts for current and previous periods sorted alphabetically by recipient. The report includes multiple SQL queries to retrieve gift data from different time periods, with parameters for ledger number, date range, currency, and field selection. The report structure uses nested levels (main, FieldLevel, YearLevel, PartnerLevel0-3, PartnerLevel, GiftLevel) to organize data hierarchically, with calculations for gift amounts across different time periods and appropriate headers and footers for each section.

 **Code Landmarks**
- `Line 11-21`: Configurable report parameters allow filtering by ledger, date range, currency, and fields, with support for multiple year comparisons
- `Line 75-110`: Dynamic SQL query construction with conditional logic based on currency parameter (Base vs International)
- `Line 357-405`: Hierarchical level structure with conditional rendering based on the number of years selected for comparison
- `Line 111-147`: Detailed filtering in SQL queries ensures only posted gift batches are included in calculations
### fieldleadergiftsummary_2.xml

This XML file defines the Field Leader Gift Summary (2) report for OpenPetra's financial reporting system. It displays income posted to local cost centers in account order, with each recipient or motivation detail listed separately. The report includes multiple SQL queries to retrieve account codes, cost centers, gift transactions, and related financial data. It implements a hierarchical structure with nested levels for accounts, cost centers, and gift details. The report calculates debits, credits, and totals for each level, with formatting for currency values and date ranges. Key calculations include SelectAccounts, SelectCostCentres, SelectGifts, and various totaling functions that process financial data for presentation.

 **Code Landmarks**
- `Line 50`: Uses nested SQL queries with conditional formatting based on currency parameter (Base vs International)
- `Line 110`: Implements hierarchical data filtering with multiple calculation steps to process financial transactions
- `Line 321`: Complex multi-level report structure with conditional display logic based on transaction counts
- `Line 367`: Uses switch/case logic to conditionally show totals only when transactions exist
- `Line 416`: Implements CleanUp calculations to process and format financial data for presentation
### finance.xml

This XML file defines common finance report calculations that can be reused across multiple finance reports in OpenPetra. It provides three SQL query calculations for retrieving cost center data: 'Select All Costcentres', 'Select All Active Costcentres', and 'Select Costcentres' with filtering capability. Each calculation returns cost center codes and names from the database based on ledger number parameters. The file follows the reports DTD structure with report parameters, headers, calculations, and level definitions, establishing a foundation for finance reporting functionality.

 **Code Landmarks**
- `Line 17`: Defines reusable calculations that can be referenced by multiple finance reports
- `Line 45`: Uses a function 'csv' to dynamically build SQL queries with variable parameters
- `Line 60`: Contains placeholder structure for future report level implementation
### hosa.xml

This XML file defines the HOSA (Home Office Statement of Accounts) report structure for OpenPetra's financial reporting system. It creates a detailed account report with special handling for income accounts, listing donations from gift details grouped by recipient partner key. The file includes report parameters like ledger number, date ranges, and currency options. It implements multiple calculation queries to retrieve transaction data, account information, and gift details. The report is structured in hierarchical levels from main down to transaction detail, with specialized formatting for debits, credits, balances, and totals. Key calculations include "Select Summary of incoming gifts", "Select Transactions", and "Select All Accounts" to ensure comprehensive financial reporting.

 **Code Landmarks**
- `Line 13`: Special handling for INCOME account types with donation listing grouped by recipient partner key
- `Line 82`: Uses SQL queries that exclude system-generated transactions like year-end reallocations and gift batches
- `Line 97`: Ensures all Income accounts are included in report even when they have no posted transactions
- `Line 254`: Implements conditional logic to handle Income accounts differently than other account types
### incomeexpensestatement.xml

This XML file defines the Income Expense Statement report for OpenPetra's financial reporting system. It specifies report parameters including ledger number, year, period range, cost center options, and display depth. The report calculates and displays financial data across multiple time periods (current year, previous year, budget) with customizable columns. The file implements a hierarchical structure with multiple report levels (main, summaryCostCentre, summaryAccount, subSummaryAccount, detailAccount) to display financial information with varying levels of detail based on user preferences. Key functionality includes cost center breakdowns, account hierarchies, and calculation of totals with proper formatting.

 **Code Landmarks**
- `Line 11-38`: Comprehensive parameter system allows for highly customizable financial reports with options for cost centers, account hierarchies, and display depth
- `Line 72-168`: Calculation section defines multiple financial views (actual/budget across different years) with dynamic captions and query functions
- `Line 169-425`: Multi-level report structure implements hierarchical display of financial data with conditional formatting and summaries
- `Line 183-198`: Switch-case logic controls report behavior based on cost center breakdown options
- `Line 254-267`: Dynamic footer implementation calculates and displays surplus/deficit totals across report sections
### oneyearmonthlygiving.xml

This XML file defines a financial report called 'OneYearMonthlyGiving' that provides a detailed summary of gifts received by recipients over a year, broken down by month and donor. The report includes multiple calculation queries that retrieve gift data from the database, filtering by ledger number, date ranges, and recipient/donor keys. It organizes results in hierarchical levels: main level, recipient levels, and donor level. For each recipient, it displays monthly gift amounts from each donor, calculates totals, and provides statistics like average monthly gifts from churches versus individuals. The report handles different currencies (base or international) and includes conditional formatting to hide confidential donor information. Key calculations include selecting recipients, retrieving monthly gift amounts, and calculating various totals and averages.

 **Code Landmarks**
- `Line 5`: Report handles confidential gifts by hiding donor information, with a TODO to show them based on user access level
- `Line 125`: Hierarchical report structure with multiple nested levels for organizing recipient and donor information
- `Line 367`: Monthly gift calculations are separated into individual queries for each month of the year
- `Line 702`: Footer calculations provide statistical analysis including averages per month from different donor types
### recipientgiftstatement.xml

This XML file defines the 'RecipientGiftStatement' report for OpenPetra's finance module. It generates statements showing donors per recipient with various filtering options. The report supports multiple view types (Complete, Gifts Only, Donors Only, List) and can filter by ledger, date range, currency, and recipient selection (All, One, Extract). Key calculations include selecting recipients, retrieving gift details, calculating totals for current and previous years, and formatting donor information. The report structure includes multiple nested levels for organizing the data hierarchically from main level down to individual donors.

 **Code Landmarks**
- `Line 48-61`: Dynamic SQL query construction with conditional clauses based on user parameters for flexible recipient selection
- `Line 75-85`: Left join pattern with date-based filtering to handle gift destination relationships for families
- `Line 364-373`: Report type switching mechanism that determines which report level to use based on user selection
- `Line 374-458`: Hierarchical level structure with parent-child relationships for organizing financial data presentation
### totalgiftsthroughfield.xml

This XML file defines the 'TotalGiftsThroughField' report structure for OpenPetra's financial reporting system. It creates a detailed summary of gifts processed through a field's gift processing system, showing both gifts for workers and for fields. The report analyzes gifts by month and year, displaying amounts and quantities with totals and monthly averages. It supports reporting for up to 12 years of data. The file defines report parameters, headers, calculations, and a multi-level structure that organizes data into year blocks with monthly breakdowns and summary sections. Key calculations include SelectMonthGifts, SelectYearGifts, and various formatting functions for displaying monetary values and counts.

 **Code Landmarks**
- `Line 43`: Uses server-side query functions from Ict.Petra.Server.MFinance.queries.QueryFinanceReport to retrieve financial data
- `Line 52`: Implements a calculation that modifies parameters using the assign() function rather than retrieving data
- `Line 167`: Uses nested level structure with conditional rendering based on parameter values
- `Line 196`: Implements dynamic summary calculations using getSumLowerReport() function to aggregate data from child reports
- `Line 283`: Creates a year summary section with monthly averages calculated from the detailed data
### totalgiftsthroughfieldTaxdeduct.xml

This XML file defines the 'Total Gifts through Field' report structure that summarizes gifts processed through the organization's gift processing system. It displays gifts for workers and fields, analyzed by month and year, showing amounts, tax deductible portions, and quantities. The report includes parameters for ledger number, currency, and year selection. The file implements multiple calculation levels to generate monthly and yearly summaries, with tax deduction tracking throughout. Key calculations include SelectMonthGifts, SelectYearGifts, and various formatting calculations for displaying monetary values, counts, and averages across different time periods.

 **Code Landmarks**
- `Line 42`: Uses server-side queries to retrieve financial data through QueryFinanceReport methods
- `Line 55`: Implements a year calculation mechanism that dynamically adjusts reporting periods
- `Line 217`: Uses nested report levels to build complex hierarchical financial summaries
- `Line 230`: Implements average calculations using division functions with rounding for financial accuracy
- `Line 333`: Creates a year summary that includes tax deductible amounts alongside regular gift totals
### totalgivingforrecipients.xml

This XML report definition creates a 'Historic Donors Per Recipient' report that displays total giving amounts for recipients over the last four years. It accepts parameters for ledger number and currency, and allows filtering by fields, recipient types, and specific recipients or extracts. The report structure includes multiple calculation elements that query donor and recipient data, and organizes output in a hierarchical layout with recipient-level headers and year-level details. The report displays donor information (key, name, class) alongside yearly giving totals, with automatic summation at the recipient level.

 **Code Landmarks**
- `Line 73`: Uses server-side query functions from Ict.Petra.Server.MFinance.queries.QueryFinanceReport for data retrieval
- `Line 17`: Implements conditional field display based on parameter values using fielddetail elements
- `Line 166`: Uses hierarchical level structure with main, RecipientLevel, and YearLevel to organize report data
### trialbalance.html

This HTML template file defines the structure for Trial Balance financial reports in OpenPetra. It includes two SQL queries: one to retrieve transaction details and another to fetch account balances from the database. The template displays financial data with columns for account codes, names, debit/credit amounts, and ending balances. It supports filtering by ledger number, account codes, cost centers, and reporting periods. The file uses conditional rendering to display different cost center information based on user parameters.

 **Code Landmarks**
- `Line 33`: SQL query with conditional filters for retrieving financial transactions based on various parameters
- `Line 61`: Second SQL query that fetches account balances with support for different filtering options
- `Line 19`: Conditional visibility tags to display different cost centre information based on user selection
### trialbalance.xml

This XML file defines a Trial Balance report for OpenPetra's financial reporting system. It displays credits, debits, and balances of account/cost center combinations at the end of a period, showing year-to-date data. The report can be sorted by account, cost center, or department, with different display options (detail, standard, summary). The same template is used for the Surplus Deficit report, which shows only net balances by cost center. The file includes parameter definitions for filtering by ledger, period, year, currency, cost centers and accounts. The report structure consists of multiple nested levels for organizing data hierarchically, with calculations for retrieving trial balance data, debits, credits, and net balances. The XML defines both the data queries and the visual presentation of the report.

 **Code Landmarks**
- `Line 37`: Parameter options allow sorting by Account, Cost Centre, or Department with different depth levels (detail, standard, summary)
- `Line 174`: Uses SQL UNION to combine accounts with and without hierarchy properties
- `Line 242`: Custom functions like getGlmSequences and getActual retrieve financial data
- `Line 299`: Complex switch/case structure determines report organization based on sort parameter
- `Line 412`: Uses string manipulation to handle department code formatting

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #