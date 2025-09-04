# C# Petra Server Core Of Petra

The Petra is a C# program that provides administrative management capabilities for non-profit organizations. The program handles data management operations and provides security controls for accessing sensitive information. This sub-project implements the core server functionality along with security and authorization mechanisms. This provides these capabilities to the Petra program:

- Database access security and permission management
- Server administration and management interfaces
- Scheduled processing and background job execution
- Comprehensive logging for errors, logins, and user activities
- System configuration and defaults management

## Identified Design Elements

1. **Layered Security Model**: Implements both table-level and module-level access controls to ensure proper authorization for all operations
2. **Centralized Error Handling**: Provides structured error logging with context information, timestamps, and user details
3. **Administrative Web Interface**: Exposes server management capabilities through a web connector for remote administration
4. **Progress Tracking System**: Implements session-based progress tracking for long-running operations
5. **Scheduled Job Processing**: Supports timed execution of maintenance tasks with manual override capabilities

## Overview
The architecture follows a modular design with clear separation between infrastructure, security, and application layers. The security model implements defense-in-depth with both coarse-grained module permissions and fine-grained table access controls. System state management includes both configuration defaults and progress tracking for long-running operations. The logging subsystem provides comprehensive audit trails for security events, errors, and user activities. The server management functionality supports database operations, user management, and system monitoring through a secure web interface.

## Business Functions

### Database and Security
- `DBAccess.cs` : Database access security layer for OpenPetra that enforces user permissions on database operations.
- `TableAccessPermissionManager.cs` : Manages database table access permissions for OpenPetra users.
- `ModuleAccessManager.cs` : Manages module access permissions for OpenPetra, controlling user access to system functionality through permission checks.

### Server Management
- `TimedProcessing.cs` : Server-side component for scheduling and executing timed processing jobs in OpenPetra.
- `ServerManager.cs` : Server manager class for OpenPetra that handles server startup, shutdown, and administration functions.
- `ServerAdminWebConnector.cs` : Server administration web connector providing methods for managing OpenPetra server operations through a web interface.
- `ProgressTracker.cs` : Manages progress tracking for long-running server operations in OpenPetra.

### Logging and Monitoring
- `ErrorLog.cs` : Manages error logging functionality for the OpenPetra server application.
- `LoginLog.cs` : Manages user login/logout tracking in OpenPetra by recording entries in the system's login log database table.
- `UserAccountActivityLog.cs` : Manages user account activity logging for OpenPetra's security system.

### System Configuration
- `LogonMessage.cs` : Manages logon messages for different language codes in OpenPetra
- `SystemDefaults.cs` : Manages system defaults for OpenPetra, providing methods to retrieve and store configuration values from the database.

## Files
### DBAccess.cs

TDataBasePetra extends TDataBase to provide database security for OpenPetra by checking user permissions before executing SQL queries. It validates if the current user has appropriate rights (create, modify, delete, inquire) for specific tables based on the s_user_table_access_permission table. The class maintains a SQL cache to store permissions, establishes database connections with user authentication, and logs unauthorized access attempts. Key methods include EstablishDBConnection, HasAccess, and LogInPetraErrorLog. It uses a delegate TDelegateAddErrorLogEntry for error logging and throws ESecurityDBTableAccessDeniedException when access is denied.

 **Code Landmarks**
- `Line 127`: Implements SQL permission checking by parsing SQL statements to extract table names and required permissions
- `Line 92`: Uses a SQL cache to store user permissions for performance optimization
- `Line 226`: Special handling for system tables like S_USER_DEFAULTS that always allow access
- `Line 247`: Throws custom security exception with detailed information when access is denied
### ErrorLog.cs

TErrorLog implements a class for recording errors in the OpenPetra system database. It provides functionality to add error log entries with error codes, context information, and message details. The class implements the IErrorLog interface and offers two overloaded AddErrorLogEntry methods - one that automatically retrieves user information and another that accepts explicit user ID and process ID parameters. Error entries include timestamps, application version, and up to three message lines. The class uses database transactions to ensure reliable storage of error information, with proper exception handling and rollback capabilities.

 **Code Landmarks**
- `Line 86`: Uses DateTime.Now for timestamping errors without timezone consideration
- `Line 115`: Database transaction uses Serializable isolation level for maximum consistency
- `Line 122`: Exception handling logs the error about failing to log an error
### LoginLog.cs

TLoginLog implements functionality for recording user authentication events in OpenPetra's s_login database table. It provides methods for adding login log entries and recording user logouts with timestamps. The class supports both creating new login records with AddLoginLogEntry() and recording user disconnections with RecordUserLogout(). These methods handle database transactions, error logging, and proper formatting of login data including user IDs, timestamps, and process IDs. The implementation includes constants for login status types and supports both internal transaction management and externally provided transactions.

 **Code Landmarks**
- `Line 57`: Method uses sequence-generated process IDs to uniquely identify login sessions
- `Line 102`: Database access uses parameterized queries to prevent SQL injection attacks
- `Line 131`: Flexible transaction handling allows method to work both with provided transactions or by creating its own
### LogonMessage.cs

TMaintenanceLogonMessage implements functionality to retrieve and manage logon messages for different language codes in the OpenPetra system. The class provides methods to fetch messages based on language code or user ID from the database. Key methods include GetLogonMessage() which retrieves messages by language code or user ID, and GetLogonMessageLanguage() which can optionally return English messages as fallback when a requested language isn't available. The class interacts with the SLogonMessageAccess data access layer to retrieve message data from the database and handles exceptions appropriately.

 **Code Landmarks**
- `Line 64`: Method overloading allows retrieving messages by UserID instead of language code
- `Line 80`: Implements fallback mechanism to English when requested language isn't available
- `Line 101`: Exception handling with detailed logging for database access failures
### ModuleAccessManager.cs

TModuleAccessManager implements security controls for OpenPetra by managing module access permissions. It provides functionality to load user modules, verify permissions for method calls, and check table access rights. The class includes methods like LoadUserModules to retrieve modules available to a user, CheckUserPermissionsForMethod to validate access to specific connector methods using custom attributes, and CheckUserPermissionsForTable to control data access permissions. The file also defines the RequireModulePermissionAttribute class for annotating server functions with required permissions and TTablePermissionEnum for specifying table access levels (read, create, modify, delete).

 **Code Landmarks**
- `Line 96`: Uses reflection to dynamically check method permissions based on custom attributes
- `Line 177`: Implements complex permission checking with logical expressions (OR/AND) for module access
- `Line 202`: Performs SQL-based permission verification against the database for table access control
- `Line 291`: Custom attribute system allows declarative security permissions on server methods
### ProgressTracker.cs

TProgressTracker implements server-side progress tracking for long-running operations. It manages progress state through session variables, allowing clients to monitor task completion. Key functionality includes initializing trackers with captions and overall amounts, retrieving current progress states, updating status messages and completion percentages, and handling job cancellation and completion. The class uses session variables to store progress information identified by client IDs. Important methods include InitProgressTracker(), GetCurrentState(), SetCurrentState(), CancelJob(), and FinishJob(). The TProgressState class stores progress information including percentage done, status messages, and cancellation flags.

 **Code Landmarks**
- `Line 54`: Uses session variables to track progress across multiple client requests
- `Line 73`: Clears all progress trackers before initializing a new one to prevent session variable overload
- `Line 89`: Converts JObject from session storage back to TProgressState object
- `Line 124`: Implements cancellation mechanism for long-running server operations
### ServerAdminWebConnector.cs

TServerAdminWebConnector implements a class that provides web-based administrative functionality for the OpenPetra server. It enables server management operations including user authentication, client connection tracking, server status monitoring, and database maintenance. Key functionality includes administrative login without password, client management (listing, disconnecting), server control (stopping, memory management), database operations (backup, restore, upgrade), and user management (adding users, setting passwords). The class uses permission attributes to secure methods, with most requiring the SYSMAN module permission. Important methods include LoginServerAdmin, GetClientList, StopServer, BackupDatabaseToYmlGZ, and SetPassword.

 **Code Landmarks**
- `Line 49`: Uses CheckServerAdminToken attribute for authentication validation without requiring password
- `Line 197`: Implements database backup functionality that returns compressed YAML data as a string
- `Line 208`: Sets client ID to -1 during database restore to avoid issues with progress tracking
- `Line 271`: Allows on-demand execution of scheduled tasks through PerformTimedProcessingNow method
### ServerManager.cs

TServerManager implements the core server management functionality for OpenPetra, handling server initialization, database operations, and administrative tasks. It provides interfaces for database upgrades, backups/restores, user management, and scheduled processing jobs. The class loads and initializes various server components through reflection, including import/export functionality, database upgrades, and user management. Key methods include UpgradeDatabase(), BackupDatabaseToYmlGZ(), RestoreDatabaseFromYmlGZ(), SetPassword(), and PerformTimedProcessingNow(). The class extends TServerManagerBase and can be remotely accessed by admin applications.

 **Code Landmarks**
- `Line 79`: Uses reflection to dynamically load and instantiate server components from separate assemblies
- `Line 172`: Implements database backup/restore functionality using YAML compressed format
- `Line 216`: Configurable timed processing system that can schedule and manually trigger server jobs
### SystemDefaults.cs

TSystemDefaults implements a manager class for OpenPetra's system-wide configuration settings. It retrieves values from the s_system_defaults database table and caches them in a typed DataTable. The class provides methods to check if defaults exist, retrieve values in various data types (string, boolean, numeric types), and set new defaults or update existing ones. Key functionality includes automatic type conversion, default value handling when settings don't exist, and special handling for the SiteKey default. Important methods include GetSystemDefault(), SetSystemDefault(), GetBooleanDefault(), GetInt32Default(), and GetSiteKeyDefault().

 **Code Landmarks**
- `Line 72`: The class implements a caching mechanism to reduce database queries by storing defaults in a typed DataTable.
- `Line 157`: Special handling for boolean values allows both 'no'/'false' and 'yes'/'true' string representations.
- `Line 312`: Special handling for SiteKey with fallback to legacy 'SiteKeyPetra2' for backward compatibility with Petra 2.3 databases.
- `Line 400`: Case-insensitive lookup of system defaults ensures consistent behavior regardless of key capitalization.
### TableAccessPermissionManager.cs

TTableAccessPermissionManager implements a class that handles user access permissions to database tables in the OpenPetra system. The primary functionality is loading table access permissions for a specific user from the database. The class contains a single static method LoadTableAccessPermissions that takes a user ID and database transaction as parameters, retrieves the user's table access permissions from the database if they exist, or returns an empty permissions table otherwise. This functionality is essential for implementing security controls within the application's data access layer.

 **Code Landmarks**
- `Line 47`: The method returns an empty permissions table if no permissions are found rather than null, avoiding null reference exceptions.
### TimedProcessing.cs

TTimedProcessing implements a static class that manages scheduled processing routines in OpenPetra. It provides functionality to register processing jobs, run them at timed intervals, and execute them manually. The class maintains a collection of delegate functions that perform database operations, handles timing calculations for daily execution, and supports both automatic and manual job execution. Key components include AddProcessingJob for registering new jobs, RunJobManually for immediate execution, StartProcessing for initializing the timing system, and GenericProcessor which handles database connections for job execution. Important variables include FProcessDelegates storing registered jobs and FDailyStartTime24Hrs for scheduling.

 **Code Landmarks**
- `Line 42`: Uses ThreadStatic attribute to ensure thread-local storage of processing delegates
- `Line 76`: Implements a generic processor pattern that handles database connections and delegate execution
- `Line 166`: Calculates precise timing for scheduled jobs using DateTime operations
### UserAccountActivityLog.cs

TUserAccountActivityLog implements a class for recording user account activities in OpenPetra's security system. It defines constants for various user activities (account creation, password changes, account locking/unlocking) and provides a method to add entries to the s_user_account_activity table. The AddUserAccountActivityLogEntry method records user activities with timestamps, activity types, and details, storing them in the database and logging them to the server log. The class serves as a centralized mechanism for tracking and auditing user account-related events within the system.

 **Code Landmarks**
- `Line 85`: Method logs activities both to database and server log for dual tracking
- `Line 31`: Comprehensive set of constants defines all trackable user account activities

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #