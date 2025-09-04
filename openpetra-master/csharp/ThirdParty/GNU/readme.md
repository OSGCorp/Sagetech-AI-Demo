# C# GNU Libraries Subproject Of OpenPetra

The OpenPetra application is a C# program that helps non-profit organizations manage administration and reduce operational costs. This sub-project implements internationalization capabilities through GNU gettext integration, providing a robust framework for multi-language support across the entire application. The C# GNU Libraries provide these capabilities to the OpenPetra program:

- Resource management for internationalization using GNU gettext principles
- Translation lookups across multiple assemblies for different cultures
- Support for both standard translations and plural forms
- Culture-specific plural form evaluation

## Identified Design Elements

1. **Custom ResourceManager Implementation**: GettextResourceManager extends the standard .NET ResourceManager to provide GNU gettext-style internationalization
2. **Multi-Assembly Translation Support**: The system can look up translations across different assemblies, enabling modular internationalization
3. **Caching Mechanism**: Translations are cached for performance optimization, reducing lookup overhead
4. **Plural Form Handling**: Sophisticated support for plural forms with culture-specific rules for proper linguistic representation
5. **Particular Context Support**: Allows for context-specific translations where the same term might have different meanings in different contexts

## Overview
The architecture follows GNU gettext internationalization principles while integrating seamlessly with .NET's resource management framework. The implementation consists of a custom ResourceManager and ResourceSet that handle both singular and plural translations with appropriate cultural sensitivity. This approach enables OpenPetra developers to create applications that can be easily localized to different languages and regions, making the software accessible to a global audience while maintaining a consistent codebase.

## Business Functions

### Internationalization Core
- `AssemblyInfo.cs` : Assembly metadata configuration file for GNU Gettext library integration in OpenPetra.
- `intl.cs` : C# implementation of GNU gettext for internationalization, providing translation functionality for OpenPetra.

### Documentation
- `Readme.txt` : Documentation file explaining the GNU gettext package integration with C# in OpenPetra.

## Files
### AssemblyInfo.cs

AssemblyInfo.cs defines metadata attributes for the GNU Gettext assembly used in OpenPetra. It sets assembly properties including title, company, copyright information, and version number (0.18.1.1). The file also configures COM visibility settings, making types invisible by default. This standard .NET assembly information file provides identity and versioning for the GNU Gettext library integration.

 **Code Landmarks**
- `Line 16`: Copyright notice indicates this is part of the Free Software Foundation's GNU project, confirming its open-source nature
- `Line 21`: ComVisible(false) attribute ensures types aren't exposed to COM by default, important for security and encapsulation
- `Line 28`: Assembly version 0.18.1.1 indicates this is using a specific version of GNU Gettext for internationalization support
### Readme.txt

This readme file provides information about the GNU gettext package used in OpenPetra for internationalization. It includes links to the official GNU gettext homepage and documentation specific to C# implementation. The file explains how to compile the GNU.Gettext.dll by creating an AssemblyInfo.cs file and using a modified copy of intl.cs from the gettext package, with instructions for compilation using the mono shell with gmcs compiler.

 **Code Landmarks**
- `Line 7`: References C# integration documentation for GNU gettext internationalization framework
- `Line 10`: Documents custom modifications to the original gettext package files for OpenPetra integration
- `Line 13`: Provides specific compilation command for building the GNU.Gettext.dll library in Mono environment
### intl.cs

GettextResourceManager implements a ResourceManager subclass that provides internationalization support using GNU gettext approach. It manages translation lookups across multiple assemblies for different cultures, supporting both standard translations and plural forms. The class loads satellite assemblies containing translations, caches them for performance, and provides methods like GetString, GetPluralString, GetParticularString, and GetParticularPluralString. The file also defines GettextResourceSet class that encapsulates a single PO file, handling both singular and plural translations with culture-specific plural form evaluation.

 **Code Landmarks**
- `Line 85`: Custom satellite assembly loading that works around issues in mono-0.28 and handles ASP.NET application paths
- `Line 102`: Sophisticated class name construction algorithm that handles non-alphanumeric characters using hexadecimal escaping
- `Line 214`: Context-specific translations using the \u0004 character as separator between context and message ID
- `Line 359`: Plural form handling with extensible PluralEval method for language-specific plural rules

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #