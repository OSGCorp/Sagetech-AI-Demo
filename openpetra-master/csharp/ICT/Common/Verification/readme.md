# C# Verification Subproject Of OpenPetra

The C# Verification subproject implements a comprehensive cross-platform validation framework that operates consistently across both client and server components of OpenPetra without requiring database access. This subproject provides the foundation for data integrity throughout the application by implementing type-specific validation logic, error handling mechanisms, and standardized messaging. The architecture follows a modular approach with specialized validators for different data types (strings, dates, numbers) and a unified result collection system.

Key capabilities provided to the OpenPetra program:

- Type-specific validation for strings, dates, numbers, and general objects
- Serializable validation results that can traverse application boundaries
- Internationalized error messages through GNU.Gettext integration
- Severity-based validation classification (critical vs. non-critical errors)
- Client-side validation without database dependencies

## Identified Design Elements

1. **Unified Validation Result System**: The `TVerificationResult` class serves as the core data structure for all validation outcomes, with specialized extensions like `TScreenVerificationResult` for UI contexts
2. **Type-Specialized Validators**: Separate validator classes (`TStringChecks`, `TDateChecks`, `TNumericalChecks`, etc.) provide focused validation logic for specific data types
3. **Remoting-Compatible Exceptions**: `EVerificationResultsException` enables validation errors to be serialized and transmitted between application tiers
4. **Centralized Resource Strings**: The `CommonResourcestrings` class ensures consistent messaging across all validation operations
5. **Helper Utilities**: Support classes like `THelper` provide formatting and comparison functions to enhance validation output readability

## Overview

The architecture emphasizes consistency and reusability across application boundaries while maintaining a clean separation of concerns through specialized validators. The framework supports both programmatic validation and user-friendly error reporting with internationalization support. By avoiding database dependencies, the validation logic can execute identically on both client and server sides, improving application responsiveness and reducing network traffic.

## Business Functions

### Validation Framework
- `Main.cs` : Implements data verification framework for validating data in both client and server contexts without database access.
- `Helper.cs` : Helper class for data verification with methods to format value descriptions for user-friendly display.
- `Resourcestrings.cs` : Defines common resource strings for verification routines in OpenPetra

### Data Type Validation
- `StringChecks.cs` : Provides string validation methods for both server and client-side use in OpenPetra.
- `DateChecks.cs` : Provides date and time validation utilities for OpenPetra's client and server components.
- `NumericalChecks.cs` : Provides numerical validation methods for client and server-side data verification in OpenPetra.
- `GeneralChecks.cs` : Provides general verification methods for data validation across both server and client sides without database access.

### Exception Handling
- `Exceptions.Remoted.cs` : Defines a remotable exception class for handling verification results in OpenPetra's client-server communication.

## Files
### DateChecks.cs

TDateChecks implements a comprehensive set of date validation methods for OpenPetra's client and server components. The class provides validation for undefined dates, date ranges, future/past dates, and date comparisons. Key functionality includes checking if dates are within specified ranges, comparing two dates (greater/lesser), validating corporate dates, and ensuring dates are current, past, or future. The class returns TVerificationResult objects with appropriate error messages. The file also includes TTimeChecks for validating time values. Important methods include IsNotUndefinedDateTime, IsCurrentOrFutureDate, IsDateBetweenDates, FirstLesserThanSecondDate, and FirstGreaterThanSecondDate. All validations avoid database access to ensure client-side compatibility.

 **Code Landmarks**
- `Line 50`: Defines TDateBetweenDatesCheckType enum for specialized date range validation scenarios
- `Line 76`: Uses GNU.Gettext for internationalization of error messages
- `Line 102`: IsNotUndefinedDateTime handles null values differently based on ATreatNullAsInvalid parameter
- `Line 265`: IsDateBetweenDates returns different verification results based on check type parameters
- `Line 545`: TTimeChecks validates integer time values in seconds (0-86399)
### Exceptions.Remoted.cs

This file defines EVerificationResultsException, a serializable exception class that can be passed between server and client via .NET Remoting. The exception holds a collection of verification results that contain severe validation errors. It implements multiple constructors for different initialization scenarios and provides proper serialization support through GetObjectData and deserialization constructor methods. The class extends EOPAppException and maintains a TVerificationResultCollection property to store validation errors that can be transmitted across application boundaries.

 **Code Landmarks**
- `Line 32`: Namespace comment explains the purpose of remotable exceptions in OpenPetra's architecture
- `Line 119`: Implements GetObjectData method required for proper serialization across remoting boundaries
- `Line 47`: The class is marked [Serializable()] to enable transmission via .NET Remoting
### GeneralChecks.cs

TGeneralChecks implements utility methods for common data validation tasks that work on both server and client sides of OpenPetra. The class specifically avoids database access to ensure client compatibility. It provides two key validation methods: ValueMustNotBeNull checks if an object is null, and ValueMustNotBeNullOrEmptyString verifies that a value is neither null nor an empty string. Both methods return TVerificationResult objects with appropriate error messages when validation fails, supporting optional context and column information for UI feedback.

 **Code Landmarks**
- `Line 29`: Class explicitly designed to work on both server and client sides without database access
- `Line 37`: Uses GNU.Gettext for internationalization of error messages
- `Line 76`: Returns specialized TScreenVerificationResult when column information is provided for UI feedback
### Helper.cs

THelper implements a static utility class for data verification in OpenPetra. It provides helper methods to format value descriptions for user-friendly display in verification messages. The key functionality is the NiceValueDescription method which takes a string value description and returns it surrounded by single quotes, or returns the localized word 'Value' if the input is empty. The method also handles special formatting cases like removing trailing colons, ampersands (used for keyboard shortcuts), and normalizing single quotes.

 **Code Landmarks**
- `Line 53`: Uses GNU.Gettext for internationalization support of error messages
- `Line 56`: Special handling for UI control labels that might contain formatting characters like colons and ampersands
### Main.cs

This file implements a comprehensive data verification framework for OpenPetra that works on both server and client sides without requiring database access. It defines key classes for handling validation results: TVerificationResult stores basic validation information including context, text, severity, and error codes; TScreenVerificationResult extends this for UI-specific validations with DataColumn references; and TVerificationResultCollection manages collections of validation results. The framework supports critical and non-critical errors, provides methods for finding, adding, and removing validation results, and includes helper methods in TVerificationHelper for comparing and formatting validation results. The file also defines TResultSeverity enumeration for error classification and TVerificationException for exception handling within the validation framework.

 **Code Landmarks**
- `Line 29`: The file explicitly states it must not access databases since client-side code doesn't have database access
- `Line 1057`: TVerificationHelper.IsNullOrOnlyNonCritical provides a null-safe way to check for critical errors in validation results
- `Line 1206`: EVerificationException transforms exception details into TVerificationResultCollection for consistent error handling
- `Line 721`: AddOrRemove method intelligently handles validation results with DataValidationRunID to prevent removing errors from the same validation run
- `Line 1152`: Special handling for duplicate record errors allows updating the error text while considering them the same error
### NumericalChecks.cs

TNumericalChecks implements a utility class for validating numerical data in both client and server contexts. It provides comprehensive methods for checking integer, decimal, and floating-point values against various constraints. Key functionality includes validating if numbers are positive, negative, non-zero, within ranges, or have appropriate precision. The class offers methods like IsValidInteger, IsPositiveDecimal, IsNegativeOrZeroInteger, FirstLesserThanSecondDecimal, and IsNumberPrecisionNotExceeded. Each validation method returns null for valid values or a TVerificationResult with appropriate error messages for invalid data. The class uses localized error messages and supports optional context information for validation results.

 **Code Landmarks**
- `Line 28`: Class designed to work on both client and server sides without database access
- `Line 44`: Uses GNU.Gettext for internationalization of error messages
- `Line 576`: IsNumberPrecisionNotExceeded method handles complex validation of decimal precision and scale
- `Line 624`: Sophisticated algorithm to remove trailing zeros when checking fractional digits
### Resourcestrings.cs

CommonResourcestrings implements a static class that provides standardized resource strings for verification routines throughout OpenPetra. It centralizes commonly used messages for data validation scenarios, making them available for consistent use across the application. The class organizes strings into logical sections: Titles for error headers and Message content texts for detailed explanations. Important string constants include StrInvalidDataTitle, StrInformationMissingTitle, StrInvalidNumberEntered, StrInvalidDateEntered, and StrSettingCannotBeEmpty. All strings use the Catalog.GetString method, likely for internationalization support.

 **Code Landmarks**
- `Line 32`: Uses Catalog.GetString() for all string definitions, suggesting internationalization support
- `Line 26`: Class is organized into regions to separate different types of resource strings
### StringChecks.cs

TStringChecks implements validation methods for string data that work on both server and client sides without database access. Key functionality includes validating email addresses (supporting single or multiple addresses), checking string emptiness, validating string length, comparing string order, and verifying if lookup values are active. Important methods include StringMustNotBeEmpty(), StringLengthLesserOrEqual(), FirstLesserOrEqualThanSecondString(), ValidateValueIsActive(), and several ValidateEmail() overloads. The class uses regular expressions for email validation and provides localized error messages through GNU.Gettext.

 **Code Landmarks**
- `Line 249`: Uses a comprehensive regular expression to validate email addresses according to RFC 2822 standards
- `Line 142`: Implements reusable validation methods that work without database access for client-side use
- `Line 196`: Provides specialized validation for inactive records in lookup tables

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #