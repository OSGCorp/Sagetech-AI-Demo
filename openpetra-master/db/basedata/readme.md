# Database Base Data Subproject Of OpenPetra

The OpenPetra is a C#/.NET program that provides comprehensive administrative management for non-profit organizations. The program handles financial transactions and contact relationship management through a structured data model. This sub-project implements the foundational reference data along with the initialization scripts that populate the database with essential lookup values. This provides these capabilities to the OpenPetra program:

- Standardized reference data across all modules
- Internationalization support through country, language, and currency definitions
- Categorization frameworks for partners, personnel, and financial entities
- Initialization scripts for database setup and deployment

## Identified Design Elements

1. **Hierarchical Data Organization**: Reference data is organized into functional domains (Partner, Personnel, Finance, Geographic) with clear relationships between entities
2. **Extensible Classification Systems**: Partner types, attributes, and relationships use flexible categorization schemes that can be adapted to different organizational needs
3. **Internationalization Support**: Comprehensive country, language, and currency data enables global operations
4. **GDPR Compliance Framework**: Consent tracking mechanisms for different communication channels and purposes

## Overview
The architecture emphasizes data consistency through predefined reference values, supports international operations with comprehensive geographic and currency information, and provides flexible classification systems for contacts and personnel. The database initialization process ensures all required lookup tables are populated during system setup. The reference data covers diverse functional areas including contact management, personnel tracking, financial operations, and system configuration, providing a solid foundation for the application's business logic.

## Business Functions

### Partner Management
- `p_addressee_type.csv` : CSV file defining addressee types for contact management in OpenPetra.
- `p_business.csv` : CSV file containing business type classifications for organizations in the OpenPetra system.
- `p_address_block_element.csv` : CSV file defining address block elements for formatting contact information in OpenPetra.
- `p_partner_classes.csv` : CSV file defining partner classes used in OpenPetra's partner management system.
- `p_marital_status.csv` : No description available
- `p_acquisition.csv` : CSV file defining acquisition types for partner records in OpenPetra's database system.
- `p_partner_attribute_category.csv` : CSV file defining partner attribute categories for contact information in OpenPetra.
- `p_partner_status.csv` : CSV file defining partner status codes used in OpenPetra's contact management system.
- `p_occupation.csv` : CSV file containing occupation codes and descriptions for the OpenPetra system.
- `p_address_layout_code.csv` : CSV data file defining address layout codes for OpenPetra's contact management system.
- `p_type.csv` : CSV data file defining partner types for OpenPetra's contact management system.
- `p_partner_attribute_type.csv` : CSV file defining partner attribute types for contact information in OpenPetra.
- `pm_document_type.csv` : CSV file defining the 'Driving Licence' document type configuration for the Petra system.
- `pt_passport_type.csv` : CSV data file containing passport type codes and descriptions for the OpenPetra system.

### Geographic Data
- `p_country.csv` : Country reference data CSV file containing ISO codes, names, phone codes, time zones, and regional information.
- `p_location_type.csv` : CSV file defining location types for contact management in OpenPetra.
- `p_location.csv` : CSV file containing default location data for the OpenPetra system with a single record representing an invalid address.
- `p_international_postal_type.csv` : CSV file defining international postal types with region codes and descriptions for OpenPetra's postal/mailing functionality.

### Financial Management
- `p_banking_details_usage_type.csv` : CSV file defining banking details usage types for the OpenPetra system.
- `a_account_property_code.csv` : CSV file defining account property codes for OpenPetra financial system.
- `p_banking_type.csv` : CSV file defining banking types used in OpenPetra's financial system.
- `a_sub_system.csv` : CSV file defining subsystems used in OpenPetra for organizational structure of the application.
- `a_frequency.csv` : CSV file defining frequency types for recurring events or transactions in OpenPetra.
- `pc_discount_criteria.csv` : CSV file defining discount criteria types for the OpenPetra system.
- `pc_cost_type.csv` : CSV data file defining cost types for financial tracking in OpenPetra.
- `a_currency.csv` : Currency reference data file containing ISO codes, names, symbols, and formatting patterns for global currencies.
- `a_budget_type.csv` : CSV file defining budget types used in OpenPetra's financial management system.

### Personnel Management
- `pt_ability_level.csv` : CSV data file defining ability levels for personnel skills tracking in OpenPetra.
- `pm_commitment_status.csv` : CSV data file defining commitment status categories for personnel in the OpenPetra system.
- `pt_ability_area.csv` : CSV file containing ability area codes and descriptions for personnel skills tracking in OpenPetra.
- `pt_assignment_type.csv` : CSV data file defining assignment types for personnel in the OpenPetra system.
- `pt_language_level.csv` : CSV file defining language proficiency levels for OpenPetra's contact management system.
- `pt_position.csv` : CSV file defining position codes and titles for organizational roles in OpenPetra.
- `pt_skill_level.csv` : CSV data file defining skill levels for personnel in the OpenPetra system.
- `pt_applicant_status.csv` : CSV file defining applicant status codes and descriptions for the OpenPetra personnel management system.
- `pt_leadership_rating.csv` : CSV file defining leadership rating codes for individuals in the OpenPetra system.

### Relationship Management
- `p_relation_category.csv` : CSV file defining relationship categories for contacts in OpenPetra's database system.
- `p_relation.csv` : CSV file defining relationship types between entities in OpenPetra's contact management system.

### System Configuration
- `s_logon_message.csv` : CSV file containing a welcome message for the Petra Demo database.
- `u_unit_type.csv` : CSV file defining unit types for organizational structure in OpenPetra
- `s_module.csv` : CSV file defining system modules for OpenPetra with their names, creation dates, and administrative information.
- `pm_document_category.csv` : CSV file defining a document category for the OpenPetra system
- `init.sql` : SQL initialization script that loads base data and sets up initial user accounts for OpenPetra.
- `s_module_table_access_permission.csv` : CSV file defining module-level database table access permissions for the Finance module in OpenPetra.
- `p_reason_subscription_given.csv` : CSV file defining reason codes for subscription gifts with donation, free, and paid categories.
- `p_reason_subscription_cancelled.csv` : CSV file defining reason codes for subscription cancellations in the OpenPetra system.

### Conference Management
- `pt_application_type.csv` : CSV file defining a single conference application type for the OpenPetra system.
- `pt_congress_code.csv` : CSV file containing congress code definitions for participant types in OpenPetra events.

### Consent Management
- `p_consent_purpose.csv` : CSV file defining consent purpose categories for data processing in OpenPetra.
- `p_consent_channel.csv` : CSV file defining consent channel types for communication preferences in OpenPetra.

### Language Support
- `p_language.csv` : CSV file containing language codes and names for internationalization support in OpenPetra.

### Transportation
- `pt_travel_type.csv` : CSV data file defining travel type codes and descriptions for the OpenPetra system.

### Skills Management
- `pt_skill_category.csv` : CSV data file defining skill categories for personnel in the OpenPetra system.

### Religious Data
- `p_denomination.csv` : CSV file containing a single denomination record with 'UNKNOWN' as the denomination code and minimal data.

### Sponsorship
- `p_type_category.csv` : CSV data file defining the 'SPONSORED_CHILD_STATUS' type category for the OpenPetra system.

## Files
### a_account_property_code.csv

This CSV file contains definitions of account property codes used in OpenPetra's financial system. It defines three property codes: BANK ACCOUNT (likely indicating accounts used for banking operations), Is_Special_Account (a flag for accounts with special handling), and CARRYFORWARDCC (which specifies equity accounts used for carrying forward cost centers). Each entry includes a description field and creation date, with additional fields that appear to be placeholders.

 **Code Landmarks**
- `Line 3`: CARRYFORWARDCC property includes complex logic for cost center handling, supporting both same-CC and standard-CC forwarding options
### a_budget_type.csv

This CSV file defines five budget types for OpenPetra's financial system: ADHOC (Adhoc), INFLATE_N (Inflate after n periods), INFLATE_BASE (Inflate off base), SAME (Same each period), and SPLIT (Split total). Each entry follows a consistent format with the type code, description, and several placeholder fields marked with question marks. These budget types likely control how financial projections and allocations are calculated within the system.

 **Code Landmarks**
- `Line 1-5`: The file uses a consistent pattern of code, description, empty field, followed by five placeholder fields marked with question marks (?)
### a_currency.csv

This CSV file serves as a currency reference database for OpenPetra, containing records for global currencies with their ISO codes, full names, symbols, country codes, and formatting patterns. Each row represents a currency with fields for currency code, name, symbol, country code, formatting pattern, and Euro status flag. The file includes both current currencies (USD, EUR, GBP) and historical ones (DEM, ITL), with formatting patterns specifying how currency values should be displayed. The trailing question marks appear to be placeholder fields for additional currency attributes not currently populated.

 **Code Landmarks**
- `Line 25`: Bitcoin (BTC) included with 8 decimal places in formatting pattern, unlike standard 2 decimal places for most currencies
- `Line 24`: Euro symbol (€) is directly encoded in the CSV, showing Unicode character support
- `Line 5`: Format patterns use PIC-style notation with '9' for digits and '>' for thousands separators
### a_frequency.csv

This CSV file contains predefined frequency types used throughout OpenPetra for recurring events or transactions. Each row defines a frequency type with its description and numerical parameters. The file includes common frequencies such as Annual, Bi-Monthly, Daily, Monthly, Quarterly, Semi-Annual, and Weekly. Each record contains fields for name, description, yearly frequency, monthly frequency, daily frequency, and several placeholder fields marked with question marks. These frequency definitions likely support scheduling, reporting, and transaction processing throughout the system.

 **Code Landmarks**
- `Line 1-7`: The numerical parameters (columns 3-5) appear to define the frequency in different time units - years, months, and days respectively
### a_sub_system.csv

This CSV file defines the subsystems used in OpenPetra's application structure. It contains three entries: Accounts Payable (AP), General Ledger (GL), and Gift Processing (GR). Each subsystem has a code and description, with placeholder values (?) for additional configuration parameters. These subsystems represent the core functional areas of the OpenPetra application for nonprofit organization management.

 **Code Landmarks**
- `Line 1-3`: The file uses a simple CSV structure with codes and descriptions that likely serve as reference data for the application's module organization.
### init.sql

This SQL initialization script establishes the foundation for an OpenPetra database. It creates initial user accounts (SYSADMIN and SELFSERVICE), loads essential reference data from CSV files into multiple database tables, and configures system defaults. The script populates numerous lookup tables for partner management, accounting, personnel tracking, and system configuration using COPY commands with a standardized format. It also sets initial passwords (which are inserted during the build process), establishes system status, and grants the SYSADMIN user access to the system management module.

 **Code Landmarks**
- `Line 2`: Uses placeholder variables like {#PASSWORDHASHSYSADMIN} that get replaced during the build process
- `Line 6`: Uses PostgreSQL COPY command with standardized format for efficient bulk data loading from CSV files
- `Line 51`: Sets the database version to match the application release version using a placeholder variable
### p_acquisition.csv

This CSV file contains predefined acquisition types used to track how partners were acquired in the OpenPetra system. Each record defines an acquisition code (like APL, CHURCH, STAFF) with attributes indicating whether it's active, applies to applicants, and other boolean flags. The file serves as reference data for the partner management module, categorizing how contacts were initially established with the organization.

 **Code Landmarks**
- `Line 3`: APL and STAFF entries are marked as applicant-related (third boolean is true), distinguishing them from other acquisition types
- `Line 17`: UNKNOWN is the only acquisition type marked as inactive (first boolean is false), likely serving as a default or placeholder value
### p_address_block_element.csv

This CSV file defines address block elements used for formatting addresses and contact information in the OpenPetra system. It contains a list of standardized elements that can be used when generating address labels, letters, or other documents. Each row defines an element with properties including its name, description, and a boolean flag indicating special formatting behaviors. Elements include standard address components (street, city, postal code), name components (first name, last name, title), and formatting controls (CapsOn, CapsOff, NoSuppress). These elements serve as building blocks for creating customized address formats across the system.

 **Code Landmarks**
- `Line 6-7`: CapsOn/CapsOff elements provide text formatting control within address blocks
- `Line 23`: NoSuppress flag forces printing of a line even when empty, important for maintaining consistent formatting
- `Line 29`: UseContact flag enables dynamic selection between organization name and contact person
### p_address_layout_code.csv

This CSV file defines address layout codes used in OpenPetra's contact management system. It contains five predefined layout types: Envelope, Letter Head, One Line, Rolodex, and Small Label. Each record includes a code identifier, description, sequence number, and several placeholder fields marked with question marks. These layout codes likely control how address information is formatted and displayed in different contexts throughout the application.

 **Code Landmarks**
- `Line 1-5`: The file uses a simple CSV structure with nine columns, where the last five columns contain placeholder values (?) that may be configured during runtime.
### p_addressee_type.csv

This CSV file defines the standard addressee types used in OpenPetra's contact management system. It contains eight predefined addressee types including Single Female, Single Male, Church, Couple, Default, Family, Organisation, and Venue. Each record includes a code identifier, description, and several placeholder fields marked with question marks. These addressee types provide categorization options for contacts within the system.

 **Code Landmarks**
- `Line 1-8`: The file uses a consistent structure with placeholder values (?) for fields that might be configured during system operation
### p_banking_details_usage_type.csv

This CSV file contains a single record defining the default banking detail usage type for partners in the OpenPetra system. The record has the code 'MAIN' with the description 'The default banking detail that should be used for this partner' and includes several configuration parameters represented by question marks and numeric values. This appears to be part of the base data setup for the banking module.

 **Code Landmarks**
- `Line 1`: The file uses a specific format where the first field is the type code, followed by description and various configuration parameters
### p_banking_type.csv

This CSV file defines three banking types for OpenPetra's financial system: bank account (ID 0), credit card (ID 1), and savings account (ID 2). Each record includes an ID, name, description, and six placeholder fields marked with question marks. The data structure supports OpenPetra's banking and financial management functionality by providing standardized categorization for different account types.

 **Code Landmarks**
- `Line 1-3`: Uses a simple CSV structure with nine fields per record, with the last six fields containing placeholder values (?) that may be used for additional banking type attributes
### p_business.csv

This CSV file defines a standardized list of business types used for categorizing organizations within OpenPetra. Each record contains a business type code, description, and several placeholder fields. The file includes 24 different business categories ranging from industries like agriculture, banking, and healthcare to organizational types like missionary organizations and para-church groups. This data likely serves as reference data for the partner/organization management functionality in OpenPetra.

 **Code Landmarks**
- `Line 1-24`: Uses a consistent format with a code, description, and placeholder fields marked with '?' that may be used for additional attributes in the system
### p_consent_channel.csv

This CSV file defines four consent channel types used in OpenPetra for tracking communication preferences: phone, letter, email, and direct conversation. Each record contains a code identifier, internal name, descriptive label, and several placeholder fields (marked with question marks) for additional attributes. These channels likely represent the methods through which the organization can contact individuals based on their consent preferences.

 **Code Landmarks**
- `Line 1-4`: The file uses a simple CSV structure with question marks as placeholders for fields that may be defined elsewhere in the system
### p_consent_purpose.csv

This CSV file defines three consent purpose categories for data processing in OpenPetra: gift receipting (GR) for donation processing, public relations (PR) for PR activities, and newsletter (NEWSLETTER) for sending newsletters. Each row contains a purpose code, name, description, and five placeholder fields marked with question marks. These consent purposes likely support GDPR compliance by tracking what personal data can be used for.

 **Code Landmarks**
- `Line 1-3`: The file uses a simple CSV structure with eight columns, but only the first three columns contain actual data while the remaining five use placeholders.
### p_country.csv

This CSV file contains a comprehensive database of country information used by OpenPetra for international operations. Each row represents a country with its ISO code, name, international dialing code, currency region, time zone information, and EU membership status. The file includes 239 countries and territories, with fields for country code, full name, phone code, region code, international dialing prefix, standard time offset, daylight saving time offset, EU membership flag, and additional placeholder fields marked with question marks. The data supports OpenPetra's international operations including contact management and financial transactions across different regions.

 **Code Landmarks**
- `Line 1`: Special entry '99,BAD COUNTRY CODE' serves as a placeholder for invalid country codes
- `Line 5-240`: Countries organized alphabetically by ISO code with consistent field structure
- `Line 73`: Euro currency zone countries marked with '1' in the EU membership field
### p_denomination.csv

This CSV file contains a single record for the 'UNKNOWN' denomination in the Petra database. It defines a default denomination with minimal attributes: code 'UNKNOWN', description 'Unknown', active status set to true, and inactive status set to false. The remaining fields contain placeholder values ('?'), likely representing optional or undefined denomination properties. This serves as a fallback or default denomination record in the system.
### p_international_postal_type.csv

This CSV file contains a list of international postal type codes used in OpenPetra for categorizing geographic regions for postal/mailing purposes. Each record includes a code (like 'AFR', 'AUS', 'EUR'), a description (like 'Africa', 'Australasia & Pacific Islands', 'Europe'), and a boolean flag, followed by several placeholder fields. The file includes special codes for local, unallocated, and unknown regions, as well as continental regions, economic zones, and numbered zones.

 **Code Landmarks**
- `Line 1`: Uses '#LOCAL' as a special identifier for local postal handling rather than a standard country/region code
### p_language.csv

This CSV file serves as a reference database for language support in OpenPetra. It contains a comprehensive list of language codes (e.g., EN, DE, FR) paired with their full names (e.g., English, German, French). Each entry includes boolean flags indicating language status, likely for UI availability and translation completeness. The file follows a consistent structure with language code, name, two boolean fields, and five placeholder fields marked with question marks. Some languages are marked as 'true' in the third column, possibly indicating priority or fully supported languages in the OpenPetra system.

 **Code Landmarks**
- `Line 1`: Uses two-letter ISO language codes as primary identifiers for most languages
### p_location.csv

This CSV file contains base data for the p_location table in OpenPetra's database. It provides a single default record representing an invalid address with the description 'No valid address on file'. The record includes placeholder values (marked with '?') for most fields, with specific values for location key (0), postal code (99), creation date (19980101), and created by user (SYSADMIN). This serves as a fallback location entry when no valid address information is available in the system.

 **Code Landmarks**
- `Line 1`: Uses '?' as placeholder for null or empty values in the CSV format
- `Line 1`: Contains a special record with ID 0 that serves as a system default for invalid addresses
### p_location_type.csv

This CSV file contains predefined location type data for the OpenPetra system's contact management functionality. It defines 13 standard location types including Business, Church, Home, Mobile, and Temporary addresses. Each record includes a type code, description, boolean flags, and placeholder fields for metadata. These location types categorize contact addresses in the system, enabling proper organization and filtering of location information for organizations, individuals, and entities within the application.

 **Code Landmarks**
- `Line 1-13`: Uses question marks as placeholders for optional or undefined fields in the CSV structure
### p_marital_status.csv

No summary available
### p_occupation.csv

This CSV file serves as a database table for occupation codes in the OpenPetra system. It contains a comprehensive list of occupational categories organized by field, including communications, ministry, music, office work, people-related professions, and practical trades. Each record includes an occupation code, description, and several boolean and placeholder fields. The data is structured with categories prefixed by domain codes (COM-, MIN-, MUS-, OFF-, PEO-, PRAC-) to group related occupations. The file provides standardized occupation data that can be used for contact management and personnel records within the OpenPetra system.

 **Code Landmarks**
- `Line 1`: Uses a structured code system where prefixes indicate occupation categories (COM-, MIN-, MUS-, etc.)
### p_partner_attribute_category.csv

This CSV file defines partner attribute categories for the OpenPetra system. It contains five predefined categories for storing contact information: Phone numbers, E-Mail addresses, Digital Media (social media/websites), Instant Messaging, and Partner Contact Details Settings. Each row specifies a category with fields for name, description, sequence number, visibility flags, and placeholder fields marked with question marks.

 **Code Landmarks**
- `Line 1-5`: The file uses a structured format with boolean flags (true/false) to control visibility and behavior of different contact categories
### p_partner_attribute_type.csv

This CSV file defines the partner attribute types used for storing contact information in OpenPetra. It categorizes different communication methods into groups like Phone, E-Mail, Digital Media, and Instant Messaging & Chat. Each entry specifies a contact type with details including display name, description, sequence number, category code, URL template (for social media), business equivalent name, and various configuration flags. The file includes definitions for phone numbers, email addresses, social media accounts, websites, and messaging services, providing a structured way to store and display different contact methods for partners in the system.

 **Code Landmarks**
- `Line 6-12`: Social media entries use a {VALUE} placeholder in URL templates to dynamically generate profile links
- `Line 19-20`: Special PARTNERS_CONTACTDETAILS_SETTINGS entries define system-level contact preferences rather than actual contact methods
### p_partner_classes.csv

This CSV file defines the basic partner classification system for OpenPetra. It contains seven predefined partner classes: BANK, CHURCH, FAMILY, ORGANISATION, PERSON, UNIT, and VENUE. Each entry follows a structure with seven fields, where only the first field (the class identifier) is populated while the remaining six fields are empty placeholders marked with question marks. These classifications serve as fundamental categories for organizing different types of contacts within the system.

 **Code Landmarks**
- `Line 1-7`: The file uses a simple CSV structure with seven columns but only populates the first column, suggesting a database schema where additional properties could be defined for each partner class.
### p_partner_status.csv

This CSV file defines the basic partner status codes used in OpenPetra's contact management system. It contains four predefined statuses: ACTIVE (for active partners), DIED (for deceased partners), INACTIVE (for inactive partners), and MERGED (for partner records that have been merged into others). Each status has boolean flags indicating whether the partner is active, can be posted to, or can be deleted, followed by placeholder fields marked with question marks.

 **Code Landmarks**
- `Line 1`: ACTIVE status is the only one with 'true' values for both active and can-be-posted-to flags
### p_reason_subscription_cancelled.csv

This CSV file contains predefined reason codes for subscription cancellations in OpenPetra. Each record includes a code (like BAD-ADDR, COMPLETE, DIED) and a description explaining why a subscription was terminated. These standardized reasons help categorize and track why partners discontinue subscriptions, supporting reporting and analysis of subscription management within the system.

 **Code Landmarks**
- `Line 1-7`: The file uses a consistent format with a code identifier followed by a human-readable description, with additional placeholder fields (represented by question marks) for potential future attributes.
### p_reason_subscription_given.csv

This CSV file defines three standard reason codes for subscriptions in the OpenPetra system. It contains three entries: 'DONATION' for partners who donated money, 'FREE' for free subscriptions, and 'PAID' for paid subscriptions. Each entry includes a code identifier and description, followed by placeholder fields (represented by question marks) for additional attributes not specified in this file. This data likely populates a lookup table used throughout the system for categorizing subscription origins.
### p_relation.csv

This CSV file defines relationship types used in OpenPetra's contact management system. Each row represents a relationship type with its properties including relationship code, forward and reverse descriptions, category (FAMILY, OTHER, CHURCH), and directionality flags. The file contains 31 relationship types covering family relationships (FATHER, MOTHER, SIBLING), organizational connections (EMPLOYER, MEMBER), church relationships (PASTOR, TREASURER), and emergency contacts. These relationship definitions enable the system to track and manage connections between different entities in the database, supporting OpenPetra's contact management functionality.

 **Code Landmarks**
- `Line 1`: Uses bidirectional relationship definitions with forward/reverse descriptions for each relationship type
- `Line 3`: Implements emergency contact hierarchy with primary, secondary, and general emergency contacts
- `Line 5`: Categorizes relationships into three main types: FAMILY, OTHER, and CHURCH
### p_relation_category.csv

This CSV file defines three basic relationship categories used in OpenPetra's contact management system: CHURCH (Church Relationships), FAMILY (Family Relationships), and OTHER (Other Relationships). Each record contains a category code, description, and several placeholder fields (marked with '?') that likely represent additional configuration options for these relationship types. These categories establish the foundation for classifying relationships between contacts in the system.

 **Code Landmarks**
- `Line 1-3`: Uses a consistent pattern of code, description, and boolean/placeholder values that suggests a standardized database schema for categorization
### p_type.csv

This CSV file contains the base data for partner types in the OpenPetra system. It defines various categories of partners such as Associate Members, Banks, Churches, Companies, Staff, Volunteers, and different types of sponsored children. Each record includes the type code, description, category, and boolean flags that determine behavior in the system. The file serves as a lookup table for classifying contacts within the organization's database, with special designations for sponsored children statuses and other organizational relationships.

 **Code Landmarks**
- `Line 17-21`: Special category 'SPONSORED_CHILD_STATUS' groups different types of sponsored children with distinct classifications
### p_type_category.csv

This CSV file defines a single type category named 'SPONSORED_CHILD_STATUS' used for storing status options for sponsored children in the OpenPetra system. The file contains only one data record with fields indicating it's a status option category for sponsored children. The structure follows the standard format for type category definitions with boolean flags and placeholder values marked with question marks.
### pc_cost_type.csv

This CSV file contains predefined cost type definitions for OpenPetra's financial tracking system. It stores two cost type records: 'ACCOMMODATION' for extra accommodation costs and 'CONFERENCE' for additional conference costs. Each record includes a code, description, and several boolean/placeholder fields marked with '?' that likely represent configuration options for how these cost types function within the financial system. The file serves as base data for initializing the cost type functionality in the application.

 **Code Landmarks**
- `Line 1-2`: The file uses a simple CSV format with question marks as placeholders for optional or configurable values
### pc_discount_criteria.csv

This CSV file contains discount criteria types used in OpenPetra's pricing or discount system. It defines four criteria types: Child, Other people, Special Event Role, and Volunteer. Each record includes a code, description, and several placeholder fields marked with question marks that likely represent additional configuration options for the discount system. The file serves as reference data for the application's discount management functionality.

 **Code Landmarks**
- `Line 1-4`: The file uses a simple comma-separated format with question mark placeholders for optional or undefined values, suggesting a flexible configuration approach for discount criteria.
### pm_commitment_status.csv

This CSV file defines commitment status categories for personnel in the OpenPetra system. It contains seven predefined statuses (GUEST, LONG-TERMER, WORKER, SHORT-TERMER, STAFF, TRANSITION, VOLUNTEER) with their descriptions and associated attributes. Each record includes fields for status code, description, and various boolean and numeric parameters that control system behavior for people in each category. The file serves as base data for the personnel management module.

 **Code Landmarks**
- `Line 1-7`: Hierarchical organization of personnel types with numeric priority values (1-7) indicating their relative importance or processing order
### pm_document_category.csv

This CSV file contains a single record defining a 'General Documents' category in OpenPetra's document management system. The record includes fields for category code, description, and several boolean and placeholder values (marked with '?') that likely control document category behavior and permissions within the system. This data would be imported into OpenPetra's database during system setup or initialization.

 **Code Landmarks**
- `Line 1`: Uses a comma-separated format with 11 fields defining document category properties
### pm_document_type.csv

This CSV file contains a single record defining the 'DRIVINGLICENCE' document type in the Petra system. It specifies configuration parameters including category (GENERAL), display name (Driving Licence), and several boolean and undefined parameters marked with question marks. This appears to be part of the base data used to initialize document type configurations in the system.

 **Code Landmarks**
- `Line 1`: Uses a comma-separated format with question marks as placeholders for undefined or null values
### pt_ability_area.csv

This CSV file serves as a reference database for ability areas or skills that can be assigned to personnel in the OpenPetra system. It contains over 200 skill codes organized into categories like communications (COM), ministry (MIN), music (MUS), office (OFF), people-related (PEO), and practical skills (PRAC). Each record includes a unique code, short and long descriptions, and several placeholder fields marked with question marks for additional attributes. The skills cover diverse areas from technical abilities like programming to soft skills like counseling, and practical trades like carpentry.

 **Code Landmarks**
- `Line 1`: Data uses category prefixes (COM-, MIN-, MUS-, etc.) to organize skills into logical groupings
- `Line all`: Question mark placeholders indicate fields reserved for future use or configuration
- `Line all`: Boolean 'false' values appear consistently in specific positions, likely indicating default settings
### pt_ability_level.csv

This CSV file contains predefined ability level classifications for personnel skills assessment in OpenPetra. It defines a 0-10 scale (plus 99 for unknown) of competency levels, from 'No exposure' to 'Expert at this ability'. Each record includes the level ID, description, and several placeholder fields (marked with '?') for additional attributes. The data structure supports OpenPetra's personnel management functionality, allowing organizations to track and categorize staff or volunteer capabilities in a standardized way.

 **Code Landmarks**
- `Line 1-12`: Uses a numeric scale (0-10, 99) with corresponding text descriptions to create a standardized competency assessment framework
### pt_applicant_status.csv

This CSV file contains a list of applicant status codes used in OpenPetra's personnel management system. Each row represents a status code with its description and several placeholder fields. The statuses include accepted applicants, various cancellation reasons (both by applicant and office), enquiries, and different hold statuses. The codes follow patterns like 'C' for applicant cancellations, 'R' for office cancellations, and 'H' for on-hold statuses, providing a standardized way to track applicant progress through the recruitment process.

 **Code Landmarks**
- `Line 1-21`: Uses a consistent pattern where single-letter codes (A, E, H) represent primary statuses while hyphenated or numbered codes (H-MED, C1) represent specific sub-statuses or reasons
### pt_application_type.csv

This CSV file defines a single application type record for the OpenPetra system. It contains one entry with the type 'CONFERENCE' labeled as 'Attending Conference' with various configuration parameters. The file appears to be part of the base data that populates the pt_application_type table in the database, establishing predefined application types used throughout the system.

 **Code Landmarks**
- `Line 1`: Uses a comma-separated format with question marks (?) representing null or undefined values in the database schema
### pt_assignment_type.csv

This CSV file contains base data for personnel assignment types in OpenPetra. It defines three standard assignment categories: Full-Time field (F), Ministry roles like guest or speaker (M), and Part-Time Job (P). Each record includes a code, description, and several placeholder fields marked with question marks that likely represent additional configuration options for each assignment type.

 **Code Landmarks**
- `Line 1-3`: The file uses a simple comma-separated format with question mark placeholders for optional or undefined configuration values
### pt_congress_code.csv

This CSV file defines various congress participant types for OpenPetra's event management system. Each row represents a different role code (like TS-CHILD, TS-COACH, TS-DOC) with corresponding descriptions and a series of boolean flags (1/0) that likely control permissions, visibility, or behavior within the system. The file appears to be part of the base data needed for setting up the congress/event management functionality, defining roles such as children, coaches, doctors, exhibitors, speakers, staff, and supervisors.

 **Code Landmarks**
- `Line 1-14`: Uses a consistent pattern of TS- prefix for all congress codes, suggesting a standardized taxonomy for participant types
### pt_language_level.csv

This CSV file defines six standardized language proficiency levels used in OpenPetra. It includes BEGINNER, BASIC, INTERMEDIATE, ADVANCED, NATIVE/ACADEMIC, and UNKNOWN categories, each with a numeric ID and detailed description of language capabilities. The file structures data for the database table pt_language_level, which likely supports contact management features for tracking individuals' language skills within the non-profit management system.

 **Code Landmarks**
- `Line 1-6`: Uses a structured progression model for language proficiency assessment from basic recognition to native fluency
### pt_leadership_rating.csv

This CSV file contains predefined leadership rating codes used in OpenPetra to categorize individuals based on their leadership capabilities. The file defines 7 rating levels ranging from -1 (No information) to 5 (Leader of 100+ - Seasoned veteran), with descriptions for each level. Each record includes a rating code, description, and several placeholder fields marked with question marks that likely represent additional attributes for the leadership rating system.

 **Code Landmarks**
- `Line 1-7`: Uses a scale from -1 to 5 to represent leadership capacity, with detailed descriptions for each level
### pt_passport_type.csv

This CSV file defines passport type codes used in the OpenPetra system. It contains six predefined passport types: Diplomatic, ID Card, Full Passport, Residence Permit, Short-term Passport, and Visitor. Each entry includes a code, description, and several placeholder fields marked with question marks. The data structure appears to support passport type configuration with boolean flags and additional parameters that are currently undefined.

 **Code Landmarks**
- `Line 1-6`: Uses a consistent data structure with boolean flags (false) and placeholder values (?) that suggests extensibility for future passport type configurations
### pt_position.csv

This CSV file contains a comprehensive list of position codes and titles used within the OpenPetra system to categorize different organizational roles. Each record defines a position with attributes including a unique code, position type (all marked as 'O'), descriptive title, and several placeholder fields (marked with '?') for additional configuration. The positions are organized by functional areas such as Communications (CM), Financial Development (FD), Finance (FIN), IT, Leadership (LDR), Member Care (MC), Ministry (MIN), Office (OFF), Personnel (PER), Practical roles (PRAC), Recruiting (REC), and Training (TRA). This data serves as a reference table for the human resources and organizational structure components of the system.

 **Code Landmarks**
- `Line 1`: Uses a consistent code prefix system to categorize positions by department/function
### pt_skill_category.csv

This CSV file contains predefined skill categories for classifying personnel capabilities within the OpenPetra system. Each row represents a skill category with a name and description, such as COMMUNICATION, EDUCATION, FINANCE, etc. The file includes 14 different categories, each with examples of related professions. The structure appears to have 10 columns with most fields containing placeholder values (?) or FALSE flags, suggesting these are default configurations for the skill category system.

 **Code Landmarks**
- `Line 1`: The file uses a consistent structure with category name, description, and several boolean/placeholder fields that likely control behavior in the application.
- `Line 10`: The OTHER category exists without examples, likely serving as a catch-all for skills that don't fit into predefined categories.
### pt_skill_level.csv

This CSV file contains predefined skill level classifications for personnel management in OpenPetra. It defines five skill levels (Basic, Moderate, Competent, Professional, and 'Level of ability not known') with their corresponding numeric identifiers. Each record includes a skill level ID, description, and several placeholder fields marked with question marks that likely represent additional attributes or configuration options. This standardized reference data supports personnel skill tracking and reporting functionality within the system.

 **Code Landmarks**
- `Line 1-5`: Uses a numeric ID system (1-4, 99) where 99 represents an unknown skill level, suggesting special handling for undetermined skills.
- `Line 1-5`: Contains multiple placeholder fields (?) indicating extensibility for future attributes or localization.
### pt_travel_type.csv

This CSV file contains base data for travel types used in the OpenPetra system. It defines six standard travel modes (airplane, bus/coach, car, train, van, walking) with their corresponding codes and descriptions. Each record includes a short code (e.g., 'AIR'), a description (e.g., 'by airplane'), and several placeholder fields marked with question marks that likely represent additional attributes or configuration options for each travel type. This data would be imported into the system's database to standardize travel type classifications.

 **Code Landmarks**
- `Line 1-6`: Uses a consistent pattern of short codes (3-4 characters) paired with human-readable descriptions for travel modes
### s_logon_message.csv

This CSV file contains a single welcome message for users logging into the Petra Demo database. The message is stored in English (EN) with the text 'Welcome to the Petra Demo database!' and is associated with the SYSADMIN user. The file follows a comma-separated format with placeholder values (?) for fields that are not populated.
### s_module.csv

This CSV file contains the definition of system modules in OpenPetra. Each record represents a module with its code, description, creation date, creator, modification date, modifier, and an additional field. Modules include various functional areas like Conference, Finance, Personnel, Partner management, Sponsorship, and System administration. The file serves as a base configuration for the system's modular structure, defining the different access levels and functional components available in the application.

 **Code Landmarks**
- `Line 2-3`: Development-specific modules (DEVADMIN, DEVUSER) indicate separation between development and production environments
- `Line 5-8`: Finance modules show a tiered access structure with basic, intermediate, advanced, and reporting user levels
- `Line 14-15`: Sponsorship functionality is split between administrative and view-only access levels
- `Line 17`: PARTNERSELFSERVICE module suggests external user access capabilities for partners
### s_module_table_access_permission.csv

This CSV file defines table access permissions for the FINANCE-1 module in OpenPetra. Each row specifies permissions for a specific database table, with columns representing different access rights (likely read, write, create, delete) using 1 for granted permissions and ? for undefined ones. The file covers financial tables including suppliers, documents, currencies, exchange rates, gift batches, journals, and ledgers, establishing the security framework for financial data management in the system.

 **Code Landmarks**
- `Line 1-14`: Uses a binary permission system (1=granted) with question marks likely indicating configurable or inherited permissions
### u_unit_type.csv

This CSV file contains predefined unit type codes and descriptions used for categorizing organizational units in OpenPetra. It defines 11 different unit types including Area, Country, Field, Conference, Key Ministry, Team, Working Group, and others. Each record contains a code, description, and several placeholder fields marked with question marks. These unit types provide the foundation for organizing the hierarchical structure of organizations within the system.

 **Code Landmarks**
- `Line 1`: Uses a simple comma-separated format with boolean flag (false) indicating these are likely system-defined, non-modifiable unit types

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #