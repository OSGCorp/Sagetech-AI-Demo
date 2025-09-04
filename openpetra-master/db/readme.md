# Database Subproject Of Petra

The Database subproject provides the foundational data structure definitions for OpenPetra, an open-source administration system for non-profit organizations. This subproject implements the core database schema and versioning mechanisms that support all data storage and retrieval operations across the application. The Database components define the structural blueprint that enables these capabilities:

- Comprehensive entity modeling for partner management, accounting, and gift processing
- Relationship definitions between system entities via primary and foreign keys
- Data validation constraints and field specifications
- Query optimization through strategic index definitions
- Version tracking for database schema compatibility

## Identified Design Elements

1. **XML-Based Schema Definition**: The database structure is defined in XML format with a corresponding DTD, allowing for programmatic generation of database scripts and data access code
2. **Logical Domain Grouping**: Tables are organized into functional domains (system management, partner management, accounting, gift processing) for better maintainability
3. **Rich Metadata**: Each field contains detailed specifications including data types, formats, constraints, and descriptive documentation
4. **Relationship Modeling**: The schema explicitly defines table relationships through primary and foreign key constraints
5. **Versioning System**: A structured versioning approach (YYYY.MM.PATCH-BUILD) enables tracking of database schema changes and compatibility

## Overview
The architecture follows a declarative approach to database design, with schema definitions separated from implementation code. This separation enables database platform independence and facilitates automated code generation for data access layers. The comprehensive metadata embedded in the schema supports robust validation, documentation, and UI generation throughout the OpenPetra application.

## Sub-Projects

### db/basedata

The program handles financial transactions and contact relationship management through a structured data model. This sub-project implements the foundational reference data along with the initialization scripts that populate the database with essential lookup values. This provides these capabilities to the OpenPetra program:

- Standardized reference data across all modules
- Internationalization support through country, language, and currency definitions
- Categorization frameworks for partners, personnel, and financial entities
- Initialization scripts for database setup and deployment

#### Identified Design Elements

1. **Hierarchical Data Organization**: Reference data is organized into functional domains (Partner, Personnel, Finance, Geographic) with clear relationships between entities
2. **Extensible Classification Systems**: Partner types, attributes, and relationships use flexible categorization schemes that can be adapted to different organizational needs
3. **Internationalization Support**: Comprehensive country, language, and currency data enables global operations
4. **GDPR Compliance Framework**: Consent tracking mechanisms for different communication channels and purposes

#### Overview
The architecture emphasizes data consistency through predefined reference values, supports international operations with comprehensive geographic and currency information, and provides flexible classification systems for contacts and personnel. The database initialization process ensures all required lookup tables are populated during system setup. The reference data covers diverse functional areas including contact management, personnel tracking, financial operations, and system configuration, providing a solid foundation for the application's business logic.

  *For additional detailed information, see the summary for db/basedata.*

### db/html

This subproject implements a frameset-based documentation system along with interactive JavaScript functionality to navigate and display database schema information. This provides these capabilities to the OpenPetra program:

- Interactive database schema documentation with navigable table relationships
- Dual-view interface showing table groups and detailed table information
- Dynamic content loading based on URL parameters
- Visual differentiation of database elements (primary keys, foreign keys, constraints)

#### Identified Design Elements

1. Frameset Architecture: Uses HTML frames to create a consistent layout with navigation sections and content areas that can be updated independently
2. Parameter-Driven Content Loading: JavaScript functions parse URL parameters to dynamically load specific table documentation
3. Visual Schema Representation: CSS styling provides clear visual differentiation between database elements like primary keys and foreign keys
4. Tooltip System: Implements a cross-browser compatible popup description system that follows the cursor for enhanced usability
5. External Documentation Integration: Links to SchemaSpy documentation showing database schema with row counts for different database configurations

#### Overview
The architecture emphasizes usability through a consistent interface with navigation tabs and split views. The JavaScript functionality enables dynamic content loading without page refreshes, while the CSS styling creates visual hierarchy and readability for complex database information. The documentation system is designed to help developers understand table relationships, field constraints, and database structure, facilitating maintenance and feature development within OpenPetra's data layer.

  *For additional detailed information, see the summary for db/html.*

### db/demodataGermany

This subproject implements localized financial structures and partner data samples along with country-specific configurations for accounting, banking, and organizational management. This provides these capabilities to the OpenPetra program:

- German-specific chart of accounts with hierarchical structure
- Localized financial transaction types and accounting periods
- Sample partner data including organizations, families, and banks
- Currency configuration with EUR as base and exchange rates to USD

#### Identified Design Elements

1. **Modular Data Organization**: Data is organized into distinct CSV files by functional domain (accounting, partners, publications) for maintainability
2. **Dynamic Date Handling**: Uses template variables like `${datetime::get-year(datetime::now())}` to ensure demo data remains relevant regardless of installation date
3. **Hierarchical Account Structure**: Implements a comprehensive chart of accounts with parent-child relationships for financial reporting
4. **Internationalization Support**: Includes multi-currency handling with EUR base and exchange rates to USD

#### Overview
The architecture emphasizes a complete demonstration environment for German non-profit organizations using OpenPetra. The data structure follows standard accounting principles with localized adaptations for German requirements. The subproject includes initialization scripts that establish proper permissions, default site settings, and sample organizational structures. The demo data covers all major functional areas of OpenPetra including partner management, accounting, gift processing, publications, and supplier management, providing a realistic testing and training environment for German implementations.

  *For additional detailed information, see the summary for db/demodataGermany.*

### db/doc

This component generates up-to-date documentation directly from the database schema, ensuring that developers always have accurate reference materials as the system evolves. The subproject implements automated schema extraction and documentation generation along with visual representation of database relationships. This provides these capabilities to the OpenPetra program:

- Automated generation of database schema documentation
- Visual representation of table relationships and dependencies
- Searchable reference of database fields, types, and constraints
- Integration with the broader OpenPetra documentation system

#### Identified Design Elements

1. **Schema Extraction Engine**: Directly queries database metadata to generate accurate documentation without manual intervention
2. **Relationship Mapping**: Visualizes foreign key relationships between tables to aid in understanding data flow
3. **Documentation Templates**: Standardized formats for presenting table structures, constraints, and relationships
4. **Version Control Integration**: Tracks schema changes over time to provide historical context for database evolution

#### Overview
The architecture follows a modular approach that separates the concerns of schema extraction, documentation generation, and presentation. The system is designed to run both as a scheduled task to maintain up-to-date documentation and on-demand when developers need the latest information. Documentation is generated in multiple formats including HTML, PDF, and interactive web-based visualizations to serve different use cases. This subproject is particularly valuable for onboarding new developers and for maintaining consistency during database schema modifications.

  *For additional detailed information, see the summary for db/doc.*

### db/demodataPublicInstaller

This component provides essential database setup functionality through SQL scripts that configure the system for demonstration use.

#### Key Technical Functions

- **User Account Configuration** - Automatically sets all non-anonymous user accounts to require password changes on first login
- **System Default Settings** - Establishes core system configuration parameters for the demonstration environment
- **Reporting Engine Configuration** - Configures OpenPetra to use its built-in XML Report engine rather than external reporting solutions

#### Technical Architecture

The installer consists of SQL scripts that perform targeted database modifications to prepare a clean demonstration environment. These scripts operate at the database layer and establish the foundation for OpenPetra's functionality by:

- Setting security flags in user tables
- Inserting system configuration records
- Establishing default operational parameters

#### Implementation Details

The implementation follows a straightforward SQL-based approach that:

1. Targets specific system tables for configuration updates
2. Maintains security best practices by requiring password resets
3. Configures the reporting subsystem for standalone operation
4. Ensures consistent behavior across demonstration instances

This component serves as the foundation for OpenPetra demonstrations, enabling evaluators and developers to experience a properly configured system without manual setup steps.

  *For additional detailed information, see the summary for db/demodataPublicInstaller.*

## Business Functions

### Database Schema Definition
- `datastructure.dtd` : XML DTD schema definition for OpenPetra database structure, defining tables, fields, keys, and relationships.
- `petra.xml` : XML database schema definition for OpenPetra, defining tables, fields, and relationships for the non-profit management system.

### Version Management
- `version.txt` : Version file containing the current OpenPetra software version number.

## Files
### datastructure.dtd

This DTD file defines the XML schema for OpenPetra's database structure. It establishes elements and attributes for database tables, fields, keys, and relationships. The schema includes definitions for tables with their fields, primary keys, unique keys, foreign keys, triggers, and indexes. It specifies data types, validation rules, descriptions, and relationships between tables. The file also defines sequences for auto-incrementing values with attributes for initial values, increments, and limits. This schema serves as the foundation for generating database scripts and data access code in the OpenPetra system.

 **Code Landmarks**
- `Line 5`: Defines the database root element structure that contains tables and sequences
- `Line 11`: Table definition includes attributes for custom reporting permissions and synchronization controls
- `Line 32`: Field definitions support both database types and .NET type mappings for ORM functionality
- `Line 19`: Trigger system allows for custom procedures on create/write/delete/assign operations
- `Line 89`: Sequence definition supports configurable auto-increment values with cycle limits
### petra.xml

The petra.xml file defines the database schema for OpenPetra, an open-source administration system for non-profit organizations. It contains comprehensive table definitions with fields, constraints, and relationships that support the system's core functionality. The schema is organized into logical groups including system management (user accounts, permissions), partner management (contacts, organizations, families), accounting (ledgers, transactions, budgets), and gift processing (donations, motivations). Each table definition includes detailed field specifications with data types, formats, constraints, and descriptions. The file also establishes relationships between tables through primary and foreign keys, and defines indexes to optimize query performance. This schema forms the foundation for OpenPetra's data storage and retrieval capabilities across its various modules.

 **Code Landmarks**
- `Line 5`: Uses a DTD (Document Type Definition) for XML validation, ensuring the database structure follows a predefined format
- `Line 1155`: Partner attribute system uses a flexible category-type-value hierarchy to store various contact details like phone numbers and email addresses
- `Line 3493`: Implements a comprehensive accounting system with support for multiple currencies, exchange rates, and hierarchical cost centers
- `Line 5559`: Gift processing system includes sophisticated motivation tracking with configurable fees and tax deductibility settings
- `Line 2805`: Security model uses multiple layers (user, group, module, table) with granular permissions for create, modify, delete, and inquire operations
### version.txt

This file contains the version number for OpenPetra, formatted as YYYY.MM.PATCH-BUILD. The current version is 2023.02.0-0, indicating it was released in February 2023 with patch level 0 and build 0. This version information is likely used throughout the system for compatibility checking, update management, and display purposes.

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #