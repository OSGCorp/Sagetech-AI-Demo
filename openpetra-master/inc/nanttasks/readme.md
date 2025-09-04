# Include NANT Tasks Subproject Of OpenPetra

The OpenPetra is a C# application that provides administrative management capabilities for non-profit organizations. The program handles financial operations and contact management while supporting cross-platform deployment. This sub-project implements custom NAnt build tasks along with project generation utilities that enable OpenPetra's build system to function across different operating environments.  This provides these capabilities to the OpenPetra program:

- Cross-platform build automation for Windows and Linux/Mono environments
- Database operations integration with PostgreSQL and MySQL
- Automated project file generation and dependency management
- Custom test execution through NUnit integration

## Identified Design Elements

1. Platform Abstraction: Tasks like ExecDotNet and ExecCmd automatically detect the operating environment and adjust command execution accordingly
2. Project Structure Analysis: GenerateNamespaceMap parses C# files to extract namespace declarations and build dependency maps
3. Template-Based Generation: GenerateProjectFiles uses templates to create consistent project files across different development environments
4. Database Integration: Dedicated tasks for PostgreSQL and MySQL operations with configurable connection parameters

## Overview
The architecture emphasizes cross-platform compatibility through environment detection and appropriate command adaptation. The build system implements sophisticated dependency analysis to generate project files with correct references. Database operations are abstracted through dedicated tasks that handle connection parameters and command execution. The project provides both high-level solution compilation and granular project-level compilation options, with fallback mechanisms when preferred tools aren't available. This design ensures OpenPetra can be built, tested, and deployed consistently across different operating systems and development environments.

## Business Functions

### Database Management Tasks
- `Postgresql.cs` : NAnt task for executing PostgreSQL commands in the OpenPetra build system.
- `Mysql.cs` : NAnt task for executing MySQL database commands in the OpenPetra build system.

### Build and Compilation Tasks
- `CompileSolution.cs` : NAnt task for compiling Visual Studio solution files with fallback to CSC compilation when MSBuild is unavailable.
- `CompileProject.cs` : NAnt task for compiling C# projects from .csproj files with various compilation options and dependency resolution.
- `GenerateProjectFiles.cs` : NAnt task that generates project and solution files for OpenPetra from templates.

### Command Execution Tasks
- `ExecDotNet.cs` : Custom NAnt task for executing .NET programs with automatic Mono support on Linux systems.
- `ExecCmd.cs` : NAnt task extension for executing shell commands with platform-specific adaptations and superuser capabilities.

### Testing Tasks
- `NUnitConsole.cs` : Custom NAnt task for running NUnit tests from the console with configurable assembly and test case parameters.

### Project Analysis Tasks
- `GenerateNamespaceMap.cs` : NAnt task that generates namespace mapping files for OpenPetra project dependencies.

## Files
### CompileProject.cs

CompileProject implements a NAnt task for compiling C# projects from .csproj files. It supports compiling by project name, specific C# file, or direct .csproj file path. The task parses project files to extract compilation settings, references, source files, and embedded resources. It offers two compilation methods: using the C# compiler directly or generating a solution for MSBuild. Key features include conditional compilation (OnlyOnce flag), platform-specific handling for Windows/Linux, and automatic namespace resolution. Important properties include CSProjFile, CSFile, ProjectName, CodeRootDir, UseCSC, and OnlyOnce.

 **Code Landmarks**
- `Line 137`: Dynamically determines the appropriate C# compiler based on platform (mcs for Unix, csc for Windows)
- `Line 313`: Handles platform-specific compiler options by replacing '/' with '-' for Unix systems
- `Line 252`: Extracts namespace and class information from C# source files using regex-free parsing
- `Line 394`: Uses a namespace mapping system to resolve project files from C# source files
### CompileSolution.cs

CompileSolution implements a NAnt task for compiling Visual Studio solution files. It provides two compilation methods: using MSBuild (preferred on Windows) or a custom CSC-based approach as fallback. The task parses solution files to extract project references and compiles each project individually. Key functionality includes checking if deliverables already exist (OnlyOnce option) and platform-specific compilation paths. Important elements include the ExecuteTask method that chooses the compilation strategy and CompileSolutionCSC method that parses and compiles projects individually.

 **Code Landmarks**
- `Line 78`: Platform detection logic to determine compilation approach (MSBuild vs CSC)
- `Line 92`: Uses NAnt's CallTask to delegate to MSBuild when available instead of direct integration
- `Line 124`: Custom solution file parsing to extract and compile individual projects
### ExecCmd.cs

ExecCmdTask implements a NAnt task extension that executes commands on the shell or command line with platform-specific adaptations. It inherits from NAnt.Core.Tasks.ExecTask and adds functionality to handle different operating systems (Windows/Unix) by automatically adjusting the command execution format. The class provides superuser capability on Unix systems through the optional SuperUser property. The ExecuteTask method overrides the base implementation to modify the command execution based on the platform, prepending appropriate shell commands and handling superuser privileges when specified.

 **Code Landmarks**
- `Line 63`: Platform detection logic that modifies command execution format based on operating system
- `Line 66`: Superuser privilege implementation for Unix systems using sudo command
### ExecDotNet.cs

ExecDotNetTask implements a custom NAnt task that extends NAnt.Core.Tasks.ExecTask to run .NET programs cross-platform. It automatically detects when running on Linux/Mono environments and modifies the execution command accordingly, prefixing the command with 'mono' and adjusting arguments. On Windows, it falls back to the default execution behavior. The class is part of the Ict.Tools.NAntTasks namespace and is marked with the TaskName attribute 'ExecDotNet' for NAnt integration.

 **Code Landmarks**
- `Line 40`: Detects platform using NAnt.Core.PlatformHelper.IsMono to determine execution approach
- `Line 43`: Preserves original program filename before modifying execution parameters for Mono
### GenerateNamespaceMap.cs

GenerateNamespaceMap implements a NAnt task that creates mapping files between namespaces and their containing directories/DLLs in the OpenPetra project. It parses C# files to extract namespace declarations and using statements, building a comprehensive map of project dependencies. The task generates two output files: a namespace-to-DLL mapping and a project dependency map showing which assemblies reference others. Key functionality includes parsing C# files, resolving third-party references, handling special cases like executable projects, and supporting standalone compilation mode. Important classes include GenerateNamespaceMap and TDetailsOfDll, with methods like ParseCSFile, UsingNamespaceMapToDll, and GetProjectNameFromCSFile.

 **Code Landmarks**
- `Line 144`: Handles special case for Windows Forms applications by detecting System.Windows.Forms references
- `Line 158`: Enforces architectural boundary by preventing server namespace references from shared code
- `Line 233`: Dynamically resolves NuGet package versions from packages.config for third-party references
- `Line 66`: Supports limiting namespace generation to specific namespaces with wildcard pattern matching
### GenerateProjectFiles.cs

GenerateProjectFiles implements a NAnt task that automates the creation of project files (.csproj) and solution files (.sln) for the OpenPetra project. It reads dependency maps and GUIDs from configuration files, processes template files for different development environments, and generates the appropriate project structure. The task handles project dependencies, references, file inclusions, and solution organization. Key functionality includes topological sorting of projects by dependencies, relative path calculation, and template substitution. Important methods include ExecuteTask(), WriteSolutionFile(), WriteProjectFile(), and SortProjectsByDependencies(). The class maintains project GUIDs for consistency across builds and supports multiple development environments.

 **Code Landmarks**
- `Line 344`: Implements topological sorting algorithm to handle project dependencies correctly
- `Line 540`: Custom relative path calculation function that handles cross-platform path separators
- `Line 1038`: Reads dependency maps from external files to determine project relationships
### Mysql.cs

MysqlTask implements a custom NAnt task for executing commands against MySQL databases. It provides functionality to run SQL commands or SQL files against a MySQL database with configurable connection parameters. The task supports specifying the MySQL executable path, database name, host, user credentials, and SQL commands either directly or from files. It can also capture command output to a file. Key properties include MysqlExecutable, Database, SQLCommand, SQLFile, OutputFile, Host, User, and Password. The ExecuteTask method handles process creation, command execution, and output processing.

 **Code Landmarks**
- `Line 186`: Handles password masking in logs for security by replacing actual passwords with 'xxx'
- `Line 233`: Implements a sleep delay before sending SQL commands to ensure the MySQL process is ready
- `Line 248`: Filters certain SQL commands (INSERT, GRANT, COPY, DELETE) from console output to reduce verbosity
### NUnitConsole.cs

NUnitConsoleTask implements a custom NAnt task for executing NUnit tests from the command line. It allows specifying which assembly to test and optionally which specific test case to run. The task handles both Windows and non-Windows environments by using appropriate commands (direct execution or through Mono). Key functionality includes validating inputs, configuring the test process, capturing output, and handling errors. Important properties include AssemblyName and TestCase, with the main ExecuteTask method managing the test execution process and error handling.

 **Code Landmarks**
- `Line 91`: Platform-specific handling for running NUnit tests on non-Windows systems using Mono
- `Line 102`: Conditional test case filtering using command-line arguments when TestCase is specified
- `Line 118`: Process execution with redirected standard error to capture test output
### Postgresql.cs

PsqlTask implements a NAnt task for running PostgreSQL commands during the build process. It provides functionality to execute SQL commands or scripts against a PostgreSQL database with configurable connection parameters. The task supports specifying the database name, host, port, username, password, and either direct SQL commands or SQL files as input. It handles command execution through the psql executable, with special handling for Unix environments when running as the postgres user. Important properties include PsqlExecutable, Database, DatabaseHost, DatabasePort, SQLCommand, SQLFile, Username, and Password. The ExecuteTask method manages the process execution and error handling.

 **Code Landmarks**
- `Line 181`: Uses environment variables to set PostgreSQL password and client message level
- `Line 219`: Special handling for Unix systems when running as postgres user via sudo
- `Line 236`: Filters common SQL output messages to reduce console noise

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #