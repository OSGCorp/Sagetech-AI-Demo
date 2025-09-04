## C# Input/Output Testing Subproject Of OpenPetra

The OpenPetra is a C# program that provides administrative management capabilities for non-profit organizations. The program handles data processing across multiple formats and implements robust I/O operations for both internal and external communications. This sub-project implements comprehensive testing for file format operations and network communications along with email functionality validation. This provides these capabilities to the OpenPetra program:

- Validation of data parsing and serialization across multiple formats (XML, YAML, CSV)
- Testing of HTTP client functionality for API interactions
- Email communication verification including address formatting and SMTP operations
- Template processing and conditional logic validation
- Spreadsheet import/export functionality testing

## Identified Design Elements

1. Multi-Format Data Processing: Test suite validates consistent handling of data across XML, YAML, CSV, Excel and ODS formats
2. Communication Layer Validation: Tests verify both HTTP client operations and email sending capabilities
3. Template Processing Verification: Ensures template engines correctly handle conditional logic and data substitution
4. String Manipulation and Compression: Tests string processing operations including compression algorithms
5. Hierarchical Data Structure Handling: Validates proper processing of parent-child relationships and attribute inheritance

## Overview
The architecture emphasizes comprehensive validation of I/O operations critical to OpenPetra's functionality. The testing framework is organized into unit tests for core I/O operations and integration tests for communication components. Test data files provide diverse scenarios including nested structures, backslash handling in paths, and attribute inheritance. The HTTP client tests verify proper handling of different response codes, while email tests validate address formatting and SMTP operations. This testing infrastructure ensures OpenPetra's I/O components remain robust and reliable across various data formats and communication protocols.

## Business Functions

### File Format Testing
- `TestData/test.yml` : YAML test data file containing a simple nested structure for testing IO functionality in OpenPetra.
- `TestData/test.csv` : CSV test data file containing a simple parent-child hierarchy structure for testing purposes.
- `TestData/testWithInheritedAttributes.xml` : XML test data file containing a simple structure with inherited attributes for OpenPetra testing.
- `TestData/testBackslash.yml` : YAML test data file containing a path with backslashes for file system testing.
- `TestData/test.xml` : XML test data file containing a simple hierarchical structure for testing XML parsing functionality.
- `test.cs` : Test suite for OpenPetra's IO functionality including XML, YAML, CSV parsing, compression, and Excel/ODS file operations.

### Email Communication Testing
- `testStmpSender.cs` : Test fixture for email sending functionality using the TSmtpSender class in OpenPetra.
- `EmailAddressListConverter.cs` : Test suite for email address list conversion functionality in OpenPetra's IO module.

### HTTP Communication Testing
- `testHttpClient.cs` : Test suite for THTTPUtils class that verifies HTTP client functionality for handling different response codes.

### Form Configuration
- `TestData/testReadWrite.yml` : YAML configuration file defining a Donor Recipient History form for the OpenPetra financial management system.

## Files
### EmailAddressListConverter.cs

TTestEmailAddressListConverter implements a test fixture for validating the TSmtpSender.ConvertAddressList method, which transforms email address lists from semicolon-separated to comma-separated format. The test suite verifies proper handling of various email address formats including simple addresses, comma and semicolon separated lists, addresses with display names in quotes, and addresses containing quoted semicolons. Each test case compares the converted output against expected results using NUnit assertions to ensure the email address conversion works correctly for all scenarios.

 **Code Landmarks**
- `Line 59`: Helper method converts MailboxAddress objects back to string format for test verification
- `Line 87`: Tests specifically verify that semicolons in quoted parts of email addresses aren't converted to commas
### TestData/test.csv

This CSV file provides test data for OpenPetra's testing framework, specifically for testing parent-child relationship functionality. It contains four records with three columns: 'name', 'childOf', and 'active'. The data represents a simple hierarchy where 'grandchild1' and 'grandchild2' are children of 'testname', with varying active status values. The file likely supports unit tests for hierarchy management or tree structure functionality in the Common/IO components.

 **Code Landmarks**
- `Line 1`: Uses standard CSV header format with quoted column names
### TestData/test.xml

This XML test file provides sample data for testing XML parsing and processing in the OpenPetra system. It contains a simple hierarchical structure with a root node 'RootNodeInternal' containing two 'XmlElement' nodes with name attributes. The first element has two child elements with active status attributes, while the second is a simple element with only an active attribute. This structure allows testing of XML parsing, node traversal, and attribute handling.

 **Code Landmarks**
- `Line 1`: Uses UTF-8 encoding declaration which is important for internationalization support in OpenPetra
### TestData/test.yml

This YAML test file provides sample data for testing OpenPetra's IO functionality. It defines a simple nested structure with a root node containing two test nodes, each with different child elements and 'active' properties. The structure demonstrates basic YAML syntax with nested nodes and property assignments, likely used for validating YAML parsing and processing capabilities in the OpenPetra system.

 **Code Landmarks**
- `Line 1`: Uses a non-standard YAML format with curly braces for properties instead of the more common colon-space notation
### TestData/testBackslash.yml

This YAML file provides test data for validating backslash handling in file paths. It contains a single test case with a Windows-style path that includes double backslashes in the format 'data\Kontoauszuege\test.csv'. The file is likely used in unit tests to verify proper parsing and handling of backslash characters in file paths across different operating systems.

 **Code Landmarks**
- `Line 2`: Uses double backslashes in YAML which represents a single backslash in the resulting string, important for cross-platform file path testing.
### TestData/testReadWrite.yml

This YAML configuration file defines the structure and behavior of a Donor Recipient History form in OpenPetra's financial management module. It extends a base YAML template and specifies form properties including dimensions, namespace, dataset type, and detail table. The file configures three actions (Close, View, Browse) with their respective button click handlers and defines a layout structure with nested panels for content organization. The form appears to be designed for browsing gift batch transaction details.

 **Code Landmarks**
- `Line 3`: Form extends a base YAML template, demonstrating OpenPetra's component inheritance system
- `Line 5`: Uses strongly-typed dataset (GiftBatchTDS) for data binding
- `Line 9`: Implements the windowSingleTable template pattern for consistent UI presentation
- `Line 13`: Defines action handlers that map UI events to backend code methods
### TestData/testWithInheritedAttributes.xml

This XML test file provides sample data for testing XML parsing functionality in OpenPetra, particularly focusing on attribute inheritance. It defines a simple hierarchical structure with a root node containing two XmlElement nodes, each with the 'active' attribute. The first element contains two child elements, with one inheriting the parent's 'active' attribute value and the other explicitly overriding it. This structure allows testing how XML parsers handle attribute inheritance across nested elements.

 **Code Landmarks**
- `Line 1`: Uses 'active' attribute that gets inherited by child elements unless explicitly overridden
### test.cs

TTestCommonIO implements a comprehensive test suite for the Ict.Common.IO library, validating various file format operations. It tests XML writing/parsing, YAML reading/writing, CSV import/export, string compression, template processing with conditional logic, and Excel/ODS spreadsheet operations. The class contains numerous test methods including TestXmlWriter, TestYmlWriter, TestCSVParser, TestCompressingString, TestTemplateEngine, TestExcelExportFile, and TestODSImportStream. Each test validates specific functionality by creating test data, processing it through the appropriate methods, and verifying the results against expected outputs.

 **Code Landmarks**
- `Line 66`: Creates a test XML document structure that's used across multiple test methods
- `Line 196`: Tests YAML handling with backslash characters which can be problematic in path strings
- `Line 209`: Tests string compression and decompression using PackTools utility
- `Line 219`: Extensive testing of template engine with conditional logic processing
- `Line 408`: Tests Excel file operations including writing, reading and unzipping the XLSX format
### testHttpClient.cs

TTestHttpClient implements a test fixture for validating the HTTP client functionality in OpenPetra. It tests the THTTPUtils class for handling different HTTP response scenarios. The class initializes logging and configuration settings before running tests. Key functionality includes testing how the system handles HTTP 500 error responses and successful HTTP 200 responses. Important methods include Init() for setup, TestHttpUtils500() which verifies exception handling for server errors, and TestHttpUtils200() which confirms successful content retrieval from a website.

 **Code Landmarks**
- `Line 47`: Tests exception handling by attempting to access a non-existent admin endpoint
- `Line 55`: Uses Assert.Throws to verify proper exception handling for HTTP 500 errors
- `Line 62`: Tests successful HTTP requests by verifying specific content is returned from the OpenPetra website
### testStmpSender.cs

TTestSmtpSender implements a test fixture for validating email sending functionality in OpenPetra. The file contains a single test class with two methods: Init() for setup and TestSendMail() for testing the TSmtpSender class. The test initializes logging and configuration settings, then attempts to send a test email if a valid SMTP host is configured. The test verifies that the SendEmail method returns true when successful. Key components include the TSmtpSender class, TLogging for logging, and TAppSettingsManager for configuration.

 **Code Landmarks**
- `Line 56`: Uses a delegate assignment to set the GetSmtpSettings function to GetSmtpSettingsFromAppSettings
- `Line 47`: Test skips execution if the default SMTP host is being used, avoiding test failures in unconfigured environments

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #