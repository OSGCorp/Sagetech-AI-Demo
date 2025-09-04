# C# Personnel Module Of OpenPetra

The OpenPetra Personnel Module is a C# implementation that provides comprehensive personnel management capabilities for non-profit organizations. The module handles organizational structures, staff records, commitments, applications, and reporting functions. This sub-project implements both data management and web service interfaces along with specialized validation logic to maintain data integrity across the personnel domain.  This provides these capabilities to the OpenPetra program:

- Organizational unit hierarchy management and persistence
- Personnel data handling (staff records, commitments, applications)
- Form letter generation with personnel-specific data
- Specialized reporting (birthdays, length of commitment, etc.)
- Extract functionality for filtering personnel by various criteria

## Identified Design Elements

1. **Layered Architecture**: Clear separation between web connectors (application services), validation logic, and data access components
2. **Caching Strategy**: Implementation of cacheable data tables for client-side storage optimization
3. **Comprehensive Validation**: Extensive validation rules for personnel data ensuring business logic compliance
4. **Query-Based Reporting**: Specialized query classes for complex personnel reporting needs
5. **Permission-Based Access Control**: Module-specific permissions (PERSONNEL, PTNRUSER) enforced at method level

## Overview
The architecture follows a service-oriented approach with web connectors providing the primary interface for client applications. Data access is optimized through caching mechanisms for frequently accessed reference data. The module integrates with partner management functionality while maintaining personnel-specific data structures. Validation logic is centralized and comprehensive, ensuring data integrity across various personnel operations. The reporting capabilities leverage SQL queries with parameter-driven filtering to generate specialized personnel reports and extracts.

## Business Functions

### Personnel Management
- `UnitHierarchy.cs` : Manages organizational unit hierarchy in OpenPetra's personnel module with retrieval and saving capabilities.
- `Personnel.cs` : Server-side implementation for personnel management in OpenPetra, handling staff data, applications, and past experiences.
- `Validation.cs` : Server-side validation for personnel data in the OpenPetra system.
- `Reporting.Personnel.UIConnectors.cs` : Server-side connector for personnel reporting in OpenPetra, providing data for emergency, personal, and job assignment reports.
- `validation/Personnel.Validation.cs` : Provides validation functions for personnel data in the OpenPetra system.

### Data Caching
- `Unit.Cacheable.ManualCode.cs` : Provides cacheable DataTables for personnel unit data in OpenPetra's personnel management module.
- `Person.Cacheable.ManualCode.cs` : Provides cacheable data tables for personnel-related information in OpenPetra's MPersonnel module.

### Web Services
- `web/ApplicationData.cs` : Web connector for retrieving personnel application data from the database.
- `web/FormLettersPersonnel.cs` : Provides server-side functionality for generating personnel form letters with data from personnel records.
- `web/IndividualData.cs` : Web connector for accessing and managing individual personnel data in OpenPetra.

### Reporting and Queries
- `queries/ReportLengthOfCommitment.cs` : Implements functionality for generating length of commitment reports for personnel in OpenPetra.
- `queries/ReportBirthday.cs` : Server-side implementation for generating birthday reports in the Personnel module of OpenPetra.

### Partner Extraction
- `queries/ExtractPartnerByField.cs` : Implements a server-side query to extract partners based on field assignments in the Personnel module.
- `queries/ExtractPartnerByCommitment.cs` : Extracts partners based on commitment criteria for personnel management in OpenPetra.
- `queries/ExtractByEvent.cs` : Extracts partner data based on event participation with filtering by status.
- `queries/ExtractPartnerByEventRole.cs` : Extracts partner data based on event roles and status for OpenPetra's personnel management system.

## Files
### Person.Cacheable.ManualCode.cs

TPersonnelCacheable implements functionality for retrieving cacheable DataTables from the MPersonnel.Person sub-namespace that can be stored on the client side. The class provides methods to fetch complete tables with all columns and rows. It includes two specialized methods: GetEventApplicationTypeListTable and GetFieldApplicationTypeListTable, which filter application types based on form type ('SHORT FORM' or 'LONG FORM'). These methods use template rows to load filtered data from the PtApplicationType table. The main entry point is GetCacheableTable which accepts a TCacheablePersonTablesEnum parameter to specify which table to retrieve.

 **Code Landmarks**
- `Line 58`: Uses a wrapper pattern to simplify access to the more complex GetCacheableTable method that has additional parameters
- `Line 66`: Implements template-based filtering for application types, demonstrating a pattern used for data access throughout the system
### Personnel.cs

TPersonnelWebConnector implements server-side functionality for the Personnel module in OpenPetra. It provides methods for saving personnel data, loading staff records, managing commitments, and handling short-term and long-term applications. Key features include converting applications to past experiences, validating personnel data, and retrieving job information. The class interacts with database tables like PmStaffData, PmShortTermApplication, and PmPastExperience through data access classes. Important methods include SavePersonnelTDS, LoadPersonellStaffData, HasCurrentCommitmentRecord, LoadShortTermApplications, and ConvertApplicationsToPreviousExperience.

 **Code Landmarks**
- `Line 69`: Data validation pattern with separate manual and automatic validation methods called before database submission
- `Line 158`: Implementation of sequence-based key generation for job records
- `Line 193`: Complex SQL query construction with dynamic parameters based on search criteria
- `Line 364`: Transaction-based pattern for converting application records to past experience records
- `Line 424`: SQL query that joins multiple tables to determine the date range of events with applications
### Reporting.Personnel.UIConnectors.cs

TPersonnelReportingWebConnector implements server-side functionality for generating personnel reports in OpenPetra. It provides methods to retrieve data for various personnel-related reports including emergency data, personal information, passport expiry, progress reports, end/start of commitment, job assignments, and unit hierarchy. The class contains several key methods like EmergencyDataReport, PersonalDataReport, PassportExpiryReport, ProgressReport, EndOfCommitmentReport, JobAssignmentReport, and StartOfCommitmentReport that query the database and return formatted DataSets or DataTables. It also includes helper methods for retrieving specific personnel data such as passports, skills, languages, emergency contacts, and job assignments. The connector interacts with database transactions and uses TPartnerReportTools for formatting and processing partner data.

 **Code Landmarks**
- `Line 46`: Uses NoRemoting attribute to prevent direct client access to this method while allowing server-side use
- `Line 75`: Implements progress tracking to provide feedback during potentially long-running database operations
- `Line 373`: Uses recursive SQL query with breadth-first traversal to build unit hierarchy reports
- `Line 588`: Helper methods encapsulate common data retrieval patterns for different personnel data types
### Unit.Cacheable.ManualCode.cs

TPersonnelCacheable implements functionality for retrieving cacheable DataTables from the Personnel Unit database that can be stored on the client side. It provides methods to fetch complete tables or filtered data sets related to personnel units. The class includes a general GetCacheableTable method and specialized methods for retrieving outreach lists and conference lists. These methods construct complex SQL queries joining multiple tables (PPartner, PUnit, PLocation, PPartnerLocation, PCountry) to retrieve comprehensive unit information including partner details, locations, and unit-specific attributes. The implementation supports OpenPetra's personnel management functionality by providing efficient data access patterns.

 **Code Landmarks**
- `Line 70`: Method constructs complex SQL joins across five different tables to create a specialized outreach list view
- `Line 111`: Similar to the outreach list query but filters specifically for conference/congregation type units
### UnitHierarchy.cs

TPersonnelWebConnector implements server-side functionality for managing organizational unit hierarchies in the Personnel module. It provides two key methods: GetUnitHeirarchy retrieves the complete organizational structure including root, unassigned, and regular units with their parent-child relationships and type information; and SaveUnitHierarchy persists changes to the hierarchy by replacing the entire unit structure table. The code uses UnitHierarchyNode objects to represent units in the hierarchy, accessing data through various tables including PPartner, PUnit, UUnitType, and UmUnitStructure. Both methods require PERSONNEL module permissions.

 **Code Landmarks**
- `Line 52`: The method builds a hierarchical representation of organizational units with special handling for root and unassigned units
- `Line 146`: Complete replacement approach for hierarchy updates - deletes all existing relationships before inserting new ones
- `Line 47`: Uses RequireModulePermission attribute for security enforcement
### Validation.cs

This file implements server-side validation for the Personnel module in OpenPetra. It contains a partial class TPersonnelWebConnector with a static partial method ValidatePersonnelStaffManual that validates personnel staff data before committing it to the database. The method iterates through each row in the submitted data table and calls TPersonnelValidation_Personnel.ValidateCommitmentManual to perform validation checks on each PmStaffDataRow. Validation results are collected in a TVerificationResultCollection object passed by reference.

 **Code Landmarks**
- `Line 41`: Uses partial classes and methods to separate validation logic from the main web connector implementation
- `Line 44`: Implements row-by-row validation of submitted data tables before database commits
### queries/ExtractByEvent.cs

QueryPartnerByEvent implements a server-side query class for extracting partner data based on event participation. It inherits from ExtractQueryBase and provides functionality to generate partner extracts filtered by event participation and application status. The class contains two main methods: CalculateExtract which initializes the extraction process using an SQL query from a file, and RetrieveParameters which processes client parameters including event selections and status filters (accepted, hold, enquiry, cancelled, rejected, active). The implementation demonstrates parameter handling for SQL queries with proper validation for required selections.

 **Code Landmarks**
- `Line 42`: This class is explicitly marked as an example for more complex reports and extracts
- `Line 59`: Uses TDataBase.ReadSqlFile to load SQL from external file rather than embedding SQL in code
- `Line 77`: Implements validation to ensure at least one event is selected before proceeding
### queries/ExtractPartnerByCommitment.cs

QueryPartnerByCommitment implements functionality to extract partners based on commitment criteria in the personnel management module. It extends ExtractQueryBase and provides the CalculateExtract method that processes parameters and executes SQL queries to find partners matching specific commitment conditions. The class handles various filtering parameters including commitment status, date ranges (start/end dates), field sending/receiving information, and active status. RetrieveParameters method transforms client parameters into SQL parameters for the database query, supporting complex filtering options for commitment-based partner extraction.

 **Code Landmarks**
- `Line 42`: Uses a SQL file rather than embedding SQL directly in code for better maintainability
- `Line 61`: Converts composite parameter values into a list for SQL parameter binding
- `Line 123`: Uses TDbListParameterValue for handling list parameters in SQL queries
### queries/ExtractPartnerByEventRole.cs

QueryPartnerByEventRole implements a server-side query class for extracting partner data based on their roles in events. It inherits from ExtractQueryBase and provides functionality to filter partners by event participation, roles, and application status. The class contains two key methods: CalculateExtract which processes parameters and returns results, and RetrieveParameters which builds SQL parameters from client inputs including event IDs, event roles, and application statuses (accepted, hold, enquiry, cancelled, rejected). The class validates that at least one event and one role are selected before executing the query.

 **Code Landmarks**
- `Line 42`: Class is designed as an example implementation for more complex reports and extracts
- `Line 50`: Uses external SQL file rather than embedding query in code
- `Line 81`: Implements parameter validation with meaningful error messages
### queries/ExtractPartnerByField.cs

QueryPartnerByField implements a server-side component for extracting partners (persons or families) based on their field assignments in OpenPetra's Personnel module. It supports filtering by receiving fields or sending fields, with options to consider only commitments or include worker field assignments. The class extends ExtractQueryBase and overrides methods for special treatment processing. Key functionality includes filtering by date ranges, active status, postal codes, and handling both person and family records. Important methods include CalculateExtract, ProcessReceivingFields, ProcessSendingFields, and ProcessCommitments.

 **Code Landmarks**
- `Line 52`: This class is designed as an example for more complex reports and extracts
- `Line 69`: Uses special treatment flag to process multiple queries instead of default single-query processing
- `Line 105`: Implements logic to handle different query paths based on sending vs receiving fields parameter
- `Line 144`: Contains detailed approach comments explaining the query logic for finding relevant partners
- `Line 261`: Uses SQL template replacement to dynamically build queries for different partner types
### queries/ReportBirthday.cs

QueryBirthdayReport implements server-side functionality for generating birthday reports in OpenPetra's Personnel module. It provides methods to retrieve family keys for specified persons and calculate birthdays based on various selection criteria. The class handles different parameter selections including individual partners, extracts, or current staff. Key functionality includes SQL query preparation with dynamic conditions, date range filtering, partner type filtering, and age calculation. The main methods are GetFamilyKeys and CalculateBirthdays, with a helper method AddPartnerSelectionParametersToSqlQuery for building SQL queries based on user parameters.

 **Code Landmarks**
- `Line 121`: Dynamic SQL query construction using a defines dictionary and parameter list for flexible reporting criteria
- `Line 180`: Date range handling with special logic for when the from-date is after the to-date in the year cycle
- `Line 231`: Runtime calculation of ages based on birthdate and reference date with dynamic column addition
- `Line 246`: Anniversary filtering implementation allowing specific milestone birthdays to be highlighted
### queries/ReportLengthOfCommitment.cs

QueryLengthOfCommitmentReport implements functionality for generating reports about staff members' length of commitment in an organization. The class calculates anniversaries that occur within a reporting period based on commitment records. Key functionality includes calculating elapsed days between dates, determining anniversary dates, and aggregating commitment periods while handling overlaps. The main method GetLengthOfCommitment retrieves commitment data from the database and processes it to identify significant anniversaries. The class handles special cases like open-ended commitments and allows filtering for specific anniversary years (e.g., 5, 10, 15 years).

 **Code Landmarks**
- `Line 37`: Custom ElapsedDays method calculates days between dates, with commented alternative month-based calculation approach
- `Line 52`: AnniversaryDate function calculates anniversary dates by finding when a person completes full years of service
- `Line 119`: Algorithm handles overlapping commitment periods by extending previous commitments rather than double-counting time
- `Line 177`: Uses a view with sorting to process commitment records chronologically by partner
- `Line 225`: Supports filtering for specific anniversary milestones through parameter-based configuration
### validation/Personnel.Validation.cs

TPersonnelValidation_Personnel implements server-side validation for personnel data in OpenPetra. It contains multiple validation methods for different personnel-related data types including commitments, job assignments, passports, personal documents, languages, skills, previous experience, progress reports, personal data, and applications. Each method validates specific data fields against business rules, checking for required values, valid date ranges, valid partner references, and ensuring values aren't marked as unassignable. The validation methods follow a consistent pattern of creating verification results for each validation check and adding them to a collection that's passed by reference. Common validations include checking that partner references are valid UNIT partners, dates are in logical sequences, and values aren't empty or unassignable.

 **Code Landmarks**
- `Line 54`: Validates partner commitments with special handling for UNIT partner references
- `Line 159`: Job assignment validation includes complex date validation chains with conditional logic
- `Line 337`: Passport validation includes special formatting requirements for passport names
- `Line 457`: Validation methods consistently use TVerificationResultCollection for error tracking
- `Line 644`: Event application validation includes time validation (hours/minutes) with range checking
### web/ApplicationData.cs

TApplicationDataWebConnector implements a server-side web connector for accessing application data related to personnel in OpenPetra. The class provides functionality to retrieve outreach codes for conferences or outreach events from the database. It contains a single method, GetOutreachCode, which takes a partner key as input, performs a database transaction to load the corresponding PUnit record, and returns the outreach code if found. The method requires both PERSONNEL and PTNRUSER module permissions to execute.

 **Code Landmarks**
- `Line 43`: Uses RequireModulePermission attribute to enforce security with a logical AND of two permissions
### web/FormLettersPersonnel.cs

TFormLettersPersonnelWebConnector implements server-side functionality for generating form letters with personnel data. It retrieves data from various personnel-related tables to populate form letters for individuals or groups from an extract. The class provides methods to fill form data objects with information about special needs, personal data, passports, languages, skills, work experience, commitments, and applicant details. Key methods include FillFormDataFromExtract, FillFormDataFromPersonnel, and FillFormDataFromApplicant. These methods query database tables like PmSpecialNeed, PmPersonalData, PmPassportDetails, and PmShortTermApplication to populate comprehensive personnel information for form letters.

 **Code Landmarks**
- `Line 75`: Uses RequireModulePermission attribute to enforce security by requiring both PERSONNEL and PTNRUSER permissions
- `Line 85`: Implements progress tracking for potentially long-running extract operations with cancel capability
- `Line 201`: Handles special case for passport data by identifying and prioritizing the main passport
- `Line 307`: Creates degree records from skill records when degrees are mentioned, showing data relationship handling
- `Line 371`: Contains commented code showing previous implementation approach for work experience data
### web/IndividualData.cs

TIndividualDataWebConnector implements server-side functionality for retrieving and managing individual personnel data in OpenPetra. It provides methods to fetch various types of personal information including passport details, languages, special needs, job assignments, and church affiliations. The class supports retrieving summary data for personnel overview screens and detailed information for specific data categories. Key methods include GetData() which loads specific personnel data types, BuildSummaryData() which constructs overview information, and SubmitChangesServerSide() which handles data persistence. The connector interacts with multiple database tables and manages relationships between personnel records, job assignments, and church affiliations while enforcing proper permissions.

 **Code Landmarks**
- `Line 73`: Method uses an enum parameter to determine which specific personnel data to load, allowing flexible data retrieval through a single interface
- `Line 177`: Private implementation handles database transaction management separately from the public API
- `Line 561`: SubmitChangesServerSide method ensures job records exist for assignments by creating them if needed
- `Line 629`: Complex merge operation to synchronize changes between different dataset types

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #