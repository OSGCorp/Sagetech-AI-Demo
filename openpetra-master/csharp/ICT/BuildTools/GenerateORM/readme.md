# C# Generate ORM Subproject Of OpenPetra

The OpenPetra C# Generate ORM subproject implements a code generation system for the Object-Relational Mapping (ORM) layer that bridges the application's domain model with its database. This subproject automates the creation of strongly-typed C# classes from database schema definitions, eliminating boilerplate code and ensuring consistency across the data access layer. The code generator processes XML and YAML definitions to produce type-safe database access components that support OpenPetra's non-profit management features.

Key technical capabilities include:

- Strongly-typed dataset and table class generation
- Automated data validation code generation based on schema constraints
- CRUD operation code generation with parameterized SQL
- Relationship-aware data access methods (foreign keys, bridge tables)
- Cascading operation support for maintaining referential integrity
- Cached table generation for performance optimization
- Topologically sorted table lists based on dependencies

## Identified Design Elements

1. **Template-Based Code Generation**: Uses templates to ensure consistency across generated code files
2. **Schema-Driven Development**: Derives C# classes directly from database definitions in petra.xml
3. **Relationship-Aware Code**: Automatically generates LoadVia methods based on foreign key relationships
4. **Type Safety**: Creates strongly-typed properties and methods matching database column definitions
5. **Validation Integration**: Generates constraint validation code based on database schema rules
6. **Modular Architecture**: Separates generation concerns into specialized classes (tables, datasets, validation, etc.)

## Architecture Overview

The ORM generator is structured around specialized code generation classes, each responsible for a specific aspect of the data access layer. The system reads database definitions and produces a comprehensive set of C# files that handle all database interactions. The architecture emphasizes type safety, performance through caching, and maintainability through consistent patterns. This generated code forms the foundation of OpenPetra's data access strategy, enabling the application's features like contact management, accounting, and sponsorship tracking.

## Business Functions

### ORM Code Generation
- `codeGenerationDatasetAccess.cs` : Generates code for accessing and manipulating typed datasets in the OpenPetra ORM framework.
- `codeGenerationTableValidation.cs` : Generates validation code for database tables in OpenPetra's ORM layer to enforce data integrity constraints.
- `codeGenerationDataset.cs` : Code generator for typed datasets in OpenPetra ORM framework, creating C# classes from XML definitions.
- `codeGenerationTable.cs` : Code generator for typed tables and rows in OpenPetra's ORM system.
- `codeGenerationAccess.cs` : Generates ORM data access code for database operations in OpenPetra
- `generateCachedTables.cs` : Code generator that creates cached table definitions for the OpenPetra ORM system.
- `codeGenerationCascading.cs` : Generates code for cascading operations (delete, count) across database tables in OpenPetra's ORM system.
- `generateTableList.cs` : Generates an ordered list of database tables based on foreign key dependencies for ORM code generation.

### Build Tools
- `main.cs` : Code generation tool that creates ORM classes for OpenPetra from database definitions in petra.xml.

## Files
### codeGenerationAccess.cs

CodeGenerationAccess implements a class that generates Object-Relational Mapping (ORM) code for database access in OpenPetra. It creates C# methods for CRUD operations and specialized data retrieval patterns based on table relationships defined in XML. Key functionality includes generating direct table access methods, foreign key relationships (LoadVia methods), and bridge table relationships. The class analyzes table constraints to create appropriate data access patterns, handles primary keys, unique keys, and foreign keys, and generates parameterized SQL queries. Important methods include WriteTypedDataAccess, InsertViaOtherTable, InsertViaLinkTable, and various helper methods that prepare code snippets for different relationship types.

 **Code Landmarks**
- `Line 73`: Maintains a collection of direct references to avoid duplicate LoadViaLink implementations
- `Line 89`: Validates foreign key constraints for generating LoadVia methods with specific business rules
- `Line 351`: Complex algorithm to identify bridge tables by analyzing primary key constraints
- `Line 428`: Dynamically generates procedure names with disambiguation logic for tables with multiple foreign keys
- `Line 508`: Detects sequence fields in tables to automatically generate ID assignment code
### codeGenerationCascading.cs

CodeGenerationCascading implements functionality to generate C# code for cascading database operations in OpenPetra's ORM system. It creates methods for cascading deletions and reference counting across related tables based on foreign key relationships defined in the data model. The class analyzes table relationships and generates appropriate code to handle dependencies when records are deleted. Key methods include PrepareCodeletsPrimaryKey for handling primary key parameters, InsertMainProcedures for generating cascading code snippets, and WriteTypedDataCascading as the main entry point. The class also implements safeguards against excessive cascading operations with the CASCADING_DELETE_MAX_REFERENCES constant.

 **Code Landmarks**
- `Line 32`: Implements a constant to limit cascading delete operations to prevent performance issues with heavily referenced tables
- `Line 35`: PrepareCodeletsPrimaryKey method creates type-safe parameter lists for primary key fields with validation for duplicate labels
- `Line 183`: Special case handling for tables that might cause excessive cascading operations (s_user, a_ledger)
- `Line 264`: Generates both delete and count operations with different template snippets for each related table
### codeGenerationDataset.cs

CodeGenerationDataset implements functionality to generate typed dataset classes for the OpenPetra ORM framework. It processes XML definitions to create strongly-typed C# classes representing database tables and their relationships. The class parses XML input files containing dataset definitions, table structures, custom fields, and relationships, then generates corresponding C# code using templates. Key functionality includes creating dataset classes, table classes, row classes, and defining relationships between tables. Important methods include CreateTypedDataSets, AddTableToDataset, and StringCollectionToValuesFormattedForArray. The class works with TDataDefinitionStore to access database schema information.

 **Code Landmarks**
- `Line 73`: Special method to retrieve original SQL field names from table definitions for generating proper array initializers
- `Line 139`: Uses a static counter (DataSetTableIdCounter) to assign unique IDs to tables in the dataset
- `Line 184`: Supports custom fields in tables, allowing dataset definitions to extend database schema
- `Line 286`: Implements custom table references across datasets, enabling composition of complex data structures
- `Line 370`: Automatically generates foreign key constraints between tables in the dataset
### codeGenerationDatasetAccess.cs

CodeGenerationDatasetAccess implements functionality for generating C# code that handles database operations for typed datasets in OpenPetra. It creates data access classes that manage reading and writing datasets to and from the database. The class provides methods for generating code that handles table operations including inserts, updates, and deletes, with special handling for sequence columns and foreign key relationships. Key functionality includes CreateTypedDataSets which processes XML dataset definitions and AddTableToDataset which generates code for individual tables. The class works with templates to produce standardized data access code with proper sequence handling.

 **Code Landmarks**
- `Line 75`: Detects sequence columns in tables and generates special handling code for database sequences
- `Line 106`: Automatically updates foreign key references when sequence values are assigned to primary keys
- `Line 173`: Uses template-based code generation to create consistent data access patterns
### codeGenerationTable.cs

CodeGenerationTable implements functionality for generating C# code for typed tables and rows in OpenPetra's ORM system. It provides methods to create strongly-typed data structures from database table definitions. Key functionality includes InsertTableDefinition which generates table class code with columns, primary keys, and properties, and InsertRowDefinition which creates corresponding row classes with typed properties and null handling. The WriteTypedTable method orchestrates the code generation process for all tables in a specified group, using templates to produce consistent C# files.

 **Code Landmarks**
- `Line 59`: Handles derived tables by setting appropriate base classes and overriding properties
- `Line 147`: Ensures primary key fields are ordered consistently with table definition to avoid confusion
- `Line 279`: Type-specific null handling for different .NET types (DateTime?, String, etc.)
- `Line 342`: Converts database default values to appropriate C# syntax based on column type
### codeGenerationTableValidation.cs

CodeGenerationTableValidation implements code generation for validating typed database tables in OpenPetra's ORM layer. It creates validation methods that enforce constraints like NOT NULL checks, date validations, string length limitations, and number range validations. The class analyzes table definitions and generates appropriate C# validation code based on field types and constraints. The main functionality includes generating validation code for individual columns and special handling for deletable rows. Key methods include InsertTableValidation() which processes individual table fields and WriteValidation() which orchestrates the overall code generation process for a group of tables.

 **Code Landmarks**
- `Line 52`: Generates different validation code based on field types, with special handling for DateTime fields
- `Line 131`: Uses a template-based code generation approach with snippets for different validation types
- `Line 177`: Implements automatic number range validation based on database field length and decimal specifications
- `Line 86`: Special handling for deletable row validation, important for data integrity when records are marked for deletion
### generateCachedTables.cs

TGenerateCachedTables implements functionality to generate code for cached database tables in OpenPetra. It reads a YAML configuration file specifying which tables should be cached, and generates both shared enum definitions and server-side implementation files. The generator creates one shared file with enum definitions and separate server files for each submodule. It handles special cases like tables that depend on ledgers, calculated lists, and tables that can be stored back to the database. The main method WriteCachedTables processes the YAML configuration and uses template-based code generation to create the necessary C# files.

 **Code Landmarks**
- `Line 42`: Uses template-based code generation to create both shared enum definitions and server implementations
- `Line 49`: Parses YAML configuration converted to XML to determine which tables should be cached
- `Line 163`: Intelligently handles table naming conventions by transforming database table names to appropriate enum names
- `Line 233`: Supports special handling for tables that depend on ledgers with separate code paths
- `Line 276`: Handles calculated lists that can be stored back to database tables
### generateTableList.cs

TGenerateTableList implements a utility class for producing an ordered list of database tables based on foreign key dependencies. The main functionality is in the WriteTableList method, which takes a data definition store and output filename to generate a C# file containing ordered table lists. It uses a template-based approach to create code that includes all database tables, tables available for custom reports, and database sequences. The class leverages TTableSort.TopologicalSort to order tables properly according to their dependencies. Important components include the TDataDefinitionStore parameter, ProcessTemplate for code generation, and separate collections for tables and sequences.

 **Code Landmarks**
- `Line 38`: Uses topological sorting to handle foreign key dependencies between database tables
- `Line 44`: Template-based code generation approach for creating maintainable ORM infrastructure
- `Line 52`: Separate handling for tables available for custom reporting functionality
### main.cs

The main.cs file implements a code generation tool for OpenPetra's Object-Relational Mapping (ORM) framework. It parses database definitions from petra.xml and generates typed tables, datasets, data access classes, validation classes, and cached tables. The program supports multiple operations including defaulttables, dataaccess, dataset, and cachedtables generation. It processes command-line arguments to determine which components to generate and where to output the files. The tool generates code for various OpenPetra modules like Partner, Finance, Personnel, and SysMan, creating consistent data access layers across the application.

 **Code Landmarks**
- `Line 36`: Defines the core parser and data store objects that read the database schema definition from petra.xml
- `Line 47`: Command-line interface with multiple operation modes for different code generation tasks
- `Line 98`: Generates typed table classes for different modules with consistent namespacing pattern
- `Line 142`: Generates validation classes that enforce business rules on database tables
- `Line 293`: Supports multiple module groups that can be processed in batch operations

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #