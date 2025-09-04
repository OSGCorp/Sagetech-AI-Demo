# XML Accounts Payable Section Of OpenPetra

The OpenPetra system is a comprehensive open-source application that helps non-profit organizations manage their administrative and financial operations. The XML Accounts Payable Section implements standardized report templates for accounts payable functionality, providing structured financial reporting capabilities. This sub-project delivers templated XML definitions that power OpenPetra's AP reporting engine, enabling consistent financial data presentation across the application.

This provides these capabilities to the OpenPetra program:

- Structured financial data presentation with hierarchical organization
- Standardized calculation queries for retrieving AP transaction data
- Flexible parameter-based filtering for date ranges and payment criteria
- Multi-currency support with appropriate currency-based grouping
- Aging analysis for outstanding supplier invoices

## Identified Design Elements

1. **Hierarchical Report Structure**: Reports implement nested levels (main, supplier, payment, document, detail) to organize financial data logically
2. **Parameterized Queries**: All reports include calculation sections that define database queries with configurable parameters
3. **Multi-Currency Support**: Reports handle both original and base currencies with appropriate conversion and display
4. **Aging Analysis**: Specialized templates for analyzing overdue payments with configurable aging periods
5. **Consistent Formatting**: Standardized headers, footers, and calculation sections across all report templates

## Overview
The architecture emphasizes a template-based approach to financial reporting, with XML definitions that separate the presentation logic from the application code. Each report template defines both the data retrieval logic (through calculation sections) and the presentation structure (through nested levels). This separation enables consistent reporting while allowing for customization of individual reports. The templates focus on accounts payable workflows including payment tracking, supplier aging analysis, and detailed transaction reporting.

## Business Functions

### Accounts Payable Reports
- `AP_accountdetail.xml` : XML report definition for Accounts Payable account detail reporting in OpenPetra's financial module.
- `AP_agedsupplierlist.xml` : XML report template for Accounts Payable aged supplier list showing outstanding invoices by supplier with aging periods.
- `AP_PaymentReport.xml` : XML report definition for Accounts Payable Payment Report showing documents comprising supplier payments.
- `AP_CurrentPayable.xml` : XML report definition for Accounts Payable showing outstanding documents due on a specific date.

## Files
### AP_CurrentPayable.xml

This XML report definition creates an Accounts Payable report showing documents that are outstanding or were outstanding on a particular date. It queries AP documents that either have no associated payment record or where the payment is dated after the selected parameter date. The report displays supplier information, document details, due dates, and amounts in both original and base currencies. Key calculations include SelectDocuments (main query), and various field calculations for displaying AP number, supplier information, document code, due date, currency, and amounts. The report includes multiple levels for organizing data presentation and calculates totals.

 **Code Landmarks**
- `Line 41`: SQL query uses LEFT JOIN to include documents without payments and filters by payment date parameter
- `Line 173`: Uses getSumLowerReport function to calculate totals across all documents in the report
### AP_PaymentReport.xml

This XML file defines the AP_PaymentReport for OpenPetra's finance module, displaying documents that make up payments to suppliers. The report includes multiple calculation queries that retrieve supplier information, payment details, and associated documents from the database. It implements a hierarchical structure with five levels: main, SupplierLevel, PaymentLevel, DocumentLevel, and DetailLevel. The report displays supplier information, payment numbers, dates, references, document codes, and detailed accounting entries. Key parameters include ledger number, payment number ranges, and date ranges, with filtering options for both date and payment number ranges.

 **Code Landmarks**
- `Line 48`: SQL query joins multiple tables to retrieve supplier information with conditional filtering based on user parameters
- `Line 74`: Hierarchical query structure where payment details are filtered by supplier key from parent query
- `Line 247`: Nested report levels create a hierarchical display of supplier > payment > document > detail information
- `Line 302`: Conditional formatting for credit notes to display negative amounts in parentheses
- `Line 358`: Detail level indentation with account codes and references provides granular transaction information
### AP_accountdetail.xml

This XML file defines the AP_accountdetail report structure for OpenPetra's financial reporting system. It implements a detailed accounts payable report that shows transaction details for specified accounts within a date range. The report includes calculations to query transaction data, account information, and cost centers, with functionality to calculate debits, credits, and balances at different levels. The report structure is organized in nested levels (main, AccountLevel, CostCentreLevel, DetailLevel) with appropriate headers, footers, and calculations for presenting financial data with proper formatting and totals.

 **Code Landmarks**
- `Line 71`: SQL query filters out system-generated year-end transactions with a specific narrative pattern
- `Line 146`: Uses conditional logic to track debits and credits separately while maintaining running totals at multiple levels
- `Line 232`: Implements sophisticated balance calculation with conditional logic to show net balances correctly based on whether debits or credits are larger
### AP_agedsupplierlist.xml

This XML report template defines the AP_agedsupplierlist report for OpenPetra's finance module. It generates a detailed breakdown of outstanding invoices by supplier, organized by currency. The report shows aging categories (overdue, due, 30+ days, 60+ days) and can filter for discounted invoices only. Key calculations include SelectCurrencies, SelectSupplier, SelectDetails, and SelectPaymentAmount queries that retrieve supplier data, invoice details, and payment information. The report structure includes multiple nested levels (main, CurrencyLevel, SupplierLevel, DetailLevel) with appropriate headers and footers for summarizing amounts. Parameters control the date ranges for aging periods and display options.

 **Code Landmarks**
- `Line 77`: Uses nested SQL queries with conditional elements based on report parameters
- `Line 152`: Implements complex date-based calculations to determine aging periods for invoices
- `Line 274`: Uses hierarchical report structure with multiple levels to organize data by currency and supplier

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #