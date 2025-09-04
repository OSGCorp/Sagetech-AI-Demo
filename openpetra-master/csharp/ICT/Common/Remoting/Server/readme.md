## C# Remoting Server of OpenPetra

The C# Remoting Server is a critical infrastructure component that enables distributed communication within the OpenPetra application. This server-side implementation manages client connections, authentication, session state, and bidirectional communication between clients and the server. The remoting architecture follows a service-oriented approach with clearly defined interfaces for core functionality.

The subproject provides these capabilities to the OpenPetra program:

- Client authentication and session management
- Server-to-client task distribution and polling mechanism
- Exception handling and comprehensive logging
- Database operations including backup, restoration, and upgrades
- State management across distributed components

## Identified Design Elements

1. **Session Management Architecture**: The DomainManager and ConnectedClient classes work together to maintain session state, track client connections, and manage session variables.
2. **Asynchronous Client Communication**: The ClientTasksManager implements a queue-based system for server-initiated communication to clients.
3. **Interface-Driven Design**: Core functionality is defined through interfaces (IUserManager, IImportExportManager, etc.) promoting loose coupling.
4. **Centralized Exception Handling**: The ExceptionHandling class provides comprehensive error logging and first-chance exception handling.

## Overview
The architecture emphasizes security through robust authentication mechanisms, scalability through efficient client connection tracking, and reliability through comprehensive error handling. The server manages client lifecycles from connection through authentication to disconnection, while providing administrative capabilities for monitoring and control. The polling mechanism enables efficient bidirectional communication while minimizing network overhead by avoiding empty data transfers.

## Business Functions

### Server Authentication and User Management
- `Interfaces.cs` : Defines server-side interfaces for user authentication, database operations, and system logging in OpenPetra.
- `ClientManager.cs` : Manages client connections, authentication, and session management for the OpenPetra server.

### Session and State Management
- `DomainManager.cs` : Manages session variables for OpenPetra server-side domain operations.
- `ServerManagerBase.cs` : Base class for OpenPetra server management providing core administration functionality and remote client management capabilities.
- `ConnectedClient.cs` : Manages connected client sessions on the server side of OpenPetra's remoting system.

### Client-Server Communication
- `PollClientTasks.cs` : Manages client tasks polling in OpenPetra's remoting system for server-client communication.
- `ClientTasksManager.cs` : Manages server-to-client messaging by queuing tasks until client retrieval via KeepAlive calls.

### Error Handling
- `ExceptionHandling.cs` : Server-side exception handling for OpenPetra with methods to log exceptions and handle unhandled errors.

## Files
### ClientManager.cs

TClientManager implements server-side functionality for handling client connections in OpenPetra. It provides methods for authenticating users, establishing client sessions, and managing client disconnections. Key functionality includes user authentication through password verification, session tracking, client task queuing, and error logging. The class interacts with database tables to verify user credentials, check system status, and retrieve site configuration. Important methods include ConnectClient, PerformLoginChecks, DisconnectClient, and AddErrorLogEntry. The class also provides utilities for listing connected clients and formatting client information for display.

 **Code Landmarks**
- `Line 92`: ThreadStatic variables ensure each web request thread has its own instance of critical services like UserManager and ErrorLog
- `Line 473`: Email-based authentication that converts email addresses to user IDs for login compatibility
- `Line 514`: Version compatibility check between client and server prevents mismatched connections
- `Line 700`: Exception mapping system converts various exception types to standardized login error codes
### ClientTasksManager.cs

TClientTasksManager implements a system for server-to-client communication in OpenPetra. It maintains two DataTables: one for new tasks waiting to be sent to clients and another for archiving tasks that have been fetched. The class provides functionality to add tasks with various parameters, check if tasks are available, retrieve tasks for client processing, and track task status. When clients make KeepAlive calls, pending tasks are returned and automatically moved to the history table with status changed from 'New' to 'Fetched'. Important methods include ClientTaskAdd, ClientTaskStatus, Get_ClientTasksNewDataTable, and Get_ClientTasksNewDataTableEmpty.

 **Code Landmarks**
- `Line 43`: Class implements server-to-client messaging with task queuing and history tracking, though marked as not thread-safe yet
- `Line 90`: Constructor creates two DataTables with identical schema - one for new tasks and one for task history
- `Line 177`: Debug code detects when tasks are added unusually close together, which could indicate potential issues
- `Line 254`: Method that moves tasks from new to history table when client retrieves them, with a TODO note about making it thread-safe
### ConnectedClient.cs

TConnectedClient implements a class that tracks and manages client connections to the OpenPetra server. It stores client information including ID, user ID, computer name, IP address, connection type, and timestamps for connection events. The class maintains session state through the TSessionStatus enum and provides methods for starting sessions, updating access times, and ending sessions with proper resource cleanup. Important properties include ClientID, UserID, ClientName, LastActionTime, and SessionStatus. The class also manages client tasks through FTasksManager and FPollClientTasks objects, and handles the lifecycle of client connections from initialization through disconnection.

 **Code Landmarks**
- `Line 173`: StartSession method initializes task managers and updates session status
- `Line 188`: EndSession handles proper resource cleanup including HttpSession objects
- `Line 182`: UpdateLastAccessTime tracks client activity for session management purposes
- `Line 246`: TSessionStatus enum defines the complete lifecycle states of a client connection
### DomainManager.cs

DomainManager implements a server-side class that manages access to critical session variables in OpenPetra. It provides properties to get and set key session data including GSiteKey (the organization's site identifier), GClientID (the current session's client identifier), and CurrentClient (the connected client object). The class uses TSession for storing and retrieving variables and includes validation to throw EOPDBInvalidSessionException when required session data is missing. It also handles JSON serialization/deserialization of the TConnectedClient object using Newtonsoft.Json.

 **Code Landmarks**
- `Line 47`: GSiteKey property includes important note that it can be changed by privileged users during an active session
- `Line 96`: CurrentClient property uses JSON serialization/deserialization to store complex TConnectedClient objects in the session
### ExceptionHandling.cs

TExceptionHandling implements server-side exception handling functionality for OpenPetra. It provides methods for logging exceptions to server log files and handling unhandled exceptions that occur during application execution. Key functionality includes logging exceptions with stack traces, handling unhandled exceptions through a special handler that forces an orderly cooperative shutdown from another thread, and implementing a first-chance exception handler that logs exceptions before the CLR searches for event handlers. Important methods include LogException(), UnhandledExceptionHandler(), and FirstChanceHandler().

 **Code Landmarks**
- `Line 69`: Implements fallback logging when standard logging isn't initialized
- `Line 85`: Uses a separate helper thread for 'cooperative async shutdown' to overcome CLR limitations with finalizers during unhandled exceptions
- `Line 123`: Special handling for OutOfMemoryException with different logging behavior than other exceptions
### Interfaces.cs

This file defines several interfaces for the server-side components of OpenPetra's remoting system. It includes IUserManager for user authentication and password management, IImportExportManager for database backup and restoration, IDBUpgrades for database updates, IErrorLog for error logging, ILoginLog for tracking user sessions, and IMaintenanceLogonMessage for retrieving welcome messages. These interfaces establish contracts for critical server operations including user authentication, password management, database maintenance, and system logging, forming the foundation for server-side functionality in the remoting architecture.

 **Code Landmarks**
- `Line 63`: Implements security measure against timing attacks by simulating authentication for non-existent users
- `Line 47`: Password parameter has empty string default value, allowing user creation without immediate password setting
- `Line 121`: Login logging interface supports transaction parameter to handle both internal and external transaction management
### PollClientTasks.cs

TPollClientTasks implements a server-side class that manages the polling mechanism for client tasks in OpenPetra's remoting system. It maintains a reference to a ClientTasksManager and tracks the last polling time. The class provides functionality to retrieve pending client tasks as a DataTable for the connected client. The main method PollClientTasks() returns either a DataTable containing tasks or null if no tasks exist, optimizing network traffic by avoiding empty table transfers. The class includes logging capabilities for debugging unusual numbers of client tasks.

 **Code Landmarks**
- `Line 76`: Optimizes network traffic by returning null instead of empty DataTable to reduce transferred bytes
- `Line 91`: Implements detailed conditional logging for unusual numbers of client task entries
- `Line 47`: Tracks last polling time to maintain client-server communication state
### ServerManagerBase.cs

TServerManagerBase implements a base class for OpenPetra server administration, providing core functionality for server management and client interaction. It implements the IServerAdminInterface with methods for monitoring connected clients, server control, and administration tasks. Key functionality includes server startup/shutdown management, client connection tracking, task queuing, security token validation, and database operations. Important properties include ClientsConnected, ServerInfoVersion, and SiteKey, while key methods are StopServer(), DisconnectClient(), QueueClientTask(), and CheckServerAdminToken(). The class also provides virtual methods for database operations that derived classes can implement.

 **Code Landmarks**
- `Line 73`: ThreadStatic attribute ensures each thread has its own instance of static variables, important for web server environments
- `Line 197`: Security token validation mechanism provides admin access control through file-based tokens
- `Line 218`: Custom exception handling for unhandled exceptions ensures proper shutdown of the server
- `Line 235`: Controlled server shutdown implementation with client notification
- `Line 254`: Thread-based delayed shutdown ensures clean termination of server process

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #