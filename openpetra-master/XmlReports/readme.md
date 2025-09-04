# XML Reports Subproject Of OpenPetra

XML Reports is a structured reporting framework that provides OpenPetra with a flexible, template-based reporting system for financial and administrative data. The subproject implements a declarative XML-based approach to report definition and generation, along with reusable data retrieval mechanisms for consistent financial reporting across the application. This provides these capabilities to the OpenPetra program:

- Standardized report definition through XML templates
- Reusable financial calculations and database queries
- Hierarchical data presentation with multi-level reporting
- Parameter-driven report customization

## Identified Design Elements

1. **Declarative Report Structure**: Reports are defined using XML templates that separate data retrieval from presentation logic
2. **Shared Query Repository**: Common financial queries are centralized in common.xml to ensure consistency and promote code reuse
3. **Hierarchical Data Representation**: The framework supports complex multi-level reports with headers, details, and footers
4. **Conditional Formatting**: Reports can include dynamic content through switch/case statements and conditional elements
5. **Standardized Calculations**: Pre-defined calculations for financial metrics like variances and percentages ensure consistency

## Overview
The architecture follows a template-based approach where report definitions are separated from the rendering engine. The DTD schema provides strict validation of report structure while allowing flexibility in presentation. Common financial calculations and queries are abstracted into reusable components, promoting consistency across reports. The system supports parameter-driven behavior, allowing reports to be customized at runtime while maintaining a standardized structure. This design enables non-profit organizations to generate consistent financial and administrative reports while minimizing code duplication.

## Sub-Projects

### XmlReports/Settings

This component serves as the bridge between raw data and formatted output by leveraging XML-based configuration files to define report structures, parameters, and presentation rules.

The subproject implements a template-driven reporting system along with parameter management capabilities. This provides these capabilities to the OpenPetra program:

- Declarative report definition through XML configuration files
- Dynamic parameter handling for customizable reports
- Consistent styling and formatting across different report types
- Support for multiple output formats (PDF, HTML, CSV)
- Reusable report components and layouts

#### Identified Design Elements

1. **XML Configuration Schema**: Defines a structured format for report definitions, including data sources, parameters, and layout specifications
2. **Parameter Processing Engine**: Handles user-provided parameters, applying validation rules and default values as specified in the configuration
3. **Report Rendering Pipeline**: Transforms the XML configuration and data into the requested output format through a series of processing steps
4. **Template System**: Provides reusable report components (headers, footers, tables) that maintain consistent styling
5. **Caching Mechanism**: Improves performance by storing frequently used report definitions and rendered components

#### Overview
The architecture follows a modular design that separates report definition from data processing and rendering. This separation enables non-technical users to modify report layouts without changing application code. The XML-based approach provides a balance between flexibility and structure, while the parameter system allows reports to be customized at runtime. The rendering pipeline supports multiple output formats to accommodate different user needs and integration scenarios.

  *For additional detailed information, see the summary for XmlReports/Settings.*

### XmlReports/Conference

The program handles financial accounting and contact relationship management through a modular architecture. This sub-project implements conference management reporting capabilities along with XML-based report definition templates. This provides these capabilities to the OpenPetra program:

- Comprehensive attendee reporting with multiple filtering options
- Financial tracking across sending/receiving/charged fields
- Demographic analysis by age, nationality, and language
- Logistical management for accommodation and transportation
- Group and role-based attendance tracking

#### Identified Design Elements

1. XML-Based Report Templates: Report definitions use a structured XML format that separates parameters, calculations, and presentation elements
2. Reusable Calculation Components: Common SQL queries and data processing functions are defined in shared files (e.g., conference.xml) for reuse across reports
3. Hierarchical Data Organization: Reports implement multi-level structures for organizing information by fields, families, or groups
4. Flexible Selection Parameters: Reports support consistent filtering options for attendees, conferences, and date ranges

#### Overview
The architecture emphasizes a declarative approach to report definition, with clear separation between data retrieval (SQL queries), data processing (calculations), and presentation (formatting). Reports follow consistent patterns for parameter handling and hierarchical data organization. The system supports various output formats and detail levels, allowing users to generate everything from summary statistics to comprehensive attendee listings with financial details. The XML templates provide a maintainable framework for extending reporting capabilities without modifying core application code.

  *For additional detailed information, see the summary for XmlReports/Conference.*

### XmlReports/Partner

The XML Partner Section subproject implements reporting and template functionality for partner-related data, providing a critical interface between the database layer and the presentation layer. This subproject primarily focuses on generating structured reports for various partner-related information in both HTML and XML formats.

Key capabilities provided to the Petra program:

- Partner data reporting with multiple output formats (HTML, XML)
- Configurable report templates with embedded SQL queries
- Hierarchical data presentation for complex partner relationships
- Conditional display logic based on partner types and attributes
- Comprehensive filtering options for targeted data retrieval

#### Identified Design Elements

1. **Dual Template System**: Supports both XML-based report definitions and HTML templates with embedded SQL for different reporting needs
2. **Hierarchical Data Organization**: Uses nested levels in XML reports to structure complex relationships between partners and their attributes
3. **Parameterized Queries**: Implements flexible filtering through configurable parameters for partner selection, date ranges, and display options
4. **Cross-Entity Data Integration**: Joins multiple database tables to provide comprehensive views of partner information, addresses, relationships, and financial data
5. **Conditional Formatting Logic**: Applies different display rules based on partner types, data availability, and user preferences

#### Overview
The architecture emphasizes flexibility through parameterized reports, maintains consistent data presentation across different output formats, and provides comprehensive partner information retrieval capabilities. The templates are designed for extensibility with clear separation between data retrieval (SQL queries) and presentation logic. The subproject handles complex relationships between different partner types (individuals, organizations, churches) while supporting various filtering options to generate targeted reports for specific administrative needs.

  *For additional detailed information, see the summary for XmlReports/Partner.*

### XmlReports/Sponsorship

The XML Sponsorship Section subproject implements the sponsorship management functionality, which is a critical component for organizations that connect donors with beneficiaries. This module handles the structured representation and processing of sponsorship data through XML-based templates and data exchange.

#### Key Technical Capabilities

- XML-based data representation for sponsorship relationships
- Template-driven report generation for sponsored children information
- Structured data exchange between the sponsorship module and other system components
- Responsive display formatting for sponsorship information across different interfaces

#### Identified Design Elements

1. **Template-Based Report Generation**: Uses HTML templates with variable placeholders to generate consistent sponsorship reports
2. **Responsive Layout Implementation**: Employs CSS classes to ensure reports display properly across different devices and screen sizes
3. **Data Binding Architecture**: Connects backend sponsorship data to presentation templates through variable substitution
4. **Modular Component Structure**: Separates sponsorship data representation from display logic for maintainability

#### Overview
The architecture follows a template-driven approach where sponsorship data is structured in XML and rendered through HTML templates. The system maintains clear separation between data and presentation, with placeholder variables in templates that get populated at runtime. The design supports both tabular and detailed views of sponsorship information, with consistent styling and formatting across the application. This approach enables flexible reporting while maintaining data integrity throughout the sponsorship management process.

  *For additional detailed information, see the summary for XmlReports/Sponsorship.*

### XmlReports/FinancialDevelopment

The XML Financial Development Section implements report templates for financial development and donor management, providing critical insights into donation patterns and donor relationships. This sub-project delivers standardized reporting capabilities through XML-defined templates that query the database and format financial information in consistent, meaningful ways.

#### Key Capabilities

- Donor Analysis Reports: Identifies patterns such as new donors, lapsed donors, and top contributors
- Gift Tracking: Monitors donation amounts, frequencies, and methods across different time periods
- Recipient-based Reporting: Analyzes donations by recipient, fund, or motivation code
- Hierarchical Data Presentation: Organizes financial information in structured, multi-level reports
- Parameterized Reporting: Supports filtering by date ranges, minimum amounts, currencies, and other criteria

#### Identified Design Elements

1. **XML-based Report Templates**: Each report is defined in a structured XML file that specifies parameters, calculations, and output formatting
2. **SQL Query Integration**: Templates incorporate SQL queries to extract and aggregate financial data from the database
3. **Hierarchical Data Organization**: Reports implement multi-level structures (main, donor, details) for logical data presentation
4. **Custom Calculation Functions**: Specialized functions like MakeTopDonor and GetPartnerBestAddress enhance report functionality
5. **Parameterized Filtering**: All reports support extensive parameter options for customized analysis

#### Architecture Overview

The XML Financial Development Section follows a template-based architecture where each report is self-contained within its XML definition. These templates serve as the presentation layer for financial data, connecting to the underlying database through embedded SQL queries. The architecture supports both detailed transaction-level reporting and summarized analytical views, with consistent formatting and calculation methods across all reports.

  *For additional detailed information, see the summary for XmlReports/FinancialDevelopment.*

### XmlReports/Finance

This sub-project provides a comprehensive framework for generating financial reports essential to non-profit organizations' accounting and administrative needs. The architecture follows a template-driven approach where report definitions are stored as XML files that define both data retrieval logic and presentation structure.

This provides these capabilities to the Petra program:

- Standardized financial report generation (Trial Balance, Balance Sheet, Income/Expense Statements)
- Donor and recipient gift tracking and reporting
- Hierarchical data presentation with multiple detail levels
- Multi-currency support (Base, International, Transaction)
- Configurable filtering and parameter-based report customization

#### Identified Design Elements

1. **Hierarchical Report Structure**: Reports are organized in nested levels (main, summary, detail) allowing for proper grouping and totaling of financial data
2. **Reusable Calculation Components**: Common SQL queries and calculations are defined in shared files (accountdetailcommon.xml, finance.xml) for reuse across multiple reports
3. **Parameterized Reporting**: All reports support extensive filtering options including date ranges, account codes, cost centers, and currencies
4. **Multiple Output Formats**: Support for both XML-based and HTML-based report rendering to accommodate different use cases
5. **Specialized Financial Reporting**: Purpose-built templates for specific financial needs like donor statements, gift summaries, and tax reporting

#### Overview
The architecture emphasizes a clear separation between data retrieval (SQL queries) and presentation logic. Reports are highly configurable through parameters while maintaining consistent formatting and calculation methods. The template-based approach ensures consistency across the financial reporting system while allowing for specialized reports to address unique organizational needs.

  *For additional detailed information, see the summary for XmlReports/Finance.*

### XmlReports/Personnel

This sub-project implements a collection of XML-based report templates and shared calculation components that enable consistent, flexible reporting across the personnel management domain. The architecture follows a template-based approach where report definitions encapsulate both data retrieval logic and presentation formatting.

This provides these capabilities to the OpenPetra program:

- Parameterized SQL queries for personnel data extraction
- Reusable calculation components across multiple reports
- Hierarchical data presentation with conditional rendering
- Flexible filtering options (by partner, extract, or staff status)
- Standardized formatting for personnel-specific data types

#### Identified Design Elements

1. **Shared Calculation Library**: Common personnel calculations are centralized in `personnel.xml` and `commonpersonnel.xml`, providing reusable query components across reports
2. **Hierarchical Report Structure**: Reports use nested levels to represent relationships between data entities (partners, family members, documents)
3. **Parameterized Queries**: SQL queries accept runtime parameters for flexible data filtering and presentation
4. **Conditional Rendering**: Report sections can be dynamically shown/hidden based on user-selected parameters
5. **Data Transformation Pipeline**: Raw database values are processed through calculation functions to produce formatted display values

#### Overview
The architecture emphasizes reusability through shared calculation components, consistent data retrieval patterns, and standardized presentation formatting. Reports are organized by functional domain (emergency contacts, commitments, documents, etc.) with clear separation between data retrieval logic and presentation formatting. The template-based approach allows for maintainable, extensible reporting capabilities while ensuring consistent data handling across the personnel management domain.

  *For additional detailed information, see the summary for XmlReports/Personnel.*

## Business Functions

### Financial Reporting
- `common.xml` : XML file defining common calculations and queries used across multiple OpenPetra financial reports.

### Report Templates
- `template.xml` : XML template for OpenPetra reports defining structure and parameters for report generation.

### Report Schema Definitions
- `reports.dtd` : XML DTD schema defining the structure for report definitions in OpenPetra's XML reporting system.

## Files
### common.xml

This XML file defines reusable calculations and database queries that are shared across multiple financial reports in OpenPetra. It provides SQL queries for retrieving financial data such as available years, account hierarchies, cost centers, and banking details. Key functionality includes selecting account children and descendants based on hierarchy codes, calculating variances between columns, determining percentages, and retrieving banking information for partners. Important calculations include 'Select Available Years', 'Select AccountChildren', 'Select AllAccountDescendants', 'Variance', 'Variance %', 'Account %', and 'Select Banking Details of partner'. The file serves as a central repository of common financial data retrieval logic to promote code reuse across the reporting system.

 **Code Landmarks**
- `Line 33`: Uses a function call 'getAllAccountDescendants' to retrieve account hierarchies dynamically
- `Line 42`: Implements conditional SQL generation with switch/case statements based on account hierarchy type
- `Line 174`: Uses custom functions for calculations like sub(), mul(), div() for financial formulas
- `Line 196`: Template-based query construction with parameter substitution for banking details
### reports.dtd

This DTD file defines the XML structure for OpenPetra's reporting system. It establishes a hierarchical schema for report definitions, including report parameters, headers, calculations, and multi-level data presentation. The schema supports conditional elements through switch/case statements, formatting options, and query definitions. Key elements include 'report' (the root element with unique ID), 'levels' (defining data hierarchy), 'calculations' (for data processing), and various formatting elements like 'field', 'value', and conditional structures. The DTD enables complex report layouts with header/footer sections, detail sections, and parameter-driven behavior.

 **Code Landmarks**
- `Line 4`: Defines a hierarchical report structure with multiple levels for complex data presentation
- `Line 71`: Implements conditional logic through switch/case/if elements for dynamic report content generation
- `Line 104`: Supports parameterized reports with options that can be conditionally displayed
- `Line 115`: Provides calculation elements with query capabilities for data processing
- `Line 137`: Enables detail reports with custom actions through the detailreport element
### template.xml

This XML template file serves as a blueprint for creating reports in OpenPetra. It defines the structure of a report including parameters, headers, calculations, and hierarchical data levels. The template includes placeholders for report identification, descriptions, query definitions, and formatting instructions. It demonstrates how to create calculations that return rows or columns of data, and how to structure multi-level reports with headers, details, and footers. The file shows the XML schema for defining report parameters, SQL queries, and data presentation in OpenPetra's reporting system.

 **Code Landmarks**
- `Line 2`: Uses a Document Type Definition (DTD) to validate the XML structure
- `Line 36`: Shows two calculation patterns - one for row data and one for column data
- `Line 55`: Implements hierarchical report levels for organizing and grouping data
- `Line 87`: Demonstrates conditional rendering with the 'condition' attribute
- `Line 91`: Shows field positioning with explicit measurements for precise layout control

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #