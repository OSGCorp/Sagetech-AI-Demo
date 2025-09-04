# XML Financial Development Section Of Petra

The Petra application is a comprehensive open-source system that helps non-profit organizations manage their administrative operations. The XML Financial Development Section implements report templates for financial development and donor management, providing critical insights into donation patterns and donor relationships. This sub-project delivers standardized reporting capabilities through XML-defined templates that query the database and format financial information in consistent, meaningful ways.

## Key Capabilities

- Donor Analysis Reports: Identifies patterns such as new donors, lapsed donors, and top contributors
- Gift Tracking: Monitors donation amounts, frequencies, and methods across different time periods
- Recipient-based Reporting: Analyzes donations by recipient, fund, or motivation code
- Hierarchical Data Presentation: Organizes financial information in structured, multi-level reports
- Parameterized Reporting: Supports filtering by date ranges, minimum amounts, currencies, and other criteria

## Identified Design Elements

1. **XML-based Report Templates**: Each report is defined in a structured XML file that specifies parameters, calculations, and output formatting
2. **SQL Query Integration**: Templates incorporate SQL queries to extract and aggregate financial data from the database
3. **Hierarchical Data Organization**: Reports implement multi-level structures (main, donor, details) for logical data presentation
4. **Custom Calculation Functions**: Specialized functions like MakeTopDonor and GetPartnerBestAddress enhance report functionality
5. **Parameterized Filtering**: All reports support extensive parameter options for customized analysis

## Architecture Overview

The XML Financial Development Section follows a template-based architecture where each report is self-contained within its XML definition. These templates serve as the presentation layer for financial data, connecting to the underlying database through embedded SQL queries. The architecture supports both detailed transaction-level reporting and summarized analytical views, with consistent formatting and calculation methods across all reports.

## Business Functions

### Financial Development Reports
- `giftsoveramount.xml` : XML report definition for generating a financial report of gifts over a specified minimum amount with donor details.
- `topdonorreport.xml` : XML report definition for a Top Donor Report that ranks donors by gift amount with percentage contributions.
- `donorreportdetail.xml` : XML report definition for a detailed donor report showing gifts made within a specified time period.
- `totalgiftsperdonor.xml` : XML report definition for generating a total gifts per donor report for Danish legal requirements.
- `sybunt.xml` : XML report definition for SYBUNT (Some Year But Unfortunately Not This year) donor tracking report.
- `lapseddonorreport.xml` : XML definition for a Lapsed Donor Report that identifies donors who have stopped giving according to specified parameters.
- `newdonorreport.xml` : XML report definition for the New Donor Report that lists first-time donors with gifts meeting minimum amount criteria.
- `methodofgiving.xml` : XML report definition for generating financial reports on gift totals by method of giving and currency.
- `donorreportshort.xml` : XML report definition for generating a Short Donor Report showing donor information and gift totals for a specified period.
- `incomelocalsplit.xml` : XML report definition for the 'Income Local Split' financial report that shows donation distributions between local and remote projects.
- `FDIncomeByFund.xml` : XML report definition for generating Income by Fund financial reports with various filtering options and hierarchical display.
- `fddonorsperrecipient.xml` : XML report definition for tracking donors who have given to specific recipients, helping organizations maintain donor relationships when workers leave.

## Files
### FDIncomeByFund.xml

This XML file defines the 'FDIncomeByFund' report structure for OpenPetra's financial development reporting. It generates income reports organized by fund, with options for different depth levels (standard or summary). The report accepts parameters for ledger number, period range, currency, and motivation filters. It implements hierarchical data presentation with multiple calculation queries that retrieve unit hierarchies and gift amounts by motivation. The report structure includes levels for main, areas, and funds, with appropriate headers and footers for each level. Key calculations include gift amounts, percentage of grand income, and hierarchical summaries.

 **Code Landmarks**
- `Line 67`: Hierarchical unit structure query that supports multiple levels of organizational depth
- `Line 170`: Complex gift amount calculation with flexible period handling and multiple filtering options
- `Line 226`: Conditional motivation filtering with support for both inclusion and exclusion patterns
- `Line 282`: Multi-level reporting structure with conditional rendering based on report depth parameter
### donorreportdetail.xml

This XML file defines a detailed donor report for OpenPetra's financial development module. It creates a structured report showing donors and their gifts within a specified date range. The report includes parameters for ledger number, partner source, extract name, currency, and date range. It implements calculations to select donors with their total gifts and retrieve gift details including recipient information, gift dates, and amounts. The report is organized in hierarchical levels showing donor information with address details followed by individual gift transactions. Key calculations include 'Select Donors and Totals', 'SelectGiftDetails', and address formatting functions.

 **Code Landmarks**
- `Line 58`: Supports multiple currency display options (base or international) through conditional query segments
- `Line 195`: Uses custom NO-SQL functions to format partner addresses by concatenating address components with commas
- `Line 204`: Implements dynamic gift detail formatting based on recipient and ledger relationships
### donorreportshort.xml

This XML file defines a report template for generating a Short Donor Report in OpenPetra's financial development module. It retrieves donor information and their total gifts within a specified date range and ledger. The report includes parameters for ledger number, partner source, extract name, currency, and date range. It implements SQL queries to fetch donor data, calculate gift totals in base or international currency, and retrieve partner address details. The report can be sorted by donor name, partner key, or gift amount and displays comprehensive donor information including contact details and total contributions.

 **Code Landmarks**
- `Line 58`: SQL query dynamically adjusts based on currency parameter (Base or International)
- `Line 72`: Conditional query construction based on partner source (Extract or AllPartner)
- `Line 96`: Dynamic ORDER BY clause implementation based on user-selected sorting preference
- `Line 112`: Uses function call to GetPartnerBestAddress to retrieve contact information for each donor
### fddonorsperrecipient.xml

This XML report definition file implements the 'FDDonorsPerRecipient' report, which helps organizations track donors who have previously supported specific recipients (workers) who have left. The report displays donor information including contact details, subscriptions, and gift history both to the specified recipient and to other recipients. Key functionality includes SQL queries to select donors based on recipient key, retrieve subscription information, find the last gift to the specified recipient, and find the last gift to other recipients. The report uses several calculations to format and display donor information, including address details, gift dates, amounts, and comments. It's designed to help organizations maintain relationships with donors even after the workers they supported have moved on.

 **Code Landmarks**
- `Line 59`: Uses a function call to retrieve ledger name dynamically in the page header
- `Line 92`: SQL query excludes reversed gifts by filtering out modified details and negative amounts
- `Line 173`: Uses 'GetPartnerBestAddress' function to determine optimal contact information for donors
- `Line 302`: Formats motivation codes with conditional slash separator only when detail code exists
### giftsoveramount.xml

This XML file defines a report template for the 'Gifts Over Amount' financial report in OpenPetra. It specifies parameters including ledger number, date range, minimum gift amount, and donor exclusion options. The report displays donors who have made gifts exceeding the specified minimum amount, showing donor details, gift dates, recipients, motivation details, and gift amounts. The report structure includes multiple calculation queries that retrieve donor information, gift details, and address information from the database. The report is organized in hierarchical levels: main, DonorLevel1, DonorLevel, and Details, with appropriate headers, footers, and formatting.

 **Code Landmarks**
- `Line 63`: Complex SQL query that filters gifts based on multiple parameters including date range, ledger number, and minimum amount
- `Line 104`: Conditional query construction that adapts based on currency parameter (Base or International)
- `Line 295`: Uses NO-SQL function calls to format partner address information from multiple fields
- `Line 307`: Conditional logic to handle confidential gifts by masking the amount
### incomelocalsplit.xml

This XML file defines the 'Income Local Split' report for OpenPetra's financial development department. It shows how donations are distributed between local fields and projects. The report includes parameters for ledger number, year, periods, and sorting options (by account, motivation, or plain list). It implements multiple calculations to retrieve accounts, motivation groups, and gift amounts, with both period-specific and year-to-date figures. The report structure uses nested levels to organize data hierarchically, allowing users to view donations grouped by either account or motivation group, with appropriate subtotals and a grand total.

 **Code Landmarks**
- `Line 55-69`: Dynamic report header generation with conditional formatting based on parameters
- `Line 123-147`: Complex SQL query that filters gifts by recipient ledger using multiplication function
- `Line 198`: Percentage calculation using custom functions to determine portion of grand income
- `Line 208-217`: Conditional lower level report selection based on orderby parameter
- `Line 220-224`: Dynamic grand total calculation using getSumLowerReport function
### lapseddonorreport.xml

This XML file defines the Lapsed Donor Report structure for OpenPetra's financial development reporting. It specifies report parameters including ledger number, date range, minimum donation amount, recipient key, frequency, and motivation codes. The report identifies donors who have stopped giving based on configured tolerance parameters. Key calculations include selecting donors within specified criteria and determining gift amounts across multiple years. The report organizes data in hierarchical levels (main, Report, DonorLevel) and includes functionality to count donors and sum gift amounts in the report footer.

 **Code Landmarks**
- `Line 81`: Uses a conditional switch statement to control display of 'Ignore if other gifts' parameter in the report header
- `Line 176`: Implements filtering for absolute gift amounts in either base or international currency
- `Line 262`: Uses IsLapsedDonor() function to dynamically filter donors based on complex lapsed donor criteria
### methodofgiving.xml

This XML report definition file creates a financial report that lists the number and sum of gifts for each currency and method of giving within a specified time period. The report accepts parameters for ledger number and date range, then queries gift transactions from posted batches. It organizes results hierarchically by currency and method of giving, displaying counts and amounts for each. The report includes calculations to retrieve distinct currencies, methods of giving, and corresponding gift totals. The structure includes multiple levels (main, currencies, methods) to properly format and organize the financial data with appropriate headers, details, and footers.

 **Code Landmarks**
- `Line 39`: Uses nested detailreport with parameterized query filtering by method of giving and currency
- `Line 61`: SQL query extracts distinct currencies from gift batches within date range
- `Line 75`: Complex SQL aggregation query with joins across multiple tables to calculate gift totals by method
- `Line 143`: Hierarchical level structure with main, currencies, and methods levels for organized reporting
### newdonorreport.xml

This XML file defines the New Donor Report for OpenPetra's financial development module. It creates a report that identifies donors who made their first gift within a specified date range and above a minimum amount threshold. The report includes parameters for ledger number, currency, date range, and minimum gift amount. It implements two main SQL queries: one to select donors with their total gift amounts and another to retrieve detailed gift information including recipients and motivation codes. The report is structured with multiple levels to display donor information hierarchically, showing donor details, addresses, and individual gift transactions with motivation codes.

 **Code Landmarks**
- `Line 11-16`: Report parameters define the core filtering criteria including ledger, currency, date range and minimum amount
- `Line 74-152`: Main SQL query uses complex joins across multiple tables with conditional elements based on user parameter selections
- `Line 154-232`: Secondary query retrieves gift details with motivation codes and descriptions for selected donors
- `Line 308-320`: Uses a special NO-SQL function to retrieve and format partner address information
### sybunt.xml

This XML file defines a financial development report called SYBUNT (Some Year But Unfortunately Not This year) that identifies donors who gave gifts above a minimum amount in a previous calendar year but gave nothing in the current year. The report includes parameters for ledger number, currency, date ranges, and minimum gift amount. It implements SQL queries to select donors with gifts in the previous year, check for absence of gifts in the current year, and display donor information including partner details, gift dates, amounts, and motivation data. The report structure includes page headers, calculations, and hierarchical levels for organizing the output.

 **Code Landmarks**
- `Line 8`: Report uses calendar years as comparison periods for donor giving patterns
- `Line 118`: Conditional query structure that adapts based on currency parameter (Base vs International)
- `Line 175`: Uses a non-SQL function 'ConditionRow' to conditionally show/hide rows based on gift count
- `Line 178`: Custom function 'SelectLastGift' retrieves additional gift details when conditions are met
### topdonorreport.xml

This XML file defines the Top Donor Report for OpenPetra's financial development reporting. It generates a ranked list of donors based on gift amounts within a specified date range, showing each donor's contribution percentage and cumulative percentages. The report supports filtering by recipient, motivation codes, and donor extracts. Key parameters include ledger number, currency, date range, and minimum amount. The report uses SQL queries to calculate total gift amounts and implements a custom MakeTopDonor function to process the data for display with donor details, gift amounts, percentages, and address information.

 **Code Landmarks**
- `Line 85`: Uses a custom function MakeTopDonor to process donor data outside of SQL
- `Line 40`: Implements conditional field display based on donor type (top, middle, bottom)
- `Line 164`: Uses NO-SQL directive with a custom function call to process data
### totalgiftsperdonor.xml

This XML report definition file implements a financial report that displays total gifts per donor for Danish legal requirements. It queries donor information and gift totals within a specified date range, filtering for posted gifts with receipt flags. The report includes donor details (name, address, postal code, city) and total gift amounts. Key calculations include 'Select Donors and Totals' which aggregates gift amounts by donor, and 'GetPartnerBestAddress' which retrieves donor contact information. The report supports filtering by country code and date/period ranges, and includes summary information showing donor count and total gifts.

 **Code Landmarks**
- `Line 18`: Uses 'DetermineBestAddress' from shared calculations module to determine the most appropriate address for each donor
- `Line 79`: SQL query joins multiple tables to calculate total gifts while filtering for posted gifts with receipt flags set
- `Line 96`: Custom function combines address lookup with conditional filtering based on country code and gift amount

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #