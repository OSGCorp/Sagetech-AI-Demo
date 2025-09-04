# C# Partner Sample Data Subproject Of OpenPetra

The OpenPetra is a C# program that provides administrative management capabilities for non-profit organizations. The program handles contact management, accounting, and sponsorship while supporting data export and international financial operations. This sub-project implements partner data import testing functionality along with sample datasets for validating the system's data handling capabilities. This provides these capabilities to the OpenPetra program:

- Sample data files for partner import validation
- Test cases for various date format handling (DMY and MDY)
- Error handling validation with deliberately invalid data
- Edge case testing with unknown columns and fields
- Sample hierarchical partner structures with different classifications

## Identified Design Elements

1. Multiple Data Format Support: The subproject provides test data in both CSV and YAML formats to validate different import pathways
2. International Date Format Handling: Specific test files target date format variations (DMY/MDY) to ensure proper parsing across regions
3. Validation Testing: Invalid data samples are included to test the system's error handling and validation mechanisms
4. Complex Relationship Modeling: The YAML structure demonstrates partner grouping hierarchies and shared attributes like addresses

## Overview
The architecture emphasizes comprehensive test coverage for the partner import functionality, providing data samples that test both normal operations and edge cases. The test data includes various partner types (individuals, families, organizations) with different classifications and special designations. The subproject ensures OpenPetra's partner management system can handle diverse data formats, international date conventions, and properly validate incoming data while rejecting or flagging problematic records.

## Business Functions

### Partner Import Test Data
- `samplePartnerImport_unknown_column.csv` : Sample CSV file for partner import testing with unknown columns.
- `SampleFilePartnerImport.yml` : Sample YAML file containing mock partner data for testing the partner import functionality in OpenPetra.
- `samplePartnerImport_dates_dmy.csv` : Sample CSV file containing partner import data with dates in day/month/year format.
- `samplePartnerImport_dates_mdy.csv` : Sample CSV file for partner import testing with dates in MM/DD/YY format.
- `samplePartnerImport_invalid.csv` : Sample CSV file containing invalid partner import data for testing purposes.

## Files
### SampleFilePartnerImport.yml

This YAML file provides sample partner data for testing the partner import functionality in OpenPetra. It contains a hierarchical structure of partner groups and individual partners with their personal information. The file includes three partner groups with different classifications (FAMILY, ORGANISATION) and special types (VOLUNTEER, SUPPORTER). Each partner has attributes such as names, addresses, email contacts, and financial details including account numbers and bank sort codes. The data structure demonstrates various test scenarios like partners sharing addresses and account numbers across different banks.

 **Code Landmarks**
- `Line 2`: Comment indicates data was generated using generatedata.com and then modified for testing purposes
- `Line 3`: TODO comments suggest improvements needed for more realistic bank sort codes
- `Line 27`: Intentional test case where two partners share the same account number but at different banks
- `Line 30`: Test case for address deduplication where multiple partners share the same physical address
### samplePartnerImport_dates_dmy.csv

This CSV file provides sample data for testing partner import functionality in OpenPetra with dates formatted in day/month/year (DMY) format. It contains header fields for partner information including personal details (name, gender, date of birth) and contact information (address, email). The file includes two sample records with dates formatted as DD/MM/YY, demonstrating the DMY date format that would need to be properly parsed during import operations.

 **Code Landmarks**
- `Line 2-3`: Date format uses DD/MM/YY pattern (19/08/79) which requires specific date parsing logic to handle correctly in the import process.
### samplePartnerImport_dates_mdy.csv

This is a sample CSV file used for testing partner data import functionality in OpenPetra, specifically with dates formatted in MM/DD/YY format. The file contains two sample partner records with personal information including name, date of birth, gender, address details, and email. The structure demonstrates the expected format for importing partner data with American-style date formatting, containing headers and sample values for each required field.

 **Code Landmarks**
- `Line 1`: CSV header defines the expected field structure for partner import with semicolon separators
- `Line 2-3`: Sample records use MM/DD/YY date format (08/19/79, 09/16/78) specifically for testing date parsing in US format
### samplePartnerImport_invalid.csv

This CSV file contains sample data for testing partner import functionality with invalid records. It includes three rows of partner data with various fields like Partner Key, FirstName, FamilyName, Gender, address information, and Email. The data is deliberately incomplete or invalid, with empty Partner Keys and missing required information, making it suitable for testing validation and error handling in the partner import process.

 **Code Landmarks**
- `Line 1`: Header row defines the expected column structure for partner import data
- `Line 2-4`: Each row contains strategically incomplete data to test different validation scenarios
### samplePartnerImport_unknown_column.csv

This CSV file provides sample data for testing partner import functionality in OpenPetra. It contains header definitions and two sample partner records with personal information including names, addresses, and contact details. The file deliberately includes an unknown column ('Test2') to test how the import system handles unexpected data fields. The data structure represents typical partner information that would be imported into the system.

 **Code Landmarks**
- `Line 1`: Contains an unknown column 'Test2' to test system handling of unexpected fields
- `Line 1`: Uses semicolons as field separators rather than commas despite being a CSV file

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #