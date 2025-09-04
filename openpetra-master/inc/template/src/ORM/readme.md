# Include Template ORM Source Subproject Of Petra

The Petra is a C# program that provides administrative management solutions for non-profit organizations. The program implements database access patterns and object-relational mapping to provide a robust data layer. This sub-project implements code generation templates for ORM classes along with caching mechanisms for frequently accessed data. This provides these capabilities to the Petra program:

- Strongly-typed data access through auto-generated classes
- Efficient data caching for frequently accessed but rarely changed tables
- Comprehensive CRUD operations with referential integrity enforcement
- Data validation before database operations

## Identified Design Elements

1. **Template-Based Code Generation**: Uses template files to generate strongly-typed data access classes, ensuring consistency across the ORM layer
2. **Caching Architecture**: Implements thread-static caching mechanisms for frequently accessed data tables to improve performance
3. **Cascading Operations**: Manages referential integrity through cascading delete verification and reference counting
4. **Type-Safe Data Access**: Generates strongly-typed properties and methods for database columns, providing compile-time type checking

## Overview
The architecture follows the Table Access Object pattern, providing a clean separation between database tables and business logic. The templates generate classes for tables, rows, datasets, and data access methods, ensuring consistent implementation across the system. The caching mechanism optimizes performance for frequently accessed data while maintaining data integrity. Validation classes enforce data integrity rules before database operations, and cascading operations ensure referential integrity is maintained across related tables. The generated code supports both traditional database access and web-based API interactions.

## Business Functions

### ORM Code Generation
- `DataTable.cs` : Auto-generated ORM code that creates typed DataTable classes for database tables in OpenPetra.
- `TableList.cs` : Template file for generating a class that provides ordered lists of database tables for OpenPetra.
- `Cacheable.Shared.cs` : Auto-generated ORM code defining cacheable table enumerations for the Petra system.
- `DataSetAccess.cs` : Template for generating ORM data access classes that handle database operations for OpenPetra datasets.
- `DataSet.cs` : Template file for generating typed DataSet classes in OpenPetra's ORM framework
- `DataCascading.cs` : Template for generating ORM data cascading classes that handle cascading delete and reference counting operations.

### Data Caching
- `Cacheable.Server.cs` : Template file for generating ORM cacheable data table server-side connectors in OpenPetra

### Data Access
- `DataAccess.cs` : Template file for auto-generating ORM data access classes for OpenPetra database tables.

### Data Validation
- `DataTableValidation.cs` : Auto-generated validation code for database tables in OpenPetra's ORM framework.

## Files
### Cacheable.Server.cs

This template file generates server-side code for managing cacheable data tables in OpenPetra's ORM system. It defines a T{#SUBMODULE}CacheableWebConnector class that provides methods for retrieving, refreshing, and saving cacheable data tables. The class implements a thread-static cache populator and methods to interact with database tables that are frequently accessed but rarely changed. The template supports both standard and ledger-specific cacheable tables with functionality for data validation, transaction management, and client notification when tables are updated. The file uses template placeholders like {#SUBNAMESPACE}, {#CACHEABLECLASS}, and {#LOADTABLESANDLISTS} that get replaced during code generation.

 **Code Landmarks**
- `Line 44`: Uses ThreadStatic attribute to ensure each thread has its own instance of the cache populator
- `Line 188`: Implements hash code comparison to determine if client needs updated data
- `Line 221`: Destructor logs lifetime of cache object for performance monitoring
- `Line 273`: Uses transaction isolation level ReadCommitted for cache population operations
### Cacheable.Shared.cs

This auto-generated file defines enumerations for cacheable tables in the Petra system's ORM layer. It creates TCacheable[SubModule]TablesEnum enumerations for different modules and submodules, providing a structured way to reference database tables that can be cached. The file uses template placeholders like {#MODULE}, {#SUBMODULE}, and {#ENUMELEMENTS} that get replaced during the build process. Each enum element includes a summary comment explaining its purpose.

 **Code Landmarks**
- `Line 2`: File is auto-generated with nant generateORM and warns against manual modifications
- `Line 12`: Uses template placeholders like {#MODULE} and {#SUBMODULE} that get replaced during build process
- `Line 16`: Creates module-specific enum types following naming pattern TCacheable{#SUBMODULE}TablesEnum
### DataAccess.cs

This template file is used to auto-generate C# data access classes for OpenPetra database tables. It defines a pattern for creating strongly-typed data access methods that follow the Table Access Object pattern. The generated classes provide comprehensive CRUD operations including loading records by primary/unique keys, using templates for filtering, counting records, and managing relationships between tables. The template includes specialized methods for handling foreign key relationships through direct references or link tables. Each generated class implements cascading delete verification to maintain referential integrity and provides methods for submitting changes to the database with proper user tracking.

 **Code Landmarks**
- `Line 1`: Auto-generated code template with warning not to modify manually
- `Line 241`: SubmitChanges method includes cascading delete verification to maintain referential integrity
- `Line 287`: AddOrModifyRecord implements upsert pattern with detailed logging of differences
- `Line 422`: LoadViaLinkTable methods handle many-to-many relationships through junction tables
- `Line 538`: Template uses placeholders like {#TABLENAME} that get replaced during code generation
### DataCascading.cs

DataCascading.cs is a template file for generating classes that handle cascading operations for database tables. It implements methods for cascading deletes and reference counting across related tables. The template defines DeleteByPrimaryKey, DeleteUsingTemplate, CountByPrimaryKey, and CountUsingTemplate methods that manage cascading operations through related tables. Each generated class extends TTypedDataAccess and provides functionality to delete records with their dependencies and count references before deletion. The template uses placeholders like {#TABLENAME} that get replaced during code generation with actual table names and relationships.

 **Code Landmarks**
- `Line 1`: Auto-generated template file that creates cascading delete and reference counting functionality for database tables
- `Line 30`: Implements cascading delete operations that handle related records in other tables before deleting the main record
- `Line 63`: Reference counting system that builds verification results to warn users about existing dependencies
- `Line 152`: Template sections use placeholders like {#TABLENAME} that get replaced during code generation
### DataSet.cs

DataSet.cs is a template file used to generate typed DataSet classes for OpenPetra's ORM framework. It defines the structure for creating strongly-typed dataset classes that represent database tables and their relationships. The template includes code for generating dataset classes with table properties, methods for handling changes, initializing tables, mapping relationships, and defining constraints. The generated classes inherit from TTypedDataSet and provide type-safe access to database tables and their data.

 **Code Landmarks**
- `Line 1`: Auto-generated template file that should not be modified manually
- `Line 12`: Uses template placeholders like {#NAMESPACE} and {#CONTENTDATASETSANDTABLESANDROWS} for code generation
- `Line 29`: Implements serializable typed datasets for database interaction
- `Line 53`: Provides typed change tracking with GetChangesTyped method
- `Line 95`: Uses template loops to generate table properties and relationship constraints
### DataSetAccess.cs

DataSetAccess.cs is a template file used to auto-generate ORM data access classes for OpenPetra. It implements functionality for submitting changes to the database through the SubmitChanges method, which handles insert, update, and delete operations within transactions. The template includes patterns for managing database connections, transaction handling, error logging, and sequence value updates across related tables. Key elements include the {#DATASETNAME}Access class, SubmitChanges method, transaction management logic, and templating variables that get replaced during code generation.

 **Code Landmarks**
- `Line 37`: Uses code generation templating with {#TAGS} that get replaced during build process
- `Line 54`: Implements transaction management with isolation level control for database operations
- `Line 86`: Contains detailed error handling with logging of operation context and stack traces
- `Line 114`: Handles sequence value synchronization between related tables with negative temporary IDs
### DataTable.cs

DataTable.cs is an auto-generated template file that creates strongly-typed DataTable classes for OpenPetra's ORM system. It generates table classes that inherit from a base class with properties and methods for each database column. The template creates both table classes with column definitions and row classes with typed properties. Key functionality includes primary key configuration, change tracking, default value initialization, and database metadata access. Important elements include the TableId static field, GetTableName/GetTableDBName methods, InitClass/InitVars methods for column setup, and typed row access methods like NewRowTyped().

 **Code Landmarks**
- `Line 1`: Auto-generated code with warning not to modify manually
- `Line 41`: Uses static initialization pattern to register table metadata with a central registry
- `Line 143`: Provides localization support for table labels via Catalog.GetString
- `Line 227`: Implements NULL value handling with specialized test and set methods for each column
### DataTableValidation.cs

DataTableValidation.cs is a template file that generates validation classes for database tables in OpenPetra's ORM framework. It creates validation methods for each table that check data integrity before database operations. The file implements two main validation methods: one for validating individual rows and another for validating entire tables. It includes specialized validation for different data types like strings, numbers, and dates, with checks for empty values, string length, number precision, and valid dates. The template uses conditional blocks to generate appropriate validation code based on table structure.

 **Code Landmarks**
- `Line 1`: Auto-generated file with warning not to modify manually
- `Line 27`: Template uses table loops to generate validation classes for multiple database tables
- `Line 58`: Skips validation for deleted or detached DataRows unless specific deletion validation is needed
- `Line 141`: Implements specialized validation snippets for different data types (strings, numbers, dates)
### TableList.cs

TTableList implements a utility class that provides ordered lists of database tables for the OpenPetra system. The class contains three static methods that return collections of database-related names: GetDBNames() returns tables ordered by constraint dependency (tables that others depend on first), GetDBSequenceNames() returns database sequence names, and GetDBNamesAvailableForCustomReport() returns tables available for custom reporting. The file is auto-generated from a template and contains placeholders ({#DBTableNames}, {#DBSequenceNames}, {#DBTableNamesAvailableForCustomReport}) that are populated during the build process.

 **Code Landmarks**
- `Line 2`: Auto-generated file with warning not to modify manually, indicating code generation is part of the build process
- `Line 23`: Tables are ordered by constraint dependency, which is crucial for proper database creation and deletion sequences
- `Line 27`: Placeholder {#DBTableNames} shows where generated code will be inserted during build

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #