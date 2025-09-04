# C# Reporting Test Data Subproject Of OpenPetra

The OpenPetra is a C# program that provides administrative management capabilities for non-profit organizations. The program handles financial operations and contact management across multiple currencies and jurisdictions. This sub-project implements the testing framework for OpenPetra's reporting system along with sample data sets for validating report generation functionality. This provides these capabilities to the OpenPetra program:

- Standardized test data for report validation
- Structured parameter definitions for report configuration
- Expected output samples for automated testing
- Cross-functional testing across financial and partner management domains

## Identified Design Elements

1. **XML-Based Report Configuration**: Test files define report parameters, data sources, column specifications, and sorting preferences in structured XML format
2. **Multi-Format Test Data**: The framework supports various data formats including CSV input data, XML configuration, and text-based expected output
3. **Domain-Specific Testing**: Specialized test data for different functional areas (Partner Management, Financial Accounting)
4. **Parameter-Driven Testing**: Tests can be configured with specific parameters (ledger numbers, dates, account codes) to validate business logic

## Overview
The architecture follows a consistent pattern of input data, configuration parameters, and expected output for each report type. The test data covers both partner-related reports (Partner by City) and financial reports (Current Accounts Payable), providing comprehensive validation capabilities. The framework enables automated verification of report formatting, data filtering, and calculation logic. This testing infrastructure ensures that OpenPetra's reporting capabilities remain reliable across software updates and modifications.

## Business Functions

### Partner Reports
- `PartnerByCity/simpletest.csv` : CSV test data file containing a single partner record for reporting tests.
- `PartnerByCity/simpletest.xml` : XML configuration file for a Partner by City report in OpenPetra's testing framework.
- `PartnerByCity/simpletest.txt` : Sample test data file for Partner by City report in OpenPetra's reporting system.

### Financial Reports
- `CurrentAccountsPayable/standard.xml` : XML configuration file defining parameters for the Current Accounts Payable report in OpenPetra's testing framework.

## Files
### CurrentAccountsPayable/standard.xml

This XML configuration file provides test parameters for the Current Accounts Payable report in OpenPetra's testing framework. It specifies the XML data source file, report name, and critical financial parameters including ledger number (27), report date (31/12/2009), and accounts payable account number (9100). These parameters are used to configure test scenarios for validating the accounts payable reporting functionality.
### PartnerByCity/simpletest.csv

This CSV file contains sample test data for the PartnerByCity report in OpenPetra's testing framework. It provides a single record with partner information including name, key, location, and classification. The file serves as input data for testing report generation functionality, particularly for partner listing by city. The columns include id, Partner name, Partner Key, City, Post Code, and Partner Class, with one data row representing a family partner named Maria Thamm in Westhausen.
### PartnerByCity/simpletest.txt

This text file contains sample test data for the Partner by City report in OpenPetra's reporting system. It displays a formatted report showing partner information filtered by city (Westhausen). The report includes a header with title, city filter, requestor, version, and date, followed by a table with columns for partner name, key, city, postal code, and partner class. It contains one sample record for a family partner named 'Thamm, Maria, Mrs.'

 **Code Landmarks**
- `Line 1`: Report uses placeholder markers (%%%) for dynamic content like dates and version numbers
- `Line 13`: Demonstrates the tabular data format with field alignment used in OpenPetra's reporting system
### PartnerByCity/simpletest.xml

This XML file defines parameters for a 'Partner by City' report test in OpenPetra's testing framework. It specifies report configuration including data source (partnerbycity.xml), isolation level, column definitions, sorting preferences, and display settings. The parameters define five columns showing Partner Name, Partner Key, City, PostCode, and Partner Class with specific column widths. The file also configures sorting by City, Partner Name, and Partner Key, and includes a filter parameter for the city 'Westhausen'. This test data file serves as input for testing OpenPetra's reporting functionality.

 **Code Landmarks**
- `Line 4`: Uses serializable isolation level for database consistency during report generation
- `Line 7`: Contains a specific test date (2017-01-04) to ensure consistent test results
- `Line 8`: Filters report data for a specific city ('Westhausen')
- `Line 12-14`: Defines a three-level sorting hierarchy for report data organization
- `Line 25`: Uses floating-point decimal representation (4616752568008179712) for column width

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #