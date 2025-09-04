# C# Data Subproject Of OpenPetra

The OpenPetra is a C# program that provides administrative management for non-profit organizations. The program handles financial transactions and manages organizational data across multiple domains. This sub-project implements the core data access layer and typed dataset functionality along with exception handling for database operations. This provides these capabilities to the OpenPetra program:

- Strongly-typed data access with automatic SQL generation
- Cross-platform compatible database abstraction
- Serializable exception handling for client-server communication
- Advanced data manipulation utilities with change tracking

## Identified Design Elements

1. **Typed Data Architecture**: Implements strongly-typed representations of database tables with metadata, constraints, and relationships through TTypedDataSet and TTypedDataTable classes
2. **Database Operation Abstraction**: Automatically generates SQL statements (SELECT, INSERT, UPDATE, DELETE) based on typed data structures
3. **Remoting-Compatible Exception Handling**: Custom exception classes designed specifically for serialization across .NET Remoting boundaries
4. **Concurrency Management**: Built-in mechanisms for detecting and handling concurrent data modifications with detailed conflict information
5. **Cross-Platform Compatibility**: Specific accommodations for differences between Mono and Microsoft .NET implementations

## Overview
The architecture emphasizes type safety through the use of generated typed datasets, provides a comprehensive database abstraction layer that handles SQL generation, and implements robust error handling for distributed operations. The data utilities offer extensive functionality for data manipulation, comparison, and conversion. This subproject forms the foundation of OpenPetra's data access strategy, enabling higher-level components to interact with the database through a consistent, strongly-typed interface while maintaining compatibility across different .NET implementations.

## Business Functions

### Data Access and Management
- `Exceptions.Remoted.cs` : Defines custom exception classes for OpenPetra's data access layer that can be passed between server and client via .NET Remoting.
- `TypedDataSet.cs` : Provides base classes for typed datasets in OpenPetra, handling constraints, relations, and cross-platform compatibility between Mono and .NET.
- `Utilities.cs` : Utility class providing data manipulation functions for ADO.NET operations in OpenPetra.
- `TypedDataTable.cs` : Base class for typed data tables with serialization support and database integration for OpenPetra.
- `TypedDataAccess.cs` : Provides data access functionality for typed database operations in OpenPetra

## Files
### Exceptions.Remoted.cs

This file implements exception classes for OpenPetra's data access layer that can be serialized and passed between server and client via .NET Remoting. It defines EOPDBTypedDataAccessException as a base class for data access exceptions, EDBConcurrencyException for handling concurrent data modifications, EDBConcurrencyNoRowToUpdateException for when records to be updated don't exist, and EDBSubmitException for database operation failures. Each exception class implements proper serialization support for remoting with GetObjectData methods and appropriate constructors. The exceptions track important metadata like database operations, table names, and user information related to data conflicts.

 **Code Landmarks**
- `Line 44`: Base exception class that inherits from EOPDBException and implements serialization for remote exception handling
- `Line 120`: EDBConcurrencyException tracks detailed metadata about database conflicts including operation type, table name, and modification history
- `Line 193`: GetObjectData implementation ensures proper serialization of custom exception properties for remoting
- `Line 218`: Specialized exception for the specific case when a record to be updated no longer exists
### TypedDataAccess.cs

TTypedDataAccess implements a base class for database access operations in OpenPetra. It provides extensive functionality for generating SQL statements and handling database operations with typed data. Key features include generating SQL clauses (SELECT, INSERT, UPDATE, DELETE), parameter handling, data modification tracking, and concurrency management. The class supports operations like loading data by primary/unique keys, submitting changes, and handling foreign key relationships. Important methods include GenerateSelectClause, GenerateWhereClause, InsertRow, UpdateRow, DeleteRow, and SubmitChanges. It also includes the TRowReferenceInfo class for tracking database row references during cascading operations.

 **Code Landmarks**
- `Line 73`: Defines system columns used for tracking changes, modification history, and soft deletion
- `Line 1046`: SubmitChanges method handles batch operations with parameter limits to avoid exceeding database constraints
- `Line 1352`: BuildVerificationResultCollectionFromRefTables creates user-friendly error messages for cascading reference constraints
- `Line 1438`: TRowReferenceInfo class tracks database row references in a serializable format for cascading operations
- `Line 1603`: PopulatePKInfoDataFromDataRow method enables serialization of primary key data across remoting boundaries
### TypedDataSet.cs

TypedDataSet.cs implements core classes for OpenPetra's data handling system. The file defines TypedDataSet (base utility class), TTypedConstraint (foreign key constraints), TTypedRelation (table relationships), and TTypedDataSet (main dataset class). TTypedDataSet extends DataSet with functionality to manage constraints and relations between tables, handle serialization, and provide methods for enabling/disabling constraints and relations. Key methods include EnableConstraints(), DisableConstraints(), EnableRelations(), RemoveTable(), and GetChangesTyped(). The code specifically addresses compatibility issues between Mono and Microsoft .NET implementations.

 **Code Landmarks**
- `Line 54`: RemoveConstraintsFromSchema uses regex to strip constraints from XML schemas to ensure compatibility between Mono and .NET
- `Line 170`: ThrowAwayAfterSubmitChanges property significantly improves performance by avoiding modification tracking when datasets can be discarded after submission
- `Line 367`: EnableRelation method creates parent-child relationships between tables with optional constraint enforcement
- `Line 466`: GetChangesTyped method returns a new dataset containing only modified data, with options to remove empty tables
### TypedDataTable.cs

TTypedDataTable implements an abstract base class for typed data tables in OpenPetra. It provides core functionality for database table representation with proper typing, serialization support, and database integration. The class manages table metadata including column definitions, primary/unique keys, and SQL mappings. Key features include table initialization, change tracking, custom reporting permissions, and ODBC parameter creation. Important nested classes include TTypedColumnInfo and TTypedTableInfo which store metadata about columns and tables. Static methods provide access to table and column information, while abstract methods require implementation by derived classes.

 **Code Landmarks**
- `Line 118`: Custom implementation of GetChanges that ensures proper typing is maintained when retrieving modified rows
- `Line 186`: Static collection stores metadata about all typed tables in the system for global access
- `Line 190`: ResetStaticVariables method supports web request isolation but notes limitations with static initializers
- `Line 193`: Comprehensive metadata system with TTypedColumnInfo and TTypedTableInfo classes for database schema representation
- `Line 367`: Static utility methods provide type-safe access to table metadata across the application
### Utilities.cs

DataUtilities provides essential functions for manipulating ADO.NET data objects in OpenPetra. The class implements methods for comparing and navigating between DataTable and DataView objects, calculating hash values, copying and comparing data rows, and handling data type conversions. Key functionality includes finding indices between related data objects, calculating hash values for data tables, comparing row values, copying modification IDs between data objects, and handling data type changes. Important methods include GetDataViewIndexByDataTableIndex, CalculateHashAndSize, HaveDataRowsIdenticalValues, CopyModificationIDsOver, and ChangeDataColumnDataType. The file also includes the DataSetAdapter generic class for converting between regular and strongly-typed DataTables.

 **Code Landmarks**
- `Line 92`: Method uses primary key comparison to find matching rows between DataView and DataTable
- `Line 254`: SHA1 hashing used for speed rather than security, with explicit warning not to use for passwords
- `Line 371`: IsReallyChanged method detects if a row marked as modified actually has different values
- `Line 512`: CopyAllColumnValues only copies when values differ to preserve destination row state
- `Line 715`: ChangeDataColumnDataType allows changing column types in tables that already contain data

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #