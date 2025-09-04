# C# Code Checker Of OpenPetra

The OpenPetra is a C# program that provides administrative management tools for non-profit organizations. The program handles financial transactions and manages organizational data across multiple modules. This sub-project implements static code analysis for the OpenPetra codebase along with pattern-based detection of potential runtime issues. This provides these capabilities to the OpenPetra program:

- Identification of static variables that could cause issues in ASP.NET environments
- Detection of improper database transaction handling patterns
- Regex-based code scanning for problematic coding patterns
- False-positive filtering system to reduce noise in analysis results

## Identified Design Elements

1. Pattern-Based Code Analysis: Uses regular expressions to identify potentially problematic code patterns in C# source files
2. Recursive File Scanning: Automatically traverses the codebase to locate and analyze all relevant source files
3. Configurable False-Positive System: Allows developers to mark known exceptions to avoid repeated flagging
4. CI/Build Integration: Returns numeric exit codes that can be interpreted by continuous integration systems

## Overview
The architecture emphasizes code quality assurance through automated static analysis. The tool focuses particularly on database transaction handling patterns, which are critical for the application's data integrity. Developers can extend the tool by adding new regex patterns or false-positive filters in their respective methods. The command-line interface provides both console output and log files for review, with a companion batch script that provides human-readable interpretation of results. This tool serves as a quality gate to prevent problematic code patterns from entering the production codebase.

## Business Functions

### Code Analysis Tools
- `CodeChecker.cs` : A code analysis tool that checks C# files for problematic patterns like static variables and database transaction issues.
- `readme.txt` : Documentation for the Ict.Tools.CodeChecker utility that scans C# code for problematic patterns using regular expressions.
- `RunCodeChecker.bat` : Batch script that runs the CodeChecker tool and reports results based on error levels.

## Files
### CodeChecker.cs

CodeChecker is a command-line utility that analyzes C# source files in the OpenPetra project for potential issues. It supports two main actions: checking for static variables (which are problematic in ASP.NET) and scanning for database access patterns that might indicate transaction handling problems. The tool uses regular expressions to identify suspicious code patterns and supports a false-positive filtering system to avoid flagging acceptable exceptions. Key functionality includes recursive file scanning, pattern matching with detailed reporting, and returning error codes that can be used by CI systems like Jenkins. Important methods include Main(), GetFiles(), DeclareRegExpressions(), and DeclareFalsePositives().

 **Code Landmarks**
- `Line 47`: Command-line interface with action parameter to select different code checking modes
- `Line 87`: Uses regular expressions with false-positive filtering to avoid flagging acceptable code patterns
- `Line 204`: Returns number of matches as exit code for integration with CI systems like Jenkins
- `Line 307`: Comprehensive database access pattern detection for transaction handling issues
- `Line 366`: Two-tier false positive system with full match and end match dictionaries for flexibility
### RunCodeChecker.bat

This batch script executes the Ict.Tools.CodeChecker.exe application and interprets its exit code to provide appropriate feedback. It handles three possible outcomes: errors (negative exit code), success (zero exit code), or matches found (positive exit code). The script outputs a corresponding message for each scenario, making it easy to understand the results of the code checking process at a glance.

 **Code Landmarks**
- `Line 3`: Executes the CodeChecker tool without any command-line parameters
- `Line 5-7`: Uses conditional branching based on ERRORLEVEL to determine execution path
- `Line 9`: Interprets negative error levels as errors in the CodeChecker tool itself
### readme.txt

This readme file documents the Ict.Tools.CodeChecker utility, a tool designed to scan the OpenPetra C# codebase for problematic code patterns using regular expressions. The utility runs without command line arguments, executing a set of hard-coded regex searches primarily focused on finding database access commands that improperly use 'null' instead of TDBTransaction instances. The tool outputs findings to both console and a log file, returning a numeric exit code indicating the number of matches found. The document explains how developers can extend the tool by adding new regex patterns in the DeclareRegExpressions method and suppress false positives in the DeclareFalsePositives method.

 **Code Landmarks**
- `Line 8-10`: Defines the scope of code scanning across both application code and related utilities
- `Line 12-16`: Explains the extensibility model where developers can add new regex patterns to find different code problems
- `Line 23-26`: Documents the exit code system that enables integration with CI/CD build server jobs

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #