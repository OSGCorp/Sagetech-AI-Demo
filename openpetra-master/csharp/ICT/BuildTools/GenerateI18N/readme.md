# C# Generate I18N Subproject Of OpenPetra

The C# Generate I18N subproject implements internationalization (i18n) functionality for OpenPetra, enabling the application to be translated into multiple languages. This toolset automates the extraction, management, and integration of translatable strings throughout the codebase. The subproject processes C# source files, UI designer files, and YAML navigation definitions to ensure all user-facing text is properly prepared for translation using the gettext framework.

Key capabilities provided to OpenPetra include:

- Automated extraction of translatable strings from source code and UI definitions
- Management of PO (Portable Object) translation files with UTF-8 encoding support
- Filtering of strings that should not be translated to simplify translator workload
- Generation of error code documentation with internationalized descriptions
- Integration with the gettext internationalization framework

## Identified Design Elements

1. **Catalog String Generation**: Automatically injects `Catalog.GetString()` calls into C# files by analyzing UI designer files and extracting text properties
2. **PO File Management**: Parses and updates gettext PO files while preserving file structure and metadata
3. **Translation Filtering**: Implements a "Do Not Translate" mechanism to exclude technical strings from translation files
4. **Command-line Interface**: Provides a comprehensive CLI tool for managing the entire i18n workflow
5. **Error Code Documentation**: Generates internationalized HTML documentation for application error codes organized by module

## Overview

The architecture follows a modular approach with specialized classes handling different aspects of the internationalization process. The system integrates with the build pipeline to ensure translations are consistently maintained. The tooling emphasizes automation to reduce manual effort in preparing strings for translation while ensuring comprehensive coverage of user-facing text throughout the application.

## Business Functions

### Internationalization Tools
- `GenerateCatalogStrings.cs` : Tool that generates internationalization code for OpenPetra by adding Catalog.GetString calls to C# files.
- `ParsePoFile.cs` : Parses and updates gettext PO files for internationalization in OpenPetra
- `DropUnwantedStrings.cs` : Utility for filtering translation strings in OpenPetra's internationalization system
- `Program.cs` : Tool for generating internationalization resources from source code for OpenPetra.

### Documentation Generation
- `GenerateErrorCodeDoc.cs` : Generates HTML documentation for error codes used throughout OpenPetra by parsing source files and creating formatted documentation.

## Files
### DropUnwantedStrings.cs

TDropUnwantedStrings implements functionality to filter out strings that should not be translated from PO translation files. It reads a 'Do Not Translate' file to identify strings to exclude, then processes translation files to remove these entries while preserving essential metadata. The class maintains source references in a separate file for reference while simplifying the main translation file. Key methods include GetDoNotTranslateStrings(), RemoveUnwantedStringsFromTranslation(), ReviewTemplateFile(), and AdaptString(). The implementation handles UTF-8 encoding and properly manages PO file structure including comments and source references.

 **Code Landmarks**
- `Line 45`: Creates a collection of strings that should not be translated by parsing a special PO file
- `Line 80`: Creates both a clean translation file and a reference file with all source locations preserved
- `Line 169`: Special handling for automatically generated strings from designer files
- `Line 209`: Secondary pass through the template file to clean up source references and empty lines
### GenerateCatalogStrings.cs

TGenerateCatalogStrings implements functionality to automate internationalization in OpenPetra by injecting Catalog.GetString calls into C# files. It parses designer files to extract UI text properties and adds them to main code files, ensuring all user-facing strings are translatable. The class also handles database help text extraction and UI navigation elements for translation. Key methods include Execute() which processes files to add translation code, GetDesignerFilename() to locate designer files, and AddTranslationUINavigation() which extracts strings from navigation YAML files.

 **Code Landmarks**
- `Line 47`: Main method that analyzes C# files and injects internationalization code at specific points in the class constructor
- `Line 173`: Uses regex pattern matching to identify database help text that needs translation
- `Line 196`: Recursively parses UI navigation YAML files to extract all menu labels for translation
### GenerateErrorCodeDoc.cs

TGenerateErrorCodeDoc implements functionality to create HTML documentation for error codes used throughout OpenPetra. It parses C# source files to extract error code definitions, their descriptions, and categories. The main functionality includes parsing source files for error code attributes, organizing them by module (General, Partner, Finance, etc.), and generating HTML documentation using templates. Key methods include Execute() which orchestrates the documentation generation process, ProcessFile() which extracts error code information from parsed files, and ExpressionToString() which converts code expressions to string representations. Important variables include ErrorCodes dictionary that stores error code information and snippets dictionary that organizes the HTML output by module.

 **Code Landmarks**
- `Line 47`: Uses a dictionary to collect and organize error codes from multiple source files before documentation generation
- `Line 66`: Dynamically creates HTML documentation snippets for different modules using a template-based approach
- `Line 147`: Parses C# code using NRefactory to extract error code attributes and their metadata
- `Line 156`: Determines error code category based on suffix (V for Validation, N for NonCriticalError)
### ParsePoFile.cs

TPoFileParser implements functionality for parsing and updating gettext PO (Portable Object) files used for internationalization in OpenPetra. The class provides two key static methods: ParsePoLine which extracts message strings from PO file entries by handling multi-line quoted text formats, and WriteUpdatedPoFile which updates existing PO files with new translations while preserving the file structure. The parser handles UTF-8 encoding and manages duplicate message IDs. Important variables include StringCollection for tracking original lines and SortedList for managing translations.

 **Code Landmarks**
- `Line 46`: Handles multi-line message entries in PO files by concatenating quoted strings
- `Line 74`: Uses UTF-8 encoding without BOM when writing updated PO files
- `Line 89`: Detects and logs duplicate message IDs in PO files
### Program.cs

Program implements a command-line tool for generating internationalization resources for OpenPetra. It extracts translatable strings from C# and YAML files, processes them with gettext, and manages translation templates. Key functionality includes parsing source files with gettext, removing strings marked as "do not translate", generating error code documentation, and processing catalog strings. Important methods include ParseWithGettext(), Main(), and references to TGenerateCatalogStrings.Execute() which handles the actual string extraction from various file types.

 **Code Landmarks**
- `Line 35`: Uses external gettext program for parsing translatable strings from source files
- `Line 75`: Supports removing strings from translation files that are marked as "do not translate"
- `Line 107`: Processes both C# and YAML files for internationalization strings
- `Line 86`: Generates documentation for error codes as part of the i18n process

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #