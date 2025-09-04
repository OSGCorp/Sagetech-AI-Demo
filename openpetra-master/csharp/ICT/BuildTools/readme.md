# C# Build Tools Subproject Of OpenPetra

The OpenPetra C# Build Tools subproject provides essential infrastructure for building, testing, and deploying the OpenPetra application. This subproject implements automated build processes and dependency management along with continuous integration support. The build tools provide these capabilities to the OpenPetra program:

- Automated build pipeline configuration
- Dependency resolution and package management
- Cross-platform build support (Windows, Linux)
- Test automation infrastructure
- Deployment packaging and configuration

## Identified Design Elements

1. **MSBuild Integration**: Custom MSBuild tasks and targets that extend the standard build process for OpenPetra-specific requirements
2. **NuGet Package Management**: Automated handling of package dependencies and versioning across the entire solution
3. **Cross-Platform Compatibility**: Build scripts that function consistently across Windows and Linux environments
4. **CI/CD Pipeline Support**: Integration with continuous integration systems for automated testing and deployment
5. **Configuration Management**: Tools for managing different build configurations (Debug, Release, Test) with appropriate settings

## Overview
The architecture follows modern .NET build practices while accommodating OpenPetra's specific needs as an open-source non-profit management system. The build tools are designed to simplify the development workflow, ensure consistent builds across environments, and facilitate contributions from the open-source community. The system uses standard .NET build technologies where possible while providing custom extensions where needed for OpenPetra's unique requirements.

## Sub-Projects

### csharp/ICT/BuildTools/DBXML

The program handles contact management, accounting, and sponsorship while supporting international operations. This sub-project implements the database schema definition and parsing infrastructure along with XML-based configuration management. This provides these capabilities to the OpenPetra program:

- Database schema representation in memory
- XML-based database definition parsing
- Type conversion between SQL and .NET types
- Automated schema generation and validation
- Relationship management between database tables

#### Identified Design Elements

1. **Schema Definition Store**: The TDataDefinitionStore class serves as a container for all database structure elements, providing methods to retrieve, add, and manipulate tables and sequences
2. **XML Schema Parser**: TDataDefinitionParser extends TXMLParser to read and validate database schema definitions from XML files
3. **Type Mapping System**: Automatic conversion between SQL and .NET types ensures data integrity across the application stack
4. **Dependency Management**: Tables are sorted by dependencies to ensure proper creation order and relationship integrity
5. **Legacy Support**: The parser includes compatibility options like SupportPetra2xLegacyStandard for maintaining backward compatibility

#### Overview
The architecture emphasizes a clear separation between schema definition and implementation, allowing for database-agnostic operations. The XML-based approach provides a declarative way to define database structures that can be validated, documented, and transformed into various outputs including SQL scripts and code-generated data access layers. This design supports OpenPetra's need for cross-platform compatibility and extensibility while maintaining strict data integrity requirements for financial and administrative operations.

  *For additional detailed information, see the summary for csharp/ICT/BuildTools/DBXML.*

### csharp/ICT/BuildTools/DeleteButtonWiki

The program handles financial transactions and manages organizational data. This sub-project implements automated analysis of delete functionality implementation across the codebase along with documentation generation for delete operations. This provides these capabilities to the Petra program:

- Static code analysis of delete button implementation patterns
- Validation of proper reference counting mechanisms
- Identification of inconsistencies between client and server table names
- Automated documentation generation for delete operations
  - Highlights screens with potential implementation issues
  - Identifies patterns of delete functionality usage

#### Identified Design Elements

1. YAML Configuration Analysis: Parses screen definition files to identify templates and their delete button implementations
2. Pattern Recognition: Identifies standard delete implementation approaches (auto vs. manual) across the codebase
3. Reference Integrity Validation: Verifies proper reference counting mechanisms are in place for data deletion
4. Documentation Generation: Produces wiki-style reports showing delete button implementation status across screens

#### Overview
The architecture focuses on ensuring data integrity by validating proper delete operation implementations throughout the OpenPetra application. It analyzes both auto-generated and manually created code to identify potential issues in delete functionality. The tool helps maintain consistency in how data deletion is handled across different screens and modules, providing developers with a comprehensive view of delete button implementations and highlighting areas that may need attention or improvement.

  *For additional detailed information, see the summary for csharp/ICT/BuildTools/DeleteButtonWiki.*

### csharp/ICT/BuildTools/DataMigrateStatistics

The program handles financial transactions and contact management while reducing operational overhead. This sub-project implements data migration verification capabilities along with statistical analysis of migration completeness. This provides these capabilities to the OpenPetra program:

- Database migration integrity validation
- Quantitative assessment of migration success rates
- Automated comparison between expected and actual data volumes
- Detailed reporting of migration discrepancies

#### Identified Design Elements

1. **Database Schema Awareness**: Connects to the database and understands the Petra XML schema structure to verify appropriate tables
2. **Configuration-Driven Verification**: Uses external configuration files (_row_count.txt) to define expected data volumes
3. **Statistical Analysis**: Calculates and reports migration completeness as percentage metrics
4. **Discrepancy Identification**: Pinpoints specific tables with incomplete or inconsistent migration results

#### Overview
The architecture follows a straightforward utility design pattern, focusing on reliability and accuracy in migration verification. The tool operates independently as a command-line utility that can be integrated into deployment pipelines or run manually. It provides both detailed table-level diagnostics and high-level statistical summaries of migration success. This verification layer ensures data integrity when moving between environments or during system upgrades, serving as a critical quality assurance component for the broader OpenPetra application.

  *For additional detailed information, see the summary for csharp/ICT/BuildTools/DataMigrateStatistics.*

### csharp/ICT/BuildTools/CheckHtml

The program handles financial transactions and manages organizational data. This sub-project implements HTML validation tooling along with quality control for the OpenPetra js-client forms. This provides these capabilities to the OpenPetra program:

- Automated validation of HTML structure in js-client forms
- Detection of modal component requirements (header, body, footer)
- Prevention of ID conflicts by identifying improper 'for' attributes in labels
- Recursive processing of all HTML files in specified directories

#### Identified Design Elements

1. **Command-Line Interface**: Provides a utility that can be integrated into build processes or run manually to ensure HTML quality
2. **Regex-Based Validation**: Uses pattern matching to identify structural issues without requiring a full HTML parser
3. **Content Normalization**: Removes whitespace and standardizes HTML content for consistent analysis
4. **Recursive Directory Processing**: Automatically handles nested folder structures to validate all relevant files

#### Overview
The architecture emphasizes quality control through automated validation, ensuring consistent structure across the js-client forms. The tool helps maintain the integrity of the OpenPetra UI by preventing common issues that could cause rendering problems or functional conflicts. By validating HTML during development or build processes, it reduces the likelihood of UI-related bugs reaching production environments.

  *For additional detailed information, see the summary for csharp/ICT/BuildTools/CheckHtml.*

### csharp/ICT/BuildTools/CodeGeneration

This component enables maintainable and consistent code production through static analysis, validation enforcement, and structured code manipulation. The subproject implements source code parsing, validation rule generation, and targeted code insertion capabilities that support OpenPetra's development workflow.

Key capabilities provided to the OpenPetra program:

- Static analysis of C# source files to extract class hierarchies, interfaces, and method signatures
- Automated data validation code generation based on database schema constraints
- Region-based code insertion for maintaining generated code sections
- Type and parameter manipulation utilities for consistent code generation

#### Identified Design Elements

1. **NRefactory Integration**: Leverages SharpDevelop's NRefactory to parse and analyze C# source files, with caching for performance optimization
2. **Database-Driven Validation**: Generates validation code based on database constraints (NULL checks, date validation, string length, numeric ranges)
3. **Namespace Management**: Handles namespace resolution and mapping to ensure proper type references across the codebase
4. **Region-Based Code Insertion**: Preserves manually written code while updating generated sections through region markers
5. **Code Formatting Utilities**: Maintains consistent code style through parameter formatting and type reference normalization

#### Architecture Overview

The architecture follows a modular approach with clear separation of concerns. CSParser handles code analysis, Validation manages constraint enforcement, AutoGenerationTools provides formatting utilities, and InsertIntoRegion enables targeted code modification. Together, these components create a pipeline for analyzing existing code, generating new code with proper validation, and inserting it into appropriate locations while maintaining code quality and consistency.

  *For additional detailed information, see the summary for csharp/ICT/BuildTools/CodeGeneration.*

### csharp/ICT/BuildTools/TinyWebServer

This subproject consists of two core components: a threaded HTTP listener wrapper and a specialized worker request handler. The server enables OpenPetra to expose its ASMX web services and web interfaces without requiring external web server dependencies.

The architecture is designed for efficiency and simplicity, with these key capabilities:

- Multi-threaded request processing for concurrent connections
- Configurable port binding and runtime parameters
- Comprehensive HTTP request/response handling
- Virtual-to-physical path translation for web resources
- Integrated logging system for diagnostics

#### Identified Design Elements

1. **ThreadedHttpListenerWrapper**: Manages the core HTTP listener instance, spawning worker threads for each incoming connection to maintain responsiveness
2. **TMyHttpWorkerRequest**: Extends the standard HttpWorkerRequest to process web requests, handling headers, request data, and response output
3. **Command-line Configuration**: Supports runtime parameters for port selection, log file location, and maximum execution time
4. **Application Host Integration**: Configures and initializes the ASP.NET application host to process web requests within the OpenPetra environment

#### Overview
The architecture prioritizes lightweight operation while providing full HTTP server capabilities. It's designed to be a drop-in replacement for more complex web servers when needed, maintaining compatibility with OpenPetra's web service requirements. The implementation balances simplicity with the necessary features to support OpenPetra's administrative web interfaces and API endpoints.

  *For additional detailed information, see the summary for csharp/ICT/BuildTools/TinyWebServer.*

### csharp/ICT/BuildTools/GenerateGlue

The system bridges client and server components through automatically generated communication interfaces. This sub-project implements code generation tooling that creates the necessary "glue" code to facilitate client-server communication and API exposure. This provides these capabilities to the OpenPetra program:

- Automated generation of interface files from YAML configurations
- Identification and exposure of connector classes through the OpenPetra API
- Generation of server-side code for client-server communication
- Creation of WebConnector and UIConnector classes with proper security validation
- Support for both core OpenPetra modules and plugins

#### Identified Design Elements

1. **Namespace Hierarchy Management**: The system parses and maintains namespace trees to organize code generation across modules
2. **Connector Discovery**: Automatically identifies classes in namespaces ending with 'Connectors' to expose through the API
3. **Security Integration**: Generated code includes permission checks and security validation for all exposed methods
4. **Plugin Support**: The architecture distinguishes between core modules and plugins, generating appropriate interfaces for each
5. **Parameter Handling**: Implements automatic type conversion and binary serialization for complex parameter types

#### Overview
The architecture emphasizes separation between client and server components while providing seamless communication through generated interfaces. The code generation process starts with parsing C# source files, identifying connector classes, and then generating appropriate server-side glue code based on templates. The system handles both standard modules and plugins differently, with special namespace handling for plugins. The generated code includes proper security checks, parameter handling, and type conversions to ensure robust client-server communication.

  *For additional detailed information, see the summary for csharp/ICT/BuildTools/GenerateGlue.*

### csharp/ICT/BuildTools/DevelopersAssistantUpdater

It handles contact management, accounting, sponsorship, and data export functionality. This sub-project implements an automated update mechanism for the Developer's Assistant component of Petra, ensuring seamless version transitions without manual intervention.

The C# Developer's Assistant Updater provides these capabilities to the Petra program:

- Automated file replacement during application downtime
- Intelligent process management (waiting for application shutdown)
- Targeted file copying for executables and debugging symbols
- Application relaunch after successful updates
- Comprehensive logging of update operations

#### Identified Design Elements

1. Process Monitoring: The updater monitors the target application's process state to determine when it's safe to perform updates
2. Configurable Deployment: Uses command-line arguments to specify target paths, making it adaptable to different environments
3. Selective File Updating: Focuses on critical application files (executables and PDB files) to minimize update time
4. Error Handling: Implements robust error handling with appropriate user feedback and logging
5. Application Lifecycle Management: Handles the complete update cycle from shutdown detection through to application restart

#### Overview
The architecture follows a straightforward procedural approach focused on reliability and minimal disruption. The updater operates as a standalone utility that can be triggered externally, making it suitable for integration with CI/CD pipelines or manual update processes. Its design prioritizes operational reliability over complex features, ensuring that the critical task of updating application files is performed consistently and safely.

  *For additional detailed information, see the summary for csharp/ICT/BuildTools/DevelopersAssistantUpdater.*

### csharp/ICT/BuildTools/GenerateORM

This subproject automates the creation of strongly-typed C# classes from database schema definitions, eliminating boilerplate code and ensuring consistency across the data access layer. The code generator processes XML and YAML definitions to produce type-safe database access components that support OpenPetra's non-profit management features.

Key technical capabilities include:

- Strongly-typed dataset and table class generation
- Automated data validation code generation based on schema constraints
- CRUD operation code generation with parameterized SQL
- Relationship-aware data access methods (foreign keys, bridge tables)
- Cascading operation support for maintaining referential integrity
- Cached table generation for performance optimization
- Topologically sorted table lists based on dependencies

#### Identified Design Elements

1. **Template-Based Code Generation**: Uses templates to ensure consistency across generated code files
2. **Schema-Driven Development**: Derives C# classes directly from database definitions in petra.xml
3. **Relationship-Aware Code**: Automatically generates LoadVia methods based on foreign key relationships
4. **Type Safety**: Creates strongly-typed properties and methods matching database column definitions
5. **Validation Integration**: Generates constraint validation code based on database schema rules
6. **Modular Architecture**: Separates generation concerns into specialized classes (tables, datasets, validation, etc.)

#### Architecture Overview

The ORM generator is structured around specialized code generation classes, each responsible for a specific aspect of the data access layer. The system reads database definitions and produces a comprehensive set of C# files that handle all database interactions. The architecture emphasizes type safety, performance through caching, and maintainability through consistent patterns. This generated code forms the foundation of OpenPetra's data access strategy, enabling the application's features like contact management, accounting, and sponsorship tracking.

  *For additional detailed information, see the summary for csharp/ICT/BuildTools/GenerateORM.*

### csharp/ICT/BuildTools/GenerateSQL

This tool bridges the gap between OpenPetra's XML data definitions and actual database implementations, supporting both PostgreSQL and MySQL database systems. The subproject implements database schema generation, data import capabilities, and database initialization processes that form the foundation of OpenPetra's data persistence layer.

#### Key Technical Capabilities

- Database schema generation from XML data definitions
- Cross-database compatibility (PostgreSQL and MySQL)
- Automated SQL script generation for table creation, constraints, and indexes
- Dependency-aware table creation through topological sorting
- Data loading from CSV files into database tables
- Command-line interface for integration into build and deployment processes

#### Identified Design Elements

1. **XML-Driven Schema Definition**: Uses XML data definitions as the source of truth for database schema, enabling database-agnostic application development
2. **Database Type Abstraction**: Handles syntax differences between PostgreSQL and MySQL transparently
3. **Topological Dependency Resolution**: Ensures tables are created in the correct order based on their relationships
4. **CSV Data Import**: Provides mechanisms to populate databases with initial data from CSV files
5. **Build Process Integration**: Functions as both a standalone tool and a component in the OpenPetra build pipeline

#### Architecture Overview

The subproject follows a modular design with clear separation of concerns. The `Program.cs` serves as the command-line entry point, delegating to specialized components: `TWriteSQL` handles SQL script generation while `TLoadMysql` manages data loading operations. The architecture emphasizes database portability, allowing OpenPetra to function across different database platforms while maintaining a consistent data model.

  *For additional detailed information, see the summary for csharp/ICT/BuildTools/GenerateSQL.*

### csharp/ICT/BuildTools/GenerateI18N

This toolset automates the extraction, management, and integration of translatable strings throughout the codebase. The subproject processes C# source files, UI designer files, and YAML navigation definitions to ensure all user-facing text is properly prepared for translation using the gettext framework.

Key capabilities provided to OpenPetra include:

- Automated extraction of translatable strings from source code and UI definitions
- Management of PO (Portable Object) translation files with UTF-8 encoding support
- Filtering of strings that should not be translated to simplify translator workload
- Generation of error code documentation with internationalized descriptions
- Integration with the gettext internationalization framework

#### Identified Design Elements

1. **Catalog String Generation**: Automatically injects `Catalog.GetString()` calls into C# files by analyzing UI designer files and extracting text properties
2. **PO File Management**: Parses and updates gettext PO files while preserving file structure and metadata
3. **Translation Filtering**: Implements a "Do Not Translate" mechanism to exclude technical strings from translation files
4. **Command-line Interface**: Provides a comprehensive CLI tool for managing the entire i18n workflow
5. **Error Code Documentation**: Generates internationalized HTML documentation for application error codes organized by module

#### Overview

The architecture follows a modular approach with specialized classes handling different aspects of the internationalization process. The system integrates with the build pipeline to ensure translations are consistently maintained. The tooling emphasizes automation to reduce manual effort in preparing strings for translation while ensuring comprehensive coverage of user-facing text throughout the application.

  *For additional detailed information, see the summary for csharp/ICT/BuildTools/GenerateI18N.*

### csharp/ICT/BuildTools/GuidedTranslation

The program handles internationalization requirements and translation management across the application. This sub-project implements automated translation template analysis and derivate management along with internationalization utilities for the OpenPetra ecosystem. This provides these capabilities to the OpenPetra program:

- Identification and management of translation string variations
- Analysis of POT (Portable Object Template) files for duplicate entries
- Processing of translation templates to identify items differing by special characters or casing
- Generation of comprehensive translation analysis reports

#### Identified Design Elements

1. Translation String Management: The ItemWithDerivates class maintains original strings and their variations, enabling efficient internationalization
2. Template File Processing: TProcessPot analyzes POT files to identify duplicates and variations, categorizing items by word count and derivation patterns
3. Derivate Tracking: The system identifies and groups related translation strings that differ only by special characters or casing
4. Command-line Interface: Program.cs provides a CLI tool for processing translation templates with configuration via application settings

#### Overview
The architecture emphasizes efficient internationalization through intelligent string management, with clear separation between template processing and string representation. The system reduces translation workload by identifying similar strings and variations, generating comprehensive analysis reports to guide translation efforts. The modular design spans multiple architectural layers including Cross-Cutting Concerns, Build & Deployment Tools, and Data Import-Export functionality, providing a cohesive approach to managing translations across the OpenPetra application.

  *For additional detailed information, see the summary for csharp/ICT/BuildTools/GuidedTranslation.*

### csharp/ICT/BuildTools/CodeChecker

The program handles financial transactions and manages organizational data across multiple modules. This sub-project implements static code analysis for the OpenPetra codebase along with pattern-based detection of potential runtime issues. This provides these capabilities to the OpenPetra program:

- Identification of static variables that could cause issues in ASP.NET environments
- Detection of improper database transaction handling patterns
- Regex-based code scanning for problematic coding patterns
- False-positive filtering system to reduce noise in analysis results

#### Identified Design Elements

1. Pattern-Based Code Analysis: Uses regular expressions to identify potentially problematic code patterns in C# source files
2. Recursive File Scanning: Automatically traverses the codebase to locate and analyze all relevant source files
3. Configurable False-Positive System: Allows developers to mark known exceptions to avoid repeated flagging
4. CI/Build Integration: Returns numeric exit codes that can be interpreted by continuous integration systems

#### Overview
The architecture emphasizes code quality assurance through automated static analysis. The tool focuses particularly on database transaction handling patterns, which are critical for the application's data integrity. Developers can extend the tool by adding new regex patterns or false-positive filters in their respective methods. The command-line interface provides both console output and log files for review, with a companion batch script that provides human-readable interpretation of results. This tool serves as a quality gate to prevent problematic code patterns from entering the production codebase.

  *For additional detailed information, see the summary for csharp/ICT/BuildTools/CodeChecker.*

### csharp/ICT/BuildTools/FilterButtonWiki

The program handles financial transactions and manages organizational contacts. This sub-project implements automated UI analysis tooling along with documentation generation for filter/find functionality across the application's screens. This provides these capabilities to the OpenPetra program:

- Automated identification of screens requiring filter functionality
- Template-based categorization of UI components
- Documentation generation in wiki format
- Mantis issue tracking integration

#### Identified Design Elements

1. YAML UI Definition Analysis: Scans client-side YAML files to identify screens with or without filter/find functionality
2. Template Categorization System: Classifies screens by template type to determine appropriate filter implementation requirements
3. Metadata-Driven Documentation: Uses XML metadata to track special cases and implementation notes
4. Issue Tracking Integration: Maps UI components to Mantis feature requests for development tracking

#### Overview
The architecture focuses on development support through automated analysis of the OpenPetra UI definition files. The tool generates comprehensive reports that help developers identify which screens need filter/find functionality implementation, which already have it, and tracks this against the issue management system. The metadata-driven approach allows for special case handling and provides a centralized reference for UI component implementation status. This tooling supports consistent UI behavior across the application while maintaining clear documentation of implementation status.

  *For additional detailed information, see the summary for csharp/ICT/BuildTools/FilterButtonWiki.*

*No project files processed for this directory.*

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #