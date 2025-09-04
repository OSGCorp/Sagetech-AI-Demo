# XML Personnel Section Of OpenPetra

The XML Personnel Section is a configuration-driven reporting framework within OpenPetra that provides comprehensive personnel data reporting capabilities. This sub-project implements a collection of XML-based report templates and shared calculation components that enable consistent, flexible reporting across the personnel management domain. The architecture follows a template-based approach where report definitions encapsulate both data retrieval logic and presentation formatting.

This provides these capabilities to the OpenPetra program:

- Parameterized SQL queries for personnel data extraction
- Reusable calculation components across multiple reports
- Hierarchical data presentation with conditional rendering
- Flexible filtering options (by partner, extract, or staff status)
- Standardized formatting for personnel-specific data types

## Identified Design Elements

1. **Shared Calculation Library**: Common personnel calculations are centralized in `personnel.xml` and `commonpersonnel.xml`, providing reusable query components across reports
2. **Hierarchical Report Structure**: Reports use nested levels to represent relationships between data entities (partners, family members, documents)
3. **Parameterized Queries**: SQL queries accept runtime parameters for flexible data filtering and presentation
4. **Conditional Rendering**: Report sections can be dynamically shown/hidden based on user-selected parameters
5. **Data Transformation Pipeline**: Raw database values are processed through calculation functions to produce formatted display values

## Overview
The architecture emphasizes reusability through shared calculation components, consistent data retrieval patterns, and standardized presentation formatting. Reports are organized by functional domain (emergency contacts, commitments, documents, etc.) with clear separation between data retrieval logic and presentation formatting. The template-based approach allows for maintainable, extensible reporting capabilities while ensuring consistent data handling across the personnel management domain.

## Business Functions

### Personnel Reports
- `personnel.xml` : XML configuration file defining personnel report calculations and parameters for OpenPetra's reporting system.
- `endofcommitmentreport.xml` : XML report definition for generating end of commitment reports for personnel in OpenPetra.
- `outreachoptions.xml` : XML report definition for listing outreach options with filtering and sorting capabilities
- `languagesreport.xml` : XML report definition for generating a personnel languages report with filtering options for application status and event selection.
- `jobassignmentreport.xml` : XML report definition for generating job assignment reports for personnel in OpenPetra.
- `birthdaylist.xml` : XML report template for generating birthday lists of partners, staff, or extracts with customizable parameters and sorting options.
- `startofcommitmentreport.xml` : XML report definition for generating a Start of Commitment Report showing active partners within a specified date range with optional commitment status filtering.
- `lengthOfCommitmentReport.xml` : XML report definition for generating Length of Commitment and Anniversary reports for personnel in OpenPetra.
- `personaldocumentexpiryreport.xml` : XML report definition for generating a list of personal documents that have expired or will expire for partners or staff.
- `personaldatareport.xml` : XML report template for generating personal data reports for personnel in OpenPetra
- `abilitiesreport.xml` : XML report definition for generating a short termer abilities report in OpenPetra's personnel module.
- `progressreport.xml` : XML report definition for generating progress reports from personnel evaluations data.
- `previousexperiencereport.xml` : XML report definition for displaying partners' previous work experiences with dates, locations, roles, and organizations.
- `generalshorttermerreport.xml` : XML report template for generating a general short termer applicant information report in OpenPetra.
- `passportexpiryreport.xml` : XML report definition for generating passport expiry reports for personnel with filtering options and detailed passport information.
- `shorttermerreport.xml` : XML report template defining short-termer participant data for events with comprehensive query parameters and display calculations.
- `unithierarchy.xml` : XML report definition for displaying unit hierarchy in OpenPetra's Personnel module.

### Emergency Contact Management
- `emergencycontactreport.xml` : XML report definition for generating emergency contact information for event participants in OpenPetra.
- `emergencycontactreportstaff.xml` : XML report template for generating emergency contact information for staff members in OpenPetra.
- `emergencydatareport.xml` : XML report definition for generating emergency data reports for personnel in OpenPetra.

### Data Layer Components
- `commonpersonnel.xml` : XML template defining common database queries for personnel reports in OpenPetra

## Files
### abilitiesreport.xml

This XML file defines an 'Abilities Report' for OpenPetra's personnel module, specifically for short-term workers. It structures SQL queries to extract data about participants' abilities, experience levels, and personal information. The report allows filtering by application status (accepted, rejected, etc.), event selection (specific event, related options, or all events), and data source (extract or event). The report organizes information hierarchically, first by ability type and then by participant, displaying details like ability level, years of experience, whether they're bringing instruments, and personal information. The report includes parameters for customizing output and formatting options.

 **Code Landmarks**
- `Line 80`: Flexible SQL query construction with conditional segments based on user parameters for event selection and data source
- `Line 183`: Comprehensive application status filtering with detailed status codes for accepted, cancelled, rejected, on hold, and enquiry statuses
- `Line 329`: Non-SQL calculation that formats data by concatenating level numbers with descriptions and converting boolean values to yes/no text
- `Line 401`: Hierarchical report structure with three nested levels: main, Partner Abilities, and Partner Level
### birthdaylist.xml

This XML report template defines a 'Birthday List' report for OpenPetra that lists birthdays of partners, extracts, or current staff. It includes various parameters for customization such as date ranges, family member inclusion, partner type filtering, and anniversary options. The report structure consists of header sections, calculations that query data from the server, and display levels. Key calculations include 'Select Partners', 'Select Family Keys', and 'GetCommitmentPeriodAndEmail'. The report displays personal information including names, contact details, dates of birth, commitment periods, and ages. The template supports different sorting options and conditional display of information based on user-selected parameters.

 **Code Landmarks**
- `Line 40-53`: Comprehensive parameter system allowing multiple selection modes and filtering options
- `Line 92-96`: Server-side calculation using C# backend functions rather than direct SQL queries
- `Line 106-112`: Dynamic type assignment based on conditional parameters
- `Line 254-266`: Multi-level report structure with parent-child relationships for family data
### commonpersonnel.xml

This XML file defines common calculations and database queries used across personnel reports in OpenPetra. It provides reusable query components for retrieving staff information, family members, passports, personal documents, special needs, skills, languages, and contact details. The file includes parameterized queries that can filter by individual partner, extract, or current staff status. Key calculations include 'Select Partners', 'GetFamilyMembers', 'SelectPassports', 'SelectPersonalDocuments', 'SelectSpecialNeeds', 'SelectSkills', and 'SelectLanguages'. The file also includes functions for retrieving partner addresses and formatting contact information.

 **Code Landmarks**
- `Line 13`: Defines parameterized queries that can be filtered by different selection criteria (single partner, extract, or all current staff)
- `Line 123`: Uses SQL-free function calls with GetPartnerBestAddress() to retrieve formatted address information
- `Line 127`: Implements conditional value assignment for contact information fields when values are missing
### emergencycontactreport.xml

This XML file defines the Emergency Contact Report for conferences, outreaches, and short-term missions in OpenPetra. It specifies report parameters including event selection, participant source (from an extract or all participants), and application status filters. The report queries participant data and their emergency contacts (parents, guardians, relatives) from the database, displaying contact information including addresses, phone numbers, and relationship types. The report structure includes multiple calculation blocks that retrieve and format partner data, emergency contact details, and address information. The layout is organized in hierarchical levels, with participants as the main level and their emergency contacts as sub-levels.

 **Code Landmarks**
- `Line 76`: Uses conditional query construction to filter participants based on event selection type (this event, related events, or all events)
- `Line 155`: Implements complex application status filtering with multiple status codes for accepted, cancelled, hold, and rejected statuses
- `Line 196`: Uses relationship type filtering to identify emergency contacts with specific relationship codes
- `Line 210`: Leverages custom functions like GetPartnerBestAddress to retrieve contact information
- `Line 224`: Uses concatenation functions to format address fields from multiple database columns
### emergencycontactreportstaff.xml

This XML report template defines the structure and queries for generating an Emergency Contact Report for staff members. It includes parameters for data source selection, report formatting options, and page headers with title and description fields. The file contains SQL queries to retrieve partner information based on different selection criteria (single partner, extract, or all current staff). The report is organized in hierarchical levels (main, Partner Level, EmergencyDetailLevel) to display emergency contact information for each staff member, with appropriate formatting and spacing.

 **Code Landmarks**
- `Line 42`: SQL query dynamically adapts based on selection parameter (one partner, extract, or all current staff)
- `Line 83`: Hierarchical report structure with three nested levels for organizing emergency contact information
- `Line 89`: Uses external calculation 'MakePartnerHeader' to format partner information in the header
### emergencydatareport.xml

This XML file defines the Emergency Data Report for OpenPetra's personnel management system. It structures a comprehensive report that displays critical emergency information about staff members. The report includes multiple sections that can be conditionally displayed: personal details, family members, addresses, passports, personal documents, emergency data (height, weight, blood type), special needs, skills, languages, emergency contacts, and proof-of-life questions. The file implements numerous SQL queries to retrieve data from various database tables and contains calculation functions to format and process this information. The report is organized in a hierarchical structure with multiple nested levels, allowing for detailed presentation of emergency-related information for each staff member.

 **Code Landmarks**
- `Line 10`: Report parameters control what sections appear in the final report through conditional display logic
- `Line 84`: Uses SQL queries with parameterized values to retrieve emergency contact information from partner relationships
- `Line 128`: Implements a non-SQL calculation that formats partner addresses through a series of concatenation functions
- `Line 177`: Hierarchical level structure with parent-child relationships for organizing complex emergency data
- `Line 396`: Conditional formatting logic to handle special cases like vegetarian dietary needs
### endofcommitmentreport.xml

This XML report definition implements a report that displays end of commitment dates for personnel. It supports three selection modes: one partner, an extract, or all current staff. The report queries staff data including commitment dates, status, and field assignments. It displays partner information including name, key, commitment dates, field name, commitment type, partner type, and contact details. The report includes parameters for selection mode, extract name, partner key, and sorting options. Key calculations include partner selection, partner type listing, and address retrieval.

 **Code Landmarks**
- `Line 54`: Dynamic SQL query construction based on selection parameter (one partner, extract, or all staff)
- `Line 107`: Organization-specific partner type codes are used in the List Types calculation
- `Line 114`: Non-SQL function call to GetPartnerBestAddress for retrieving contact information
### generalshorttermerreport.xml

This XML file defines a report template for displaying general information about short-term applicants in OpenPetra. It structures a report with parameters for filtering by application status (accepted, cancelled, rejected, enquiry, hold) and data source (event, extract, or entire database). The template includes page headers with title and sorting information, a telephone calculation, and multiple report levels for displaying partner details in either one or two-line format. The report is designed to present applicant information in a structured format with conditional rendering based on user-selected parameters.

 **Code Landmarks**
- `Line 54`: Implements conditional field display based on application status parameters
- `Line 76`: Uses a switch/case structure to determine report layout based on user preference
- `Line 81`: Implements hierarchical report levels with parent-child relationships for structured data presentation
### jobassignmentreport.xml

This XML file defines a Job Assignment Report for OpenPetra's personnel module. It allows users to generate reports of job assignments for a single partner, partners in an extract, all current staff, or all staff in the database. The report includes parameters for selection criteria, sorting options, and date filtering. It retrieves data through SQL queries that join various personnel tables to collect information about job assignments, including partner details, position names, start/end dates, and unit information. The report displays partner names, keys, role information, assignment dates, and contact details in a structured format.

 **Code Landmarks**
- `Line 11`: Flexible report parameter system with conditional options based on selection type
- `Line 65`: Dynamic SQL query construction that changes based on the selection parameter
- `Line 143`: Non-SQL calculation using GetPartnerBestAddress function to retrieve address data
### languagesreport.xml

This XML file defines a report template for generating a Languages Report in OpenPetra's Personnel module. It specifies report parameters for filtering by application status (accepted, cancelled, rejected, etc.) and data source (outreach, conference, extract, or entire database). The report queries personnel language data from multiple database tables including partner information, person languages, applications, and language codes. The report includes calculations for displaying partner details, language proficiency levels, years of experience, and application status. The template supports different sorting options and conditional query details based on user-selected parameters.

 **Code Landmarks**
- `Line 83`: Complex conditional query structure that adapts based on multiple parameter combinations for event selection and data source
- `Line 168`: Comprehensive application status filtering with multiple status codes for cancelled, hold, and rejected statuses
- `Line 31`: Dynamic page header that changes based on selected application status and data source parameters
### lengthOfCommitmentReport.xml

This XML report definition file configures the 'Length Of Commitment' report that lists commitment duration and optional anniversaries for current staff. It defines report parameters including date ranges, sorting options, and anniversary filtering. The report includes calculations for retrieving personnel data through server queries, displaying information such as names, contact details, country of service, commitment dates, and commitment length. The report structure consists of a main level that calls a Partner Detail level, which displays comprehensive information about each staff member including contact information and commitment details.

 **Code Landmarks**
- `Line 59`: Uses a server-side query function 'GetLengthOfCommitment' to retrieve personnel commitment data
- `Line 70`: Implements dynamic address retrieval using GetPartnerBestAddress function with variable assignment for telephone numbers
- `Line 155`: Hierarchical report structure with main level and Partner Detail sublevel identified by partner key
### outreachoptions.xml

This XML report definition file creates a report that lists outreach options according to specified parameters. It defines report parameters for filtering by year, outreach code, and sorting preferences. The report queries the database to retrieve outreach information including codes, unit details, and date ranges. Key calculations include 'Select Outreaches' which retrieves the main data, and several field calculations for displaying unit names, keys, dates, and codes. The report structure includes page headers with conditional display logic and two levels: 'main' and 'Outreach Detail'.

 **Code Landmarks**
- `Line 42`: Implements conditional display logic in page header using switch/case structure
- `Line 65`: SQL query with dynamic filtering based on parameter existence
- `Line 101`: Custom formatting of date values using 'formatteddate' format specification
### passportexpiryreport.xml

This XML file defines a Passport Expiry Report for the OpenPetra system. It queries passport details from the database with options to filter by a single partner, an extract, or all current staff. The report displays comprehensive passport information including expiry dates, passport numbers, nationality, issue details, and personal information. The file structures SQL queries to retrieve data from multiple tables including p_partner, p_person, pm_passport_details, and p_country. It includes calculations for formatting and displaying various passport-related fields and organizes output in a hierarchical structure with main and partner detail levels.

 **Code Landmarks**
- `Line 42`: SQL query joins multiple tables to collect comprehensive passport and personal information
- `Line 74`: Conditional query construction based on selection parameter (one partner, extract, or all current staff)
- `Line 106`: Custom function calls for retrieving partner fields with date-aware variations
- `Line 109`: Organization-specific GetType function with OMER/EX-OMER/ASSOC parameters
### personaldatareport.xml

This XML file defines the structure and content of a Personal Data Report in OpenPetra. It specifies multiple SQL queries to retrieve personnel information including local partner data, job assignments, commitment periods, passports, personal documents, special needs, skills, languages, and previous experiences. The report is organized into hierarchical levels with detailed formatting instructions for displaying partner information. The template includes conditional rendering based on user-selected parameters, allowing customization of which sections appear in the final report. The file uses XML elements like calculations, levels, fields, and values to structure the data presentation, with specific formatting for different data types.

 **Code Landmarks**
- `Line 41`: Uses SQL queries with parameterized values to retrieve personnel data from multiple database tables
- `Line 68`: Implements conditional rendering based on data type with a switch-case structure for different field formats
- `Line 287`: Hierarchical level structure allows for complex nested data presentation with headers, details, and footers
- `Line 435`: Uses conditional logic to control report section visibility based on user parameters
### personaldocumentexpiryreport.xml

This XML report definition file configures a 'Personal Documents Expiry Report' that tracks expired or soon-to-expire personal documents for partners or staff. It allows filtering by partner, extract, or all current staff, with options to specify document types and expiration date ranges. The report includes SQL queries to retrieve document details including type, ID, start date, expiry date, issue information, and comments. The report displays this information in a structured format with appropriate headers and sorting options.

 **Code Landmarks**
- `Line 11-15`: Implements three distinct selection modes (one partner, extract, or all current staff) with conditional parameter requirements
- `Line 91-107`: SQL query dynamically adapts based on selection parameters with different JOIN conditions for each selection mode
- `Line 108-112`: Conditional date filtering that handles both date ranges and null expiration dates
### personnel.xml

This XML file defines common calculations and parameters used across multiple reports in the Personnel module of OpenPetra. It establishes report parameters including extract name, partner key, unit key, and selection options. The file implements several calculation functions that retrieve and format personnel data such as partner names, commitment periods, and address components (street, city, postal code, country, etc.). These calculations serve as reusable components that can be referenced by other personnel reports to maintain consistency and reduce duplication.

 **Code Landmarks**
- `Line 33`: Implements a custom function to concatenate partner key and name with specific formatting
- `Line 42`: Uses a custom GetCurrentCommitmentPeriod function that takes partner key and date parameters
- `Line 19`: Conditional parameter display based on selection value using the condition attribute
### previousexperiencereport.xml

This XML report definition creates a 'Previous Experience Report' for displaying partners' work history. It allows filtering by individual partner, extract, or all current staff, and includes sorting options. The report queries partner data, past experience records, and staff data to display start/end dates, locations, roles, and organizations where partners worked previously. It handles special formatting for internal organization experience versus external organizations. The report structure includes report parameters, headers, calculations with SQL queries, and presentation levels for organizing the data hierarchically.

 **Code Landmarks**
- `Line 58`: SQL query joins multiple tables conditionally based on selection parameter
- `Line 87`: Conditional logic to display organization name differently based on whether work was internal or external
- `Line 94`: TODO comment indicates organization-specific code that needs customization
### progressreport.xml

This XML file defines a Progress Report template for the OpenPetra system that displays personnel evaluation data. It structures a report that can show evaluation information for a single partner, an extract, or all current staff. The report includes fields for report date, type, reporter, next report date, and comments. The main SQL query retrieves data from personnel tables including p_person, p_partner, and pm_person_evaluation. The file supports different selection criteria and sorting options, with appropriate page headers and field formatting for dates and text values.

 **Code Landmarks**
- `Line 42`: Conditional query construction based on selection parameter (one partner, extract, or all staff)
- `Line 65`: Date comparison logic for filtering current staff based on commitment dates
- `Line 9`: Report requires personnel.xml as a dependency for proper functioning
### shorttermerreport.xml

This XML report template defines a comprehensive report for short-term mission participants, providing detailed participant information for events. The file implements SQL queries to extract participant data from the database with flexible filtering options based on event selection, application status, and data source (direct event or extract). The main query retrieves personal details, application status, event participation, travel arrangements, and fellowship group information. The file contains numerous calculation elements that format and prepare data for display, including personal information, contact details, travel arrangements, passport information, medical needs, and church information. It supports various report parameters for customization, such as hiding empty lines, showing passport details, and filtering by application status. The calculations section transforms raw data into formatted display values and handles conditional display logic.

 **Code Landmarks**
- `Line 36`: Main SQL query uses multiple conditional query segments to build different queries based on parameter selections
- `Line 193`: CleanUp calculation uses function assignments to format time values and retrieve additional data through external functions
- `Line 217`: Conditional query details allow selective inclusion of additional information based on report parameters
- `Line 239`: Boolean values are transformed into human-readable text through conditional assignments
### startofcommitmentreport.xml

This XML file defines a report template for the Start of Commitment Report in OpenPetra's Personnel module. It queries active partners with commitments starting within a specified date range, with options to filter by commitment status types. The report includes partner details such as name, key, commitment dates, status, field name, address, and contact information. Key calculations include 'Select Partners' which retrieves the main partner data, 'GetAddressAndEmail' for contact details, and various formatting calculations for displaying partner information. The report parameters allow filtering by date range and commitment status, with options to include partners with no status.

 **Code Landmarks**
- `Line 96`: SQL query uses conditional XML to modify query structure based on user parameter selections for commitment status filtering
- `Line 130`: Uses function call 'GetPartnerBestAddress' to retrieve contact information for each partner
- `Line 42`: Dynamic report header construction using switch/case conditions to display different status filter descriptions
### unithierarchy.xml

This XML report definition file creates a Unit Hierarchy Report for OpenPetra's Personnel module. It defines the structure and content of a report that displays organizational units in a hierarchical format. The report includes parameters for sorting and filtering, calculations for retrieving unit information (name, type, key), and a multi-level structure to display parent-child relationships between units. The report uses the GenerateUnitHierarchy function to retrieve child units based on a specified base unit code and inclusion parameters.

 **Code Landmarks**
- `Line 67`: Uses a custom function 'GenerateUnitHierarchy' to dynamically generate the unit hierarchy data structure
- `Line 82`: Implements a nested level structure with 'ChildUnitLevel' to display hierarchical relationships

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #