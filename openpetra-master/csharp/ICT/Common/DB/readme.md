# C# Database Subproject Of OpenPetra

The OpenPetra is a C# program that provides administrative management tools for non-profit organizations. The program handles financial transactions and manages organizational data across multiple modules. This sub-project implements the database abstraction layer along with transaction management capabilities. This provides these capabilities to the OpenPetra program:

- Database-agnostic data access with support for PostgreSQL and MySQL
- Transaction management with isolation levels and thread safety
- SQL query caching for improved performance
- Parameterized query execution with proper type handling
- Sequence management for database identifiers

## Identified Design Elements

1. **Database Abstraction Layer**: The IDataBaseRDBMS interface allows OpenPetra to work with different database systems through database-specific implementations (PostgreSQL.cs, MySQL.cs)
2. **Transaction Management**: TDBTransaction provides a wrapper for ADO.NET transactions with additional tracking and debugging information
3. **Connection Pooling**: TDBConnection handles connection establishment, pooling, and proper resource disposal
4. **Query Caching**: TSQLCache improves performance by storing and reusing query results
5. **Exception Handling**: Specialized database exceptions provide detailed error information and support for automatic retry mechanisms

## Overview
The architecture emphasizes database independence through a well-defined abstraction layer, ensuring OpenPetra can work with different database systems without code changes. The transaction management system provides robust isolation with proper thread safety controls. Performance optimization is achieved through connection pooling and query caching. The design includes comprehensive error handling with specialized exceptions and automatic retry capabilities for transient errors. This foundation supports OpenPetra's data-intensive features like contact management, accounting, and sponsorship tracking.

## Business Functions

### Database Connection Management
- `Connection.cs` : Manages database connections for OpenPetra, handling connection creation, closing, and connection pool management.
- `Access.cs` : Provides database connection and transaction management utilities for OpenPetra's database access layer.

### Transaction Management
- `Transaction.cs` : Manages database transactions in OpenPetra by wrapping ADO.NET transaction objects with tracking and error handling capabilities.
- `Database.cs` : Database access layer for OpenPetra providing connection management, SQL execution, and transaction handling
- `ServerBusyHelper.cs` : Helper class for handling server busy states during coordinated database access with automatic retry functionality.

### Database Abstraction
- `DatabaseInterface.cs` : Defines the database interface requirements for OpenPetra's RDBMS compatibility layer.
- `PostgreSQL.cs` : PostgreSQL database adapter for OpenPetra that implements connection handling and query formatting for PostgreSQL databases.
- `MySQL.cs` : MySQL database connector implementation for OpenPetra that handles connection management and query formatting for MySQL databases.
- `Exceptions.DB.cs` : Defines database-related exception classes for OpenPetra's database abstraction layer.
- `Utilities.cs` : Database utility classes for OpenPetra providing data adapter cancellation and SQL parameter list handling.

### Query Caching
- `Cache.cs` : Implements a SQL query caching system to improve database performance by storing and reusing query results.

## Files
### Access.cs

DBAccess implements utilities for database connection management and transaction handling in OpenPetra. It defines debug level constants for logging different aspects of database operations and provides methods to create and manage database connections. Key functionality includes establishing connections with the Connect method, and simplified transaction management through ReadTransaction and WriteTransaction methods that can work with anonymous database connections. The class also provides a property to determine the configured RDBMS type from application settings.

 **Code Landmarks**
- `Line 83`: Connect method allows reusing existing connections through optional parameter, improving connection management efficiency
- `Line 99`: WriteTransaction method supports both new and existing transactions, automatically handling connection lifecycle when needed
- `Line 113`: ReadTransaction method implements similar pattern to WriteTransaction for read operations with automatic connection management
### Cache.cs

TSQLCache implements a class for caching SQL query results to improve performance by avoiding redundant database calls. The cache stores SQL queries as strings along with their parameters and corresponding datasets. Key functionality includes retrieving cached datasets or executing new queries when needed, comparing SQL statements and parameters to identify matches, and invalidating cache entries when tables are modified. The class provides methods for getting datasets (GetDataSet), data tables (GetDataTable), and string lists (GetStringList) from cached results, with support for ODBC parameters. Important variables include storedDataSet, storedSQLQuery, and storedParameters ArrayLists that maintain the cache state. The class includes a note that it is not thread-safe and requires locks for proper concurrent operation.

 **Code Landmarks**
- `Line 32`: Class is marked as not thread-safe, needing locks for ArrayList access
- `Line 82`: Complex parameter comparison logic to ensure exact parameter matching before returning cached results
- `Line 158`: Returns copies of cached datasets to prevent modifications affecting the cache
- `Line 240`: Table-specific cache invalidation to maintain data consistency
### Connection.cs

TDBConnection implements low-level database connection management functionality for OpenPetra. It provides methods for establishing database connections with various parameters, closing connections safely, and managing connection pools. The class is designed to be used internally by the TDataBase class rather than directly by developers. Key functionality includes GetConnection for creating database connections with specific parameters, CloseDBConnection for safely closing connections, and methods for clearing connection pools. The file also defines EDBConnectionAlreadyClosedException, which is thrown when attempting to close an already closed connection.

 **Code Landmarks**
- `Line 47`: TDBConnection is marked internal, indicating it's meant to be used only through the TDataBase class for database access abstraction
- `Line 98`: Connection closing includes explicit disposal to prevent resource leaks and detailed logging for connection tracking
- `Line 131`: Connection pool clearing methods are provided but with explicit warnings about performance impacts
### Database.cs

TDataBase implements a database abstraction layer for OpenPetra, handling connections to PostgreSQL and MySQL databases. It provides methods for executing SQL queries, managing transactions with different isolation levels, and retrieving data through various methods like Select, SelectDT, and ExecuteScalar. The class ensures thread safety through coordinated database access mechanisms, handles connection state changes, and supports parameterized queries. Key functionality includes transaction management with automatic retry capabilities, sequence value management, and helper methods for SQL statement formatting and logging. Important methods include EstablishDBConnection, CloseDBConnection, BeginTransaction, Command, Select variants, and ExecuteNonQuery.

 **Code Landmarks**
- `Line 67`: Uses SemaphoreSlim to ensure thread-safe database access across multiple client/server threads
- `Line 1075`: Implements transaction isolation level compatibility checking to prevent nested transaction issues
- `Line 1386`: Auto-reconnection logic attempts to recover from broken database connections
- `Line 2001`: Helper methods ReadTransaction and WriteTransaction encapsulate transaction management with proper isolation levels
- `Line 1006`: Supports parameterized queries with special handling for IN clause parameters
### DatabaseInterface.cs

IDataBaseRDBMS interface defines the contract that any database system must implement to work with OpenPetra. It provides essential database operations including connection management, query formatting, parameter conversion, and transaction handling. Key functionality includes creating database connections, formatting SQL queries for specific RDBMS systems, handling exceptions, creating commands and adapters, and managing sequences. The interface abstracts database-specific implementations, allowing OpenPetra to work with different database systems through a common API while handling database-specific formatting and behavior.

 **Code Landmarks**
- `Line 83`: FormatQueryRDBMSSpecific method enables database-agnostic SQL queries by handling RDBMS-specific formatting at runtime
- `Line 92`: ConvertOdbcParameters transforms ODBC parameters to database-specific parameters, enabling cross-database compatibility
- `Line 167`: Sequence management methods provide abstraction for different sequence implementations across database systems
### Exceptions.DB.cs

This file implements a comprehensive set of exception classes for OpenPetra's database abstraction layer. It defines specialized exceptions for various database connection and transaction scenarios, including connection failures, transaction coordination issues, and thread safety violations. Key exceptions include EDBConnectionNotEstablishedException, EDBTransactionBusyException, and EDBTransactionSerialisationException. Each exception class follows a consistent pattern with multiple constructors and proper serialization support for remoting. The file also includes the TDBExceptionHelper static class with methods to identify specific PostgreSQL error conditions like serializable transaction collisions. These exceptions help maintain database integrity by enforcing proper transaction isolation, preventing cross-thread access, and handling connection failures gracefully.

 **Code Landmarks**
- `Line 757`: EDBTransactionBusyException implements custom ToString() to include nested transaction details for better debugging
- `Line 1235`: TDBExceptionHelper.IsFirstChanceNpgsql40001Exception detects PostgreSQL serialization failures for transaction retry logic
- `Line 1252`: Special handling for primary key constraint violations that may indicate transaction collisions
### MySQL.cs

TMySQL implements the IDataBaseRDBMS interface to provide MySQL database connectivity for OpenPetra. It manages database connections, parameter handling, query formatting, and transaction management specific to MySQL. The class handles conversion between ODBC parameters and MySQL parameters, formats SQL queries to be MySQL-compatible, and provides sequence management functionality. Key methods include GetConnection for establishing database connections, FormatQueryRDBMSSpecific for MySQL-specific query formatting, ConvertOdbcParameters for parameter conversion, and NewCommand for creating database commands. The class also implements sequence management through GetNextSequenceValue and GetCurrentSequenceValue methods.

 **Code Landmarks**
- `Line 133`: Formats SQL queries specifically for MySQL by replacing schema prefixes, handling date formats, and converting boolean values
- `Line 173`: Converts ODBC parameters to MySQL parameters with proper naming and type conversion
- `Line 385`: Implements sequence management using MySQL tables rather than native sequence objects
- `Line 442`: Custom implementation to convert DAYOFYEAR function to MySQL's DATE_FORMAT equivalent
### PostgreSQL.cs

TPostgreSQL implements the IDataBaseRDBMS interface to provide PostgreSQL database access for OpenPetra using the Npgsql .NET data provider. It handles connection management, query formatting, parameter conversion, and database-specific operations. Key functionality includes creating database connections with proper connection strings, converting ODBC parameters to PostgreSQL format, formatting SQL queries to be PostgreSQL-compatible, and managing database sequences. The class transforms SQL syntax for PostgreSQL compatibility, including replacing LIKE with ILIKE for case-insensitive searches, converting DAYOFYEAR functions, and handling parameter placeholders. Important methods include GetConnection, FormatQueryRDBMSSpecific, ConvertOdbcParameters, NewCommand, and sequence management methods like GetNextSequenceValue.

 **Code Landmarks**
- `Line 188`: Converts SQL queries to PostgreSQL format, including case-insensitive LIKE operations using a complex parsing algorithm
- `Line 293`: Converts ODBC parameters to PostgreSQL parameters with proper naming and type conversion
- `Line 461`: Warns that sequence values persist even if transactions are rolled back, which is important for data integrity
- `Line 525`: Custom function to replace DAYOFYEAR() with PostgreSQL's to_char() function for date handling
### ServerBusyHelper.cs

TServerBusyHelper implements a static class that provides functionality for handling 'server busy' states that can occur due to multi-threading database access issues. It primarily implements automatic retry handling for coordinated database access through the CoordinatedAutoRetryCall method, which executes encapsulated code with configurable retry attempts when database transactions are busy or used by other threads. The class defines resource strings for user messages and handles various database exceptions including EDBTransactionBusyException and EDBAttemptingToWorkWithTransactionThatGotStartedOnDifferentThreadException. It supports configurable retry counts via application settings and optional exception throwing when retries are exhausted.

 **Code Landmarks**
- `Line 47`: Uses C# delegates (Action) to encapsulate code blocks for retry handling
- `Line 74`: Configurable retry count pulled from application settings
- `Line 101`: Thread.Sleep implementation to handle thread contention
- `Line 33`: Class designed for inheritance - GUI components extend this functionality in another class
### Transaction.cs

TDBTransaction implements a wrapper class for ADO.NET database transactions that provides transaction management functionality for OpenPetra. It encapsulates database transactions with additional tracking information such as transaction identifiers, names, thread information, and stack traces to aid debugging. Key functionality includes beginning transactions, committing changes, rolling back transactions, and proper resource disposal. The class implements IDisposable and maintains transaction state information for validation. Important methods include BeginTransaction(), Commit(), Rollback(), and Dispose(), while key properties include TransactionIdentifier, DataBaseObj, Valid, and WrappedTransaction.

 **Code Landmarks**
- `Line 47`: Wraps native database transactions with additional metadata for tracking and debugging
- `Line 124`: Maintains thread and AppDomain context information to ensure transactions are properly managed across execution contexts
- `Line 213`: Implements comprehensive transaction commit with error handling and logging
- `Line 266`: Implements rollback with similar safeguards as commit to handle server-side failures
- `Line 317`: Implements IDisposable pattern to ensure proper resource cleanup
### Utilities.cs

This file implements database utility classes for OpenPetra's data access layer. TDataAdapterCanceller provides a thread-safe mechanism to cancel DbDataAdapter Fill operations, requiring execution on a separate thread for proper functionality. TDbListParameterValue implements support for SQL IN clause parameters by expanding a collection of values into individual parameters, handling empty collections gracefully by using NULL. The class implements IEnumerable<OdbcParameter> to generate parameter collections and includes a convenience method OdbcListParameterValue() for creating parameters with list values. These utilities enhance database operations with cancellation support and simplified parameter handling for IN clauses.

 **Code Landmarks**
- `Line 52`: CancelFillOperation must be called on a separate thread due to ADO.NET implementation details
- `Line 93`: Handles empty collections by using NULL in the IN clause since 'column IN ()' is invalid SQL syntax
- `Line 127`: Uses C# yield return for lazy enumeration of dynamically created parameters

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #