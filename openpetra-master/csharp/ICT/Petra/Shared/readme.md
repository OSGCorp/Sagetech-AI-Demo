# C# Petra Shared Components Of Petra

The Petra application is a C# program that helps non-profit organizations manage administration and reduce operational overhead. The program handles contact management, accounting, and sponsorship while supporting international operations. This sub-project implements core shared components that serve as the foundation for the entire Petra ecosystem, providing cross-cutting functionality used throughout the application.

- Security infrastructure (authentication, authorization, encryption)
- Data caching mechanisms for performance optimization
- Type conversion and standardized data handling
- Centralized error management and messaging
- System-wide constants and configuration

## Identified Design Elements

1. **Comprehensive Security Framework**: Implements multi-layered security with user permissions, module access controls, and data encryption using TripleDES cryptography
2. **Thread-Safe Caching System**: Provides efficient data access through a sophisticated caching mechanism with reader-writer locks to prevent concurrency issues
3. **Standardized Type System**: Defines core enumerations and conversion utilities that ensure consistent data representation across the application
4. **Centralized Error Handling**: Maintains a structured error code system organized by module with internationalization support
5. **Partner Access Control**: Implements granular access levels for partner data with restrictions based on user, group, and foundation ownership

## Overview
The architecture emphasizes security and performance through its caching mechanisms while maintaining consistent data representation across the application. The components follow a modular design that separates concerns like security, data access, and error handling. This shared foundation enables other Petra modules to operate with consistent behavior while reducing code duplication. The cross-cutting nature of these components makes them critical to the overall system integrity and performance.

## Sub-Projects

### csharp/ICT/Petra/Shared/lib

The program handles financial transactions and organizational data management across multiple currencies and jurisdictions. This sub-project implements core shared libraries and common functionality along with data access patterns that support the broader application architecture. This provides these capabilities to the OpenPetra program:

- Cross-cutting concerns implementation (logging, configuration, security)
- Data access layer with ORM functionality
- Common business logic shared across application modules
- Utility functions for data transformation and validation
- Multi-language and internationalization support

#### Identified Design Elements

1. **Data Access Abstraction**: Implements a repository pattern that decouples business logic from database operations, supporting multiple database backends
2. **Domain Model Implementation**: Contains the core business entities and validation logic used throughout the application
3. **Service Layer Architecture**: Provides intermediary services between controllers and data repositories to encapsulate business rules
4. **Cross-Cutting Concerns**: Centralizes logging, error handling, security, and configuration management
5. **Internationalization Framework**: Supports multiple languages and regional formatting for global deployment

#### Overview
The architecture follows a modular design that promotes code reuse and maintainability. The shared libraries implement common patterns and utilities that ensure consistency across the application. The domain model represents the core business concepts while the service layer enforces business rules. This separation of concerns allows for easier testing and maintenance while providing a solid foundation for feature development. The libraries are designed to support both web-based and desktop interfaces through common abstractions.

  *For additional detailed information, see the summary for csharp/ICT/Petra/Shared/lib.*

## Business Functions

### Data Management
- `Cache.cs` : Implements a caching system for accessing shared data across the OpenPetra application.
- `CacheableTablesManager.cs` : Manages a cache of DataTables for both server and client sides of OpenPetra, providing thread-safe access to shared lookup data.
- `Delegates.cs` : Defines a delegate type for retrieving cacheable data tables in OpenPetra's client-server architecture.

### Security
- `Security.cs` : Security utility class providing permission checks for OpenPetra modules and setup screens
- `Security.Data.cs` : Implements data encryption and decryption using TripleDES with key derivation from image and text inputs.
- `Security.Principal.cs` : Implements user authentication and authorization for OpenPetra, managing user permissions and access control.
- `Security.Types.cs` : Defines the TPartnerAccessLevelEnum for managing partner access permissions in OpenPetra's security system.
- `UserInfo.cs` : Manages user information storage and retrieval in the OpenPetra session system.

### Core Types and Constants
- `Constants.cs` : Defines system-wide constants for OpenPetra, including module access permissions, registry keys, and system default settings.
- `Types.cs` : Defines essential enumeration types and helper methods for partner management in OpenPetra.
- `TypeConverter.cs` : Utility file for type conversion in OpenPetra, but appears to be empty or incomplete.

### Error Handling
- `ErrorCodes.cs` : Defines error codes used throughout OpenPetra for standardized error handling and user messaging.
- `Messages.cs` : Utility class for formatting error messages from verification results in OpenPetra.

### Utilities
- `Conversions.cs` : Utility class providing DateTime conversion functions for OpenPetra.

## Files
### Cache.cs

TSharedDataCache implements a caching mechanism for accessing frequently used data tables across different modules of OpenPetra. It organizes cache functionality into nested classes for different domains (Common, Conference, Finance, Partner, Personnel, SysMan), each with delegate patterns to retrieve cacheable tables. Each domain class defines delegates for retrieving specific data tables, properties to set these delegates, and methods that use these delegates to fetch data tables. The pattern allows for lazy loading of data while providing a consistent interface across the application. The implementation uses ThreadStatic attributes to ensure thread safety for delegate references.

 **Code Landmarks**
- `Line 43`: Uses ThreadStatic attribute to ensure thread safety for delegate references in a multi-threaded environment
- `Line 263`: Modern C# property syntax (get; set;) used in TMPartner class while other classes use traditional property implementation
- `Line 48`: Delegate pattern implementation allows for dependency injection of data retrieval functionality
### CacheableTablesManager.cs

TCacheableTablesManager implements a thread-safe cache system for DataTables used by both PetraServer and PetraClient. It manages a collection of cacheable tables with functionality for adding, refreshing, merging, and retrieving DataTables from the cache. The class uses ReaderWriterLock to protect shared resources and returns copies of cached tables to prevent multi-threading issues. Key methods include AddCachedTable, GetCachedDataTable, AddOrRefreshCachedTable, and MarkCachedTableNeedsRefreshing. The file also defines TCacheableTablesLoader helper class and custom exceptions for cache management errors. The implementation supports cache size management, table refreshing notifications across clients, and handling of both typed and untyped DataTables.

 **Code Landmarks**
- `Line 88`: Uses ThreadStatic attribute to ensure thread-local storage of cache data, preventing cross-thread contamination
- `Line 149`: ReaderWriterLock implementation allows multiple readers but exclusive writer access for thread safety
- `Line 306`: Returns copies of DataTables rather than references to prevent multi-threading issues with external modifications
- `Line 564`: Cross-client notification system using delegate to inform other clients when cached data changes
- `Line 1013`: Custom exception hierarchy for distinguishing between general cache errors and tables needing refresh
### Constants.cs

SharedConstants defines a comprehensive set of constant values used throughout the OpenPetra application. It primarily contains string constants for user access permissions (PETRAMODULE and PETRAGROUP prefixed constants), registry keys, system default settings identifiers, client task groups, and other application-wide values. The file organizes constants into logical groups such as module access permissions, remoting URL identifiers, system defaults, and security-related values. It also includes a few readonly fields for values that need to be translatable rather than fixed constants. These constants provide a centralized reference point for string literals used across the application, improving maintainability and consistency.

 **Code Landmarks**
- `Line 257`: Contains translatable 'constants' implemented as readonly fields rather than const values
- `Line 30`: Organizes constants into logical groups with consistent naming conventions (PETRAMODULE_, SYSDEFAULT_, etc.)
- `Line 173`: Extensive system defaults constants suggest a configuration system based on key-value pairs
### Conversions.cs

Conversions implements a utility class that provides methods for converting between DateTime objects and integer representations of time. The class contains two key static methods: DateTimeToInt32Time which encodes the time portion of a DateTime object into an Int32 value representing seconds (hours*3600 + minutes*60 + seconds), and Int32TimeToDateTime which converts an Int32 time value back into a DateTime object using the current date. These conversion functions support Petra's data handling requirements when working with time values.

 **Code Landmarks**
- `Line 38`: Converts DateTime time component to seconds-based integer representation
- `Line 48`: Creates DateTime using current date but with time from integer seconds value
### Delegates.cs

This file defines a single delegate type TGetCacheableDataTableFromCache in the Ict.Petra.Shared namespace. The delegate is designed for both client and server-side use to retrieve DataTable objects from a data cache system. It takes a string parameter for the cacheable table name and returns both a DataTable and an output System.Type parameter. This delegate facilitates data caching functionality across the OpenPetra application architecture.

 **Code Landmarks**
- `Line 30`: The delegate definition supports cross-boundary data caching between client and server components
### ErrorCodes.cs

PetraErrorCodes defines a comprehensive collection of error codes used throughout the OpenPetra application for standardized error handling and user messaging. The file organizes error codes into logical sections by module (General, Conference, Finance, Partner, Personnel, and SysMan) with each error having a unique identifier following the pattern "[MODULE].[NUMBER][TYPE]" where TYPE indicates severity (V for verification, N for non-critical, E for error). Each error code is decorated with ErrCodeAttribute containing message text, title, and descriptions that support internationalization. This centralized approach ensures consistent error reporting and enables support staff to identify errors regardless of the user's language setting.

 **Code Landmarks**
- `Line 35`: Error codes follow a specific format: module abbreviation, period, five-digit number, and severity indicator (V/N/E)
- `Line 48`: ErrCodeAttribute decorates error constants with metadata for displaying user-friendly messages
- `Line 54`: Error codes are designed to be language-independent, allowing support staff to identify errors regardless of UI language
### Messages.cs

Messages implements a utility class for processing error messages in OpenPetra. It provides functionality to format error messages from verification results with appropriate headlines. The class contains a static method BuildMessageFromVerificationResult that takes a message headline and verification result collection, then constructs a formatted error message string with bullet points for each verification entry. The class also defines a static string constant StrWarningsAttention used when displaying warnings to users. The method handles different scenarios including critical errors versus warnings and properly formats context information and result codes.

 **Code Landmarks**
- `Line 43`: Uses the Catalog.GetString method for internationalization of user-facing messages
- `Line 53`: Implements null-checking logic with appropriate exception throwing when parameters are invalid
- `Line 55`: Dynamically selects appropriate message headline based on error severity
### Security.Data.cs

TSecureData implements a security class that provides encryption and decryption functionality using TripleDES cryptography. The class uses a combination of text strings and an image to derive encryption keys. Key methods include GetData() for decryption and PutData() for encryption, both converting between clear text and encrypted Base64 strings. Private helper methods I2KB() converts an image to a byte array, while GetKey() derives encryption keys from the image data and text salt values. The class stores text arrays, image data, and encoding information as member variables.

 **Code Landmarks**
- `Line 74`: Custom key derivation algorithm that uses both image data and text strings as entropy sources for cryptographic keys
- `Line 96`: Image-to-key-bytes conversion method that transforms image data into raw bytes for cryptographic operations
- `Line 47`: Constructor initializes TripleDES encryption with UTF8 encoding, storing both text and image inputs for key derivation
### Security.Principal.cs

TPetraPrincipal implements a class representing an authenticated user in the OpenPetra system with their associated security permissions. It manages user identity, group memberships, module access rights, roles, and functions. Key functionality includes checking if a user belongs to specific groups, has certain roles, can access specific modules, or has permissions for particular ledgers. The class stores user information like UserID, PartnerKey, ProcessID, and LoginMessage. Important methods include IsInGroup(), IsInRole(), IsInModule(), and IsInLedger(). The file also defines two custom exception classes: ELoginMessageAlreadySetException and EProcessIDAlreadySetException for handling permission-related errors.

 **Code Landmarks**
- `Line 144`: Arrays are sorted to enable fast BinarySearch operations for permission checks
- `Line 174`: Table access permission checking is disabled in the current implementation
- `Line 214`: Special handling for ledger access with formatted ledger number strings
### Security.Types.cs

This file defines the TPartnerAccessLevelEnum enumeration within the Ict.Petra.Shared.Security namespace. The enumeration specifies four levels of access control for partners in the OpenPetra system: unrestricted access (palGranted), access restricted to a group (palRestrictedToGroup), access restricted to a specific user (palRestrictedToUser), and access restricted by foundation ownership (palRestrictedByFoundationOwnership). These enumeration values are used to implement security controls for partner data throughout the application.
### Security.cs

TSecurityChecks implements static security utility methods for the OpenPetra application. It defines constants for security permissions and restrictions, and provides functionality to check user access rights to various modules. Key features include methods for validating permissions to edit setup screens, determining module-specific permissions, and recursively evaluating complex permission expressions using AND/OR logic. The class supports both client-side and server-side security validation, throwing appropriate exceptions when access is denied. Important constants include SECURITYPERMISSION_EDITING_AND_SAVING_OF_SETUP_DATA and SECURITYRESTRICTION_READONLY, while key methods are CheckUserModulePermissions and GetModulePermissionForSavingOfSetupScreenData.

 **Code Landmarks**
- `Line 119`: Recursive permission checking that supports complex boolean expressions with OR/AND operators
- `Line 73`: Module-specific permission mapping system for setup screen data access
- `Line 177`: Dual-purpose security check that works on both client and server sides
- `Line 212`: Special handling for ledger-specific permissions with numeric formatting
### TypeConverter.cs

TypeConverter.cs appears to be a placeholder file in the OpenPetra.org project. While it includes standard copyright notices and license information (GNU GPL v3), the file contains only namespace declaration for Ict.Petra.Shared without any actual implementation code. The file seems intended for type conversion functionality based on its name and imported namespaces (System, System.Globalization, System.Collections, System.ComponentModel, and System.Data), but lacks any concrete classes, methods, or variables.
### Types.cs

Types.cs defines core enumeration types and conversion utilities used throughout the OpenPetra system. It implements enumerations for partner classifications (TPartnerClass), addressee types (TStdAddresseeTypeCode), partner status codes (TStdPartnerStatusCode), table access permissions (TTableAccessPermission), and module identifiers (TModule, TModuleEnum). The SharedTypes class provides utility methods for converting between string representations and enumeration values, particularly for partner classes, addressee types, and partner status codes. These types form the foundation for partner data management across the system, ensuring consistent representation of key entities and their states.

 **Code Landmarks**
- `Line 30-71`: Defines TPartnerClass enum with seven distinct partner categories that form the foundation of OpenPetra's contact management system
- `Line 297-323`: PartnerClassEnumToString method uses switch statement to safely convert enum values to their string representations
- `Line 390-423`: StdAddresseeTypeCodeEnumToString handles special formatting cases like '1-FEMALE' that don't directly match enum names
### UserInfo.cs

UserInfo implements a class that handles user information storage and retrieval within the OpenPetra session system. It provides functionality to store and access security-related user data globally across the application. The class contains two static methods: GetUserInfo() retrieves user information from the session as a TPetraPrincipal object using JSON deserialization, and SetUserInfo() stores a TPetraPrincipal object in the session and sets the UserID variable. The class serves as a centralized access point for user authentication and authorization data.

 **Code Landmarks**
- `Line 46`: Uses JSON deserialization to convert session data back into TPetraPrincipal objects
- `Line 67`: Sets both the complete UserInfo object and a separate UserID variable for quick access

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #