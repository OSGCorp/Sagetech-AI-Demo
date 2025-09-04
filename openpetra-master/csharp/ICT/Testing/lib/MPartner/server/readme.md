# C# Partner Server of OpenPetra

The C# Partner Server is a core component of OpenPetra that implements the server-side functionality for managing partner data and relationships within the system. This sub-project handles the processing of partner-related operations including partner creation, modification, merging, and reporting. It provides a robust API layer that supports both HTML and JSON responses, enabling both web interface and programmatic access to partner data. This sub-project provides these capabilities to the OpenPetra program:

- Partner data management (creation, editing, deletion)
- Partner relationship handling and merging operations
- Banking details management for partners
- Address validation and selection based on validity dates and preferences
- Reporting functionality with multiple output formats
- CSV import/export capabilities with validation
- GDPR-compliant consent management for contact information

## Identified Design Elements

1. **Multi-format Response Handling**: Controllers support both JSON and HTML output formats, allowing the system to serve both API and web interface consumers
2. **Modular Report Generation**: The reporting system uses parameter-driven templates to generate consistent reports across different partner views (by city, special type, subscription)
3. **Partner Type Validation**: The system enforces business rules regarding partner relationships, merges, and dependencies
4. **Address Selection Logic**: Sophisticated algorithms determine the best address for partners based on validity dates and mailing preferences
5. **Data Consent Management**: Implementation of GDPR-compliant consent tracking for contact information display and usage
6. **Parameterized Testing Framework**: Comprehensive test suite using XML parameter files and expected result templates

## Overview
The architecture follows a modular design with clear separation between partner data management, reporting, and utility functions. The partner management components handle the core CRUD operations while enforcing business rules about relationships and dependencies. The reporting system uses a parameter-driven approach with flexible output formatting. Address management includes sophisticated selection logic to determine the most appropriate address based on multiple criteria. The entire system is thoroughly tested with a comprehensive suite of unit tests covering various partner operations and edge cases.

## Business Functions

### Partner Reporting
- `Reporting/TestData/PartnerBySpecialTypes.Parameters.Expected.xml` : Test data XML file defining expected parameters for the Partner By Special Types report in OpenPetra.
- `Reporting/TestData/PartnerBySpecialTypes.Results.Expected.html` : Expected test results HTML template for Partner By Special Types report in OpenPetra.
- `Reporting/TestData/PartnerByCity.Results.Expected.html` : HTML template for the 'Partner By City' report showing partner information with columns for key, name, address, and contact details.
- `Reporting/TestData/PartnerByCity.Results.Expected.csv` : Expected test results CSV file containing a single partner record with basic identification and location information.
- `Reporting/TestData/PartnerBySpecialTypes.Test.xml` : Test data file for Partner By Special Type report with parameters for filtering and displaying partner information.
- `Reporting/TestData/PartnerBySubscription.Results.Expected.html` : Expected HTML output for a partner subscription report test in OpenPetra.
- `Reporting/TestData/PartnerByCity.Parameters.Expected.xml` : XML test data file defining expected parameters for the Partner by City report in OpenPetra's testing framework.
- `Reporting/TestData/PartnerByCity.Test.xml` : Test data file for the Partner by City report with predefined parameters and column configurations.
- `Reporting/TestData/PartnerBySpecialTypes.Results.Expected.csv` : Expected test results CSV file containing partner data organized by special types for reporting tests.
- `Reporting/PartnerReports.test.cs` : Test suite for partner report functionality in OpenPetra, validating reports by special types, city, and subscription.

### Address Management
- `AddressTools/AddressTools.test.cs` : Test suite for address management tools in OpenPetra, verifying best address selection logic for partners.

### Partner Import/Export
- `PartnerExports/PartnerImportCSV.cs` : Test suite for the CSV partner import functionality in OpenPetra, verifying proper data handling and validation.

### Partner Merging
- `PartnerMerge/TestPartnerMerge.cs` : Tests the partner merge functionality in OpenPetra, verifying merging of different partner types.

### Partner Editing
- `PartnerEdit/TestPartnerEditBankingDetails.cs` : Test class for validating banking details functionality in the Partner Edit module of OpenPetra.
- `PartnerEdit/TestPartnerEdit.cs` : Test suite for partner editing functionality in OpenPetra, focusing on creating, modifying, and deleting different partner types.

## Files
### AddressTools/AddressTools.test.cs

This test file validates the address selection functionality in OpenPetra's partner management system. It tests TAddressTools methods that determine the best address for partners based on date validity and mailing preferences. The test suite creates various address scenarios (expired, current, future) with both mailing and non-mailing addresses, then verifies the system correctly identifies the most appropriate address. Key methods include CreateExpiredAddresses, CreateFutureAddresses, CreateCurrentAddresses, and ActAndAssert, which run the address selection logic and validate results. The tests ensure proper prioritization of current addresses over future ones, and future addresses over expired ones.

 **Code Landmarks**
- `Line 72`: Database reset verification ensures test isolation by confirming location records don't exist before testing
- `Line 173`: Test for equal dates includes a sleep to ensure timestamp differences, testing address selection when dates match
- `Line 254`: Address creation includes partner key in street name to avoid duplicate addresses during testing
- `Line 282`: Verification results handling ensures only non-critical errors are allowed when saving test data
### PartnerEdit/TestPartnerEdit.cs

TestPartnerEdit implements a comprehensive test suite for OpenPetra's partner management functionality. It tests creating, modifying, and deleting various partner types (families, persons, units, churches, organizations, banks, venues) with their associated data. Key tests include saving partners with new or existing locations, handling location propagation in families, and enforcing business rules that prevent deletion of partners with dependencies. The class also tests the SimplePartnerEdit functionality, including GDPR-compliant consent management for contact information. Important methods include TestSaveNewPartnerWithLocation, TestNewPartnerWithLocation0, TestDeleteFamily, TestDeletePerson, and TestCreateSimplePartnerUserAndEdit. The tests verify both successful operations and expected validation failures.

 **Code Landmarks**
- `Line 73`: Tests partner creation with location management, a core functionality in OpenPetra's partner system
- `Line 107`: Tests handling of location 0, which is a special case in the partner location system
- `Line 372`: Comprehensive test suite for partner deletion with business rule validation
- `Line 779`: Tests GDPR-compliant consent management for contact information
- `Line 826`: Demonstrates the data history tracking system with consent permissions
### PartnerEdit/TestPartnerEditBankingDetails.cs

TestPartnerEditBankingDetails implements unit tests for partner banking details management in OpenPetra. It tests the creation, modification, and deletion of banking accounts associated with partners. The test creates a partner, adds banking details, verifies main account requirements, adds secondary accounts, and tests deletion scenarios. The class validates business rules like requiring one main account per partner. It uses the TPartnerEditUIConnector to interact with the database and verifies operations through assertions and verification result collections.

 **Code Landmarks**
- `Line 113`: Creates test partner data with banking details and validates proper saving through the connector
- `Line 175`: Tests adding multiple bank accounts while maintaining main account designation
- `Line 232`: Verifies business rule that requires one bank account to be designated as the main account
- `Line 261`: Tests reassigning main account status when deleting the current main account
### PartnerExports/PartnerImportCSV.cs

TPartnerImportCSVTest implements a test suite for validating the partner import functionality from CSV files in OpenPetra. It tests various scenarios including successful imports, handling of unknown columns, validation of required fields, and date format processing (DMY/MDY). The class contains test methods like TestImportCSV, TestImportCSVUnknownColumn, TestImportCSVWithoutName, TestImportCSV_Dates_DMY, and TestImportCSV_Dates_MDY. Each test verifies different aspects of the import process, checking for proper data validation, error handling, and correct parsing of partner information.

 **Code Landmarks**
- `Line 83`: Tests the core CSV import functionality, verifying proper handling of different partner types (families and organizations)
- `Line 105`: Tests error handling for unknown columns in import files
- `Line 128`: Tests validation of required fields (name, contact details)
- `Line 150`: Tests date format handling with DMY (day-month-year) format
- `Line 186`: Tests date format handling with MDY (month-day-year) format
### PartnerMerge/TestPartnerMerge.cs

TPartnerMergeTest implements comprehensive unit tests for OpenPetra's partner merge functionality. It tests merging between different partner types (Unit, Church, Family, Person, Organisation, Bank, Venue) and verifies which combinations are allowed or disallowed. The tests create test partners, attempt merges, and verify the results including data integrity. Key functionality includes testing partner class compatibility, verifying data transfers during merges, and ensuring related records like gift information, recurring gifts, accounts payable data, and personal data are properly handled during merges. The class contains numerous test methods for different partner type combinations, each with arrangement, action, assertion, and cleanup phases.

 **Code Landmarks**
- `Line 1196`: Tests merging gift information between partners, ensuring donation records are properly transferred
- `Line 1313`: Tests merging recurring gift information, verifying proper handling of scheduled donations
- `Line 1430`: Tests merging accounts payable information between partners, ensuring supplier records are properly transferred
- `Line 1547`: Tests merging personnel management data between partners, ensuring personal records are properly transferred
- `Line 92`: Uses a boolean array to track categories of data to be merged, allowing selective merging of partner information
### Reporting/PartnerReports.test.cs

TPartnerReportsTest implements a test fixture for validating partner reporting functionality in OpenPetra. It tests three main reports: PartnerBySpecialTypes, PartnerByCity, and PartnerBySubscription. The class includes setup and teardown methods for database connections, and helper methods AddAddressPermission and AddSubscription to prepare test data. Each test method configures specific parameters, calculates the report, and validates results against expected output files. The tests verify filtering by special types, city location, and subscription status while considering consent permissions.

 **Code Landmarks**
- `Line 78`: Uses TReportTestingTools to calculate and validate reports against expected HTML output files
- `Line 107`: Demonstrates adding address permissions with consent codes for testing partner filtering
- `Line 157`: Shows how partner subscriptions are added with specific consent codes for testing
### Reporting/TestData/PartnerByCity.Parameters.Expected.xml

This XML file contains expected parameter values for testing the 'Partner by City' report functionality in OpenPetra. It defines column configurations including alignment, captions, formatting, positions, and widths for displaying partner information such as names, keys, cities, postal codes, and classes. The file also specifies report control sources, isolation level, sorting parameters, and filtering criteria specifically for partners in 'Westhausen'. These parameters serve as expected output values for automated tests validating the report generation functionality in the MPartner module.

 **Code Landmarks**
- `Line 31-35`: Uses eComposite parameter type to combine string and datetime values for report timestamp generation
- `Line 46-48`: Defines detail report action that allows opening a partner record in the Partner Edit Screen when clicked
- `Line 49-50`: Implements custom sorting using column references and human-readable descriptions
### Reporting/TestData/PartnerByCity.Results.Expected.csv

This CSV file contains expected test results for partner data queries in OpenPetra's testing framework. It includes a single record with columns for partner ID, name, key, city, postal code, and partner class. The data represents a family partner named 'Thamm, Maria, Mrs.' with partner key '0043013259' and partner class 'FAMILY'. This file serves as a reference for automated tests validating the correct functioning of partner data retrieval operations.
### Reporting/TestData/PartnerByCity.Results.Expected.html

This HTML file serves as an expected results template for testing the 'Partner By City' report in OpenPetra. It defines the structure and formatting of the report output with a header section and a table-like layout using div elements with column classes. The report displays partner information including partner key, name, street address, postal code, city, country, and phone number. The template includes a single sample partner record with some fields marked as 'NO_CONSENT' to demonstrate privacy handling.

 **Code Landmarks**
- `Line 24-31`: Uses a CSS grid-based layout system with column classes (col-1, col-2, etc.) instead of traditional HTML tables for structured data display
- `Line 36`: Shows handling of privacy-sensitive data with 'NO_CONSENT' placeholders for protected information
### Reporting/TestData/PartnerByCity.Test.xml

This XML file contains test data parameters for the 'Partner by City' report in OpenPetra. It defines report configuration settings including the source XML file, isolation level, column calculations, widths, and sorting preferences. The file specifies parameters for filtering by city ('Westhausen'), sorting order (by City, Partner Name, Partner Key), and display configuration with five columns showing Partner Name, Partner Key, City, PostCode, and Partner Class. It also includes date parameters and column width specifications for proper report rendering during testing.

 **Code Landmarks**
- `Line 4`: Defines serializable isolation level for database operations during report generation
- `Line 7`: Sets specific test date (2017-01-04) for consistent report generation results
- `Line 8`: Filters report data by city parameter 'Westhausen'
- `Line 9-11`: Establishes three-level sorting hierarchy for report data organization
- `Line 26`: Uses decimal encoding (4616752568008179712) for column width specification
### Reporting/TestData/PartnerBySpecialTypes.Parameters.Expected.xml

This XML file contains expected parameter values for testing the 'Partner By Special Types' report in OpenPetra. It defines column formatting properties including alignment, captions, formats, positions, and widths for displaying partner information such as keys, names, addresses, and contact details. The file also specifies report control parameters, sorting options, and filtering criteria specifically for partners with the 'LEDGER' special type. These parameters configure how the report displays and sorts partner data, with options for address filtering, solicitation exclusions, and detail report actions.

 **Code Landmarks**
- `Line 59-61`: Uses large decimal values (4611686018427387904+) for precise column positioning in the report layout
- `Line 77-79`: Contains parameters for report header information including title and filtering criteria
- `Line 94`: Defines a detail report action that allows opening the partner in the Partner Edit Screen
- `Line 98`: Specifies 'LEDGER' as the explicit special type filter for the report
### Reporting/TestData/PartnerBySpecialTypes.Results.Expected.csv

This CSV file contains expected test results for partner reporting functionality in OpenPetra, specifically for the 'PartnerBySpecialTypes' report. It lists 20 partner records with their associated data fields including partner key, class (all marked as 'U' for unit), partner name, address information (mostly showing 'No valid address on file'), country code (99), and field name. Each record represents a country or organization like Cambodia, Germany, International Clearing House, etc. The file serves as a reference dataset to validate that the partner reporting functionality correctly retrieves and formats partner data by special types.
### Reporting/TestData/PartnerBySpecialTypes.Results.Expected.html

This HTML file serves as an expected test results template for the 'Partner By Special Types' report in OpenPetra's testing framework. It displays a formatted table of partner information including partner keys, names, addresses, and contact details. The file contains sample data for 20 partners with their respective IDs and names, but with placeholder 'NO_CONSENT' values for street addresses and empty fields for other contact information. The HTML structure uses a responsive grid layout with column classes to organize partner data in a consistent tabular format.

 **Code Landmarks**
- `Line 3`: Uses UTF-8 charset to ensure proper handling of international characters in partner names
- `Line 5`: References an external CSS file for report styling rather than inline styles
- `Line 21`: Implements a responsive grid layout using column classes (col-1, col-2) for consistent data presentation
- `Line 32`: Each partner entry has a unique ID (partner0, partner1, etc.) for potential JavaScript interaction or testing validation
### Reporting/TestData/PartnerBySpecialTypes.Test.xml

This XML file contains test parameters for the 'Partner By Special Type' report in OpenPetra. It defines report configuration including display columns, filtering options, and sorting preferences. Parameters include location filters (city, county, postcode, region, country), address validity options, partner activity status, and display preferences. The file specifies nine display columns with their widths and calculations, including Partner Key, Partner Class, Partner Name, address details, phone number, and field. It also defines sort orders and special filtering options for mailing addresses and solicitation preferences.

 **Code Landmarks**
- `Line 1`: XML parameter file structure used for report testing in OpenPetra's partner module
- `Line 2`: Parameter 'xmlfiles' references external XML data source for the report test
- `Line 4`: Report isolation level set to 'serializable' for transaction consistency
- `Line 25-27`: Three-level sorting configuration with readable description and column references
- `Line 28-52`: Column definitions with width specifications using both integer and decimal values
### Reporting/TestData/PartnerBySubscription.Results.Expected.html

This HTML file serves as an expected test result for the PartnerBySubscription report in OpenPetra's testing framework. It defines the structure and formatting of a report that displays partner subscription information. The document includes column headings for partner details such as PartnerKey, address validity, name, address components, and email. The content section contains sample partner data with two entries showing different levels of data consent (with some fields marked as NO_CONSENT). The file uses CSS classes for layout formatting with a column-based structure.

 **Code Landmarks**
- `Line 18`: Column layout system using CSS classes (col-1, col-2, etc.) for responsive report formatting
- `Line 36`: Use of 'NO_CONSENT' placeholder text indicating privacy compliance for data that cannot be displayed
- `Line 31`: Partner data is organized in separate div blocks with sequential IDs (partner0, partner1)

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #