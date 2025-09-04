# C# Reporting Module Of OpenPetra

The OpenPetra is a C# program that helps non-profit organizations manage administration and reduce operational costs. The program handles financial transactions and provides comprehensive reporting capabilities. This sub-project implements a modular reporting framework along with specialized financial, partner, and personnel reporting components. This provides these capabilities to the OpenPetra program:

- Dynamic HTML report generation with SQL query integration
- Multi-format output support (HTML, PDF, Excel)
- Domain-specific reporting functions for Finance, Partners, and Personnel
- GDPR-compliant data filtering based on consent requirements

## Identified Design Elements

1. **Template-Based Report Generation**: The module uses HTML templates with embedded SQL queries that are processed to generate dynamic reports
2. **Domain-Specific Calculators**: Specialized calculator classes handle different report types (TrialBalance, AccountDetail, PartnerByCity) through reflection-based instantiation
3. **Extensible Function Framework**: Domain-specific reporting functions are implemented in separate classes (TRptUserFunctionsFinance, TRptUserFunctionsPartner, etc.)
4. **Consent-Aware Data Processing**: Reports automatically filter sensitive data based on GDPR consent requirements

## Overview

The architecture follows a modular design with clear separation between report definition, data calculation, and output formatting. The HTMLTemplateProcessor handles template parsing and parameter substitution, while specialized calculator classes perform domain-specific data processing. Financial reporting includes trial balances, account details, and ledger status tracking. Partner reporting provides functionality for listing partners by location, subscription, or special types. The system supports asynchronous report generation with progress tracking and multiple output formats, making it both flexible and extensible for future reporting needs.

## Business Functions

### Report Generation Core
- `core/Calculator.cs` : Implements a calculator for generating HTML reports from templates in the OpenPetra reporting system.
- `connect/WebConnector.cs` : Server-side web connector for OpenPetra's reporting system that handles report generation, processing, and delivery.
- `FormatQuery.cs` : Formats SQL queries for reporting by handling parameter substitution and value conversion for OpenPetra's reporting system.
- `HTMLTemplateProcessor.cs` : HTML template processor for OpenPetra reporting that transforms templates with parameters into HTML and Excel outputs.

### Financial Reporting
- `MFinance/TrialBalance.cs` : Server-side implementation of the TrialBalance financial report generator for OpenPetra.
- `MFinance/Functions.cs` : Finance module reporting functions for OpenPetra that handle account calculations, budgets, and financial data retrieval.
- `MFinance/AccountDetail.cs` : Generates financial account detail reports with transaction data and balances for OpenPetra's reporting system.
- `MFinance/LedgerDate.cs` : Financial reporting utility class for handling ledger dates, periods, and fiscal years in OpenPetra.
- `MFinance/LedgerStatus.cs` : Provides financial period management and ledger status tracking for OpenPetra's financial reporting system.

### Partner Management Reporting
- `MPartner/Functions.cs` : Server-side functions for partner-related reports in OpenPetra, providing data access for reporting templates.
- `MPartner/PartnerByCity.cs` : Generates a Partner By City report by processing HTML templates with partner data filtered by city and consent requirements.
- `MPartner/PartnerBySubscription.cs` : Generates reports of partners based on subscription data with consent filtering.
- `MPartner/PartnerBySpecialTypes.cs` : Generates the Partner By Special Types report by processing HTML templates with partner data.
- `MPartner/Utils.cs` : Utility class for handling GDPR compliance in partner data reports by filtering out data without proper consent.
- `MPartner/AnnualReportWithoutAnnualReceiptRecipients.cs` : Generates annual reports for supporters who didn't receive annual receipts, filtering by consent requirements.

### Personnel Reporting
- `MPersonnel/Functions.cs` : Provides personnel-specific functions for OpenPetra reporting, including commitment periods, unit hierarchies, and personal information retrieval.

### Financial Development Reporting
- `MFinDev/Functions.cs` : Financial Development reporting functions for donor analysis in OpenPetra

## Files
### FormatQuery.cs

TRptFormatQuery implements a class that formats SQL queries for the OpenPetra reporting system. It manages SQL statements and their parameters, replacing placeholders with actual values from a parameter list. Key functionality includes adding text or values to queries, combining queries, replacing variables with ODBC parameters, and handling variant values that aren't SQL statements. The class supports different parameter patterns and maintains SQL statement integrity. Important methods include ReplaceVariables(), AddOdbcParameters(), Add(), and IsZeroOrNull(). Properties like SQLStmt, OdbcParameters, IsVariant, and VariantValue provide access to the formatted query components.

 **Code Landmarks**
- `Line 76`: Implements variant value handling to distinguish between SQL statements and direct values
- `Line 202`: Uses pattern matching with different bracket styles to handle various parameter formats
- `Line 247`: Maintains a counter to limit repeated warnings about missing variables
- `Line 315`: Special handling for DateTime values to strip time components for date-only comparisons
### HTMLTemplateProcessor.cs

HTMLTemplateProcessor implements functionality for processing HTML templates for reports in OpenPetra. It parses templates, replaces parameters with values, processes SQL queries embedded in templates, and evaluates conditional sections. Key features include parameter insertion with different formatting options, SQL query extraction, conditional processing with #if/#endif directives, and conversion of HTML tables to Excel spreadsheets. The class provides methods for adding parameters from data rows, setting individual parameters, and retrieving processed HTML. Important methods include InsertParameters(), ProcessIfDefs(), GetSQLQuery(), and static utility methods Table2Html() and HTMLToCalc() for report generation.

 **Code Landmarks**
- `Line 77`: Separates SQL queries from HTML templates using comment markers for later execution
- `Line 157`: Implements parameter replacement with multiple syntax options and formatting rules
- `Line 292`: Processes conditional sections with #if/#endif directives for dynamic content generation
- `Line 365`: Converts DataTable objects to HTML using templates with placeholder substitution
- `Line 425`: Transforms HTML documents to Excel workbooks with proper formatting for dates and currencies
### MFinDev/Functions.cs

TRptUserFunctionsFinDev implements specialized reporting functions for the Financial Development module in OpenPetra. It provides methods to analyze donor giving patterns and behaviors. Key functionality includes calculating gift statistics, identifying lapsed donors based on giving frequency, finding last gifts from donors, and generating top donor reports. The class supports filtering by date ranges, recipients, motivation codes, and currencies. Important methods include GetGiftStatistics(), IsLapsedDonor(), SelectLastGift(), IsTopDonor(), and MakeTopDonor(). These functions help organizations track donor behavior, identify giving patterns, and generate reports for fundraising analysis.

 **Code Landmarks**
- `Line 73`: Function selector pattern routes function calls based on string names, enabling dynamic report function invocation
- `Line 123`: GetGiftStatistics calculates donor regularity using percentage-based heuristics to identify consistent giving patterns
- `Line 213`: IsLapsedDonor implements sophisticated temporal analysis to identify donors who have stopped their regular giving pattern
- `Line 359`: GetTimeFrequency converts human-readable frequency descriptions into numerical time units for calculations
- `Line 483`: MakeTopDonor builds complex SQL queries with multiple optional filtering parameters for donor ranking reports
### MFinance/AccountDetail.cs

AccountDetail implements a financial reporting class that generates detailed account reports from HTML templates. It calculates account details by retrieving transaction and balance data from the database, then processes this information to create HTML reports. The class provides functionality to filter transactions by account/cost center combinations, calculate totals for credits and debits, and format the data according to templates. Key methods include Calculate() which processes the report definition and returns an HTML document, and CalculateData() which handles the detailed formatting of balance and transaction information.

 **Code Landmarks**
- `Line 72`: Intelligently skips account/cost center combinations with zero balance and no transactions to streamline reports
- `Line 101`: Uses HTML template processing with parameter substitution for flexible report formatting
- `Line 123`: Maintains running totals of credits and debits for financial summary calculations
### MFinance/Functions.cs

TRptUserFunctionsFinance implements financial reporting functions for OpenPetra's reporting system. It provides methods to retrieve and calculate financial data from the database, including account balances, transaction amounts, and budget figures. Key functionality includes retrieving actual values for specific periods, calculating year-to-date totals, handling different currencies (base, international, transaction), and working with account hierarchies. The class extends TRptUserFunctions and registers specialized finance functions through the FunctionSelector method. Important methods include GetActual, GetBudget, GetActualPeriods, GetAssetsMinusLiabs, and GetAllAccountDescendants. The class also handles currency conversions and supports various reporting formats like balance sheets and income statements.

 **Code Landmarks**
- `Line 73`: FunctionSelector method handles dynamic function dispatch based on string names, enabling flexible report generation
- `Line 173`: GetActualSummary recursively calculates summary account values by traversing account hierarchies
- `Line 215`: GetActual handles complex period calculations including forwarding periods and year transitions
- `Line 343`: GetBudgetSummary implements parallel logic to GetActualSummary for budget calculations
- `Line 429`: CalculateBudget handles cross-year budget calculations by splitting requests into same-year segments
### MFinance/LedgerDate.cs

TRptUserFunctionsDate extends TRptUserFunctions to provide financial date-related utilities for reporting in OpenPetra. The class handles period calculations, date conversions, and fiscal year operations from a ledger perspective. Key functionality includes retrieving period start/end dates, determining period status (closed/current/forward), generating quarter or period descriptions, and creating year captions. Important methods include GetPeriodDetails, GetEndDateOfPeriod, GetStartDateOfPeriod, GetQuarterOrPeriod, GetStatePeriod, and various date formatting utilities. The class works with the database to retrieve accounting period information and supports parameter-based operations.

 **Code Landmarks**
- `Line 56`: Function selector pattern used to dynamically dispatch method calls based on string function names
- `Line 83`: Methods support column parameters to handle multi-column reporting contexts
- `Line 169`: Date calculation accounts for fiscal year differences from calendar years
- `Line 257`: Side effects in functions that set parameters while returning formatted strings
- `Line 307`: Period status determination logic handles closed, current and forward periods
### MFinance/LedgerStatus.cs

This file implements classes for managing financial periods and ledger status in OpenPetra's reporting system. The LedgerStatus class maintains caches for GLM sequences and exchange rates. TFinancialPeriod handles period calculations across financial years, managing real periods/years and exchange rates. TGlmSequence represents general ledger master entries with account information, while TGlmSequenceCache provides methods to retrieve and cache GLM sequences from both database and memory. The code handles period adjustments, year transitions, and maintains information about account types, posting status, and exchange rates needed for financial reporting.

 **Code Landmarks**
- `Line 96`: MainConstructor handles complex period calculations including year transitions and forwarding periods
- `Line 142`: Exchange rate cache integration for currency conversion in financial reports
- `Line 369`: Negative sequence number generation for summary accounts without GLM records
- `Line 430`: Caching mechanism for GLM sequences improves performance by reducing database queries
### MFinance/TrialBalance.cs

TrialBalance implements a financial report generator that calculates and formats trial balance reports. It processes HTML templates with SQL queries to retrieve transaction and balance data from the database. The Calculate method accepts an HTML report definition, parameters, and a database transaction, then returns a populated HTML document. The CalculateData method processes account balances and their transactions, skipping zero-balance accounts with no transactions, and calculates total debits and credits. The class uses HTMLTemplateProcessor to handle parameter substitution and HTML manipulation.

 **Code Landmarks**
- `Line 71`: Intelligently skips account/cost center combinations with zero balance and no transactions to streamline report output
- `Line 98`: Uses HtmlAgilityPack for HTML DOM manipulation rather than string operations
- `Line 111`: Calculates running totals for credits and debits separately before inserting into the report template
### MPartner/AnnualReportWithoutAnnualReceiptRecipients.cs

AnnualReportWithoutAnnualReceiptRecipients implements a report generator for creating annual reports specifically for supporters who did not receive annual receipts. The class provides functionality to calculate and format HTML reports based on templates, filter recipients by consent requirements, and format partner names appropriately. Key methods include Calculate(), which processes the HTML template and retrieves recipient data, and CalculateData(), which populates the template with partner information. The implementation handles special formatting for family partners and ensures empty email columns are properly displayed in the final HTML output.

 **Code Landmarks**
- `Line 54`: Filters recipients based on consent requirements using a helper method
- `Line 67`: Special handling for FAMILY partner class with reverse shortname formatting
- `Line 80`: HTML manipulation to fix empty email column display issues
### MPartner/Functions.cs

Functions.cs implements reporting-specific user functions for the Partner module in OpenPetra. The file contains a TRptUserFunctionsPartner class that extends TRptUserFunctions to provide specialized data access methods for partner reports. Key functionality includes retrieving partner addresses, contact details, subscription information, and generating statistical reports about publications. The class implements methods for formatting address strings, checking account numbers, retrieving partner types, and calculating publication statistics across counties. Notable functions include GetPartnerBestAddress, GetPartnerContactDetails, GetSubscriptions, and methods for the publication statistical report that calculate distributions across geographic regions.

 **Code Landmarks**
- `Line 71`: The entire file is wrapped in a #if disabled directive, suggesting this code is currently disabled or being maintained for reference
- `Line 106`: FunctionSelector method provides a central dispatch mechanism for all report functions, making the class extensible
- `Line 1046`: Publication statistical report calculation uses complex SQL queries with parameter substitution for security
- `Line 1220`: The GetPartnerBestAddressRow static method demonstrates the pattern used for address selection throughout the system
- `Line 1297`: Contact details retrieval demonstrates integration with the partner data aggregation system
### MPartner/PartnerByCity.cs

PartnerByCity implements a report generator for listing partners by city in OpenPetra. It processes HTML templates with partner data retrieved from the database based on city parameters and consent requirements. The class provides functionality to calculate the report, filter partners by consent, and generate HTML output. The main method Calculate() accepts an HTML report definition, parameter list, and database transaction to produce a formatted report. The private CalculateData() method populates the HTML template with partner information from the database results.

 **Code Landmarks**
- `Line 47`: Validates city parameter by checking if it's not empty after removing wildcards
- `Line 60`: Filters partner data based on consent requirements using Utils.PartnerRemoveUnconsentReportData
- `Line 77`: Uses HTML template processing with node cloning to generate report rows for each partner
### MPartner/PartnerBySpecialTypes.cs

PartnerBySpecialTypes implements a report generator for the Partner By Special Types report in OpenPetra. It processes HTML templates with partner data retrieved from the database. The Calculate method takes an HTML report definition, parameter list, and database transaction to generate the report. It retrieves partners using a SQL query from the template, applies consent filtering, and processes the data. The CalculateData method populates the HTML template with partner information by cloning a template node for each partner row and replacing parameter placeholders with actual values.

 **Code Landmarks**
- `Line 58`: Filters partner data based on consent requirements, ensuring privacy compliance
- `Line 69`: Uses HTML template processing for report generation rather than hardcoded output
- `Line 84`: Dynamically creates HTML elements by cloning templates and replacing parameters
### MPartner/PartnerBySubscription.cs

PartnerBySubscription implements a server-side reporting class that generates HTML reports listing partners with specific subscriptions. The class provides a Calculate method that processes an HTML template with embedded SQL queries to retrieve subscription recipients, filters them based on consent requirements, and generates a formatted HTML document. The implementation handles special formatting for family partners and ensures proper consent validation before including partners in the report. Key functionality includes HTML template processing, SQL query execution, consent filtering, and dynamic HTML generation with partner data.

 **Code Landmarks**
- `Line 55`: Uses HTMLTemplateProcessor to extract SQL queries embedded in HTML templates
- `Line 60`: Implements consent filtering to ensure GDPR compliance for subscription data
- `Line 77`: Special handling for FAMILY class partners with different name formatting requirements
- `Line 89`: Dynamic HTML generation by cloning template nodes and populating with partner data
### MPartner/Utils.cs

Utils implements a utility class for GDPR compliance in OpenPetra's reporting system. The main functionality is the PartnerRemoveUnconsentReportData method which filters partner data in reports based on consent permissions. It examines each row in a partner data table and checks if the partner has given consent for specific data types (address, phone numbers, email) to be included in reports. If consent is missing for the specified purpose code, the sensitive data is replaced with "NO_CONSENT" or empty strings. The method uses TDataHistoryWebConnector to retrieve the latest consent records for each partner.

 **Code Landmarks**
- `Line 51`: The code explicitly notes that this implementation could be significantly simplified with a single SQL query but uses existing functions for now.
- `Line 57`: The implementation checks multiple address-related columns individually rather than using a more generic approach.
- `Line 104`: The consent checking pattern is repeated for each data type (address, phone, email) with similar logic.
### MPersonnel/Functions.cs

TRptUserFunctionsPersonnel implements specialized reporting functions for the Personnel module in OpenPetra. It extends TRptUserFunctions to provide personnel-specific data retrieval capabilities for report generation. Key functionality includes retrieving commitment periods, unit hierarchies, passport details, special needs information, and personal data. The class handles queries for site names, partner types, missing application information, languages, church details, and age calculations. Important methods include GetCurrentCommitmentPeriod, GenerateUnitHierarchy, GetPassport, GetNationalities, and GetMissingInfo. The class maintains cached special needs data and provides helper methods for formatting unit keys and retrieving partner information.

 **Code Landmarks**
- `Line 73`: FunctionSelector method routes function calls to appropriate handlers based on string name matching
- `Line 186`: GetCurrentCommitmentPeriod uses complex SQL logic to find the most relevant commitment period for a person at a given date
- `Line 282`: GetType method supports both exact and prefix matching of partner types with configurable type lists
- `Line 302`: GenerateUnitHierarchy implements recursive traversal of organizational unit structures with optional filtering
- `Line 663`: Static GetLatestPassport method provides reusable passport retrieval logic with prioritization of main passports
### connect/WebConnector.cs

TReportGeneratorWebConnector implements server-side functionality for OpenPetra's reporting system. It manages the entire reporting workflow including report creation, calculation, storage, and delivery. Key functionality includes asynchronous report generation, progress tracking, result storage in the database, and multiple output formats (HTML, PDF, Excel). The class provides methods for creating report IDs, starting report calculations, retrieving results, downloading in various formats, and sending reports via email. Important methods include Create(), Start(), GetProgress(), DownloadHTML/PDF/Excel(), and SendEmail(). The connector uses a database table to store report results and supports parameter management through TParameterList.

 **Code Landmarks**
- `Line 76`: Uses a thread-based approach to run reports asynchronously while providing progress tracking
- `Line 181`: Implements database cleanup by automatically removing expired report results
- `Line 235`: Uses a parameter list serialized to JSON for storing report configuration
- `Line 307`: Converts HTML reports to Excel using NPOI and HTMLTemplateProcessor
- `Line 315`: Converts HTML reports to PDF using Html2Pdf utility
### core/Calculator.cs

TRptDataCalculator implements a class that generates HTML reports by calculating data based on report definitions. It loads HTML templates from standard or custom report paths, processes parameters, and invokes specialized calculator classes through reflection to perform the actual calculations. Key functionality includes loading report definition files, executing calculation classes dynamically, and generating HTML output. Important methods include GenerateResult(), CalculateFromClass(), and LoadReportDefinitionFile(). The class maintains paths to standard and custom reports, parameters, and HTML templates and output documents.

 **Code Landmarks**
- `Line 94`: Uses reflection to dynamically load and invoke report calculation classes based on namespace and class name parameters
- `Line 150`: Implements a caching mechanism for loaded assemblies to improve performance when processing multiple reports
- `Line 46`: Uses HtmlAgilityPack for HTML document manipulation instead of string-based processing

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #