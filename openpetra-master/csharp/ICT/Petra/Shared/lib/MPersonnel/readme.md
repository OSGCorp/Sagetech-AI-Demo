# C# Shared Personnel Module Of OpenPetra

The OpenPetra is a C# program that helps non-profit organizations manage administration tasks and reduce operational costs. The program handles contact management and financial accounting while supporting international operations. This sub-project implements personnel management functionality along with organizational hierarchy structures. This provides these capabilities to the OpenPetra program:

- Personnel data validation and relationship management
- Nationality determination from passport records
- Organizational unit hierarchy representation
- Standardized application form type definitions
- Comprehensive personnel data structures through typed datasets

## Identified Design Elements

1. **Family Relationship Validation**: The module implements checks to validate changes to personnel family relationships, particularly focusing on access control implications
2. **Passport-Based Nationality Calculation**: Sophisticated logic for determining a person's nationalities from passport records, including handling of expired documents
3. **Hierarchical Unit Structure**: Tree-based representation of organizational units with parent-child relationships
4. **Typed Dataset Architecture**: Strongly-typed datasets that define relationships between personnel data entities
5. **Personnel Data Categorization**: Enumeration-based organization of personnel information for UI presentation

## Overview
The architecture emphasizes data integrity through validation checks, provides specialized business logic for personnel-specific calculations, and maintains hierarchical organizational structures. The module integrates with other OpenPetra components through shared data structures and constants. The typed datasets create a comprehensive data model that combines information from multiple database tables, supporting both basic staff data management and more complex scenarios like application processing and qualification tracking.

## Business Functions

### Personnel Management
- `Checks.cs` : Provides personnel-related validation checks for family changes that affect user access to system resources.
- `Calculations.cs` : Provides personnel-related calculation functions for determining nationalities from passport data.

### Constants and Reference Data
- `Constants.cs` : Defines constants for the personnel module in OpenPetra.
- `Person.DataElements.cs` : Defines enumeration for individual personnel data items in the Partner Edit screen.

### Organizational Structure
- `UnitTreeNode.cs` : Defines the UnitHierarchyNode class for representing units in a hierarchical organization structure.

### Data Structures
- `data/PersonnelTypedDataSets.xml` : XML definition of typed datasets for personnel management in OpenPetra.

## Files
### Calculations.cs

Calculations class implements functions for the Personnel Module that determine a person's nationalities based on passport information. The main functionality calculates a formatted list of nationalities from passport records, handling main passports, expired passports, and duplicate nationality entries. The class contains methods to process passport data, mark expired passports, and format nationality strings according to specific business rules. Key methods include DeterminePersonsNationalities and DeterminePassportNationality, which work with passport data tables and use a cache retriever delegate to access country information. Constants PASSPORT_EXPIRED and PASSPORTMAIN_EXPIRED are used for marking expired passport statuses.

 **Code Landmarks**
- `Line 52`: Algorithm for nationality determination follows specific business rules with main passports listed first and special handling for expired passports
- `Line 137`: Uses HashSet for automatic elimination of duplicate nationality entries
- `Line 164`: Delegate pattern used to retrieve country data from either client or server cache
### Checks.cs

PersonnelChecks implements functionality to validate changes to personnel family relationships in OpenPetra. The class primarily focuses on warning about family changes that might affect user access to information in the system. The main method WarnAboutFamilyChange checks if changing a person's family would impact their visibility of support information in the intranet system, especially for users with current commitments. It uses a delegate pattern (TDelegateShowFamilyChangeWarning) to display warnings appropriately on either client or server side, returning whether the family change should proceed.

 **Code Landmarks**
- `Line 67`: Uses a delegate pattern to allow client/server flexibility in displaying warnings
- `Line 71`: Early return pattern for optimization when no family change is occurring
- `Line 108`: Detailed warning message construction with internationalization support via Catalog.GetString
### Constants.cs

MPersonnelConstants defines a small set of constants used throughout the personnel module of OpenPetra. The class contains two string constants that represent application form types: APPLICATIONFORMTYPE_SHORTFORM ("SHORT FORM") and APPLICATIONFORMTYPE_LONGFORM ("LONG FORM"). These constants likely provide standardized values for categorizing different types of application forms within the personnel management system.
### Person.DataElements.cs

TIndividualDataItemEnum defines an enumeration of individual data items displayed in the Partner Edit screen's 'Personnel Data' tab group. The enumeration categorizes different types of personnel information such as personal data, emergency contacts, passport details, professional areas, language skills, job assignments, and applications. This enumeration likely serves as a navigation or organization structure for the personnel management interface, allowing the system to reference specific data sections within the personnel records.

 **Code Landmarks**
- `Line 28`: The enum is specifically designed to match UI elements in the Partner Edit screen, showing tight coupling between data model and interface
### UnitTreeNode.cs

UnitHierarchyNode implements a serializable class that represents nodes in a unit hierarchy for the Personnel module. It stores essential information about organizational units including unit keys, parent relationships, descriptions, and type codes. The class provides properties to track a unit's unique identifier (MyUnitKey), its parent unit (ParentUnitKey), descriptive name, type code, and a boolean flag indicating if the unit is 'sticky'. This class serves as a data structure for tree-based representations of organizational hierarchies within the OpenPetra system.

 **Code Landmarks**
- `Line 37`: The class is marked as [Serializable] to support data transfer across application boundaries
### data/PersonnelTypedDataSets.xml

PersonnelTypedDataSets.xml defines the structure of several typed datasets used in the Personnel module of OpenPetra. It creates three main datasets: PersonnelTDS for basic staff data, IndividualDataTDS for comprehensive personal information including applications, qualifications, languages, and job assignments, and ApplicationTDS for managing different types of applications. The file establishes table relationships, defines custom fields and tables, and creates data structures that combine information from multiple database tables. It includes specialized datasets for reporting purposes such as LengthOfCommitmentReportTDS. The file imports several data units from other OpenPetra modules to integrate personnel data with partner, finance, and hospitality information.

 **Code Landmarks**
- `Line 18`: PersonnelTDS combines partner and staff data tables for core personnel management
- `Line 23`: IndividualDataTDS integrates 17+ tables for comprehensive personnel records management
- `Line 74`: Custom SummaryData table aggregates person information from multiple sources
- `Line 101`: JobAssignmentStaffDataCombined creates a view combining job and staff information
- `Line 130`: Custom relations defined between application tables enable proper data relationships

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #