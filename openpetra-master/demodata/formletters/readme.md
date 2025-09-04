# Demo Form Letters Subproject Of Petra

The Demo Form Letters subproject provides templating functionality for generating standardized financial and donor communications within OpenPetra. Implemented as HTML templates with dynamic data binding capabilities, this subsystem enables the generation of professional financial documents and donor communications. The templates use a placeholder system (prefixed with #) that gets replaced with actual data during document generation, allowing for personalized yet consistent outputs across different document types.

## Key Technical Features

- Standardized Document Generation: Creates consistent financial and donor communications
- Dynamic Data Binding: Uses placeholder variables for runtime data insertion
- Multi-format Support: Templates designed for both print and digital distribution
- Localization Support: Region-specific templates (e.g., en-GB variants) for international compliance
- Conditional Content Sections: Templates can include or exclude sections based on document type

## Identified Design Elements

1. **Template Inheritance System**: Base templates can be extended for specific document types while maintaining consistent styling and structure
2. **Responsive Layout Design**: Documents adapt to different output formats while preserving readability
3. **Financial Data Formatting**: Specialized handling for currency values, dates, and financial identifiers
4. **Multi-page Support**: Page break controls for proper document pagination in both digital and print formats
5. **Regulatory Compliance**: Templates include required legal disclaimers and tax information formatting

## Architecture Overview

The templates are organized by functional domain (Finance Management, Reports, Gift Receipts) and follow HTML standards with embedded styling. The architecture emphasizes separation between presentation and data, allowing the business logic to focus on data preparation while the templates handle formatting and layout. This design supports easy maintenance and extension of document types without requiring changes to the underlying application code.

## Business Functions

### Accounts Payable Documentation
- `ApRemittanceAdvice.html` : HTML template for accounts payable remittance advice showing payment details to suppliers.

### Donation Receipt Templates
- `GiftReceipt.html` : HTML template for gift receipt letters showing donation details to donors.
- `AnnualReceipt2.html` : HTML template for annual donation receipt with placeholders for donor information and donation details.
- `AnnualReceipt.html` : HTML template for generating annual donation receipts and thank-you letters for a charity organization.
- `GiftReceipt.en-GB.html` : HTML template for generating gift receipts in English (UK) for OpenPetra's donor communication system.

## Files
### AnnualReceipt.html

This HTML template creates annual donation receipts and thank-you letters for a charity organization. It includes conditional sections for different document types (receipts or thank-you letters), styling for proper formatting, and placeholders for donor information that will be populated dynamically. The document features recipient and organization address sections, donation details including amounts and dates, legal disclaimers required for tax purposes, and an optional consent form for future communications. The template includes proper page breaks for multi-page documents and responsive styling for different document elements.

 **Code Landmarks**
- `Line 98`: Uses conditional templating (#if RECEIPT/#if THANKYOU) to generate different document types from the same template
- `Line 191`: Implements dynamic content insertion with #VARIABLE placeholders that will be replaced during document generation
- `Line 235`: Contains conditional section for GDPR consent form that only appears when UNDEFINEDCONSENT flag is set
### AnnualReceipt2.html

This HTML template file creates an annual donation receipt document for a charity organization. It defines the layout for displaying donor information, donation details, and organization contact information. The template includes placeholders (prefixed with #) that will be replaced with actual data during processing, such as donor name, address, donation amounts, dates, and tax-deductible information. The document is structured using HTML tables for layout control and includes styling for page breaks to support multi-page receipts.

 **Code Landmarks**
- `Line 19`: Uses HTML table structure for precise positioning of content elements rather than modern CSS layout techniques
- `Line 32`: Implements placeholders with # prefix for dynamic content insertion
- `Line 7`: Simple CSS class for page breaks to support multi-page document generation
- `Line 74`: Structured table for financial data with columns for date, amounts, and categorization of donations
### ApRemittanceAdvice.html

This HTML template creates a standardized remittance advice document for accounts payable in OpenPetra. It displays supplier information, payment references, dates, and itemizes invoices being paid. The template uses placeholders prefixed with # that get replaced with actual data during document generation. It includes basic styling for fonts and table formatting, with a detail section that likely repeats for multiple invoices. The document clearly shows the total payment amount and organizes information in a professional layout suitable for sending to suppliers.

 **Code Landmarks**
- `Line 37`: Uses a <detail> tag which is likely a custom element for template processing that generates rows for each invoice in the payment
- `Line 19`: Uses non-breaking space entity (&#160;) to create vertical spacing in the document rather than CSS margins
- `Line 21`: Template uses #-prefixed placeholders that will be replaced with dynamic data during document generation
### GiftReceipt.en-GB.html

This HTML template file serves as a layout for gift receipts in OpenPetra's donor communication system. It contains placeholders that get replaced with actual donor and gift data when generating receipts. The template includes sections for the recipient's address, current date, personalized greeting, and a tabular breakdown of gift details including date, amount, recipient organization, purpose, and reference number. The layout is simple and functional, designed for formal donor acknowledgment communications.

 **Code Landmarks**
- `Line 10-12`: Uses placeholder variables with # prefix that get replaced with actual donor data during template processing
- `Line 22-30`: Structured table layout for displaying gift details with specific fields that match OpenPetra's gift data model
### GiftReceipt.html

This HTML template file creates gift receipt letters for donors. It displays the recipient's address, current date, and a personalized greeting. The main content is a table showing gift details including date given, amount, recipient organization, purpose of the donation, and reference number. The template uses placeholder variables (prefixed with #) that get replaced with actual donor and gift data when the letter is generated.

 **Code Landmarks**
- `Line 20`: Uses a <detail> tag which is non-standard HTML, likely a custom element for template processing
- `Line 11-13`: Uses #-prefixed placeholder variables that will be replaced with actual donor data during processing

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #