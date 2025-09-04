# C# Common Library Of OpenPetra

The C# Common Library is a foundational component of OpenPetra that provides cross-cutting concerns, infrastructure services, and utility functions used throughout the application. This library implements essential functionality that supports the core operations of OpenPetra's non-profit management system.

The library is organized into several key functional areas:

- **Error Handling & Logging**: Comprehensive exception hierarchy with serializable exceptions for remoting, standardized error codes, and a flexible logging system supporting multiple output destinations
- **Security & Authentication**: Password management with multiple hashing schemes (V1-V4) using Scrypt key stretching, security exceptions, and access control mechanisms
- **Configuration Management**: Server settings, application configuration, and command-line parameter processing
- **Internationalization**: Support for multiple languages through GNU Gettext, number-to-words conversion, and culture-aware formatting
- **Utility Components**: 
  - Type definitions and data structures (TSelfExpandingArrayList, TVariant)
  - String manipulation and formatting utilities
  - Network and system information gathering
  - Version management and file handling
  - Thread management and identification

## Identified Design Elements

1. **Versioned Security Implementation**: Password hashing schemes are versioned (V1-V4) to allow secure migration paths as security requirements evolve
2. **Cross-Platform Compatibility**: Utilities detect operating environment and adapt behavior for different platforms
3. **Extensible Error System**: Centralized error code inventory with metadata for support staff
4. **Flexible Configuration**: Layered configuration system combining file-based settings with command-line overrides
5. **Remoting Support**: Serializable exceptions and data structures designed to work across .NET Remoting boundaries

## Overview
The architecture emphasizes security, internationalization, and cross-platform compatibility to support OpenPetra's global mission. The library provides a foundation for both client and server components, with clear separation between infrastructure concerns and application-specific logic. The modular design allows for extension and maintenance while maintaining backward compatibility for existing systems.

## Sub-Projects

### csharp/ICT/Common/Remoting

This subproject implements a .NET Remoting framework that facilitates transparent method calls between client applications and the server, allowing OpenPetra to function as a distributed system while maintaining a cohesive user experience. The remoting layer abstracts network communication details from the application logic, providing these capabilities:

- Remote procedure calls across network boundaries
- Serialization and deserialization of complex data objects
- Session management and authentication
- Fault tolerance and error handling for network operations
- Asynchronous communication patterns

#### Identified Design Elements

1. **Service Interface Contracts**: Defines the communication boundaries between client and server components through well-defined interfaces
2. **Proxy Generation**: Automatically creates client-side proxies that mirror server-side objects, making remote calls appear local
3. **Connection Management**: Handles network connections, reconnection logic, and connection pooling for optimal performance
4. **Data Marshalling**: Transforms complex .NET objects into serializable formats for transmission over the network
5. **Security Layer**: Implements authentication, authorization, and secure communication channels between clients and server

#### Overview
The architecture follows a service-oriented approach with clear separation between interface definitions and implementations. The remoting infrastructure is designed to be transparent to application code, allowing developers to focus on business logic rather than communication details. Performance optimizations include connection pooling, batched operations, and efficient serialization techniques to minimize network overhead in multi-user environments.

  *For additional detailed information, see the summary for csharp/ICT/Common/Remoting.*

### csharp/ICT/Common/IO

This cross-cutting component provides standardized interfaces for file operations, data conversion, communication protocols, and security features. The subproject enables OpenPetra's core functionality through robust I/O operations that support the nonprofit management features of the application.

Key capabilities provided to the OpenPetra program:

- File Operations: Reading, writing, compressing, and processing various file formats (text, XML, YAML, CSV)
- Data Conversion: Transforming between formats (XML/YAML/CSV/Excel) while preserving structure and types
- External System Integration: Email (SMTP), HTTP communication, Excel automation, SEPA financial services
- Security: Encryption services using Rijndael algorithm for sensitive data protection
- Template Processing: Text template handling with placeholders, snippets, and conditional sections

#### Identified Design Elements

1. **Format Conversion Framework**: Multiple specialized parsers (XML, YAML, CSV, OpenDocument) with consistent interfaces for transforming between data representations
2. **Compression Utilities**: Unified API for handling various compression formats (ZIP, TAR, GZip, 7Zip) with streaming capabilities
3. **External Protocol Adapters**: Abstraction layers for SMTP, HTTP, and financial protocols that shield application code from implementation details
4. **Text Processing Pipeline**: Sophisticated encoding detection, line ending normalization, and template processing for consistent text handling
5. **Security Infrastructure**: Encryption services with key management for protecting sensitive information

#### Overview

The architecture emphasizes modularity through specialized components that handle specific I/O concerns while maintaining consistent interfaces. The design separates low-level file operations from higher-level data transformation and external system integration. Error handling is robust throughout the codebase, with specialized exceptions for different failure scenarios. The subproject balances performance considerations (streaming large files) with developer ergonomics (intuitive APIs) to support both application functionality and maintainability.

  *For additional detailed information, see the summary for csharp/ICT/Common/IO.*

### csharp/ICT/Common/Printing

The program handles financial transactions and contact management while reducing operational costs. This sub-project implements document generation and printing functionality along with barcode generation capabilities. This provides these capabilities to the Petra program:

- PDF document generation from HTML templates
- Flexible printing layouts for reports, letters, and labels
- Barcode generation for Code 128 format
- Multi-format output support (PDF, HTML, TXT)

#### Identified Design Elements

1. **Layered Printing Architecture**: Abstract base classes (TPrinter, TPrinterLayout) provide core functionality that is extended by format-specific implementations (TPdfPrinter, TTxtPrinter)
2. **Template-Based Document Generation**: Form letters, labels, and reports are generated from HTML templates with variable substitution
3. **Cross-Platform Compatibility**: Font resolution and rendering is handled to ensure consistent output across Windows and Linux environments
4. **Flexible Positioning System**: Supports both absolute positioning for precise layouts and flow-based positioning for dynamic content

#### Overview
The architecture follows a modular design with clear separation between abstract printing interfaces and concrete implementations. The HTML-to-PDF conversion leverages external tools (wkhtmltopdf) while maintaining a consistent API. The system supports various output formats and printing styles including report-style (with text fitting) and form letter style (with exact positioning). Barcode generation capabilities are integrated for document identification needs. The printing subsystem handles complex layout requirements including tables, images, and text with various formatting options while managing pagination and page breaks automatically.

  *For additional detailed information, see the summary for csharp/ICT/Common/Printing.*

### csharp/ICT/Common/Verification

This subproject provides the foundation for data integrity throughout the application by implementing type-specific validation logic, error handling mechanisms, and standardized messaging. The architecture follows a modular approach with specialized validators for different data types (strings, dates, numbers) and a unified result collection system.

Key capabilities provided to the OpenPetra program:

- Type-specific validation for strings, dates, numbers, and general objects
- Serializable validation results that can traverse application boundaries
- Internationalized error messages through GNU.Gettext integration
- Severity-based validation classification (critical vs. non-critical errors)
- Client-side validation without database dependencies

#### Identified Design Elements

1. **Unified Validation Result System**: The `TVerificationResult` class serves as the core data structure for all validation outcomes, with specialized extensions like `TScreenVerificationResult` for UI contexts
2. **Type-Specialized Validators**: Separate validator classes (`TStringChecks`, `TDateChecks`, `TNumericalChecks`, etc.) provide focused validation logic for specific data types
3. **Remoting-Compatible Exceptions**: `EVerificationResultsException` enables validation errors to be serialized and transmitted between application tiers
4. **Centralized Resource Strings**: The `CommonResourcestrings` class ensures consistent messaging across all validation operations
5. **Helper Utilities**: Support classes like `THelper` provide formatting and comparison functions to enhance validation output readability

#### Overview

The architecture emphasizes consistency and reusability across application boundaries while maintaining a clean separation of concerns through specialized validators. The framework supports both programmatic validation and user-friendly error reporting with internationalization support. By avoiding database dependencies, the validation logic can execute identically on both client and server sides, improving application responsiveness and reducing network traffic.

  *For additional detailed information, see the summary for csharp/ICT/Common/Verification.*

### csharp/ICT/Common/Conversion

This cross-cutting concern provides robust date parsing, validation, and formatting capabilities that support the broader administrative and financial functions of the OpenPetra platform. The implementation handles culture-specific date formats, input validation, and automatic error correction to ensure consistent date handling throughout the application.

Key technical capabilities include:

- Culture-aware date parsing from various input formats (numeric, relative, special values)
- Intelligent date validation with automatic correction of common errors
- Bidirectional conversion between DateTime objects and localized string representations
- Comprehensive error handling with user-friendly messages

#### Identified Design Elements

1. **Format Flexibility**: Supports multiple date input patterns including numeric formats (211105), relative dates (+/- days), and special values like "today"
2. **Cultural Awareness**: Handles different regional date formats through culture-specific parsing and formatting
3. **Error Correction**: Automatically resolves common user input errors such as month/day transposition
4. **Validation Chain**: Implements a progressive validation approach that attempts multiple parsing strategies before failing

#### Architecture
The date conversion utilities are implemented as a cross-cutting concern, making them available throughout the application without creating dependencies between functional modules. This design supports OpenPetra's international focus by properly handling date formats across different regions while maintaining data integrity for financial and administrative operations.

  *For additional detailed information, see the summary for csharp/ICT/Common/Conversion.*

### csharp/ICT/Common/DB

The program handles financial transactions and manages organizational data across multiple modules. This sub-project implements the database abstraction layer along with transaction management capabilities. This provides these capabilities to the OpenPetra program:

- Database-agnostic data access with support for PostgreSQL and MySQL
- Transaction management with isolation levels and thread safety
- SQL query caching for improved performance
- Parameterized query execution with proper type handling
- Sequence management for database identifiers

#### Identified Design Elements

1. **Database Abstraction Layer**: The IDataBaseRDBMS interface allows OpenPetra to work with different database systems through database-specific implementations (PostgreSQL.cs, MySQL.cs)
2. **Transaction Management**: TDBTransaction provides a wrapper for ADO.NET transactions with additional tracking and debugging information
3. **Connection Pooling**: TDBConnection handles connection establishment, pooling, and proper resource disposal
4. **Query Caching**: TSQLCache improves performance by storing and reusing query results
5. **Exception Handling**: Specialized database exceptions provide detailed error information and support for automatic retry mechanisms

#### Overview
The architecture emphasizes database independence through a well-defined abstraction layer, ensuring OpenPetra can work with different database systems without code changes. The transaction management system provides robust isolation with proper thread safety controls. Performance optimization is achieved through connection pooling and query caching. The design includes comprehensive error handling with specialized exceptions and automatic retry capabilities for transient errors. This foundation supports OpenPetra's data-intensive features like contact management, accounting, and sponsorship tracking.

  *For additional detailed information, see the summary for csharp/ICT/Common/DB.*

### csharp/ICT/Common/Data

The program handles financial transactions and manages organizational data across multiple domains. This sub-project implements the core data access layer and typed dataset functionality along with exception handling for database operations. This provides these capabilities to the OpenPetra program:

- Strongly-typed data access with automatic SQL generation
- Cross-platform compatible database abstraction
- Serializable exception handling for client-server communication
- Advanced data manipulation utilities with change tracking

#### Identified Design Elements

1. **Typed Data Architecture**: Implements strongly-typed representations of database tables with metadata, constraints, and relationships through TTypedDataSet and TTypedDataTable classes
2. **Database Operation Abstraction**: Automatically generates SQL statements (SELECT, INSERT, UPDATE, DELETE) based on typed data structures
3. **Remoting-Compatible Exception Handling**: Custom exception classes designed specifically for serialization across .NET Remoting boundaries
4. **Concurrency Management**: Built-in mechanisms for detecting and handling concurrent data modifications with detailed conflict information
5. **Cross-Platform Compatibility**: Specific accommodations for differences between Mono and Microsoft .NET implementations

#### Overview
The architecture emphasizes type safety through the use of generated typed datasets, provides a comprehensive database abstraction layer that handles SQL generation, and implements robust error handling for distributed operations. The data utilities offer extensive functionality for data manipulation, comparison, and conversion. This subproject forms the foundation of OpenPetra's data access strategy, enabling higher-level components to interact with the database through a consistent, strongly-typed interface while maintaining compatibility across different .NET implementations.

  *For additional detailed information, see the summary for csharp/ICT/Common/Data.*

### csharp/ICT/Common/Session

The program handles contact management, accounting, and sponsorship while supporting data export capabilities. This sub-project implements thread-safe session state management with database persistence along with concurrent access control mechanisms. This provides these capabilities to the OpenPetra program:

- Database-backed session storage instead of standard ASP.NET session handling
- Thread-safe session management with mutex-based concurrency control
- Context-independent session handling that works with or without HttpContext
- Session variable management with typed storage and retrieval

#### Identified Design Elements

1. **Thread-Static Session Management**: Uses thread-static variables to maintain session state per thread, allowing for clean separation of concerns across concurrent operations
2. **Database Persistence**: Sessions are stored in a database rather than in-memory, enabling session continuity across application restarts
3. **Mutex-Based Concurrency Control**: Implements mutex locking to prevent deadlocks during parallel session operations
4. **Context-Independent Architecture**: Session functionality works consistently regardless of whether it's accessed from web or non-web contexts

#### Overview
The architecture emphasizes reliability through database persistence, scalability through thread-safe operations, and flexibility by decoupling from ASP.NET's standard session handling. The session management system provides a consistent interface for storing and retrieving session variables while handling session lifecycle events like initialization, refresh, and cleanup of expired sessions. This approach enables OpenPetra to maintain state across both web and non-web components of the application.

  *For additional detailed information, see the summary for csharp/ICT/Common/Session.*

## Business Functions

### Authentication and Security
- `PasswordHashingSchemeV4.cs` : Implements a secure password hashing scheme using the Scrypt key stretching algorithm through libsodium-net.
- `PasswordHashingSchemeV1.cs` : Password hashing implementation using Scrypt algorithm with a known weakness in salt generation
- `PasswordHelper.cs` : Password security utility providing hashing schemes and secure password generation for OpenPetra.
- `PasswordHashingSchemeV2.cs` : Implements a secure password hashing scheme using the Scrypt key stretching algorithm via libsodium-net.
- `PasswordHashingSchemeV3.cs` : Implements a secure password hashing scheme using the Scrypt key stretching algorithm via libsodium-net.
- `Exceptions.cs` : Defines custom exception classes for OpenPetra's security and authentication system.

### Error Handling
- `Exceptions.Remoted.cs` : Defines a hierarchy of exception classes for OpenPetra's client-server communication via .NET Remoting.
- `ErrorCodes.cs` : Defines standard error codes for the OpenPetra application with descriptive attributes for error handling and display.
- `ErrorCodesHelper.cs` : Helper class for managing error codes, providing methods to retrieve error information and messages in OpenPetra.
- `ErrorCodeInventory.cs` : Manages error code inventory for OpenPetra, providing registration, lookup and cataloging of application error codes.

### Configuration Management
- `ServerSettings.cs` : Server settings management class for OpenPetra that handles configuration, system information, and runtime environment details.
- `CustomAppSettings.cs` : Manages application settings from configuration files and command line parameters for OpenPetra applications.

### Utilities and Helpers
- `Types.cs` : Defines common enumerations, types, and utility classes for OpenPetra's cross-platform functionality.
- `ArrayList.cs` : A wrapper around System.Collections.ArrayList that allows indexing beyond the existing count of objects.
- `FileVersion.cs` : Manages file version information with comparison and formatting capabilities for OpenPetra applications.
- `NumberToWords.cs` : Utility class for converting currency amounts to words in different languages.
- `Gettext.cs` : Provides internationalization support through GNU Gettext for OpenPetra's multilingual interface.
- `CommandLineProcessing.cs` : Command line argument processing utility for OpenPetra applications
- `Helper.Numeric.cs` : Helper class for numeric operations, specifically calculating decimal places in OpenPetra numeric formats.
- `StringHelper.cs` : Provides string manipulation utilities for OpenPetra including CSV parsing, formatting, and currency handling.
- `Utilities.cs` : Provides general utility functions for ICT applications in OpenPetra, including OS detection, runtime environment identification, and method information retrieval.
- `WindowHandling.cs` : Windows API wrapper for window management functions in OpenPetra.
- `Variant.cs` : Implements a variant data type class that supports multiple data types and conversions between them.
- `Helper.Threads.cs` : Helper class providing thread identification utilities for OpenPetra's multithreading operations.
- `Networking.cs` : Provides networking utilities for determining computer network configuration in OpenPetra applications.

### Logging
- `Logging.cs` : Implements a comprehensive logging system for OpenPetra with multiple output destinations and debug levels.
- `LogWriter.cs` : Logging utility class that writes messages to log files with timestamps and optional prefixes, supporting log rotation.

### Web Infrastructure
- `Interfaces.cs` : Defines the IOPWebServerManagerActions interface for OpenPetra web server management.

## Files
### ArrayList.cs

TSelfExpandingArrayList implements a wrapper class around System.Collections.ArrayList that automatically expands the array when accessing or setting elements beyond its current count. It overrides the indexer property to add null values when accessing indices greater than the current count, ensuring no index out-of-range exceptions occur. The class also provides a Compact() method to remove null elements, reducing the array size. This utility class simplifies working with dynamic arrays where indices might be sparse or assigned out of order, as demonstrated in the included example code.

 **Code Landmarks**
- `Line 71`: Overrides the indexer to automatically expand the array with null values when accessing beyond current bounds
- `Line 94`: Compact() method efficiently removes null elements, optimizing memory usage after sparse assignments
### CommandLineProcessing.cs

TCmdOpts implements a command line parameter processing class for OpenPetra applications. It parses command line arguments in the format -flag:value, storing them in a StringCollection and a Dictionary for fast lookup. The class provides methods to check if flags are set (IsFlagSet), retrieve parameter values (GetOptValue), and get available option keys (GetOptKeys). It handles special cases like parameters with spaces and empty values. The constructor automatically processes command line arguments from Environment.GetCommandLineArgs() and populates the internal collections for subsequent queries.

 **Code Landmarks**
- `Line 76`: Handles space after colon to allow automatic tab expansion for filenames on command line
- `Line 81`: Special handling to avoid splitting path names that would otherwise require quotes
- `Line 93`: Builds dictionary of options for fast lookup by splitting on dash and colon characters
### CustomAppSettings.cs

TAppSettingsManager implements a utility class for reading application settings from configuration files and command line parameters in OpenPetra. It provides methods to access configuration values with type conversion (string, integer, boolean, etc.) and supports default values when settings are missing. The class handles file paths with special placeholders and maintains application directory information. Key functionality includes checking for parameter existence, retrieving values with appropriate type conversion, and supporting command-line overrides of config file settings. Important methods include GetValue(), GetBoolean(), GetInt32(), HasValue(), and GetKeys().

 **Code Landmarks**
- `Line 57`: Uses ThreadStatic attribute to ensure configuration settings are thread-safe
- `Line 84`: Intelligently determines application directory with fallback mechanisms
- `Line 112`: Implements ApplicationBaseDirectory property that handles development vs production environments
- `Line 307`: Supports dynamic placeholder substitution in configuration values
- `Line 316`: Auto-detects instance name from directory structure for certain configuration parameters
### ErrorCodeInventory.cs

ErrorCodeInventory implements a static class that manages OpenPetra's error code system. It maintains a catalog of error codes from registered types, providing functionality to build an inventory of error codes, retrieve error code information, and check if types are catalogued. The class uses thread-static collections to store error codes and their metadata. Key methods include Init() to initialize static variables, RetrieveErrCodeInfo() to look up error code details, BuildErrorCodeInventory() to populate the catalog from a type's constants, and ListRegisteredTypes() to enumerate registered error code sources. The file also defines EDuplicateErrorCodeException for handling duplicate error code scenarios.

 **Code Landmarks**
- `Line 43`: Uses ThreadStatic attribute to ensure thread safety for static collections in web environment
- `Line 65`: Init() method ensures clean state for each web request by resetting static variables
- `Line 106`: Handles anonymous delegates by detecting special naming patterns in stack trace
- `Line 193`: Dynamically builds error code inventory by reflecting on public constants with ERR_ prefix
### ErrorCodes.cs

CommonErrorCodes implements a centralized inventory of application-independent error codes used throughout OpenPetra. It defines string constants with unique identifiers following a structured format (GENC.#####X) that represent various validation and system errors. Each error code is decorated with ErrCodeAttribute providing error message text, titles, and full descriptions. The class includes error codes for date validation, numeric validation, string validation, database operations, and configuration issues. These standardized codes help support staff identify errors regardless of the user interface language.

 **Code Landmarks**
- `Line 43`: Error codes follow a structured format: 'GENC' prefix, period, 5-digit number, and error type indicator (V/N/E)
- `Line 35`: Class includes documentation explaining how error codes help support staff identify issues across different languages
- `Line 63`: ErrCodeAttribute decoration pattern provides rich metadata for each error code including descriptions and message text
### ErrorCodesHelper.cs

ErrorCodesHelper.cs implements helper methods for working with error codes in the OpenPetra system. It provides functionality to retrieve error information, messages, descriptions, and categories through the ErrorCodes class. The file defines three key classes: ErrorCodes (containing helper methods), ErrCodeInfo (storing information about specific error codes), and ErrCodeAttribute (for adding information to error code constants). It also defines the ErrCodeCategory enum for categorizing errors and EErrorCodeNotRegisteredException for handling unregistered error codes. Important methods include GetErrorInfo(), GetErrorText(), GetErrorDescription(), and GetErrorHelpID().

 **Code Landmarks**
- `Line 44`: GetErrorInfo method throws EErrorCodeNotRegisteredException when error codes aren't found in registered types
- `Line 105`: String formatting with placeholders allows dynamic error message construction with context-specific information
- `Line 294`: Internationalization support through Catalog.GetString for error messages
### Exceptions.Remoted.cs

Exceptions.Remoted.cs implements a comprehensive hierarchy of serializable exception classes that can be passed between server and client via .NET Remoting in the OpenPetra system. The file defines EOPException as the base class for all OpenPetra-specific exceptions, with specialized subclasses for application-level exceptions (EOPAppException), database access exceptions (EOPDBException), session exceptions (EOPDBSessionException), security exceptions (ESecurityAccessDeniedException), and finance system exceptions (EFinanceSystemUnexpectedStateException). Each exception class implements proper serialization support through GetObjectData and serialization constructors to ensure exceptions can be properly transmitted across remoting boundaries. The file organizes exceptions by functional area, including database table access, security violations, cached data operations, and finance system state issues.

 **Code Landmarks**
- `Line 47`: Base class EOPException requires all OpenPetra exceptions to derive from it, ensuring consistent exception handling throughout the system
- `Line 84`: All exception classes implement serialization support via GetObjectData method to enable proper transmission across remoting boundaries
- `Line 1074`: Finance system exceptions form their own hierarchy to handle specialized error conditions in financial operations
### Exceptions.cs

Exceptions.cs implements a comprehensive set of custom exception classes for OpenPetra's security and authentication system. The file defines specialized exceptions for various security scenarios including EPetraSecurityException as the base class, with derived exceptions for specific cases like EUserNotExistantException, EPasswordWrongException, EUserRetiredException, EUserAccountLockedException, EAccessDeniedException, and ESystemDisabledException. Each exception class includes standard constructors and serialization support for .NET remoting. The file also contains the TExceptionHelper utility class that provides methods for detecting database connection issues and displaying appropriate error messages to users.

 **Code Landmarks**
- `Line 36`: EPetraSecurityException serves as the base class for all security-related exceptions in the system
- `Line 697`: TExceptionHelper class provides utilities to detect and handle database connection failures
- `Line 710`: EXCEPTION_DATA_DBUNAVAILABLE constant is used to mark exceptions caused by database unavailability
- `Line 731`: IsExceptionCausedByUnavailableDBConnectionClientSide method traverses the full exception hierarchy to check for database issues
- `Line 764`: IsExceptionCausedByUnavailableDBConnectionServerSide specifically handles PostgreSQL connection failures
### FileVersion.cs

TFileVersionInfo implements a class for handling file version information in the OpenPetra system. It stores version numbers in major.minor.build.private format and provides multiple constructors to initialize from different source formats. Key functionality includes version comparison (with and without private part), string formatting, and retrieving the application version from various sources. The class supports converting between different version formats and includes methods like Compare(), ToString(), ToStringDotsHyphen(), and the static GetApplicationVersion() which checks for version files before falling back to assembly version information.

 **Code Landmarks**
- `Line 217`: GetApplicationVersion() implements a hierarchical version detection system, checking pkg_version.txt, version.txt, and assembly version as fallbacks
- `Line 57`: Constructor supports both dot and hyphen notation (2.2.35-99 or 2.2.35.99) for version parsing
- `Line 153`: Compare() method implements a cascading comparison algorithm for semantic versioning
### Gettext.cs

Catalog implements internationalization support for OpenPetra using GNU Gettext. It manages language and culture settings for the application, allowing translation of UI strings. Key functionality includes initializing the resource manager with default or specific languages, setting culture information, and retrieving translated strings with plural form support. Important methods include Init(), SetCulture(), SetLanguage(), GetString(), and GetPluralString(). The class uses Thread.CurrentThread.CurrentUICulture and CurrentCulture to manage localization settings, though actual translation functionality is conditionally compiled with USE_GETTEXT_CATALOG.

 **Code Landmarks**
- `Line 42`: Conditional compilation flag USE_GETTEXT_CATALOG controls whether actual translation occurs or just returns original strings
- `Line 78`: Handles special case for 'en-EN' language code by converting it to 'en-GB'
- `Line 174`: GetPluralString handles complex plural form selection based on number and language rules
### Helper.Numeric.cs

THelperNumeric implements a utility class for numeric operations in the OpenPetra system. The class provides functionality for parsing numeric format strings to determine the number of decimal places they contain. The main method, CalcNumericFormatDecimalPlaces, takes a numeric format string as input, locates the decimal point position, and returns the count of characters after the decimal point as an integer. If no decimal point exists or it's at the end of the string, the method returns zero. This functionality appears to support OpenPetra's financial and data processing requirements.

 **Code Landmarks**
- `Line 39`: The method references 'x_dp.p' suggesting compatibility with a legacy format or specification
### Helper.Threads.cs

ThreadingHelper implements static methods for thread identification in OpenPetra's multithreading operations. The class provides functionality to generate human-readable thread identifiers that combine thread names with their managed thread IDs. The two key methods are GetCurrentThreadIdentifier(), which returns an identifier for the current thread, and GetThreadIdentifier(Thread), which generates an identifier for any specified thread. The class ensures consistent formatting of thread identifiers by adding apostrophes around thread names and appending thread IDs in a standardized format.

 **Code Landmarks**
- `Line 55`: Handles null thread references gracefully by returning a descriptive message instead of throwing an exception
- `Line 64`: Ensures consistent formatting of thread identifiers with apostrophes and thread IDs
### Interfaces.cs

This file defines the IOPWebServerManagerActions interface used by the OpenPetra Web Server. The interface provides methods for managing web server operations, including shutdown, starting and stopping all web site servers, and checking server status. Key methods include ManagerShutdown which handles shutdown requests (optionally for a specific port), ManagerStartAll and ManagerStopAll for controlling all web site servers, and IsServerStarted which reports the current server status. This interface facilitates communication between the OpenPetra Web Server and its manager component.
### LogWriter.cs

TLogWriter implements a logging framework that writes messages to specified log files with date/time stamps and optional prefixes. It manages log file rotation based on dates, keeping a configurable number of historical log files. Key functionality includes writing log messages, rotating log files, checking if rotation is needed, and managing thread-safe logging operations. Important methods include Log() for writing messages, RotateFiles() for log rotation, and NeedToRotateFiles() for determining when to rotate. The class uses thread-static variables to maintain state across different threads and provides properties to configure logging behavior such as LogtextPrefix and SuppressDateAndTime.

 **Code Landmarks**
- `Line 39`: Uses ThreadStatic attributes to maintain separate logging state for each thread
- `Line 132`: Implements log file rotation that preserves historical logs with numbered suffixes
- `Line 188`: Thread synchronization with lock object to ensure thread-safe logging
- `Line 190`: Conditional formatting based on debug level for different log message formats
- `Line 217`: Daily log rotation check to create new log files each day
### Logging.cs

TLogging implements a flexible logging system for OpenPetra that supports writing messages to console, log files, and status bars. It provides methods for logging messages with different debug levels, context information, and exception details. The class manages log file creation, supports logging to string buffers for Windows Forms applications, and includes utilities for stack trace logging. Key functionality includes configurable output destinations, debug level filtering, context tracking, and exception handling. Important elements include the TLoggingType enum, Log methods, LogAtLevel methods, and constants defining debug levels.

 **Code Landmarks**
- `Line 74`: Uses ThreadStatic attribute to ensure thread-safety for logging variables in multi-threaded environments
- `Line 219`: ResetStaticVariables method enables proper cleanup between web requests to prevent memory leaks
- `Line 370`: Implements dynamic truncation of log strings to prevent memory issues in long-running applications
- `Line 302`: Handles console logging failures gracefully by catching NotSupportedException when console isn't available
### Networking.cs

Networking implements a utility class that contains general networking procedures for ICT Applications. The primary functionality is determining the network configuration of the computer where the code is executed. The class provides a single static method, DetermineNetworkConfig, which retrieves the computer's hostname and all associated IP addresses. The method outputs the computer name as a string and concatenates all IP addresses into a semicolon-separated string. It also checks application settings for a manually configured IP address that might be needed for virtual servers where IP forwarding is used.

 **Code Landmarks**
- `Line 55`: Overrides automatically detected IP addresses with a manually configured one from application settings when running on virtual servers with iptables forwarding
### NumberToWords.cs

NumberToWords implements a utility class that converts numerical currency values to their written word form in different languages. It currently supports English and German languages, determining which to use based on the current culture. The class provides methods to convert integers and decimals to words, handling singular and plural forms of currency units. Key functions include AmountToWords (public method for converting currency values) and language-specific internal methods AmountToWordsInternalDE and AmountToWordsInternalUK. The class uses arrays of string constants for number words in each language.

 **Code Landmarks**
- `Line 45`: Language-specific arrays for German number words
- `Line 106`: Language-specific arrays for English number words
- `Line 189`: Culture detection to determine which language implementation to use
### PasswordHashingSchemeV1.cs

TPasswordHashingScheme_V1 implements a password hashing scheme using the Scrypt key stretching algorithm through the libsodium-net library. This class is marked as deprecated due to a weakness in salt generation that reduced byte value representations from 256 to 128 when converting to ASCII. It provides methods for generating secure random passwords, salts, and password hashes. Key methods include GetNewPasswordSaltAndHash, GetNewPasswordSalt, GetNewPasswordSaltString, and GetPasswordHash. The class implements the IPasswordHashingScheme interface and is designed to be automatically migrated to a newer version when users log in.

 **Code Landmarks**
- `Line 33`: Class is explicitly marked as deprecated due to a security weakness in salt generation
- `Line 38`: Class naming convention is important as it's evaluated by TPasswordHelper.GetPasswordSchemeVersionNumber
- `Line 97`: Warning that changing salt generation or hash strength parameters would invalidate all existing passwords
### PasswordHashingSchemeV2.cs

TPasswordHashingScheme_V2 implements a secure password hashing mechanism using the Scrypt key stretching algorithm through the libsodium-net library. It generates salted password hashes with medium strength security. The class implements the IPasswordHashingScheme interface with three key methods: GetNewPasswordSaltAndHash for generating random passwords with corresponding salt and hash, GetNewPasswordSalt for creating new salt values, and GetPasswordHash for producing password hashes using the Scrypt algorithm. The implementation is version-specific, with the class name ending in '_V2' for version identification by the TPasswordHelper class.

 **Code Landmarks**
- `Line 44`: Class name must end with '_V' followed by version number for proper evaluation by TPasswordHelper.
- `Line 75`: Changing salt generation or password hash strength would invalidate all existing passwords, requiring system-wide password resets.
- `Line 85`: Uses Scrypt algorithm with medium strength setting as a balance between security and performance.
### PasswordHashingSchemeV3.cs

TPasswordHashingScheme_V3 implements a password hashing scheme that extends TPasswordHashingScheme_V2 to provide enhanced security through the Scrypt key stretching algorithm. The class generates salted password hashes with medium-slow strength settings via the libsodium-net library. It overrides the GetPasswordHash method to convert passwords into secure hashes using Unicode encoding and the Scrypt algorithm. The implementation warns that changing salt generation or hash strength parameters would invalidate existing passwords, requiring a password reset process for all users.

 **Code Landmarks**
- `Line 36`: Class naming convention enforces version numbering that's evaluated by TPasswordHelper.GetPasswordSchemeVersionNumber
- `Line 47`: Implementation uses PasswordHash.Strength.MediumSlow setting which balances security and performance
- `Line 58`: Password is encoded as Unicode before hashing, ensuring proper handling of international characters
### PasswordHashingSchemeV4.cs

TPasswordHashingScheme_V4 implements a password hashing scheme that extends TPasswordHashingScheme_V2 to provide enhanced security through the Scrypt key stretching algorithm. The class generates salted password hashes using the libsodium-net library with a security strength set to 'Sensitive'. It overrides the GetPasswordHash method to convert passwords to Unicode bytes before applying the Scrypt algorithm and returning the result as a Base64 string. The implementation is designed to be compatible with OpenPetra's password management system, with the class name ending in '_V4' for version identification.

 **Code Landmarks**
- `Line 32`: Class naming convention is critical for version identification in the password system
- `Line 45`: Warning that changing salt generation or hash strength parameters would invalidate all existing user passwords
- `Line 60`: Uses PasswordHash.Strength.Sensitive setting for high security in the Scrypt algorithm
### PasswordHelper.cs

PasswordHelper implements password security functionality for OpenPetra through multiple hashing scheme versions. It defines the IPasswordHashingScheme interface that requires implementations to generate secure passwords, salts, and password hashes. The static TPasswordHelper class provides factory methods to create instances of different password hashing schemes (V1-V4), with V2 being the current default. Key functionality includes generating random secure passwords with sufficient entropy, creating random tokens for password resets, and comparing byte arrays in a way that prevents timing attacks. Important elements include the CurrentPasswordScheme property, GetPasswordSchemeHelperForVersion method, and GetRandomSecurePassword method.

 **Code Landmarks**
- `Line 103`: Factory pattern implementation for password hashing schemes, allowing version selection while maintaining a consistent interface
- `Line 171`: Password generation with entropy requirements (>91) to ensure sufficient security against brute force attacks
- `Line 233`: Implementation of timing-attack resistant byte array comparison to prevent security vulnerabilities
### ServerSettings.cs

TSrvSetting implements a class for storing and accessing server configuration settings in OpenPetra. It provides read-only access to server parameters gathered from command line, configuration files, and runtime environment. Key functionality includes retrieving database type, host information, IP addresses, port settings, application version, and operating system details. The class uses properties with static getters that instantiate the class to access private fields. Important properties include ConfigurationFile, RDMBSType, HostName, HostIPAddresses, IPBasePort, ServerLogFile, and ApplicationBinFolder.

 **Code Landmarks**
- `Line 45`: Singleton-like pattern where static properties instantiate the class to access private fields
- `Line 134`: Uses TAppSettingsManager to retrieve configuration values with default fallbacks
- `Line 156`: Handles conditional log file path determination based on existing settings
### StringHelper.cs

StringHelper implements a comprehensive collection of string utility functions for the OpenPetra application. It provides methods for manipulating strings, parsing CSV data, formatting currencies, and handling dates. Key functionality includes splitting and merging strings with delimiters, CSV parsing with GetNextCSV methods, currency formatting with various international formats, and date handling with localization support. The class also offers utilities for partner key formatting, case conversion, quote handling, and string comparison. Important methods include StrSplit, GetNextCSV, FormatCurrency, DateToLocalizedString, and UpperCamelCase. The file also defines helper classes like CommonTagString and CommonJoinString for specific string constants used throughout the application.

 **Code Landmarks**
- `Line 173`: CSV parsing with support for quoted text and escaped characters
- `Line 435`: GetCSVSeparator intelligently determines the separator character in CSV data
- `Line 1031`: FormatCurrency handles complex currency formatting with international support
- `Line 1547`: LooksLikeFloat detects ambiguous decimal separators in different cultures
- `Line 1653`: LooksLikeAmbiguousShortDate handles date parsing across different cultural formats
### Types.cs

Types.cs defines fundamental enumerations, data structures, and utility classes used throughout the OpenPetra application. It includes enumerations for runtime environments (TExecutingCLREnum), operating systems (TExecutingOSEnum), database types (TDBType), data modes (TDataModeEnum), and connection types (TClientServerConnectionType). The file implements several utility classes including TSearchCriteria for SQL queries, THyperLinkHandling for managing different types of hyperlinks, CommonTypes for enum conversions, TSaveConvert for safe date conversions, and TProgressState for tracking long-running operations. It also defines custom attributes like NoRemotingAttribute and ResourceStringAttribute for code generation and internationalization support.

 **Code Landmarks**
- `Line 76`: Comprehensive OS detection enumeration that distinguishes between different Windows versions for platform-specific functionality
- `Line 121`: TSearchCriteria class provides a simple but flexible structure for building SQL query conditions
- `Line 142`: THyperLinkHandling implements a robust system for handling various hyperlink protocols with constants and parsing
- `Line 399`: TSaveConvert provides null-safe conversion methods for dates, addressing common issues with database null values
- `Line 553`: Custom ResourceStringAttribute supports internationalization by marking strings for translation
### Utilities.cs

Utilities implements a class containing general utility functions for ICT applications that don't fit elsewhere in the Ict.Common namespace. It provides methods for determining the operating system (DetermineExecutingOS) and Common Language Runtime environment (DetermineExecutingCLR), retrieving current time in a formatted string (CurrentTime), and obtaining information about calling methods (GetMethodName, GetMethodSignature). The class also includes utilities for thread and AppDomain information (GetThreadAndAppDomainCallInfo) and array manipulation (AddToArray). Key constants include StrThreadAndAppDomainCallInfo for formatting thread information. The file supports cross-platform functionality by detecting different operating systems and runtime environments.

 **Code Landmarks**
- `Line 71`: OS detection logic that handles various Windows versions and Linux platforms
- `Line 121`: Runtime environment detection that identifies Microsoft .NET, Mono, or other CLR implementations
- `Line 171`: Method signature extraction using reflection for debugging and logging purposes
- `Line 235`: Generic array extension method that creates a new array with added elements
### Variant.cs

TVariant implements a versatile data type container that can store and convert between different data types including strings, integers, decimals, booleans, dates, and composite values. It provides functionality for type conversion, formatting, comparison, and serialization. The class supports operations like assigning values, adding values to composites, encoding/decoding to strings, and formatted output for display or export. Key methods include ToInt(), ToDecimal(), ToString(), ToDate(), ToFormattedString(), and IsZeroOrNull(). The class also implements ISerializable to support .NET remoting.

 **Code Landmarks**
- `Line 104`: Implements a variant type system similar to dynamic languages but with explicit type tracking
- `Line 372`: Sophisticated date parsing that handles multiple ISO formats including those from SQLite and OpenDocument
- `Line 492`: Custom serialization implementation that encodes variants as strings to avoid .NET remoting issues
- `Line 692`: Format string handling that supports specialized formatting like partner keys and composite values
### WindowHandling.cs

TWindowHandling implements a static utility class that provides cross-platform compatible wrappers for Windows API functions related to window management. It defines constants for window display states (SW_HIDE, SW_SHOWNORMAL, SW_SHOW, SW_RESTORE) and wraps two key Windows API functions: ShowWindow and SetForegroundWindow. The wrappers (ShowWindowWrapper and SetForegroundWindowWrapper) handle exceptions that might occur on non-Windows platforms like Linux, preventing application crashes. Each method takes a window handle (IntPtr) as input and returns a Boolean indicating success or failure of the operation.

 **Code Landmarks**
- `Line 64`: Exception handling to ensure cross-platform compatibility by preventing crashes on Linux
- `Line 82`: Similar cross-platform exception handling pattern repeated for consistency across API wrappers

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #