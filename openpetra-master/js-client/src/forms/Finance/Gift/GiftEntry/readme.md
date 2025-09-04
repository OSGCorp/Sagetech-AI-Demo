# JavaScript Client Gift Entry

The JavaScript Client Gift Entry is a critical sub-project of OpenPetra that implements the financial transaction management capabilities of the system. This module provides a modern web-based interface for handling gift entry, bank imports, and recurring gift management within the broader non-profit administration platform.

The sub-project is structured around three primary functional areas:

- **Gift Batch Management**: Handles creation, editing, and posting of gift batches with associated transactions
- **Recurring Gift Processing**: Manages SEPA direct debit and recurring donation workflows
- **Bank Statement Import**: Provides functionality to import and transform bank transactions from various formats (CSV, CAMT, MT940)

## Identified Design Elements

1. **Modular Template Architecture**: Each functional area has dedicated HTML templates and JavaScript controllers that follow consistent patterns
2. **Client-Server Communication**: JavaScript controllers manage API interactions with the server for data retrieval and persistence
3. **Rich UI Components**: Implements autocomplete for donors, motivation codes, accounts, and cost centers
4. **Transaction Transformation**: Logic for converting bank imports to gifts or general ledger entries
5. **Export Capabilities**: Supports exporting financial data in various formats including Excel and SEPA

## Technical Architecture

The architecture follows a template-controller pattern where HTML files define the UI structure while JavaScript files implement the business logic and server interactions. The code leverages Bootstrap for styling and includes specialized components for financial data handling. The module integrates with OpenPetra's broader authentication and data management systems while maintaining a focused responsibility on gift entry workflows.

## Business Functions

### Gift Entry Management
- `BankImport.html` : HTML template for bank import functionality in the Gift Entry module, allowing users to import and manage bank transactions.
- `BankImport.js` : Client-side JavaScript for bank statement import and transaction management in OpenPetra's finance module.

### Gift Batch Processing
- `GiftBatches.html` : HTML template for managing gift batches in OpenPetra's financial module, including batch creation, editing, and transaction management.
- `GiftBatches.js` : JavaScript component for managing gift batches in OpenPetra's finance module

### Recurring Gift Management
- `RecurringGiftBatches.html` : HTML template for managing recurring gift batches in OpenPetra's finance module with SEPA direct debit support.
- `RecurringGiftBatches.js` : JavaScript module for managing recurring gift batches in OpenPetra's finance system.

## Files
### BankImport.html

This HTML file defines the user interface for importing and managing bank transactions in the Gift Entry module. It provides a structured layout for displaying bank statement entries, editing transaction details, and processing them as gifts or general ledger entries. The file includes templates for transaction rows, detail rows, and modal dialogs for editing transactions and their details. Key functionality includes importing bank statements in various formats (CSV, CAMT, MT940), filtering transactions by status, transforming transactions to gifts or GL entries, and checking for sponsorships. The interface supports autocomplete for donors, motivation codes, accounts, and cost centers.

 **Code Landmarks**
- `Line 78`: Dynamic requirement class system that shows/hides UI elements based on transaction match type (UNMATCHED, MATCHED-GIFT, MATCHED-GL, DONT-MATCH)
- `Line 322`: Multi-format import support with dropdown menu for CSV, CAMT, and MT940 bank statement formats
- `Line 334`: Configurable CSV import settings with options for number format, date format, separators and encoding
- `Line 469`: Transformation buttons to convert bank transactions to either gift or general ledger entries
### BankImport.js

This file implements the client-side functionality for importing bank statements and managing transactions in OpenPetra's Gift Entry module. It handles importing bank statements from CSV, CAMT, and MT940 formats, displaying transaction lists, editing transaction details, and processing transactions into gifts or general ledger entries. Key functions include display_dropdownlist(), import_csv_file(), edit_gift_trans(), save_edit_trans(), transform_to_gift(), and transform_to_gl(). The code manages donor associations, transaction details, and provides interfaces for viewing and modifying imported bank data.

 **Code Landmarks**
- `Line 36`: Dynamic loading of bank statements with support for multiple file formats (CSV, CAMT, MT940)
- `Line 215`: Modal-based transaction editing with dynamic form updates based on transaction type
- `Line 261`: Conditional UI display for membership fees based on motivation group selection
- `Line 403`: Support for importing CAMT files from ZIP archives
- `Line 496`: Integration with sponsorship system to check incoming donations
### GiftBatches.html

This HTML template implements the user interface for managing gift batches in OpenPetra's Finance module. It provides functionality for viewing, creating, editing, and posting gift batches and their associated transactions. The file contains multiple template sections for displaying batch lists, transaction details, and modal dialogs for editing batches, transactions, and transaction details. Key features include batch status management, transaction entry, motivation code selection, donor selection via autocomplete, and batch posting functionality. The interface includes filtering options by year, period, and batch status. The template relies on several JavaScript libraries for autocomplete functionality, utilities, templating, and modal dialog management.

 **Code Landmarks**
- `Line 3`: Uses phantom hidden div as a template storage mechanism for dynamic content generation
- `Line 29`: Conditional display of buttons based on batch posting status using CSS classes
- `Line 185`: Implements autocomplete functionality for donor selection with key-name attribute for storing IDs
- `Line 247`: Dynamic requirement of form fields based on motivation type using requireClass attribute
- `Line 319`: Filter panel with collapsible interface for searching batches by year, period and status
### GiftBatches.js

This file implements the gift batch management interface in OpenPetra's finance module. It provides functionality to view, create, edit, and manage gift batches and their transactions. Key features include loading gift batches for specific periods, displaying transaction details, creating new batches/transactions, editing existing entries, posting batches, canceling batches, and exporting batch data. The file handles API communication with the server, form data extraction, and UI updates. Important functions include display_list(), display_batch(), new_batch(), edit_batch(), save_edit_batch(), post_batch(), and export_batch().

 **Code Landmarks**
- `Line 26`: Document ready function loads available years and preset data for the gift batch interface
- `Line 91`: The display_batch function supports direct navigation to specific gift transactions via URL parameters
- `Line 330`: Gift transaction detail editing includes special handling for membership fees with conditional UI elements
- `Line 429`: Save functions implement different behaviors based on whether the operation is create or edit mode
- `Line 521`: Batch export functionality generates Excel files and triggers browser download
### RecurringGiftBatches.html

This HTML template implements the user interface for managing recurring gift batches in OpenPetra's finance module. It provides functionality for creating, editing, and submitting batches of recurring gifts with SEPA direct debit support. The template includes modals for editing batches, gift transactions, and transaction details, with fields for donor information, banking details, SEPA mandates, and gift motivations. Key components include batch listing with collapsible transaction details, buttons for batch operations (open, edit, submit), and transaction management tools. The file integrates with autocomplete functionality for donors, bank accounts, cost centers, and motivation codes.

 **Code Landmarks**
- `Line 37`: SEPA direct debit functionality with date selection and batch submission options
- `Line 173`: Donor selection with autocomplete and integrated contact editing button
- `Line 186`: Dynamic bank account selection with refresh capability for SEPA transactions
- `Line 301`: Conditional display for membership fee specific fields based on motivation code
### RecurringGiftBatches.js

This file implements the recurring gift batch management interface in OpenPetra's finance system. It provides functionality to display, create, edit, and submit recurring gift batches and their associated transactions. Key features include loading batch data from the server, managing gift transactions within batches, editing transaction details, handling donor banking information, and exporting batch data in Excel or SEPA formats. Important functions include display_list(), new_batch(), edit_batch(), save_edit_batch(), submit_batch(), and export_batch(). The file also handles recurring gift transaction details with functions like edit_gift_trans() and save_edit_trans_detail().

 **Code Landmarks**
- `Line 31`: Uses API calls to load recurring gift batch data from the server with proper ledger context
- `Line 143`: Implements dynamic date calculation for gift submission dates based on current date
- `Line 269`: Handles SEPA mandate reference generation using donor key and date
- `Line 356`: Maintains recurring gift details with special handling for membership-related fields
- `Line 444`: Implements SEPA file export functionality for recurring gift batches

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #