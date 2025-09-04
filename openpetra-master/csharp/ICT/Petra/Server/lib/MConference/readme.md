# C# Conference Module Of OpenPetra

The OpenPetra Conference Module is a C# implementation that provides comprehensive conference management capabilities within the larger OpenPetra non-profit management system. The module handles the complete lifecycle of conferences, from setup and registration to attendee management and financial processing. This sub-project implements robust data management patterns and web-based interfaces along with form letter generation capabilities. The Conference Module provides these capabilities to the OpenPetra program:

- Conference setup and configuration management
- Application processing and attendee tracking
- Cost calculation and discount management
- Form letter generation for conference communications
- Data caching for performance optimization

## Identified Design Elements

1. **Layered Architecture**: Clear separation between data access, business logic, and web service layers for maintainable code organization
2. **Transaction Management**: Database operations are wrapped in transactions to ensure data integrity across related tables
3. **Permission-Based Security**: All public methods require specific module permissions (CONFERENCE, PTNRUSER) for access control
4. **Caching Strategy**: Implements client-side caching of reference data tables to improve performance
5. **Data Validation**: Dedicated validation classes ensure data integrity before database operations

## Overview
The architecture follows a service-oriented approach with specialized classes handling distinct functional areas: TConferenceMasterData manages conference settings, TApplicationManagement handles registrations, TAttendeeManagement maintains attendee records, and TFormLettersConference generates communications. The module integrates with OpenPetra's partner management system to link conferences with organizing partners and uses dataset-based data transfer objects (TDS) for efficient data exchange. The validation framework ensures business rules are enforced consistently across the application.

## Business Functions

### Conference Management
- `web/ConferenceMaster.cs` : Server-side web connector for conference management in OpenPetra, handling conference settings and data operations.
- `web/ConferenceFindForm.cs` : Server-side implementation for conference management operations in OpenPetra's MConference module.
- `ConferenceOptions.cs` : Server-side implementation for managing conference-related options and retrieving field units in OpenPetra.

### Application Management
- `ConferenceApplications.cs` : Server-side management of conference applications in OpenPetra, handling data access for conference registrations and attendees.
- `RefreshAttendees.cs` : Server-side component that manages conference attendee records and outreach code synchronization.

### Data Services
- `web/ServerLookups.DataReader.cs` : Server-side data retrieval functions for conference management in OpenPetra, providing currency, date, and application data access.
- `Cacheable.ManualCode.cs` : Provides cacheable conference data tables for client-side caching in OpenPetra.

### Document Generation
- `web/FormLettersConference.cs` : Provides functionality for generating conference attendee form letters from extracts or for all attendees in the OpenPetra system.

### Data Validation
- `validation/Cacheable.Validation.cs` : Validation module for conference cost type data in OpenPetra's MConference module.
- `validation/Conference.Validation.cs` : Validation module for conference-related data in OpenPetra, ensuring data consistency for standard costs and registration dates.

## Files
### Cacheable.ManualCode.cs

TCacheable implements a partial class that handles retrieving cacheable DataTables for the MConference module in OpenPetra. The class provides methods to fetch complete database tables that can be safely cached on the client side. It includes a wrapper method GetCacheableTable that takes a TCacheableConferenceTablesEnum parameter to specify which table to retrieve. This implementation is part of the server-side functionality that supports the conference management features of the OpenPetra system, allowing efficient data access through client-side caching mechanisms.

 **Code Landmarks**
- `Line 55`: Implements a wrapper method that simplifies access to cacheable conference tables without requiring additional parameters
### ConferenceApplications.cs

TApplicationManagement implements functionality for managing conference applications in OpenPetra's server-side code. It provides methods to retrieve conference applications based on user permissions, event codes, and registration offices. The class handles access control by checking if users have appropriate permissions for specific registration offices, and determines if a user represents the conference organizing office. Key methods include GetApplications() which retrieves application data filtered by various parameters, GetRegistrationOfficeKeysOfUser() which determines offices a user has access to, and LoadApplicationsFromDB() which performs the actual database queries. The class works with ConferenceApplicationTDS datasets containing information about attendees, applications, and related partner data.

 **Code Landmarks**
- `Line 61`: Uses a constant to determine when a user should be considered a conference organizer based on office access count
- `Line 78`: Implements permission-based access control for registration offices using module permissions
- `Line 95`: Special case handling for organizers who get access to all attendees when they have sufficient office permissions
- `Line 220`: Complex SQL query construction with dynamic parameters based on filtering criteria
- `Line 332`: Merges multiple database tables into a single dataset for comprehensive application information
### ConferenceOptions.cs

TConferenceOptions implements server-side functionality for managing conference-related data in OpenPetra. It provides methods to retrieve units with matching outreach codes, get conference information, determine earliest/latest arrival and departure dates, and fetch field units associated with conferences. Key methods include GetOutreachOptions to find units with matching outreach codes, GetConferences to retrieve conference information, GetEarliestAndLatestDate to determine conference timeframes, and GetFieldUnits to retrieve different types of field units (sending fields, receiving fields, outreach options). The class handles various unit relationships including charged fields, registering fields, and conference-specific units, supporting the conference management module of OpenPetra.

 **Code Landmarks**
- `Line 63`: Method retrieves units sharing outreach code prefix with a given unit, essential for conference organization
- `Line 104`: Conference search implementation with flexible pattern matching for outreach codes and conference names
- `Line 167`: Calculates earliest/latest dates across all participants, critical for conference planning
- `Line 252`: Complex method that retrieves different types of field units based on an enum parameter
### RefreshAttendees.cs

TAttendeeManagement implements functionality for managing conference attendees in OpenPetra. The class provides two main methods: RefreshOutreachCode synchronizes outreach codes between conference and application records, and RefreshAttendees ensures all attendee records are properly maintained. The code handles creating new attendee records for valid applications, removing invalid attendees, and updating outreach codes. It works with database transactions to maintain data integrity across multiple tables including PcAttendee, PmShortTermApplication, and PcConference. The class also manages related accommodation and cost records when removing attendees.

 **Code Landmarks**
- `Line 60`: Updates OutreachPrefix in conference record based on Unit record to maintain consistency
- `Line 134`: Comprehensive attendee refresh process that synchronizes data between application and attendee tables
- `Line 196`: Cleanup logic that removes accommodation and extra costs when an attendee is no longer valid
- `Line 232`: Validation logic determines if an attendee should remain in the system based on multiple criteria
### validation/Cacheable.Validation.cs

TValidation_CacheableDataTables implements validation functionality for conference-related cacheable data tables in OpenPetra. The class provides static methods to validate data integrity before database operations. The primary method ValidateConferenceCostType checks if a conference cost type's UnassignableDate field is properly set when the UnassignableFlag is true. It uses TValidationControlHelper to verify date validity and manages verification results through a TVerificationResultCollection. The validation skips deleted data rows and focuses on ensuring data consistency for conference cost management.

 **Code Landmarks**
- `Line 47`: Static partial class design allows for extension in other files while maintaining validation logic separation
- `Line 58`: Validation skips deleted rows, preventing unnecessary validation processing on data being removed
- `Line 72`: Conditional validation based on UnassignableFlag demonstrates context-aware validation logic
### validation/Conference.Validation.cs

TConferenceValidation_Conference provides validation functions for conference data in OpenPetra. It implements two key validation methods: ValidateConferenceStandardCost checks that conference cost entries maintain logical pricing consistency across different option days, and ValidateEarlyLateRegistration ensures that registration dates are valid relative to the conference end date and that early registration dates don't occur after late registration dates. Both methods add appropriate verification results to a collection when validation issues are detected.

 **Code Landmarks**
- `Line 54`: Validates conference costs by comparing each row against all other rows to ensure logical pricing consistency (more days shouldn't cost less)
- `Line 106`: Validates registration dates against conference end date and checks for logical date sequence between early and late registration periods
- `Line 53`: Uses a verification result collection pattern to accumulate validation errors without immediately halting execution
### web/ConferenceFindForm.cs

TConferenceFindForm implements server-side functionality for the Conference Find Form in OpenPetra's MConference module. It provides methods for creating and deleting conferences. The CreateNewConference method creates a new conference record associated with a partner key, setting default values based on unit and partner location data. The DeleteConference method removes a conference and all its related data from multiple tables, with progress tracking. Both methods require the PTNRUSER module permission and operate within database transactions for data integrity.

 **Code Landmarks**
- `Line 53`: Creates a new conference record with default values derived from partner data
- `Line 131`: Implements a multi-table deletion process with progress tracking for user feedback
- `Line 49`: Uses RequireModulePermission attribute for security enforcement
### web/ConferenceMaster.cs

TConferenceMasterDataWebConnector implements server-side functionality for managing conference data in OpenPetra. It provides methods for loading and saving conference settings, including venue information, options, and discounts. The class handles database transactions to retrieve conference data through LoadConferenceSettings, which returns a ConferenceSetupTDS dataset containing conference details. SaveConferenceSetupTDS persists changes to conference data. The class also ensures required conference option types exist through the private CreateOptionTypes method, which initializes standard options like cost calculation methods. All public methods require CONFERENCE module permissions.

 **Code Landmarks**
- `Line 63`: Method loads multiple related database tables in a single transaction for conference settings
- `Line 102`: Accepts changes to dataset before remoting to ensure client receives unmodified rows
- `Line 121`: Creates standard option types if they don't exist, ensuring system consistency
- `Line 126`: Uses serializable isolation level for transaction to prevent concurrency issues
### web/FormLettersConference.cs

TFormLettersConferenceWebConnector implements functionality for generating form letters for conference attendees. The class provides methods to fill form letter data either from an extract (FillFormDataFromExtract) or for all attendees (FillFormDataForAllAttendees). It retrieves attendee information including discovery groups, work groups, and venue details, and populates TFormDataAttendee objects with this data. The implementation uses database transactions to access attendee records and supports progress tracking for potentially lengthy operations. The class works with the Conference module and requires appropriate permissions.

 **Code Landmarks**
- `Line 73`: Uses TProgressTracker to monitor and allow cancellation of potentially lengthy form letter generation operations
- `Line 161`: Overloaded method implementation allows form data population with or without an explicit transaction parameter
- `Line 205`: Retrieves venue information and stores venue key with specific formatting (10-digit format)
### web/ServerLookups.DataReader.cs

TConferenceDataReaderWebConnector implements server-side lookup functions for the MConference module in OpenPetra. It provides methods to retrieve conference-related data such as currency information, start/end dates, outreach types, and application data. Key functionality includes checking if discount criteria or cost types exist, verifying conference existence, counting record references, and retrieving conference applications. The class requires PTNRUSER module permission for all public methods and uses database transactions to ensure data integrity. Important methods include GetCurrency, GetStartDate, GetEndDate, GetOutreachTypes, ConferenceExists, and GetConferenceApplications.

 **Code Landmarks**
- `Line 65`: Database transaction pattern with delegate for safe data access
- `Line 202`: Custom SQL query construction for retrieving outreach types with substring operations
- `Line 219`: Recursive record reference counting system for database integrity checks
- `Line 341`: Refreshes outreach codes before retrieving conference applications to ensure data consistency

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #