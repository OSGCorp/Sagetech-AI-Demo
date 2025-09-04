# C# Generate SQL Subproject Of OpenPetra

The C# Generate SQL subproject is a critical database management component of OpenPetra that handles SQL script generation and data loading operations. This tool bridges the gap between OpenPetra's XML data definitions and actual database implementations, supporting both PostgreSQL and MySQL database systems. The subproject implements database schema generation, data import capabilities, and database initialization processes that form the foundation of OpenPetra's data persistence layer.

## Key Technical Capabilities

- Database schema generation from XML data definitions
- Cross-database compatibility (PostgreSQL and MySQL)
- Automated SQL script generation for table creation, constraints, and indexes
- Dependency-aware table creation through topological sorting
- Data loading from CSV files into database tables
- Command-line interface for integration into build and deployment processes

## Identified Design Elements

1. **XML-Driven Schema Definition**: Uses XML data definitions as the source of truth for database schema, enabling database-agnostic application development
2. **Database Type Abstraction**: Handles syntax differences between PostgreSQL and MySQL transparently
3. **Topological Dependency Resolution**: Ensures tables are created in the correct order based on their relationships
4. **CSV Data Import**: Provides mechanisms to populate databases with initial data from CSV files
5. **Build Process Integration**: Functions as both a standalone tool and a component in the OpenPetra build pipeline

## Architecture Overview

The subproject follows a modular design with clear separation of concerns. The `Program.cs` serves as the command-line entry point, delegating to specialized components: `TWriteSQL` handles SQL script generation while `TLoadMysql` manages data loading operations. The architecture emphasizes database portability, allowing OpenPetra to function across different database platforms while maintaining a consistent data model.

## Business Functions

### Database Management
- `writesql.cs` : Generates SQL scripts for creating database tables, constraints, and indexes for OpenPetra based on XML data definitions.
- `loadMysql.cs` : Utility for loading data into MySQL databases from CSV files and SQL statements for OpenPetra.

### Application Entry Point
- `Program.cs` : Command-line tool that generates SQL scripts from XML data definitions for OpenPetra database creation.

## Files
### Program.cs

Program implements a command-line tool for generating SQL scripts from XML data definitions for OpenPetra databases. It supports two operations: 'sql' for creating SQL scripts from XML definitions, and 'load' for directly loading data into MySQL databases. The tool parses XML data definitions, processes them through a data definition store, and either outputs SQL scripts or loads data directly. Key functions include Main() which handles command-line arguments and error handling. Important classes referenced include TDataDefinitionParser, TDataDefinitionStore, TWriteSQL, and TLoadMysql.

 **Code Landmarks**
- `Line 36`: Uses a command-line options parser to handle different operations (sql/load) and database types
- `Line 45`: Parses XML data definitions into a store object before generating SQL
- `Line 60`: Direct database loading capability for MySQL databases, bypassing SQL file generation
### loadMysql.cs

TLoadMysql implements functionality to initialize MySQL databases by loading data from CSV files and executing SQL statements. The class provides methods to establish database connections, parse load scripts, and execute SQL commands. It handles both direct SQL statements (INSERT, UPDATE) and data imports from CSV files, converting PostgreSQL COPY syntax to direct data loading since MySQL's LOAD DATA LOCAL INFILE has security restrictions. Key methods include ExecuteLoadScript which processes SQL files and LoadData which imports CSV data using prepared statements with parameters.

 **Code Landmarks**
- `Line 75`: Converts PostgreSQL COPY commands to direct data loading due to MySQL 8 security restrictions on LOAD DATA LOCAL INFILE
- `Line 106`: Custom CSV file handling that supports local file overrides with .local extension
- `Line 112`: Uses parameterized queries for data loading to prevent SQL injection
### writesql.cs

TWriteSQL implements functionality to generate SQL scripts for database creation from XML data definitions. It supports PostgreSQL and MySQL database types, generating create table statements, constraints, indexes, and sequences. The class provides methods to write complete SQL scripts including table creation with and without constraints, scripts to remove tables, and scripts to clean data. Key methods include WriteSQL (main entry point), WriteTable, DumpFields, WriteField, DumpConstraints, WriteConstraint, and DumpIndexes. The class handles database-specific syntax differences and generates properly ordered SQL statements based on table dependencies through topological sorting.

 **Code Landmarks**
- `Line 67`: Implements database type enum supporting PostgreSQL and MySQL with different SQL syntax handling
- `Line 103`: Uses topological sorting to ensure tables are created in proper dependency order
- `Line 177`: Generates multiple SQL script variants including with/without constraints and table removal scripts
- `Line 280`: Special handling for MySQL sequences using tables with AUTO_INCREMENT as PostgreSQL doesn't have native sequences
- `Line 367`: Database-specific field type mapping and default value handling between PostgreSQL and MySQL

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #