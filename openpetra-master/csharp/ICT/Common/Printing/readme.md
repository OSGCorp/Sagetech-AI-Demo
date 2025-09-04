# C# Printing Subproject Of Petra

The Petra is a C# program that provides administrative management capabilities for non-profit organizations. The program handles financial transactions and contact management while reducing operational costs. This sub-project implements document generation and printing functionality along with barcode generation capabilities. This provides these capabilities to the Petra program:

- PDF document generation from HTML templates
- Flexible printing layouts for reports, letters, and labels
- Barcode generation for Code 128 format
- Multi-format output support (PDF, HTML, TXT)

## Identified Design Elements

1. **Layered Printing Architecture**: Abstract base classes (TPrinter, TPrinterLayout) provide core functionality that is extended by format-specific implementations (TPdfPrinter, TTxtPrinter)
2. **Template-Based Document Generation**: Form letters, labels, and reports are generated from HTML templates with variable substitution
3. **Cross-Platform Compatibility**: Font resolution and rendering is handled to ensure consistent output across Windows and Linux environments
4. **Flexible Positioning System**: Supports both absolute positioning for precise layouts and flow-based positioning for dynamic content

## Overview
The architecture follows a modular design with clear separation between abstract printing interfaces and concrete implementations. The HTML-to-PDF conversion leverages external tools (wkhtmltopdf) while maintaining a consistent API. The system supports various output formats and printing styles including report-style (with text fitting) and form letter style (with exact positioning). Barcode generation capabilities are integrated for document identification needs. The printing subsystem handles complex layout requirements including tables, images, and text with various formatting options while managing pagination and page breaks automatically.

## Business Functions

### Barcode Generation
- `BarCode128.cs` : Implements barcode generation for Code 128 format in the OpenPetra printing system.

### Printing Core
- `Printer.cs` : Defines the TPrinter abstract class for handling printing operations in OpenPetra with various formatting options.
- `PrinterLayout.cs` : Defines abstract printer layout classes for OpenPetra's printing system.

### Printing Implementations
- `GfxPrinter.cs` : Base class for graphics-based printing in OpenPetra, providing core functionality for text and graphics rendering.
- `PdfPrinter.cs` : PDF printer implementation for OpenPetra using PdfSharp library to generate PDF documents with text, graphics, and images.
- `TxtPrinter.cs` : Text-based printer implementation for OpenPetra that allows positioning text in memory before writing to file.

### HTML Processing
- `PrinterHtml.cs` : HTML rendering class for printing HTML content to screen or PDF in OpenPetra

### Document Conversion
- `Html2Pdf.cs` : Utility class for converting HTML documents to PDF using wkhtmltopdf external tool.

### Template Processing
- `FormLetters.cs` : Provides tools for generating form letters, labels, and reports in HTML format for printing or email preparation.

### Image Management
- `PageImageList.cs` : Implements a simple image list for storing page previews in the OpenPetra printing system.

## Files
### BarCode128.cs

TBarCode128 implements functionality for converting text strings into Code 128 barcode format for printing. The class provides a single static method Encode() that transforms input text into a character string compatible with the code128.ttf font. The implementation handles automatic switching between Code 128 Table B (for text) and Table C (for numeric pairs), calculates the required checksum, and adds start/stop characters. The code includes validation to ensure input characters are within the valid range (32-126 or 203). The algorithm is adapted from an external source with modifications to properly handle table switching and checksum calculation.

 **Code Landmarks**
- `Line 47`: Validates input characters are within the valid Code 128 range before processing
- `Line 59`: Implements intelligent switching between Code 128 Table B and Table C based on character sequences
- `Line 132`: Calculates the Code 128 checksum using modulo 103 algorithm
### FormLetters.cs

TFormLettersTools implements utility functions for creating and managing form letters, labels, and reports in HTML format. The class provides methods to find role-specific template files, attach pages to documents, extract content from HTML divs, and parse style attributes. It contains three inner classes: THTMLFormLetter (base class for HTML document generation), THTMLSimpleLetter (for single-page letters with field replacements), THTMLFormLabels (for printing labels in a grid layout), and THTMLFormReport (for multi-section reports with group headers and footers). Key methods include GetRoleSpecificFile, AttachNextPage, PrintSimpleHTMLLetter, PrintLabels, PrintReport, and GeneratePDFFromHTML. The class handles page breaks, content positioning, and template variable substitution.

 **Code Landmarks**
- `Line 66`: Implements a template file resolution system that handles country-specific and form-specific variations with fallback logic
- `Line 152`: Custom HTML parsing technique that correctly handles nested div tags by counting opening and closing tags
- `Line 385`: Implements a flexible label printing system that calculates positions based on page dimensions and label sizes
- `Line 488`: Uses a header-detail-footer pattern for report generation with conditional page breaks
- `Line 694`: PDF generation from HTML with debugging capability that saves intermediate HTML files
### GfxPrinter.cs

TGfxPrinter implements a base class for graphics-based printing in OpenPetra, particularly serving as the foundation for TPdfPrinter. It manages printing text with different alignments, handling text wrapping, drawing lines and rectangles, and rendering bitmaps. The class supports two printing behaviors: report-style (where text can be fitted) and form letter style (where everything must be printed). Key functionality includes text positioning, width calculation, line feeds, page management, and coordinate system conversions. Important methods include PrintString(), PrintStringWrap(), GetWidthString(), DrawLine(), LineFeed(), and ValidYPos(). The class also handles margin settings, page orientation, and printer resolution conversions.

 **Code Landmarks**
- `Line 56`: Defines two printer behaviors that affect text handling: report mode (can fit text) and form letter mode (must print everything)
- `Line 200`: GetFittedText method intelligently shortens text with ellipsis when it doesn't fit available space
- `Line 252`: Word wrapping algorithm that handles special cases when even the first word doesn't fit available space
- `Line 367`: DrawBitmap supports both absolute dimensions and percentage-based sizing
- `Line 461`: Uses a standard 300 DPI resolution for pixel-to-unit conversions when printer resolution is unavailable
### Html2Pdf.cs

Html2Pdf implements functionality to convert HTML content to PDF format using the wkhtmltopdf external tool. It provides methods to locate the wkhtmltopdf binary, strip unnecessary page breaks from HTML content, and generate PDF files from HTML. Key methods include GetWkHTMLToPDFPath() for locating the binary, StripLastPageBreak() for removing redundant page breaks, and HTMLToPDF() which handles the conversion process by creating a temporary HTML file with embedded CSS and executing wkhtmltopdf. The class also manages embedding CSS and JavaScript resources from the OpenPetra installation directory.

 **Code Landmarks**
- `Line 40`: Searches for wkhtmltopdf binary in multiple standard locations before failing
- `Line 56`: Intelligent page break handling to prevent empty pages at the end of documents
- `Line 95`: Embeds external CSS and JavaScript directly into HTML before conversion to ensure proper rendering
### PageImageList.cs

PageImageList implements a simple class that extends List<Image> to store page preview images for printing functionality. The file includes two implementation approaches: an active simple version that directly extends List<Image>, and a commented-out more complex version that would store images as byte arrays to conserve GDI resources. The commented implementation includes methods for adding, retrieving, and clearing images, along with conversion utilities between Image objects and byte arrays using Windows GDI API calls.

 **Code Landmarks**
- `Line 32`: Contains a commented-out alternative implementation that would store images as byte arrays to avoid GDI resource limitations (Windows only supports 10,000 simultaneous GDI objects).
- `Line 78`: Uses Windows GDI interop to extract metafile bits from images in the alternative implementation.
- `Line 92`: The active implementation is deliberately simplified to a basic List<Image> extension for performance reasons.
### PdfPrinter.cs

TPdfPrinter implements a PDF printing capability for OpenPetra using the PdfSharp library. It extends TGfxPrinter to provide PDF-specific functionality including text rendering with various fonts and alignments, drawing lines and rectangles, inserting images, and managing page layout. The class handles font initialization, text measurement, page formatting, and PDF generation with proper margins and paper sizes. Key methods include PrintString for text output, DrawLine and DrawRectangle for graphics, DrawBitmap for images, and SavePDF for document generation. It also includes a MonoFontResolver class to handle font resolution across different platforms, particularly for Linux environments.

 **Code Landmarks**
- `Line 84`: Uses a custom MonoFontResolver to handle font resolution across platforms, particularly important for Linux compatibility
- `Line 187`: Font caching implementation to improve performance when repeatedly using the same fonts
- `Line 290`: Special handling for text alignment on Unix/Mono platforms to work around rendering issues
- `Line 626`: Font substitution logic maps common Windows fonts to Linux equivalents like Arial to LiberationSans
- `Line 545`: PDF document generation with configurable paper sizes and margins
### Printer.cs

TPrinter.cs implements an abstract class that provides a foundation for printing functionality in OpenPetra. It defines numerous enumerations for print settings like orientation, margins, alignment, and font types. The class manages printer state through the TPrinterState class, which tracks current positions, fonts, and formatting. Key functionality includes text printing with various alignment options, line drawing, bitmap rendering, and table printing. The class provides methods for position management, state preservation through push/pop operations, and unit conversions between centimeters, inches, and pixels. TPrinter is designed to be extended by concrete implementations like TxtPrinter and GfxPrinter that implement the abstract methods for specific output formats.

 **Code Landmarks**
- `Line 496`: Implements a state stack pattern allowing printer state to be saved and restored, useful for nested formatting operations
- `Line 566`: Table rendering implementation with support for complex formatting including borders, cell alignment and wrapping
- `Line 506`: Simulation mode allows measuring text without actual printing, helping with layout calculations
### PrinterHtml.cs

TPrinterHtml implements HTML rendering functionality for the OpenPetra printing system. It parses HTML documents and renders them to various output formats, primarily PDF. The class handles HTML elements like tables, images, fonts, and text formatting, supporting features such as absolute positioning, page breaks, and table layouts. It manages pagination by tracking content that doesn't fit on the current page and continuing it on subsequent pages. Key methods include RenderContent for interpreting HTML elements, PrintTable for table rendering, and GetPageSize for determining document dimensions from CSS. The class also provides utility methods for extracting specific sections from HTML templates.

 **Code Landmarks**
- `Line 72`: Handles HTML page breaks by replacing tags with body elements for pagination
- `Line 133`: Custom HTML parser that converts HTML to XML for easier processing
- `Line 186`: Extracts page size information from CSS styles in the HTML body
- `Line 471`: Position handling system that supports both absolute and relative positioning from CSS
- `Line 711`: Table rendering with pagination support that remembers header rows for multi-page tables
### PrinterLayout.cs

TPrinterLayout implements an abstract base class for printer layouts in OpenPetra's printing system. It defines the interface for rendering content on printed pages with methods for page setup, document initialization, and rendering page components (header, body, footer). The file also includes two supporting classes: TTableCellGfx for representing table cells with properties like content, alignment, borders, and spanning; and TTableRowGfx for managing rows of table cells. Key methods include GetPageSize(), StartPrintDocument(), PrintPageHeader/Body/Footer(), and the virtual RenderContent() method that derived classes can implement for specific content rendering.

 **Code Landmarks**
- `Line 46`: RenderContent method uses XmlNode for content representation, suggesting XML-based document structure for printing
- `Line 36`: Abstract class design pattern allows for different printer implementations while maintaining a consistent interface
- `Line 59`: TTableCellGfx implements HTML-like table cell representation with border constants using bitfield flags
### TxtPrinter.cs

TTxtPrinter implements a text-based printer class that allows free positioning of text in memory before writing to a file. It extends TPrinter with text-specific functionality for creating reports with proper alignment, centering, and line management. The class maintains an ArrayList of text lines and provides methods for line manipulation, text positioning, and word wrapping. Key functionality includes printing strings with various alignment options, calculating text widths, drawing separator lines, and managing page layout. Important methods include PrintString(), GetFittedText(), LineFeed(), and WriteToFile(). The class supports both portrait and landscape orientations with predefined character limits per line.

 **Code Landmarks**
- `Line 163`: Word wrapping algorithm that calculates how much text will fit in a given width
- `Line 246`: Smart text fitting that adds ellipsis (...) when text is too long for available space
- `Line 385`: InsertLineForMarkingLine method allows adding separator lines without disrupting existing content
- `Line 535`: FinishText method optimizes output by trimming separator lines to match content width

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #