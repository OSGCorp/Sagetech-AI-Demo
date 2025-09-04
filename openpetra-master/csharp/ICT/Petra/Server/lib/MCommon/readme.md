# C# Common Module Of OpenPetra

The OpenPetra C# Common Module provides core cross-cutting functionality that supports the entire application ecosystem. This module implements foundational services and utilities that enable data management, validation, caching, and client-server communication. The Common Module serves as the backbone for OpenPetra's modular architecture, providing these essential capabilities:

- Database sequence management for generating unique identifiers
- Cacheable data table handling with validation
- Progress tracking for long-running operations
- Form template management across different modules
- Data extraction and filtering capabilities
- Office-specific data label customization

## Identified Design Elements

1. **Layered Architecture**: The module is organized into distinct layers including Data Layer, Integration Layer, and Cross-Cutting Concerns, providing clear separation of responsibilities.

2. **Validation Framework**: Comprehensive validation system with specialized validators for different data types and business rules, ensuring data integrity throughout the application.

3. **Caching Mechanism**: Efficient data caching implementation that reduces database load while maintaining data consistency through validation before persistence.

4. **Progress Tracking**: Client-server communication system for monitoring long-running processes, allowing users to track operation status and cancel if needed.

5. **Extensible Data Processing**: The ProcessDataChecks system enables automated data consistency verification through configurable SQL-based rules with reporting capabilities.

## Overview

The architecture emphasizes reusability through shared utilities and validation logic, maintains data integrity via comprehensive validation, and provides efficient client-server communication patterns. The module serves as a foundation for other OpenPetra components, offering essential services like sequence generation, caching, and progress tracking that enable the application's non-profit management capabilities while ensuring data consistency and reliability.

## Business Functions

### Data Validation
- `validation/ControlHelper.cs` : Helper class providing validation functions for controls in the OpenPetra server environment.
- `validation/Cacheable.Validation.cs` : Validation module for cacheable data tables in OpenPetra's MCommon module.
- `validation/Common.Validation.cs` : Validation utility for MCommon DataTables in OpenPetra, focusing on international postal code validation.
- `validation/Helper.cs` : Helper class for data validation in OpenPetra's server-side validation operations.

### Data Processing
- `processing/ProcessDataChecks.cs` : Implements automated data consistency checks for OpenPetra database with email notifications for administrators and responsible users.
- `queries/ExtractBase.cs` : Base class for server-side extract queries that filters partners based on various criteria.

### Web Connectors
- `web/FormTemplates.cs` : Server-side web connector for managing form templates in OpenPetra, supporting upload/download of document templates for various modules.
- `OfficeSpecificDataLabels.cs` : Server-side connector for managing office-specific data labels in OpenPetra, handling data retrieval and saving for partner and application records.
- `ProgressTracker.cs` : Provides a web connector for tracking long-running procedures with progress reporting capabilities.

### Data Caching
- `Cacheable.Validation.cs` : Server-side validation for cacheable data tables in OpenPetra's common module.
- `Cacheable.ManualCode.cs` : Server-side implementation for caching common database tables for client use

### Core Utilities
- `Sequences.cs` : Provides a server-side connector for retrieving sequential numeric values from the database.
- `Main.cs` : Server-side utility classes for common operations across OpenPetra modules.

## Files
### Cacheable.ManualCode.cs

TCacheable implements server-side functionality for retrieving cacheable DataTables from the MCommon namespace that can be stored on the client side. The class provides methods to fetch complete database tables with all columns and rows. It includes three main methods: a basic GetCacheableTable that takes only the table enum parameter, an overloaded version that returns the DataTable type, and a partial implementation suggesting additional functionality in other files. The class works with the TCacheableCommonTablesEnum to identify which tables can be cached.

 **Code Landmarks**
- `Line 42`: This class is marked as partial, indicating implementation is split across multiple files
- `Line 69`: Method can be used with Delegate TGetCacheableDataTableFromCache, suggesting a callback pattern for cache retrieval
### Cacheable.Validation.cs

TCacheable implements validation functionality for cacheable data tables in OpenPetra's server-side common module. This partial class contains methods specifically for validating cached data before it's committed to the database. The key functionality is the ValidateCountryListManual method, which iterates through rows in a submitted country data table and validates each row using the ValidateCountrySetupManual method from TValidation_CacheableDataTables. The validation process generates error messages that identify the specific row where validation issues occur, helping administrators identify and fix data problems.

 **Code Landmarks**
- `Line 32`: Uses partial class implementation to separate validation logic from other cacheable functionality
- `Line 39`: Validation errors include row numbers to help pinpoint exactly where data issues occur
### Main.cs

MCommonMain provides utility functions for Petra Server that are shared across modules. It includes methods to retrieve partner information like ShortName, PartnerClass, and PartnerStatus, and to check partner existence. The file also implements TPagedDataSet, a class that executes SQL queries and returns results in pages, useful for find screens and handling large datasets. TDynamicSearchHelper assists with assembling ODBC parameters for dynamic searches, supporting various match types like BEGINS, ENDS, CONTAINS, and EXACT. TReportingDbAdapter provides functionality for database queries with FastReports, including connection management and query execution with optional parameters.

 **Code Landmarks**
- `Line 73`: RetrievePartnerShortName provides efficient partner data retrieval with minimal database columns
- `Line 243`: TPagedDataSet implements asynchronous query execution in separate threads with progress tracking
- `Line 371`: ExecuteFullQuery handles database connection management with proper transaction isolation
- `Line 454`: GetData method implements pagination logic with error handling for invalid page requests
- `Line 722`: TReportingDbAdapter implements exception handling that prevents server crashes from unhandled thread exceptions
### OfficeSpecificDataLabels.cs

TOfficeSpecificDataLabelsUIConnector implements a server-side connector for managing office-specific data labels in OpenPetra. It handles retrieval and saving of custom data fields for different partner types (person, family, organization) and applications. The class provides functionality to load data labels based on partner keys or application keys, determine if data rows are obsolete, prepare changes for saving, and retrieve partner information. Key methods include GetData(), PrepareChangesServerSide(), IsRowObsolete(), and CountLabelUse(). The connector works with typed datasets and handles different data types including character, numeric, date, boolean, and partner key values.

 **Code Landmarks**
- `Line 121`: IsRowObsolete method intelligently determines if a data row should be saved based on its content type and value
- `Line 202`: GetData method uses different data loading strategies based on the office-specific data label use enumeration
- `Line 272`: PrepareChangesServerSide performs cleanup by removing empty/null value rows before saving to conserve database space
### ProgressTracker.cs

TProgressTrackerWebConnector implements a web connector that allows clients to track the progress of long-running procedures. It provides methods for resetting the tracker, retrieving the current state of a job (including caption, status message, percentage done, and completion status), and canceling running jobs. The connector uses the TProgressTracker class to manage progress tracking and identifies clients using DomainManager.GClientID. All methods require USER module permission, ensuring only authenticated users can access tracking functionality.

 **Code Landmarks**
- `Line 43`: Uses RequireModulePermission attribute for security enforcement on all public methods
- `Line 54`: Returns progress state with multiple out parameters to provide comprehensive status information
- `Line 64`: Return value combines percentage and message status to indicate valid tracking data
### Sequences.cs

TSequenceWebConnector implements a server-side connector for managing database sequence values in OpenPetra. The class provides functionality to retrieve the next value in a specified sequence, which is essential for generating unique identifiers across the system. The GetNextSequence method accepts a sequence name enumeration and an optional database connection, creating a new connection if none is provided. It executes a database transaction to retrieve and return the next value in the specified sequence. The method is marked with [NoRemoting] attribute, indicating it's intended for internal server-side use only.

 **Code Landmarks**
- `Line 43`: Uses [NoRemoting] attribute to prevent this method from being exposed to remote clients
- `Line 46`: Implements a flexible database connection approach, allowing either reuse of existing connections or creation of new ones
- `Line 52`: Uses delegate pattern within transaction to encapsulate database operations
### processing/ProcessDataChecks.cs

TProcessDataChecks implements a server-side process that performs automated data consistency checks against the OpenPetra database. It executes SQL queries from predefined files to identify data inconsistencies, then generates reports and sends email notifications to both administrators and the users responsible for creating or modifying problematic records. Key functionality includes scheduling regular checks (configurable to run daily or weekly), executing SQL-based validation rules, generating Excel reports of errors, and sending targeted notifications. Important methods include Process(), CheckModule(), SendEmailToAdmin(), SendEmailForUser(), and SendEmailsPerUser(). The class uses constants like PROCESSDATACHECK_LAST_RUN and SENDREPORTFORDAYS_TOUSERS to control execution frequency and reporting timeframes.

 **Code Landmarks**
- `Line 53`: Uses system defaults to track last execution time to prevent excessive runs and email notifications
- `Line 79`: Dynamically loads SQL validation rules from files based on module prefix patterns
- `Line 93`: Modifies SQL queries to include audit fields (created/modified timestamps and users)
- `Line 154`: Generates Excel reports from error DataTables for email attachments
- `Line 196`: Filters errors by user and date range for targeted notification emails
### queries/ExtractBase.cs

ExtractQueryBase implements an abstract base class for server-side extract queries in OpenPetra. It provides functionality to filter partners based on various criteria including address information, postal codes, location types, and date ranges. The class handles SQL query construction, parameter processing, and database interactions to generate extracts containing filtered partner lists. Key methods include CalculateExtractInternal which executes queries and creates extracts, AddAddressFilter which extends queries with address-related conditions, and PostcodeFilter which filters results by postal code criteria. The class also provides helper methods like ComparePostcodes for comparing postal codes and PostcodeInRegion for region-based filtering. Derived classes must implement RetrieveParameters to handle extract-specific parameters.

 **Code Landmarks**
- `Line 63`: CalculateExtractInternal method creates extracts from SQL queries with parameter handling and transaction management
- `Line 151`: AddAddressFilter dynamically builds SQL queries with address filtering conditions based on user parameters
- `Line 359`: ComparePostcodes implements sophisticated universal postcode comparison logic handling mixed alphanumeric formats
- `Line 532`: Abstract method RetrieveParameters forces derived classes to implement parameter handling specific to each extract type
### validation/Cacheable.Validation.cs

TValidation_CacheableDataTables provides validation functions for various cacheable data tables in OpenPetra. The class implements validation methods for configuration screens across multiple modules including MCommon, MPartner, and MPersonnel. Each validation method checks specific business rules for different entity types such as countries, frequencies, acquisition codes, marital statuses, and various personnel-related data. Common validation patterns include checking for required fields, validating date fields when certain flags are set, ensuring numerical values are positive, and verifying relationships between values. The validation methods follow a consistent pattern of checking row state, validating specific fields, and adding verification results to a collection.

 **Code Landmarks**
- `Line 53`: Validation pattern checks for deleted rows first to avoid unnecessary processing
- `Line 77`: Uses Auto_Add_Or_AddOrRemove pattern for efficient verification result collection management
- `Line 185`: Complex validation ensures at least one time value is positive across multiple fields
- `Line 307`: Common pattern for validating unassignable dates when a flag is set
### validation/Common.Validation.cs

TCommonValidation provides validation functionality for MCommon DataTables in the OpenPetra server. The class implements a single static method, IsValidInternationalPostalCode, which verifies whether an international postal type code exists in the system database. The method accepts the code to validate, a description for error messages, and optional context parameters. It returns null for valid codes or a TVerificationResult object containing error details. The validation process involves querying the database to check if the provided code exists in the PInternationalPostalType table.

 **Code Landmarks**
- `Line 55`: Uses a database transaction with a delegate pattern for data access operations
- `Line 58`: Employs typed data tables for type-safe database operations
- `Line 61`: Uses DataTable.Rows.Find method for efficient lookup by primary key
### validation/ControlHelper.cs

TValidationControlHelper implements a static utility class that provides helper functions for shared validation of data specific to controls in the OpenPetra server environment. The class currently contains a single method, IsNotInvalidDate, which checks whether a given DateTime is invalid or undefined. This method wraps TDateChecks.IsNotUndefinedDateTime and returns a TVerificationResult object that contains details about any validation problems. The method supports handling null values and can be configured to treat null values as invalid dates.

 **Code Landmarks**
- `Line 40`: The method provides a wrapper around TDateChecks.IsNotUndefinedDateTime with additional context parameters for validation results
### validation/Helper.cs

TValidationHelper provides utility functions for shared validation of data in OpenPetra's server component. The class contains a single static method IsRowAddedOrFieldModified that determines if a DataRow has been newly added or if a specific field has been modified compared to its original value. This method helps validation processes determine when validation checks should be performed based on data state changes. It works by examining the row state and comparing original and current field values when appropriate.

 **Code Landmarks**
- `Line 42`: The method handles both newly added rows and modified rows with different logic paths
- `Line 51`: Uses TTypedDataAccess.GetSafeValue to safely retrieve values from different DataRow versions
### web/FormTemplates.cs

TFormTemplatesWebConnector provides server-side functionality for managing document templates in OpenPetra's Common module. It implements methods for uploading and downloading form templates for different modules (Finance, Partner, Personnel, Conference) with appropriate permission controls. The class handles template storage in the database, including metadata like upload timestamps and file extensions. Key functionality includes retrieving available templates for specific modules and supporting specialized features like Swiss office mail sorting. Important methods include UploadFinanceFormTemplate, DownloadPartnerFormTemplate, GetPersonnelForms, and GetCHDefinitionFiles.

 **Code Landmarks**
- `Line 49`: Permission-based access control using RequireModulePermission attribute for security enforcement
- `Line 146`: Direct SQL execution for template updates rather than using data access layer abstractions
- `Line 271`: Module-specific constants (FORM_CODE_*) used to differentiate template types across the system
- `Line 347`: Dynamic path resolution to locate configuration files based on installation directory structure
- `Line 367`: Support for specialized regional functionality (Swiss mail sorting) with external definition files

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #