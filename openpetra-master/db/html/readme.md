# Database HTML Subproject Of OpenPetra

The Database HTML subproject provides comprehensive documentation for OpenPetra's database structure through a web-based interface. This subproject implements a frameset-based documentation system along with interactive JavaScript functionality to navigate and display database schema information. This provides these capabilities to the OpenPetra program:

- Interactive database schema documentation with navigable table relationships
- Dual-view interface showing table groups and detailed table information
- Dynamic content loading based on URL parameters
- Visual differentiation of database elements (primary keys, foreign keys, constraints)

## Identified Design Elements

1. Frameset Architecture: Uses HTML frames to create a consistent layout with navigation sections and content areas that can be updated independently
2. Parameter-Driven Content Loading: JavaScript functions parse URL parameters to dynamically load specific table documentation
3. Visual Schema Representation: CSS styling provides clear visual differentiation between database elements like primary keys and foreign keys
4. Tooltip System: Implements a cross-browser compatible popup description system that follows the cursor for enhanced usability
5. External Documentation Integration: Links to SchemaSpy documentation showing database schema with row counts for different database configurations

## Overview
The architecture emphasizes usability through a consistent interface with navigation tabs and split views. The JavaScript functionality enables dynamic content loading without page refreshes, while the CSS styling creates visual hierarchy and readability for complex database information. The documentation system is designed to help developers understand table relationships, field constraints, and database structure, facilitating maintenance and feature development within OpenPetra's data layer.

## Business Functions

### Documentation
- `index.html` : HTML frameset structure for OpenPetra database documentation interface.
- `table-doc.css` : CSS stylesheet for database table documentation in OpenPetra
- `table-doc-top.html` : HTML header frame for OpenPetra database documentation
- `header.html` : HTML header file for OpenPetra database design documentation

### Frontend Components
- `table-doc.js` : JavaScript function to load and display table documentation in a framed interface.
- `table-doc-sub.js` : JavaScript module for creating tooltip-style popup descriptions that follow the mouse cursor on web pages.
- `footer.html` : HTML footer closing tags for OpenPetra web interface pages.

## Files
### footer.html

This file contains only the closing HTML tags (</BODY> and </HTML>) that complete HTML documents in the OpenPetra web interface. It serves as a standard footer included at the bottom of HTML pages throughout the application to properly terminate the document structure.
### header.html

This HTML header file sets up the structure for OpenPetra's database design documentation. It defines the document type, title, metadata keywords and description related to OpenPetra's database design. The file links to an external CSS stylesheet and JavaScript file for functionality. The body includes an anchor tag and heading that introduces the OpenPetra.org datastructure, with an onLoad event that calls a JavaScript function to load parameter tables based on the URL search parameters.

 **Code Landmarks**
- `Line 10`: Uses JavaScript function loadParameterTable() to dynamically load content based on URL search parameters
### index.html

This HTML file serves as the main entry point for OpenPetra's database documentation. It creates a frameset layout with a top navigation bar and a split view showing table groups on the left and detailed table information on the right. The page loads with p_partner.html as the default table documentation and includes JavaScript functionality to load specific tables based on URL parameters. The file references CSS and JavaScript files for styling and interactive functionality.

 **Code Landmarks**
- `Line 7`: Uses JavaScript onLoad event to parse URL parameters and load specific table documentation
- `Line 9-16`: Implements a nested frameset structure to organize database documentation into navigation and content areas
### table-doc-sub.js

This JavaScript file implements a tooltip-style popup description system for web pages. It creates a floating description box that follows the mouse cursor and displays custom text. The code includes browser detection functionality, positioning logic for the popup relative to the cursor, and methods to show and hide the description box. Key functions include checkBrowser() for browser detection, makeObj() to create cross-browser compatible objects, popmousemove() to track cursor position, popupInit() to initialize the system, popup() to display text, and popout() to hide the popup.

 **Code Landmarks**
- `Line 13`: Custom browser detection function that identifies different browser versions and capabilities before DOM standardization
- `Line 34`: Cross-browser compatible object creation approach to handle differences between IE4/5, Netscape 4/5
- `Line 53`: Mouse position capture implementation with specific handling for different browser event models
- `Line 71`: Event capture for Netscape 4 using document.captureEvents, showing legacy browser support
### table-doc-top.html

This HTML file serves as the top frame header for OpenPetra's database documentation. It displays the title 'Database Design of OpenPetra.org' and provides links to SchemaSpy documentation showing database schema with row counts for both a large database and a base database. The file uses minimal styling with a reference to an external CSS file and creates a simple two-column layout for the header content.

 **Code Landmarks**
- `Line 12`: Links to external SchemaSpy documentation tools that visualize the database schema with actual row counts
- `Line 13-14`: Provides two different schema views - one for a complete database and another for just the base database structure
### table-doc.css

This CSS file defines the styling for database table documentation in OpenPetra. It establishes visual formatting for various HTML elements including body backgrounds, navigation tabs, links sections, tables, and specialized fonts. The stylesheet implements color schemes, positioning, borders, and font styles to create a consistent documentation interface. Key styling includes different background colors for navigation sections, positioned div elements for tabs and links, table formatting with borders, and specialized font styles for database elements like primary keys, foreign keys, and field constraints.

 **Code Landmarks**
- `Line 13`: Uses absolute positioning for tab elements which creates a fixed layout structure
- `Line 46`: Implements specialized font styling for database table names with distinct coloring
- `Line 102`: Creates a tooltip-like description system with z-index and visibility control
### table-doc.js

This JavaScript file implements a function for loading table documentation in a framed interface. The loadParameterTable function parses URL parameters to determine which table documentation to display, with parameters for table name and optional group/module. It updates the content of two frames: 'table-info' to show specific table HTML documentation and 'tables' to show a listing of tables for a particular module. The function handles default values and focuses the tables frame after loading.

 **Code Landmarks**
- `Line 5`: Default parameter handling provides a fallback to the p_partner table when no parameters are specified
- `Line 13`: There appears to be a bug where the equality operator (=) is used instead of comparison operator (==) when checking parameters[1].length

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #