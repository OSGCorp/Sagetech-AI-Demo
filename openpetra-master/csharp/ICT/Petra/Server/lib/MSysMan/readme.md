# C# System Management Module Of OpenPetra

The OpenPetra System Management Module is a C# implementation that provides core infrastructure services for the OpenPetra application. The module handles authentication, authorization, user management, database versioning, and system configuration. This sub-project implements critical security and administrative functionality along with data management capabilities that provide these capabilities to the OpenPetra program:

- User authentication and permission management
- Database version control and upgrade mechanisms
- System and user configuration management
- Security logging and audit trails
- Data import/export functionality

## Identified Design Elements

1. **Layered Security Architecture**: The module implements multiple security layers including authentication, authorization, and comprehensive audit logging for system access and user activities.
2. **Database Versioning System**: Provides a robust mechanism for tracking database versions and applying incremental upgrades through reflection-based method discovery.
3. **Pluggable Authentication**: Supports both database-based authentication and plugin-based methods for flexible identity management.
4. **Configuration Management**: Implements hierarchical configuration with system-wide defaults and user-specific preferences.
5. **Data Portability**: Offers comprehensive import/export capabilities for database content in compressed YML format.

## Overview

The architecture follows a service-oriented approach with clear separation between authentication, authorization, and user management concerns. The database upgrade system provides a maintainable path for schema evolution through versioned SQL scripts and corresponding C# handlers. Security features include password quality validation, account locking mechanisms, and comprehensive audit logging. The module serves as the administrative backbone of OpenPetra, providing essential infrastructure services while maintaining strict security controls through permission requirements on all administrative functions.

## Business Functions

### System Management
- `TableAccessPermissionManager.cs` : Wrapper class for table access permission management in OpenPetra's security system.
- `ImportExport.cs` : Server-side module for importing and exporting OpenPetra database data in compressed YML format.
- `ErrorLog.cs` : Facade class for error logging in OpenPetra's system management module.
- `LoginLog.cs` : Manages login activity logging for OpenPetra's security system.
- `web/Settings.cs` : Server-side settings management for OpenPetra, handling site configuration, user setup, and system initialization.
- `LanguageCultureSettings.cs` : Manages user language and culture settings in OpenPetra's system management module.
- `GroupManager.cs` : Manages security groups for users in the OpenPetra system.
- `ServerLookups.cs` : Server-side lookup functions for MSysMan module providing database version and patch information.
- `Cacheable.ManualCode.cs` : Provides cacheable data tables for MSysMan database tables that can be stored on the client side.
- `Common/UserDefaults.cs` : Manages user preferences and default settings in OpenPetra with methods to retrieve and store user-specific configuration values.
- `Common/SystemDefaults.cs` : Server-side connector for accessing and managing system defaults in OpenPetra with permission controls.
- `UserManager.cs` : Manages user authentication, security, and account management for OpenPetra system.
- `UserManagement.cs` : User management web connector for OpenPetra with authentication and password handling functionality.
- `validation/SysMan.Validation.cs` : Validates user data in the SysMan module, focusing on password security and quality checks.

### Database Upgrades
- `DBUpgrades/Upgrade202004_202006.cs` : Database upgrade script that adds data consent tables and country permissions for OpenPetra version 2020-06.
- `DBUpgrades/Upgrade202002_202003.cs` : Database upgrade script that modifies text fields in tables when upgrading from version 2020-02 to 2020-03.
- `DBUpgrades/Upgrade202207_202301.cs` : Database upgrade script that adds SEPA mandate fields to recurring gifts and creates partner membership payment tracking tables.
- `DBUpgrades/Upgrade201908_201909.cs` : Database upgrade script for migrating from version 201908 to 201909 in the OpenPetra system.
- `DBUpgrades/Upgrade202010_202012.cs` : Database upgrade script for OpenPetra version 2020-12 that contains no actual database structure changes.
- `DBUpgrades/Upgrade202003_202004.sql` : SQL script for database schema upgrades from version 202003 to 202004 in OpenPetra.
- `DBUpgrades/Upgrade202202_202206.cs` : Database upgrade script that adds a user ID column to the session table for version 2022-06.
- `DBUpgrades/Upgrade202207_202301.sql` : SQL database upgrade script that adds membership management tables and fields to OpenPetra.
- `DBUpgrades/Upgrade202012_202101.cs` : Database upgrade script for OpenPetra from version 202012 to 202101 that adds banking type data.
- `DBUpgrades/Upgrade201912_202002.cs` : Database upgrade module for OpenPetra version 2020-02 with no structural changes.
- `DBUpgrades/Upgrade202004_202006.sql` : SQL script for database upgrade implementing GDPR consent tracking tables and data.
- `DBUpgrades/Upgrade202104_202110.cs` : Database upgrade module for OpenPetra version 2021.04 to 2021.10 with no structural changes.
- `DBUpgrades/Upgrade202009_202010.sql` : SQL script that updates the database schema by adding a new type category for sponsored child status.
- `DBUpgrades/Upgrade202110_202202.cs` : Database upgrade script for OpenPetra that adds a new publication type for thank you letters without receipts.
- `DBUpgrades/DBUpgrades.cs` : Database upgrade manager for OpenPetra that handles version tracking and schema updates.
- `DBUpgrades/Upgrade202101_202102.cs` : Database upgrade module for transitioning from version 202101 to 202102 with no structural changes.
- `DBUpgrades/Upgrade202102_202104.cs` : Database upgrade module for OpenPetra that handles version migration from 2021-02 to 2021-04.
- `DBUpgrades/Upgrade201911_201912.cs` : Database upgrade script for OpenPetra that fixes motivation detail activation status.
- `DBUpgrades/Upgrade202301_202302.cs` : Database upgrade script for OpenPetra version 2023-01 to 2023-02 with no structural changes.
- `DBUpgrades/Upgrade201908_201909.sql` : SQL script that creates a database table for storing report results in OpenPetra.
- `DBUpgrades/Upgrade202002_202003.sql` : SQL script that modifies column types to longtext in MySQL database tables for OpenPetra.
- `DBUpgrades/Upgrade202206_202207.cs` : Database upgrade script that adds SEPA mandate fields to the recurring gift table for version 2022-07.
- `DBUpgrades/Upgrade202003_202004.cs` : Database upgrade script for OpenPetra from version 2020-03 to 2020-04.
- `DBUpgrades/Upgrade201910_201911.cs` : Database upgrade module for OpenPetra from version 2019-10 to 2019-11.
- `DBUpgrades/Upgrade202009_202010.cs` : Database upgrade script for migrating from version 202009 to 202010 in OpenPetra.
- `DBUpgrades/Upgrade202006_202009.cs` : Database upgrade module for OpenPetra that handles version migration from 2020-06 to 2020-09.
- `DBUpgrades/Upgrade201909_201910.sql` : SQL script that adds Partner Self-Service module and user to the OpenPetra database.
- `DBUpgrades/Upgrade201909_201910.cs` : Database upgrade script for migrating from version 2019-09 to 2019-10, implementing partner self-service functionality.

## Files
### Cacheable.ManualCode.cs

TCacheable implements functionality for retrieving cacheable DataTables from the MSysMan subsystem that can be stored client-side. The class provides methods to fetch complete tables with all columns and rows. It includes a general GetCacheableTable method that accepts a TCacheableSysManTablesEnum parameter to specify which table to retrieve, and a private GetUserListTable method that loads user data and enhances it with a combined last and first name column for display purposes. The implementation supports the OpenPetra system's caching strategy for improved performance.

 **Code Landmarks**
- `Line 73`: Creates a derived column that combines first and last name fields using an expression, demonstrating DataTable column manipulation
### Common/SystemDefaults.cs

TSystemDefaultsConnector implements a static class that provides a secure interface for reading and updating system defaults in OpenPetra. It acts as a wrapper around the TSystemDefaults class, adding permission requirements to all methods. The class offers functionality to check if defaults exist, retrieve default values in various data types (boolean, char, numeric types, string), and set new default values. Each method is decorated with the RequireModulePermission attribute to enforce SYSMAN module access. The class provides convenience methods for type-specific retrieval with optional default values and special handling for the SiteKey system default.

 **Code Landmarks**
- `Line 43`: Uses RequireModulePermission attribute to enforce security on all system default operations
- `Line 70`: Implements overloaded methods for each data type with default value fallbacks
- `Line 231`: Special handling for SiteKey default that can be changed during runtime
- `Line 255`: Provides methods to modify system defaults with tracking of whether values were added or updated
### Common/UserDefaults.cs

TUserDefaults implements server-side functionality for reading and saving user preferences in OpenPetra. It provides methods to retrieve and store various data types (boolean, char, numeric, string) from the s_user_defaults database table. The class offers both type-specific getters (GetBooleanDefault, GetInt32Default, etc.) and general methods like GetUserDefault and SetDefault. It also handles propagating changes to connected clients through a notification system that queues client tasks when preferences are modified. The class supports both single and batch updates to user defaults and includes transaction management for database operations.

 **Code Landmarks**
- `Line 77`: NoRemoting attribute prevents these methods from being exposed through remote interfaces, ensuring they're only used server-side
- `Line 185`: Database transaction pattern with callback delegates for safe database operations
- `Line 289`: Client notification system that propagates user preference changes to all connected clients
- `Line 326`: Cross-session synchronization ensures user defaults are consistent across multiple client instances
### DBUpgrades/DBUpgrades.cs

TDBUpgrades implements functionality to upgrade the OpenPetra database schema to newer versions. It provides methods to retrieve the current database version, set a new version, and execute appropriate upgrade methods based on version comparisons. The class uses reflection to find and invoke upgrade methods that match specific version patterns, ensuring the database schema stays compatible with the application version. Key methods include UpgradeDatabase(), GetCurrentDBVersion(), and SetCurrentDBVersion(). The implementation checks if upgrade methods are compatible with the current application version before applying them.

 **Code Landmarks**
- `Line 85`: Uses reflection to dynamically find and invoke appropriate upgrade methods based on version patterns
- `Line 90`: Version comparison ensures upgrades are only applied if compatible with current application version
- `Line 46`: Database version is stored in s_system_defaults table with code 'CurrentDatabaseVersion'
### DBUpgrades/Upgrade201908_201909.cs

TDBUpgrade implements a static class responsible for upgrading the OpenPetra database from version 2019-08 to 2019-09. The file contains a single method, UpgradeDatabase201908_201909, which executes SQL statements from an external SQL file named 'Upgrade201908_201909.sql'. The method creates a database transaction, reads and splits the SQL file by semicolons, and executes each non-empty statement within the transaction. The upgrade specifically adds a new table called 's_report_result' to the database schema. The method always returns true upon completion.

 **Code Landmarks**
- `Line 41`: Uses a delegate pattern with WriteTransaction to ensure proper transaction handling
- `Line 45`: Reads SQL statements from an external file rather than hardcoding them in the upgrade method
- `Line 47`: Splits SQL statements by semicolons to execute multiple commands from a single file
### DBUpgrades/Upgrade201908_201909.sql

This SQL script creates the s_report_result table in the OpenPetra database as part of a database upgrade from version 201908 to 201909. The table stores report execution results including report ID, session ID, validity period, parameters, HTML output, success status, error messages, and standard audit fields. It includes primary and foreign key constraints linking to the s_user table for tracking who created and modified records.

 **Code Landmarks**
- `Line 2`: Uses composite primary key structure with report_id and session_id columns
- `Line 6`: Includes s_result_html_c field to store HTML report output directly in the database
- `Line 12`: Implements automatic timestamp updating with CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
### DBUpgrades/Upgrade201909_201910.cs

TDBUpgrade implements a database upgrade function for OpenPetra, specifically handling the migration from version 2019-09 to 2019-10. The file contains a single static method, UpgradeDatabase201909_201910, which executes SQL statements from an external file 'Upgrade201909_201910.sql' within a database transaction. The upgrade adds a new PARTNERSELFSERVICE module and SELFSERVICE user to the system. The method reads the SQL file, splits statements by semicolons, and executes each non-empty statement against the database using the provided TDataBase instance.

 **Code Landmarks**
- `Line 43`: Uses a delegate within WriteTransaction to execute SQL statements in a transaction context
- `Line 46`: Reads external SQL file and splits by semicolons to execute multiple statements sequentially
### DBUpgrades/Upgrade201909_201910.sql

This SQL script performs two database insertions for the OpenPetra system. It adds a new module called 'PARTNERSELFSERVICE' with the description 'Self-Service module for partner' to the s_module table. It also creates a new user account 'SELFSERVICE' in the s_user table with the account locked flag set to true, indicating this is a system account that cannot be directly logged into.

 **Code Landmarks**
- `Line 1`: Creates a module entry for partner self-service functionality, extending OpenPetra's modular architecture
- `Line 2`: Creates a locked system user account specifically for self-service operations, following security best practices
### DBUpgrades/Upgrade201910_201911.cs

TDBUpgrade implements a database upgrade utility for OpenPetra. This file contains a specific upgrade method (UpgradeDatabase201910_201911) that handles the transition from version 2019-10 to 2019-11. The method takes a TDataBase parameter but performs no actual database structure changes, simply returning true to indicate successful completion. The class is part of the MSysMan.DBUpgrades namespace and contributes to OpenPetra's database versioning system.

 **Code Landmarks**
- `Line 40`: The upgrade method is implemented as a no-op, indicating no database structure changes were needed for this particular version upgrade.
### DBUpgrades/Upgrade201911_201912.cs

TDBUpgrade implements a database upgrade function for OpenPetra, specifically upgrading from version 2019-11 to 2019-12. The file contains a single static method UpgradeDatabase201911_201912 that executes a database transaction to fix an issue with motivation details where new details were not being properly activated. The method uses a write transaction to execute an SQL update statement that sets the a_motivation_status_l field to 1 for all records in the a_motivation_detail table, ensuring all motivation details are properly activated.

 **Code Landmarks**
- `Line 43`: Uses a delegate pattern within the WriteTransaction method to encapsulate the database operations
### DBUpgrades/Upgrade201912_202002.cs

TDBUpgrade implements a database upgrade module for OpenPetra, specifically handling the transition from version 2019-12 to 2020-02. The file defines a static partial class that contains a single method, UpgradeDatabase201912_202002, which takes a TDataBase parameter. This method is designed to perform database structure modifications during version upgrades, but in this particular case, it simply returns true as there are no changes required to the database structure for this version upgrade.
### DBUpgrades/Upgrade202002_202003.cs

TDBUpgrade implements a database upgrade function for OpenPetra that transitions the database from version 2020-02 to 2020-03. The file contains a single static method UpgradeDatabase202002_202003 that executes SQL statements from an external SQL file within a database transaction. The method reads the SQL file, splits it by semicolons, and executes each non-empty statement against the database. The upgrade process modifies text fields in various tables. The function uses TDataBase and TDBTransaction classes to manage database operations and returns true upon completion.

 **Code Landmarks**
- `Line 42`: Uses a delegate pattern with WriteTransaction to ensure proper transaction handling
- `Line 46`: Reads SQL statements from an external file rather than hardcoding them in the upgrade method
- `Line 48`: Splits SQL statements by semicolons to execute them individually
### DBUpgrades/Upgrade202002_202003.sql

This SQL script performs database schema modifications for an OpenPetra upgrade from version 202002 to 202003. It alters two tables (s_report_result and p_form) by changing specific columns (s_result_html_c and p_template_document_c) to use the longtext data type in MySQL. The script includes a comment noting that this modification is MySQL-specific and that PostgreSQL implementations should use the text data type instead.

 **Code Landmarks**
- `Line 1`: The comment indicates database platform differences between MySQL and PostgreSQL for handling large text fields
### DBUpgrades/Upgrade202003_202004.cs

TDBUpgrade implements a static class responsible for upgrading the OpenPetra database from version 2020-03 to 2020-04. The file contains a single method, UpgradeDatabase202003_202004, which executes SQL statements from an external SQL file named 'Upgrade202003_202004.sql'. The method creates a database transaction, reads and splits the SQL file by semicolons, and executes each non-empty statement against the database. The upgrade specifically adds new types for sponsorship functionality. The method always returns true upon completion, indicating a successful upgrade process.

 **Code Landmarks**
- `Line 40`: Uses a delegate pattern with WriteTransaction to ensure proper transaction handling
- `Line 44`: Reads SQL from external file rather than embedding SQL in code, promoting better separation of concerns
- `Line 46`: Splits SQL statements by semicolons to execute multiple statements from a single file
### DBUpgrades/Upgrade202003_202004.sql

This SQL script performs database schema upgrades for OpenPetra, transitioning from version 202003 to 202004. It adds new partner types for child status tracking, creates system modules for partner and sponsorship management, and modifies several tables including p_partner_reminder, s_group_partner_reminder, and p_family. The script adds primary keys, modifies column types to longtext for comments, adds new columns for family data like birth date and gender, and creates a new s_module_table_access_permission table with initial permissions for finance-related tables. These changes support enhanced sponsorship management and partner tracking capabilities.

 **Code Landmarks**
- `Line 8`: Adds system modules specifically for sponsorship management functionality
- `Line 10`: Restructures p_partner_reminder table with proper primary key and unique constraints
- `Line 19`: Creates sequence table for auto-incrementing partner reminder IDs
- `Line 29`: Adds demographic fields to p_family table including gender and photo storage
- `Line 40`: Implements table-level access control system with granular CRUD permissions
### DBUpgrades/Upgrade202004_202006.cs

TDBUpgrade implements a database upgrade process for OpenPetra from version 2020-04 to 2020-06. The file contains a single static method UpgradeDatabase202004_202006 that executes SQL statements from an external file 'Upgrade202004_202006.sql' within a database transaction. The upgrade adds new tables for data consent functionality and permissions for country data. The method reads the SQL file, splits statements by semicolons, and executes each non-empty statement against the database. Key components include the TDBUpgrade class, UpgradeDatabase202004_202006 method, and the transaction handling using TDBTransaction.

 **Code Landmarks**
- `Line 45`: Uses a delegate pattern within WriteTransaction to ensure proper transaction handling
- `Line 47`: Reads SQL from external file and splits by semicolons for execution, allowing separation of code and SQL logic
### DBUpgrades/Upgrade202004_202006.sql

This SQL script creates database tables for GDPR compliance in OpenPetra, implementing a consent tracking system. It defines four tables: p_consent_channel (tracks communication channels), p_consent_history (records consent events), p_consent_purpose (defines data usage purposes), and p_consent_history_permission (links consent entries to purposes). The script also creates a sequence table for entry IDs, populates the channel and purpose tables with initial values, adds a finance module permission, and updates the p_type table with a system type flag.

 **Code Landmarks**
- `Line 1`: Creates a comprehensive GDPR consent tracking system with related tables
- `Line 148`: Implements many-to-many relationship between consent history and purposes
- `Line 199`: Uses auto-increment sequence table for consent entry IDs
- `Line 211`: Handles potential duplicate key issues with ON DUPLICATE KEY UPDATE clause
### DBUpgrades/Upgrade202006_202009.cs

TDBUpgrade implements a static class responsible for database upgrades in OpenPetra's system management module. This specific file contains the UpgradeDatabase202006_202009 method that would handle database structure changes when upgrading from version 2020-06 to version 2020-09. However, this particular upgrade implementation is minimal as it indicates no changes in database structure are needed, simply returning true to signal successful completion. The class is part of the MSysMan.DBUpgrades namespace within the server-side codebase.

 **Code Landmarks**
- `Line 39`: The method returns true without making any changes, indicating this was a version increment without database schema modifications
### DBUpgrades/Upgrade202009_202010.cs

TDBUpgrade implements a database upgrade function for OpenPetra to migrate from version 2020-09 to 2020-10. The file contains a single static method, UpgradeDatabase202009_202010, which executes SQL statements from an external file 'Upgrade202009_202010.sql' within a database transaction. The method reads the SQL file, splits it by semicolons, and executes each non-empty statement against the database. The upgrade specifically updates status options for sponsored children. The function uses TDataBase and TDBTransaction classes for database operations and always returns true upon completion.

 **Code Landmarks**
- `Line 41`: Uses a delegate within WriteTransaction for transaction management, ensuring proper commit/rollback handling
- `Line 43`: Reads SQL statements from an external file rather than hardcoding them in the upgrade method
- `Line 45`: Splits SQL statements by semicolons to execute multiple commands from a single file
### DBUpgrades/Upgrade202009_202010.sql

This SQL script performs database schema updates for OpenPetra version 202009 to 202010. It adds a new type category called 'SPONSORED_CHILD_STATUS' to track status options for sponsored children, and updates existing child status types to associate them with this new category. The script helps organize child sponsorship data by properly categorizing status options like 'BOARDING_SCHOOL', 'CHILDREN_HOME', 'CHILD_DIED', 'HOME_BASED', and 'PREVIOUS_CHILD'.

 **Code Landmarks**
- `Line 1`: Creates a non-deletable type category specifically for tracking sponsored child statuses
### DBUpgrades/Upgrade202010_202012.cs

TDBUpgrade implements a database upgrade module for OpenPetra's system management functionality. This file specifically handles the upgrade from version 2020-10 to 2020-12, though it contains no actual database structure changes. The class provides a static method UpgradeDatabase202010_202012 that takes a TDataBase parameter and simply returns true, indicating a successful upgrade with no modifications needed to the database schema.

 **Code Landmarks**
- `Line 38`: The upgrade method returns true without making any changes, suggesting this is a placeholder for version compatibility or represents a release with only non-database changes.
### DBUpgrades/Upgrade202012_202101.cs

TDBUpgrade implements a database upgrade function for OpenPetra from version 202012 to 202101. While no structural changes are made to the database, the upgrade adds default data to banking-related tables if they are empty. The UpgradeDatabase202012_202101 method checks if PUB_p_banking_type and PUB_p_banking_details_usage_type tables are empty, and if so, inserts default values for bank account types and banking detail usage types within a transaction. The function uses SQL queries to check table contents and insert data as needed.

 **Code Landmarks**
- `Line 42`: Uses a delegate within a transaction to ensure data consistency during the upgrade process
- `Line 46`: Checks if tables are empty before adding data, making the upgrade script idempotent
### DBUpgrades/Upgrade202101_202102.cs

TDBUpgrade implements a database upgrade module for transitioning from version 202101 to 202102. This file is part of the MSysMan namespace in OpenPetra's server component. The class defines a single static method UpgradeDatabase202101_202102 that takes a TDataBase parameter but performs no actual database structure modifications, simply returning true to indicate successful completion. This represents a version increment without schema changes.

 **Code Landmarks**
- `Line 39`: The upgrade method returns true without making any changes, indicating a version increment with no database structure modifications
### DBUpgrades/Upgrade202102_202104.cs

TDBUpgrade implements a static class responsible for database version upgrades in OpenPetra. This file specifically contains the UpgradeDatabase202102_202104 method that would handle database structure changes when upgrading from version 2021-02 to 2021-04. However, this particular upgrade doesn't make any database structure modifications, as indicated by the method simply returning true without performing any operations.

 **Code Landmarks**
- `Line 38`: The upgrade method is empty and returns true, indicating no database structure changes were needed for this version upgrade.
### DBUpgrades/Upgrade202104_202110.cs

TDBUpgrade implements a database upgrade utility for OpenPetra, specifically handling the migration from version 2021.04 to 2021.10. This file contains a single static method UpgradeDatabase202104_202110 that takes a TDataBase parameter but performs no actual database structure modifications, simply returning true to indicate successful completion. The file is part of the MSysMan.DBUpgrades namespace within the OpenPetra server infrastructure, supporting the system's database versioning mechanism.

 **Code Landmarks**
- `Line 39`: The upgrade method is empty and returns true, indicating no database structure changes were needed for this version upgrade.
### DBUpgrades/Upgrade202110_202202.cs

TDBUpgrade implements a database upgrade function for OpenPetra, transitioning from version 202110 to 202202. The file contains a single static method UpgradeDatabase202110_202202 that doesn't modify database structure but adds data if needed. It specifically checks if the 'THANKYOU_NO_RECEIPT' publication type exists in the PUB_p_type table and inserts it if missing. The method uses a transaction to ensure data integrity while making changes. The upgrade adds support for thank you letters without gift receipts in the publication system.

 **Code Landmarks**
- `Line 40`: Uses a transaction pattern with delegate to ensure database changes are atomic
- `Line 44`: Performs existence check before insertion to prevent duplicate records
### DBUpgrades/Upgrade202202_202206.cs

TDBUpgrade implements a database upgrade function for OpenPetra, specifically handling the transition from version 2022-02 to 2022-06. The main functionality is adding a user ID column to the PUB_s_session table if it doesn't already exist. The file contains a single static method UpgradeDatabase202202_202206 that executes within a database transaction, checks for the column's existence by attempting a query, and adds the column if needed. The method uses TDataBase and TDBTransaction objects to manage the database connection and transaction.

 **Code Landmarks**
- `Line 48`: Uses a try-catch block as a way to detect if a column exists rather than querying metadata
- `Line 39`: Uses delegate pattern for transaction management to ensure proper commit/rollback
### DBUpgrades/Upgrade202206_202207.cs

TDBUpgrade implements a database upgrade process from version 2022-06 to 2022-07. The file contains a single static method UpgradeDatabase202206_202207 that adds two new columns to the PUB_a_recurring_gift table: a_sepa_mandate_reference_c and a_sepa_mandate_given_d. The method first checks if the columns already exist by attempting to query data from the potential column, then executes ALTER TABLE statements within a transaction if needed. The upgrade is part of OpenPetra's database versioning system to support SEPA (Single Euro Payments Area) mandate tracking for recurring gifts.

 **Code Landmarks**
- `Line 54`: Uses a try-catch block as a column existence check rather than querying database metadata
- `Line 43`: Uses delegate pattern with WriteTransaction to ensure proper transaction handling
### DBUpgrades/Upgrade202207_202301.cs

TDBUpgrade implements database schema upgrades from version 202207 to 202301 in OpenPetra. It adds SEPA mandate reference and date columns to the PUB_a_recurring_gift table if they don't exist, supporting European payment processing. The script also checks for and creates a new PUB_p_partner_membership_paid table by executing SQL statements from an external file. The upgrade process runs within a database transaction to ensure data integrity, with error handling to check for existing columns and tables before attempting modifications.

 **Code Landmarks**
- `Line 48`: Uses a transaction with delegate pattern to ensure database changes are atomic
- `Line 53`: Employs try-catch to detect if columns exist rather than querying metadata tables
- `Line 87`: Reads external SQL file for complex schema changes rather than embedding all SQL in code
### DBUpgrades/Upgrade202207_202301.sql

This SQL script implements database schema changes for version upgrade from 202207 to 202301, focusing on membership management functionality. It creates three new tables: p_partner_membership_paid (tracking payments and service hours), p_partner_membership (storing membership relationships), and p_membership (defining membership types). The script establishes primary keys, foreign key constraints, and indexes for these tables. Additionally, it modifies the existing a_motivation_detail table by adding three boolean columns to track sponsorship, membership, and worker support flags. The script includes comprehensive field documentation and proper MySQL syntax for InnoDB tables with UTF8 character encoding.

 **Code Landmarks**
- `Line 3`: Creates a table structure for tracking membership payments with dual-purpose fields for both monetary payments and service hours
- `Line 39`: Implements a partner membership tracking system with status management and date tracking
- `Line 75`: Defines membership types with both financial fee options and service hour requirements
- `Line 164`: Extends the existing motivation detail table with boolean flags for different contribution types
### DBUpgrades/Upgrade202301_202302.cs

TDBUpgrade implements a database upgrade function for OpenPetra from version 2023-01 to 2023-02. The file contains a single static method UpgradeDatabase202301_202302 that takes a TDataBase parameter but performs no actual database changes, simply returning true to indicate successful completion. This is a placeholder upgrade script that maintains version continuity without modifying the database structure.

 **Code Landmarks**
- `Line 38`: The upgrade method returns true without making any changes, indicating a version increment without structural database modifications.
### ErrorLog.cs

TErrorLog implements a facade class that provides methods for recording errors in OpenPetra's error log table. The class offers two overloaded AddErrorLogEntry methods that accept parameters such as error code, context, message lines, user ID, and process ID. Both methods delegate to identically named methods in the Ict.Petra.Server.App.Core.Security.ErrorLog namespace. The class serves as a simplified interface to the error logging functionality, allowing other components to record errors without directly interacting with the core security implementation.

 **Code Landmarks**
- `Line 56`: Method appears to call itself recursively, which is likely a bug that could cause stack overflow errors
- `Line 77`: Second method also appears to call itself recursively rather than the intended core implementation
### GroupManager.cs

TGroupManager implements functionality for working with security groups in the OpenPetra system. The class provides methods to retrieve user group assignments from the database. The primary function LoadUserGroups retrieves all groups associated with a specific user ID by querying the SUserGroup table via data access methods. It returns an array of group IDs that the specified user belongs to. The class interacts with database transaction objects and uses ArrayList for intermediate storage before converting results to a string array.

 **Code Landmarks**
- `Line 47`: Uses database transaction parameter pattern for consistent transaction handling across the application.
- `Line 59`: Optimizes memory allocation by pre-sizing the ArrayList to the exact number of groups found.
### ImportExport.cs

ImportExport.cs implements functionality for exporting and importing OpenPetra database content. The TImportExportWebConnector class provides methods to export all database tables to a compressed YML format and restore databases from such exports. Key functionality includes exporting tables by module, handling data type conversions, managing database sequences, and supporting database reset operations. The file also implements TImportExportManager which exposes backup/restore functionality to the server admin console. Important methods include ExportAllTables, ResetDatabase, LoadTable, and LoadSequence. The code handles proper transaction management and provides progress tracking during lengthy operations.

 **Code Landmarks**
- `Line 63`: Uses a RequireModulePermission attribute to enforce SYSMAN permission for sensitive database export operations
- `Line 182`: Implements custom sequence export to maintain database integrity during import/export operations
- `Line 217`: Uses TProgressTracker to provide real-time feedback during lengthy database restoration process
- `Line 323`: Handles special cases for various tables to ensure data integrity during import
- `Line 371`: Uses parameterized queries with proper type handling to prevent SQL injection
### LanguageCultureSettings.cs

TMaintainLanguageSettingsWebConnector implements functionality for managing user interface language and culture preferences in OpenPetra. The class provides two key methods: SetLanguageAndCulture for storing a user's language and culture code preferences, and GetLanguageAndCulture for retrieving these settings. Both methods interact with the TUserDefaults system to persist and retrieve these user-specific settings. The [NoRemoting] attribute indicates these methods are intended for internal server-side use rather than remote client calls.

 **Code Landmarks**
- `Line 43`: Uses TUserDefaults system to persist user preferences with the true parameter indicating permanent storage
- `Line 33`: [NoRemoting] attribute prevents these methods from being exposed to remote clients despite being in a WebConnector class
### LoginLog.cs

TLoginLog implements a class that records user login activities in the s_login database table. It defines constants for various login status types (successful login, wrong password, locked account, etc.) and provides methods to add login log entries and record user logouts. The class delegates actual functionality to methods in the Ict.Petra.Server.App.Core.Security.LoginLog namespace. Key methods include AddLoginLogEntry for recording login attempts and RecordUserLogout for tracking user disconnections. The class maintains a comprehensive audit trail of system access.

 **Code Landmarks**
- `Line 46-57`: Defines a comprehensive set of constants for different login status types, enabling detailed security monitoring
- `Line 73`: Delegates implementation to another namespace while maintaining the same API signature
- `Line 91`: Creates a new instance for logout recording despite using static method for login recording
### ServerLookups.cs

TSysManServerLookups implements server-side lookup functionality for the MSysMan module. It provides methods to retrieve database information for client applications. The class contains two main methods: GetDBVersion which retrieves the current database version from the s_system_defaults table, and GetInstalledPatches which returns a sorted list of all patches installed in the system. Both methods use database transactions to safely access data and require basic user permissions. The class handles error conditions by throwing exceptions when database information is missing or invalid.

 **Code Landmarks**
- `Line 49`: Uses RequireModulePermission attribute for security authorization
- `Line 58`: Uses delegate pattern with ReadTransaction for database access
- `Line 105`: Implements custom sorting of patch data before returning to client
### TableAccessPermissionManager.cs

TTableAccessPermissionManager implements a simple wrapper class that provides access to table permission functionality for users in the Petra database system. The class contains a single static method, LoadTableAccessPermissions, which delegates to the same method in the Ict.Petra.Server.App.Core.Security namespace. This method loads table access permissions for a specified user within a database transaction and returns them as a SUserTableAccessPermissionTable object. The class serves as an intermediary layer in the system's security architecture.

 **Code Landmarks**
- `Line 46`: The class acts as a facade pattern implementation, delegating all actual functionality to another namespace with identical method names.
### UserManagement.cs

TMaintenanceWebConnector implements user management functionality for OpenPetra, handling user authentication, password management, and permissions. It provides methods for creating users, setting and changing passwords, managing user permissions, and handling user account states (locked/retired). The class supports both database authentication and plugin-based authentication methods. Key functionality includes secure password hashing, token-based password reset, self-service user registration, and comprehensive user activity logging. Important methods include SetUserPassword, CreateUser, SaveUserAndModulePermissions, RequestNewPassword, and SignUpSelfService. The class also handles partner creation for self-service users and implements security measures against timing attacks and password hash exposure.

 **Code Landmarks**
- `Line 86`: Implements secure password management with quality checks and authentication method flexibility
- `Line 275`: Uses timing attack prevention through TPasswordHelper.EqualsAntiTimingAttack when comparing password hashes
- `Line 349`: Token-based password reset system with email notification
- `Line 639`: Self-service user registration with email verification and automatic partner record creation
- `Line 1012`: Password scheme versioning system allows for secure upgrades of hashing algorithms
### UserManager.cs

TUserManagerWebConnector implements server-side user authentication and management functionality for OpenPetra. It handles user login verification, password validation, account locking, and security permissions. The class supports multiple authentication methods including database-based authentication and plugin-based methods. Key functionality includes user authentication, password hashing, failed login tracking, account locking, and user information management. Important methods include PerformUserAuthentication, LoadUser, CreateHashOfPassword, and SimulatePasswordAuthenticationForNonExistingUser. The file also contains a TUserManager class that implements the IUserManager interface for core server functionality.

 **Code Landmarks**
- `Line 112`: Implements anti-timing attack protection by using constant-time comparison for password verification
- `Line 180`: Supports pluggable authentication through dynamic assembly loading
- `Line 270`: Implements security measures to prevent timing attacks when authenticating non-existent users
- `Line 394`: Implements progressive password scheme upgrading when users log in with older hash schemes
- `Line 535`: Uses client task queuing system to notify all connected clients when user information changes
### validation/SysMan.Validation.cs

TSysManValidation implements server-side validation for the SysMan module in OpenPetra, focusing on user account security. It provides methods to validate user details, particularly password requirements. The main functionality includes validating that passwords are not empty and meet quality standards through regex pattern matching. The class contains two key methods: ValidateSUserDetails which validates user data rows and reports errors, and CheckPasswordQuality which ensures passwords meet minimum security requirements (8+ characters, containing digits and letters). The validation results are collected in a TVerificationResultCollection for UI feedback.

 **Code Landmarks**
- `Line 77`: Password quality check uses regex pattern to enforce minimum security standards of 8+ characters with at least one digit and one letter
- `Line 54`: Validation skips deleted rows, showing defensive programming to prevent errors during data operations
- `Line 65`: Special handling for first-time passwords without salt, indicating a password lifecycle management approach
### web/Settings.cs

TSettingsWebConnector implements server-side functionality for system settings management in OpenPetra. It provides methods for retrieving and configuring available sites, managing site keys in the partner ledger, checking password change requirements, and handling initial system setup. Key functionality includes retrieving site data sorted by usage status, saving site configurations, checking if users need to change passwords, determining if the setup assistant is needed, and running the first-time system setup with user creation and site configuration. Important methods include GetAvailableSites(), SaveSiteKeys(), GetPasswordNeedsChange(), GetSetupAssistant(), GetDefaultsForFirstSetup(), and RunFirstSetup().

 **Code Landmarks**
- `Line 65`: Method sorts sites by whether they're already in partner ledger before returning them
- `Line 169`: Calculates last partner ID by finding maximum existing partner key in database
- `Line 193`: Refreshes cached site list after changes to ensure consistency
- `Line 304`: Creates a secure random password for initial user setup
- `Line 391`: Sets system defaults and creates initial site during first-time setup

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #