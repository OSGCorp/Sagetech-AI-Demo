# C# Sample Data Constructor Of OpenPetra

The C# Sample Data Constructor is a specialized testing and demonstration tool within the OpenPetra ecosystem that generates comprehensive sample data for development, testing, and demonstration purposes. This subproject implements automated data generation for all major functional areas of OpenPetra, creating realistic test environments without manual data entry. The constructor creates interconnected partner, financial, and operational records that reflect real-world usage patterns of the OpenPetra system.

This provides these capabilities to the OpenPetra program:

- Automated generation of complete test datasets from CSV templates
- Creation of realistic partner hierarchies including families, organizations, banks, and field units
- Population of financial systems with ledgers, transactions, gift batches, and invoices
- Establishment of proper relational connections between all generated entities
  - Partner relationships and hierarchies
  - Financial transactions linked to appropriate accounts and partners

## Identified Design Elements

1. **Modular Data Generation**: Each functional area has dedicated generator classes that handle specific entity types while maintaining referential integrity
2. **CSV-Based Input**: Uses Benerator-created CSV files as source data, allowing for controlled randomization and realistic data patterns
3. **Transactional Processing**: Ensures database consistency by using OpenPetra's existing data access layer and transaction management
4. **Progressive Data Building**: Constructs data in a logical sequence where dependent entities are created after their prerequisites

## Overview
The architecture follows a command-line interface pattern where the main Program class orchestrates the generation process by invoking specialized generator classes for each data domain. These generators (Workers, Donors, Organizations, Banks, Ledgers, GiftBatches, AccountsPayable) create appropriate database records through OpenPetra's standard data access interfaces, ensuring that all business rules and data integrity constraints are respected. The system is designed for repeatability and can generate consistent datasets for testing while supporting variable parameters for different testing scenarios.

## Business Functions

### Partner Management
- `GenerateDonors.cs` : Generates sample donor data for OpenPetra with family partners and consent records.
- `GenerateUnitPartners.cs` : Generates sample unit partners (fields and key ministries) for OpenPetra's demonstration data.
- `GenerateOrganisations.cs` : Generates sample organization partner records for OpenPetra from Benerator-created data files.

### Financial Management
- `GenerateLedger.cs` : Generates sample financial data for OpenPetra ledgers including gifts, invoices, exchange rates, and period closings.
- `GenerateAccountsPayable.cs` : Generates sample accounts payable data for OpenPetra including invoices, posting, and payment processing.
- `GenerateGiftBatches.cs` : Generates and posts sample gift batches for OpenPetra's financial system using data from Benerator files.

### Banking
- `GenerateBanks.cs` : Generates bank partner data for OpenPetra from CSV files, creating bank records with branch information and partner entries.

### Personnel
- `GenerateWorkers.cs` : Generates sample worker data for OpenPetra with family records, personal information, addresses, commitments, and banking details.

### Core System
- `Program.cs` : Tool for generating sample data for OpenPetra by importing partners, organizations, and ledger information.

## Files
### GenerateAccountsPayable.cs

SampleDataAccountsPayable implements functionality for generating sample accounts payable data for testing and demonstration purposes in OpenPetra. It provides methods to create invoices from Benerator-generated CSV files and to post and pay those invoices. The GenerateInvoices method creates invoice records with appropriate supplier details, expense accounts, and financial data. The PostAndPayInvoices method processes invoices for a specific period, optionally leaving some unposted. Important variables include FLedgerNumber and MaxInvoicesPerYear, which control the ledger used and limit the number of generated invoices.

 **Code Landmarks**
- `Line 63`: Uses CSV to XML conversion to process external data files for invoice generation
- `Line 77`: Dynamically selects suppliers and expense accounts from the database to create realistic sample data
- `Line 103`: Uses sequence generator to ensure unique document IDs for invoices
- `Line 173`: Leverages transaction isolation levels for data consistency during operations
### GenerateBanks.cs

SampleDataBankPartners implements functionality to generate bank partner data for OpenPetra from CSV files. The GenerateBanks method reads bank information from a CSV file, converts it to XML, and creates bank records with corresponding partner entries in the database. For each bank, it creates a PBankRow with branch name, branch code, and BIC, a PPartnerRow with partner class set to bank, and an empty PPartnerLocationRow to ensure the partner can be found in search screens. The method reserves partner keys in batches of 100 for efficiency and logs progress during creation.

 **Code Landmarks**
- `Line 56`: Uses TCsv2Xml utility to convert CSV data to XML format for processing
- `Line 62`: Implements batch reservation of partner keys (100 at a time) for performance optimization
- `Line 104`: Creates empty location records to ensure partners appear in search screens
### GenerateDonors.cs

SampleDataDonors class implements functionality to generate sample family partner data for OpenPetra from a Benerator-created CSV file. It creates different types of family records (single men, single women, and families with children) with associated partner data, addresses, and bank details. The class handles the creation of partner records in the database with appropriate relationship structures and consent records for communications. The main method GenerateFamilyPartners processes the input file, creates the necessary data structures, and saves them to the database through the PartnerEditTDSAccess interface.

 **Code Landmarks**
- `Line 76`: Uses XML parsing to process CSV data that was converted to XML format
- `Line 147`: Strategically clears person records for family partners to maintain proper data relationships
- `Line 196`: Implements consent management with JSON serialization for GDPR compliance
### GenerateGiftBatches.cs

SampleDataGiftBatches provides functionality for generating and posting gift batches with sample data in OpenPetra. It loads gift data from Benerator-generated text files, organizes gifts by date, creates gift batches for specific accounting periods, and supports posting these batches to the general ledger. The class handles various gift types including support for workers, field donations, and key ministry contributions. Key methods include LoadBatches(), CreateGiftBatches(), PostBatches(), and private helper methods SortGiftsByDate() and CreateGiftBatches(). The class maintains important variables like FLedgerNumber and MaxGiftsPerBatch to control the generation process.

 **Code Landmarks**
- `Line 63`: Organizes gifts by date in a SortedList for efficient batch creation by accounting period
- `Line 154`: Uses database transactions to safely retrieve partner data for gift creation
- `Line 206`: Handles different gift types (worker support, field donations, key ministries) with specific business logic
- `Line 308`: Implements transaction management pattern with delegate for safe database operations
### GenerateLedger.cs

SampleDataLedger implements functionality to create and populate ledgers with sample financial data for testing purposes. It handles ledger creation, updating settings, initializing exchange rates, and populating data with gift batches and invoices. The class works with accounting periods, posting transactions, and performing month/year-end operations. Key methods include CreateNewLedger(), UpdateLedger(), InitExchangeRate(), and PopulateData(). Important variables include FLedgerNumber (default 43), FNumberOfClosedPeriods, and FNumberOfFwdPostingPeriods which control the ledger configuration and data generation process.

 **Code Landmarks**
- `Line 69`: Configurable ledger parameters allow flexible test data generation with adjustable periods
- `Line 95`: Creates a complete ledger with standardized settings including currency and fiscal year configuration
- `Line 176`: Systematically populates ledger with transactions, posts batches, and performs period/year closings to simulate real accounting cycles
### GenerateOrganisations.cs

SampleDataOrganisations implements functionality to generate sample organization partner records for OpenPetra from Benerator-created CSV files. It reads organization data, creates partner records with appropriate keys, and stores them in the database. The class handles creating organization records, partner records, location information, and supplier details when applicable. Key functionality includes parsing CSV files, reserving partner keys, and submitting changes to the database. The main method GenerateOrganisationPartners processes the input file and creates all necessary records with appropriate relationships.

 **Code Landmarks**
- `Line 60`: Uses a partner key reservation system to obtain blocks of 100 keys at a time for efficiency
- `Line 75`: Creates multiple related database records (POrganisation, PPartner, PLocation) for each organization
- `Line 111`: Conditionally creates supplier records based on a flag in the input data
### GenerateUnitPartners.cs

SampleDataUnitPartners implements functionality to generate sample unit partners for OpenPetra's demonstration data. It provides two main methods: GenerateFields creates field units based on country data from a CSV file, establishing partner records, unit hierarchies, cost centers, and ledger connections; GenerateKeyMinistries creates key ministry units linked to field units. The class handles partner key generation, data structure creation, and database submissions. Important elements include FLedgerNumber (static variable), GenerateFields and GenerateKeyMinistries methods, and the use of PartnerImportExportTDS and GLSetupTDS datasets for data manipulation.

 **Code Landmarks**
- `Line 59`: Creates unit partners with IDs derived from country data with systematic partner key generation (id * 1000000)
- `Line 103`: Establishes financial structures by creating cost centers and valid ledger numbers for each field unit
- `Line 139`: Uses database transaction to retrieve field partner keys but rolls back immediately after reading
### GenerateWorkers.cs

SampleDataWorkers implements functionality to generate sample worker data for OpenPetra from a Benerator CSV file. It creates family and person partner records with appropriate relationships, addresses, commitment data for workers in fields, and banking details. The class handles different family situations (single men, single women, families with children) and creates all necessary database records. Key methods include GenerateWorkers (main entry point), GenerateFamilyRecord, GeneratePersonRecord, GenerateAddressForFamily, GenerateCommitmentRecord, and GenerateBankDetails. The class manages partner keys, location records, and personnel data.

 **Code Landmarks**
- `Line 58`: Main entry point that processes a Benerator CSV file and generates worker data with database transactions
- `Line 96`: Family generation logic handles different family situations (single, married, with children) based on input data
- `Line 175`: Partner key reservation system that efficiently manages blocks of keys to minimize database access
- `Line 254`: Family relationship management that updates marital status and addressee types based on family composition
- `Line 373`: Worker commitment records generation with appropriate status codes based on commitment length
### Program.cs

TSampleDataConstructor implements a command-line tool that creates and imports sample data into OpenPetra. It connects to the OpenPetra server and processes raw data files generated by benerator to create partners, organizations, banks, workers, and ledger entries. The program supports different operations including importing partners, importing recipients, creating ledgers for one year or multiple years, and creating a second ledger. Key functionality includes partner generation, organization creation, recipient importing, and ledger setup with appropriate accounting periods. Important methods include Main(), CalculatedNumberOfClosedPeriods(), and various calls to helper classes like SampleDataBankPartners, SampleDataDonors, and SampleDataLedger.

 **Code Landmarks**
- `Line 76`: Uses a bitwise flag system (eOperations enum) to control which data generation operations to perform
- `Line 94`: Connects to the OpenPetra server using TPetraServerConnector without requiring explicit connection parameters
- `Line 153`: Calculates closed accounting periods dynamically to ensure demo databases always have an open period
- `Line 166`: Creates multiple ledgers with different start dates to demonstrate multi-ledger functionality

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #