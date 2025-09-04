# XML Conference Group Report Subproject Of OpenPetra

The XML Conference Group Report subproject provides configurable reporting templates for OpenPetra's conference management functionality. This component implements XML-based configuration files that define standardized reports for different conference grouping structures (Fellowship Groups, Discovery Groups, and Work Groups). The architecture follows a declarative approach where report parameters, data sources, and visual presentation are defined in structured XML templates. This provides these capabilities to the OpenPetra program:

- Configurable report templates with consistent structure
- Flexible attendee filtering and conference selection parameters
- Standardized column definitions for participant information display
- Customizable visual presentation with defined column widths

## Identified Design Elements

1. **Standardized Report Configuration**: All templates follow a consistent XML structure defining system settings, data sources, and display options
2. **Modular Group Reporting**: Separate templates for different group types (Fellowship, Discovery, Work) with shared structural elements
3. **Flexible Data Selection**: Configurable parameters for filtering conference participants based on roles and attributes
4. **Consistent Data Presentation**: Standardized column definitions across reports (personal details, demographics, group affiliations)

## Overview
The architecture emphasizes configuration over code, allowing report customization without programming. The XML templates serve as declarative definitions that control both data selection and visual presentation. Each report template maintains consistent column structures while allowing specialization for different group contexts. The design supports OpenPetra's non-profit focus by providing efficient ways to organize and report on conference participants across various organizational groupings, enhancing administrative capabilities for event management.

## Business Functions

### Conference Reporting
- `standard Work Group.xml` : XML configuration file for a standard Work Group report in the Conference module of OpenPetra.
- `standard Group Report.xml` : XML configuration file for the standard Group Report in OpenPetra's Conference module.
- `standard Discovery Group.xml` : XML configuration file defining parameters for a Discovery Group report in the Conference module.
- `standard Fellowship Group.xml` : XML configuration file for a standard Fellowship Group report in the Conference module of OpenPetra.

## Files
### standard Discovery Group.xml

This XML file defines parameters for a standard Discovery Group report within the Conference module of OpenPetra. It configures system settings, XML data sources, and report display options. The file specifies conference and attendee selection parameters along with column configurations for displaying participant information. The report is designed to show attendee details including names, nationality, age, gender, family key, and group assignments across ten columns with defined widths. The parameters control both data selection criteria and visual presentation of the report.

 **Code Landmarks**
- `Line 3-4`: Defines system settings and XML data sources that provide the underlying data structure for the report
- `Line 11-13`: Parameters for conference selection that determine which conference data will be displayed in the report
- `Line 16-17`: Group filtering parameters that specifically target Discovery Groups and participant roles
- `Line 18-37`: Column configuration with ten distinct data fields and their respective widths for report display
### standard Fellowship Group.xml

This XML configuration file defines parameters for the 'standard Fellowship Group' report in OpenPetra's Conference module. It specifies system settings, XML data sources, and report display properties. The file configures ten columns for the report including First Name, Preferred Name, Last Name, Nationality, Age, Gender, Family Key, Fellowship Group, Discovery Group, and Work Group. Each column has a defined width parameter. Additional parameters control conference selection, attendee filtering, and participant role options. The file serves as a template that determines how fellowship group data is presented in the reporting system.

 **Code Landmarks**
- `Line 4`: References external XML files that provide the actual data for the report
- `Line 7`: MaxDisplayColumns parameter limits the report to exactly 10 columns
- `Line 11`: Uses integer keys (param_partnerkey, param_conferencekey) for database record identification
- `Line 15`: Defines participant filtering with 'ListOnlyAttendeesWhoseRoleIsAParticipant' option
### standard Group Report.xml

This XML file defines parameters for the standard Group Report in OpenPetra's Conference module. It configures system settings, XML data sources, report display options, and column specifications. The file sets up a report that displays attendee information across multiple columns including personal details (names, nationality, age, gender), identification (family key), and group affiliations (fellowship, discovery, and work groups). Parameters control conference selection, attendee filtering, and column widths for optimal display. The configuration supports customizable reporting of conference group data with up to 10 display columns.

 **Code Landmarks**
- `Line 3-4`: References external XML files that contain the actual data for the report
- `Line 6`: Sets maximum display columns to 10, limiting the report width
- `Line 11-13`: Parameters for dynamic conference selection that allow filtering report data
- `Line 15-16`: Group and participant selection parameters that control which data is included
- `Line 17-36`: Column configuration with width specifications using both integer and decimal values
### standard Work Group.xml

This XML file defines parameters for a Conference Work Group report in OpenPetra. It configures system settings, XML data sources, and report display options. The file specifies report selection criteria including conference and attendee filters, and defines ten display columns with their respective widths. Column definitions include personal information fields (First Name, Last Name, Preferred Name), demographic data (Nationality, Age, Gender), and organizational groupings (Family Key, Fellowship Group, Discovery Group, Work Group). The parameters control both the data selection and visual presentation of the report.

 **Code Landmarks**
- `Line 3`: System settings parameter enables access to global configuration
- `Line 4`: References external XML files for data sources using relative paths
- `Line 11-13`: Conference selection parameters allow filtering by partner key, conference key and name
- `Line 15-16`: Group selection parameters control which types of groups and participants are included
- `Line 17-36`: Column configuration uses a pattern of param_calculation and ColumnWidth pairs for each display column

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #