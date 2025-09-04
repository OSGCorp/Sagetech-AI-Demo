# XML Donor Gifts To Fields Report Subproject Of Petra

The Petra application is a financial management system that helps non-profit organizations track donations and manage financial reporting. The XML Donor Gifts To Fields Report subproject implements a configurable reporting framework for generating detailed and summary reports of donor gifts allocated to specific fields. This subproject provides these capabilities to the Petra program:

- Configurable report generation with multiple output formats
- Flexible donor gift tracking with field-specific allocation reporting
- Tax claim documentation generation for donor contributions
- Customizable display columns with formatting options

## Identified Design Elements

1. **XML-Based Configuration Architecture**: Uses structured XML files to define report parameters, display columns, and filtering options without requiring code changes
2. **Multiple Report Variants**: Supports complete, summary, and tax claim report types through separate configuration files
3. **Parameterized Filtering System**: Implements donor selection, amount range filtering, and field selection parameters
4. **Flexible Column Configuration**: Allows customization of display columns, widths, and formatting for different reporting needs

## Overview

The architecture follows a configuration-driven approach where report definitions are externalized in XML files. Each report variant (Complete, Summary, Tax Claim) extends a standard base configuration with specialized display columns and calculations. The system supports filtering by donor information, gift amount ranges, and field selection criteria. The reporting engine processes these configurations to extract relevant financial data and format it according to the specified parameters. This design enables non-profit organizations to generate specialized reports for different purposes (operational tracking, donor communications, tax documentation) while maintaining consistency in the underlying data model.

## Business Functions

### Financial Reports Configuration
- `standard Complete.xml` : XML configuration file for the Donor Gifts to Fields report in OpenPetra's finance module.
- `standard.xml` : XML configuration file for donor gifts to fields report settings in OpenPetra's finance module.
- `standard Summary.xml` : Configuration file for Donor Gifts to Fields summary report in OpenPetra's finance module.
- `standard Tax Claim.xml` : XML configuration file for a Tax Claim report showing donor gifts to fields with customizable columns and parameters.

## Files
### standard Complete.xml

This XML configuration file defines parameters for the 'Donor Gifts to Fields' report in OpenPetra's finance module. It specifies report settings including system parameters, XML dependencies, report type, and display configuration. The file configures seven display columns showing recipient name, field, gift date, receipt, gift type, method, and gift amount. It also includes parameters for filtering by donor, amount range, currency format, and field selection. The configuration supports both complete and year-to-date reporting options for each column, with customizable column widths.

 **Code Landmarks**
- `Line 3-5`: References external XML files that provide additional report structure and functionality
- `Line 9`: MaxDisplayColumns parameter limits the report to 7 columns
- `Line 10`: Report type is set to 'Complete' which affects data presentation
- `Line 40`: Currency format parameter controls how monetary values are displayed
- `Line 43`: Gift amount column is specifically identified for calculations
### standard Summary.xml

This XML configuration file defines parameters for the Donor Gifts to Fields summary report in OpenPetra's finance module. It specifies report settings including system parameters, XML data sources, display constraints, and column configurations. The file sets up four display columns: Donor Key, Donor Name, Donor Address, and Gift Amount, each with specific width settings. It also configures filtering options for donors, gift amounts, currency format, and field selection criteria. These parameters control how donor gift data is summarized and presented in the financial reporting system.

 **Code Landmarks**
- `Line 3`: Loads multiple XML files for report generation, showing modular data structure approach
- `Line 9`: MaxDisplayColumns parameter limits the report to 4 columns regardless of available data
- `Line 23`: Uses scientific notation (eDecimal:4615063718147915776) for column width specification
### standard Tax Claim.xml

This XML file defines parameters for a Tax Claim report in the Finance module of OpenPetra. It configures a donor gifts to fields report with settings for report type, donor selection, amount range, and currency. The file specifies six display columns with their calculations (Recipient Name, Field, Gift Date, Gift Type, Method, and Gift Amount) along with column widths and formatting options. Additional parameters control field selection, currency format, and other report behaviors needed to generate tax claim documentation for donor gifts.

 **Code Landmarks**
- `Line 3`: References system settings with a boolean parameter that likely enables system-wide configuration access
- `Line 4`: Links to three XML files that provide the report structure and shared components
- `Line 9-11`: Defines financial parameters for filtering gifts by amount range and currency
- `Line 12-29`: Implements a column-based configuration system with calculation types and widths for each display column
### standard.xml

This XML file defines default parameters for the Donor Gifts to Fields report in OpenPetra's finance module. It specifies system settings, XML file dependencies, report type, donor selection criteria, amount range parameters, currency settings, field selection options, and extraction parameters. The file establishes initial values for report generation, including minimum and maximum gift amounts, currency base, and donor key values.

 **Code Landmarks**
- `Line 4`: References multiple XML files that must be loaded for the report to function properly
- `Line 6`: Default report type is set to 'Complete' which affects what data is included in the report
- `Line 8-9`: Defines default minimum and maximum amount parameters that control which donations are included in the report

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #