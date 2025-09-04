# JavaScript Client Ledger Reports Of Petra

The Petra is a web-based program that provides financial management capabilities for non-profit organizations. The program handles accounting data processing and generates financial reports for analysis and compliance. This sub-project implements client-side report generation functionality along with configuration-driven report parameterization. This provides these capabilities to the Petra program:

- Dynamic financial report generation with configurable parameters
- Interactive form interfaces for report filtering and customization
- Multi-format export capabilities (HTML, PDF, Excel)
- Account code and cost center selection with autocomplete functionality
- Period-based financial data analysis and presentation

## Identified Design Elements

1. **JSON Configuration-Driven Reports**: Each report type has a corresponding JSON configuration file that defines parameters, templates, and server-side calculation classes
2. **Modular JavaScript Controllers**: Report-specific JS files handle initialization, parameter collection, and report generation requests
3. **Standardized HTML Templates**: Consistent Bootstrap-based interfaces with common filtering options across different report types
4. **Client-Server Communication**: JavaScript functions extract form parameters and communicate with server-side calculation components
5. **State Management**: Global variables track report data state (e.g., last_opened_entry_data) for interactive reporting features

## Overview
The architecture follows a clear separation between configuration (JSON files), presentation (HTML templates), and behavior (JavaScript controllers). Reports like Trial Balance and Account Detail follow consistent patterns, making the system extensible for new report types. The client-side components handle form initialization, dynamic updates of period dropdowns based on selected years, and parameter preparation before sending requests to the server. The design emphasizes reusability and a consistent user experience across different financial reporting functions.

## Business Functions

### Financial Reports
- `TrialBalance.js` : Implements the Trial Balance report functionality for the Finance/GeneralLedger module in OpenPetra.
- `AccountDetail.json` : Configuration file for Account Detail financial report in OpenPetra's General Ledger module.
- `AccountDetail.js` : JavaScript client for the Account Detail financial report in OpenPetra's General Ledger module.
- `TrialBalance.json` : Configuration file for Trial Balance report in OpenPetra's finance module.
- `TrialBalance.html` : HTML template for the Trial Balance report in OpenPetra's financial module with filtering options for year, period, and accounts.
- `AccountDetail.html` : Account Detail report form for OpenPetra's finance module with filtering options for year, period, and account codes.

## Files
### AccountDetail.html

This HTML file implements the Account Detail report interface for OpenPetra's finance module. It provides a form with filtering options including fiscal year selection, start/end period dropdowns, and account code range inputs with autocomplete functionality. The form includes buttons to calculate the report and download results in Excel or PDF formats. The interface uses Bootstrap for layout and includes several JavaScript libraries for report generation, autocomplete functionality, and finance-related operations. The report results are displayed in a container below the filter options.

 **Code Landmarks**
- `Line 58`: Uses autocomplete functionality with key-name attribute to separate display value from actual parameter value
- `Line 85`: Includes multiple specialized JavaScript libraries for reports, finance operations, and autocomplete functionality
- `Line 21`: Uses phantom storage technique to define reusable HTML templates
### AccountDetail.js

This file implements client-side functionality for the Account Detail report in OpenPetra's finance module. It handles initialization of the report form by loading available years and periods, and provides functions to update period dropdowns based on selected years. The main function calculate_report() extracts form parameters, adds ledger information, adjusts period indices, and calls a common report calculation function. The file maintains a global variable last_opened_entry_data to track report entries.

 **Code Landmarks**
- `Line 27`: Uses document.ready to initialize the form with available years and periods
- `Line 45`: Subtracts 1 from period parameters to convert from UI representation to backend indexing
- `Line 47`: Uses '*NOTUSED*' as a placeholder value for unused parameters
### AccountDetail.json

This JSON configuration file defines parameters for the Account Detail report in OpenPetra's Finance module. It specifies the HTML template to use, server-side calculation class, report title, and numerous report parameters including account code ranges, cost center codes, currency settings, financial period selections, and sorting preferences. The file contains default values for filtering options such as date ranges, period breakdowns, and analysis attributes. These parameters control how financial transaction details are filtered, calculated, and displayed when the report is generated.

 **Code Landmarks**
- `Line 2`: Links to server-side calculation class that processes the report data
- `Line 5-6`: Default account code parameters that determine which accounts are included in the report
- `Line 10-13`: Cost centre filtering parameters that control organizational unit reporting
- `Line 15-17`: Currency configuration parameters for financial reporting
- `Line 35`: Radio button group parameter for account selection method
### TrialBalance.html

This HTML file defines the Trial Balance report interface in OpenPetra's Finance/GeneralLedger module. It provides a form for filtering report data by year, start/end periods, and account codes with autocomplete functionality. The interface includes buttons for generating the report, downloading as Excel, or as PDF. The file uses phantom storage for templates, includes several JavaScript libraries for functionality, and organizes the layout using Bootstrap's grid system. The form allows users to select accounting periods and account code ranges before generating the financial report.

 **Code Landmarks**
- `Line 8`: Uses phantom storage pattern for HTML templates that can be cloned via JavaScript
- `Line 60`: Implements autocomplete functionality for account codes with separate input and key fields
- `Line 90`: Includes multiple specialized JavaScript libraries for finance, reports, and autocomplete functionality
### TrialBalance.js

This JavaScript file implements the Trial Balance report functionality for the Finance/GeneralLedger module. It initializes the report form by loading available years and periods, and handles the report calculation process. The file contains functions to update period dropdowns based on selected year and to prepare parameters for the report calculation. Key functions include calculate_report() which extracts form data, sets default parameters, and calls the common report calculation function. The file uses jQuery for DOM manipulation and relies on external functions for retrieving accounting periods and report generation.

 **Code Landmarks**
- `Line 27`: Uses a global variable to store last opened entry data across function calls
- `Line 39`: Updates both start and end period dropdowns when year selection changes
- `Line 51`: Adjusts period parameters by subtracting 1, suggesting zero-based indexing in the backend
### TrialBalance.json

This JSON configuration file defines parameters for the Trial Balance report in OpenPetra's finance module. It specifies the HTML template file, server-side calculation class, and various report parameters including account codes, cost center ranges, currency settings, financial period selections, and sorting preferences. The file contains default values for report generation such as account code range (0100-0100), cost center range (10100-10500), base currency (EUR), and period settings. These parameters control how financial data is filtered, processed, and displayed when generating trial balance reports.

 **Code Landmarks**
- `Line 2`: Links to server-side calculation class that processes the report data
- `Line 3`: Default report type set to 'Account Detail' which determines the report format
- `Line 16`: Currency parameter defaults to base currency (EUR) for financial calculations
- `Line 36`: Report organization parameters control grouping by Account or Cost Centre

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #