# C# Petra Definitions Subproject Of OpenPetra

The C# Petra Definitions subproject provides the core configuration and metadata that drives OpenPetra's functionality across multiple architectural layers. This subproject implements configuration-driven behavior definitions and UI templates along with help system integration. This provides these capabilities to the OpenPetra program:

- Cacheable data structure definitions for performance optimization
- Wizard interface templates for complex data entry workflows
- Help system topic mappings for contextual documentation
- YAML-based configuration for code generation

## Identified Design Elements

1. **Configuration-Driven Architecture**: Uses YAML files to define cacheable data structures and UI workflows, enabling code generation and runtime behavior configuration
2. **Modular Wizard Framework**: The Shepherd system provides a reusable, configurable wizard interface that can be composed from smaller components
3. **Hierarchical Data Organization**: Structures data into logical domains (Partner, Finance, etc.) with clear relationships and dependencies
4. **Cross-Reference Documentation System**: Maps UI components directly to help documentation through XML configuration

## Overview
The architecture emphasizes configuration over code, allowing many system behaviors to be defined declaratively rather than programmatically. The caching system improves performance by defining which database tables and calculated lists should be cached. The Shepherd wizard framework enables complex data entry workflows through composable, reusable components. The help system integration ensures context-appropriate documentation is available throughout the application. These definitions serve as the backbone for code generation, UI composition, and system behavior across OpenPetra's various modules.

## Business Functions

### Configuration
- `CacheableTablesAndLists.yaml` : Configuration file defining cacheable database tables and calculated lists for the OpenPetra application.

### Partner Management Templates
- `Shepherd_Family_Definition.yaml` : YAML configuration file defining the Shepherd Family wizard interface with multiple pages for personnel, family, and finish steps.
- `Shepherd_Church_Definition.yaml` : YAML configuration file defining the Church Shepherd wizard interface structure and components.

### Documentation
- `HelpTopics.xml` : XML mapping file that connects UI form contexts to help documentation bookmarks

## Files
### CacheableTablesAndLists.yaml

This YAML configuration file defines the structure of cacheable data for OpenPetra's server and client components. It organizes database tables and calculated lists into logical domains (Partner, Common, Personnel, Finance, Conference, SysMan) that can be cached for performance. Each entry specifies database tables to cache along with optional enum names and comments. The file also defines calculated lists that require custom code to populate from database queries. Many entries include metadata like comments for documentation, custom enum names, and dependencies on ledger selection. This configuration drives code generation for the server's caching system and shared enum definitions.

 **Code Landmarks**
- `Line 4`: Defines a structure that generates both server-side caching code and shared enums from a single configuration source
- `Line 9`: Implements automatic enum naming convention by removing first letter and adding 'List' suffix
- `Line 37`: Introduces 'CalculatedLists' that require manual code implementation rather than direct table caching
- `Line 107`: Uses 'DependsOnLedger' flag to mark finance-related tables that vary based on selected accounting ledger
### HelpTopics.xml

HelpTopics.xml defines mappings between OpenPetra UI forms/controls and their corresponding help documentation links. The file contains a structured XML document with a 'pageGuide' root element that maps form contexts (like TLoginForm, TFrmSetupCurrency, TFrmGiftBatch) and their components to specific HTML bookmark locations in the help documentation. Each mapping is defined by a 'page' element with a 'context' attribute and a nested 'link' element containing the HTML file and bookmark reference.

 **Code Landmarks**
- `Line 1-6`: Maps both entire forms (TLoginForm) and specific form components (TFrmGiftBatch.ucoTransactions) to help documentation
### Shepherd_Church_Definition.yaml

This YAML configuration file defines the structure and components of the Church Shepherd wizard interface in OpenPetra. It specifies a multi-page wizard with dimensions, title, and a finish page note. The file defines several shepherd pages including PartnerDetailsChurch, PartnerDetailsFamily, and FinalShepherd, each with properties like ID, title, visibility settings, user control information, and help context. It also includes three SubShepherd sections that import pages from other YAML files (Shepherd_Family_Definition.yaml and Shepherd_Test_Only.yaml) with specific page selections.

 **Code Landmarks**
- `Line 4`: Defines wizard dimensions and UI properties that control the overall interface appearance
- `Line 13`: UserControl components are referenced with namespace and class name pairs for dynamic loading
- `Line 19`: SubShepherd sections allow importing pages from external YAML files, supporting modular wizard configuration
- `Line 34`: ImportPages parameter allows selective inclusion of specific pages by ID from external definitions
### Shepherd_Family_Definition.yaml

This YAML configuration file defines the structure and properties of a 'Shepherd' wizard interface for adding new families in OpenPetra. It specifies a window with dimensions and three shepherd pages: PersonnelShepherd, OtherShepherd, and FinalShepherd. Each page has properties including ID, title, visibility, enabled status, user control namespace, class name, help context, and user control type. The pages appear to represent different steps in a family creation workflow, with specific user controls assigned to each step.

 **Code Landmarks**
- `Line 1`: Uses YAML format for UI configuration rather than code-based definition, allowing for easier modification without recompilation
- `Line 6`: Implements a multi-page wizard pattern with different user controls for each step of the family creation process
- `Line 12`: References external user controls from different namespaces that get loaded dynamically based on this configuration

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #