# C# Code Generation in OpenPetra

The C# Code Generation subproject provides essential tooling for automated code analysis, manipulation, and generation within the OpenPetra ecosystem. This component enables maintainable and consistent code production through static analysis, validation enforcement, and structured code manipulation. The subproject implements source code parsing, validation rule generation, and targeted code insertion capabilities that support OpenPetra's development workflow.

Key capabilities provided to the OpenPetra program:

- Static analysis of C# source files to extract class hierarchies, interfaces, and method signatures
- Automated data validation code generation based on database schema constraints
- Region-based code insertion for maintaining generated code sections
- Type and parameter manipulation utilities for consistent code generation

## Identified Design Elements

1. **NRefactory Integration**: Leverages SharpDevelop's NRefactory to parse and analyze C# source files, with caching for performance optimization
2. **Database-Driven Validation**: Generates validation code based on database constraints (NULL checks, date validation, string length, numeric ranges)
3. **Namespace Management**: Handles namespace resolution and mapping to ensure proper type references across the codebase
4. **Region-Based Code Insertion**: Preserves manually written code while updating generated sections through region markers
5. **Code Formatting Utilities**: Maintains consistent code style through parameter formatting and type reference normalization

## Architecture Overview

The architecture follows a modular approach with clear separation of concerns. CSParser handles code analysis, Validation manages constraint enforcement, AutoGenerationTools provides formatting utilities, and InsertIntoRegion enables targeted code modification. Together, these components create a pipeline for analyzing existing code, generating new code with proper validation, and inserting it into appropriate locations while maintaining code quality and consistency.

## Business Functions

### Code Generation Tools
- `CSParser.cs` : C# parser utility for code generation that analyzes C# source files using NRefactory.
- `Validation.cs` : Provides data validation functionality for database fields in OpenPetra's code generation system.
- `AutoGenerationTools.cs` : Utility class for code generation with methods for parameter formatting and type handling.
- `InsertIntoRegion.cs` : Utility class for inserting generated code into specific regions of source files.

## Files
### AutoGenerationTools.cs

AutoGenerationTools implements utility functions for code generation in OpenPetra. It provides methods to manipulate and format method declarations, parameters, and type references. Key functionality includes adding parameters to method declarations, formatting parameter lists for both definition and actual parameter usage, and converting type references to readable strings with namespace handling. Important methods include AddParameter(), FormatParameters(), and TypeToString(), which help generate properly formatted C# code with appropriate parameter modifiers and type declarations.

 **Code Landmarks**
- `Line 86`: Handles parameter modifiers (ref/out) when formatting method parameters
- `Line 126`: Intelligently shortens type names by removing namespace prefixes when the type is in the current namespace
### CSParser.cs

CSParser implements a wrapper for NRefactory from SharpDevelop to parse and analyze C# source files for code generation. It provides functionality to parse C# files, extract namespaces, classes, interfaces, methods, properties, fields, and constructors. The class includes methods for finding specific classes or interfaces across multiple files, retrieving implemented interfaces, and working with web connector classes. It also manages namespace mappings and caches parsed files for performance. Key methods include ParseFile, GetNamespaces, GetClasses, GetMethods, FindClass, and GetWebConnectorClasses. The parser supports traversing the code structure and extracting information needed for code generation tasks.

 **Code Landmarks**
- `Line 72`: Uses NRefactory library from SharpDevelop to parse C# code into an abstract syntax tree
- `Line 379`: Implements caching of parsed files with a static SortedList to improve performance when accessing the same files multiple times
- `Line 347`: Uses a namespace mapping system to locate source directories based on namespace names
- `Line 394`: Filters generated code files to avoid parsing auto-generated code except for specific cases
### InsertIntoRegion.cs

TInsertIntoRegion implements a utility class for code generation that modifies existing source files by replacing content within specified regions. The class provides a single static method InsertIntoRegion that takes a filename, region name, and new code to insert. It reads the source file line by line, identifies the target region by its name, replaces its content with the provided code while preserving indentation, and writes the result to a new file. The method uses TTextFile.UpdateFile to finalize the changes and returns a boolean indicating success or failure.

 **Code Landmarks**
- `Line 53`: Uses string replacement of 'INDENT' placeholder to maintain proper indentation in generated code
- `Line 72`: Leverages TTextFile.UpdateFile for safe file replacement after modification
### Validation.cs

TDataValidation implements functionality for automatic data validation in OpenPetra's code generation system. It defines an enumeration for validation scopes (TAutomDataValidationScope) and provides the GenerateAutoValidationCodeForDBTableField method that determines whether validation code should be generated for database fields. The method checks various conditions based on the specified scope, including NOT NULL constraints, date validation, string length limits, and number range validations. It returns a boolean indicating if validation is needed and outputs a reason string explaining the validation requirement.

 **Code Landmarks**
- `Line 72`: Comprehensive validation logic that handles multiple constraint types including primary keys, NOT NULL constraints, and foreign keys
- `Line 87`: Special handling for string fields that are foreign keys, requiring non-empty values

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #