# XML Conference Attendee Report Subproject Of OpenPetra

The OpenPetra is a free, open-source program that helps non-profit organizations manage administration tasks through features like contact management, accounting, and sponsorship. The XML Conference Attendee Report subproject implements configurable report generation for conference management within the system. This provides these capabilities to the OpenPetra program:

- Flexible report configuration through XML definition files
- Multiple report formats tailored to different administrative needs
- Customizable sorting and filtering of attendee data
- Standardized display of participant information across various report types

## Identified Design Elements

1. **XML-Based Report Configuration**: Each report type is defined through structured XML files that specify data sources, sorting criteria, and column definitions
2. **Standardized Column Definitions**: Reports maintain consistent data presentation through predefined column sets with specified widths and calculation parameters
3. **Multiple Report Variants**: The architecture supports specialized reports including alphabetical listings, police listings, and registration date-based reports
4. **Configurable Sorting Logic**: Each report template defines specific sorting criteria appropriate to its intended use case

## Overview
The architecture emphasizes configuration over coding through XML-based report definitions. Each report template specifies its data sources, sorting criteria, and display columns with appropriate formatting parameters. The system supports various specialized report types including standard alphabetical listings (by extract or across all conferences), police listings, and registration date-based reports. This approach enables administrators to generate appropriate attendee reports for different operational needs while maintaining consistent data presentation and formatting across the system.

## Business Functions

### Conference Attendee Reports
- `standard Alphabetical Listing all conferences.xml` : Configuration file for an alphabetical attendee report listing across all conferences.
- `standard Police Listing.xml` : XML configuration file for a Police Listing report in the Conference Attendee module.
- `standard Alphabetical Listing by extract.xml` : XML configuration for a standard alphabetical conference attendee report sorted by extract.
- `standard by Registration Date.xml` : XML configuration file for an Attendee Report sorted by Registration Date in the OpenPetra Conference module.
- `standard Alphabetical Listing.xml` : XML configuration file for a standard alphabetical listing of conference attendees.

## Files
### standard Alphabetical Listing all conferences.xml

This XML configuration file defines parameters for generating an attendee report across all conferences in the OpenPetra system. It specifies report settings including data sources, sorting criteria, and column definitions. The file configures 12 display columns showing attendee information such as name, passport number, age, gender, charged office, outreach code, room assignment, fellowship group, arrival/departure dates, and conference role. It establishes sorting by partner name, nationality, and charged office, with column widths defined for proper display formatting.

 **Code Landmarks**
- `Line 2-4`: Core system configuration parameters that link this report to specific XML data sources
- `Line 10-14`: Parameters for conference and partner selection that allow filtering of report data
- `Line 15-21`: Defines a three-level sorting hierarchy with both readable labels and column references
- `Line 22-45`: Detailed column configuration with calculation parameters and precise width specifications
### standard Alphabetical Listing by extract.xml

This XML file defines parameters for a conference attendee report that displays participant information alphabetically from an extract. It configures system settings, data sources, report title, and display options. The file specifies sorting criteria (Partner Name, Nationality, Age) and defines 12 display columns including Partner Name, Passport Number, Age, Gender, Charged Office, Outreach Code, Conference Room, Fellowship Group, Group Leader, Arrival/Departure Dates, and Conference Role. Each column has assigned widths and calculation parameters to control how data is presented in the report.

 **Code Landmarks**
- `Line 3-4`: References external XML files (attendeereport.xml and conference.xml) as data sources
- `Line 12-15`: Defines three-level sorting hierarchy with readable description and column mappings
- `Line 19-40`: Defines 12 display columns with precise width specifications using different numeric formats (Integer and Decimal)
### standard Alphabetical Listing.xml

This XML file defines parameters for a conference attendee report in alphabetical order. It configures system settings, XML data sources, and report display options. The file specifies sorting criteria (Partner Name, Nationality, Age) and defines 12 columns to display attendee information including Partner Name, Passport Number, Age, Gender, Charged Office, Outreach Code, Conference Room, Fellowship Group, Group Leader, Arrival/Departure Dates, and Conference Role. Each column has an assigned width parameter to control the display format in the generated report.

 **Code Landmarks**
- `Line 3-4`: References external XML files that contain the actual data for the report
- `Line 12-14`: Configures a three-level sorting hierarchy for organizing attendee data
- `Line 16-17`: Defines readable sort parameters and column mapping for the report engine
### standard Police Listing.xml

This XML configuration file defines parameters for a Conference Attendee Police Listing report. It specifies system settings, XML data sources, report selection criteria, and display formatting. The file configures sorting options by Passport Name, Nationality, and Age, and defines seven display columns including Passport Name, Passport Number, Age, Gender, Conference Room, Arrival Date, and Departure Date. Each column has specified widths and calculation parameters to control the report's appearance and data organization.

 **Code Landmarks**
- `Line 4`: References external XML data sources that provide the actual data for the report
- `Line 11-13`: Parameters for conference and attendee selection that control report filtering
- `Line 17-19`: Defines a three-level sorting hierarchy for organizing attendee data
- `Line 24-37`: Column definitions with width specifications that control the report layout
### standard by Registration Date.xml

This XML file defines parameters for an Attendee Report in the Conference module, configured to display attendee information sorted by Registration Date. It specifies report columns including Partner Key, Partner Name, Registration Date, Age, Gender, Conference details, Room, Charged Office, Outreach Code, and Arrival/Departure dates. The file sets column widths, defines three sort criteria (Registration Date, Partner Name, Age), and configures display settings. It references external XML files (attendeereport.xml and conference.xml) and establishes default parameter values for conference and attendee selection filters.

 **Code Landmarks**
- `Line 3`: Uses eBoolean type parameter for system settings configuration
- `Line 4`: References external XML files using relative paths with double backslashes for Windows compatibility
- `Line 15-19`: Implements a three-level sorting hierarchy with corresponding readable text representation
- `Line 20-41`: Defines 11 display columns with explicit width specifications using different numeric formats (Integer and Decimal)

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #