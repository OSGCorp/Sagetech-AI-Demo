# JavaScript Client Partner Report Templates Of Petra

The Petra is a web-based program that helps non-profit organizations manage administrative tasks and reduce operational overhead. The program provides contact management and reporting capabilities while maintaining a clean separation between client and server components. This sub-project implements client-side report templates and associated JavaScript functionality for partner-related reports, along with configuration files that connect the front-end interfaces to server-side processing logic. This provides these capabilities to the Petra program:

- Dynamic report generation with customizable filters
- Multiple export formats (HTML, Excel, PDF)
- Consent-based filtering for privacy compliance
- Partner categorization by various attributes (city, subscription, special type)

## Identified Design Elements

1. **Configuration-Driven Architecture**: JSON configuration files connect HTML templates with server-side processing classes
2. **Modular Report Components**: Each report consists of HTML template, JavaScript controller, and JSON configuration
3. **Dynamic Form Generation**: JavaScript controllers dynamically populate form elements based on server data
4. **Standardized Report Processing**: Common patterns for parameter collection and report submission
5. **Consent Management Integration**: Built-in support for filtering based on communication consent preferences

## Overview
The architecture follows a consistent pattern where JSON configuration files define the relationship between client templates and server processing classes. HTML templates provide the user interface structure, while JavaScript controllers handle dynamic content loading, form validation, and report generation requests. The design emphasizes reusability through common utility functions and standardized approaches to report parameter collection. Reports cover various partner categorizations including special types, city, subscription status, and annual reporting requirements.

## Business Functions

### Partner Reports Configuration
- `PartnerBySpecialType.json` : Configuration file for Partner by Special Types report in OpenPetra's partner management module.
- `PartnerByCity.json` : Configuration file for Partner by City report in OpenPetra's partner management module.
- `PartnerBySubscription.json` : Configuration file for Partner by Subscription report in OpenPetra's partner management module.
- `AnnualReportWithoutAnnualReceiptRecipients.json` : Configuration file for the Annual Report without Annual Receipt Recipients report in OpenPetra.

### Partner Reports Templates
- `AnnualReportWithoutAnnualReceiptRecipients.html` : HTML form for generating annual reports for partners who did not receive annual receipts.
- `PartnerBySubscription.html` : Partner subscription report interface allowing users to filter by publication code and consent permissions.
- `PartnerByCity.html` : HTML template for Partner by City report with filtering options and download capabilities
- `PartnerBySpecialType.html` : HTML template for generating partner reports filtered by special type with consent options.

### Partner Reports Implementation
- `PartnerBySpecialType.js` : Partner report component for filtering and generating reports by special partner types
- `PartnerBySubscription.js` : Handles partner subscription report generation with publication selection and consent filtering.
- `PartnerByCity.js` : A JavaScript module for generating partner reports filtered by city in OpenPetra.
- `AnnualReportWithoutAnnualReceiptRecipients.js` : Implements a report generator for annual reports without annual receipt recipients in the OpenPetra partner management system.

## Files
### AnnualReportWithoutAnnualReceiptRecipients.html

This HTML file defines a form interface for generating annual reports for partners without annual receipts. It provides filters for publication code selection, consent permissions, and donation date ranges. The form includes buttons for calculating the report and downloading results in Excel or PDF formats. The interface uses a container structure with rows and columns for organizing form elements, and includes a hidden template for consent options. The file references external JavaScript libraries for utilities, templating, and report generation functionality.

 **Code Landmarks**
- `Line 10`: Uses a dynamic select element for publication codes that gets populated via JavaScript
- `Line 18`: Implements a consent permissions section with a custom 'consents' attribute for dynamic generation
- `Line 48`: Contains a hidden phantom div with a template for consent options that gets cloned via JavaScript
### AnnualReportWithoutAnnualReceiptRecipients.js

AnnualReportWithoutAnnualReceiptRecipients.js implements functionality for generating annual reports that exclude annual receipt recipients. The file initializes a report form with default date values (previous year), loads available publications from the server, and handles report generation based on user-selected parameters. It also includes functionality to load consent options dynamically from the server. Key functions include get_publications(), calculate_report(), and loadInConsents(). The file uses jQuery for DOM manipulation and makes API calls to OpenPetra server endpoints to fetch data.

 **Code Landmarks**
- `Line 26`: Maintains state of last opened entry data in a global variable
- `Line 37`: Automatically sets default date range to previous calendar year when initializing the form
- `Line 54`: Dynamically loads consent options from server and builds UI elements based on returned data
### AnnualReportWithoutAnnualReceiptRecipients.json

This JSON configuration file defines parameters for the 'Annual Report without Annual Receipt Recipients' report in OpenPetra's Partner module. It specifies the HTML template file location ('Partner/AnnualReportWithoutAnnualReceiptRecipients.html'), the server-side calculation class ('Ict.Petra.Server.MReporting.MPartner.AnnualReportWithoutAnnualReceiptRecipients'), and the report's display name. These settings connect the front-end report interface with the back-end processing logic.
### PartnerByCity.html

This HTML template implements the Partner by City report interface for OpenPetra. It provides a form with filtering options including city name, active partners only, valid addresses, exclusion of no-solicitation partners, and consent permissions. The template includes buttons for generating the report and downloading results in Excel or PDF formats. The file uses phantom elements as templates for dynamic content generation and includes references to utility JavaScript files for report functionality.

 **Code Landmarks**
- `Line 3`: Uses phantom storage pattern for HTML templates that will be cloned via JavaScript
- `Line 74`: Hidden phantom element for consent options that will be dynamically populated
- `Line 79`: References three JavaScript libraries for utilities, templating, and report generation
### PartnerByCity.js

This JavaScript file implements functionality for the Partner By City report in OpenPetra. It provides functions to gather filter parameters and generate reports based on user selections. The calculate_report function extracts data from form fields, collects selected partner tags, and passes parameters to a common report calculation function. The loadInConsents function fetches consent channel and purpose data from the server and dynamically populates consent options in the report filter interface. Key functions include calculate_report() and loadInConsents(). The file also initializes consent loading when the document is ready.

 **Code Landmarks**
- `Line 26`: Global variable tracks last opened entry data across function calls
- `Line 33`: Dynamic collection of selected partner tags from UI checkboxes
- `Line 45`: API integration with server-side web connector for fetching consent data
- `Line 47`: Dynamic UI generation for consent options with internationalization support
### PartnerByCity.json

This JSON configuration file defines settings for the Partner by City report in OpenPetra's partner management system. It specifies three key parameters: the HTML template file path ('Partner/partnerbycity.html'), the server-side calculation class ('Ict.Petra.Server.MReporting.MPartner.PartnerByCity'), and the report's display name ('Partner by City'). These settings connect the client-side report interface with the server-side processing logic.

 **Code Landmarks**
- `Line 2`: References server-side calculation class that handles the actual report generation logic
### PartnerBySpecialType.html

This HTML file defines the interface for the Partner By Special Type report in OpenPetra. It provides a form with filtering options including partner types selection, checkboxes for active partners only, valid addresses, and excluding no-solicitation partners. The template includes consent permission options and buttons to generate the report and download it in Excel or PDF formats. The file uses phantom templates for checkbox and consent option rendering, and includes JavaScript libraries for utilities, templating, and report generation functionality.

 **Code Landmarks**
- `Line 13`: Uses phantom storage pattern to define reusable HTML templates that are hidden from view
- `Line 37`: Dynamic partner type selection area with scrollable container for potentially long lists
- `Line 74`: Template for consent options that will be dynamically populated with radio buttons
### PartnerBySpecialType.js

PartnerBySpecialType.js implements a client-side form for generating partner reports filtered by special types. It loads partner types from the server and displays them as checkboxes for selection. The component includes functionality to load consent options, extract form data, and submit report parameters to a common report calculation function. Key functions include display_report_form(), calculate_report(), loadInConsents(), and load_tags(). The file uses jQuery for DOM manipulation and makes API calls to OpenPetra's server endpoints to retrieve partner types and consent information.

 **Code Landmarks**
- `Line 27`: Uses a global variable to store last opened entry data across component lifecycle
- `Line 48`: Dynamic checkbox generation for partner types based on server data
- `Line 62`: Collects selected partner types by iterating through DOM elements rather than using form serialization
### PartnerBySpecialType.json

This JSON configuration file defines settings for the Partner by Special Types report in OpenPetra. It specifies the HTML template file path ('Partner/partnerbyspecialtype.html'), the server-side calculation class ('Ict.Petra.Server.MReporting.MPartner.PartnerBySpecialTypes'), and the report's display name ('Partner by Special Types'). These parameters connect the client-side report interface with server-side processing logic.
### PartnerBySubscription.html

This HTML file defines a report interface for listing partners by subscription. It provides a filter form with publication code selection and consent permission options. The interface includes buttons for generating the report, downloading Excel, and downloading PDF formats. The file uses JavaScript libraries for utilities, templates, and report generation. Key elements include the report filter container, publication code dropdown, consent permissions section, and a hidden template for consent options.

 **Code Landmarks**
- `Line 26`: Download buttons are initially hidden and only displayed after report calculation
- `Line 37`: Uses a hidden 'phantom' element as a template for dynamically generating consent option controls
- `Line 42`: Imports three separate JavaScript libraries for utilities, templates and report functionality
### PartnerBySubscription.js

PartnerBySubscription.js implements functionality for generating partner reports based on subscription data. It provides a user interface for selecting publications and consent options as report filters. The file loads available publications from the server and populates a dropdown menu for user selection. It also loads consent channels and purposes to create filtering options for the report. Key functions include get_publications() to fetch and display publication options, calculate_report() to gather filter parameters and generate the report, and loadInConsents() to populate consent filtering options. The file uses jQuery for DOM manipulation and API calls to fetch data.

 **Code Landmarks**
- `Line 26`: Uses a global variable last_opened_entry_data to store state between function calls
- `Line 45`: Calls external calculate_report_common function with JSON configuration file path
- `Line 50`: Dynamically builds UI elements for consent options based on server data
### PartnerBySubscription.json

This JSON configuration file defines settings for the Partner by Subscription report in OpenPetra. It specifies the HTML template file path ('Partner/PartnerBySubscription.html'), the server-side calculation class ('Ict.Petra.Server.MReporting.MPartner.PartnerBySubscription'), and the report's display name ('Partner by Subscription'). These parameters connect the client-side report interface with the server-side processing logic.

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #