# XML Partner Section Of Petra

The Petra application is a comprehensive open-source system that helps non-profit organizations manage administrative tasks. The XML Partner Section subproject implements reporting and template functionality for partner-related data, providing a critical interface between the database layer and the presentation layer. This subproject primarily focuses on generating structured reports for various partner-related information in both HTML and XML formats.

Key capabilities provided to the Petra program:

- Partner data reporting with multiple output formats (HTML, XML)
- Configurable report templates with embedded SQL queries
- Hierarchical data presentation for complex partner relationships
- Conditional display logic based on partner types and attributes
- Comprehensive filtering options for targeted data retrieval

## Identified Design Elements

1. **Dual Template System**: Supports both XML-based report definitions and HTML templates with embedded SQL for different reporting needs
2. **Hierarchical Data Organization**: Uses nested levels in XML reports to structure complex relationships between partners and their attributes
3. **Parameterized Queries**: Implements flexible filtering through configurable parameters for partner selection, date ranges, and display options
4. **Cross-Entity Data Integration**: Joins multiple database tables to provide comprehensive views of partner information, addresses, relationships, and financial data
5. **Conditional Formatting Logic**: Applies different display rules based on partner types, data availability, and user preferences

## Overview
The architecture emphasizes flexibility through parameterized reports, maintains consistent data presentation across different output formats, and provides comprehensive partner information retrieval capabilities. The templates are designed for extensibility with clear separation between data retrieval (SQL queries) and presentation logic. The subproject handles complex relationships between different partner types (individuals, organizations, churches) while supporting various filtering options to generate targeted reports for specific administrative needs.

## Business Functions

### Partner Reports
- `AnnualReportWithoutAnnualReceiptRecipients.html` : HTML template for generating reports of partners who haven't received annual receipts
- `partnercontactreport.xml` : XML definition for a Partner Contact Report that displays contact information for partners in OpenPetra.
- `brieffoundationreport.xml` : XML report definition for generating a brief foundation report with partner information, contact details, and submission data.
- `validbankaccountreport.xml` : XML report template for validating bank account numbers for partners in OpenPetra.
- `bulkaddressreport.xml` : XML report template for generating bulk address reports with various partner selection options and address details.
- `PartnerBySubscription.html` : HTML report template for displaying partners subscribed to a publication with their contact details.
- `subscriptionreport.xml` : XML report template for generating partner subscription information with addresses and gift details.
- `localpartnerdata.xml` : XML report definition for displaying partner-specific data labels in OpenPetra, supporting various selection criteria and custom data fields.
- `publicationstatisticalreport.xml` : XML report definition for generating publication statistics by county within a country.
- `supportingchurchesreport.xml` : XML definition for a Supporting Churches Report that displays church information and their supported partners or officers.
- `relationshipreport.xml` : XML report template for generating relationship reports between partners with various filtering options.
- `partnerbycity.html` : HTML template for generating a Partner By City report in OpenPetra with SQL query and formatting.
- `printpartner.xml` : XML report template for printing detailed partner information with configurable sections and formatting.
- `partneraddressreport.xml` : XML report definition for generating partner address reports with various selection criteria and formatting options.
- `partnerbyspecialtype.html` : HTML template for generating reports of partners filtered by special types in OpenPetra.

## Files
### AnnualReportWithoutAnnualReceiptRecipients.html

This HTML template file defines the structure for an Annual Report that lists partners who have not received annual receipts. It contains an embedded SQL query that selects partners with permanent subscriptions to a specific publication, excluding those who have received annual receipts within a specified date range. The query joins partner data with location information and email addresses. The report displays partner details in a tabular format including partner key, name, address information, country code, and email address. The template uses CSS classes for layout and formatting.

 **Code Landmarks**
- `Line 13`: Embedded SQL query between BeginSQL and EndSQL tags for server-side processing
- `Line 29`: SQL excludes donors with annual receipts using a NOT EXISTS subquery
- `Line 43`: Template uses parameterized values like {PublicationCode} and {DonationStartDate} for dynamic report generation
- `Line 59`: HTML structure uses a template pattern with id='partner_template' for iterative content generation
### PartnerBySubscription.html

PartnerBySubscription.html is a report template for OpenPetra that displays recipients of a specific publication subscription. It contains an embedded SQL query that selects partners with active, permanent subscriptions to a specified publication, retrieving their contact information including postal addresses and email. The template formats this data in a tabular layout with columns for partner key, address validity, partner name, street, postal code, city, country, and email address. The report filters for active partners without solicitation restrictions and whose subscriptions haven't been canceled or expired.

 **Code Landmarks**
- `Line 13`: Uses SQL comment delimiters (BeginSQL/EndSQL) to embed database queries directly in the HTML template
- `Line 17`: Query joins multiple tables (p_partner, p_subscription, p_partner_location, p_location) to gather comprehensive contact information
- `Line 26`: Implements filtering logic for active subscriptions by checking expiry dates against current date
### brieffoundationreport.xml

This XML file defines a report template for generating a Brief Foundation Report in OpenPetra. It allows users to select foundations based on three criteria: a single partner, an extract, or all current staff. The report retrieves foundation data including partner keys, names, owner information, contact details, addresses, and submission information. Key calculations include selecting foundation data from database tables, retrieving best address details, and formatting display fields. The report structure consists of multiple levels that organize how foundation information is displayed, with the main level triggering data retrieval and subsequent levels handling the presentation of foundation details.

 **Code Landmarks**
- `Line 40`: Uses conditional query details to modify SQL based on user parameter selection
- `Line 87`: Implements a NO-SQL calculation that uses functions to format address data for display
- `Line 181`: Conditional lower level report rendering based on foundation owner data presence
### bulkaddressreport.xml

This XML file defines a report template for generating bulk address reports in OpenPetra. It allows users to select partners by individual partner key, extract, or current staff, and displays detailed address information. The report includes calculations to retrieve partner data from the database and format address details. Key functionality includes partner selection queries, address formatting, and display of contact information such as email, phone numbers, and postal addresses. The report organizes data in hierarchical levels and provides options for sorting by partner name, partner key, number of copies, or subscription type.

 **Code Landmarks**
- `Line 51`: Dynamic SQL query construction based on user parameter selection (one partner, extract, or current staff)
- `Line 95`: MakeAddress calculation uses GetPartnerBestAddress function to determine optimal address for each partner
- `Line 103`: Address validation logic with multiple status flags (valid, no post, not yet current, expired)
- `Line 329`: Multi-level report structure with main, intermediate, and partner levels for hierarchical data organization
### localpartnerdata.xml

This XML report file defines a report structure for displaying partner-specific data labels in OpenPetra. It provides multiple selection options including individual partners, extracts, or all current staff. The report queries partner data and associated data labels from the database, supporting both partner-specific and application-specific labels. Key calculations include 'Select Partners' to identify partners based on selection criteria, 'PartnerLabelValue' to retrieve label data, and 'RetrieveDataLabelValues' to format the data for display. The report structure includes parameters for date ranges, label selection, and display formatting.

 **Code Landmarks**
- `Line 31-46`: Dynamic query construction based on user selection parameters (one partner, extract, or all staff)
- `Line 49-82`: Conditional SQL filtering for partner and application data labels with dynamic CSV parameter injection
- `Line 84-123`: Flexible data type handling for various label values (text, numeric, date, boolean, etc.)
- `Line 126-132`: Uses GetPartnerLabelValues() function to retrieve formatted label values for display
### partneraddressreport.xml

This XML file defines a report for displaying partner addresses in OpenPetra. It provides multiple selection options including single partner, extract, or all current staff. The report includes SQL queries to retrieve partner data, addresses, contact details, and location information. It implements calculations for formatting and displaying partner information such as names, addresses, phone numbers, email addresses, and other contact details. The file structures the report with page headers, calculations, and hierarchical levels for organizing the data presentation.

 **Code Landmarks**
- `Line 30-41`: Dynamic page header that changes based on selection parameters
- `Line 43-124`: SQL queries with conditional segments that adapt based on user selection parameters
- `Line 126-137`: Uses custom functions like GetPartnerBestAddress and GetPartnerContactDetails to retrieve formatted data
- `Line 139-307`: Extensive set of calculation definitions that transform raw data into formatted display fields
- `Line 309-336`: Hierarchical report structure with conditional rendering based on address detail parameter
### partnerbycity.html

This HTML template defines the Partner By City report in OpenPetra. It contains an embedded SQL query that retrieves partner information along with their location details from multiple database tables. The query supports various filtering parameters including active status, family-only partners, solicitation preferences, address validity dates, city, and country. The template formats the data in a responsive table layout with columns for partner key, name, street, postal code, city, country, and phone number. The report uses CSS styling from an external report.css file for consistent presentation.

 **Code Landmarks**
- `Line 14`: Uses embedded SQL within HTML comments with BeginSQL/EndSQL tags for database queries
- `Line 29`: Implements conditional SQL filtering with #if directives for dynamic query generation based on parameters
- `Line 41`: Uses special {LISTCMP} function for comparing country code parameters against database values
- `Line 59`: Employs a template-based approach with {placeholder} syntax for dynamic data insertion
### partnerbyspecialtype.html

This HTML template file defines the structure for the 'Partner By Special Types' report in OpenPetra. It contains an embedded SQL query that retrieves partner data filtered by various special type criteria. The query joins partner tables with location and attribute information to display contact details including addresses, phone numbers, and email. The template supports multiple filtering parameters such as excluding specific special types, filtering by active status, families only, valid addresses, city, and country. The report displays results in a tabular format with columns for partner key, name, address details, and contact information.

 **Code Landmarks**
- `Line 16`: Embedded SQL query with multiple conditional filters controlled by template parameters
- `Line 30`: Complex JOIN structure connecting partner data with location and multiple attribute types
- `Line 41`: Custom LISTCMP template function used for list comparison operations in SQL filtering
### partnercontactreport.xml

This XML file defines the Partner Contact Report for OpenPetra, which displays contact information for partners based on various selection criteria. The report allows filtering by a single partner, an extract, or all current staff, with additional date range parameters. It contains SQL queries to retrieve partner data, contact details, and contact attributes from the database. The report structure includes multiple calculation sections that handle data retrieval and formatting, and several levels for organizing the display hierarchy. Key functionality includes partner selection, contact information retrieval, address formatting, and conditional display of contact attributes. The report presents partner names, addresses, contact methods, dates, and related details in a structured format.

 **Code Landmarks**
- `Line 3`: Report structure follows a hierarchical XML format with nested levels for organizing partner contact data
- `Line 76`: SQL queries use dynamic conditions based on user parameters for flexible data filtering
- `Line 232`: Left join used to handle cases where contact attributes may not exist
- `Line 298`: Custom function 'GetPartnerBestAddress' retrieves optimal address for each partner
- `Line 404`: Report uses conditional switching between different data retrieval methods based on attribute parameters
### printpartner.xml

This XML file defines a report template for printing comprehensive partner information in OpenPetra. It retrieves and displays data about partners (individuals, organizations, churches, etc.) from the database, including basic details, class-specific information, subscriptions, relationships, locations, contact details, financial information, interests, contacts, and reminders. The report is highly configurable through parameters that allow hiding empty sections and controlling display options. The file contains multiple SQL queries to fetch different aspects of partner data and organizes the output into hierarchical levels with formatted fields. It supports different partner classes (CHURCH, FAMILY, ORGANISATION, PERSON, UNIT) and handles conditional display of information based on data availability.

 **Code Landmarks**
- `Line 1`: Uses XML DTD for structured report definition with hierarchical levels and conditional display logic
- `Line 17`: Parameterized report design allows configurable sections and display options
- `Line 173`: SQL query uses conditional logic to handle different partner selection methods (single partner or extract)
- `Line 1066`: Implements dynamic address formatting based on country-specific address order conventions
- `Line 1219`: Uses function call 'GetPartnerOverallContactSettings' for retrieving contact settings rather than direct SQL query
### publicationstatisticalreport.xml

This XML report file defines a Publication Statistical Report that generates statistics about publications across different counties within a selected country. It contains SQL queries and calculations to retrieve county data, count active partners, and calculate publication statistics including percentages and totals. The report structure includes multiple nested levels for displaying data hierarchically, with calculations for different partner types (donors, churches, applicants) and special handling for partners without county information or from foreign countries. The file uses custom functions like GetCountyPublicationStatistic and CalculatePublicationStatisticPercentage to process the data.

 **Code Landmarks**
- `Line 39`: Uses parameterized SQL queries with {param_cmbCountryCode} to filter data by country
- `Line 59`: Custom function calls embedded in XML structure for specialized data processing
- `Line 73`: Special handling for partners with no county (*NONE*) and foreign partners (*FOREIGN*)
- `Line 196`: Hierarchical report structure with 7 nested levels for organizing statistical data
### relationshipreport.xml

This XML file defines a report template for listing different types of personnel relationships in OpenPetra. The report allows filtering relationships by type, active status, solicitation preferences, and mailing address requirements. It implements SQL queries to retrieve relationship data between partners, with support for both direct and reciprocal relationship views. The file contains multiple calculations that fetch partner details based on their class (Person, Family, Church, Organization, Unit) and retrieves address information. The report structure includes multiple levels to organize the display of partner pairs and their relationship details, with conditional logic to handle different partner types appropriately. Key parameters include relationship types, active status filters, and display options for contact information.

 **Code Landmarks**
- `Line 37`: Supports both direct and reciprocal relationship queries through conditional execution based on param_use_reciprocal_relationship parameter
- `Line 128`: Uses a calculation to conditionally hide rows when mailing address filtering is enabled but no mailing address exists
- `Line 232`: Implements partner-class specific data retrieval through conditional queries based on partner classification
- `Line 542`: Uses a switch statement with multiple cases to handle different partner classes (CHURCH, PERSON, FAMILY, etc.) for proper data display
### subscriptionreport.xml

This XML file defines a subscription report template for OpenPetra that displays partner information including addresses, subscriptions, and gift details. The report allows filtering by individual partner, extract, or current staff. It retrieves partner data from the database including contact information, addresses with validity periods, subscription details, and gift transactions. The report displays address flags for expired or future addresses and handles special formatting for gift information based on system parameters. Key calculations include selecting partners, retrieving addresses, gift details, and subscription information. The report is structured in hierarchical levels (main, partner, address, subscription, gift) to organize the displayed information.

 **Code Landmarks**
- `Line 15-22`: Parameterized report with three selection modes: single partner, extract, or all current staff
- `Line 51-112`: Dynamic SQL query construction based on selection parameters with conditional query segments
- `Line 207-221`: System parameter lookup to control which gift details (amount, recipient, field) are displayed
- `Line 254-277`: Address status flagging system with special indicators for expired, not yet current, and no-mail addresses
- `Line 626-631`: Hierarchical report structure with parent-child relationships between partner, address, and gift information
### supportingchurchesreport.xml

This XML file defines a report for displaying supporting churches and their relationships with partners. It provides options to select one partner, an extract, or all current staff, and sorts results by partner name or key. The report shows church addresses along with their supported partners or officers, displaying contact information and relationships. Key calculations include retrieving church data, supported partners, church officers, and formatting addresses. The report is organized in hierarchical levels, starting with churches and then listing their associated partners with relevant contact details. It includes counters for the total number of churches and individuals printed, and handles special cases like churches without supported partners.

 **Code Landmarks**
- `Line 52`: Uses a complex SQL query with conditional sections based on user parameter selections
- `Line 157`: Organization-specific partner type codes are used (marked with TODO comments)
- `Line 206`: Implements address formatting using concatenation functions with whitespace control
- `Line 244`: Uses counter variables to track totals across report levels
- `Line 422`: Hierarchical report structure with conditional rendering based on data availability
### validbankaccountreport.xml

This XML report template defines a 'Valid Bank Account Report' that checks the validity of bank accounts for partners in OpenPetra. It allows users to select partners by individual key, extract, or all current staff. The report queries banking details from multiple database tables and validates account numbers using a CheckAccountNumber function, displaying results with reasons for validation failures. The report includes partner information, bank details, and validation status, filtering out valid accounts to highlight problematic ones.

 **Code Landmarks**
- `Line 96`: Uses a custom CheckAccountNumber function to validate bank account numbers against country-specific rules
- `Line 104`: Implements conditional row filtering to hide valid accounts from the report output
- `Line 97`: Uses numeric return codes (-1 to 4) to represent different validation statuses

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #