# C# Partner Module Of Petra

The C# Partner Module is a core component of OpenPetra that implements comprehensive contact management functionality. The module handles all aspects of partner data management including individuals, families, organizations, churches, banks, and venues. It provides a robust foundation for OpenPetra's administrative capabilities through structured data management, relationship tracking, and communication features.

This sub-project implements partner data management along with address handling, contact logging, and reporting capabilities. This provides these capabilities to the Petra program:

- Partner record management (individuals, families, organizations, churches, banks)
- Address and contact information tracking with "best address" determination
- Extract creation and management for targeted communications
- Form letter generation with country-specific address formatting
- Duplicate detection and partner merging functionality
- Import/export capabilities for partner data

## Identified Design Elements

1. **Modular Architecture**: The module is organized into logical components (web, processing, queries, common, connect, data, validation) with clear separation of concerns
2. **Data Aggregation Pattern**: Uses specialized aggregates to collect and process related data from multiple sources
3. **UI Connector Pattern**: Implements server-side connectors that handle client requests, process data, and return only necessary information
4. **Caching Strategy**: Employs cacheable data tables for frequently accessed reference data to improve performance
5. **Validation Framework**: Provides comprehensive validation at multiple levels (field, record, cross-record) to ensure data integrity

## Overview
The architecture emphasizes data integrity through extensive validation, supports complex relationship management between different partner types, and provides flexible search capabilities. The module integrates with other OpenPetra components like accounting and sponsorship through well-defined interfaces. The design supports internationalization with country-specific address formatting and multi-language capabilities. Security is implemented through permission-based access controls for partner data, with special handling for sensitive information.

## Business Functions

### Partner Management
- `processing/Partner.cs` : Server-side implementation for managing family relationships and partner tracking in OpenPetra.
- `web/Partner.cs` : Server-side web connector for partner management operations in OpenPetra
- `web/MergePartners.cs` : Server-side implementation for merging two partner records in OpenPetra, handling all related data across multiple tables.
- `web/MergePartnersCheck.cs` : Validates whether partners can be merged, checking for compatibility and potential issues.
- `web/SimplePartnerEdit.cs` : Server-side connector for partner data management in OpenPetra
- `web/SimplePartnerFind.cs` : Server-side component for simple partner search functionality in OpenPetra, providing methods to find partners by various criteria.
- `Common/Partner.PartnerFind.cs` : Server-side implementation of partner search functionality for OpenPetra's partner management module.
- `Common/Partner.Export.cs` : Exports partner data from OpenPetra database into a typed dataset for external use.
- `connect/Partner.PartnerEdit.cs` : Partner editing UIConnector for managing partner data in OpenPetra's partner module
- `connect/Partner.PartnerFind.cs` : Server-side connector for Partner Find functionality in OpenPetra, handling search queries and results pagination.
- `connect/Partner.PartnerEdit.Validation.cs` : Server-side validation for partner and bank data in OpenPetra's partner management module.

### Contact Management
- `web/Contacts.cs` : Server-side connector for managing partner contact logs and attributes in OpenPetra's partner management module.
- `Common/Mailroom.cs` : Provides functionality to retrieve the last contact date for partners in the OpenPetra system.
- `Common/DataAggregates.ContactDetails.cs` : Provides methods for retrieving and managing partner contact details like phone numbers and email addresses.
- `Common/Calculations.ContactDetails.cs` : Provides server-side calculations for contact details in the Partner module.

### Address Management
- `web/AddressTools.WebConnector.cs` : Web connector for retrieving partner address information in OpenPetra.
- `web/DuplicateAddressCheck.cs` : Server-side component for detecting duplicate addresses in the OpenPetra partner management system.
- `web/PostcodeRegions.cs` : Server-side connector for managing postcode regions and ranges in the OpenPetra partner management system.
- `Common/Calculations.Addresses.cs` : Provides address-related calculation functions for the Partner module in OpenPetra.
- `Common/Calculations.BestAddress.cs` : Provides server-side functionality to determine the best address for a partner in OpenPetra.
- `Common/AddressTools.cs` : Provides address management utilities for partners in OpenPetra, focusing on best address retrieval and country code handling.
- `Common/DataAggregates.PPartnerAddress.cs` : Manages partner address data with functionality for creating, editing, and deleting addresses in OpenPetra.
- `connect/Partner.PartnerLocationFind.cs` : Server-side connector for partner location search functionality in OpenPetra

### Extract Management
- `web/ExtractMaster.cs` : Server-side connector for managing partner extracts, including creation, manipulation, and operations on extract data.
- `queries/ExtractFamilyMembers.cs` : Extracts family members (persons) from families in a base extract for OpenPetra partner management.
- `queries/ExtractPartnerBySubscription.cs` : Server-side implementation for extracting partners based on publication subscriptions with various filtering criteria.
- `queries/ExtractPartnerByRelationship.cs` : Extracts partner data based on relationship criteria from OpenPetra's partner database.
- `queries/ExtractByPartnerCity.cs` : Implements a query to extract partners living in a specific city for reporting purposes.
- `queries/ExtractPartnerBySpecialType.cs` : Implements a query for extracting partners by special type in OpenPetra's partner management system.
- `queries/ExtractFamilyForPersons.cs` : Implements a query to find family records for persons in a base extract.
- `queries/ExtractPartnerByGeneralCriteria.cs` : Extracts partner data based on general criteria like partner class, status, language, and modification details.
- `queries/ExtractPartnerByContactLog.cs` : Backend implementation for extracting partner data based on contact log criteria in OpenPetra.
- `Common/Extracts.cs` : Server-side module for managing partner extracts in OpenPetra, handling creation, deletion, and population of partner lists.
- `connect/Extract.NewExtract.cs` : Connector class for creating and managing partner data extracts in OpenPetra's partner management module.

### Import/Export
- `web/ImportExport.cs` : Handles importing and exporting partner data to/from CSV, ODS, and XLSX files in OpenPetra.
- `web/ImportExport.LocationConversionHelper.cs` : Helper class for converting partner location data into contact details during partner data import/export.
- `web/ImportExportCSV.cs` : Server-side component for importing partner data from CSV files into OpenPetra database
- `web/ImportExportTax.cs` : Server-side implementation for importing and exporting partner tax authority data in OpenPetra.

### Reporting
- `web/Reporting.Partner.UIConnectors.cs` : Server-side connector providing partner data for reporting screens in OpenPetra.
- `web/FormLetters.cs` : Server-side implementation for generating form letters with partner data in OpenPetra
- `Common/ReportTools.cs` : Provides utility functions for partner-related reports in OpenPetra, handling data filtering, extraction, and formatting.
- `Common/FormLetterTools.cs` : Provides tools for building address blocks in form letters for partner communications.

### Partner Reminders
- `processing/ProcessPartnerReminders.cs` : Processes partner reminders by sending emails based on configured schedules and tracking reminder dates.

### Data Consent Management
- `web/Partner.DataHistory.cs` : Manages partner data consent history, tracking permissions for contact methods like email and addresses.

### Server Lookups
- `web/ServerLookups.DataReader.cs` : Server-side data retrieval for partner-related lookups in OpenPetra's MPartner module
- `Common/ServerLookups.PartnerInfo.cs` : Server-side lookup functionality for retrieving partner information in the OpenPetra partner management module.
- `Common/ServerLookups.cs` : Server-side partner lookup functionality for OpenPetra's partner management module

### Mailing
- `Common/Mailing.cs` : Server-side module for handling partner mailing addresses and contact information in OpenPetra.
- `data/Mailing.Cacheable.ManualCode.cs` : Server-side implementation for retrieving cacheable mailing data tables for client-side use.

### Configuration and Setup
- `web/ModuleSetup.cs` : Server-side web connector for managing partner-related setup tables in OpenPetra's Partner module.

### Security
- `Common/Security.cs` : Security module for partner access control in OpenPetra's server component.

### Validation
- `validation/Helper.cs` : Helper class providing validation functions for partner data in OpenPetra's partner management module.
- `validation/Partner.Validation.cs` : Validation module for partner-related data in OpenPetra's partner management system
- `data/Cacheable.Validation.cs` : Validates cacheable partner data tables in OpenPetra's MPartner module, specifically marital status records.

### Utility Functions
- `Common/Checks.cs` : Server-side validation module for partner data in OpenPetra that performs various checks on partner types, data labels, and gift destinations.
- `Common/Calculations.cs` : Provides standardized calculations for partner data formatting, including name formatting, age calculation, and counting various partner-related records.
- `Common/CodeHelper.cs` : Helper class for Partner Module code operations, providing methods to retrieve descriptions and synchronize location data.
- `Common/Common.cs` : Manages partner key generation and allocation in OpenPetra, ensuring unique identifiers for partner records.

### Cacheable Data
- `data/Partner.Cacheable.ManualCode.cs` : Provides cacheable data tables for the MPartner.Partner module that can be stored on the client side.
- `data/Subscriptions.Cacheable.ManualCode.cs` : Server-side implementation for caching subscription-related data tables in OpenPetra's partner management module.

## Files
### Common/AddressTools.cs

TAddressTools implements utility functions for managing partner addresses in OpenPetra. The class primarily handles retrieving the best address for partners based on various criteria. Key functionality includes finding the current best address for a single partner, retrieving addresses for multiple partners from a list, and handling country code information. The class provides methods like GetBestAddress, GetBestAddressOnlySendMail, GetBestAddressForPartners, GetBestAddressForPartnersAsJoinedTable, AddBestAddressForPartner, GetCountryCodeFromSiteLedger, and GetCountryName. These methods work with database transactions and partner location data to determine the most appropriate address for communication purposes.

 **Code Landmarks**
- `Line 91`: Determines the best address among multiple partner locations using the Calculations.DetermineBestAddress method
- `Line 134`: Converts a DataTable of partner keys to a comma-separated list for efficient database querying
- `Line 163`: Uses SQL template files with replaceable parameters for flexible address queries
- `Line 238`: Dynamically adds address columns to an existing partner table with 'addr_' prefix
- `Line 285`: Uses site key division to determine ledger number for country code retrieval
### Common/Calculations.Addresses.cs

The Calculations class in the MPartner.Common namespace implements address-related functionality for the OpenPetra system. It provides methods to determine the best address for a partner, calculate address status (current, future, or expired), and format location strings for display. Key functions include DetermineBestAddress which identifies and marks the most appropriate address for a partner, DeterminePartnerLocationsDateStatus which validates addresses against dates, and DetermineLocationString which formats address components into readable strings. The class handles different address formatting requirements based on country-specific address ordering and supports multiple output formats including comma-separated, line-break separated, and HTML formats.

 **Code Landmarks**
- `Line 74`: Defines a TPartnerLocationFormatEnum that controls how address strings are formatted in output
- `Line 108`: Uses custom DataColumns (Icon and BestAddress) to track address status and selection
- `Line 168`: Implements sophisticated address sorting logic to determine the best address from multiple options
- `Line 245`: Carefully preserves row state (AcceptChanges) to prevent UI updates when only internal status changes
- `Line 498`: Supports three different address ordering formats based on country-specific requirements
### Common/Calculations.BestAddress.cs

ServerCalculations implements methods to determine the 'Best Address' of a partner in the OpenPetra system. The class provides four overloaded DetermineBestAddress methods that retrieve address information from the database based on a partner key. These methods return a TLocationPK object pointing to the best address and optionally output PPartnerLocationRow and PLocationRow objects containing detailed location data. The implementation supports optional database transaction handling and can work with either a provided database connection or create its own.

 **Code Landmarks**
- `Line 52`: Core method that determines the best address for a partner by key
- `Line 75`: Implementation handles database transactions properly with try/finally pattern
- `Line 101`: Method overloading provides flexibility in retrieving different levels of address information
### Common/Calculations.ContactDetails.cs

Calculations.ContactDetails implements server-side functionality for managing partner contact information in OpenPetra. It handles determination and retrieval of primary email addresses, phone numbers, and other contact details from partner attributes. The class provides methods to identify contact types (phone, email, fax), format phone numbers with international prefixes, and manage overall contact settings. Key functionality includes retrieving primary/secondary contact methods, determining contact attributes from database records, and formatting hyperlinks. Important classes include TOverallContSettingKind (enum), TPartnersOverallContactSettings, and TOverallContactSettings, with methods like GetPrimaryEmailAddress, DeterminePrimaryOrWithinOrgSettings, and DeterminePartnerContactDetailAttributes.

 **Code Landmarks**
- `Line 78`: Defines a flags enum that categorizes different types of contact settings for partners
- `Line 266`: Implementation of a dictionary class that stores contact settings for multiple partners with convenient accessor methods
- `Line 1007`: Method that determines which partner attributes are contact details by checking against system categories
- `Line 1151`: Creates and configures custom data columns for attribute tables to support contact detail functionality
- `Line 1243`: Builds hyperlinks from contact values using a template format with placeholder replacement
### Common/Calculations.cs

Calculations is a partial class containing utility methods for the Partner Module in OpenPetra. It provides functionality for formatting partner names in various styles, calculating formal greetings, determining partner ages, and counting records in partner-related tables. Key methods include DeterminePartnerShortName for standardized name formatting, FormatShortName for converting between different name formats, FormalGreeting for generating appropriate letter salutations, and CalculateAge for determining age from birthdate. The class also includes several CalculateTabCounts methods for counting addresses, contact details, subscriptions, and relationships in partner data tables.

 **Code Landmarks**
- `Line 46`: This is a partial class with additional functionality in separate files for Addresses and Contact Details
- `Line 171`: FormatShortName implements multiple name formatting options through an enum-based approach
- `Line 240`: FormalGreeting uses string replacements to handle gender-specific greeting formats
- `Line 299`: Age calculation handles edge cases where the calculation date falls before birthday in current year
### Common/Checks.cs

Checks implements server-side validation functions for the Partner Module in OpenPetra. It provides methods to verify if a partner type exists in a partner type table, whether data labels exist for a specific partner class, and if a partner is an ex-worker based on gift destination history. Key methods include HasPartnerType() which checks if a partner type exists with exact or partial matching, HasPartnerClassLocalPartnerDataLabels() which verifies data label existence for partner classes, and PartnerIsExWorker() which determines if a partner was previously but is no longer an active worker based on gift destination records.

 **Code Landmarks**
- `Line 46`: Implements flexible partner type matching with both exact and prefix-based search options
- `Line 90`: Uses cacheable data tables for efficient partner class data label lookups
- `Line 111`: Determines ex-worker status through temporal analysis of gift destination records
### Common/CodeHelper.cs

PartnerCodeHelper implements utility functions for the Partner Module in OpenPetra. It provides methods to retrieve descriptions for partner-related codes and synchronize location data between tables. The class contains two main functions: GetMaritalStatusDescription retrieves descriptions for marital status codes from a cache, and SyncPartnerEditTDSPartnerLocation updates partner location records with data from location records, including address details and metadata like creation timestamps. These methods support both single row and table-wide synchronization operations, with options to preserve change tracking.

 **Code Landmarks**
- `Line 42`: Uses a delegate parameter to abstract cache retrieval, allowing the same code to work with both client-side and server-side caches
- `Line 70`: Synchronization method handles both individual rows and entire tables through method overloading
- `Line 98`: Conditional timestamp handling ensures null values don't cause exceptions during synchronization
### Common/Common.cs

TNewPartnerKey implements functionality for creating and managing unique partner keys in the OpenPetra system. It handles the generation of new partner identifiers, ensuring they don't conflict with existing records. Key functionality includes retrieving the next available partner key, submitting user-selected keys for validation, and reserving blocks of keys for batch operations. The class provides methods GetNewPartnerKey() to retrieve the next available key, SubmitNewPartnerKey() to validate and commit a user's key choice, and ReservePartnerKeys() to allocate multiple keys at once. It interacts with the database to maintain the PPartnerLedger table that tracks key allocation and prevents duplicate keys.

 **Code Landmarks**
- `Line 72`: Implements a transaction-based approach to retrieve the next available partner key while checking for existing records
- `Line 111`: Uses isolation level RepeatableRead to prevent race conditions when multiple users request partner keys simultaneously
- `Line 148`: Updates the LastPartnerId field to track allocated keys, ensuring system-wide uniqueness
- `Line 213`: Provides batch key reservation functionality with collision detection for bulk partner creation
### Common/DataAggregates.ContactDetails.cs

TContactDetailsAggregate implements static methods for retrieving contact information for partners in the OpenPetra system. It provides functionality to get primary phone numbers, email addresses, fax numbers, and within-organization contact details. The class works with partner attributes stored in the database and uses the Calculations class to process this data. Key methods include GetPrimaryPhoneNumber, GetPrimaryEmailAddress, GetWithinOrganisationEmailAddress, GetPartnersOverallCS, and GetPartnersAdditionalPhoneNumbers. The class serves as a data aggregation layer that simplifies access to partner contact information throughout the application.

 **Code Landmarks**
- `Line 47`: Uses a pattern where contact details are retrieved through specialized methods that handle different types of contact information
- `Line 173`: Implements a flexible method to retrieve multiple types of contact settings using bitwise flags
- `Line 212`: Uses transaction management pattern with delegates for database operations
- `Line 297`: Implements helper method to format phone numbers with international country prefixes
### Common/DataAggregates.PPartnerAddress.cs

TPPartnerAddressAggregate implements logic for managing partner addresses in OpenPetra, handling both location and partner-location relationships. It provides methods for loading address data, checking for similar locations to avoid duplication, applying address security based on user permissions, and handling address propagation for family members. The class enforces business rules when adding, modifying, or deleting addresses, including checking for location reuse, handling location changes that affect multiple partners, and ensuring proper deletion of locations. It also supports special cases like empty locations (LocationKey=0) and restricted addresses with CAN suffix.

 **Code Landmarks**
- `Line 72`: ApplySecurity method implements role-based security for address data, hiding sensitive information from unauthorized users
- `Line 396`: CheckLocationChange handles complex business logic for updating locations shared by multiple partners
- `Line 1084`: PerformLocationFamilyMemberPropagationChecks implements family relationship logic to propagate address changes to family members
- `Line 1216`: PerformSimilarLocationReUseChecks prevents duplicate locations by identifying and reusing similar existing addresses
- `Line 1469`: RemoveLocationFromExtracts ensures data integrity by updating extract references when locations are deleted
### Common/Extracts.cs

TExtractsHandling provides functionality for creating and managing partner extracts in OpenPetra's Partner Module. It implements methods for creating new extracts, deleting extracts, checking if extracts exist, and adding partners to extracts. Key functionality includes creating extracts from lists of partner keys, extending existing extracts, and determining best location addresses for partners. The class handles partner filtering based on criteria like active status, mailing preferences, and solicitation settings. Important methods include CreateNewExtract, DeleteExtract, CheckExtractExists, AddPartnerToExtract, CreateExtractFromListOfPartnerKeys, and ExtendExtractFromListOfPartnerKeys. The module works with database transactions to ensure data integrity when modifying extract records.

 **Code Landmarks**
- `Line 73`: Creates new extracts with unique IDs and names, using database transactions for data integrity
- `Line 221`: Implements partner filtering based on multiple criteria including active status and solicitation preferences
- `Line 445`: Uses a sophisticated algorithm to determine best location addresses for partners in extracts
### Common/FormLetterTools.cs

TFormLetterTools implements functionality for generating formatted address blocks for form letters in OpenPetra's partner management system. It provides methods to build address blocks according to country-specific layouts and templates. The class offers two main public methods: BuildAddressBlock that accepts either an address layout code or a pre-defined address layout block string. The implementation handles various address tokens like names, titles, postal information, and formatting instructions. It supports special cases like contact persons for organizations and churches, and provides formatting options like capitalization. The class interacts with database tables for retrieving address block layouts and partner information.

 **Code Landmarks**
- `Line 52`: Implements fallback mechanism for address layouts - first tries specified country code, then falls back to default '99' country code
- `Line 95`: Uses token-based templating system for address blocks with special markers like [[TitleAndSpace]] that control formatting
- `Line 157`: Implements special contact resolution for churches and organizations to use contact person's details in address blocks
- `Line 385`: Handles line suppression logic to avoid empty lines in address blocks unless explicitly marked with [[NoSuppress]]
### Common/Mailing.cs

TMailing implements server-side functionality for managing partner mailing addresses and contact information in OpenPetra. It provides methods to retrieve partner locations with various filtering options, determine the best address for a partner, and fetch email addresses. Key functions include GetPartnerLocations which retrieves location data with filtering options, GetPartnersBestLocationData which returns the optimal address for a partner, GetPartnersBestLocation which returns just the location primary key, and GetBestEmailAddress/GetBestEmailAddressWithDetails for retrieving primary email addresses. The class works with partner location tables and handles security verification to ensure proper access to partner data.

 **Code Landmarks**
- `Line 77`: Comprehensive SQL query with multiple parameters to filter partner locations based on various criteria
- `Line 222`: Uses Calculations.DetermineBestAddress to intelligently select the optimal address from multiple partner locations
- `Line 347`: Combines location data with email address retrieval for comprehensive contact information
### Common/Mailroom.cs

TMailroom implements a server-side class for the Partner Module that handles mailroom-related business logic. The primary functionality is retrieving the last contact date for a specific partner through the GetLastContactDate method. This method accepts a partner key, an output parameter for the last contact date, and an optional database connection. It queries the PContactLog table, ordered by contact date in descending order, to find the most recent contact entry for the specified partner. If no contact records exist, it returns DateTime.MinValue. The class operates within the Ict.Petra.Server.MPartner.Common namespace and supports OpenPetra's partner management system.

 **Code Landmarks**
- `Line 57`: Method uses an optional database parameter pattern allowing either reuse of existing connections or creation of new ones
- `Line 66`: Uses delegate pattern with ReadTransaction to ensure proper transaction handling
### Common/Partner.Export.cs

TExportAllPartnerData implements functionality to export comprehensive partner data from the OpenPetra database. The class provides a single public static method ExportPartner that loads all data related to a specific partner into a PartnerImportExportTDS dataset. The method handles different partner types (Church, Family, Person, Organisation, Unit, Venue) by loading appropriate related tables based on the partner class. For each partner type, it retrieves specific associated data like locations, attributes, comments, and type-specific information using database access classes within a transaction context.

 **Code Landmarks**
- `Line 43`: Method accepts optional APartnerClass parameter that determines which related data tables to load
- `Line 52`: Uses database transaction pattern with delegate to ensure data consistency during multiple table reads
- `Line 58`: Dynamically determines partner class from database if not provided as parameter
### Common/Partner.PartnerFind.cs

TPartnerFind implements comprehensive partner search functionality for OpenPetra's partner management module. It handles building and executing complex SQL queries based on various search criteria including partner details, contact information, bank details, and location data. The class provides methods for performing searches, retrieving paginated results, filtering by best address, and adding search results to extracts. Key functionality includes building custom WHERE clauses for different search types, handling special cases like phone number formats, supporting various partner statuses, and managing search result pagination. Important methods include PerformSearch, GetDataPagedResult, BuildCustomWhereCriteria, and AddAllFoundPartnersToExtract.

 **Code Landmarks**
- `Line 76`: Uses a separate thread for asynchronous query execution with progress tracking
- `Line 243`: Implements complex SQL query building with dynamic field selection and joins based on search criteria
- `Line 394`: Special handling for international phone numbers with country code lookup
- `Line 583`: Implements partner extract functionality to save search results for later use
- `Line 686`: Filters search results to show only best address for each partner using ServerCalculations
### Common/ReportTools.cs

TPartnerReportTools implements a collection of utility methods for generating partner-related reports in OpenPetra. It provides functionality for filtering address data, extracting partner information, and formatting report data. Key methods include UCAddressFilterDataViewRowFilter and UCExtractChkFilterSQLConditions for applying filter conditions to queries, GetPrimaryPhoneFax and AddPrimaryPhoneEmailFaxToTable for retrieving contact information, ConvertDbFieldNamesToReadable for improving column readability, AddFieldNameToTable for adding field names to data tables, and ColumnMapping for translating column names. The class also includes utilities for handling partner keys and optimizing report queries by replacing unused columns with NULL values.

 **Code Landmarks**
- `Line 74`: Creates dynamic SQL filter conditions based on address parameters for reports
- `Line 151`: Complex SQL query construction with conditional segments for contact information
- `Line 341`: Sophisticated CTE-based query to determine field names for different partner types
- `Line 469`: Performance optimization by replacing unused columns with NULL values
### Common/Security.cs

TSecurity implements server-side security functions for partner access control in OpenPetra. It provides methods to verify if the current user has permission to access specific partner records, with special handling for organization partners that might be foundations. Key functionality includes checking partner access permissions and throwing exceptions when access is denied. Important methods are CanAccessPartner, CanAccessPartnerExc, and CanAccessPartnerByKey, which validate access rights against partner records using database queries and the shared security implementation.

 **Code Landmarks**
- `Line 59`: Implements a dual security check system that first determines if a partner is a foundation organization before applying appropriate security rules
- `Line 126`: Uses exception-based security control flow where denied access throws ESecurityPartnerAccessDeniedException rather than just returning a status code
- `Line 146`: Method supports both exception-throwing and return value approaches to access control depending on caller needs
### Common/ServerLookups.PartnerInfo.cs

TServerLookups_PartnerInfo provides methods for retrieving various types of partner data in the OpenPetra system. It implements functionality to fetch location information, partner attributes, and comprehensive partner details. The class supports different data retrieval patterns including location-only, partner-location-only, and complete partner information. Key methods include LocationPartnerLocationAndRestOnly, PartnerLocationOnly, PartnerAttributesOnly, and AllPartnerInfoData. The implementation handles security checks, applies address security, and provides specialized data retrieval for different partner classes (Person, Family, Unit). The class also supports retrieving family member information, unit structure data, and language preferences from the personnel system.

 **Code Landmarks**
- `Line 75`: Method implements security checks that throw ESecurityPartnerAccessDeniedException if access isn't granted
- `Line 101`: Address security is applied to location data through TPPartnerAddressAggregate.ApplySecurity
- `Line 350`: Partner class-specific data retrieval with specialized handling for Person, Family and Unit types
- `Line 406`: Language codes are sorted by proficiency level so the best language appears first
- `Line 486`: SQL query explicitly excludes merged partners when retrieving family members
### Common/ServerLookups.cs

TPartnerServerLookups provides server-side lookup functionality for the Partner module in OpenPetra. It implements methods to retrieve partner information such as short names, partner classes, status codes, and relationships. Key functions include GetPartnerShortName, VerifyPartner, PartnerInfo, MergedPartnerDetails, and GetRecentlyUsedPartners. The class supports various partner verification operations, checking if partners are active, linked to cost centers, or have valid gift destinations. It also provides access to partner attributes, location data, and foundation status, with appropriate security permissions required for each operation.

 **Code Landmarks**
- `Line 72`: Partner lookup with merged partner handling - returns partner details with option to exclude merged partners
- `Line 225`: Partner verification with partner class validation - checks if partner exists and belongs to specified partner classes
- `Line 305`: Gift destination lookup with date-based filtering using SQL date comparison
- `Line 534`: Partner information retrieval with configurable scope levels for optimizing data transfer
- `Line 729`: Recently used partners retrieval with sorting by date/time and partner class filtering
### connect/Extract.NewExtract.cs

TPartnerNewExtractUIConnector implements a UI connector class for creating new extracts in the m_extract_master data table. It provides functionality to create extracts with unique names and descriptions, create extracts from lists of partner keys, and delete extracts when needed. The class interfaces with TExtractsHandling to perform the actual data operations. Key methods include CreateNewExtract, CreateExtractFromListOfPartnerKeys, and DeleteExtractAgain. The class maintains the ID of the newly created extract in FNewExtractID for reference and potential deletion.

 **Code Landmarks**
- `Line 69`: Creates extracts with validation ensuring extract names are unique, returning success status and extract ID
- `Line 95`: Supports creating extracts from partner key lists with address filtering capabilities
- `Line 132`: Implements cleanup functionality to handle failed extract creation by deleting partially created extracts
### connect/Partner.PartnerEdit.Validation.cs

TPartnerEditUIConnector implements validation methods for partner data in OpenPetra's server-side partner management module. The file contains two key validation methods: ValidatePPartnerManual and ValidatePBankManual, which validate partner and bank data respectively. Both methods iterate through submitted data tables, calling appropriate validation methods from TPartnerValidation_Partner class for each row, and collecting verification results. The validation is performed on the server side before data is committed to the database, ensuring data integrity and consistency.

 **Code Landmarks**
- `Line 31`: Uses partial methods pattern allowing implementation in separate files without modifying the base class
- `Line 36`: Validation messages are deliberately not translated, keeping server messages in English only
### connect/Partner.PartnerEdit.cs

TPartnerEditUIConnector implements a server-side connector that handles data retrieval and saving for the Partner Edit screen. It manages various partner types (Person, Family, Church, Organisation, Bank, Unit, Venue) and their associated data including addresses, contact details, relationships, subscriptions, banking information, and partner types. The connector provides functionality for loading partner data with optional delayed loading, creating new partners, handling address changes, and submitting changes back to the database. Key methods include GetData(), SubmitChanges(), GetDataNewPartner(), and specialized methods for handling different partner aspects like banking details, family members, and partner relationships.

 **Code Landmarks**
- `Line 1045`: Handles banking details with special validation to ensure only one main account exists per partner
- `Line 2042`: Implements a transaction-based approach for submitting changes with verification results
- `Line 2561`: Special handling for location changes to prevent optimistic locking errors when primary keys change
- `Line 2388`: Cascading delete implementation for foundation partners to maintain referential integrity
- `Line 2149`: Partner status changes can be propagated to family members through special processing
### connect/Partner.PartnerFind.cs

TPartnerFindUIConnector implements a server-side connector for the Partner Find screen that handles client requests for searching partners. It wraps a TPartnerFind object to perform searches based on various criteria, manage search results in paginated form, and provide functionality for extracting partner data. Key methods include PerformSearch, GetDataPagedResult, AddAllFoundPartnersToExtract, and FilterResultByBestAddress. The class follows the UIConnector pattern, retrieving data via business objects and transferring only necessary data to clients in manageable chunks.

 **Code Landmarks**
- `Line 92`: Implements pagination pattern for efficiently transferring large result sets to the client
- `Line 107`: Allows searching for partners by bank details, an alternative search method
- `Line 142`: Provides ability to locate a specific partner within paginated results
- `Line 157`: Integrates with Extract functionality to add search results to partner extracts
### connect/Partner.PartnerLocationFind.cs

TPartnerLocationFindUIConnector implements a server-side connector for the Partner Location Search screen. It provides functionality to search for partner locations based on various criteria like address, city, postal code, and country. The class uses a paged data set approach to handle potentially large result sets, executing queries asynchronously in a separate thread. Key methods include PerformSearch() which initiates the search based on criteria data, BuildCustomWhereCriteria() which constructs SQL WHERE clauses, and GetDataPagedResult() which returns paginated results. The connector leverages TDynamicSearchHelper for building search parameters.

 **Code Landmarks**
- `Line 72`: Uses TPagedDataSet to handle large result sets with pagination support
- `Line 91`: Implements asynchronous query execution using a dedicated thread
- `Line 124`: Dynamic WHERE clause construction based on user-provided search criteria
- `Line 133`: Uses TDynamicSearchHelper to build parameterized queries for different location fields
### data/Cacheable.Validation.cs

TPartnerCacheable implements validation functionality for cacheable data tables in the MPartner module of OpenPetra. The file contains a partial class implementation with a single method ValidateMaritalStatusListManual that validates marital status records before they are committed to the database. The method iterates through each row in the submitted table, skipping deleted rows, and calls the ValidateMaritalStatus method from TValidation_CacheableDataTables to perform the actual validation. Validation results are collected in a TVerificationResultCollection object passed by reference.

 **Code Landmarks**
- `Line 42`: Uses partial class implementation pattern to separate validation logic from other functionality
### data/Mailing.Cacheable.ManualCode.cs

TPartnerCacheable implements functionality for retrieving cacheable DataTables from the MPartner.Mailing sub-namespace that can be stored on the client side. The class provides a method GetCacheableTable that accepts a TCacheableMailingTablesEnum parameter to specify which table to retrieve. This method serves as a wrapper for another overloaded GetCacheableTable method that has additional parameters. The implementation supports the OpenPetra partner management system by providing efficient data access for mailing-related operations.

 **Code Landmarks**
- `Line 52`: The method is a wrapper for a more complex overload that returns additional type information
### data/Partner.Cacheable.ManualCode.cs

TPartnerCacheable implements functionality to retrieve cacheable DataTables from the Partner module that can be stored on the client side. The class provides methods to fetch various partner-related data including county lists, foundation owner lists, installed sites, country lists, data labels for partner classes, and contact categories/types. Key functionality includes retrieving complete tables or filtered subsets based on specific criteria. The class implements several private methods for specialized data retrieval operations, each returning a DataTable with specific partner information. Important methods include GetCacheableTable() overloads and specialized private methods like GetCountyListTable(), GetFoundationOwnerListTable(), and GetDataLabelsForPartnerClassesListTable().

 **Code Landmarks**
- `Line 148`: Creates a custom DataTable on-the-fly with dynamic columns and primary key for partner class data labels
- `Line 156`: Uses TOfficeSpecificDataLabelsUIConnector to check data label availability for different partner classes
- `Line 231`: Uses template pattern with typed rows to filter database queries for contact categories
### data/Subscriptions.Cacheable.ManualCode.cs

TPartnerCacheable implements server-side functionality for retrieving and managing cacheable subscription data tables. It provides methods to fetch complete data tables that can be cached on the client side, specifically handling publication-related information. The class includes a GetCacheableTable method that returns specified cacheable tables, an AfterSaving partial method that updates dependent tables when publications are modified, and a GetPublicationInfoListTable method that loads publication data and adds a validity column. The implementation supports OpenPetra's partner management module by enabling efficient data access through caching.

 **Code Landmarks**
- `Line 76`: AfterSaving partial method ensures dependent tables (PublicationInfoList) are updated when PublicationList changes
- `Line 87`: GetPublicationInfoListTable adds a calculated validity column using SQL expression to transform boolean values into localized text
### processing/Partner.cs

This file implements three key classes for partner management in OpenPetra. TPartnerFamilyIDHandling manages family ID assignment, ensuring proper numbering (0-1 for parents, 2+ for children) when adding or moving people between families. TRecentPartnersHandling tracks recently accessed partners for each user, maintaining a configurable list size with special handling for PostgreSQL's predicate locking. TFamilyHandling facilitates moving people between families, updating relationships and family member flags. The code includes detailed validation logic for family ID assignment, with parent IDs (0-1) given priority and special handling for preserving IDs when moving between families.

 **Code Landmarks**
- `Line 270`: Implements recovery mechanism for PostgreSQL's predicate locking errors with transaction rollback and retry logic
- `Line 485`: Family ID assignment follows specific rules prioritizing parent IDs (0-1) and maintaining logical numbering for family members
- `Line 559`: ChangeFamily method handles complex cascading updates when moving a person between families
### processing/ProcessPartnerReminders.cs

TProcessPartnerReminders implements functionality for sending automated reminder emails about partners in OpenPetra. The class handles retrieving reminders that need processing, sending emails to recipients, and updating reminder records. Key functionality includes tracking when reminders were last sent via system defaults, determining which reminders need processing based on dates and active status, sending emails with partner information, and updating reminder records after processing. Important methods include Process(), GetLastReminderDate(), UpdateLastReminderDate(), GetRemindersToProcess(), and SendReminderEmail(). The class manages reminder frequency, event dates, and deactivates reminders when appropriate.

 **Code Landmarks**
- `Line 69`: Uses a database transaction to ensure the entire reminder process succeeds or fails as a unit
- `Line 91`: Implements error handling for SMTP server initialization failures
- `Line 126`: Logic to deactivate reminders when their event date is in the past
- `Line 281`: Uses a parameterized SQL query to prevent SQL injection when retrieving reminders
- `Line 343`: Builds email content dynamically from templates with partner-specific parameters
### queries/ExtractByPartnerCity.cs

QueryPartnerByCity implements a server-side query class for extracting partners based on city location. It inherits from ExtractQueryBase and provides functionality to generate SQL queries for partner extraction based on city parameters. The class includes a static CalculateExtract method that reads an SQL file and processes the query, and an overridden RetrieveParameters method that handles parameter preparation for the SQL query. The implementation serves as an example for creating more complex reports and extracts in the OpenPetra system.

 **Code Landmarks**
- `Line 42`: Class is explicitly designed as an example template for more complex reports and extracts
- `Line 51`: Uses SQL file loading pattern rather than embedding SQL directly in code
- `Line 66`: Demonstrates parameter handling pattern for OpenPetra queries with OdbcParameter usage
### queries/ExtractFamilyForPersons.cs

QueryFamilyExtractForPersons implements a server-side query class that extracts family records for person records contained in a base extract. The class extends ExtractQueryBase and provides functionality to calculate extracts by executing SQL queries against the database. It includes a static CalculateExtract method that creates a new instance of the class and delegates to CalculateExtractInternal, and overrides RetrieveParameters to process input parameters for the SQL query. The main functionality revolves around retrieving family data based on person records from a specified base extract ID.

 **Code Landmarks**
- `Line 54`: Uses a SQL file (Partner.Queries.ExtractFamilyForPersons.sql) rather than embedding SQL directly in code
- `Line 71`: Demonstrates parameter handling pattern for ODBC queries in the OpenPetra system
### queries/ExtractFamilyMembers.cs

QueryFamilyMembersExtract implements a query class that finds all family members (persons) belonging to families specified in a base extract. It extends ExtractQueryBase and provides functionality to calculate extracts by executing SQL queries. The class contains a static CalculateExtract method that creates a new instance and calls the internal calculation method, and an overridden RetrieveParameters method that processes parameters from the client and builds the SQL parameter list for the query. The implementation uses a SQL file named 'Partner.Queries.ExtractFamilyMembers.sql' to perform the actual data extraction.

 **Code Landmarks**
- `Line 45`: Static method creates an instance of the class to perform the extract calculation, demonstrating a factory pattern approach
- `Line 59`: Uses SQL file from external source rather than embedding SQL in code, promoting better separation of concerns
### queries/ExtractPartnerByContactLog.cs

QueryPartnerByContactLog implements functionality for creating partner extracts based on contact log criteria. It extends ExtractQueryBase to handle SQL query execution for filtering partners by various contact log parameters. The class provides the CalculateExtract method that processes parameters and executes a SQL query from an external file. The RetrieveParameters method builds SQL parameter lists from client inputs including contactor, contact code, mailing code, contact attributes, date ranges, and various boolean filters like active status and families-only options.

 **Code Landmarks**
- `Line 39`: Class extends ExtractQueryBase to leverage common extract functionality while implementing contact log-specific filtering
- `Line 53`: Uses external SQL file rather than embedding query in code, improving maintainability
- `Line 77`: Implements parameter handling for complex filtering including comma-separated list parameters
### queries/ExtractPartnerByGeneralCriteria.cs

QueryPartnerByGeneralCriteria implements a server-side class that generates SQL queries for extracting partner data based on various criteria. It inherits from ExtractQueryBase and provides functionality to filter partners by class, status, language, creation/modification dates and users, and other parameters. The class contains two main methods: CalculateExtract (static method that creates a new instance and calls the internal calculation) and RetrieveParameters (protected override that builds SQL parameters from client parameters). The implementation handles various filter conditions including partner-specific tables for church denomination and organization business code.

 **Code Landmarks**
- `Line 53`: Static factory method pattern that creates an instance of the class to perform the actual work
- `Line 70`: Dynamic SQL construction with parameter substitution for secure database queries
- `Line 164`: Conditional SQL table joining based on parameter existence for church denomination and business code
### queries/ExtractPartnerByRelationship.cs

QueryPartnerByRelationship implements functionality to extract partner data based on relationship criteria. It provides methods to calculate extracts either from a specific partner or from an existing extract, filtering by relationship types. The class extends ExtractQueryBase and overrides RetrieveParameters to process user-specified parameters including relationship types, active status, and solicitation preferences. The main method CalculateExtract determines the appropriate SQL query file to use based on selection parameters and delegates to the base class for execution. Important methods include CalculateExtract and RetrieveParameters.

 **Code Landmarks**
- `Line 39`: This class is explicitly marked as an example for implementing more complex reports and extracts
- `Line 52`: Dynamically selects SQL query file based on parameter selection type (extract vs single partner)
- `Line 86`: Validates that at least one relationship option is selected before proceeding
### queries/ExtractPartnerBySpecialType.cs

QueryPartnerBySpecialType implements a server-side class for extracting partners based on their special types in the OpenPetra system. It inherits from ExtractQueryBase and provides functionality to generate SQL queries for partner extraction. The class contains a static CalculateExtract method that processes parameters and executes the extraction, and a RetrieveParameters method that builds SQL parameter lists from client parameters. It handles filtering by special types, dates, active status, family-only filtering, and solicitation preferences.

 **Code Landmarks**
- `Line 54`: Uses SQL file loading pattern rather than embedding SQL directly in code
- `Line 75`: Implements parameter validation with exception throwing for empty special types
- `Line 80`: Uses OdbcListParameterValue for handling multiple special type values in a single parameter
### queries/ExtractPartnerBySubscription.cs

QueryPartnerBySubscription implements functionality to extract partners based on their publication subscriptions and additional filtering criteria. The class extends ExtractQueryBase and provides the CalculateExtract method that processes parameters from the client and executes an SQL query. The RetrieveParameters method builds SQL parameter lists from client parameters, handling publication selections, subscription status, partner types, date ranges, and copy quantity filters. The class supports filtering by active/inactive status, free subscriptions, partner types (persons/families), solicitation preferences, and subscription date ranges.

 **Code Landmarks**
- `Line 54`: Uses SQL file loading pattern for query separation from code
- `Line 69`: Converts comma-separated string parameter into a list for database query
- `Line 75`: Uses specialized OdbcListParameterValue for handling list parameters in SQL
- `Line 101`: Contains duplicate parameter addition that may cause issues
### validation/Helper.cs

TPartnerValidationHelper implements static helper functions for validating partner data in OpenPetra. It defines delegates for partner verification operations and provides methods to check partner existence, status, and relationships. The class uses a delegate pattern where implementation functions must be registered before use. Key functionality includes verifying partner existence, checking active status, determining if partners are linked to cost centers, and validating gift destinations. Important delegates include TVerifyPartner, TPartnerHasActiveStatus, TPartnerIsLinkedToCC, TPartnerOfTypeCCIsLinked, and TPartnerHasCurrentGiftDestination.

 **Code Landmarks**
- `Line 73`: Uses ThreadStatic attribute to ensure delegate references are thread-safe
- `Line 163`: Implements delegate pattern requiring initialization before use to separate interface from implementation
- `Line 176`: Throws InvalidOperationException if delegate not initialized, enforcing proper setup
### validation/Partner.Validation.cs

TPartnerValidation_Partner implements comprehensive validation functionality for partner data in OpenPetra's MPartner module. It provides methods to validate different partner types (Person, Family, Church, Bank) and related entities like subscriptions, relationships, banking details, and contact information. The file contains validation methods for partner attributes, ensuring data integrity through checks on dates, codes, statuses, and relationships. Key methods include ValidatePartnerPersonManual, ValidatePartnerFamilyManual, ValidateSubscriptionManual, ValidateBankingDetails, and IsValidPartner. The class supports both individual field validation and cross-field validation to maintain data consistency across the partner management system.

 **Code Landmarks**
- `Line 69`: Detailed error message template for BIC/Swift code validation with specific format requirements
- `Line 247`: Cross-field validation between marital status and assignable flag with temporal validity checking
- `Line 493`: Comprehensive date validation ensuring logical consistency between multiple date fields
- `Line 1066`: Partner validation with support for multiple valid partner classes and merged status checking
- `Line 1359`: Email and phone number format validation with international code handling
### web/AddressTools.WebConnector.cs

TAddressWebConnector implements a server-side web connector that provides functionality for retrieving address information for partners in the OpenPetra system. The main functionality is the GetBestAddress method, which retrieves the current best address for a specified partner. This method requires the PTNRUSER module permission and connects to the database to fetch address information. It returns location data and the local country name for a given partner key. The class leverages the TAddressTools class to perform the actual address retrieval within a database transaction.

 **Code Landmarks**
- `Line 37`: Uses RequireModulePermission attribute for security access control to the method
- `Line 40`: Implements database transaction management with proper isolation level and error handling
- `Line 49`: Delegates actual address retrieval to TAddressTools class, showing separation of concerns
### web/Contacts.cs

TContactsWebConnector implements server-side functionality for managing contact logs between partners in OpenPetra. It provides methods to add, find, and delete contact records and their associated attributes. Key functionality includes adding contact logs to individual partners or groups, associating contact attributes with partners, retrieving contact data for the Partner Edit interface, and determining if a contact log is associated with multiple partners. The class handles database transactions for contact-related operations and supports searching contacts by various criteria. Important methods include AddContactLog(), FindContacts(), GetPartnerContactLogData(), and DeleteContacts().

 **Code Landmarks**
- `Line 88`: Uses database transactions with delegate pattern for safe database operations
- `Line 150`: Demonstrates batch processing of partner contacts with LINQ for efficient database operations
- `Line 213`: Implements complex SQL query building with dynamic conditions based on search parameters
- `Line 339`: Handles cascading deletion logic for contact logs with multiple partner associations
### web/DuplicateAddressCheck.cs

TAddressDumplicateWebConnector implements server-side functionality to identify potential duplicate addresses in the OpenPetra system. It provides the FindAddressDuplicates method that compares location records to find possible matches based on address components. The class uses sophisticated string comparison algorithms including Damerau-Levenshtein distance to detect similar addresses despite typos or formatting differences. It groups locations by country code for efficient comparison, handles progress tracking, and implements security through permission requirements. The comparison logic considers postal codes, locality, street names, and handles numeric components with configurable exact/fuzzy matching.

 **Code Landmarks**
- `Line 71`: Uses grouping of locations by country code to optimize comparison performance
- `Line 166`: Implements time estimation for long-running operations with percentage completion tracking
- `Line 219`: Uses regex pattern matching to normalize address strings by handling punctuation and spacing between letters and numbers
- `Line 360`: Implements Damerau-Levenshtein distance algorithm for fuzzy string matching with variable tolerance based on string length
### web/ExtractMaster.cs

TExtractMasterWebConnector implements server-side functionality for managing partner extracts in OpenPetra. It provides methods to retrieve, create, combine, intersect, and subtract extracts, as well as perform operations on partners within extracts. Key functionality includes retrieving extract headers with filtering options, checking extract existence, creating empty extracts, and performing batch operations like updating subscription data, solicitation flags, and partner types. The class also supports extract manipulation through combining multiple extracts, finding intersections between extracts, and subtracting extracts from a base extract. Important methods include GetAllExtractHeaders, CreateEmptyExtract, SaveExtractMaster, UpdateSolicitationFlag, AddSubscription, and CombineExtracts.

 **Code Landmarks**
- `Line 76`: Uses direct SQL statements rather than DB access classes for improved performance when retrieving extract headers
- `Line 307`: Creates extracts from partner keys with filtering options for inactive partners and mailing preferences
- `Line 495`: Implements cascading delete for extract master records to maintain database integrity
- `Line 752`: Uses SQL for batch updating partner flags across an entire extract for performance optimization
- `Line 880`: Implements set operations (combine, intersect, subtract) on extracts to create new extracts
### web/FormLetters.cs

TFormLettersWebConnector implements server-side functionality for generating form letters in OpenPetra's Partner module. It provides methods to populate form data objects with partner information from extracts or individual partner records. Key functionality includes retrieving partner details (personal information, addresses, contact details, subscriptions), building formatted address blocks according to country-specific layouts, resolving greeting placeholders, and updating subscription records. The class supports different partner types (Person, Family, Organization, Church) and handles email address splitting for multiple recipients. Important methods include FillFormDataFromExtract, FillFormDataFromPartner, FillFormDataFromPerson, BuildAddressBlock, and UpdateSubscriptionsReceived.

 **Code Landmarks**
- `Line 76`: Uses a progress tracker pattern to provide feedback during potentially lengthy extract operations
- `Line 248`: Implements email address splitting to generate multiple form letters for partners with multiple email addresses
- `Line 467`: Handles special case for organizations and churches by using their contact person's details for formal greetings
- `Line 564`: Complex placeholder resolution system for customizing greetings based on partner type and available fields
- `Line 746`: Provides a preview function with dummy data to test address block formatting
### web/ImportExport.LocationConversionHelper.cs

TPartnerContactDetails_LocationConversionHelper provides functionality for converting partner location data into contact details in OpenPetra's partner management system. It handles the creation of partner attribute records from imported data, manages existing attributes, and supports the transition from old database schema to new contact details schema. Key methods include CreatePartnerContactDetailRecord for creating new partner attribute records, ExistingPartnerAttributes for checking duplicate entries, and TakeExistingPartnerAttributeRecordAndModifyIt for updating existing records with imported data. The class also defines a PPartnerAttributeRecord class to hold partner attribute data during conversion.

 **Code Landmarks**
- `Line 65`: Uses a delegate function for database operations, allowing flexibility in how partner attributes are loaded
- `Line 93`: Adds legacy columns to support data migration from Petra 2.x database schema
- `Line 171`: Carefully preserves database record state to ensure proper UPDATE statements are generated
- `Line 193`: PPartnerAttributeRecord class implements a data transfer object pattern for partner attributes
### web/ImportExport.cs

TImportExportWebConnector implements functionality for importing and exporting partner data in OpenPetra. It provides methods to read partner data for CSV export, import data from CSV/ODS/XLSX files, and commit changes to the database. The class performs extensive validation and reference checking before committing imported data, ensuring data integrity by verifying or creating necessary reference records in related tables. It handles different partner types (person, family, organization, etc.), addresses, contact details, and specialized information like passports, qualifications, and applications. Key methods include ReadPartnerDataForCSV, ImportFromCSVFileReturnDataSet, ImportFromODSFile, ImportFromXLSXFile, and CommitChanges.

 **Code Landmarks**
- `Line 76`: Implements a web connector pattern for partner data import/export with permission requirements
- `Line 157`: Uses a transaction-based approach to read partner data with different handling based on partner class
- `Line 319`: Implements complex address handling logic to prevent duplicate addresses during import
- `Line 1048`: Handles partner key reassignment for imported partners with negative keys
- `Line 1158`: Uses database query to check if an extract contains family partners
### web/ImportExportCSV.cs

TPartnerImportCSV implements functionality to import partner data from CSV files into OpenPetra. The class handles creating new partners (families and organizations), locations, bank accounts, special types, consents, and contact details. It validates imported data, checks for existing partners to avoid duplicates, and manages address requirements. The code processes CSV data row by row, mapping columns to database fields and handling various data types including dates, addresses, and contact information. Key methods include ImportData, CreateNewFamily, CreateNewOrganisation, CreateNewLocation, and specialized methods for creating bank accounts, consents, and other partner-related records.

 **Code Landmarks**
- `Line 79`: Maintains a list of unused columns to detect mapping issues in the CSV import
- `Line 173`: Comprehensive validation of address criteria requiring either valid address, contact details or IBAN
- `Line 285`: Checks for existing partners to prevent duplicates by searching by name and location
- `Line 427`: Supports multiple consent purposes with comma-separated values
- `Line 482`: Creates partner type records only if they don't already exist in the database
### web/ImportExportTax.cs

TImportExportTaxWebConnector implements functionality for importing tax codes for partners from CSV files. The main method ImportPartnerTaxCodes processes imported data, validates partner existence and type, handles tax code operations (add/modify/delete), and optionally creates an extract of affected partners. The implementation includes progress tracking, transaction management, and comprehensive error handling. Key features include configurable options for handling empty tax codes, overwriting existing codes, and partner validation. The class integrates with OpenPetra's partner management system and provides detailed output of processing results.

 **Code Landmarks**
- `Line 70`: Uses TProgressTracker to provide real-time feedback to client during potentially long-running import operation
- `Line 91`: Transaction-based approach ensures database consistency during import operations
- `Line 177`: Partner validation logic with configurable failure behavior for non-person partners
- `Line 234`: Implements three different strategies for handling empty tax codes based on user preference
- `Line 359`: Optional extract creation functionality to group imported partners for further operations
### web/MergePartners.cs

TMergePartnersWebConnector implements server-side functionality for merging two partner records in OpenPetra. It handles the complex process of combining data from various partner-related tables including gift information, accounts payable, motivations, extracts, greetings, contacts, interests, locations, types, subscriptions, applications, personnel data, jobs, relationships, and banking details. The class provides the MergeTwoPartners method which orchestrates the entire merge process with transaction support, progress tracking, and detailed verification results. It can merge different partner types (person, family, organization, church, etc.) and handles special cases like merging partners from different families. The implementation ensures data integrity by carefully managing primary keys, foreign keys, and avoiding duplicate records.

 **Code Landmarks**
- `Line 73`: Implements a custom exception class for user-initiated cancellation of merge operations
- `Line 103`: Main method orchestrates the entire merge process with progress tracking and transaction support
- `Line 1017`: Partner merge operations maintain an audit trail by recording merge history in PPartnerMergeTable
- `Line 1063`: Special handling for venue merges includes managing buildings, rooms and conference allocations
- `Line 1155`: Updates user defaults and recent partner references to maintain UI consistency after merge
### web/MergePartnersCheck.cs

TMergePartnersCheckWebConnector implements server-side validation for merging partners in OpenPetra. It ensures partners can be safely merged by checking partner classes, currencies for suppliers, family members, donations, bank accounts, and commitments. Key functionality includes validating that family partners aren't merged into different classes when they have members or donations, verifying supplier currencies match, checking for active commitments that might affect Intranet access, and handling foundation organization merges. The class also manages banking details and contact information transfers, and validates gift destination records. Important methods include CheckPartnersCanBeMerged, CanFamilyMergeIntoDifferentClass, CheckPartnerCommitments, GetSupplierCurrency, NeedMainBankAccount, and ActiveGiftDestination.

 **Code Landmarks**
- `Line 71`: Comprehensive verification system that builds a collection of results with different severity levels for merge validation
- `Line 157`: Special handling for merging family records ensures data integrity for family members, donations, and bank accounts
- `Line 243`: Security check for foundation organizations prevents unauthorized merges based on user permissions
- `Line 325`: Banking details validation ensures the merged partner will have exactly one main bank account
- `Line 394`: Complex SQL query that finds contact details from source partner that don't duplicate existing details in target partner
### web/ModuleSetup.cs

TPartnerSetupWebConnector provides server-side functionality for managing partner-related configuration tables in OpenPetra. It implements methods for loading and maintaining partner types, memberships, consent channels, consent purposes, and publications. Each entity type has corresponding load and maintain methods that handle CRUD operations with appropriate permission requirements. The class uses database transactions to ensure data integrity and returns typed datasets containing the requested information. All methods require specific module permissions like PTNRUSER for read operations and PTNRADMIN for write operations.

 **Code Landmarks**
- `Line 73`: Permission-based access control using RequireModulePermission attribute to restrict functionality to authorized users
- `Line 96`: Pattern of database transaction handling with delegate for safe database operations
- `Line 170`: Verification result collection used for error handling and validation feedback
- `Line 175`: Consistent CRUD pattern implementation across different entity types
### web/Partner.DataHistory.cs

TDataHistoryWebConnector implements functionality for managing and tracking data consent history for partners in OpenPetra. It provides methods to retrieve unique data types, check consent status, get historical consent entries, and edit consent permissions. The class handles different consent types (address, email, landline, mobile) and maintains a history of consent changes with associated permissions and channels. Key methods include GetUniqueTypes, LastKnownEntry, GetHistory, EditHistory, and RegisterChanges. The file also defines a DataHistoryChange class for serializing consent change data between client and server.

 **Code Landmarks**
- `Line 88`: UndefinedConsent method is duplicated in another file to avoid cyclic dependencies
- `Line 122`: Creates placeholder consent entries for existing contact data that doesn't yet have formal consent records
- `Line 252`: EditHistory creates new history entries rather than modifying existing ones, preserving audit trail
- `Line 307`: RegisterChanges processes serialized JSON consent changes from client side
### web/Partner.cs

TPartnerWebConnector implements server-side functionality for the Partner module, handling operations like tracking recently used partners, determining best addresses, changing family relationships, and managing bank partners by sort code. The file's primary focus is partner deletion, with extensive validation checks to prevent deletion of partners with dependencies across the system. It provides methods to verify if a partner can be deleted, collect statistics for confirmation, and execute the actual deletion process by removing or updating related records across multiple database tables. The class handles specific deletion logic for different partner types (family, person, unit, organization, church, bank, venue) with appropriate permission checks.

 **Code Landmarks**
- `Line 73`: AddRecentlyUsedPartner tracks partner usage history for different scenarios
- `Line 103`: GetBankBySortCode creates a new bank partner if one doesn't exist for the given branch code
- `Line 161`: CanPartnerBeDeleted performs extensive validation across multiple tables to prevent orphaned references
- `Line 253`: DeletePartner implements a complex cascading deletion process across many related tables
- `Line 1006`: Partner type-specific deletion methods handle the unique relationships of each partner class
### web/PostcodeRegions.cs

TPostcodeRegionsDataWebConnector implements server-side functionality for managing postcode regions and ranges in OpenPetra's partner management system. It provides two main methods: SavePostcodeRegionsTDS for storing postcode region data and updating the server cache, and GetPostCodeRegionsAndRanges for retrieving combined region and range information from the database. The class requires PTNRUSER module permission for both operations and interacts with the database through transaction management to ensure data integrity. After saving changes, it marks cached tables as needing refreshing to maintain consistency across clients.

 **Code Landmarks**
- `Line 61`: Updates cacheable tables in server cache and notifies clients to reload data, ensuring consistency across the system
- `Line 79`: SQL query joins multiple tables to create a combined view of postcode regions and ranges in a single operation
### web/Reporting.Partner.UIConnectors.cs

TPartnerReportingWebConnector implements server-side functionality for generating partner reports in OpenPetra. It provides methods to retrieve partner data for various reporting needs, including BriefAddressReport, PrintPartner, PartnerByRelationship, and PartnerBySpecialType. Each method queries the database for specific partner information based on provided parameters and returns datasets containing partner details, addresses, contact information, relationships, and other relevant data. The class leverages helper methods from TPartnerReportTools and TAddressTools to format and enhance the data with additional information like phone numbers, email addresses, and best addresses. The implementation supports filtering, sorting, and parameter-based data selection to generate customized partner reports for the client application.

 **Code Landmarks**
- `Line 52`: Uses NoRemoting attribute to prevent remote access to these methods, ensuring they're only called from server-side code
- `Line 77`: Implements conditional address retrieval logic based on parameter values
- `Line 123`: Uses dictionary mapping to translate between readable column names and database field names
- `Line 166`: Comprehensive partner data retrieval with 17 different tables for complete partner information
- `Line 522`: Implements reciprocal relationship handling with dynamic SQL generation based on parameters
### web/ServerLookups.DataReader.cs

TPartnerDataReaderWebConnector implements server-side data retrieval functionality for the MPartner module in OpenPetra. It provides methods to fetch various partner-related data including event units, active field units, ledger units, banking details, and bank records. The class includes methods to check if a unit is a conference, find partners sharing bank accounts, retrieve banking details, and get the root unit key in the unit hierarchy. All methods require the PTNRUSER module permission and operate through database transactions to ensure data integrity. Key methods include GetEventUnits(), GetActiveFieldUnits(), GetLedgerUnits(), GetBankingDetailsRow(), and SharedBankAccountPartners().

 **Code Landmarks**
- `Line 58`: Uses RequireModulePermission attribute for security authorization on all public methods
- `Line 75`: Complex SQL query joins multiple tables to retrieve comprehensive event unit data
- `Line 330`: Special handling for inactive bank accounts by adding a qualifier to branch codes
- `Line 371`: Implements a system to generate sequential keys for gift destinations by finding the highest existing key
### web/SimplePartnerEdit.cs

TSimplePartnerEditWebConnector provides functionality for creating, editing, and managing partner records in OpenPetra. It implements methods for retrieving partner details, creating new partners, saving partner information, and handling specialized operations like bank account management and memberships. Key functions include NewPartnerKey, CreateNewPartner, GetPartnerDetails, SavePartner, DeletePartner, ValidateIBAN, and methods for maintaining bank accounts and memberships. The class supports both standard user operations and self-service functionality with appropriate permission requirements. It interacts with the database through transactions and uses UIConnectors for data submission.

 **Code Landmarks**
- `Line 75`: NewPartnerKey method ensures unique partner keys by using a specialized key generation and submission process
- `Line 328`: SavePartner method handles both new partner creation and updates with different logic paths
- `Line 446`: Implements consent tracking for partner data changes through TDataHistoryWebConnector
- `Line 618`: ValidateIBAN uses external web service to validate bank account information
- `Line 636`: FindOrCreateBank method prevents duplicate bank entries by searching for existing banks with the same BIC
### web/SimplePartnerFind.cs

TSimplePartnerFindWebConnector implements server-side functionality for partner searching in OpenPetra. It provides three main methods: FindPartners with multiple search criteria (partner key, name, address, email, etc.), FindPartners by partner key (with exact/fuzzy matching), and TypeAheadPartnerFind for autocomplete functionality. The class handles search criteria preparation, executes searches through TPartnerFind, formats results according to name format preferences, and applies sorting. All methods require appropriate module permissions and return partner data in standardized table formats. The implementation supports filtering by partner class, active status, membership, and limits result sets.

 **Code Landmarks**
- `Line 45`: Uses RequireModulePermission attribute for security authorization
- `Line 86`: Implements flexible name search with wildcard insertion between words
- `Line 165`: Supports multiple name formatting options with dynamic result transformation
- `Line 205`: Partner key search can perform fuzzy matching by treating trailing zeros as wildcards

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #