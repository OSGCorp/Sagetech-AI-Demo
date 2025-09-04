# JavaScript Client Settings Of Petra

The Petra application is a JavaScript-based web client that provides user interface components for managing application settings. This sub-project implements user preference management and localization features along with secure credential handling. This provides these capabilities to the Petra program:

- User interface for language selection and switching
- Secure password management and validation
- Bootstrap-styled consistent UI components
- Client-side validation and server communication

## Identified Design Elements

1. **Localization Framework Integration**: The client settings module integrates with the i18next library to provide seamless language switching between English, German, and Norwegian throughout the application
2. **Security-Focused Password Management**: Implements comprehensive password validation including matching checks, complexity requirements, and secure server communication
3. **Bootstrap UI Integration**: Leverages Bootstrap styling conventions for consistent form presentation and responsive design
4. **Client-Server Communication**: Handles API interactions with server endpoints like TMaintenanceWebConnector_SetUserPassword2 for persistent settings changes

## Overview

The architecture follows a clean separation between presentation (HTML templates) and behavior (JavaScript) with consistent styling through Bootstrap. The localization system allows for dynamic language switching without page reloads, while the password management functionality implements proper validation and secure communication with the server. The module maintains user preferences in local storage for persistence across sessions and implements proper error handling for failed operations. The templates use placeholder text that gets replaced with localized content at runtime, ensuring a consistent multilingual experience.

## Business Functions

### User Settings
- `ChangePassword.html` : HTML form template for changing user passwords in the OpenPetra system.
- `ChangeLanguage.js` : Provides language switching functionality for the OpenPetra application interface.
- `ChangeLanguage.html` : A simple HTML form for language selection in OpenPetra with buttons for English, German, and Norwegian.
- `ChangePassword.js` : Handles user password changes with validation and API integration.

## Files
### ChangeLanguage.html

This HTML file provides a user interface for changing the application language in OpenPetra. It displays a heading and three buttons for selecting English, German, or Norwegian languages. The buttons are styled using Bootstrap's primary button class. The file includes a script reference to frmChangeLanguage.js which likely contains the functionality to handle language changes when buttons are clicked.

 **Code Landmarks**
- `Line 3-7`: Uses Bootstrap's btn-primary class for consistent styling across the application
### ChangeLanguage.js

This JavaScript file implements language switching functionality for the OpenPetra web client. It defines click event handlers for three language buttons (English, German, and Norwegian) that change the application's language using the i18next library. When a language button is clicked, the code changes the language setting, navigates to the home screen, and reloads the page to apply the changes throughout the application interface.

 **Code Landmarks**
- `Line 26`: Uses i18next internationalization framework for language switching
- `Line 28`: Reloads the entire page after language change to ensure all components reflect the new language
### ChangePassword.html

This HTML template defines a simple password change form with three input fields: current password, new password, and repeat new password. The form includes labels for each field and a submit button. All text elements use placeholders (enclosed in curly braces) that will be replaced with localized text when rendered. The form follows Bootstrap styling conventions with form-group classes and a primary-styled submit button.

 **Code Landmarks**
- `Line 1-16`: Uses template variables in curly braces for internationalization, allowing the form to be displayed in multiple languages
### ChangePassword.js

This JavaScript file implements the password change functionality for OpenPetra's settings section. It handles the password change form submission, validates that the new passwords match, and communicates with the server API to update the password. The code includes validation for password criteria, checks if the current password is correct, and ensures the new password differs from the old one. Upon successful password change, it updates the local storage and redirects to the home page. Key functionality includes the click handler for the submit button and API communication with the TMaintenanceWebConnector_SetUserPassword2 endpoint.

 **Code Landmarks**
- `Line 23`: Checks localStorage to determine if user must change password immediately
- `Line 31`: Client-side validation to ensure new password entries match before submission
- `Line 46`: Handles multiple error cases with specific user-friendly messages based on server response codes
- `Line 55`: Updates localStorage flag and redirects user after successful password change

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #