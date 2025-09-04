# C# Testing Library Subproject Of OpenPetra

The OpenPetra C# Testing Library is a specialized testing framework designed to support the development and quality assurance of the OpenPetra application. This subproject provides a comprehensive suite of testing utilities tailored specifically for the C# components of OpenPetra, enabling developers to validate functionality across the system's various modules including contact management, accounting, and data processing features. The testing library implements automated test execution and validation alongside mocking capabilities for external dependencies.

This provides these capabilities to the OpenPetra program:

- Unit testing framework customized for OpenPetra's unique architecture
- Integration test utilities for validating cross-module functionality
- Mock data generation for testing database operations
- Test fixtures for common OpenPetra components
- Automated regression testing capabilities

## Identified Design Elements

1. **Test Isolation Framework**: Ensures tests run independently without side effects by providing clean environment setup and teardown
2. **Database Mocking Layer**: Simulates database operations without requiring actual database connections, speeding up test execution
3. **Module-Specific Test Helpers**: Specialized utilities for testing accounting, contact management, and other core modules
4. **Assertion Extensions**: Custom assertion methods tailored to OpenPetra's data structures and business rules
5. **Continuous Integration Support**: Test runners and reporting tools designed to integrate with CI/CD pipelines

## Overview
The architecture follows standard testing patterns while accommodating OpenPetra's specific requirements. Tests are organized hierarchically by module and function, with shared fixtures reducing duplication. The library emphasizes readability and maintainability through descriptive test naming conventions and clear failure messages. Performance considerations include parallel test execution where possible and efficient resource cleanup to minimize testing overhead.

## Sub-Projects

### csharp/ICT/Testing/lib/MFinance

The module handles core financial operations through a layered architecture that separates business logic, data access, and presentation concerns. This sub-project implements double-entry bookkeeping principles and multi-currency transaction processing, delivering these key capabilities to the Petra application:

- Complete General Ledger functionality with chart of accounts management
- Multi-currency transaction processing with automated exchange rate handling
- Financial reporting with configurable report templates
- Budget management and variance analysis
- International Clearing House operations for minimizing currency exchange costs
- Batch processing for efficient transaction management

#### Identified Design Elements

1. **Domain-Driven Financial Model**: Implements accounting principles as domain objects with clear separation from persistence mechanisms
2. **Multi-Currency Engine**: Core transaction processing maintains base and foreign currency values with configurable exchange rate sources
3. **Reporting Framework**: Extensible reporting system with customizable templates and export formats
4. **Data Validation Layer**: Comprehensive validation rules ensure financial data integrity before persistence
5. **Batch Processing System**: Transactions are grouped into batches for review, approval, and posting workflows

#### Overview
The architecture follows accounting best practices with strict data validation and audit trails. The module integrates with other Petra components through well-defined interfaces while maintaining financial data integrity. The reporting system provides both standard financial statements and customizable reports for organizational needs. The International Clearing House functionality is particularly notable for organizations operating across multiple countries, reducing currency exchange costs through internal settlement processes.

  *For additional detailed information, see the summary for csharp/ICT/Testing/lib/MFinance.*

### csharp/ICT/Testing/lib/Reporting

The program handles financial transactions and organizational data management across multiple domains. This sub-project implements the reporting infrastructure along with testing frameworks for report validation. This provides these capabilities to the OpenPetra program:

- Parameter processing and management for report generation
- Financial report generation with configurable outputs
- Test infrastructure for validating report accuracy
- Configuration management specific to the reporting subsystem

#### Identified Design Elements

1. Parameter Management System: The TParameterList class provides a robust framework for handling report parameters, including saving/loading from JSON and parameter retrieval with different fit options
2. Financial Data Simulation: Test utilities create sample ledgers with accounting data to validate report generation in controlled environments
3. Report Output Validation: Automated comparison of generated reports against baseline expectations with support for variable substitution
4. Configuration Isolation: Specialized configuration handling for the reporting system that maintains separation from the main application settings

#### Overview
The architecture emphasizes testability through comprehensive test fixtures, maintains consistent reporting outputs via parameter standardization, and provides flexible configuration options. The reporting module is designed for reliability and accuracy, with clear separation between the reporting engine and test infrastructure. The parameter system supports complex data types including currency formatting and multi-level parameter access, while the testing framework enables automated validation of report outputs against approved baselines.

  *For additional detailed information, see the summary for csharp/ICT/Testing/lib/Reporting.*

### csharp/ICT/Testing/lib/MSponsorship

This sub-project implements the child sponsorship management functionality, providing a structured approach to tracking sponsored children and their relationships with sponsors. This module provides these capabilities to the Petra program:

- Child record management with unique identifiers
- Reminder system for sponsorship-related actions
- Comment tracking for sponsored children
- Sponsorship relationship management between donors and children

#### Identified Design Elements

1. Database Integration: The module connects to a database backend to store and retrieve sponsorship data
2. Random Data Generation: Support for creating test data with randomized child information
3. Verification Systems: Methods to confirm successful creation and modification of sponsorship records
4. Relationship Management: Structured approach to linking sponsors with children
5. Web Connectivity: Uses TSponsorshipWebConnector to facilitate web-based interactions with the sponsorship system

#### Overview

The architecture follows a layered approach with clear separation between data access, business logic, and testing components. The module is designed for testability, with comprehensive test coverage ensuring reliability of core sponsorship functions. The data model supports complex relationships between sponsors and children while maintaining data integrity. Integration with the broader Petra application allows this module to leverage shared components while providing specialized functionality for sponsorship management.

  *For additional detailed information, see the summary for csharp/ICT/Testing/lib/MSponsorship.*

### csharp/ICT/Testing/lib/MPartner

The module serves as the foundation for managing all entity relationships in the system, handling both individual and organizational contacts. This sub-project implements a robust data model with extensive validation logic along with flexible search and retrieval capabilities. The Partner Module provides these capabilities to the OpenPetra program:

- Complete contact management for individuals, organizations, and families
- Address and communication data handling with international format support
- Relationship mapping between different partner entities
- Subscription and publication management
- Customizable partner attribute tracking and classification

#### Identified Design Elements

1. **Domain-Driven Design Structure**: Clear separation between domain entities, repositories, and service layers for maintainable code organization
2. **Flexible Data Access Layer**: Repository pattern implementation allowing for efficient querying and data manipulation
3. **Comprehensive Validation Framework**: Robust validation rules ensure data integrity across all partner-related operations
4. **Event-Based Architecture**: Changes to partner data trigger appropriate events for dependent modules
5. **Internationalization Support**: Built-in handling for different address formats, name conventions, and localization requirements

#### Overview
The Partner Module architecture emphasizes scalability and extensibility, allowing for easy addition of new partner types and attributes. The codebase follows SOLID principles with clear separation between data access, business logic, and presentation concerns. The module integrates with other OpenPetra components through well-defined interfaces, providing a foundation for features like accounting, sponsorship, and reporting functionality.

  *For additional detailed information, see the summary for csharp/ICT/Testing/lib/MPartner.*

### csharp/ICT/Testing/lib/NUnitTools

The program handles financial transactions and contact management across multiple currencies and regions. This sub-project implements specialized testing infrastructure for OpenPetra's automated test suite along with database management utilities for test environments. This provides these capabilities to the OpenPetra program:

- Test database initialization and management
- Event verification and assertion framework
- Test data loading and validation
- Server administration during test execution
- Automated ledger creation for financial testing

#### Identified Design Elements

1. **Database Test Environment Management**: Provides utilities for initializing test databases, loading test data, and resetting database state between tests
2. **Event Testing Framework**: Implements specialized classes for capturing and verifying event raising during test execution
3. **Server Control Interface**: Offers methods to start, stop and control the OpenPetra server during integration tests
4. **Data Verification Tools**: Includes utilities for validating test results against expected values in various formats
5. **Test Data Manipulation**: Provides methods for working with CSV files and SQL data specifically formatted for testing scenarios

#### Overview
The architecture emphasizes test isolation through database reset capabilities, provides comprehensive event verification, and enables automated test setup. The tools are designed for maintainability across the OpenPetra test suite, with clear separation between database management and event verification concerns. The framework supports both unit and integration testing needs, with robust utilities for validating complex financial operations across the application.

  *For additional detailed information, see the summary for csharp/ICT/Testing/lib/NUnitTools.*

### csharp/ICT/Testing/lib/Shared

The program handles financial operations and contact management while reducing operational overhead. This sub-project implements unit testing infrastructure for the Ict.Petra.Shared library components, focusing on validation of core shared functionality that underpins the entire application.  This provides these capabilities to the OpenPetra program:

- Automated validation of shared component functionality
- Verification of critical data conversion operations
- Testing of date and time handling mechanisms
- Regression prevention for core utility methods

#### Identified Design Elements

1. **Test Fixture Architecture**: Implements standardized test fixtures for validating shared library components
2. **Date/Time Conversion Testing**: Focuses on validating bidirectional conversions between DateTime objects and integer representations
3. **Logging Integration**: Incorporates logging initialization in test setup to ensure proper diagnostic capabilities
4. **Isolated Test Cases**: Maintains separate test cases for different conversion scenarios to isolate potential issues

#### Overview
The architecture follows standard unit testing practices with clear test case isolation and proper setup/teardown procedures. The test suite specifically targets the Conversions class functionality, with particular attention to date and time handling which is critical for many OpenPetra operations including financial transactions and scheduling. The tests validate that conversions are lossless and maintain data integrity across the application's shared components.

  *For additional detailed information, see the summary for csharp/ICT/Testing/lib/Shared.*

### csharp/ICT/Testing/lib/Common

The program handles financial transactions and contact management across multiple currencies and regions. This sub-project implements comprehensive unit testing for the Ict.Common library along with configurable logging mechanisms for test execution. This provides these capabilities to the OpenPetra program:

- Validation of core utility functions across different cultures and locales
- Testing of data type operations including the TVariant type
- Verification of string manipulation and formatting operations
- Configurable logging for test execution and debugging

#### Identified Design Elements

1. Cross-Cultural Testing Framework: Tests validate functionality across different cultures, ensuring proper handling of regional formats for dates, numbers, and currencies
2. Comprehensive Common Library Coverage: Test cases cover essential utilities including date parsing, CSV operations, number formatting, and currency handling
3. Flexible Logging Configuration: XML-based log4net configuration allows developers to direct test output to console, files, or filtered views
4. Validation of Localization: Tests verify proper string localization and formatting across different regional settings

#### Overview
The architecture emphasizes thorough validation of common library components that form the foundation of the OpenPetra application. The testing framework ensures reliability across different deployment environments and cultural settings. Log configuration provides developers with flexible debugging options during test execution. The test suite serves as both validation of existing functionality and documentation of expected behavior for developers implementing new features or maintaining existing code.

  *For additional detailed information, see the summary for csharp/ICT/Testing/lib/Common.*

### csharp/ICT/Testing/lib/NUnitPetraServer

The program handles financial transactions and contact management while supporting internationalization requirements. This sub-project implements automated testing infrastructure for the server components along with database connection management for test execution. This provides these capabilities to the OpenPetra program:

- Automated server initialization for integration testing
- Test authentication management
- Database connection establishment and teardown
- Transaction management for test isolation

#### Identified Design Elements

1. **Server Connection Management**: The TPetraServerConnector class provides static methods to initialize and terminate server connections during test execution
2. **Credential Management**: Authentication is handled through configuration files with support for both automatic and specified paths
3. **Session State Management**: Test session information is stored in domain objects for consistent access across test cases
4. **Transaction Isolation**: Proper connection setup ensures tests can run independently without interfering with each other

#### Overview
The architecture emphasizes test isolation through proper connection management, maintains consistent authentication through configuration-based credentials, and provides a reliable foundation for integration testing. The connector is designed for maintainability and extensibility, with clear separation between server initialization and test execution concerns, enabling developers to focus on writing effective tests rather than managing infrastructure details.

  *For additional detailed information, see the summary for csharp/ICT/Testing/lib/NUnitPetraServer.*

*No project files processed for this directory.*

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #