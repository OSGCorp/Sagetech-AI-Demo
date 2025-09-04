# XML Short Termer Report Subproject Of OpenPetra

The OpenPetra system is a comprehensive open-source application that helps non-profit organizations manage administrative tasks and reduce operational costs. The XML Short Termer Report subproject implements a configurable reporting framework specifically for managing short-term personnel data within the Personnel module. This subproject uses XML configuration files to define report parameters, data sources, filtering options, and display formats, enabling flexible reporting capabilities for short-term personnel management.

## Key Technical Features

- **XML-Based Configuration System**: Defines report parameters through structured XML files for consistent report generation
- **Flexible Data Filtering**: Supports filtering by event selection, application status, dates, and other personnel attributes
- **Customizable Column Layouts**: Configurable column definitions with width specifications and sorting preferences
- **Multiple Report Types**: Specialized reports for different operational needs:
  - Applicant information (general and extended)
  - Arrival and departure tracking
  - Medical and dietary requirements
  - Payment status monitoring
  - Fellowship group organization

## Identified Design Elements

1. **Modular Report Configuration**: Each report type is defined in a separate XML file, allowing for independent maintenance and customization
2. **Consistent Parameter Structure**: All reports follow a standardized parameter format for system settings, data sources, and display options
3. **Hierarchical Data Organization**: Reports can be sorted by multiple criteria with defined priority levels
4. **Conditional Display Logic**: Parameters control visibility of empty lines and optional detail sections (addresses, passports, church information)

## Architecture Overview

The architecture follows a declarative configuration approach where report definitions are separated from the processing logic. The XML files serve as templates that the OpenPetra reporting engine interprets to generate the appropriate output. This design promotes maintainability by centralizing report definitions in the Infrastructure.Configuration.Reports layer while keeping the processing logic independent of specific report implementations.

## Business Functions

### Short Termer Reports
- `standard Event Report.xml` : XML configuration file for a Short Termer Report in the Personnel module of OpenPetra.
- `standard Departure Report.xml` : XML configuration file for a Short Termer Departure Report in OpenPetra's personnel reporting system.
- `standard Medical Report.xml` : XML configuration file defining parameters for a Short Termer Medical Report in OpenPetra's reporting system.
- `standard Applicant Extended Info.xml` : Configuration file for the Short Termer Report defining display parameters, columns, and data filters for personnel reporting.
- `standard Needs and Notes Report.xml` : XML configuration file for a Short Termer Report that defines parameters for displaying personnel needs and notes.
- `standard Arrival Report.xml` : XML configuration file for a Short Termer Arrival Report in OpenPetra's reporting system.
- `standard Applicant General Info.xml` : XML configuration file for the Applicant General Info report in OpenPetra's Personnel module.
- `standard Applications Report.xml` : XML configuration for Short Termer Report defining parameters, calculations, and display settings for personnel applications.
- `standard Payment Status Report.xml` : XML configuration for a Short Termer Payment Status Report in OpenPetra's personnel management system.
- `standard Dietary Report.xml` : Configuration file for a dietary needs report for short-term personnel in OpenPetra.
- `standard Fellowship Group Report.xml` : XML configuration file for a Short Termer Report that defines parameters for fellowship group reporting in OpenPetra.

## Files
### standard Applicant Extended Info.xml

This XML configuration file defines parameters for the 'Short Termer Report' in OpenPetra's personnel reporting system. It specifies report data sources, filtering options for application statuses (accepted, cancelled, enquiry, hold, rejected), sorting preferences, and column configurations. The file defines 13 display columns including Partner Name, Passport Number, Application Status, Event Code, dates, and other applicant information. It also contains settings for display options like hiding empty lines, address details, passport details, and church information.

 **Code Landmarks**
- `Line 2-3`: Establishes system settings and links to external XML files that contain the core report logic
- `Line 11-19`: Configurable application status filters allowing selective display of different applicant statuses
- `Line 20-22`: Multi-level sorting capability with readable descriptions and column references
- `Line 27-53`: Detailed column configuration with width specifications using different numeric formats (Integer and Decimal)
### standard Applicant General Info.xml

This XML file defines parameters for the 'Applicant General Info' report in OpenPetra's Personnel module. It configures system settings, XML data sources, report display options, and filtering criteria for short-term applicants. The file specifies sorting preferences by partner name, application status, and partner key, and defines eight report columns including Partner Key, Partner Name, Application Status, Event Code, Arrival/Departure Dates, Fellowship Group, and Accepted by Serving Field. Each column has a specified width for proper display formatting. The parameters control which application statuses are included in the report output.

 **Code Landmarks**
- `Line 3`: Uses eBoolean data type for system settings parameter
- `Line 4`: References external XML files that contain the actual report data definitions
- `Line 11-16`: Implements filtering parameters for event selection and extraction
- `Line 17-21`: Boolean flags control which application statuses are included in report output
- `Line 22-26`: Defines multi-level sorting order for report data
### standard Applications Report.xml

This XML file defines configuration parameters for the 'Short Termer Report' in OpenPetra's Personnel module. It specifies system settings, XML data sources, report display properties, and filtering options for short-term personnel applications. The file configures sorting criteria (by Event Name, Partner Key, and Application Status), column calculations, column widths, and visibility options. It includes parameters for filtering applications by status (accepted, cancelled, enquiry, hold, rejected) and data source (Event). The configuration controls how application data is presented, organized, and filtered in the report output.

 **Code Landmarks**
- `Line 3`: References external XML files that provide the actual report data structure
- `Line 6`: MaxDisplayColumns parameter limits visible columns to 7 despite defining more calculations
- `Line 11-20`: Comprehensive filtering options for application statuses with boolean flags
- `Line 21-24`: Multi-level sorting configuration with both machine and human-readable formats
- `Line 37-40`: Optional detail sections that can be toggled for address, passport and church information
### standard Arrival Report.xml

This XML configuration file defines parameters for generating a Short Termer Arrival Report in OpenPetra. It specifies system settings, XML source files, report title, and display configuration with 11 columns. The file configures data source parameters (event-based), application status filters, sorting criteria (by arrival date, time, and location), and column definitions including Partner Key, Partner Name, arrival/departure details, and transport information. Each column has defined widths and calculation parameters. Additional toggle parameters control display options like hiding empty lines, printing two lines, and showing address, passport, or church details.

 **Code Landmarks**
- `Line 3`: Uses boolean parameter 'systemsettings' to enable system-level configuration
- `Line 4`: References external XML files that contain the core report structure and functionality
- `Line 20-22`: Implements three-level sorting hierarchy for organizing short termer data
- `Line 24`: Uses column numbering system that differs from display order (column 11 used for sorting)
### standard Departure Report.xml

This XML configuration file defines parameters for a 'Standard Departure Report' for short-term personnel in OpenPetra. It specifies system settings, XML source files, report title, display options, and data filtering parameters. The file configures sorting order (by departure date, time, and partner name), column definitions with their respective widths, and calculation parameters. It defines seven visible columns including Partner Key, Partner Name, Departure Date/Time, Arrival Date/Time, and Event Code. Additional parameters control display options like hiding empty lines and detail visibility for addresses, passports, and churches.

 **Code Landmarks**
- `Line 3`: Includes systemsettings parameter that enables system-wide configuration access
- `Line 4`: References external XML files that contain the core report definition logic
- `Line 21-23`: Implements a three-level sorting hierarchy for organizing personnel records
- `Line 24-26`: Maps sort parameters to specific column numbers for data processing
- `Line 29-42`: Defines column structure with explicit width allocations for layout control
### standard Dietary Report.xml

This XML configuration file defines parameters for a 'Short Termer Report' focused on dietary needs in the Personnel module of OpenPetra. It specifies data sources, filtering options (event selection, application status), sorting preferences (by Partner Name, Partner Key, Dietary Needs), and column configurations. The report includes three columns: Partner Key, Partner Name, and Dietary Needs, with defined column widths. Additional parameters control display options like hiding empty lines and excluding address, passport, and church details from the report output.

 **Code Landmarks**
- `Line 3`: Uses systemsettings parameter to enable system-wide configuration access
- `Line 4`: References external XML files for report structure through the xmlfiles parameter
- `Line 19-21`: Implements multi-level sorting with three orderby parameters
- `Line 22-26`: Provides both human-readable and system sorting parameters
- `Line 30-35`: Defines column structure with specific width allocations for optimal display
### standard Event Report.xml

This XML file defines parameters for generating a Short Termer Report in OpenPetra's Personnel module. It configures report settings including data sources, filtering options, sorting preferences, and column layouts. The file specifies XML data sources, event selection criteria, application status filters, and column calculations. It defines display properties like column widths and sorting order (by Application Status, Accepted by Serving Field, and Partner Name). The configuration also includes parameters for hiding empty lines, printing format options, and whether to include additional details like nationalities and church information.

 **Code Landmarks**
- `Line 3-4`: References external XML files that contain the actual report definition and data structure
- `Line 9-13`: Configures multiple event selection parameters allowing filtering by event code, name, extract, conference, or unit
- `Line 14-18`: Boolean flags for filtering applications by different status types (accepted, cancelled, enquiry, etc.)
- `Line 19-23`: Defines a three-level sorting hierarchy for organizing report data
- `Line 31-49`: Defines column structure with calculations and width specifications for the report layout
### standard Fellowship Group Report.xml

This XML configuration file defines parameters for the 'Short Termer Report' in OpenPetra's Personnel module, specifically for fellowship group reporting. It specifies system settings, XML source files, report name, and display options. The file configures data source parameters (event selection, application status filters), sorting preferences (by Fellowship Group, Leader, Partner Name), and column definitions with their respective widths. Each column is mapped to specific data fields like Partner Key, Conference Role, Event Code, and date information. Additional options control display behavior such as hiding empty lines and showing address, passport, or church details.

 **Code Landmarks**
- `Line 3`: References external XML files that contain the actual report implementation logic
- `Line 19-23`: Application status parameters allow filtering short-termers by multiple status conditions simultaneously
- `Line 24-27`: Multi-level sorting configuration with both human-readable and column-reference formats
- `Line 29-46`: Column configuration uses a pattern of paired parameters - calculation type and width - for each display column
### standard Medical Report.xml

This XML configuration file defines parameters for generating a Short Termer Medical Report in OpenPetra. It specifies system settings, XML source files, report name, display options, and filtering parameters for event-based reporting. The file configures sorting options by Partner Name, Medical Info, and Partner Key, and defines three columns to display: Partner Key, Partner Name, and Medical Info with their respective column widths. Additional parameters control display options like hiding empty lines and whether to include address, passport, or church details.

 **Code Landmarks**
- `Line 3`: System settings parameter enables access to global configuration settings
- `Line 4`: References external XML files that contain the core report definition templates
- `Line 19-21`: Configures a three-level sort order hierarchy for organizing report data
- `Line 22-26`: Maps sort fields to specific column numbers for data processing
- `Line 30-35`: Defines column structure with specific width allocations for proper report formatting
### standard Needs and Notes Report.xml

This XML configuration file defines parameters for the 'Short Termer Report' in OpenPetra's reporting system. It specifies report data sources, filtering options for event selection and application status, and column configurations. The file sets up three main display columns showing Partner Key, Partner Name, and Other Needs and Notes, with defined column widths. It also configures sorting preferences, display options like hiding empty lines, and toggles for additional detail sections such as address, passport, and church information. The parameters control both the data selection and visual presentation of the short-termer personnel report.

 **Code Landmarks**
- `Line 3`: System settings parameter enables access to global configuration
- `Line 4`: References external XML files that contain the actual report structure and logic
- `Line 14-19`: Application status filtering parameters allow fine-grained control over which personnel records appear in the report
- `Line 20-25`: Multi-level sorting configuration with readable description and column references
- `Line 35-37`: Toggle parameters for optional detail sections that can be included in the report
### standard Payment Status Report.xml

This XML file defines parameters for a Short Termer Payment Status Report in OpenPetra. It configures report settings including data sources, filtering options, sorting preferences, and column definitions. The report focuses on tracking payment statuses for short-term personnel, with parameters for event selection, application status filtering, and display of financial information. Key parameters include system settings, XML file references, event selection criteria, application status filters, sort order specifications, and column definitions for partner information and payment details like booking and program fees received.

 **Code Landmarks**
- `Line 3`: References system settings with a boolean parameter that likely enables system-wide configuration
- `Line 4`: Links to external XML files that contain the core report definition templates
- `Line 22-24`: Implements a three-level sorting hierarchy for organizing report data
- `Line 29-38`: Defines column structure with explicit width specifications using different data types (Integer and Decimal)

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #