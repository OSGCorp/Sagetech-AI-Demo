# C# Petra Shared Libraries Of OpenPetra

The OpenPetra is a C# application that provides administrative management capabilities for non-profit organizations. The program handles financial transactions and organizational data management across multiple currencies and jurisdictions. This sub-project implements core shared libraries and common functionality along with data access patterns that support the broader application architecture. This provides these capabilities to the OpenPetra program:

- Cross-cutting concerns implementation (logging, configuration, security)
- Data access layer with ORM functionality
- Common business logic shared across application modules
- Utility functions for data transformation and validation
- Multi-language and internationalization support

## Identified Design Elements

1. **Data Access Abstraction**: Implements a repository pattern that decouples business logic from database operations, supporting multiple database backends
2. **Domain Model Implementation**: Contains the core business entities and validation logic used throughout the application
3. **Service Layer Architecture**: Provides intermediary services between controllers and data repositories to encapsulate business rules
4. **Cross-Cutting Concerns**: Centralizes logging, error handling, security, and configuration management
5. **Internationalization Framework**: Supports multiple languages and regional formatting for global deployment

## Overview
The architecture follows a modular design that promotes code reuse and maintainability. The shared libraries implement common patterns and utilities that ensure consistency across the application. The domain model represents the core business concepts while the service layer enforces business rules. This separation of concerns allows for easier testing and maintenance while providing a solid foundation for feature development. The libraries are designed to support both web-based and desktop interfaces through common abstractions.

## Sub-Projects

### csharp/ICT/Petra/Shared/lib/MFinance

The module handles core accounting functions, gift processing, tax calculations, and international currency operations. This sub-project implements cross-cutting financial services and data structures that serve as the foundation for OpenPetra's financial operations, providing these capabilities:

- General Ledger management with batch/journal transaction processing
- Gift processing with tax deductibility calculations
- Bank import and reconciliation functionality
- Multi-currency support with exchange rate management
- Accounts payable operations with supplier payment processing

#### Identified Design Elements

1. **Typed Dataset Architecture**: Strongly-typed datasets (TDS) define the data structures for different financial subsystems, ensuring type safety and clear data relationships
2. **Financial Constants System**: Comprehensive enumeration system standardizes transaction types, account codes, and financial identifiers across the application
3. **Cross-Currency Operations**: Built-in exchange rate management with optimization algorithms for finding best available rates
4. **Financial Validation Framework**: Robust validation for banking information including IBAN/BIC verification according to international standards
5. **Tax Calculation Engine**: Specialized services for calculating tax-deductible portions of financial transactions

#### Overview
The architecture emphasizes data integrity through strong typing and validation, while supporting international financial operations with multi-currency capabilities. The module implements financial industry standards for banking information validation and follows accounting principles for transaction processing. The codebase separates concerns between data definition (TypedDataSets), business logic (Routines classes), and system constants, creating a maintainable structure that can accommodate country-specific financial requirements and complex non-profit accounting scenarios.

  *For additional detailed information, see the summary for csharp/ICT/Petra/Shared/lib/MFinance.*

### csharp/ICT/Petra/Shared/lib/MPersonnel

The program handles contact management and financial accounting while supporting international operations. This sub-project implements personnel management functionality along with organizational hierarchy structures. This provides these capabilities to the OpenPetra program:

- Personnel data validation and relationship management
- Nationality determination from passport records
- Organizational unit hierarchy representation
- Standardized application form type definitions
- Comprehensive personnel data structures through typed datasets

#### Identified Design Elements

1. **Family Relationship Validation**: The module implements checks to validate changes to personnel family relationships, particularly focusing on access control implications
2. **Passport-Based Nationality Calculation**: Sophisticated logic for determining a person's nationalities from passport records, including handling of expired documents
3. **Hierarchical Unit Structure**: Tree-based representation of organizational units with parent-child relationships
4. **Typed Dataset Architecture**: Strongly-typed datasets that define relationships between personnel data entities
5. **Personnel Data Categorization**: Enumeration-based organization of personnel information for UI presentation

#### Overview
The architecture emphasizes data integrity through validation checks, provides specialized business logic for personnel-specific calculations, and maintains hierarchical organizational structures. The module integrates with other OpenPetra components through shared data structures and constants. The typed datasets create a comprehensive data model that combines information from multiple database tables, supporting both basic staff data management and more complex scenarios like application processing and qualification tracking.

  *For additional detailed information, see the summary for csharp/ICT/Petra/Shared/lib/MPersonnel.*

### csharp/ICT/Petra/Shared/lib/MSponsorship

The module integrates with OpenPetra's partner management and financial systems to create a comprehensive sponsorship tracking solution. This sub-project implements typed datasets for sponsorship data management along with the necessary relationships between partners, gifts, and custom sponsorship fields.

##### Key Technical Features

- **Integrated Data Model**: Links partner records with financial transactions through typed datasets
- **Custom Field Support**: Extends standard partner records with sponsorship-specific attributes
- **Cross-Module Integration**: Connects partner management with recurring gift functionality
- **Search Optimization**: Provides specialized data structures for efficient sponsorship queries

##### Identified Design Elements

1. **Typed Dataset Architecture**: Uses XML-defined typed datasets to create strongly-typed data access
2. **Relational Data Structure**: Maintains relationships between donors, sponsors, and recurring gifts
3. **Module Dependency Management**: Imports and extends data structures from Partner and Gift modules
4. **Custom Table Implementation**: Defines specialized tables like SearchResult that combine data from multiple sources

##### Technical Implementation

The module is built around two primary typed datasets: SponsorshipTDS for core sponsorship data management and SponsorshipFindTDS for optimized search operations. These datasets create a bridge between partner information (p_partner, p_family) and financial transactions (a_recurring_gift_batch, a_recurring_gift_detail), enabling the system to track the complete lifecycle of sponsorship relationships while maintaining data integrity across the OpenPetra system.

  *For additional detailed information, see the summary for csharp/ICT/Petra/Shared/lib/MSponsorship.*

### csharp/ICT/Petra/Shared/lib/MPartner

This module implements fundamental partner management capabilities and security controls that serve as the foundation for the partner-related functionality throughout the system.

The module provides these capabilities to the OpenPetra program:

- Comprehensive partner data modeling through typed datasets
- Partner access security and permission enforcement
- Standardized constants for partner types, statuses, and attributes
- Custom exception handling for partner-specific operations
- Location and partner key management

#### Identified Design Elements

1. **Strongly Typed Data Structure**: The module uses XML-defined typed datasets to create a robust data model for partner information, ensuring type safety and relationship integrity.
2. **Layered Security Model**: Security implementation provides granular access control based on user permissions, group membership, and foundation ownership.
3. **Consistent Data Representation**: Extensive constants and enumerations ensure uniform handling of partner data across the application.
4. **Specialized Exception Handling**: Custom exceptions provide clear error information specific to partner operations, enhancing troubleshooting.
5. **Location Management**: Dedicated structures for handling partner locations with proper equality comparison and deep copy capabilities.

#### Overview
The architecture emphasizes type safety through strongly-typed datasets, enforces security through a comprehensive permission model, and maintains data integrity through well-defined constants and enumerations. The module serves as the foundation for all partner-related operations in OpenPetra, providing both the data structures and business rules necessary for partner management. Its design supports extensibility while maintaining strict access control and data validation.

  *For additional detailed information, see the summary for csharp/ICT/Petra/Shared/lib/MPartner.*

### csharp/ICT/Petra/Shared/lib/MConference

The program handles financial transactions and manages organizational data across multiple domains. This sub-project implements the core conference management data structures and constants along with type definitions that support conference operations throughout the system. This provides these capabilities to the OpenPetra program:

- Standardized conference data structures through typed datasets
- Conference application status tracking and management
- Field reporting categorization through unit type enumerations
- Cross-module conference data integration with partner and venue information

#### Identified Design Elements

1. **Typed Dataset Architecture**: Three primary datasets (SelectConferenceTDS, ConferenceSetupTDS, and ConferenceApplicationTDS) provide structured data access patterns for different conference management functions
2. **Standardized Constants**: Centralized definition of application types and statuses ensures consistency across the conference module
3. **Enumeration-Based Categorization**: The TUnitTypeEnum provides a type-safe way to categorize different field types for reporting purposes
4. **Relational Data Integration**: Conference data structures maintain relationships with partner, venue, and person data, enabling comprehensive application management

#### Overview
The architecture establishes a foundation for conference management within OpenPetra through well-defined data structures and constants. The module supports the full lifecycle of conference management from setup and configuration to application processing and reporting. By maintaining clear relationships between conferences, venues, applications, and attendees, the system enables non-profit organizations to efficiently manage conference operations while integrating with the broader partner management and financial capabilities of OpenPetra.

  *For additional detailed information, see the summary for csharp/ICT/Petra/Shared/lib/MConference.*

### csharp/ICT/Petra/Shared/lib/MCommon

This module implements core data structures, constants, type definitions, and utility functions that are leveraged throughout the system. The module provides these capabilities to the OpenPetra program:

- Standardized constants and type definitions for system-wide consistency
- Partner classification and address handling utilities
- Form letter generation and mail merge functionality
- Typed datasets for structured data management
- Country-specific information retrieval

#### Identified Design Elements

1. **Centralized Constants Management**: The module defines system-wide constants for isolation levels, data types, form design codes, and localized strings to ensure consistency across the application.

2. **Partner Classification System**: Implements conversion utilities between different representations of partner types (person, family, organization, church) to standardize handling across modules.

3. **Address Handling Framework**: Provides default location and addressee types based on partner classification to ensure consistent address management.

4. **Form Letter Generation**: Comprehensive data structures for mail merge operations, supporting various entity types with specialized formatting for different contexts.

5. **Cached Data Access**: Utilities for retrieving country-specific information from cached data tables, improving performance for frequently accessed reference data.

#### Overview
The architecture emphasizes reusability through shared constants and utilities, maintains consistent partner classification across the system, and provides robust support for document generation. The module serves as a foundation for higher-level application components, with clear separation between data structures, utility functions, and type definitions. The typed datasets provide structured data management capabilities while the form data classes enable sophisticated document generation for various business contexts.

  *For additional detailed information, see the summary for csharp/ICT/Petra/Shared/lib/MCommon.*

### csharp/ICT/Petra/Shared/lib/MHospitality

The program handles financial transactions and manages organizational resources through a modular architecture. This sub-project implements the hospitality management functionality along with the underlying data structures that support accommodation and resource allocation. This provides these capabilities to the OpenPetra program:

- Facility management for buildings and rooms
- Resource allocation and booking system
- Attribute-based room classification and filtering
- Temporal management of accommodations and reservations

#### Identified Design Elements

1. **Strongly-Typed Data Structure**: The module uses TypedDataSets to ensure type safety and provide compile-time checking for hospitality data operations
2. **Relational Data Model**: Implements relationships between buildings, rooms, bookings, and allocations through a well-defined schema
3. **Attribute System**: Flexible room classification through attributes allows for dynamic filtering and categorization
4. **Temporal Resource Management**: Handles time-based allocation of resources with booking start/end date tracking

#### Overview
The architecture follows a data-centric design where the TypedDataSet definition serves as the foundation for all hospitality operations. This approach ensures data integrity while providing a clear interface between the data layer and business logic. The module integrates with other OpenPetra components through imported data structures, maintaining consistency across the application. The hospitality module is designed to be extensible, allowing for additional attributes and booking types to be added as organizational needs evolve.

  *For additional detailed information, see the summary for csharp/ICT/Petra/Shared/lib/MHospitality.*

### csharp/ICT/Petra/Shared/lib/MSysMan

This module implements the foundational data structures and constants required for user management, permissions, and system-wide configuration settings. It serves as the backbone for authentication, authorization, and user preference management across the application.

This module provides these capabilities to the OpenPetra program:

- User account and group management data structures
- Permission and access control definitions
- System-wide configuration constants
- User preference storage and retrieval
- Module-specific default settings management

#### Identified Design Elements

1. **Typed Dataset Architecture**: Strongly-typed datasets provide a structured approach to user and permission data management with built-in validation
2. **Modular Configuration System**: Constants organized by functional area allow for centralized management of system settings
3. **Hierarchical Permission Model**: Multi-level permissions system spanning users, groups, modules, and tables
4. **User Preference Framework**: Standardized approach to storing and retrieving user-specific settings across all modules
5. **Cross-Module Integration**: The system management module interfaces with partner management, finance, and other modules through well-defined constants

#### Overview
The architecture follows a data-centric approach with clear separation between data structures and constants. The typed datasets provide a strongly-typed interface to the underlying database tables, while the constants system offers a centralized repository for configuration values. This design facilitates consistent access to system settings and user preferences throughout the application, particularly for modules with numerous configurable options like partner management.

  *For additional detailed information, see the summary for csharp/ICT/Petra/Shared/lib/MSysMan.*

### csharp/ICT/Petra/Shared/lib/MReporting

The program handles financial transactions and manages organizational data across multiple domains. This sub-project implements the shared reporting infrastructure along with parameter management for report generation. This provides these capabilities to the OpenPetra program:

- Flexible parameter management with hierarchical organization
- Type-safe parameter handling for various data types (Boolean, Decimal, String, DateTime, Int32)
- Parameter lookup with configurable fit strategies
- Report column layout and dimension management
- Serialization support for parameter data to/from JSON format

#### Identified Design Elements

1. Hierarchical Parameter Organization: Parameters are stored with column and level attributes, enabling structured organization of reporting values
2. Flexible Parameter Lookup: Multiple fit strategies (exact, best fit, all columns) allow for precise or approximate parameter matching
3. Type Safety: Strong typing for parameters ensures data integrity throughout the reporting pipeline
4. Serialization Support: Built-in JSON serialization facilitates data exchange between system components
5. Column Management: Dedicated classes for handling report column layouts and dimensions

#### Overview
The architecture emphasizes flexibility through its parameter management system, providing a foundation for OpenPetra's reporting capabilities. The module is designed for maintainability with clear separation between parameter handling and column management. While the TResultList component is marked for removal, the parameter infrastructure is robust and well-structured. The module serves as a critical bridge between data sources and report presentation, enabling non-profit organizations to generate meaningful insights from their operational data.

  *For additional detailed information, see the summary for csharp/ICT/Petra/Shared/lib/MReporting.*

### csharp/ICT/Petra/Shared/lib/Interfaces

This sub-project implements the core interface definitions that enable plugin-based extensibility and standardized communication between system components. The C# Shared Interfaces establish the contract-based architecture that allows OpenPetra to maintain loose coupling while supporting features like external authentication systems and modular component integration.

This provides these capabilities to the OpenPetra program:

- Standardized authentication interfaces for plugin-based user verification
- Namespace dependency management across the application
- Contract definitions for cross-module communication
- Extension points for implementing custom authentication providers (LDAP, etc.)

#### Identified Design Elements

1. **Plugin Architecture**: The interfaces define extension points that allow third-party implementations to integrate with core OpenPetra functionality
2. **Authentication Abstraction**: IUserAuthentication interface provides a consistent API for different authentication mechanisms
3. **Namespace Management**: Structured YAML configuration controls interface dependencies across the application
4. **Modular Design**: Interfaces are organized by functional domains (Common, Finance, Partner, etc.)

#### Overview
The architecture follows a contract-first design approach where interfaces define clear boundaries between system components. This enables the development of loosely coupled modules that can evolve independently while maintaining compatibility through stable interface contracts. The authentication plugin system demonstrates this approach by allowing OpenPetra to support various authentication mechanisms without modifying core application code.

  *For additional detailed information, see the summary for csharp/ICT/Petra/Shared/lib/Interfaces.*

*No project files processed for this directory.*

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #