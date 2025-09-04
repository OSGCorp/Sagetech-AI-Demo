# C# Generate Glue Subproject Of OpenPetra

The OpenPetra system is a C# application that provides administrative management capabilities for non-profit organizations. The system bridges client and server components through automatically generated communication interfaces. This sub-project implements code generation tooling that creates the necessary "glue" code to facilitate client-server communication and API exposure. This provides these capabilities to the OpenPetra program:

- Automated generation of interface files from YAML configurations
- Identification and exposure of connector classes through the OpenPetra API
- Generation of server-side code for client-server communication
- Creation of WebConnector and UIConnector classes with proper security validation
- Support for both core OpenPetra modules and plugins

## Identified Design Elements

1. **Namespace Hierarchy Management**: The system parses and maintains namespace trees to organize code generation across modules
2. **Connector Discovery**: Automatically identifies classes in namespaces ending with 'Connectors' to expose through the API
3. **Security Integration**: Generated code includes permission checks and security validation for all exposed methods
4. **Plugin Support**: The architecture distinguishes between core modules and plugins, generating appropriate interfaces for each
5. **Parameter Handling**: Implements automatic type conversion and binary serialization for complex parameter types

## Overview
The architecture emphasizes separation between client and server components while providing seamless communication through generated interfaces. The code generation process starts with parsing C# source files, identifying connector classes, and then generating appropriate server-side glue code based on templates. The system handles both standard modules and plugins differently, with special namespace handling for plugins. The generated code includes proper security checks, parameter handling, and type conversions to ensure robust client-server communication.

## Business Functions

### Code Generation
- `CreateInterfaces.cs` : Generates interface files for OpenPetra's shared code components.
- `CollectConnectors.cs` : Collects connector classes from server code to publish in the client API for OpenPetra's remote method invocation system.
- `CreateServerGlue.cs` : Generates server-side code for OpenPetra's client-server communication layer.
- `NamespaceItem.cs` : Defines TNamespace class for parsing and managing namespace hierarchies in OpenPetra's code generation tools.
- `Program.cs` : Build tool that generates client-server glue code for OpenPetra by parsing C# files and creating web service connectors.

## Files
### CollectConnectors.cs

TCollectConnectorInterfaces implements functionality to parse C# code and identify connector classes that should be exposed in the OpenPetra API. It identifies connectors by their namespace ending with 'Connectors' and collects their method definitions. The class provides methods to find connectors in specific namespaces and filter methods based on visibility attributes. Key methods include GetConnectors() which parses C# files to extract connector classes, FindTypesInNamespace() to locate connectors in specific namespaces, and IgnoreMethod() to determine if methods should be excluded from remoting.

 **Code Landmarks**
- `Line 51`: Identifies connector classes by checking if namespace ends with 'Connectors' string pattern
- `Line 118`: Uses NoRemoting attribute to exclude methods from being available to remote clients
- `Line 134`: Special handling for plugins by searching in different directory paths than standard modules
- `Line 147`: Caches connector collections by module name to improve performance on subsequent calls
### CreateInterfaces.cs

CreateInterfaces implements functionality to generate interface files for OpenPetra's shared code. The class provides a static method AddNamespacesFromYmlFile that reads namespace definitions from YAML configuration files and generates appropriate using statements for C# interface files. It handles both standard modules and plugins differently, with special handling for plugin namespaces. The method reads from InterfacesUsingNamespaces.yml in the output path and adds namespace references based on the specified module name. The class is part of the GenerateSharedCode namespace and relies on several utility libraries for XML parsing and code generation.

 **Code Landmarks**
- `Line 51`: Special handling for plugins with different namespace resolution approach
- `Line 58`: Uses YAML configuration files to dynamically determine required namespaces for interface files
### CreateServerGlue.cs

CreateServerGlue.cs generates server-side code that handles communication between client and server components in OpenPetra. It creates WebConnector and UIConnector classes that expose methods to clients while implementing security checks. The code processes C# class definitions to generate server glue code with proper parameter handling, type conversions, and module permission checks. Key functionality includes creating web service endpoints, handling binary serialization for complex types, and implementing security validation. Important methods include GenerateCode, CreateServerGlue, WriteWebConnector, WriteUIConnector, and InsertWebConnectorMethodCall.

 **Code Landmarks**
- `Line 58`: Creates module access permission checks for methods, ensuring proper security validation before execution
- `Line 167`: Handles complex parameter conversion between client-server communication including binary serialization
- `Line 396`: Special handling for interface properties that represent sub-UIConnectors, maintaining object references across the service boundary
### NamespaceItem.cs

TNamespace implements a class for parsing and managing namespace hierarchies in OpenPetra's code generation tools. It provides functionality to represent namespaces as tree structures with parent-child relationships. The class supports parsing namespaces from C# source files, finding or creating namespaces in the hierarchy, and searching for specific modules. Key methods include ParseFromDirectory, FindOrCreateNamespace, FindModuleIndex, and FindModule. Important properties include Name and Children, which stores child namespaces in a SortedList.

 **Code Landmarks**
- `Line 76`: FindOrCreateNamespace implements recursive namespace hierarchy creation, handling dot-notation for nested namespaces
- `Line 108`: ParseFromDirectory method uses CSParser to extract namespaces from C# files, focusing on those ending with 'Connectors'
- `Line 126`: Static FindModuleName variable is used as shared state between FindModuleIndex and FindModule methods
### Program.cs

Program.cs implements a build tool that generates client-server communication code (glue) for OpenPetra. It parses C# files to extract namespace hierarchies and generates web service connectors based on templates. The tool supports generating code for both the core OpenPetra system and plugins. Key functionality includes parsing source directories for namespaces, generating server-side glue code, and handling command-line options. Important methods include Main(), GenerateGlueForOpenPetraCore(), and GenerateGlueForPlugin(). The program uses TCmdOpts for command-line parsing and TNamespace for namespace hierarchy management.

 **Code Landmarks**
- `Line 49`: Command-line interface with sample call pattern for proper usage
- `Line 69`: Dynamically calculates ICT path from output directory to ensure proper file structure
- `Line 98`: Parses C# files to build namespace hierarchy before code generation
- `Line 119`: Handles special case for ServerAdmin namespace that may not be detected through parsing

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #