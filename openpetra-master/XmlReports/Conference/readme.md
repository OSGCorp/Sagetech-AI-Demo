# XML Conference Section Of OpenPetra

The OpenPetra is a C# program that provides comprehensive administrative management for non-profit organizations. The program handles financial accounting and contact relationship management through a modular architecture. This sub-project implements conference management reporting capabilities along with XML-based report definition templates. This provides these capabilities to the OpenPetra program:

- Comprehensive attendee reporting with multiple filtering options
- Financial tracking across sending/receiving/charged fields
- Demographic analysis by age, nationality, and language
- Logistical management for accommodation and transportation
- Group and role-based attendance tracking

## Identified Design Elements

1. XML-Based Report Templates: Report definitions use a structured XML format that separates parameters, calculations, and presentation elements
2. Reusable Calculation Components: Common SQL queries and data processing functions are defined in shared files (e.g., conference.xml) for reuse across reports
3. Hierarchical Data Organization: Reports implement multi-level structures for organizing information by fields, families, or groups
4. Flexible Selection Parameters: Reports support consistent filtering options for attendees, conferences, and date ranges

## Overview
The architecture emphasizes a declarative approach to report definition, with clear separation between data retrieval (SQL queries), data processing (calculations), and presentation (formatting). Reports follow consistent patterns for parameter handling and hierarchical data organization. The system supports various output formats and detail levels, allowing users to generate everything from summary statistics to comprehensive attendee listings with financial details. The XML templates provide a maintainable framework for extending reporting capabilities without modifying core application code.

## Business Functions

### Conference Reports
- `conferencerolereport.xml` : XML report definition for generating a conference role report showing attendee statistics by gender and family type.
- `childrenreport.xml` : XML report definition for generating a list of conference attendees under 18 years old with their personal and travel details.
- `groupreports.xml` : XML definition for Conference Group Reports that generates participant listings by different group types.
- `attendeereport.xml` : XML definition for a Conference Attendee Report with configurable parameters and formatting.
- `languagesreport.xml` : XML report definition for generating a conference languages summary showing attendee language distribution.
- `sendingfieldreport.xml` : XML report template for generating conference sending field reports with financial and attendance details.
- `presentattendeereport.xml` : XML report definition for generating a conference attendee report showing present participants with their personal and travel details.
- `familylistingreport.xml` : XML report definition for generating Conference Family Listings with detailed attendee information.
- `arrivalslistingreport.xml` : XML report definition for generating a conference arrivals listing report showing attendees arriving or departing on a specific date.
- `commentsreport.xml` : XML report definition for generating a Conference Attendees comments report with filtering options and detailed attendee information.
- `outreachreport.xml` : XML configuration for conference outreach reports with attendee details and financial calculations.
- `extracostsreport.xml` : XML report definition for tracking extra costs associated with conference attendees in OpenPetra.
- `arrivaldeparturegroupreport.xml` : XML report definition for conference arrival and departure group reports in OpenPetra.
- `conference.xml` : XML configuration file defining conference report parameters, queries, and calculations for attendee data in the OpenPetra system.
- `nationalitysummaryreport.xml` : XML report definition for generating nationality summaries for conference attendees.
- `registeringfieldreport.xml` : XML report definition for generating conference registering field reports with financial and attendance details.
- `agesummaryreport.xml` : XML report definition for generating age summary statistics for conference attendees.
- `chargedfieldreport.xml` : XML report definition for conference charged fields, showing financial and attendance data for conference participants.
- `transportreport.xml` : XML template for conference transportation reports showing arrivals or departures with sorting and filtering options.
- `accommodationreport.xml` : XML report definition for generating accommodation reports for conferences with brief, full, or detailed views of attendee housing arrangements.
- `absenteereport.xml` : XML report definition for generating a list of conference attendees who are absent from events they registered for.
- `receivingfieldreport.xml` : XML report template for generating conference receiving field reports with attendee details and financial information.
- `attendancesummaryreport.xml` : XML report definition for generating conference attendance summary reports with date ranges and attendee selection options.

## Files
### absenteereport.xml

This XML file defines the 'Absentee Report' for OpenPetra's conference management system. It generates a report of registered attendees who have not actually arrived at conferences. The report includes detailed SQL queries that pull attendee information, conference details, travel arrangements, and registration data from multiple database tables. The report supports filtering by specific attendee, all attendees, or attendees from an extract, and can be limited to a single conference or include all conferences. The report displays personal information, arrival/departure details, and conference roles for absent attendees.

 **Code Landmarks**
- `Line 58`: Complex SQL query joins multiple tables to identify absent attendees based on registration status and arrival dates
- `Line 140`: Conditional query construction based on user parameters for flexible report filtering
- `Line 169`: Logic to identify absentees by comparing scheduled arrival dates with current date parameter
### accommodationreport.xml

This XML report file defines the structure and functionality for generating accommodation reports for conferences. It provides three levels of detail (Brief, Full, Detail) for displaying attendee housing arrangements. The report accepts parameters including report detail level, date ranges, and selection criteria for conferences and attendees. Key calculations include selecting attendees based on various criteria, retrieving room bookings, calculating accommodation details for individual attendees, and formatting the final accommodation table. The report structure includes page headers with title and selection information, and multiple processing levels that handle data collection, processing, and display formatting.

 **Code Landmarks**
- `Line 82`: Uses complex SQL query with conditional sections based on user parameter selections for attendee and conference filtering
- `Line 150`: Implements custom function calls to external code with 'NO-SQL' marker for specialized accommodation calculations
- `Line 186`: Uses a multi-level report structure with dummy levels to initialize and finalize data processing
- `Line 138`: Complex date range logic to find accommodations that overlap with the selected date period
### agesummaryreport.xml

This XML file defines the 'Age Summary Report' for conference attendees, providing demographic statistics by age and gender. The report allows filtering by specific attendee, all attendees, or extract selections, and by specific conference or all conferences. It queries attendee data from multiple database tables, calculates ages from birth dates, and presents results in a structured format with totals by gender. The file includes SQL queries, parameter definitions, and layout specifications for displaying age distribution statistics in a tabular format.

 **Code Landmarks**
- `Line 107`: Uses custom function 'CalculateSingleAge' to process birth dates and gender data
- `Line 115`: Implements table management with 'ClearAgeTable' and 'FinishAgeTable' functions
- `Line 177`: Uses a multi-level report structure with dummy levels to initialize and finalize data processing
### arrivaldeparturegroupreport.xml

This XML file defines a report for conference arrival and departure groups in OpenPetra. It generates lists of attendees grouped by their arrival or departure groups based on user-selected parameters. The report queries conference attendee data from multiple database tables, filtering by partner, conference selection, and group type. Key functionality includes displaying attendee details with travel information, accommodating different selection criteria (single attendee, all attendees, or extract-based), and handling special cases like unassigned groups. The file contains SQL queries that join partner, person, conference, and application tables to retrieve comprehensive attendee information organized by their travel groups.

 **Code Landmarks**
- `Line 12`: Report supports dynamic title switching between 'Arrival Groups Report' and 'Departure Groups Report' based on parameter selection
- `Line 79`: Complex SQL query with multiple conditional segments that adapt based on user parameter selections
- `Line 254`: Special handling for empty/null group values, displaying them as 'Unassigned' in the report output
- `Line 85`: Implements flexible attendee selection through three different methods: single attendee, all attendees, or extract-based filtering
### arrivalslistingreport.xml

This XML file defines the 'Arrivals Listing Report' for OpenPetra, which generates a listing of conference attendees arriving or departing on a specific date. The report accepts parameters including report day type (arrival/departure), report date, conference selection, and attendee filtering options. It implements a SQL query that retrieves detailed attendee information including personal details, conference data, travel arrangements, and group assignments. The report structure includes page headers with title and parameter information, and organizes data in hierarchical levels for proper formatting. The main calculation performs a complex database query joining multiple tables to retrieve all relevant attendee information filtered by the selected parameters.

 **Code Landmarks**
- `Line 76`: Complex SQL query joins 8 tables to gather comprehensive attendee information including personal details, conference data, and travel arrangements
- `Line 36`: Conditional formatting based on report type (arrival vs departure) using switch/case structure
- `Line 127`: Flexible attendee selection logic with three different filtering methods: single attendee, all attendees, or from an extract
### attendancesummaryreport.xml

This XML file defines the structure of an Attendance Summary Report for conferences in OpenPetra. It specifies report parameters for date ranges, page headers with title and description fields, and calculations to retrieve attendee data. The report allows filtering by specific attendees, all attendees, or from an extract, and by conference selection. Key calculations include 'Select Attendees' which retrieves attendance data, 'CalculateSingleAttendance' for processing individual records, and functions to manage an attendance table. The report displays dates, totals, actual and expected attendance, and children counts.

 **Code Landmarks**
- `Line 85`: Complex SQL query with dynamic conditions based on multiple parameter selections for attendee and conference filtering
- `Line 140`: Uses custom function 'CalculateSingleAttendance' to process attendance data with multiple parameters
- `Line 147`: Implements a 'NO-SQL' approach with custom functions to manage attendance data outside standard SQL queries
### attendeereport.xml

This XML file defines the structure for an Attendee Report in the OpenPetra conference management system. It specifies report parameters, headers, and calculation logic for displaying conference attendee information. The report supports different selection criteria including individual attendees, all attendees, or attendees from an extract. It also allows filtering by specific conferences or all conferences, with options for displaying outreach codes. The report uses a hierarchical structure with a main level and an 'Attendee Details' sublevel that renders attendee information through the GetOtherDetails calculation.

 **Code Landmarks**
- `Line 4`: Report structure follows the reports.dtd document type definition
- `Line 18-42`: Conditional field rendering based on parameter values using eq() function
- `Line 71`: Uses lowerLevelReport to create a hierarchical report structure
### chargedfieldreport.xml

This XML file defines a report for conference charged fields, which displays financial and attendance information for conference participants. The report allows filtering by conference, attendee selection, and charged field options. Key functionality includes calculating field costs, displaying financial details, showing attendee information, and generating sign-off lines. The report contains multiple calculation sections that query attendee data, process financial information, and format output. Important calculations include SelectChargedFields, Select Attendees for field, InitFieldCostsCalculation, PrintFieldFinancialCosts, and GetExtraCosts. The report structure is organized into nested levels with headers, details, and footers that present the information in a structured format with appropriate formatting and alignment.

 **Code Landmarks**
- `Line 59`: Uses conditional query construction based on parameter selections for flexible report generation
- `Line 76`: Implements complex SQL query with multiple joins across partner, person, conference and application tables
- `Line 173`: Uses custom functions like InitFieldCostsCalculation to handle financial calculations outside SQL
- `Line 201`: Implements dynamic data formatting with functions like formattime and calculateAge
- `Line 334`: Uses conditional flag explanations that only appear when relevant discount types exist in the data
### childrenreport.xml

This XML report file defines a 'Children Report' for conference attendees under 18 years old. It queries attendee data from multiple database tables to display personal information (name, gender, birthday, age), conference details, travel arrangements (arrival/departure dates, transportation needs), and accommodation information. The report supports filtering by specific attendee, all attendees, or from an extract, and can be limited to a single conference or include all conferences. Key calculations include 'Select Attendees' which retrieves the core data and 'GetOtherChildDetails' which formats additional information like passport details, travel times, and room allocations.

 **Code Landmarks**
- `Line 126`: Uses custom function GetPassport() to retrieve passport information for each attendee
- `Line 128-129`: Custom time formatting for arrival and departure times using formattime() function
- `Line 130`: Calculates attendee age based on birthday using calculateAge() function
- `Line 131-133`: Uses helper functions to retrieve room and travel point information with fallback values
### commentsreport.xml

This XML file defines a 'Comments Report' for conference attendees in OpenPetra. It specifies the report structure including parameters, headers, and calculations needed to generate a report showing comments associated with conference attendees. The report allows filtering by specific attendees, all attendees, or attendees from an extract, and by specific conferences or all conferences. It retrieves detailed information about attendees including personal details, conference participation, arrival/departure information, and comments. Key calculations include 'Select Comments' which retrieves the data from the database and 'GetCommnetDetails' which processes additional information like age, room allocation, and transport needs.

 **Code Landmarks**
- `Line 12`: Report parameter allows hiding entries with no comments
- `Line 94-142`: Complex SQL query joins multiple tables to collect comprehensive attendee information including personal details, conference data, and travel arrangements
- `Line 168-183`: Custom functions process additional data like passport information, age calculation, and room allocation
- `Line 177`: Conditional row filtering based on user parameter to hide entries without comments
### conference.xml

This XML file defines a report template for the Conference module in OpenPetra that provides common calculations needed by several conference reports. It establishes report parameters for conference and attendee selection, and implements detailed SQL queries to retrieve attendee information. The file contains two main calculations: 'Select Attendee Details' which retrieves comprehensive attendee data from multiple database tables, and 'GetOtherDetails' which processes additional information like passport details, arrival/departure times, and age calculations. The file also defines numerous smaller calculations that format and present specific attendee attributes such as personal details, conference roles, travel information, and group assignments. These calculations serve as building blocks that can be reused across different conference reports.

 **Code Landmarks**
- `Line 36`: Main SQL query joins multiple tables to collect comprehensive attendee information including personal details, conference data, and travel arrangements
- `Line 125`: Uses conditional query construction based on parameter values to filter attendees by extract or specific conference
- `Line 166`: NO-SQL function calls retrieve additional data and perform formatting operations not available in standard SQL
### conferencerolereport.xml

This XML file defines a Conference Role Report for OpenPetra that summarizes conference roles by attendee demographics. It includes report parameters for selecting specific attendees or conferences, and implements SQL queries to gather attendee data. The report calculates statistics based on gender and family status using custom functions like CalculateSingleConferenceRole. The report structure includes multiple calculation sections for displaying totals of attendees by categories (Male, Female, Couple, Family) and organizes data through several nested report levels.

 **Code Landmarks**
- `Line 114`: Uses custom function CalculateSingleConferenceRole to process demographic data by gender, family, and conference parameters
- `Line 126`: Implements a pattern of clearing, populating, and finalizing a conference role table through sequential function calls
- `Line 221`: Uses a multi-level report structure with dummy levels to manage data processing flow
### extracostsreport.xml

This XML file defines the 'Extra Costs Report' for OpenPetra's conference management system. It generates a report showing extra costs incurred by conference attendees. The report includes parameters for selecting specific conferences, attendees, and charged fields. The main SQL query retrieves detailed information about attendees, conferences, and associated extra costs, including cost types, amounts, and authorizing information. The report structure includes page headers with titles and selection criteria, and organizes data in hierarchical levels for proper display. Calculations are defined to format and present cost data including cost types, amounts, authorizing fields and persons, and comments.

 **Code Landmarks**
- `Line 60`: Supports multiple attendee selection methods (single attendee, all attendees, or from extract)
- `Line 107`: Handles different conference selection scenarios with conditional query building
- `Line 173`: Implements field-level filtering for charged fields using conditional query construction
### familylistingreport.xml

This XML file defines a Conference Family Listing report that displays information about families attending conferences. The report can filter by specific attendees, all attendees, or from an extract, and can focus on one conference or all conferences. It retrieves family data and detailed attendee information through two main SQL queries: 'Select Families' and 'Get Family Details'. The queries collect comprehensive information including personal details, conference participation, travel arrangements, and group assignments. The report is structured with three levels: main, Family Details, and Attendee Details, allowing hierarchical presentation of information.

 **Code Landmarks**
- `Line 62`: Complex SQL query with dynamic WHERE clauses based on multiple parameter conditions
- `Line 122`: Detailed data collection gathering over 30 fields across 8 database tables with multiple joins
- `Line 196`: Hierarchical report structure with conditional rendering based on family member count
### groupreports.xml

This XML file defines a Conference Group Reports template for OpenPetra that generates reports for different types of conference groups (discovery, fellowship, or work groups). The report allows filtering by group type, participant role, and conference selection. It implements SQL queries that retrieve group information and attendee details, with options to show only participants or those with assigned groups. The report organizes data hierarchically by group and attendee, displaying personal information, travel details, and group assignments. Key calculations include 'Select Groups', 'Select Attendee from group', and several helper functions that track participant counts. The report supports various output formats with customizable headers and footers that show group totals and overall participant counts.

 **Code Landmarks**
- `Line 41`: Dynamic report title generation based on group type parameter selection
- `Line 98`: Complex SQL query with multiple conditional sections based on report parameters
- `Line 209`: Flexible attendee filtering with extract, single attendee, or all attendees options
- `Line 287`: Conditional filtering to handle null/empty group assignments
- `Line 358`: Custom calculation using function calls rather than SQL queries
### languagesreport.xml

This XML report definition creates a 'Languages Report' that summarizes the languages spoken by conference attendees. It allows filtering by specific attendee, all attendees, or from an extract, and can target either a single conference or all conferences. The report queries the database to count attendees by language, grouping results by language description. The report includes a page header with selection criteria and conference details, and displays results in a tabular format showing language names and corresponding counts. Key calculations include 'Select Languages', 'Language', and 'Number', which retrieve and format the language data for display.

 **Code Landmarks**
- `Line 73`: SQL query dynamically adapts based on multiple selection parameters (attendee selection, conference selection)
- `Line 92`: Uses conditional query segments to handle different conference selection modes
- `Line 111`: Filters applications by status codes beginning with 'A' or 'H'
### nationalitysummaryreport.xml

This XML report file defines the Nationality Summary Report for conferences in OpenPetra. It generates statistics about attendees' nationalities, broken down by gender and language. The report supports filtering by specific attendee, all attendees, or from an extract, and can target one conference or all conferences. It uses custom functions like CalculateNationalities, ClearNationalityTable, and FinishNationalityTable to process data and display results in a structured format with columns for nationalities, totals, gender breakdowns, and languages.

 **Code Landmarks**
- `Line 86`: Uses a complex SQL query with conditional segments that change based on report parameters
- `Line 142`: Implements custom function calls with 'NO-SQL' tag to process nationality data outside the database layer
- `Line 205`: Uses a multi-level report structure with dummy levels to initialize and finalize data processing
### outreachreport.xml

This XML file defines a report template for conference outreach activities within OpenPetra. It generates detailed information about outreach participants, including personal details, travel arrangements, and financial data. The report can be configured to show different levels of detail through various parameters like showing financial reports, accepted applications, or extra costs. Key functionality includes selecting outreaches and attendees, calculating field costs, retrieving passport information, and generating financial summaries. The file contains multiple calculation elements that perform database queries and custom functions to format and present the data in organized levels with appropriate headers, details, and footers.

 **Code Landmarks**
- `Line 59`: Uses a complex SQL query with conditional segments based on report parameters to select attendee data
- `Line 240`: Implements custom functions for financial calculations that are initialized, used, and cleared during report generation
- `Line 291`: Contains multiple dummy calculation fields that likely serve as placeholders for dynamic data insertion
- `Line 428`: Uses conditional flag explanations that only appear when specific discount types are present in the report
- `Line 497`: Implements a hierarchical report structure with multiple nested levels for organizing different sections of output
### presentattendeereport.xml

This XML file defines a report template for displaying conference attendees who are currently present at events. It specifies the report structure with headers, parameters, and detailed SQL queries to retrieve attendee information. The report can filter by individual attendee, all attendees, or from an extract list, and can target a single conference or all conferences. The SQL query retrieves comprehensive attendee data including personal details, conference information, travel arrangements, and attendance status, focusing specifically on attendees who have arrived but not yet departed.

 **Code Landmarks**
- `Line 60`: SQL query filters for present attendees by checking actual arrival dates against current date and ensuring departure date is either null or in the future
- `Line 42`: Dynamic query construction based on selection parameters (one attendee, all attendees, or from extract)
- `Line 50`: Conditional query segments handle different conference selection modes
### receivingfieldreport.xml

This XML file defines a report template for conference receiving fields, displaying attendee information organized by their receiving field. The report provides detailed information about conference attendees including personal details, travel arrangements, accommodation, and financial costs. Key functionality includes filtering attendees by conference, extracting attendee data from various database tables, calculating financial costs per field, and generating sign-off lines. The report supports different detail levels (summary or full) and optional sections like financial reports and extra costs. Important calculations include SelectReceivingFields, Select Attendees for field, PrintFieldFinancialCosts, GetExtraCosts, and CalculateOneAttendeeFieldCost. The template uses multiple nested levels to organize data hierarchically by receiving field and attendee.

 **Code Landmarks**
- `Line 60`: Uses a union of two SQL queries to find attendees - first from staff table, then from person/family 'Worker-ER field'
- `Line 175`: Implements conditional display of financial information based on parameter settings
- `Line 341`: Uses custom functions like InitFieldCostsCalculation and PrintFieldFinancialCosts for complex financial calculations
- `Line 391`: Implements detailed flag system to indicate various discount types in financial reporting
- `Line 450`: Handles attendees with no assigned field separately from those with fields
### registeringfieldreport.xml

This XML file defines a report for conference registering fields in OpenPetra. It generates detailed information about conference attendees organized by their registering offices. The report includes functionality for financial reporting, attendance tracking, and application status monitoring. Key calculations include selecting registering fields, retrieving attendee information, calculating costs, and generating financial summaries. The report supports various filtering options through parameters like selected fields, conference selection, attendee selection, and report detail level. It can display financial details with discount flags, travel information, accommodation costs, and missing information for each attendee. The report structure consists of multiple nested levels that organize data hierarchically by registering office and attendee.

 **Code Landmarks**
- `Line 60`: Uses a CSV function to dynamically build SQL query conditions from selected field keys
- `Line 75`: Complex SQL query joins multiple tables to retrieve comprehensive attendee information
- `Line 225`: Implements custom functions for financial calculations that are called from XML
- `Line 367`: Uses conditional flag explanations that only appear when specific discount types exist in the data
- `Line 434`: Implements different report detail levels (Summary vs Full) that control information density
### sendingfieldreport.xml

This XML file defines a report template for generating sending field reports for conferences in OpenPetra. The report provides information about attendees from different sending fields (home offices) participating in conferences. It includes functionality to display attendee details, financial information, travel arrangements, and accommodation. The report supports various filtering options including by conference, attendee selection, and extract. Key calculations include SelectSendingFields, Select Attendees for field, InitFieldCostsCalculation, PrintFieldFinancialCosts, and GetExtraCosts. The report can generate financial summaries with discount explanations, sign-off lines for financial or attendance verification, and detailed or summary views of attendee information. The template uses conditional logic to customize output based on user-selected parameters.

 **Code Landmarks**
- `Line 59`: Uses a complex nested query structure with multiple conditional sections to filter attendees based on user parameters
- `Line 164`: Implements NO-SQL function calls to external code for financial calculations and report formatting
- `Line 196`: Uses dynamic variable assignment for calculated values like age and formatted times
- `Line 401`: Implements conditional explanation sections for financial discount flags based on their presence in the data
### transportreport.xml

This XML file defines a report template for conference transportation, allowing users to generate either arrival or departure transport reports. The template includes parameters for sorting, filtering by travel date, transport needs, and incomplete details. The report queries detailed attendee information including personal data, conference details, travel dates, transport types, and group assignments. It supports filtering by specific attendee, all attendees, or from an extract. The report structure includes page headers with title and description fields that change based on selected parameters, and a main calculation that retrieves all relevant transport data from multiple database tables.

 **Code Landmarks**
- `Line 18-26`: Dynamic title generation based on report parameters (arrivals vs departures)
- `Line 79-166`: Complex SQL query joining multiple tables with conditional WHERE clauses based on user parameters
- `Line 168-196`: Conditional filtering logic for transport needs, incomplete details, and travel dates

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #