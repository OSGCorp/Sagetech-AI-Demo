# C# Input/Output Of OpenPetra

The OpenPetra C# Input/Output subproject implements essential data processing and external system integration capabilities. This cross-cutting component provides standardized interfaces for file operations, data conversion, communication protocols, and security features. The subproject enables OpenPetra's core functionality through robust I/O operations that support the nonprofit management features of the application.

Key capabilities provided to the OpenPetra program:

- File Operations: Reading, writing, compressing, and processing various file formats (text, XML, YAML, CSV)
- Data Conversion: Transforming between formats (XML/YAML/CSV/Excel) while preserving structure and types
- External System Integration: Email (SMTP), HTTP communication, Excel automation, SEPA financial services
- Security: Encryption services using Rijndael algorithm for sensitive data protection
- Template Processing: Text template handling with placeholders, snippets, and conditional sections

## Identified Design Elements

1. **Format Conversion Framework**: Multiple specialized parsers (XML, YAML, CSV, OpenDocument) with consistent interfaces for transforming between data representations
2. **Compression Utilities**: Unified API for handling various compression formats (ZIP, TAR, GZip, 7Zip) with streaming capabilities
3. **External Protocol Adapters**: Abstraction layers for SMTP, HTTP, and financial protocols that shield application code from implementation details
4. **Text Processing Pipeline**: Sophisticated encoding detection, line ending normalization, and template processing for consistent text handling
5. **Security Infrastructure**: Encryption services with key management for protecting sensitive information

## Overview

The architecture emphasizes modularity through specialized components that handle specific I/O concerns while maintaining consistent interfaces. The design separates low-level file operations from higher-level data transformation and external system integration. Error handling is robust throughout the codebase, with specialized exceptions for different failure scenarios. The subproject balances performance considerations (streaming large files) with developer ergonomics (intuitive APIs) to support both application functionality and maintainability.

## Business Functions

### File Operations
- `TextFile.cs` : Utility class for text file operations including file comparison, encoding detection, and line ending conversions.
- `PackTools.cs` : Utility class for file compression and extraction operations using SharpZipLib library in OpenPetra.
- `FileHelper.cs` : Utility class providing file operations including compression, stream handling, backup, and file format conversion for OpenPetra.
- `TextFileEncoding.cs` : Manages text file character encoding support for OpenPetra with Unicode and ANSI code pages.

### Data Processing
- `SimpleYmlParser.cs` : A simple YAML parser for efficiently processing database files saved in YML format without loading the entire document at once.
- `Csv2Xml.cs` : Utility class for converting between CSV, XML, and Excel formats with support for data transformation and hierarchical structures.
- `XmlParser.cs` : XML parsing utility class that simplifies reading and navigating XML documents with validation support.
- `Yml2Xml.cs` : YAML to XML converter for OpenPetra that supports simple YAML syntax and hierarchical document merging

### Financial Services
- `SEPAWriter.cs` : Implements SEPA Direct Debit file generation for electronic payment processing in XML format.

### Communication
- `SmtpEmail.cs` : Email sending functionality for OpenPetra using SMTP protocol
- `HTTPUtils.cs` : Utility class for HTTP operations including session management, web requests, and file downloads for OpenPetra.

### Integration
- `Excel.cs` : Excel integration utility for OpenPetra that provides programmatic access to Microsoft Excel for data manipulation and chart creation.
- `OpenDocumentParser.cs` : Parses OpenDocument files, specifically extracting spreadsheet data from ODS files into DataTables.

### Security
- `Encryption.cs` : Provides encryption and decryption functionality using Rijndael algorithm for OpenPetra.

### Template Processing
- `ProcessTemplate.cs` : Utility class for processing text templates with placeholders, snippets, and conditional sections.

### Data Import-Export
- `ImportExportTextFile.cs` : Handles import and export of text files in a format compatible with Petra 2.x, supporting various data types and formats.

## Files
### Csv2Xml.cs

TCsv2Xml implements conversion utilities between CSV, XML, and Excel formats in OpenPetra. The class provides methods for transforming data between these formats while preserving structure and data types. Key functionality includes converting XML to CSV (flattening hierarchical structures), XML to Excel, DataTable to Excel, CSV to XML, and Excel to DataTable. The class handles data type conversion, formatting, and structure preservation during transformations. Important methods include Xml2Csv, Xml2ExcelStream, DataTable2ExcelStream, ParseCSVFile2Xml, and ParseExcelWorkbook2DataTable. The implementation supports features like hierarchical data flattening using 'childOf' relationships, proper data type handling, and multi-worksheet Excel exports.

 **Code Landmarks**
- `Line 51`: GetAllAttributesAndNodes recursively collects XML attributes and nodes to build CSV headers while flattening hierarchical structures
- `Line 167`: Xml2ExcelWorksheet handles type conversion from XML to Excel with special handling for dates, integers and decimals
- `Line 293`: CSV2ExcelStream converts raw CSV strings to Excel without requiring intermediate data structures
- `Line 367`: ParseExcelWorkbook2DataTable provides selective column import capability through AColumnsToImport parameter
- `Line 474`: StringHelper.GetNextCSV supports multi-line CSV values that contain line breaks
### Encryption.cs

EncryptionRijndael implements encryption and decryption functionality using the Rijndael algorithm for OpenPetra. It provides methods for managing secret keys, encrypting/decrypting strings and streams. Key functionality includes reading and creating secret keys from files, encrypting messages with initialization vectors, and decrypting encrypted content. Important methods include ReadSecretKey, CreateSecretKey, Encrypt (for both strings and streams), Decrypt (for both strings and streams), and GetEncryptionName. The class uses System.Security.Cryptography for implementing the Rijndael algorithm and handles Base64 encoding/decoding for storing keys and encrypted messages.

 **Code Landmarks**
- `Line 44`: Reads encryption keys stored as Base64 strings for easier handling
- `Line 65`: Encryption method generates initialization vectors that must be shared with recipients
- `Line 103`: Stream-based encryption for handling larger data volumes
### Excel.cs

TExcel implements a class that provides an interface to Microsoft Excel through COM interop. It enables OpenPetra to programmatically create Excel workbooks, add sheets, manipulate cell values, and create charts. Key functionality includes instantiating Excel, creating new worksheets, accessing cell ranges, setting values and formulas, and generating various chart types. The file also defines several enumerations (XlChartType, XlChartLocation, XlDataLabelsType) that represent Excel-specific constants. Important methods include NewSheet(), GetRange(), SetValue(), SetFormula(), AddChart(), and GiveUserControl() which transfers control of the Excel instance to the user.

 **Code Landmarks**
- `Line 240`: Uses reflection (InvokeMember) for COM interop with Excel, allowing dynamic access to Excel objects without direct COM references
- `Line 190`: FixSheetName method handles Excel's sheet naming limitations by truncating names and ensuring uniqueness
- `Line 392`: AddChart method demonstrates complex Excel chart creation through COM interop with multiple object manipulations
### FileHelper.cs

TFileHelper implements static utility methods for file operations in OpenPetra. It provides functionality for compressing and decompressing files using ZIP format, handling memory streams, file backup, directory creation, temporary file generation, and Base64 encoding/decoding of binary files. The class is organized into nested classes for stream operations and compression. Key methods include DeflateFilesIntoMemoryStream for compressing multiple files, InflateFilesFromStream for decompression, MoveToBackup for safe file backup, GetTempFileName for generating unique temporary filenames, and conversion methods between binary files and Base64 strings. It also includes platform-specific utilities for finding text editors on Windows systems.

 **Code Landmarks**
- `Line 69`: Implements ZIP compression with optional password protection and path preservation
- `Line 195`: CreateDirectory method works around symbolic link issues on Linux systems
- `Line 229`: Base64 conversion methods for binary files enable easier data transfer
- `Line 291`: Platform detection for Windows-specific registry access to find text editors
### HTTPUtils.cs

THTTPUtils implements utility functions for HTTP operations in OpenPetra. It provides methods for reading website content, posting requests, and downloading files while managing session cookies. Key functionality includes handling authentication sessions through cookies, retrying failed requests, and managing secure connections. The class uses WebClient with custom session handling and supports both synchronous and asynchronous operations. Important methods include ReadWebsite, PostRequest, DownloadFile, and ResetSession. The class also contains helper methods for cookie management and request logging, with thread-static variables to maintain session state.

 **Code Landmarks**
- `Line 72`: Custom WebClient implementation that maintains cookie state between requests
- `Line 95`: Configurable SSL certificate validation bypass for testing environments
- `Line 177`: ThreadStatic attribute ensures session cookies are thread-specific
- `Line 275`: Configurable retry mechanism for failed HTTP requests
- `Line 234`: Session validation prevents unauthorized server connections
### ImportExportTextFile.cs

TImportExportTextFile implements functionality for reading and writing text files in a format compatible with Petra 2.x. It provides methods for handling various data types including strings, booleans, integers, decimals, and dates with proper formatting. The class maintains internal state for reading and writing operations, manages line parsing, and handles special cases like multi-line strings and nullable values. Key methods include StartWriting(), FinishWriting(), InitReading(), ReadString(), ReadBoolean(), ReadInt32(), ReadDecimal(), ReadDate(), and their corresponding Write() methods. The class also supports date format configuration and tracks the current line position during parsing operations.

 **Code Landmarks**
- `Line 122`: Handles multi-line string values by detecting incomplete quoted strings and combining lines
- `Line 193`: Special handling for the '?' character which represents null values in the Petra 2.x format
- `Line 362`: Accommodates legacy date formats by detecting and handling two-digit years
### OpenDocumentParser.cs

TOpenDocumentParser implements functionality for parsing OpenDocument files, specifically focusing on extracting data from OpenDocument Spreadsheet (.ods) files. The class provides a static method ParseODSStream2DataTable that reads a spreadsheet from a memory stream and converts it into a DataTable structure. It handles worksheet selection, column filtering, header rows, and various cell data types including strings, numbers, dates, and booleans. The parser uses SharpZipLib to extract the content.xml file from the ODS archive and processes the XML structure to populate the DataTable. The implementation supports features like repeated columns and type conversion.

 **Code Landmarks**
- `Line 49`: Main method that converts ODS spreadsheets to DataTable with options for headers and column filtering
- `Line 173`: Handles extraction of content.xml from the ODS zip file using SharpZipLib
- `Line 95`: Processes different cell data types (float, date, boolean, string) with appropriate type conversion
### PackTools.cs

PackTools implements utility functions for compressing and extracting files and directories, primarily used by OpenPetra's patch program. It provides methods for zipping/unzipping files, compressing/decompressing strings, and handling various archive formats including ZIP, TAR, GZip, and 7Zip. Key functionality includes directory recursion for ZIP creation, selective file extraction, and external 7Zip integration. Important methods include Unzip(), ZipDirectory(), ZipString(), UnzipString(), ExtractTarGz(), ExtractTar(), Extract7Zip(), PackTar(), and ExtractSRPM(). The class handles path normalization, file streams, and compression levels while supporting exclusion filters for directories.

 **Code Landmarks**
- `Line 193`: Implements string compression using GZip and Base64 encoding for efficient data transfer
- `Line 217`: String unzipping method returns array of lines, supporting multi-line text extraction
- `Line 307`: External process integration with 7Zip CLI tool requiring it to be available on PATH
- `Line 343`: Custom TAR archive creation with explicit user information settings
- `Line 387`: Multi-step extraction process for SRPM files using both 7Zip and internal extraction methods
### ProcessTemplate.cs

ProcessTemplate implements a utility class for handling text templates in OpenPetra. It manages template files with placeholders, snippets, and conditional sections (IFDEF/IFNDEF). Key functionality includes loading templates from files, managing code snippets, replacing placeholders with actual content, processing conditional sections, and writing the processed templates to output files. The class supports template inclusion, nested codelets, and proper indentation of inserted code. Important methods include constructor for loading templates, GetSnippet, InsertSnippet, ReplacePlaceHolder, ProcessIFDEFs, and FinishWriting. The class also provides StringBuilderExtensions for easier string manipulation.

 **Code Landmarks**
- `Line 69`: Supports recursive template inclusion with {#INCLUDE filename} syntax
- `Line 104`: Extracts reusable code snippets marked with {##name} for later use
- `Line 320`: Uses nested IFDEF/IFNDEF processing with proper tag matching
- `Line 437`: Maintains proper code indentation when inserting multi-line content
- `Line 606`: Provides StringBuilder extensions for easier string manipulation
### SEPAWriter.cs

TSEPAWriterDirectDebit implements functionality for creating SEPA (Single Euro Payments Area) Direct Debit XML files. It handles initialization of the XML document with proper headers, adding payment sections for different sequence types (FRST, OOFF, RCUR, FNAL), and adding individual payment transactions with debtor information. Key methods include Init() to create the file header with creditor details, AddPaymentSectionToSEPADirectDebitFile() to organize payments by sequence type, and AddPaymentToSEPADirectDebitFile() to add individual transactions. The class also provides a utility method FormatIBAN() for formatting IBAN numbers with spaces for readability.

 **Code Landmarks**
- `Line 47`: Creates XML structure following the ISO 20022 pain.008.001.02 schema for SEPA Direct Debit transactions
- `Line 91`: Implements payment grouping by sequence type (first, one-off, recurring, final) as required by SEPA standards
- `Line 152`: Handles transaction details including mandate information required for SEPA compliance
- `Line 207`: Utility method for IBAN formatting that supports both machine and human-readable formats
### SimpleYmlParser.cs

TSimpleYmlParser implements a specialized YAML parser designed for efficiently processing large database files saved in YML.GZ format. It extends TYml2Xml but avoids loading the entire document into memory at once. The class parses YAML files by identifying node captions and their line ranges, then allows iterating through rows with their attributes. Key functionality includes parsing captions to build a navigation index and retrieving attribute values from individual lines. Important methods include ParseCaptions(), StartParseList(), and GetNextLineAttributes(), which work together to enable sequential processing of large YAML structures.

 **Code Landmarks**
- `Line 45`: Class extends TYml2Xml but implements a more memory-efficient approach for large files
- `Line 91`: ParseCaptions builds an index of node positions with start/end line numbers for efficient navigation
- `Line 135`: GetNextLineAttributes parses YAML dictionary syntax into a SortedList of key-value pairs
### SmtpEmail.cs

TSmtpSender implements a wrapper around the MailKit SMTP email services, providing functionality to send emails within the OpenPetra system. It handles SMTP server configuration, email composition, and sending. The class supports features like setting sender addresses, adding attachments, CC/BCC recipients, reply-to addresses, and sending emails from templates. It includes error handling for failed deliveries and server connection issues. Key classes include TSmtpSender (main email sending functionality), TSmtpServerSettings (configuration structure), TsmtpFailedRecipient (tracking failed deliveries), and custom exceptions like ESmtpSenderException. Important methods include SendEmail, SendEmailFromTemplate, SendMessage, and ValidateEmailAddress.

 **Code Landmarks**
- `Line 317`: Handles email address parsing with special handling for both semicolon and comma separators to support Microsoft Outlook compatibility
- `Line 539`: Implements retry logic for sending emails, with 3 attempts and 1-minute delays between attempts
- `Line 186`: Uses a delegate pattern to fetch SMTP settings, allowing the class to be used from both client and server contexts
### TextFile.cs

TTextFile implements utility functions for handling text files in OpenPetra. It provides methods for recursively processing files, converting between DOS and Unix line endings, comparing file contents, detecting file encodings, and updating files conditionally. Key functionality includes SameContent() for comparing files with options to ignore line endings, Diff() for showing differences between texts, and AutoDetectTextEncodingAndOpenFile() for sophisticated encoding detection. The class also offers methods for converting between encodings and updating files only when content has changed, which is useful for generated code files.

 **Code Landmarks**
- `Line 127`: Implements a sophisticated file comparison algorithm with options to ignore line endings and handle special placeholder patterns with multiple percentage characters
- `Line 269`: Implements a simple but effective diff algorithm that shows line-by-line differences between two text files
- `Line 319`: UpdateFile method intelligently handles generated code files by only replacing originals when content has actually changed
- `Line 407`: Comprehensive encoding detection algorithm that handles UTF-7/8/16/32 with or without BOM markers
- `Line 462`: Statistical heuristic approach to detect UTF-8 without BOM by analyzing byte patterns
### TextFileEncoding.cs

TTextFileEncoding implements a class that handles various character encodings supported by OpenPetra. It extends DataTable to store encoding information including code pages, descriptions, and classification (Unicode, single-byte ANSI, or multi-byte ANSI). The class provides methods to create and populate the encoding table with supported code pages, automatically adds the user's default encoding if not already included, and offers utility methods to identify Unicode versus Windows ANSI code pages. Key properties include column name accessors and constants for encoding classifications.

 **Code Landmarks**
- `Line 106`: Creates a comprehensive table of supported encodings including Unicode, single-byte ANSI, and multi-byte character sets like Chinese, Japanese, and Korean
- `Line 148`: Automatically detects and adds the user's default system encoding to supported encodings if not already present
- `Line 171`: Provides utility methods to distinguish between Unicode and Windows ANSI code pages for file operations
### XmlParser.cs

TXMLParser implements a class for parsing XML documents and navigating their contents. It provides functionality for loading XML files with optional validation, navigating through document elements, and retrieving attribute values. The class includes methods for finding nodes recursively, handling attributes (getting, setting, checking existence), and converting attribute values to various data types (string, int, bool, double, decimal). It also provides utilities for XML-to-string conversion with different formatting options and a custom URL resolver for DTD file handling. Key methods include GetDocument(), XmlToString(), FindNodeRecursive(), GetAttribute(), and various attribute accessor methods.

 **Code Landmarks**
- `Line 62`: Custom XmlUrlResolver implementation to handle relative DTD paths by resolving them against the XML file location
- `Line 113`: Special handling for Mono runtime by disabling validation to avoid DTD processing issues
- `Line 246`: Helper method to transform XML structure by moving element names to attributes for DTD/Schema validation
- `Line 301`: Utility method to skip comment nodes and empty text nodes when navigating XML
- `Line 379`: Recursive attribute lookup that can inherit values from parent nodes
### Yml2Xml.cs

TYml2Xml implements a converter that parses YAML files into XML documents for the OpenPetra system. It supports basic YAML syntax including indentation, sequences, mappings, and scalar values, transforming them into XML elements and attributes. The class provides functionality for parsing single YAML files, merging multiple YAML documents with inheritance hierarchies, and converting XML back to YAML. Key features include handling base/derived relationships, attribute inheritance, and element ordering. Important methods include ParseYML2XML(), Merge(), LoadChild(), Tag(), and GetChildren(). The class also includes utilities for manipulating XML attributes and elements while maintaining hierarchical relationships.

 **Code Landmarks**
- `Line 40`: Custom YAML parser implementation rather than using existing libraries to simplify working with XML in the program
- `Line 371`: Implements hierarchical document merging with base/derived relationships for YAML documents
- `Line 544`: Tag() method moves all leaf nodes to a base element, enabling version control of document elements
- `Line 639`: YamlItemOrderComparer provides custom sorting of XML nodes based on Order attribute and depth

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #