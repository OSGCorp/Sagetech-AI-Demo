# JavaScript Client Library Of Petra

The Petra JavaScript Client Library implements the frontend functionality for OpenPetra, providing a comprehensive web interface for nonprofit organization management. This client-side framework handles user interactions, data presentation, and server communication through a modular architecture that emphasizes reusability and maintainability. The library serves as the bridge between users and OpenPetra's backend services, enabling access to the system's administrative and financial capabilities.

## Key Technical Features

- **Authentication System**: Manages user sessions, login/logout operations, and self-service account management
- **Dynamic Navigation Framework**: Handles page routing, menu generation, and form loading based on user permissions
- **Internationalization Support**: Provides multilingual capabilities with automatic language detection
- **AJAX Communication Layer**: Configures standardized API communication with error handling
- **UI Component Library**: Implements reusable interface elements including modals, autocomplete fields, and templates
- **Financial Data Handling**: Specialized components for accounting periods, motivation codes, and financial reporting
- **Reporting Engine**: Client-side functionality for generating and displaying reports in multiple formats

## Identified Design Elements

1. **Template-Based UI Rendering**: The tpl.js module provides data binding between JavaScript objects and HTML templates with security validation
2. **Modular Autocomplete System**: Specialized autocomplete implementations for different entity types (partners, financial codes) built on a common foundation
3. **Session Management**: Persistent authentication state using localStorage with automatic session validation
4. **Progressive Enhancement**: Browser compatibility detection prevents unsupported browsers from accessing the application
5. **Internationalized Interface**: Complete translation system with dynamic content updating and language switching

## Architecture Overview
The library follows a layered architecture with clear separation between presentation components, integration services, and cross-cutting utilities. The design emphasizes component reusability, consistent styling through Bootstrap integration, and robust error handling. The modular structure allows for independent development of features while maintaining a cohesive user experience.

## Business Functions

### Authentication and Security
- `auth.js` : Authentication module for OpenPetra's JavaScript client that handles user login, logout, session management, and self-service account functions.
- `app.js` : Client-side application initialization script for OpenPetra that handles authentication, session management, and navigation setup.

### UI Components
- `autocomplete_posting_acc_cc.js` : Provides autocomplete functionality for account codes and cost center codes in the OpenPetra financial system.
- `modal.js` : JavaScript module for managing modal dialogs in the OpenPetra web interface
- `tpl.js` : Client-side template manipulation library for OpenPetra that handles data binding, formatting, and DOM manipulation.

### Navigation
- `navigation.js` : Client-side navigation system for OpenPetra that manages page routing, menu generation, and dashboard display.

### Data Communication
- `ajax.js` : Client-side AJAX configuration for OpenPetra's API communication using Axios.

### Autocomplete Features
- `autocomplete.js` : Implements autocomplete functionality for input fields in the OpenPetra web client interface.
- `autocomplete_motivation.js` : Provides autocomplete functionality for motivation groups and details in OpenPetra's finance module.
- `autocomplete_partner.js` : Provides autocomplete functionality for partner search in the OpenPetra client interface.

### Financial Services
- `finance.js` : JavaScript module providing financial period and year selection functionality for OpenPetra's accounting system.

### Reporting
- `reports.js` : Client-side JavaScript module for generating, downloading, and displaying reports in OpenPetra.

### Internationalization
- `i18n.js` : Internationalization module for OpenPetra's JavaScript client that handles translation of UI elements.

### Utilities
- `string.js` : Utility function for string manipulation in OpenPetra's JavaScript client
- `utils.js` : Utility functions for the OpenPetra JS client providing message display, error handling, and data manipulation capabilities.

### Browser Compatibility
- `checkIE.js` : Utility script that detects Internet Explorer browsers and redirects them to a dedicated page.

## Files
### ajax.js

This file configures Axios for handling AJAX requests in the OpenPetra JavaScript client. It creates two Axios instances: 'api' for server API communication and 'src' for report parameters. The main instance sets JSON content type headers, includes response interceptors for error handling, and implements custom error display functionality. The interceptor parses error responses from different formats and displays appropriate error messages to users. The file ensures proper communication between the JavaScript client and the OpenPetra backend services.

 **Code Landmarks**
- `Line 31`: Creates a configured Axios instance with baseURL and response type settings
- `Line 36`: Implements response interceptor to handle error responses with custom parsing logic
- `Line 52`: Creates a secondary Axios instance specifically for accessing report parameter files
### app.js

This file implements the core client-side initialization for the OpenPetra web application. It handles user authentication flows including login, logout, password reset, and self-registration. Key functionality includes maintaining server connections through polling, loading navigation components after authentication, and managing various authentication states. Important functions include keepConnection() for session maintenance, loadNavigation() for UI setup, resetPassword() and selfSignUp() for handling special authentication flows, and auth.checkAuth() which serves as the main entry point determining application state based on authentication status.

 **Code Landmarks**
- `Line 28`: Implements a polling mechanism to maintain server connection by calling the server every 30 seconds
- `Line 40`: Uses i18next translation system with deferred loading that checks if translations are ready before proceeding
- `Line 105`: Implements URL parameter parsing to handle different authentication flows like password reset and self-signup
- `Line 125`: Uses a callback-based authentication check that determines the application's initial state and UI display
### auth.js

Auth class implements authentication functionality for the OpenPetra JavaScript client. It manages user sessions through login/logout operations, session validation, and self-service account management. Key functionality includes authenticating users against the server, maintaining session state in local storage, checking session validity, and supporting self-service features like signup and password reset. The class communicates with server endpoints through API calls and handles responses appropriately. Important methods include login(), logout(), checkAuth(), signUp(), requestNewPassword(), and setNewPassword(). The implementation uses window.localStorage to persist authentication state between page loads.

 **Code Landmarks**
- `Line 84`: Session timeout check occurs every 5 minutes to maintain security while minimizing server requests
- `Line 92`: Session expiration handling forces user logout and page reload when server indicates session is no longer valid
- `Line 40`: Authentication success stores multiple pieces of user context including module permissions and password change requirements
### autocomplete.js

This JavaScript file implements autocomplete functionality for input fields in the OpenPetra web client. It provides two main functions: autocomplete() and autocompleteWithGroup(), which create dropdown suggestions as users type. The code handles keyboard navigation (up/down arrows), selection (Enter key), and click events on suggestions. Each suggestion can store additional metadata like keys, group keys, and details. The file also includes a utility function delete_all_guesses() to remove all autocomplete suggestion elements from the DOM.

 **Code Landmarks**
- `Line 27`: The autocomplete function is a wrapper around autocompleteWithGroup with null group object
- `Line 32`: Uses attribute 'init-autocomplete' to prevent multiple initializations of the same field
- `Line 40-67`: Implements keyboard navigation with arrow keys and Enter key selection
- `Line 84`: Stores complex object data as JSON string in 'details' attribute for later retrieval
### autocomplete_motivation.js

This file implements autocomplete functionality for motivation-related fields in OpenPetra's finance module. It contains two main functions: autocomplete_motivation_group and autocomplete_motivation_detail, which fetch data from the server based on user input and display matching options. Both functions make API calls to retrieve motivation data from the server, parse the JSON response, and format the results for display in autocomplete dropdown lists. The autocomplete_motivation_detail function additionally supports linking motivation details with their corresponding groups through the onselect parameter.

 **Code Landmarks**
- `Line 29`: Uses localStorage to retrieve the current ledger number for API requests
- `Line 45`: Custom formatting of autocomplete results with HTML to display code and description
- `Line 67`: Stores additional metadata (membership, sponsorship, workersupport) in autocomplete items for potential use in selection handling
### autocomplete_partner.js

This file implements autocomplete functionality for partner searches in the OpenPetra client interface. It contains three main functions: autocomplete_contact, autocomplete_donor, and autocomplete_member. The autocomplete_contact function handles API requests to the server for partner information based on user input, with a 500ms debounce timeout. It formats the returned partner data for display in the autocomplete dropdown, including partner name, key, address, and status. The other two functions are specialized wrappers around autocomplete_contact with specific parameters.

 **Code Landmarks**
- `Line 23`: Uses a timeout variable for debouncing autocomplete requests to prevent excessive API calls
- `Line 31`: Implements a 500ms delay before sending search requests to reduce server load during typing
- `Line 47`: Formats partner display with HTML markup to show status, name, ID and address information in the dropdown
### autocomplete_posting_acc_cc.js

This file implements autocomplete functionality for financial data entry in OpenPetra. It contains two main functions: autocomplete_a for account code lookup and autocomplete_cc for cost center code lookup. Both functions make API calls to the server with search parameters and format the returned data for display in autocomplete dropdowns. The functions handle parameters like ledger number, search text, and various filtering options. They transform server responses into formatted lists with key, label, and HTML display properties that are passed to a generic autocomplete function.

 **Code Landmarks**
- `Line 27`: Uses browser localStorage to retrieve the current ledger number for API requests
- `Line 31`: Configures specific search parameters for account lookup including posting-only and limit options
- `Line 42`: Creates rich HTML display format for autocomplete results with bold codes and descriptions
### checkIE.js

This file implements a browser detection utility that prevents Internet Explorer users from accessing the OpenPetra application. It contains a failForInternetExplorer function that checks if the user's browser is Internet Explorer by examining the navigator.appName and userAgent string. When the document is ready, it calls this function and redirects IE users to a dedicated page (/page_for_ie.html) that likely explains the incompatibility.

 **Code Landmarks**
- `Line 27`: Uses two detection methods to identify Internet Explorer - checking navigator.appName and using regex pattern matching
- `Line 35`: Special case handling for Internet Explorer 11 which has a different user agent string pattern
### finance.js

This finance.js module implements utility functions for retrieving and displaying available financial years and periods in OpenPetra's accounting system. It provides two key functions: get_available_years fetches and populates dropdown selectors with available accounting years, and get_available_periods retrieves and displays accounting periods for a selected year. Both functions make API calls to OpenPetra's server endpoints and handle the population of HTML select elements with the returned data. The functions support optional callbacks and can handle both single and multiple period selection scenarios.

 **Code Landmarks**
- `Line 27`: Uses browser localStorage to retrieve the current ledger number for API calls
- `Line 46`: Supports both single and multiple period selection through the OfferMultiplePeriods parameter
- `Line 66`: Implements internationalization (i18next) for period names translation
### i18n.js

This file implements internationalization functionality for the OpenPetra JavaScript client. It initializes the i18next library to handle translations with fallback to English, detects browser language, and loads translation files. Key functions include translateElement() which translates individual DOM elements by their attributes, updateContent() which applies translations to specific page sections, translate() for processing text with translation markers, and language management functions changeLng() and currentLng(). The file also handles development vs. release modes for resource loading and updates UI language indicators.

 **Code Landmarks**
- `Line 30`: Dynamic resource versioning that changes behavior between development and release builds
- `Line 47`: Translation initialization with namespaces and automatic language detection
- `Line 55`: Custom translation function that handles placeholder, HTML content, href and title attributes
- `Line 91`: Translation function that processes text with {key} markers and supports form-specific translations
- `Line 113`: Language change event handler that updates UI and displays appropriate country flag emoji
### modal.js

This file provides utility functions for managing Bootstrap modal dialogs in the OpenPetra web interface. It handles modal stacking by dynamically adjusting z-index values to ensure proper layering when multiple modals are open. The module implements functions for creating, finding, and closing modal dialogs. Key functions include ShowModal() for displaying a new modal with specific HTML content, FindModal() and FindMyModal() for locating modal elements, and CloseModal() for hiding and optionally removing modals from the DOM. Event handlers ensure proper modal behavior and cleanup.

 **Code Landmarks**
- `Line 24`: Implements z-index management for stacked modals to prevent visual overlap issues
- `Line 33`: Ensures proper cleanup of Bootstrap modal data to prevent memory leaks
- `Line 42`: ShowModal function configures modals with static backdrop to prevent accidental dismissal
### navigation.js

Navigation.js implements the client-side navigation system for OpenPetra, handling page routing, menu generation, and form loading. It manages the application's navigation structure by processing URL paths, loading appropriate forms, maintaining browser history, and rendering both the top navigation bar and side menu. Key functionality includes opening forms, loading JavaScript files, managing navigation state, building dashboards, and handling user permissions for ledger access. Important methods include OpenForm(), LoadJavascript(), getCurrentModule(), GetNavigationItem(), and loadDashboard(). The class also handles the ledger selection dropdown for finance operations based on user permissions.

 **Code Landmarks**
- `Line 54`: Dynamic JavaScript loading with caching control based on development mode
- `Line 92`: Custom URL handling that transforms paths between UI representation and internal navigation structure
- `Line 177`: Navigation state management with browser history API integration
- `Line 304`: Dynamic dashboard generation from navigation configuration data
- `Line 396`: Permission-based UI adaptation for finance ledger selection
### reports.js

This file implements client-side functionality for OpenPetra's reporting system. It provides functions to generate, download, and display reports in various formats (HTML, PDF, Excel). Key functionality includes displaying a loading spinner during report generation, checking report progress, handling errors, and formatting parameters for report calculations. The file manages report generation through API calls to the server and handles the display and download of completed reports. Important functions include download_pdf(), download_excel(), print_report(), check_for_report(), calculate_report_common(), and start_calculate_report().

 **Code Landmarks**
- `Line 27`: Implements a singleton pattern for the loading spinner modal dialog
- `Line 89`: Implements polling mechanism to check report generation progress on the server
- `Line 116`: Handles type conversion of parameters from JavaScript types to server-expected format
### string.js

This file provides a single utility function for string manipulation in the OpenPetra JavaScript client. The replaceAll function extends JavaScript's native string replacement capability by replacing all occurrences of a search string within content, not just the first occurrence. It uses a global regular expression to achieve this functionality. This helper function likely supports various text processing needs throughout the application.

 **Code Landmarks**
- `Line 26`: Uses RegExp with global flag to replace all occurrences of a string, addressing a common JavaScript limitation where replace() only affects the first match by default
### tpl.js

This JavaScript library provides template manipulation functions for the OpenPetra client interface. It implements data binding between JavaScript objects and HTML templates, with key functionality including variable replacement in templates, form data extraction, and formatting of dates and currencies. The library offers functions like format_tpl() for binding data to templates, extract_data() for retrieving form values, and specialized formatters for currencies, dates, and checkboxes. It handles various input types including text fields, checkboxes, radio buttons, and select elements, while also providing security validation against HTML injection.

 **Code Landmarks**
- `Line 26`: translate_constants function provides internationalization support for constant values
- `Line 67`: replace_val_variables recursively processes DOM elements to substitute template variables with actual data
- `Line 196`: parseJSONDate handles conversion of ISO date strings to localized date formats
- `Line 385`: ValidateStringForInjectedHTML provides basic protection against cross-site scripting attacks
- `Line 317`: resetInput function resets form elements to their default state while respecting initial values
### utils.js

This utility file provides essential client-side functionality for the OpenPetra web application. It implements message display systems with display_message() and display_error() functions that show notifications with customizable styling, content, and timeout options. The file includes data manipulation utilities like translate_to_server(), replace_data(), and capitalizeFirstLetter() for transforming data between client and server formats. Other utilities handle file uploads, local storage for presets, modal management, and empty object detection. Key functions include toggle_pin_message(), uploadFile(), and save_preset().

 **Code Landmarks**
- `Line 26`: Implements a message pinning system that persists messages using localStorage
- `Line 48`: Creates a dynamic message display area if not present in the DOM
- `Line 59`: Handles large messages by creating downloadable text files when content exceeds display limits
- `Line 175`: Complex data transformation function that handles nested objects and special naming conventions
- `Line 232`: Custom base64 decode function that properly handles Unicode characters including umlauts

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #