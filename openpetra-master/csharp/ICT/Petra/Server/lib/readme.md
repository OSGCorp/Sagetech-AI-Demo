# C# Petra Server Libraries Of OpenPetra

The OpenPetra is a C# program that provides administrative management for non-profit organizations. The program handles financial transactions and organizational data management across multiple currencies and regions. This sub-project implements the server-side business logic and data access components along with the API interfaces that connect to client applications. This provides these capabilities to the OpenPetra program:

- Data persistence and retrieval through ORM (Object-Relational Mapping)
- Business logic implementation for financial, contact, and sponsorship management
- Authentication and authorization services
- Multi-tenant architecture support for hosting multiple organizations
- Cross-platform server deployment capabilities

## Identified Design Elements

1. Service-Oriented Architecture: Core functionality is organized into discrete services that can be accessed through standardized interfaces
2. Data Access Layer Abstraction: Database operations are abstracted through a provider model allowing support for multiple database backends
3. Modular Design: Functionality is separated into distinct modules (accounting, contact management, etc.) that can be maintained independently
4. Internationalization Support: Built-in support for multiple languages and regional formatting requirements
5. Scalable Processing: Batch processing capabilities for handling large data operations efficiently

## Overview
The architecture follows a multi-tier design with clear separation between data access, business logic, and service interfaces. The codebase employs dependency injection patterns to maintain loose coupling between components. Security is implemented through a comprehensive permission system that controls access at both feature and data levels. The server components are designed to support both local deployment and cloud hosting scenarios, with configuration options to adapt to various operational environments.

## Sub-Projects

### csharp/ICT/Petra/Server/lib/MFinance

The module provides a robust multi-currency accounting system with support for general ledger operations, accounts payable, gift management, budgeting, and international clearing house capabilities. This sub-project implements both the business logic and data access layers for financial operations along with specialized import/export functionality for various financial formats. This provides these capabilities to the Petra program:

- Multi-currency accounting with exchange rate management
- Complete gift processing with receipting and tax deductibility tracking
- General ledger operations with batch processing and period-end procedures
- Accounts payable with supplier management and payment processing
- Budget creation, management and forecasting
- International Clearing House (ICH) for cross-ledger transactions

#### Identified Design Elements

1. **Modular Financial Components**: The finance module is organized into distinct functional areas (GL, Gift, AP, Budget, ICH) that share common validation and data access patterns
2. **Transaction-Based Processing**: Financial operations use batch/journal/transaction hierarchies with validation at each level to maintain accounting integrity
3. **Multi-Currency Support**: Comprehensive handling of base, foreign, and international currencies with exchange rate management
4. **Period Management**: Structured accounting periods with month-end and year-end procedures to maintain financial data integrity
5. **Import/Export Framework**: Standardized approaches for importing and exporting financial data in various formats (CSV, MT940, CAMT, SEPA)

#### Overview
The architecture emphasizes accounting integrity through comprehensive validation at multiple levels. The module maintains a clear separation between different financial subsystems while providing integration points through the general ledger. The design supports both interactive operations through web connectors and batch processing for imports and period-end procedures. Data access is optimized through caching mechanisms for frequently accessed financial data, and the module includes extensive reporting capabilities for financial analysis and compliance requirements.

  *For additional detailed information, see the summary for csharp/ICT/Petra/Server/lib/MFinance.*

### csharp/ICT/Petra/Server/lib/MPersonnel

The module handles organizational structures, staff records, commitments, applications, and reporting functions. This sub-project implements both data management and web service interfaces along with specialized validation logic to maintain data integrity across the personnel domain.  This provides these capabilities to the OpenPetra program:

- Organizational unit hierarchy management and persistence
- Personnel data handling (staff records, commitments, applications)
- Form letter generation with personnel-specific data
- Specialized reporting (birthdays, length of commitment, etc.)
- Extract functionality for filtering personnel by various criteria

#### Identified Design Elements

1. **Layered Architecture**: Clear separation between web connectors (application services), validation logic, and data access components
2. **Caching Strategy**: Implementation of cacheable data tables for client-side storage optimization
3. **Comprehensive Validation**: Extensive validation rules for personnel data ensuring business logic compliance
4. **Query-Based Reporting**: Specialized query classes for complex personnel reporting needs
5. **Permission-Based Access Control**: Module-specific permissions (PERSONNEL, PTNRUSER) enforced at method level

#### Overview
The architecture follows a service-oriented approach with web connectors providing the primary interface for client applications. Data access is optimized through caching mechanisms for frequently accessed reference data. The module integrates with partner management functionality while maintaining personnel-specific data structures. Validation logic is centralized and comprehensive, ensuring data integrity across various personnel operations. The reporting capabilities leverage SQL queries with parameter-driven filtering to generate specialized personnel reports and extracts.

  *For additional detailed information, see the summary for csharp/ICT/Petra/Server/lib/MPersonnel.*

### csharp/ICT/Petra/Server/lib/MSponsorship

The program handles contact management, accounting, and data export capabilities. This sub-project implements the sponsorship management functionality, providing a bridge between donors and recipients within the OpenPetra ecosystem. This module provides these capabilities to the Petra program:

- Child record management (creation, editing, deletion)
- Sponsorship relationship tracking
- Photo and documentation management for sponsored children
- Recurring gift arrangement processing
- Donation validation against sponsorship records

#### Identified Design Elements

1. **Cross-Module Integration**: Interfaces with partner, finance, and gift modules to create a cohesive sponsorship management system
2. **Comprehensive Search Functionality**: Implements filtering mechanisms to locate children based on various attributes
3. **Media Management**: Handles upload and association of photos with sponsored children records
4. **Financial Processing**: Validates incoming donations against sponsorship records and manages recurring gift arrangements
5. **Data Validation**: Ensures integrity of sponsorship relationships and financial transactions

#### Overview
The architecture follows a server-side implementation pattern through the TSponsorshipWebConnector class, which exposes methods for all sponsorship-related operations. The module is designed with clear separation between record management, financial processing, and media handling components. It serves as a critical link between the donor management aspects of OpenPetra and the financial tracking systems, enabling non-profits to efficiently administer child sponsorship programs while maintaining proper financial records and communication channels.

  *For additional detailed information, see the summary for csharp/ICT/Petra/Server/lib/MSponsorship.*

### csharp/ICT/Petra/Server/lib/MPartner

The module handles all aspects of partner data management including individuals, families, organizations, churches, banks, and venues. It provides a robust foundation for OpenPetra's administrative capabilities through structured data management, relationship tracking, and communication features.

This sub-project implements partner data management along with address handling, contact logging, and reporting capabilities. This provides these capabilities to the Petra program:

- Partner record management (individuals, families, organizations, churches, banks)
- Address and contact information tracking with "best address" determination
- Extract creation and management for targeted communications
- Form letter generation with country-specific address formatting
- Duplicate detection and partner merging functionality
- Import/export capabilities for partner data

#### Identified Design Elements

1. **Modular Architecture**: The module is organized into logical components (web, processing, queries, common, connect, data, validation) with clear separation of concerns
2. **Data Aggregation Pattern**: Uses specialized aggregates to collect and process related data from multiple sources
3. **UI Connector Pattern**: Implements server-side connectors that handle client requests, process data, and return only necessary information
4. **Caching Strategy**: Employs cacheable data tables for frequently accessed reference data to improve performance
5. **Validation Framework**: Provides comprehensive validation at multiple levels (field, record, cross-record) to ensure data integrity

#### Overview
The architecture emphasizes data integrity through extensive validation, supports complex relationship management between different partner types, and provides flexible search capabilities. The module integrates with other OpenPetra components like accounting and sponsorship through well-defined interfaces. The design supports internationalization with country-specific address formatting and multi-language capabilities. Security is implemented through permission-based access controls for partner data, with special handling for sensitive information.

  *For additional detailed information, see the summary for csharp/ICT/Petra/Server/lib/MPartner.*

### csharp/ICT/Petra/Server/lib/MFinDev

The program handles financial transactions and supports organizational operations across multiple currencies and regions. This sub-project implements the financial development functionality along with web-based access controls for donation management and fundraising activities. This provides these capabilities to the OpenPetra program:

- Donor relationship management interfaces
- Financial development tracking and reporting
- Web-based access to fundraising data
- Permission-controlled financial development operations

#### Identified Design Elements

1. Module-Based Permission System: The TFinDevWebConnector requires specific 'FINANCE-2' module permissions, ensuring proper access control to sensitive financial development data
2. Web Connector Architecture: Implements a connector pattern to bridge web interfaces with the core financial development functionality
3. Extensible Framework: Currently contains placeholder methods designed for future implementation of complete financial development features
4. Namespace Organization: Structured within the Ict.Petra.Server.MFinDev.WebConnectors namespace for clear separation of concerns

#### Overview
The architecture follows OpenPetra's modular design principles, with the financial development module specifically focused on fundraising and donor management capabilities. The web connector provides a standardized interface for accessing these features through web-based clients. While currently in early development with placeholder functionality, the framework is designed to support comprehensive financial development operations including donor tracking, campaign management, and fundraising analytics. The module integrates with OpenPetra's broader financial management system while maintaining its own specialized domain logic.

  *For additional detailed information, see the summary for csharp/ICT/Petra/Server/lib/MFinDev.*

### csharp/ICT/Petra/Server/lib/MConference

The module handles the complete lifecycle of conferences, from setup and registration to attendee management and financial processing. This sub-project implements robust data management patterns and web-based interfaces along with form letter generation capabilities. The Conference Module provides these capabilities to the OpenPetra program:

- Conference setup and configuration management
- Application processing and attendee tracking
- Cost calculation and discount management
- Form letter generation for conference communications
- Data caching for performance optimization

#### Identified Design Elements

1. **Layered Architecture**: Clear separation between data access, business logic, and web service layers for maintainable code organization
2. **Transaction Management**: Database operations are wrapped in transactions to ensure data integrity across related tables
3. **Permission-Based Security**: All public methods require specific module permissions (CONFERENCE, PTNRUSER) for access control
4. **Caching Strategy**: Implements client-side caching of reference data tables to improve performance
5. **Data Validation**: Dedicated validation classes ensure data integrity before database operations

#### Overview
The architecture follows a service-oriented approach with specialized classes handling distinct functional areas: TConferenceMasterData manages conference settings, TApplicationManagement handles registrations, TAttendeeManagement maintains attendee records, and TFormLettersConference generates communications. The module integrates with OpenPetra's partner management system to link conferences with organizing partners and uses dataset-based data transfer objects (TDS) for efficient data exchange. The validation framework ensures business rules are enforced consistently across the application.

  *For additional detailed information, see the summary for csharp/ICT/Petra/Server/lib/MConference.*

### csharp/ICT/Petra/Server/lib/MCommon

This module implements foundational services and utilities that enable data management, validation, caching, and client-server communication. The Common Module serves as the backbone for OpenPetra's modular architecture, providing these essential capabilities:

- Database sequence management for generating unique identifiers
- Cacheable data table handling with validation
- Progress tracking for long-running operations
- Form template management across different modules
- Data extraction and filtering capabilities
- Office-specific data label customization

#### Identified Design Elements

1. **Layered Architecture**: The module is organized into distinct layers including Data Layer, Integration Layer, and Cross-Cutting Concerns, providing clear separation of responsibilities.

2. **Validation Framework**: Comprehensive validation system with specialized validators for different data types and business rules, ensuring data integrity throughout the application.

3. **Caching Mechanism**: Efficient data caching implementation that reduces database load while maintaining data consistency through validation before persistence.

4. **Progress Tracking**: Client-server communication system for monitoring long-running processes, allowing users to track operation status and cancel if needed.

5. **Extensible Data Processing**: The ProcessDataChecks system enables automated data consistency verification through configurable SQL-based rules with reporting capabilities.

#### Overview

The architecture emphasizes reusability through shared utilities and validation logic, maintains data integrity via comprehensive validation, and provides efficient client-server communication patterns. The module serves as a foundation for other OpenPetra components, offering essential services like sequence generation, caching, and progress tracking that enable the application's non-profit management capabilities while ensuring data consistency and reliability.

  *For additional detailed information, see the summary for csharp/ICT/Petra/Server/lib/MCommon.*

### csharp/ICT/Petra/Server/lib/MHospitality

The program handles financial transactions and contact management across international boundaries. This sub-project implements the hospitality management functionality within the OpenPetra ecosystem, though it is currently in a preliminary implementation state with placeholder functionality.

The C# Hospitality Module is designed to provide these capabilities to the OpenPetra program:

- Conference and event management functionality
- Participant registration and tracking
- Resource allocation for hospitality events
- Permission-controlled access through the CONFERENCE module security context

#### Identified Design Elements

1. **Web Connector Architecture**: Follows OpenPetra's server-client communication pattern through dedicated WebConnector classes
2. **Security Integration**: Incorporates OpenPetra's permission-based security model with specific module-level permissions
3. **Modular Design**: Structured as a distinct module (MHospitality) within the larger application architecture
4. **Extensible Framework**: Currently implemented as a placeholder structure ready for feature expansion

#### Overview
The architecture is designed to integrate with OpenPetra's existing systems while maintaining a clear separation of concerns. The module is positioned within the server-side components and exposes functionality through web connectors. While currently in skeletal form, the structure demonstrates adherence to OpenPetra's architectural patterns, preparing for future development of hospitality management features. The module's permission requirements suggest it will handle conference-related functionality, likely supporting non-profit organizations in managing events and participant logistics.

  *For additional detailed information, see the summary for csharp/ICT/Petra/Server/lib/MHospitality.*

### csharp/ICT/Petra/Server/lib/MSysMan

The module handles authentication, authorization, user management, database versioning, and system configuration. This sub-project implements critical security and administrative functionality along with data management capabilities that provide these capabilities to the OpenPetra program:

- User authentication and permission management
- Database version control and upgrade mechanisms
- System and user configuration management
- Security logging and audit trails
- Data import/export functionality

#### Identified Design Elements

1. **Layered Security Architecture**: The module implements multiple security layers including authentication, authorization, and comprehensive audit logging for system access and user activities.
2. **Database Versioning System**: Provides a robust mechanism for tracking database versions and applying incremental upgrades through reflection-based method discovery.
3. **Pluggable Authentication**: Supports both database-based authentication and plugin-based methods for flexible identity management.
4. **Configuration Management**: Implements hierarchical configuration with system-wide defaults and user-specific preferences.
5. **Data Portability**: Offers comprehensive import/export capabilities for database content in compressed YML format.

#### Overview

The architecture follows a service-oriented approach with clear separation between authentication, authorization, and user management concerns. The database upgrade system provides a maintainable path for schema evolution through versioned SQL scripts and corresponding C# handlers. Security features include password quality validation, account locking mechanisms, and comprehensive audit logging. The module serves as the administrative backbone of OpenPetra, providing essential infrastructure services while maintaining strict security controls through permission requirements on all administrative functions.

  *For additional detailed information, see the summary for csharp/ICT/Petra/Server/lib/MSysMan.*

### csharp/ICT/Petra/Server/lib/MReporting

The program handles financial transactions and provides comprehensive reporting capabilities. This sub-project implements a modular reporting framework along with specialized financial, partner, and personnel reporting components. This provides these capabilities to the OpenPetra program:

- Dynamic HTML report generation with SQL query integration
- Multi-format output support (HTML, PDF, Excel)
- Domain-specific reporting functions for Finance, Partners, and Personnel
- GDPR-compliant data filtering based on consent requirements

#### Identified Design Elements

1. **Template-Based Report Generation**: The module uses HTML templates with embedded SQL queries that are processed to generate dynamic reports
2. **Domain-Specific Calculators**: Specialized calculator classes handle different report types (TrialBalance, AccountDetail, PartnerByCity) through reflection-based instantiation
3. **Extensible Function Framework**: Domain-specific reporting functions are implemented in separate classes (TRptUserFunctionsFinance, TRptUserFunctionsPartner, etc.)
4. **Consent-Aware Data Processing**: Reports automatically filter sensitive data based on GDPR consent requirements

#### Overview

The architecture follows a modular design with clear separation between report definition, data calculation, and output formatting. The HTMLTemplateProcessor handles template parsing and parameter substitution, while specialized calculator classes perform domain-specific data processing. Financial reporting includes trial balances, account details, and ledger status tracking. Partner reporting provides functionality for listing partners by location, subscription, or special types. The system supports asynchronous report generation with progress tracking and multiple output formats, making it both flexible and extensible for future reporting needs.

  *For additional detailed information, see the summary for csharp/ICT/Petra/Server/lib/MReporting.*

*No project files processed for this directory.*

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #