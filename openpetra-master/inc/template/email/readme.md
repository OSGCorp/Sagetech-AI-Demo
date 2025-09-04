# Include Template EMail Subproject Of Petra

The Petra application is a free, open-source software system designed to help non-profit organizations manage administration tasks efficiently. The Include Template EMail subproject implements a comprehensive email templating system that supports multilingual communication across various user workflows. This subproject provides standardized email templates for critical user interactions within the OpenPetra ecosystem.

## Key Capabilities

- Multilingual Support: Templates available in multiple languages (currently English and German)
- Personalized Communication: Dynamic content insertion through placeholders (e.g., {FirstName}, {LastName})
- Security-Focused Workflows: Password reset, account creation, and verification processes
- Conditional Content Rendering: Templates use {ifdef} tags to include optional sections based on available data
- Automated Notifications: Standardized reminder and notification templates

## Identified Design Elements

1. **Template Standardization**: Common structure across templates ensures consistent user experience and brand identity
2. **Internationalization Framework**: Separate template files for each supported language (e.g., _en.txt, _de.txt)
3. **Token-Based Security**: Implementation of secure token parameters for sensitive operations like password resets
4. **Placeholder System**: Flexible variable substitution mechanism for dynamic content insertion
5. **Conditional Sections**: Support for including or excluding content blocks based on context

## Architecture Overview

The email templates serve as the presentation layer for system-generated communications. They follow a consistent naming convention that identifies both function and language. The architecture separates content from functionality, allowing for easy maintenance and localization. Templates are designed to be human-readable text files that can be modified without programming knowledge while maintaining the necessary placeholders for dynamic content insertion.

## Business Functions

### Email Templates
- `requestnewpassword_en.txt` : Email template for password reset requests in OpenPetra system.
- `selfservicesignup_en.txt` : Email template for self-service user account signup confirmation in OpenPetra.
- `newuserpassword_en.txt` : Email template for sending new user passwords in OpenPetra.
- `welcomeemail_de.txt` : German welcome email template for new OpenPetra users with account setup instructions.
- `reminder_en.txt` : Email template for reminders about contacts in OpenPetra system.
- `welcomeemail_en.txt` : Email template for welcoming new OpenPetra users with account setup instructions.
- `reminder_de.txt` : German email template for contact reminders in OpenPetra system.
- `selfservicesignup_de.txt` : German email template for self-service signup confirmation in OpenPetra.
- `newuserpassword_de.txt` : German email template for sending new user passwords to OpenPetra users.
- `requestnewpassword_de.txt` : German email template for password reset requests in OpenPetra.

## Files
### newuserpassword_de.txt

This file contains a German language email template used when sending new password information to users. The template includes placeholders for dynamic content such as {UserId}, {FirstName}, {LastName}, {Domain}, and {NewPassword}. It informs users of their new password, instructs them to change it upon next login, and includes a disclaimer that this is an automated email that should be ignored if the recipient didn't register on the platform.

 **Code Landmarks**
- `Line 1-11`: Uses simple text-based templating with {placeholder} syntax for variable substitution rather than a complex templating engine
### newuserpassword_en.txt

This file contains an email template used when a new user is created in OpenPetra or when a password reset is requested. The template includes placeholders for dynamic content such as {UserId}, {FirstName}, {LastName}, {Domain}, and {NewPassword}. The email informs users of their initial password and instructs them to change it upon first login, while also including a disclaimer for recipients who did not register.

 **Code Landmarks**
- `Line 1`: Template uses simple text format rather than HTML for maximum compatibility across email clients
- `Line 5`: Password is included in plaintext in the email, which has security implications
- `Line 9-11`: Security notice advising recipients to ignore the email if they didn't register helps prevent social engineering attacks
### reminder_de.txt

This template file contains the German version of an automated reminder email sent to contacts in the OpenPetra system. It includes placeholders for dynamic content such as recipient name, contact details, reason for the reminder, event date, comments, next reminder date, and notification if the reminder has been disabled. The template is structured with conditional sections that only display when specific information is available.

 **Code Landmarks**
- `Line 4`: Uses {ifdef} conditional templating syntax to handle optional content sections
- `Line 1`: Template uses {variable} placeholders for dynamic content insertion
### reminder_en.txt

This template file defines the structure and content of automated reminder emails sent by OpenPetra. It includes placeholders for recipient details, contact information, reminder reasons, event dates, comments, and next reminder dates. The template uses conditional formatting with {ifdef} tags to include optional sections only when relevant data is available. It also includes a special section for disabled reminders and clearly identifies itself as an automated email.

 **Code Landmarks**
- `Line 1`: Uses a simple template system with {variable} placeholders for dynamic content insertion
- `Line 7-9`: Implements conditional content blocks with {ifdef} and {endif} tags for optional information
- `Line 23-25`: Contains conditional section for disabled reminders, showing template's ability to adapt to different reminder states
### requestnewpassword_de.txt

This file contains a German language email template used when users request a password reset in OpenPetra. The template includes personalized greeting with the user's name, notification about the password reset request, instructions to ignore if not requested, and a link with tokens to set a new password. The template uses placeholders like {UserId}, {FirstName}, {LastName}, {Domain}, and {Token} that get replaced with actual values when the email is sent.

 **Code Landmarks**
- `Line 7`: Uses {Token} placeholder for secure password reset token that will be dynamically inserted
### requestnewpassword_en.txt

This file contains an email template used when users request a password reset in OpenPetra. The template includes placeholders for personalization (UserId, FirstName, LastName, Domain) and contains the password reset link with a security token. The message explains the purpose of the email, provides instructions for setting a new password, and includes a disclaimer for users who didn't request the reset.

 **Code Landmarks**
- `Line 9`: Uses a token-based security mechanism for password resets via the ResetPasswordToken parameter
### selfservicesignup_de.txt

This file contains a German language email template used when a user signs up for OpenPetra self-service access. The template includes a greeting with the recipient's name, notification about an account request, a warning to ignore if they didn't request access, and a verification link containing user ID and token parameters. The email concludes with a note indicating it's automatically generated.

 **Code Landmarks**
- `Line 5`: Uses template variables {FirstName} and {LastName} for personalization
- `Line 6`: Uses {Domain} variable to dynamically insert the application domain
- `Line 11`: Contains verification URL with security parameters {UserId} and {SelfSignupPasswordToken}
### selfservicesignup_en.txt

This email template file provides the standard text for account signup confirmation emails in OpenPetra's self-service registration process. It includes placeholders for personalization (FirstName, LastName) and system variables (Domain, UserId, Token). The template informs users about their account request, provides instructions to complete signup by following a personalized link, and includes security information for users who didn't request an account.

 **Code Landmarks**
- `Line 7`: Uses token-based authentication for secure self-service signup completion
- `Line 6`: Implements security measure by acknowledging potential unauthorized signup attempts
### welcomeemail_de.txt

This file contains a German language email template used to welcome new users to their OpenPetra instance. The template includes personalized placeholders for user information such as names, domain, email addresses, and reset tokens. It provides instructions for setting up two user accounts: a SYSADMIN account for administrative tasks and a regular user account for daily work. The template explains how to set initial passwords via provided links, how to reset forgotten passwords, and where to find help documentation and support forums.

 **Code Landmarks**
- `Line 1`: Template uses simple {Placeholder} syntax for variable substitution in the email body
- `Line 7`: Links to external documentation resources to help new users get started
- `Line 18-22`: Contains password reset links with security tokens for both user accounts
### welcomeemail_en.txt

This file contains an email template in English that welcomes new users to their OpenPetra instance. It provides information about two user accounts (SYSADMIN and a regular user account), including links to set passwords for both accounts using reset tokens. The template includes instructions for password recovery, links to documentation for initial setup, and information about the forum for support questions. The template uses placeholders like {FirstName}, {LastName}, {Domain}, {UserId}, and various token variables that get replaced with actual values when the email is sent.

 **Code Landmarks**
- `Line 7`: Links to external documentation for initial instance setup, showing integration with broader documentation system
- `Line 11-19`: Implements security best practice by separating administrative and regular user accounts with clear usage guidance
- `Line 21-24`: Uses token-based password reset mechanism with unique URLs for secure account initialization

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #