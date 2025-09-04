# C# Reporting Module Of OpenPetra

The OpenPetra system is a C# application that provides administrative management capabilities for non-profit organizations. The program handles financial transactions and organizational data management across multiple domains. This sub-project implements the reporting infrastructure along with testing frameworks for report validation. This provides these capabilities to the OpenPetra program:

- Parameter processing and management for report generation
- Financial report generation with configurable outputs
- Test infrastructure for validating report accuracy
- Configuration management specific to the reporting subsystem

## Identified Design Elements

1. Parameter Management System: The TParameterList class provides a robust framework for handling report parameters, including saving/loading from JSON and parameter retrieval with different fit options
2. Financial Data Simulation: Test utilities create sample ledgers with accounting data to validate report generation in controlled environments
3. Report Output Validation: Automated comparison of generated reports against baseline expectations with support for variable substitution
4. Configuration Isolation: Specialized configuration handling for the reporting system that maintains separation from the main application settings

## Overview
The architecture emphasizes testability through comprehensive test fixtures, maintains consistent reporting outputs via parameter standardization, and provides flexible configuration options. The reporting module is designed for reliability and accuracy, with clear separation between the reporting engine and test infrastructure. The parameter system supports complex data types including currency formatting and multi-level parameter access, while the testing framework enables automated validation of report outputs against approved baselines.

## Sub-Projects

### csharp/ICT/Testing/lib/Reporting/TestData

The program handles financial operations and contact management across multiple currencies and jurisdictions. This sub-project implements the testing framework for OpenPetra's reporting system along with sample data sets for validating report generation functionality. This provides these capabilities to the OpenPetra program:

- Standardized test data for report validation
- Structured parameter definitions for report configuration
- Expected output samples for automated testing
- Cross-functional testing across financial and partner management domains

#### Identified Design Elements

1. **XML-Based Report Configuration**: Test files define report parameters, data sources, column specifications, and sorting preferences in structured XML format
2. **Multi-Format Test Data**: The framework supports various data formats including CSV input data, XML configuration, and text-based expected output
3. **Domain-Specific Testing**: Specialized test data for different functional areas (Partner Management, Financial Accounting)
4. **Parameter-Driven Testing**: Tests can be configured with specific parameters (ledger numbers, dates, account codes) to validate business logic

#### Overview
The architecture follows a consistent pattern of input data, configuration parameters, and expected output for each report type. The test data covers both partner-related reports (Partner by City) and financial reports (Current Accounts Payable), providing comprehensive validation capabilities. The framework enables automated verification of report formatting, data filtering, and calculation logic. This testing infrastructure ensures that OpenPetra's reporting capabilities remain reliable across software updates and modifications.

  *For additional detailed information, see the summary for csharp/ICT/Testing/lib/Reporting/TestData.*

## Business Functions

### Testing and Validation
- `testParameters.cs` : Test class for validating the parameter list functionality used in OpenPetra's reporting system.
- `ReportTesting.tools.cs` : Testing utility for OpenPetra reporting functionality with methods to set up test ledgers and validate report outputs.
- `ConfigTest.cs` : Test fixture for verifying configuration file loading in OpenPetra's reporting system.

## Files
### ConfigTest.cs

TConfigTest implements a NUnit test fixture for validating configuration file handling in the OpenPetra reporting system. The class tests whether the correct configuration file is loaded for NUnit tests. It includes setup methods that initialize the application settings manager with a test server configuration file and sets up logging. The main test method, CheckConfigFile(), verifies that values can be properly retrieved from the configuration using TAppSettingsManager. The class notes that while TAppSettingsManager works correctly, the standard .NET ConfigurationManager doesn't seem to use the custom config file.

 **Code Landmarks**
- `Line 43`: Uses a custom configuration file path for testing rather than the default application configuration
- `Line 64`: Documents an issue with System.Configuration.ConfigurationManager not using the custom config file despite connection working
### ReportTesting.tools.cs

TReportTestingTools provides utilities for testing finance reports in OpenPetra. It implements methods to create test ledgers with sample accounting data through SetupTestLedgerWithPostedBatches(), which creates a ledger with basic transactions. CalculateReport() runs reports with specified parameters and saves results to files, while TestResult() compares generated report outputs against expected results with support for variable substitution. The class facilitates automated testing of financial reporting functionality by creating test data, executing reports, and validating outputs against approved baselines.

 **Code Landmarks**
- `Line 55`: Creates a test ledger with predefined transactions for consistent report testing
- `Line 93`: Forces English culture to ensure consistent date and number formatting in reports
- `Line 134`: Uses a dynamic replacement system to handle variable data in report comparison
### testParameters.cs

TParameterListTest implements a test fixture for validating the TParameterList class used in OpenPetra's reporting system. The file contains a single test class with methods to test parameter processing functionality. The TestGeneralParametersProcessing method tests adding parameters, saving/loading from JSON files, parameter existence checking, and parameter retrieval with different fit options. It specifically tests currency formatting, parameter access across levels, and string handling of comma-separated integers.

 **Code Landmarks**
- `Line 65`: Tests parameter retrieval with different fit options including eBestFitEvenLowerLevel which allows accessing parameters from higher levels
- `Line 76`: Changes thread culture to en-GB to ensure consistent formatting behavior during tests
- `Line 79`: Demonstrates parameter persistence by saving to JSON, loading, and verifying content integrity

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #