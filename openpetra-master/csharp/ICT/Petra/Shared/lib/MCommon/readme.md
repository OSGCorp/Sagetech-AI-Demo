# C# Shared Common Module Of OpenPetra

The OpenPetra C# Shared Common Module provides foundational functionality and cross-cutting concerns that support the entire application ecosystem. This module implements core data structures, constants, type definitions, and utility functions that are leveraged throughout the system. The module provides these capabilities to the OpenPetra program:

- Standardized constants and type definitions for system-wide consistency
- Partner classification and address handling utilities
- Form letter generation and mail merge functionality
- Typed datasets for structured data management
- Country-specific information retrieval

## Identified Design Elements

1. **Centralized Constants Management**: The module defines system-wide constants for isolation levels, data types, form design codes, and localized strings to ensure consistency across the application.

2. **Partner Classification System**: Implements conversion utilities between different representations of partner types (person, family, organization, church) to standardize handling across modules.

3. **Address Handling Framework**: Provides default location and addressee types based on partner classification to ensure consistent address management.

4. **Form Letter Generation**: Comprehensive data structures for mail merge operations, supporting various entity types with specialized formatting for different contexts.

5. **Cached Data Access**: Utilities for retrieving country-specific information from cached data tables, improving performance for frequently accessed reference data.

## Overview
The architecture emphasizes reusability through shared constants and utilities, maintains consistent partner classification across the system, and provides robust support for document generation. The module serves as a foundation for higher-level application components, with clear separation between data structures, utility functions, and type definitions. The typed datasets provide structured data management capabilities while the form data classes enable sophisticated document generation for various business contexts.

## Business Functions

### Common Module Core
- `Constants.cs` : Defines constants for OpenPetra's common module including data types, form codes, and import-related messages.
- `Types.cs` : Defines enumerations and conversion methods for partner classes and office-specific data labels in OpenPetra.

### Address Management
- `AddressHandling.cs` : Utility class for handling address-related defaults based on partner classification in OpenPetra.

### Data Utilities
- `CodeHelper.cs` : Provides utility methods for retrieving country information from cached data tables in OpenPetra.

### Document Generation
- `FormData.cs` : Defines data structures for form letter generation and mail merge operations in OpenPetra.

## Files
### AddressHandling.cs

TSharedAddressHandling implements utility functions for address handling in OpenPetra's partner management system. The class provides two key static methods that return default values based on a partner's classification (person, family, church, organization, etc.). GetDefaultLocationType returns appropriate location types (HOME, CHURCH, BUSINESS, FIELD) based on partner class, while GetDefaultAddresseeType returns corresponding addressee types (FAMILY, CHURCH, ORGANISA, VENUE). These methods help standardize address handling across the application by providing consistent default values for different partner types.

 **Code Landmarks**
- `Line 40`: Switch statement maps partner classes to appropriate location types for address standardization
- `Line 67`: Similar pattern for addressee types shows consistent design approach for partner classification handling
### CodeHelper.cs

CommonCodeHelper implements utility methods for retrieving country-related information from cached data tables in the Common Module of OpenPetra. The class provides two key static methods: GetCountryName retrieves a country's name based on its country code, and GetCountryIntlTelephoneCode retrieves a country's international telephone code. Both methods use a delegate parameter to access cached data tables, specifically working with the TCacheableCommonTablesEnum enumeration to locate the CountryList table. The methods return empty strings if the requested information cannot be found.

 **Code Landmarks**
- `Line 43`: Uses a delegate pattern (TGetCacheableDataTableFromCache) to abstract data retrieval from different cache sources (client or server)
- `Line 47`: Leverages Enum.GetName to dynamically convert enum values to strings for cache lookup
### Constants.cs

MCommonConstants defines various constants used throughout the OpenPetra system. It includes isolation levels for cacheable DataTables, data type definitions for office-specific data labels (like string, decimal, date), form design codes (SYSTEM, PARTNER, etc.), form design type codes, gift options, and form letter contexts. The class also contains numerous localized string constants related to data importing operations, including error messages, validation warnings, and information strings. These constants provide standardized values and messages for consistent use across the application.

 **Code Landmarks**
- `Line 36`: Defines isolation level for cacheable DataTables, important for database transaction management
- `Line 42`: User default setting for date format interpretation during imports
- `Line 100-126`: Comprehensive set of data type constants for office-specific data, supporting various business data needs
- `Line 130-180`: Form design codes and options that standardize document generation across the system
### FormData.cs

FormData.cs implements classes for generating form letters and mail merge documents in OpenPetra. It defines a comprehensive set of data structures to hold partner information for export to Word or Excel documents. The main classes include TFormLetterInfo for managing form letter creation processes, TFormDataPartnerList for organizing partner data, and various TFormData-derived classes that represent different entity types like partners, persons, gifts, and contact details. The file implements functionality for retrieving formatted data, handling formality levels for salutations, managing address blocks, and supporting specialized data for finance letters. Key classes include TFormDataPartner, TFormDataPerson, TFormDataGift, and TFormDataContactDetail, each containing properties that correspond to mail merge fields in templates.

 **Code Landmarks**
- `Line 191`: Implements a sophisticated algorithm for finding the best formality record match based on language, country, addressee type and formality level
- `Line 354`: BuildRetrievalList method dynamically determines which data sections need to be retrieved based on the tags present in templates
- `Line 582`: Supports splitting email addresses to apply the same partner data to multiple rows in output documents
- `Line 1037`: Specialized classes for different entity types (Person, Unit, Gift, etc.) create a comprehensive object model for mail merge operations
### Types.cs

This file defines types and conversion methods for the MCommon module in OpenPetra. It includes the TOfficeSpecificDataLabelUseEnum enumeration for categorizing different partner types and data labels, and the MCommonTypes class which provides static conversion methods between partner classes and their corresponding enum values. Key methods include PartnerClassEnumToOfficeSpecificDataLabelUseEnum, OfficeSpecificDataLabelUseToPartnerClassEnum, and PartnerClassStringToOfficeSpecificDataLabelUseEnum, which handle conversions between different representations of partner classifications in the system.

 **Code Landmarks**
- `Line 96`: Conversion method throws ArgumentException when partner class cannot be converted to an office-specific data label enum
- `Line 149`: Bidirectional conversion between partner classes and office-specific data labels enables flexible type handling across the system

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #