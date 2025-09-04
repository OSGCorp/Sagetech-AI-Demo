# Demo SKR49 Chart of Accounts Subproject Of Petra

The Demo SKR49 Chart of Accounts is a specialized data configuration subproject that implements the German standard chart of accounts (SKR49) for non-profit organizations within the OpenPetra financial management system. This subproject provides a comprehensive accounting structure specifically designed for German associations, foundations, and charitable organizations, enabling proper financial reporting and tax compliance.

The subproject consists of three key components:
- A YAML-based account definition file that structures the hierarchical chart of accounts
- A Python conversion utility that transforms GnuCash XML account data to OpenPetra format
- Documentation for implementation and usage

## Key Technical Features

- Hierarchical account structure with 10 main classes (0-9) covering all financial aspects of German non-profits
- Account metadata including active status, account type, debit/credit nature, and validity periods
- Multilingual support with both short and long descriptions for financial reporting
- Specialized account categorization for tax-exempt vs. taxable operations
- Data conversion pipeline from industry-standard formats (GnuCash) to OpenPetra's internal format

## Identified Design Elements

1. **Standardized Financial Data Model**: Implements the German SKR49 standard while maintaining compatibility with OpenPetra's financial engine
2. **Data Transformation Pipeline**: Provides tools to convert between different accounting system formats
3. **Locale-Aware Processing**: Handles German-specific accounting requirements and character encoding
4. **Hierarchical Account Organization**: Maintains proper parent-child relationships for financial reporting and analysis

## Overview
The architecture emphasizes standards compliance for German financial reporting while providing the flexibility needed for various non-profit operational models. The conversion tools enable data migration from other systems, and the comprehensive account structure supports both basic and advanced accounting needs for German charitable organizations.

## Business Functions

### Chart of Accounts
- `skr49_accounts.yml` : Chart of accounts for German non-profit organizations following the SKR49 standard, with accounts organized for associations, foundations, and charitable LLCs.

### Data Conversion Tools
- `convert.py` : Script to convert German SKR49 chart of accounts from GnuCash format to OpenPetra format

### Documentation
- `README.md` : Instructions for importing SKR49 chart of accounts from GnuCash into OpenPetra

## Files
### README.md

This README provides instructions for importing the German SKR49 chart of accounts into OpenPetra. It outlines a three-step process: downloading the GnuCash SKR49 account chart XML file, setting the German locale environment variable, and converting the file to YAML format using a Python script. The converted YAML file can then be imported into OpenPetra through the Finance/Setup/GL/AccountTree interface. The file serves as a quick reference guide for users wanting to use the German standard chart of accounts in OpenPetra.

 **Code Landmarks**
- `Line 4`: Uses curl to download GnuCash's XML-based chart of accounts file
### convert.py

This script converts the German SKR49 chart of accounts from GnuCash XML format to OpenPetra's text format. It downloads the GnuCash SKR49 account chart, parses the XML structure, and transforms account data into OpenPetra's hierarchical format. The script handles account properties including codes, descriptions, types (Asset, Income, Expense, Liability), debit/credit attributes, and active status. It also fixes several data inconsistencies in the source file and organizes accounts into a proper parent-child hierarchy for export. Key functions include export_account() which recursively outputs the account hierarchy.

 **Code Landmarks**
- `Line 15`: Validates German locale is set before processing to ensure proper character handling
- `Line 46`: Intelligent formatting of account properties that only outputs differences from parent accounts to reduce redundancy
- `Line 86`: Contains fixes for specific data inconsistencies in the source GnuCash file
- `Line 103`: Maps GnuCash account types to OpenPetra's accounting system types
### skr49_accounts.yml

This file defines a comprehensive chart of accounts (SKR49) specifically designed for German non-profit organizations including associations, foundations, and charitable limited liability companies. The chart organizes accounts into a hierarchical structure with main classes 0-9 representing different types of accounts: assets (0), liabilities (1), income from non-profit activities (2-3), income from asset management (4), income from tax-exempt sports operations (5), income from other tax-exempt operations (6), income from taxable sports operations (7), income from other taxable operations (8), and statistical accounts (9). Each account is defined with properties including active status, account type (Asset, Liability, Income, Expense), debit/credit nature, validity, and descriptions. The file provides both short and long descriptions for accounts, making it suitable for financial reporting and tax compliance in the German non-profit sector.

 **Code Landmarks**
- `Line 5`: Implements DATEV SKR49 chart of accounts specifically for non-profit organizations in Germany
- `Line 17`: Hierarchical structure organizes accounts by type with parent-child relationships for reporting
- `Line 22`: Each account includes metadata like active status, account type, and tax relevance

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #