# C# Remoting Shared Of OpenPetra

OpenPetra is a C# program that provides administrative management capabilities for non-profit organizations. The program facilitates contact management, accounting, and sponsorship while enabling data export and international clearing house functionality. This sub-project implements the core remoting infrastructure that enables distributed communication between clients and servers within the OpenPetra architecture.  This provides these capabilities to the OpenPetra program:

- Binary and JSON serialization for cross-platform compatibility
- Interface definitions for remote procedure calls
- Server administration functionality through standardized interfaces
- Security validation to prevent injection attacks
- Caching mechanisms for performance optimization

## Identified Design Elements

1. **Standardized Remote Interface Architecture**: The IInterface base interface provides a foundation for all remoting interfaces, enabling consistent type casting across the distributed system
2. **Flexible Serialization System**: THttpBinarySerializer handles conversion between .NET objects and various formats (JSON, binary) with special handling for complex data structures
3. **Format-Aware Communication**: The system detects client types (JavaScript vs. fat clients) and adapts serialization formats accordingly
4. **Security-First Design**: Built-in validation prevents HTML injection attacks during serialization/deserialization processes
5. **Comprehensive Server Management**: The ServerAdminInterface provides robust capabilities for monitoring and controlling server operations remotely

## Overview
The architecture establishes a communication foundation between client and server components, with clear separation between interface definitions and implementation. The remoting layer handles data transformation, security validation, and protocol management, while providing administrators with tools to monitor and maintain the server environment. This sub-project serves as the critical integration layer that enables OpenPetra's distributed architecture to function seamlessly across network boundaries.

## Business Functions

### System Infrastructure
- `ConstantsAndTypes.cs` : Defines core interfaces and constants for OpenPetra's remoting functionality.

### Data Serialization
- `Serialization.cs` : Serialization utility for converting objects between JSON and binary formats for OpenPetra client-server communication.

### Server Administration
- `ServerAdminInterface.cs` : Defines the server administration interface for OpenPetra, enabling remote management of the server and connected clients.

## Files
### ConstantsAndTypes.cs

ConstantsAndTypes.cs defines fundamental components for OpenPetra's remoting system. It establishes the IInterface base interface that all remoting interfaces can derive from, enabling type casting in .NET remoting scenarios. The file includes the RemotingConstants class with constants for client-server communication, such as parameter separators and URL identifiers. It also defines ResourceTexts for localized messages and the ICacheableTablesManager interface. These elements provide the foundation for remote procedure calls and client-task management in the distributed application architecture.

 **Code Landmarks**
- `Line 46`: IInterface serves as a base interface to enable type casting in .NET Remoting scenarios, allowing objects to be passed as function arguments
- `Line 58`: RemotingConstants defines communication parameters used in client-server interactions, particularly for task management
### Serialization.cs

THttpBinarySerializer provides methods for serializing and deserializing objects between different formats for OpenPetra's remoting functionality. It handles conversion between .NET objects and JSON strings, with special handling for complex types like DataSets, DataTables, and verification results. The class detects whether requests come from JavaScript or fat clients and formats data accordingly. Key methods include SerializeObject, SerializeObjectJSON, DeserializeObject, DeserializeDataTable, and DeserializeDataSet. The class also implements security validation to prevent HTML injection attacks and provides Base64 encoding/decoding functionality.

 **Code Landmarks**
- `Line 54`: Detects client type (JavaScript vs fat client) to determine appropriate serialization format
- `Line 67`: Custom JSON serialization for DataSets with special handling for DateTime values
- `Line 245`: Security validation to prevent HTML injection attacks in deserialized strings
- `Line 321`: Dynamic type resolution for enum deserialization across loaded assemblies
- `Line 334`: Binary deserialization using BinaryFormatter for complex types
### ServerAdminInterface.cs

IServerAdminInterface defines a remote interface for server administration tasks in OpenPetra. It provides functionality to monitor server status, manage client connections, perform server operations, and handle database maintenance. The interface exposes properties for retrieving server information (version, memory usage, connected clients) and methods for controlling the server (shutdown, client disconnection). It also includes database management capabilities like upgrading, backup/restore operations, and user administration functions. Key methods include StopServer, DisconnectClient, UpgradeDatabase, BackupDatabaseToYmlGZ, and AddUser.

 **Code Landmarks**
- `Line 43`: Interface designed specifically for admin applications, explicitly noting that regular clients should not use this interface
- `Line 101`: Implements two different server shutdown methods - controlled (graceful) and immediate (forced)
- `Line 173`: Database backup/restore functionality using yml.gz format for data portability
- `Line 195`: Connection pool management for database performance optimization

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #