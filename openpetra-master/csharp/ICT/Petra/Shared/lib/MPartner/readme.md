# C# Shared Partner Module Of OpenPetra

The OpenPetra Shared Partner Module is a C# component that provides the core data structures, security mechanisms, and type definitions for managing partner entities within the OpenPetra application. This module implements fundamental partner management capabilities and security controls that serve as the foundation for the partner-related functionality throughout the system.

The module provides these capabilities to the OpenPetra program:

- Comprehensive partner data modeling through typed datasets
- Partner access security and permission enforcement
- Standardized constants for partner types, statuses, and attributes
- Custom exception handling for partner-specific operations
- Location and partner key management

## Identified Design Elements

1. **Strongly Typed Data Structure**: The module uses XML-defined typed datasets to create a robust data model for partner information, ensuring type safety and relationship integrity.
2. **Layered Security Model**: Security implementation provides granular access control based on user permissions, group membership, and foundation ownership.
3. **Consistent Data Representation**: Extensive constants and enumerations ensure uniform handling of partner data across the application.
4. **Specialized Exception Handling**: Custom exceptions provide clear error information specific to partner operations, enhancing troubleshooting.
5. **Location Management**: Dedicated structures for handling partner locations with proper equality comparison and deep copy capabilities.

## Overview
The architecture emphasizes type safety through strongly-typed datasets, enforces security through a comprehensive permission model, and maintains data integrity through well-defined constants and enumerations. The module serves as the foundation for all partner-related operations in OpenPetra, providing both the data structures and business rules necessary for partner management. Its design supports extensibility while maintaining strict access control and data validation.

## Business Functions

### Partner Management
- `Constants.cs` : Constants file defining standard values used throughout the Partner module of OpenPetra.
- `Types.cs` : Defines enumerations and classes for partner data management in OpenPetra.

### Security
- `Security.cs` : Security utility class for managing partner access control in OpenPetra's partner management module.
- `Exceptions.cs` : Defines a custom exception for partner security access violations in OpenPetra's partner management module.

### Error Handling
- `Main.cs` : Defines custom exception classes for partner-related errors in the OpenPetra partner management module.

## Files
### Constants.cs

MPartnerConstants defines a comprehensive collection of string and numeric constants used throughout the Partner module of OpenPetra. It includes constants for partner types (PERSON, FAMILY, ORGANISATION), partner statuses (ACTIVE, INACTIVE), location types, gender designations, address types, banking types, and contact methods. The file also contains numerous constants for partner import field names, consent types, and translated strings for partner merge operations. These constants ensure consistency in data handling and user interface messages across the application.

 **Code Landmarks**
- `Line 374`: The file uses readonly fields with Catalog.GetString() for translatable constants, enabling internationalization of user-facing messages
- `Line 1`: This constants file serves as a central reference point for the entire Partner module, promoting consistency across the codebase
### Exceptions.cs

This file defines ESecurityPartnerAccessDeniedException, a custom exception class that extends ESecurityAccessDeniedException to handle security violations when accessing partner data. The class stores information about the denied access including the partner key, partner short name, and access level. It implements multiple constructors for different initialization scenarios and provides properties to access the stored partner information. The class also implements the necessary serialization methods for .NET remoting support through GetObjectData and a specialized constructor that accepts SerializationInfo and StreamingContext parameters.

 **Code Landmarks**
- `Line 38`: Custom exception class specifically for partner access security violations
- `Line 119`: Constructor that captures partner-specific security context including partner key, name and access level
- `Line 147`: Implementation of serialization support for remoting exceptions across application boundaries
### Main.cs

This file implements three custom exception classes for the OpenPetra partner management module. EPartnerNotExistantException is thrown when a business object attempts to load data for a non-existent, deleted, or inactive partner. EPartnerLocationNotExistantException signals that a partner location key doesn't exist in the database. EPartnerFamilyIDException indicates problems with finding a family ID. Each exception class includes multiple constructors for different initialization scenarios and implements proper serialization support for .NET remoting through the GetObjectData method. All exceptions inherit from EOPAppException, providing consistent error handling throughout the application.

 **Code Landmarks**
- `Line 38`: Custom exception for partner not found scenarios inherits from application-specific base exception class
- `Line 97`: All exception classes implement proper serialization support for .NET remoting
- `Line 84`: Null check in GetObjectData ensures serialization information is valid
### Security.cs

TSecurity implements a static class that provides security-related functions for partner access control in OpenPetra. It determines whether users can access specific partners based on restrictions and permissions. Key functionality includes checking if a user has access to a partner, evaluating foundation security, and throwing appropriate exceptions when access is denied. Important constants include PARTNER_RESTRICTED_TO_GROUP and PARTNER_RESTRICTED_TO_USER. Critical methods are CanAccessPartner, CanAccessPartnerExc, AccessLevelExceptionEvaluatorAndThrower, and CheckFoundationSecurity. The class works with partner data rows and foundation information to enforce security rules based on user permissions, group membership, and foundation ownership.

 **Code Landmarks**
- `Line 72`: Partner access control logic that checks both user-specific and group-specific restrictions
- `Line 100`: Foundation-specific security check for organizational partners
- `Line 132`: Exception-throwing wrapper around access control that ensures denied access doesn't go unnoticed
- `Line 176`: Foundation security implementation with special handling for development users/admins
### Types.cs

Types.cs defines essential enumerations and classes for the Partner module in OpenPetra. It includes enumerations for partner edit tab pages (TPartnerEditTabPageEnum), partner information scope (TPartnerInfoScopeEnum), short name formatting (eShortNameFormat), attribute value types (TPartnerAttributeTypeValueKind), and merge actions (TMergeActionEnum). The file also implements TLocationPK class for storing site and location key combinations with equality comparison and hash code generation, plus a static TLocationPKCopyHelper class for creating deep copies of TLocationPK arrays.

 **Code Landmarks**
- `Line 176`: TLocationPK implements equality comparison for location identifiers, essential for partner location management
- `Line 201`: TLocationPKCopyHelper provides deep copying functionality for two-dimensional location arrays, supporting array extension

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #