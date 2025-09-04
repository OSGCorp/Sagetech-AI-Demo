# Pet Store Applications Root Of Java Pet Store

The Java Pet Store Applications Root is a Java EE program that serves as a reference implementation demonstrating best practices for building enterprise e-commerce applications. The program implements a complete online pet store with shopping cart functionality and order processing capabilities. This sub-project implements the root directory structure containing all application modules that make up the Pet Store system along with build configuration and deployment descriptors.  This provides these capabilities to the Java Pet Store program:

- Centralized build system for coordinating all sub-modules
- Modular architecture with clear separation of concerns
- Multi-tier design following J2EE best practices
- Internationalization support across multiple languages
  - English, Japanese, and Chinese implementations
  - Locale-specific formatting and content

## Identified Design Elements

1. Component-Based Architecture: The application is divided into distinct modules (petstore, admin, supplier, opc) each with specific responsibilities in the overall system
2. Event-Driven Controller Framework: Uses events and actions to handle user interactions and business processes across web and EJB tiers
3. Data Access Object Pattern: Implements database operations through XML-configured SQL statements supporting multiple database types
4. Service Locator Pattern: Centralizes JNDI lookups and resource access through utility classes that abstract away the complexity of resource discovery

## Overview
The architecture follows a multi-tier J2EE design with presentation, business logic, and data access layers clearly separated. The build system coordinates compilation and deployment across all modules using Apache Ant. The application demonstrates enterprise patterns including MVC, DAO, Service Locator, and Business Delegate. The system handles the complete e-commerce workflow from browsing products to order fulfillment, with specialized modules for administration, supplier integration, and order processing. XML configuration is extensively used for deployment descriptors, SQL statements, and UI definitions, promoting flexibility and maintainability.

## Sub-Projects

### src/apps/opc/src

The OPC implements a comprehensive workflow system for processing customer orders, managing supplier interactions, and handling customer communications through asynchronous messaging. This subproject provides these capabilities to the Java Pet Store program:

- Order lifecycle management from submission through fulfillment
- Automated order approval based on configurable business rules
- Supplier purchase order generation and invoice processing
- Customer notification system for order status updates
- Administrative interface for order monitoring and reporting

#### Identified Design Elements

1. **Message-Driven Architecture**: The system uses JMS queues and message-driven beans to handle asynchronous processing of orders, invoices, and customer communications
2. **XML-Based Data Exchange**: XML documents with XSL transformations enable standardized data exchange between system components and external entities
3. **Transition Delegate Pattern**: Workflow transitions are managed through delegate classes that encapsulate communication logic between system components
4. **Facade Pattern**: Administrative functions are exposed through a simplified facade interface that abstracts complex backend operations

#### Overview
The architecture follows a modular design with clear separation between order processing, supplier interactions, and customer communications. The system is built on Enterprise JavaBeans (EJB) technology with stateless session beans for administration and message-driven beans for event processing. Data persistence is handled through Container-Managed Persistence (CMP) entity beans. The workflow engine manages state transitions throughout the order lifecycle, with configurable business rules for order approval. Email notifications are generated using locale-specific templates and delivered through a dedicated mail service component. The design emphasizes scalability, reliability, and maintainability through loose coupling between components.

  *For additional detailed information, see the summary for src/apps/opc/src.*

### src/apps/admin/src

The subproject implements both a web-based administration interface and a rich client application launched via Java Web Start, allowing administrators to manage orders and view sales analytics. This provides these capabilities to the Java Pet Store program:

- Order management (viewing, approving, denying, and updating order statuses)
- Sales data visualization through interactive charts (pie and bar charts)
- Revenue and order analytics with date range filtering
- Asynchronous processing of administrative actions

#### Identified Design Elements

1. Multi-Tier Architecture: Clear separation between presentation layer (client), business delegate layer, and EJB-based backend services
2. Dual Interface Support: Provides both web-based administration and a rich client application launched via JNLP
3. Asynchronous Processing: Uses JMS for non-blocking order status updates through the AsyncSender EJB
4. Internationalization: Supports multiple languages through localized property files
5. MVC Pattern: Implements Model-View-Controller pattern with DataSource as model, panels as views, and actions as controllers

#### Overview

The architecture emphasizes clean separation of concerns with business delegates isolating the presentation layer from implementation details. The rich client application features interactive data visualization components with sortable tables and dynamic charts. The system employs asynchronous processing for order status updates and uses XML for client-server communication. Security is managed through role-based access control with the 'administrator' role. The build system uses Apache Ant for compilation, packaging, and deployment, creating both WAR and EAR files for the J2EE environment.

  *For additional detailed information, see the summary for src/apps/admin/src.*

### src/apps/supplier/src

This sub-project implements the supplier-facing functionality including inventory management, purchase order processing, and order fulfillment. It provides these capabilities to the Java Pet Store program:

- Purchase order processing through message-driven beans
- Inventory management with container-managed persistence
- Order fulfillment with automated invoice generation
  - XML-based document processing
  - JMS-based asynchronous communication

#### Identified Design Elements

1. **J2EE Component Architecture**: The supplier application follows standard J2EE patterns with EJBs (Entity, Session, and Message-Driven Beans), servlets, and JSPs organized in a multi-tier architecture
2. **XML-Based Document Processing**: XML document editors and handlers process supplier orders and generate invoices using DTDs, schemas, and XSLT transformations
3. **Asynchronous Messaging**: JMS topics facilitate communication between the supplier system and the main pet store application
4. **Service Locator Pattern**: JNDI lookup utilities centralize resource references across the application

#### Overview
The architecture emphasizes J2EE best practices with clear separation between presentation, business logic, and persistence layers. The supplier module receives purchase orders via JMS messages, processes them through the OrderFulfillmentFacade, checks inventory availability, and generates invoices when orders are shipped. The system includes tools for database population and a web interface for inventory management. The design supports asynchronous processing through message-driven beans and maintains transactional integrity across distributed components.

  *For additional detailed information, see the summary for src/apps/supplier/src.*

### src/apps/petstore/src

The project implements a multi-tier J2EE architecture with web, business, and data access layers, along with comprehensive build and deployment configurations. This provides these capabilities to the Java Pet Store program:

- Complete e-commerce functionality including product browsing, user management, and order processing
- Enterprise-grade architecture demonstrating J2EE best practices
- Database population tools for sample data initialization
- Comprehensive build system for deployment across different environments

#### Identified Design Elements

1. MVC Architecture: Clear separation between presentation (JSP), control (WebController), and model (EJB) components
2. Event-Driven Design: Extensive use of events (CartEvent, OrderEvent, etc.) to decouple system components
3. Facade Pattern: ShoppingClientFacadeLocal provides a unified interface to multiple subsystems
4. Service Locator Pattern: Centralized JNDI lookups through ServiceLocator to reduce EJB lookup complexity

#### Overview
The architecture follows J2EE 1.3 patterns with a web tier using JSP and servlets, and an EJB tier providing business logic through session and entity beans. The controller layer processes user actions through specialized HTML and EJB action classes, while events facilitate communication between tiers. Database operations are handled through Container-Managed Persistence (CMP) with detailed deployment descriptors configuring table mappings and relationships.

The build system uses Ant with platform-specific scripts (build.bat/build.sh) to compile and package components into WAR and EJB JAR files, ultimately creating a deployable EAR. The application demonstrates enterprise patterns including service locators, business delegates, and value objects, making it an excellent reference for J2EE application design principles.

  *For additional detailed information, see the summary for src/apps/petstore/src.*

## Business Functions

### Documentation
- `opc/src/application.xml` : J2EE application deployment descriptor for the Order Processing Center application.
- `opc/src/ejb-jar-manifest.mf` : Manifest file for EJB JAR specifying class path dependencies for the OPC application.
- `opc/src/sun-j2ee-ri.xml` : J2EE deployment descriptor for OPC application configuring EJBs, web modules, and resource references.
- `admin/src/application.xml` : J2EE application deployment descriptor for the Pet Store admin module.
- `admin/src/sun-j2ee-ri.xml` : J2EE Reference Implementation configuration file for the Pet Store admin application.
- `supplier/src/application.xml` : J2EE application deployment descriptor for the Supplier module of Java Pet Store.
- `supplier/src/ejb-jar-manifest.mf` : EJB manifest file defining class path dependencies for the supplier module.
- `supplier/src/sun-j2ee-ri.xml` : J2EE Reference Implementation configuration file for the supplier application in Java Pet Store.
- `petstore/src/application.xml` : J2EE application deployment descriptor for the Java Pet Store application.
- `petstore/src/ejb-jar-manifest.mf` : Manifest file defining class path dependencies for the Java Pet Store EJB components.
- `petstore/src/sun-j2ee-ri.xml` : J2EE deployment descriptor for Java Pet Store application defining EJB configurations and database mappings.
- `petstore/src/ejb-jar.xml` : EJB deployment descriptor defining enterprise beans for the Java Pet Store application's shopping controller functionality.

### Build Configuration
- `opc/src/build.xml` : Ant build script for the Order Processing Center (OPC) component of Java Pet Store, managing compilation, packaging, and deployment.
- `opc/src/build.bat` : Windows batch script for building the OPC component of Java Pet Store using Apache Ant.
- `opc/src/build.sh` : Build script for the OPC component of Java Pet Store that sets up environment variables and executes Ant build tasks.
- `admin/src/build.xml` : Ant build script for the PetStore Admin application that compiles, packages, and deploys the administration module.
- `admin/src/build.bat` : Windows batch script for building the Java Pet Store admin application using Apache Ant.
- `admin/src/build.sh` : Shell script that builds the admin application component of Java Pet Store.
- `supplier/src/build.xml` : Ant build script for the Supplier Application component of Java Pet Store, managing compilation, packaging and deployment tasks.
- `supplier/src/build.bat` : Windows batch script for building the supplier application in Java Pet Store using Apache Ant.
- `supplier/src/build.sh` : Shell script for building the supplier application in Java Pet Store using Ant.
- `petstore/src/build.xml` : Ant build script for the Java Pet Store application that manages compilation, packaging, and deployment of the application components.
- `petstore/src/build.bat` : Windows batch script for building the Java Pet Store application using Apache Ant.
- `petstore/src/build.sh` : Shell script that sets up the build environment and runs Ant for the Java Pet Store application.
- `build.xml` : Ant build file that orchestrates the compilation and deployment of Java Pet Store application components.

### Order Processing
- `opc/src/com/sun/j2ee/blueprints/opc/ejb/OrderApprovalMDB.java` : Message-driven bean that processes order approval messages, updates order status, and sends supplier purchase orders.
- `opc/src/com/sun/j2ee/blueprints/opc/ejb/PurchaseOrderMDB.java` : Message-driven bean that processes purchase orders from Java Pet Store by creating PurchaseOrder EJBs and handling order approval workflow.
- `opc/src/com/sun/j2ee/blueprints/opc/ejb/InvoiceMDB.java` : Message-driven bean that processes invoice messages and updates purchase order status in the Java Pet Store application.
- `opc/src/com/sun/j2ee/blueprints/opc/transitions/PurchaseOrderTD.java` : Transition delegate for purchase order processing in the Order Processing Center component.
- `opc/src/com/sun/j2ee/blueprints/opc/transitions/OrderApprovalTD.java` : Transition delegate for order approval that sends purchase orders to suppliers and approval notifications to customers.
- `opc/src/com/sun/j2ee/blueprints/opc/transitions/InvoiceTD.java` : Transition delegate for sending completed order information to a JMS queue for invoice processing.
- `supplier/src/com/sun/j2ee/blueprints/supplier/processpo/ejb/SupplierOrderMDB.java` : Message-driven bean that processes purchase orders from OPC, fulfills them, and sends back invoices when orders are shipped.
- `supplier/src/com/sun/j2ee/blueprints/supplier/orderfulfillment/ejb/OrderFulfillmentFacadeEJB.java` : Facade EJB that processes supplier purchase orders, manages inventory, and generates invoices for the Java Pet Store.
- `supplier/src/com/sun/j2ee/blueprints/supplier/orderfulfillment/ejb/OrderFulfillmentFacadeLocal.java` : Local interface for order fulfillment operations in the supplier module of Java Pet Store.
- `supplier/src/com/sun/j2ee/blueprints/supplier/orderfulfillment/ejb/OrderFulfillmentFacadeLocalHome.java` : Local home interface for the supplier's order fulfillment facade EJB.

### Customer Relations
- `opc/src/com/sun/j2ee/blueprints/opc/customerrelations/ejb/LocaleUtil.java` : Utility class for converting locale strings to Locale objects in the OPC customer relations module.
- `opc/src/com/sun/j2ee/blueprints/opc/customerrelations/ejb/MailCompletedOrderMDB.java` : Message-driven bean that emails customers when their orders are completely shipped
- `opc/src/com/sun/j2ee/blueprints/opc/customerrelations/ejb/MailInvoiceMDB.java` : Message-driven bean that emails order invoices to customers after shipment.
- `opc/src/com/sun/j2ee/blueprints/opc/customerrelations/ejb/MailOrderApprovalMDB.java` : Message-driven bean that processes order approval messages and sends email notifications to customers.
- `opc/src/com/sun/j2ee/blueprints/opc/customerrelations/ejb/MailContentXDE.java` : Formats email content by applying XSL transformations to XML documents with locale support.
- `opc/src/com/sun/j2ee/blueprints/opc/customerrelations/ejb/JNDINames.java` : Constants class defining JNDI names for EJB references and environment variables in the OPC customer relations module.
- `opc/src/com/sun/j2ee/blueprints/opc/transitions/MailCompletedOrderTD.java` : Transition delegate for sending email notifications to customers about completed orders.
- `opc/src/com/sun/j2ee/blueprints/opc/transitions/MailInvoiceTransitionDelegate.java` : Implements a transition delegate for sending email invoices to customers via JMS messaging.
- `opc/src/com/sun/j2ee/blueprints/opc/transitions/MailOrderApprovalTransitionDelegate.java` : Implements a transition delegate for sending email notifications to customers for order approval.

### Administration
- `opc/src/com/sun/j2ee/blueprints/opc/admin/ejb/OPCAdminFacade.java` : Remote interface for OPC Admin client to query order information and chart data.
- `opc/src/com/sun/j2ee/blueprints/opc/admin/ejb/OrdersTO.java` : Defines the OrdersTO interface for transferring order collections between system components.
- `opc/src/com/sun/j2ee/blueprints/opc/admin/ejb/OrderDetails.java` : A serializable value object that represents order details for the admin client in the Java Pet Store application.
- `opc/src/com/sun/j2ee/blueprints/opc/admin/ejb/OPCAdminFacadeException.java` : Custom exception class for OPC admin facade operations in the Java Pet Store application.
- `opc/src/com/sun/j2ee/blueprints/opc/admin/ejb/OPCAdminFacadeEJB.java` : Facade EJB that provides administrative functionality for the Order Processing Center (OPC) in the Java Pet Store application.
- `opc/src/com/sun/j2ee/blueprints/opc/admin/ejb/OPCAdminFacadeHome.java` : Home interface for OPC-Admin facade EJB in Java Pet Store's order processing center.
- `admin/src/com/sun/j2ee/blueprints/admin/web/AdminRequestProcessor.java` : No description available
- `admin/src/com/sun/j2ee/blueprints/admin/web/ApplRequestProcessor.java` : No description available
- `admin/src/com/sun/j2ee/blueprints/admin/web/AdminBDException.java` : No description available
- `admin/src/com/sun/j2ee/blueprints/admin/web/AdminRequestBD.java` : No description available
- `admin/src/docroot/index.html` : Simple HTML landing page for the Java Pet Store Admin interface.

### XML Processing
- `opc/src/com/sun/j2ee/blueprints/opc/ejb/TPAInvoiceXDE.java` : XML document editor for TPA invoices that parses and extracts order and line item data.
- `opc/src/docroot/schemas/EntityCatalog.properties` : XML schema mapping configuration file for Trading Partner Agreement (TPA) document types
- `supplier/src/com/sun/j2ee/blueprints/supplier/orderfulfillment/ejb/TPASupplierOrderXDE.java` : XML document editor for transforming supplier orders between XML and Java objects in the supplier order fulfillment system.
- `supplier/src/com/sun/j2ee/blueprints/supplier/rsrc/SupplierOrderStyleSheetCatalog.properties` : Configuration file mapping XML DTDs and namespaces to XSL stylesheets for supplier order processing

### Messaging Infrastructure
- `opc/src/com/sun/j2ee/blueprints/opc/transitions/QueueHelper.java` : Helper class for sending JMS text messages to a queue in the Order Processing Center component.
- `opc/src/com/sun/j2ee/blueprints/opc/transitions/JNDINames.java` : Defines JNDI names for JMS resources used in order processing component of Java Pet Store.
- `supplier/src/com/sun/j2ee/blueprints/supplier/transitions/JNDINames.java` : Defines JNDI resource names for the supplier component of Java Pet Store.
- `supplier/src/com/sun/j2ee/blueprints/supplier/transitions/TopicSender.java` : Helper class for sending JMS messages to a Topic in the Supplier application.
- `supplier/src/com/sun/j2ee/blueprints/supplier/transitions/SupplierOrderTD.java` : TransitionDelegate implementation for handling supplier order transitions in the Java Pet Store application.
- `supplier/src/com/sun/j2ee/blueprints/supplier/processpo/ejb/JNDINames.java` : Constants class defining JNDI names for EJB and JMS resources in the supplier module.

### Web Configuration
- `opc/src/docroot/WEB-INF/web.xml` : Web application deployment descriptor for the Order Processing Center (OPC) application.
- `admin/src/docroot/WEB-INF/web.xml` : Web application deployment descriptor for the PetStore Admin module defining servlets, security, and EJB references.
- `supplier/src/docroot/WEB-INF/web.xml` : Web application deployment descriptor for the Supplier module of Java Pet Store.
- `petstore/src/docroot/WEB-INF/web.xml` : Web application deployment descriptor for Java Pet Store, defining servlets, filters, and EJB references.
- `petstore/src/docroot/WEB-INF/signon-config.xml` : Configuration file defining sign-on security constraints for the Java Pet Store application.
- `petstore/src/docroot/WEB-INF/mappings.xml` : XML configuration file mapping events and URLs to controller actions in the Java Pet Store application.

### Admin Client UI
- `admin/src/client/resources/petstore.properties` : Properties file defining UI strings and labels for the Pet Store Admin client application.
- `admin/src/client/resources/petstore_de.properties` : German language resource file for the Pet Store Admin client application interface.
- `admin/src/client/com/sun/j2ee/blueprints/admin/client/PetStoreProxy.java` : Interface defining the proxy for interacting with the Java Pet Store server administration functions.
- `admin/src/client/com/sun/j2ee/blueprints/admin/client/OrdersViewPanel.java` : UI panel for displaying completed, approved, or denied orders in the admin client interface.
- `admin/src/client/com/sun/j2ee/blueprints/admin/client/About.java` : A dialog component that displays project information with animated Duke mascot and scrolling names.
- `admin/src/client/com/sun/j2ee/blueprints/admin/client/AbstractItemAction.java` : Abstract action class supporting item events for toggle buttons and checkbox menu items.
- `admin/src/client/com/sun/j2ee/blueprints/admin/client/TableSorter.java` : A sortable table model implementation that provides column-based sorting functionality for JTable components.
- `admin/src/client/com/sun/j2ee/blueprints/admin/client/PieChartPanel.java` : A Swing panel that displays sales data as an interactive pie chart with date range selection.
- `admin/src/client/com/sun/j2ee/blueprints/admin/client/WorkQueue.java` : Implements a thread-safe work queue for asynchronous task execution in the admin client application.
- `admin/src/client/com/sun/j2ee/blueprints/admin/client/BarChartPanel.java` : Displays bar charts for sales data in the Pet Store admin interface
- `admin/src/client/com/sun/j2ee/blueprints/admin/client/Chart.java` : Abstract class for creating different chart visualizations in the Java Pet Store admin client.
- `admin/src/client/com/sun/j2ee/blueprints/admin/client/StatusBar.java` : A singleton status bar component for displaying messages at the bottom of the admin application.
- `admin/src/client/com/sun/j2ee/blueprints/admin/client/PetStoreAdminClient.java` : Java Swing client application for administering the PetStore, providing order management and sales data visualization.
- `admin/src/client/com/sun/j2ee/blueprints/admin/client/ServerAction.java` : Provides a base class for GUI actions that execute asynchronously on a WorkQueue thread.
- `admin/src/client/com/sun/j2ee/blueprints/admin/client/HttpPostPetStoreProxy.java` : Client proxy implementation that communicates with the PetStore admin backend via HTTP POST requests.
- `admin/src/client/com/sun/j2ee/blueprints/admin/client/OrdersApprovePanel.java` : UI panel for order approval management in the Java Pet Store admin client
- `admin/src/client/com/sun/j2ee/blueprints/admin/client/TableMap.java` : A utility class that implements a table model adapter by forwarding requests to an underlying model.
- `admin/src/client/com/sun/j2ee/blueprints/admin/client/ToggleActionPropertyChangeListener.java` : PropertyChangeListener implementation that synchronizes toggle state between UI components created from the same AbstractItemAction.
- `admin/src/client/com/sun/j2ee/blueprints/admin/client/DataSource.java` : Central data management class for the Pet Store admin client that connects to server and provides data models for UI components.

### Inventory Management
- `supplier/src/com/sun/j2ee/blueprints/supplier/inventory/web/RcvrRequestProcessor.java` : Servlet that processes inventory management requests from the supplier's receiver role in the Pet Store application.
- `supplier/src/com/sun/j2ee/blueprints/supplier/inventory/web/JNDINames.java` : Defines JNDI name constants for accessing EJBs and resources in the supplier inventory system.
- `supplier/src/com/sun/j2ee/blueprints/supplier/inventory/web/DisplayInventoryBean.java` : A JavaBean that provides access to inventory data for the supplier application's web interface.
- `supplier/src/com/sun/j2ee/blueprints/supplier/inventory/ejb/InventoryLocal.java` : Local interface for the Inventory EJB component defining inventory management operations.
- `supplier/src/com/sun/j2ee/blueprints/supplier/inventory/ejb/InventoryEJB.java` : Entity bean implementation for inventory management in the supplier module of Java Pet Store.
- `supplier/src/com/sun/j2ee/blueprints/supplier/inventory/ejb/InventoryLocalHome.java` : Local home interface for the Inventory EJB component in the supplier application.
- `supplier/src/docroot/index.html` : Simple HTML landing page for the Java Pet Store Supplier application with a link to enter the supplier section.

### Database Population
- `supplier/src/com/sun/j2ee/blueprints/supplier/tools/populate/PopulateException.java` : Custom exception class for handling errors during database population operations in the supplier module.
- `supplier/src/com/sun/j2ee/blueprints/supplier/tools/populate/PopulateServlet.java` : A servlet that populates the supplier database with inventory data from an XML file.
- `supplier/src/com/sun/j2ee/blueprints/supplier/tools/populate/InventoryPopulator.java` : A utility class for populating inventory data from XML into the supplier database.
- `supplier/src/com/sun/j2ee/blueprints/supplier/tools/populate/XMLDBHandler.java` : SAX parser handler for populating database from XML data in the supplier application
- `petstore/src/com/sun/j2ee/blueprints/petstore/tools/populate/CategoryPopulator.java` : Database populator for category data in the Pet Store application
- `petstore/src/com/sun/j2ee/blueprints/petstore/tools/populate/PopulateException.java` : Custom exception class for handling errors in the PetStore database population tool.
- `petstore/src/com/sun/j2ee/blueprints/petstore/tools/populate/CustomerPopulator.java` : Populates customer data in the Java Pet Store database from XML sources.
- `petstore/src/com/sun/j2ee/blueprints/petstore/tools/populate/ItemDetailsPopulator.java` : Populates item details data from XML into database tables for the Pet Store application.
- `petstore/src/com/sun/j2ee/blueprints/petstore/tools/populate/PopulateServlet.java` : A servlet that populates the PetStore database with initial catalog, customer, and user data from XML files.
- `petstore/src/com/sun/j2ee/blueprints/petstore/tools/populate/ItemPopulator.java` : Utility class for populating item data in the PetStore database from XML sources.
- `petstore/src/com/sun/j2ee/blueprints/petstore/tools/populate/ProductPopulator.java` : Utility class for populating product data into a database from XML files.
- `petstore/src/com/sun/j2ee/blueprints/petstore/tools/populate/CreditCardPopulator.java` : Utility class for populating credit card data into the database from XML files.
- `petstore/src/com/sun/j2ee/blueprints/petstore/tools/populate/AccountPopulator.java` : Populates Account entities with associated ContactInfo and CreditCard data for the PetStore application.
- `petstore/src/com/sun/j2ee/blueprints/petstore/tools/populate/PopulateUtils.java` : Utility class providing SQL execution methods for database population in the Pet Store application.
- `petstore/src/com/sun/j2ee/blueprints/petstore/tools/populate/CatalogPopulator.java` : Utility class for populating the PetStore catalog database with categories, products, and items from XML data.
- `petstore/src/com/sun/j2ee/blueprints/petstore/tools/populate/CategoryDetailsPopulator.java` : Utility class for populating category details data into a database from XML sources.
- `petstore/src/com/sun/j2ee/blueprints/petstore/tools/populate/XMLDBHandler.java` : SAX-based XML handler for database population in the Java Pet Store application.
- `petstore/src/com/sun/j2ee/blueprints/petstore/tools/populate/ProfilePopulator.java` : Utility class for populating Profile EJB entities from XML data in the Pet Store application.
- `petstore/src/com/sun/j2ee/blueprints/petstore/tools/populate/UserPopulator.java` : Populates user data in the Java Pet Store database from XML files
- `petstore/src/com/sun/j2ee/blueprints/petstore/tools/populate/AddressPopulator.java` : Utility class for populating address data in the Java Pet Store database from XML sources.
- `petstore/src/com/sun/j2ee/blueprints/petstore/tools/populate/ContactInfoPopulator.java` : Utility class for populating ContactInfo entities in the PetStore database from XML data.
- `petstore/src/com/sun/j2ee/blueprints/petstore/tools/populate/ProductDetailsPopulator.java` : Utility class for populating product details in the Pet Store database from XML data.

### XML Data Definitions
- `petstore/src/docroot/populate/dtds/PopulateSQL.dtd` : DTD file defining the XML structure for database population scripts in the Pet Store application.
- `petstore/src/docroot/populate/dtds/ProductDetails.dtd` : DTD file defining the structure for product details XML in the Pet Store application.
- `petstore/src/docroot/populate/dtds/Profile.dtd` : DTD file defining the structure of user profile XML documents in the Pet Store application.
- `petstore/src/docroot/populate/dtds/Item.dtd` : DTD file defining the XML structure for Item entities in the Java Pet Store application.
- `petstore/src/docroot/populate/dtds/Customer.dtd` : DTD file defining the structure of Customer XML elements in the Java Pet Store application.
- `petstore/src/docroot/populate/dtds/Account.dtd` : DTD file defining the Account element structure for XML validation in the PetStore application.
- `petstore/src/docroot/populate/dtds/ContactInfo.dtd` : DTD file defining the structure of ContactInfo XML elements for the Pet Store application.
- `petstore/src/docroot/populate/dtds/User.dtd` : DTD file defining the XML structure for User entities in the Java Pet Store application.
- `petstore/src/docroot/populate/dtds/ItemDetails.dtd` : DTD file defining the structure for item details XML data in the Pet Store application.
- `petstore/src/docroot/populate/dtds/Address.dtd` : DTD file defining the XML structure for address information in the Java Pet Store application.
- `petstore/src/docroot/populate/dtds/CreditCard.dtd` : DTD file defining the XML structure for credit card information in the Java Pet Store application.
- `petstore/src/docroot/populate/dtds/Product.dtd` : DTD file defining the Product element structure for the Java Pet Store application's XML data model.
- `petstore/src/docroot/populate/dtds/CategoryDetails.dtd` : DTD file defining the structure for CategoryDetails XML elements in the Java Pet Store application.
- `petstore/src/docroot/populate/dtds/Category.dtd` : DTD file defining the XML structure for Category entities in the Java Pet Store application.
- `petstore/src/docroot/populate/dtds/CommonElements.dtd` : DTD file defining common XML elements used across the Java Pet Store application's data structures.

### Sample Data
- `supplier/src/docroot/populate/Populate-UTF8.xml` : XML data file defining initial inventory quantities for supplier application products.
- `petstore/src/docroot/populate/Populate-UTF8.xml` : XML data file for populating the Java Pet Store database with multilingual product catalog information.
- `petstore/src/docroot/populate/PopulateSQL.xml` : XML configuration file defining SQL statements for database schema creation and population in the Java Pet Store application.
- `petstore/src/docroot/CatalogDAOSQL.xml` : XML configuration file defining SQL queries for catalog data access operations in the Pet Store application.

### User Interface Definitions
- `petstore/src/docroot/WEB-INF/screendefinitions_zh_CN.xml` : Chinese (Simplified) screen definitions XML file for Java Pet Store web application UI layout.
- `petstore/src/docroot/WEB-INF/screendefinitions_en_US.xml` : XML configuration file defining screen layouts and components for the Java Pet Store web application.
- `petstore/src/docroot/WEB-INF/screendefinitions_ja_JP.xml` : Japanese localization screen definitions for the Pet Store web application.

### Web Controller
- `petstore/src/com/sun/j2ee/blueprints/petstore/util/PetstoreKeys.java` : Defines constant keys used for data storage in the PetStore application's web tier.
- `petstore/src/com/sun/j2ee/blueprints/petstore/util/JNDINames.java` : Defines JNDI names for EJB home objects in the Java Pet Store application.
- `petstore/src/com/sun/j2ee/blueprints/petstore/controller/web/ShoppingWebController.java` : Web controller that proxies shopping-related events to the EJB tier in the Java Pet Store application.
- `petstore/src/com/sun/j2ee/blueprints/petstore/controller/web/exceptions/MissingFormDataException.java` : Custom exception for handling missing form data in the Java Pet Store web controller.
- `petstore/src/com/sun/j2ee/blueprints/petstore/controller/web/SignOnNotifier.java` : Implements a session attribute listener that notifies the Petstore backend when a user signs on.
- `petstore/src/com/sun/j2ee/blueprints/petstore/controller/web/BannerHelper.java` : Helper class that selects appropriate banner images based on product categories.
- `petstore/src/com/sun/j2ee/blueprints/petstore/controller/web/PetstoreComponentManager.java` : Component manager for the Java Pet Store web tier that manages session resources and provides access to EJB services.
- `petstore/src/com/sun/j2ee/blueprints/petstore/controller/web/flow/handlers/CreateUserFlowHandler.java` : Flow handler that directs users to appropriate screens after account creation.

### Web Actions
- `petstore/src/com/sun/j2ee/blueprints/petstore/controller/web/actions/CreateUserHTMLAction.java` : Handles user registration form submission in the Java Pet Store web application.
- `petstore/src/com/sun/j2ee/blueprints/petstore/controller/web/actions/CartHTMLAction.java` : Handles shopping cart actions in the Pet Store web application.
- `petstore/src/com/sun/j2ee/blueprints/petstore/controller/web/actions/CustomerHTMLAction.java` : Handles customer account creation and updates in the Java Pet Store web application.
- `petstore/src/com/sun/j2ee/blueprints/petstore/controller/web/actions/SignOffHTMLAction.java` : Handles user sign-off functionality in the Java Pet Store application
- `petstore/src/com/sun/j2ee/blueprints/petstore/controller/web/actions/OrderHTMLAction.java` : Processes order form data and creates order events in the Java Pet Store application.

### Controller Events
- `petstore/src/com/sun/j2ee/blueprints/petstore/controller/exceptions/DuplicateAccountException.java` : Custom exception class for handling duplicate user account scenarios in the Pet Store application.
- `petstore/src/com/sun/j2ee/blueprints/petstore/controller/exceptions/ShoppingCartEmptyOrderException.java` : Custom exception for empty shopping cart order attempts in the Java Pet Store application.
- `petstore/src/com/sun/j2ee/blueprints/petstore/controller/events/CreateUserEvent.java` : Event class for user creation in the Java Pet Store application.
- `petstore/src/com/sun/j2ee/blueprints/petstore/controller/events/CustomerEvent.java` : Event class representing customer data changes for the Pet Store controller
- `petstore/src/com/sun/j2ee/blueprints/petstore/controller/events/OrderEventResponse.java` : Represents a response event containing order information after an order has been placed.
- `petstore/src/com/sun/j2ee/blueprints/petstore/controller/events/CartEvent.java` : Defines a CartEvent class that represents shopping cart operations in the Java Pet Store application.
- `petstore/src/com/sun/j2ee/blueprints/petstore/controller/events/OrderEvent.java` : Event class representing order information for the Pet Store application's checkout process.
- `petstore/src/com/sun/j2ee/blueprints/petstore/controller/events/SignOnEvent.java` : Event class representing user sign-on actions in the Java Pet Store application.

### EJB Controller
- `petstore/src/com/sun/j2ee/blueprints/petstore/controller/ejb/ShoppingClientFacadeLocalEJB.java` : Facade EJB providing unified access to shopping-related components like cart and customer in the Pet Store application.
- `petstore/src/com/sun/j2ee/blueprints/petstore/controller/ejb/ShoppingControllerEJB.java` : Controller EJB that manages shopping functionality in the Java Pet Store application.
- `petstore/src/com/sun/j2ee/blueprints/petstore/controller/ejb/ShoppingControllerLocal.java` : Local interface for the Shopping Controller EJB that extends EJBControllerLocal.
- `petstore/src/com/sun/j2ee/blueprints/petstore/controller/ejb/ShoppingClientFacadeLocalHome.java` : Local home interface for ShoppingClientFacade EJB in the Java Pet Store application.
- `petstore/src/com/sun/j2ee/blueprints/petstore/controller/ejb/ShoppingControllerLocalHome.java` : Local home interface for ShoppingController EJB in the Java Pet Store application.
- `petstore/src/com/sun/j2ee/blueprints/petstore/controller/ejb/ShoppingClientFacadeLocal.java` : Interface defining shopping client facade for the Pet Store application

### EJB Actions
- `petstore/src/com/sun/j2ee/blueprints/petstore/controller/ejb/actions/CartEJBAction.java` : Handles shopping cart operations in the Java Pet Store application.
- `petstore/src/com/sun/j2ee/blueprints/petstore/controller/ejb/actions/CustomerEJBAction.java` : EJB action class that handles customer creation and update operations in the Pet Store application.
- `petstore/src/com/sun/j2ee/blueprints/petstore/controller/ejb/actions/SignOnEJBAction.java` : Handles user sign-on authentication and session setup in the Pet Store application.
- `petstore/src/com/sun/j2ee/blueprints/petstore/controller/ejb/actions/CreateUserEJBAction.java` : Handles user account creation in the Java Pet Store application by interfacing with SignOn EJB.
- `petstore/src/com/sun/j2ee/blueprints/petstore/controller/ejb/actions/OrderEJBAction.java` : Processes order submissions by creating purchase orders and sending them asynchronously for fulfillment.
- `petstore/src/com/sun/j2ee/blueprints/petstore/controller/ejb/actions/ChangeLocaleEJBAction.java` : Handles locale change events in the Java Pet Store application by updating user preferences.

### JNDI Configuration
- `opc/src/com/sun/j2ee/blueprints/opc/ejb/JNDINames.java` : Constants class defining JNDI names for EJBs and XML validation parameters in the Order Processing Center component.
- `supplier/src/com/sun/j2ee/blueprints/supplier/orderfulfillment/ejb/JNDINames.java` : Defines JNDI name constants for EJB components in the supplier order fulfillment system.

## Files
### admin/src/application.xml

This XML file serves as the J2EE application deployment descriptor for the Pet Store admin module. It defines the structure and components of the AdminEAR application, including module configurations for both web and EJB components. The file specifies that the application consists of a web module (petstoreadmin.war with context root 'admin') and an EJB module (asyncsender-ejb.jar). Additionally, it defines a security role named 'administrator' that controls access to the application's functionality.

 **Code Landmarks**
- `Line 48`: Defines the application's web context root as 'admin', determining the URL path for accessing the admin interface
- `Line 52`: Includes asyncsender-ejb.jar, suggesting asynchronous messaging functionality in the admin module
- `Line 55`: Establishes a security role 'administrator' that restricts access to authorized administrative users
### admin/src/build.bat

This batch file configures and executes the Apache Ant build system for the Java Pet Store admin application. It sets up the necessary environment variables including ANT_HOME and constructs the ANT_CLASSPATH with required JAR files from Java and J2EE environments. The script then launches the Java process to run Ant with specific parameters for building the application, including paths for documentation generation. It allows passing up to four command-line arguments to the Ant build process.

 **Code Landmarks**
- `Line 43`: Sets up a comprehensive classpath that includes Ant libraries, Java tools, and J2EE dependencies for the build process
- `Line 45`: Executes Ant with specific system properties that configure platform-specific script suffixes and documentation classpath settings
### admin/src/build.sh

This build script automates the compilation process for the admin application in Java Pet Store. It first checks for required environment variables (JAVA_HOME and J2EE_HOME), setting up appropriate paths if needed. The script then configures the classpath for Apache Ant, including necessary JAR files from both the JDK and J2EE environment. Finally, it executes Ant with the configured classpath and properties to build the admin application. Key variables include JAVA_HOME, J2EE_HOME, ANT_HOME, and ANT_CLASSPATH.

 **Code Landmarks**
- `Line 45`: Fallback mechanism to locate Java if JAVA_HOME isn't set by finding 'java' in PATH
- `Line 65`: Sets up classpath with multiple dependencies including Ant, XML tools, and J2EE libraries
### admin/src/build.xml

This build.xml file defines the Ant build process for the PetStore Admin application. It establishes targets for compiling Java classes, packaging WAR and EAR files, and deploying the application to a J2EE server. The script manages dependencies on several components including OPC, AsyncSender, ServiceLocator, and XMLDocuments. Key targets include init (setting properties), compile, war, ear, deploy, undeploy, and clean. The file defines important properties for paths, component locations, and deployment settings, creating a complete build pipeline for the administration module.

 **Code Landmarks**
- `Line 72-75`: Builds the classpath by combining multiple component JARs with J2EE libraries, showing component dependencies
- `Line 124-133`: Creates EAR file by assembling WAR and multiple EJB JARs with deployment descriptors
- `Line 156-164`: Uses J2EE deploytool commands to generate SQL and deploy the application to the server
- `Line 177-182`: Coordinates building of dependent components before building the main application
### admin/src/client/com/sun/j2ee/blueprints/admin/client/About.java

About implements a modal dialog box for displaying project information with animated visual elements. It consists of three classes: About (main dialog), AboutPanel (animation panel), and TranLabel (semi-transparent label). The AboutPanel features a waving Duke mascot animation and scrolling team member names with color gradient effects. Key functionality includes animation timing, alpha compositing for fade effects, and gradient painting. Important methods include setVisible(), start(), stop(), actionPerformed(), and paintComponent(). The timer-driven animation controls Duke's movement and name scrolling with fade transitions.

 **Code Landmarks**
- `Line 84`: Uses static initialization block to preload all animation frames for Duke mascot
- `Line 176`: Implements fade effect using AlphaComposite with decreasing opacity values
- `Line 94`: Uses RenderingHints for antialiasing to improve text rendering quality
### admin/src/client/com/sun/j2ee/blueprints/admin/client/AbstractItemAction.java

AbstractItemAction extends AbstractAction and implements ItemListener to provide support for item events in UI components like toggle buttons and checkbox menu items. It maintains a selected state that can be toggled and queried. The class provides methods to get and set the selection state (isSelected and setSelected), with the latter firing property change events when the state changes. Developers must subclass this abstract class and implement the actionPerformed and itemStateChanged methods to create functional actions for selectable UI components.

 **Code Landmarks**
- `Line 69`: Fires property change events when selection state changes, enabling automatic UI updates in Swing components
### admin/src/client/com/sun/j2ee/blueprints/admin/client/BarChartPanel.java

BarChartPanel implements a Swing panel for displaying bar charts of sales data in the Pet Store admin application. It creates a UI with date range selection fields and a graphical bar chart representation. The panel listens for property changes in the data model and updates accordingly. The nested BarChart class handles the actual rendering of the chart, including axes, bars, labels, and tooltips. Key functionality includes date range selection, data visualization, and interactive tooltips. Important components include the BarChartPanel class, BarChart inner class, and methods like createUI(), updateModelDates(), and renderChart().

 **Code Landmarks**
- `Line 152`: Custom tooltip implementation that shows order counts when hovering over chart bars
- `Line 99`: Property change listener pattern used to update UI when data model changes
- `Line 170`: Dynamic calculation of chart dimensions and scaling based on available data
### admin/src/client/com/sun/j2ee/blueprints/admin/client/Chart.java

Chart implements an abstract class for creating various chart visualizations in the Pet Store admin application. It provides a foundation for rendering different chart types using a common data model. The class maintains a static array of colors for chart elements and tracks total revenue and order counts. Key functionality includes abstract rendering methods and utilities for calculating data totals. Important elements include the renderChart abstract method, calculateTotals helper method, paintComponent override, colorList static array, and the constructor that accepts a DataSource.ChartModel parameter.

 **Code Landmarks**
- `Line 53`: Defines a reusable color palette that can be used across different chart implementations
- `Line 74`: Abstract renderChart method forces subclasses to implement specific chart visualization logic
- `Line 76`: calculateTotals method provides centralized data aggregation for all chart types
### admin/src/client/com/sun/j2ee/blueprints/admin/client/DataSource.java

DataSource serves as the central point for data exchange between the Pet Store admin client and server. It manages order data, sales statistics, and provides table models for UI components. The class implements data retrieval from a PetStoreProxy server, handles data refreshing, and maintains models for orders viewing, orders approval, pie charts, and bar charts. Key functionality includes retrieving order data, updating order statuses, and refreshing data from the server. Important components include OrdersViewTableModel, OrdersApproveTableModel, PieChartModel, BarChartModel, and the RefreshAction class. The DataSource uses property change support to notify UI components of data changes and server connectivity issues.

 **Code Landmarks**
- `Line 73`: Uses SwingPropertyChangeSupport to implement the observer pattern for notifying UI components of data changes
- `Line 176`: Implements lazy initialization pattern for various data models to improve performance
- `Line 234`: RefreshAction class extends ServerAction to handle asynchronous server communication
- `Line 347`: OrdersApproveTableModel implements a commit method that batches approved/denied orders for server updates
- `Line 486`: ChartModel abstract class provides common functionality for different chart types with template method pattern
### admin/src/client/com/sun/j2ee/blueprints/admin/client/HttpPostPetStoreProxy.java

HttpPostPetStoreProxy implements the PetStoreProxy interface to provide communication between the admin client and server components. It handles XML-based HTTP POST requests to retrieve order information, sales data, and update order statuses. The class manages connections to the server, parses XML responses into domain objects, and formats requests according to the expected protocol. Key methods include getOrders(), getRevenue(), updateStatus(), and helper methods for XML parsing and HTTP communication. Important variables include url for the server endpoint, documentBuilder for XML processing, and dateFormat for date handling.

 **Code Landmarks**
- `Line 92`: Custom XML request/response handling without using standard web service frameworks, showing a manual HTTP client implementation
- `Line 151`: XML parsing helper methods that extract typed values from nodes, demonstrating pre-JAXB XML handling techniques
- `Line 321`: Sales data retrieval methods dynamically adjust XML tag names based on whether category filtering is applied
### admin/src/client/com/sun/j2ee/blueprints/admin/client/OrdersApprovePanel.java

OrdersApprovePanel implements a Swing panel that allows administrators to review and change the status of pending orders in the Java Pet Store application. It displays orders in a sortable table with color-coded status cells (green for approved, red for denied, yellow for pending) and provides buttons for mass approval, denial, and committing changes. The panel listens for property change events to refresh the table data and enable/disable action buttons. Key components include the order table with custom cell rendering, status dropdown editor, and action buttons for batch operations.

 **Code Landmarks**
- `Line 156`: Custom cell renderer that changes background color based on order status (green/approved, red/denied, yellow/pending)
- `Line 93`: Implementation of mass approval/denial functionality through button actions that update multiple selected rows at once
- `Line 128`: Uses TableSorter wrapper around the data model to enable column-based sorting
- `Line 184`: PropertyChange event handling to update UI state based on data source changes
### admin/src/client/com/sun/j2ee/blueprints/admin/client/OrdersViewPanel.java

OrdersViewPanel implements a read-only view for displaying order data in the Java Pet Store admin client. It extends JPanel and implements PropertyChangeListener to update when order data changes. The panel creates a sortable table using TableSorter to display orders with their statuses. Key functionality includes creating the UI with a non-reorderable table that allows row selection, and responding to ORDER_DATA_CHANGED events from the DataSource. Important components include the orderTable JTable, tableModel for data, and sorter for column sorting capabilities.

 **Code Landmarks**
- `Line 66`: Uses TableSorter to add sortable columns to the JTable without modifying the underlying data model
- `Line 68`: Disables column reordering to maintain a consistent view of order data
- `Line 77`: Implements PropertyChangeListener pattern to update the UI when order data changes
### admin/src/client/com/sun/j2ee/blueprints/admin/client/PetStoreAdminClient.java

PetStoreAdminClient implements a Swing-based administration interface for the Java Pet Store application. It provides two main functional areas: order management (viewing and approving orders) and sales data visualization (pie and bar charts). The client connects to a server using a DataSource component that handles data retrieval and updates. Key functionality includes switching between orders and sales views, refreshing data from the server, and displaying localized UI components. Important classes include DataSource, OrdersViewPanel, OrdersApprovePanel, PieChartPanel, BarChartPanel, and various action classes for UI interactions. The UI is built with JTabbedPane components and uses PropertyChangeListeners for event handling.

 **Code Landmarks**
- `Line 80`: Uses a client-server model with a DataSource component that handles all server communication
- `Line 107`: Implements PropertyChangeListener pattern for communication between UI components
- `Line 155`: Comprehensive event listener registration system for updating different UI panels
- `Line 247`: Internationalization support through ResourceBundle for all UI text
- `Line 338`: Uses AbstractItemAction pattern to handle toggle button state synchronization
### admin/src/client/com/sun/j2ee/blueprints/admin/client/PetStoreProxy.java

PetStoreProxy interface defines the contract for client-server communication in the Pet Store admin application. It provides methods for retrieving and updating order information and generating sales reports. The interface includes two nested classes: Order, which represents order data with status constants (PENDING, DENIED, APPROVED, COMPLETED), and Sales, which stores revenue or order count information. Key methods include setup() for establishing server connection, getOrders() for retrieving orders by status, getRevenue() and getOrders() for sales reporting, and updateStatus() for changing order statuses.

 **Code Landmarks**
- `Line 46`: The interface defines a nested Order class with static constants for order status values
- `Line 117`: The Sales nested class uses -1 as a sentinel value to indicate which metric (revenue or orders) is being tracked
- `Line 159`: The setup method must be called before any operations, establishing a connection pattern
### admin/src/client/com/sun/j2ee/blueprints/admin/client/PieChartPanel.java

PieChartPanel implements a graphical component for displaying sales data as a pie chart in the Pet Store admin interface. It creates an interactive visualization showing category sales percentages with tooltips displaying exact values. The panel includes date range selection fields for filtering data and handles property change events to update the display. Key components include the main PieChartPanel class, the nested PieChart class for rendering, and integration with DataSource.PieChartModel for data access. Important methods include createUI(), updateModelDates(), propertyChange(), and the PieChart's renderChart() method.

 **Code Landmarks**
- `Line 147`: Implements interactive tooltips that display percentage information when hovering over pie chart segments
- `Line 118`: Uses PropertyChangeListener pattern to update UI when underlying data model changes
- `Line 95`: Handles date parsing and validation with error feedback through dialog boxes
### admin/src/client/com/sun/j2ee/blueprints/admin/client/ServerAction.java

ServerAction implements an abstract base class for creating GUI actions that perform work asynchronously. It extends AbstractAction and provides a framework for executing requests on a background thread while handling responses on the event dispatch thread. The class manages a shared WorkQueue for all ServerAction instances and implements a pattern where subclasses override request(), response(), and handleException() methods. The actionPerformed() method orchestrates the asynchronous execution flow, enqueueing work and ensuring proper thread handling between background operations and UI updates.

 **Code Landmarks**
- `Line 131`: Implements a thread-safe pattern for passing values between background worker threads and the EDT using synchronized getValue/setValue methods
- `Line 147`: Uses nested Runnable implementations to coordinate work between background threads and the Event Dispatch Thread
- `Line 178`: Implements lazy initialization of a shared WorkQueue that's used across all ServerAction instances
### admin/src/client/com/sun/j2ee/blueprints/admin/client/StatusBar.java

StatusBar implements a singleton JLabel subclass that provides a simple status bar for displaying messages at the bottom of the Java Pet Store admin application. The class follows the singleton pattern with a private constructor and a static getInstance() method that returns the single instance. Key functionality includes setting status messages through the setMessage() method, which updates the displayed text and triggers a repaint. The class also implements readResolve() to maintain singleton integrity during serialization. Important elements include the INSTANCE static field, getInstance() method, and setMessage() method.

 **Code Landmarks**
- `Line 50`: Implements the singleton pattern with a private static final instance
- `Line 62`: Implements readResolve() to maintain singleton integrity during deserialization
### admin/src/client/com/sun/j2ee/blueprints/admin/client/TableMap.java

TableMap implements a table model adapter that acts as a pass-through layer in a chain of table data manipulators. It extends DefaultTableModel and implements TableModelListener, routing all table model requests to its underlying model and all events to its listeners. The class provides methods to get and set the underlying model, and implements standard TableModel interface methods like getValueAt, setValueAt, getRowCount, getColumnCount, getColumnName, getColumnClass, and isCellEditable by delegating to the wrapped model. It also forwards TableModelEvents through the tableChanged method.

 **Code Landmarks**
- `Line 46`: The class serves as both an adapter and potential base class for table filters that only need to override specific methods
- `Line 59`: The model registration automatically sets up event listening with addTableModelListener
### admin/src/client/com/sun/j2ee/blueprints/admin/client/TableSorter.java

TableSorter extends TableMap to provide sorting capabilities for JTable components. It maintains an array of indexes to map between the sorted view and the underlying data model without duplicating data. The class implements various comparison methods for different data types (Number, Date, String, Boolean), supports multi-column sorting, and provides stable sorting algorithms. Key functionality includes adding click-to-sort capability to table headers with visual indicators (up/down arrows). Important methods include compareRowsByColumn(), sort(), shuttlesort(), sortByColumn(), and addMouseListenerToHeaderInTable(). The class also handles table model changes by reallocating indexes and resorting data.

 **Code Landmarks**
- `Line 143`: Implements type-specific comparison logic for different column data types (Number, Date, String, Boolean)
- `Line 237`: Uses a stable shuttlesort algorithm that preserves relative order of equal elements
- `Line 245`: Contains an optimization for partially ordered lists to improve sorting performance
- `Line 358`: Implements custom header renderer that displays sort direction indicators (up/down arrows)
### admin/src/client/com/sun/j2ee/blueprints/admin/client/ToggleActionPropertyChangeListener.java

ToggleActionPropertyChangeListener implements a PropertyChangeListener that ensures toggle state synchronization between UI components created from the same AbstractItemAction. When attached to AbstractButton instances (buttons or menu items), it listens for 'selected' property changes and updates the button's selected state accordingly. This maintains consistency across multiple UI components controlled by the same action. The class contains a reference to the AbstractButton it monitors and implements the propertyChange method to handle selection state changes.

 **Code Landmarks**
- `Line 53`: The comment indicates the button reference should be a WeakRef to prevent memory leaks since it may become unreachable
### admin/src/client/com/sun/j2ee/blueprints/admin/client/WorkQueue.java

WorkQueue implements a thread-safe queue for asynchronous task execution using a producer-consumer pattern. It creates a dedicated worker thread that processes Runnable tasks submitted to the queue. The class provides methods to enqueue work items and stop the queue processing. When tasks are added to the queue, the worker thread is notified and executes them sequentially. Key components include the queue (implemented as a LinkedList), a worker thread that runs at minimum priority, and synchronization mechanisms to ensure thread safety. Important methods include enqueue(Runnable), stop(), and the inner WorkerThread class that handles the execution loop.

 **Code Landmarks**
- `Line 48`: Uses a private inner class WorkerThread to encapsulate the consumer thread implementation
- `Line 55`: Thread synchronization using wait/notify pattern for efficient blocking queue implementation
- `Line 56`: Sets worker thread to minimum priority to avoid competing with more important threads
### admin/src/client/resources/petstore.properties

This properties file contains localized strings for the Pet Store Administration client application. It defines text resources for UI components including action names, mnemonics, tooltips, panel titles, button labels, dialog messages, and table column headers. The file organizes strings into logical sections for application actions, orders panels, frames/dialogs, sales tab elements, status bar messages, table columns, and chart configuration. These properties support internationalization and provide consistent text throughout the application interface.

 **Code Landmarks**
- `Line 1-18`: Comprehensive action definition pattern with name, mnemonic, tooltip and description for each UI action
- `Line 47-52`: Table column naming convention that prefixes each property with the component name (OrdersTable)
- `Line 54-62`: Chart configuration properties that mix display text with numerical configuration values
### admin/src/client/resources/petstore_de.properties

This properties file contains German language translations for the Pet Store Admin client application. It defines localized text strings for UI elements including menu actions, dialog messages, button labels, and status indicators. The file organizes translations for various application components including file operations, order management panels, sales charts, and status messages. Key sections include action names with mnemonics, order panel labels, dialog messages, chart configuration strings, and status indicators for order processing states and pet categories.

 **Code Landmarks**
- `Line 1-15`: Implements complete menu action localization with name, mnemonic, tooltip and description for each action
### admin/src/com/sun/j2ee/blueprints/admin/web/AdminBDException.java

No summary available
### admin/src/com/sun/j2ee/blueprints/admin/web/AdminRequestBD.java

No summary available
### admin/src/com/sun/j2ee/blueprints/admin/web/AdminRequestProcessor.java

No summary available
### admin/src/com/sun/j2ee/blueprints/admin/web/ApplRequestProcessor.java

No summary available
### admin/src/docroot/WEB-INF/web.xml

This web.xml deployment descriptor configures the web application component of the PetStore Admin module. It defines two servlets (AdminRequestProcessor and ApplRequestProcessor) with their URL mappings, sets a 54-minute session timeout, and specifies index.html as the welcome file. The file implements form-based authentication security constraints requiring the 'administrator' role to access the AdminRequestProcessor. It also defines EJB references including a remote OPCAdminFacadeRemote and a local AsyncSender EJB, establishing the connection points between the web tier and business logic components.

 **Code Landmarks**
- `Line 86`: Security constraint implementation restricting AdminRequestProcessor access to users with administrator role
- `Line 98`: Form-based authentication configuration with custom login and error pages
- `Line 111`: Remote EJB reference declaration for OPC admin functionality
- `Line 118`: Local EJB reference with explicit EJB link to AsyncSenderEJB for messaging
### admin/src/docroot/index.html

This HTML file serves as the landing page for the Java Pet Store Admin interface. It provides a minimal interface with a title, heading, and a single hyperlink that directs users to the AdminRequestProcessor servlet to access the administration functionality. The page has a white background and includes copyright notices for Sun Microsystems from 2001.

 **Code Landmarks**
- `Line 17`: The link to '/admin/AdminRequestProcessor' suggests the admin functionality is implemented as a servlet that processes administrative requests.
### admin/src/sun-j2ee-ri.xml

This XML configuration file defines the J2EE Reference Implementation specific settings for the Pet Store admin application. It specifies role mappings for administrator access, web module configuration with context root and EJB references, and enterprise bean configurations for the AsyncSenderEJB. The file establishes JNDI names, security settings, resource references for JMS queue connections, and resource environment references. It connects the admin web interface to backend EJB components and configures security and messaging infrastructure needed by the admin application.

 **Code Landmarks**
- `Line 45`: Role mapping configuration assigns the 'administrator' role to principal 'jps_admin' and group 'administrator_group'
- `Line 54`: Web module configuration sets the context root to 'admin' for the admin interface URL path
- `Line 56`: EJB reference maps the logical name 'ejb/OPCAdminFacadeRemote' to the JNDI name for remote access
- `Line 79`: Security configuration for the AsyncSenderEJB requiring username/password authentication
- `Line 92`: JMS resource configuration connecting the AsyncSenderQueue to the OrderApprovalQueue for asynchronous order processing
### build.xml

This build.xml file serves as the main build configuration for the Java Pet Store application's components. It defines targets for building, deploying, verifying, undeploying, and cleaning the core application modules. The file orchestrates build processes by delegating to subordinate build files in the admin, opc, petstore, and supplier directories. Each target in this file invokes the corresponding target in each component's build file, providing a centralized way to manage the build lifecycle across all application modules.

 **Code Landmarks**
- `Line 46`: The build file uses a hierarchical structure to coordinate builds across multiple application components
- `Line 49-54`: Core target builds all four application components in sequence, showing the modular architecture of the Pet Store application
- `Line 56-61`: Deploy target demonstrates the deployment workflow across all application components
### opc/src/application.xml

This application.xml file serves as the J2EE deployment descriptor for the OrderProcessingCenterEAR application. It defines the structure of the enterprise application by declaring the modules that compose it. The file includes five modules: four EJB modules (opc-ejb.jar, po-ejb.jar, mailer-ejb.jar, and processmanager-ejb.jar) and one web module (opc.war with context root '/opc'). This XML configuration follows the J2EE 1.3 standard and enables the application server to properly deploy and integrate all components of the Order Processing Center application.

 **Code Landmarks**
- `Line 44`: Uses J2EE 1.3 DTD specification for the application descriptor format
- `Line 47-49`: Provides display name and description that identify this as the Order Processing Center application
- `Line 63-67`: Configures the web module with both the WAR file location and the context root path
### opc/src/build.bat

This batch file serves as a build script for the OPC (Order Processing Center) component of the Java Pet Store application on Windows systems. It configures the environment for Apache Ant by setting necessary environment variables including ANT_HOME and constructing the ANT_CLASSPATH with required JAR files. The script then executes the Java interpreter to run Ant with appropriate parameters, passing along any command-line arguments. Key environment variables include ANT_HOME, ANT_CLASSPATH, JAVA_HOME, and J2EE_HOME, which are essential for the build process.

 **Code Landmarks**
- `Line 46`: Sets up a comprehensive classpath that includes Ant libraries, Java tools, and J2EE libraries for building enterprise components
- `Line 47`: Uses parameter passing (%1 %2 %3 %4) to allow flexible Ant target execution from command line
### opc/src/build.sh

This shell script automates the build process for the OPC (Order Processing Center) component of Java Pet Store. It first checks for required environment variables (JAVA_HOME and J2EE_HOME), setting up Java command paths if needed. The script then configures the Ant build environment by setting the classpath to include necessary JAR files from Ant, Java tools, and J2EE libraries. Finally, it executes the Ant build tool with the specified command-line arguments, passing along the J2EE home directory and other configuration parameters needed for the build process.

 **Code Landmarks**
- `Line 41`: Checks for JAVA_HOME environment variable and attempts to locate Java automatically if not set
- `Line 53`: Validates J2EE_HOME environment variable which is required for the build process
- `Line 64`: Constructs the Ant classpath with all necessary dependencies for building the application
### opc/src/build.xml

This build.xml file defines the build process for the Order Processing Center (OPC) component of the Java Pet Store application. It manages compilation, packaging, and deployment of the OPC module. The script defines targets for initializing properties, compiling source code, creating EJB JARs and client JARs, building WAR files, assembling EAR packages, and deploying to J2EE servers. It also includes targets for cleaning, generating documentation, and verifying deployments. The file establishes dependencies on other components like xmldocuments, servicelocator, purchaseorder, mailer, and processmanager, and defines paths for source code, build artifacts, and deployment destinations.

 **Code Landmarks**
- `Line 47`: Defines the project structure with a hierarchical organization of components and build artifacts
- `Line 124`: Creates a comprehensive classpath that includes all dependent components and J2EE libraries
- `Line 190`: Extracts schema files from JAR archives to make them available in the web application
- `Line 241`: Uses the J2EE deploytool to generate SQL and deploy the application to the server
- `Line 267`: Builds comprehensive JavaDoc documentation that includes all related components
### opc/src/com/sun/j2ee/blueprints/opc/admin/ejb/OPCAdminFacade.java

OPCAdminFacade interface defines the remote interface for the Order Processing Center (OPC) admin client. It extends EJBObject to provide remote access to administrative functionality. The interface declares two key methods: getOrdersByStatus for retrieving orders filtered by their status, and getChartInfo for obtaining chart data based on date ranges and categories. Both methods can throw RemoteException for network-related issues and OPCAdminFacadeException for business logic errors. This interface is part of the EJB tier in the Java Pet Store's order processing subsystem.

 **Code Landmarks**
- `Line 48`: Interface extends EJBObject, making it a remote EJB interface accessible across network boundaries
- `Line 50`: Returns OrdersTO transfer object, demonstrating use of Data Transfer Object pattern for remote communication
### opc/src/com/sun/j2ee/blueprints/opc/admin/ejb/OPCAdminFacadeEJB.java

OPCAdminFacadeEJB implements a session bean that serves as a facade for administrative operations in the Order Processing Center. It provides methods to retrieve orders by status and generate chart information about revenue and orders within specified date ranges. The class interacts with PurchaseOrder and ProcessManager EJBs through local interfaces. Key methods include getOrdersByStatus() which returns order details for a given status, and getChartInfo() which generates revenue or order quantity data that can be filtered by category or item. The class uses ServiceLocator for dependency injection and handles various exceptions through OPCAdminFacadeException.

 **Code Landmarks**
- `Line 103`: Uses ServiceLocator pattern to obtain EJB references, demonstrating J2EE best practices for dependency lookup
- `Line 149`: Implements a facade pattern to shield clients from the complexity of interacting with multiple EJBs
- `Line 195`: Complex data aggregation logic that processes order data to generate business intelligence metrics
### opc/src/com/sun/j2ee/blueprints/opc/admin/ejb/OPCAdminFacadeException.java

OPCAdminFacadeException implements a custom exception class that extends the standard Java Exception class. It serves as a specialized exception for the Order Processing Center (OPC) administration facade in the Java Pet Store application. The class is designed to be thrown when user errors occur during administration operations, such as invalid field inputs. The class provides two constructors: a default constructor with no arguments and another that accepts a string parameter to explain the exception condition. This exception helps in providing meaningful error handling for the OPC administration component.
### opc/src/com/sun/j2ee/blueprints/opc/admin/ejb/OPCAdminFacadeHome.java

OPCAdminFacadeHome interface defines the home interface for the Order Processing Center (OPC) Admin facade Enterprise JavaBean. It extends the standard EJBHome interface from the J2EE framework, providing the contract for client applications to obtain references to the OPC administration facade. The interface declares a single create() method that returns an OPCAdminFacade remote interface, allowing clients to create instances of the bean for managing OPC administrative operations. This interface follows the standard EJB pattern for remote access to enterprise beans, throwing RemoteException and CreateException when appropriate.

 **Code Landmarks**
- `Line 47`: Implements the standard EJB home interface pattern required for client access to enterprise beans in J2EE applications
### opc/src/com/sun/j2ee/blueprints/opc/admin/ejb/OrderDetails.java

OrderDetails implements a serializable value object that encapsulates order information for the admin client in the Java Pet Store application. It stores essential order attributes including order ID, user ID, order date, monetary value, and status. The class provides a constructor to initialize all fields and getter methods to access each property. This simple data transfer object facilitates the transmission of order information between the enterprise beans and the administration client interface.
### opc/src/com/sun/j2ee/blueprints/opc/admin/ejb/OrdersTO.java

OrdersTO defines a serializable interface for transferring collections of orders between system components in the Java Pet Store application. It extends standard Collection interface methods like iterator(), size(), contains(), and isEmpty(). The interface includes a nested static class MutableOrdersTO that extends ArrayList and implements OrdersTO, providing a concrete implementation for order collection transfer. This transfer object pattern facilitates data exchange between the Order Processing Center (OPC) admin EJB components and client applications while maintaining loose coupling.

 **Code Landmarks**
- `Line 45`: Uses the Transfer Object pattern to encapsulate business data for client-tier consumption
- `Line 58`: Implements a nested static class that provides a concrete ArrayList-based implementation of the interface
### opc/src/com/sun/j2ee/blueprints/opc/customerrelations/ejb/JNDINames.java

JNDINames is a utility class that centralizes JNDI name constants used throughout the OPC customer relations module. It defines string constants for EJB references and environment variables needed for the application's operation. The class includes JNDI names for the PurchaseOrder EJB and various email notification settings (order confirmation, approval, and completion emails). It also defines constants for XML validation parameters including invoice validation, order approval validation, XSD validation, and entity catalog URL. The class has a private constructor to prevent instantiation, as it's designed to be used statically.

 **Code Landmarks**
- `Line 48`: Private constructor prevents instantiation of this utility class, enforcing its use as a static constants container only.
- `Line 60-76`: Email notification configuration parameters are defined as environment variables, allowing deployment-time configuration of notification behavior.
### opc/src/com/sun/j2ee/blueprints/opc/customerrelations/ejb/LocaleUtil.java

LocaleUtil provides a utility method for converting string representations of locales into Java Locale objects within the OPC customer relations module. The class implements a single static method, getLocaleFromString(), which parses locale strings formatted with language, country, and optional variant components separated by underscores. The method handles special cases like null inputs and 'default' locale strings, returning appropriate Locale objects or null when parsing fails. This utility supports internationalization features in the Java Pet Store application by facilitating locale-based customization.

 **Code Landmarks**
- `Line 47`: The method parses locale strings with a specific format (language_country_variant) using string manipulation rather than built-in locale parsing methods.
- `Line 48`: Special handling for 'default' string returns the system's default locale rather than parsing it as a locale string.
### opc/src/com/sun/j2ee/blueprints/opc/customerrelations/ejb/MailCompletedOrderMDB.java

MailCompletedOrderMDB implements a message-driven bean that processes JMS messages containing completed order information. It generates and sends email notifications to customers when their orders are fully shipped. The class retrieves order details, formats them using XSL transformation, creates mail messages, and sends them through a mailer service. Key functionality includes message processing, email content generation, and transition handling. Important components include the onMessage() method for processing incoming messages, doWork() for generating email content, and doTransition() for sending emails. The class uses ServiceLocator to access configuration and EJB references.

 **Code Landmarks**
- `Line 116`: Uses XSL transformation to format order data into customer-friendly email content
- `Line 91`: Implements conditional email sending based on configuration parameter retrieved from ServiceLocator
- `Line 157`: Leverages TransitionDelegate pattern to decouple message sending from the core business logic
### opc/src/com/sun/j2ee/blueprints/opc/customerrelations/ejb/MailContentXDE.java

MailContentXDE extends XMLDocumentEditor.DefaultXDE to format email content by applying XSL stylesheets to XML documents. It manages transformers for different locales, caches them for reuse, and provides methods to set XML source documents and retrieve formatted output. The class includes a nested FormatterException for error handling with root cause tracking. Key functionality includes locale-specific stylesheet selection, XML transformation, and output encoding management. Important methods include getTransformer(), setDocument(), getDocument(), getDocumentAsString(), and format(). The class uses TransformerFactory to create XSLT processors and maintains a HashMap of transformers keyed by locale.

 **Code Landmarks**
- `Line 91`: Implements recursive root cause exception tracking pattern for better error diagnosis
- `Line 137`: Uses locale-based resource path construction for internationalized XSL stylesheets
- `Line 187`: Lazy initialization pattern for transformation results improves performance
### opc/src/com/sun/j2ee/blueprints/opc/customerrelations/ejb/MailInvoiceMDB.java

MailInvoiceMDB implements a message-driven bean that processes JMS messages containing invoice information for shipped orders. It transforms the invoice XML into a formatted HTML email and sends it to customers. Key functionality includes parsing invoice XML documents, retrieving purchase order details, formatting email content using XSL stylesheets, and sending the email through a mail service. Important components include the onMessage() method for JMS message handling, doWork() for email content generation, and doTransition() for email delivery. The class uses TransitionDelegate for workflow management and various XDE classes for XML document processing.

 **Code Landmarks**
- `Line 89`: Uses service locator pattern to obtain configuration values and EJB references
- `Line 113`: Implements JMS MessageListener interface to receive asynchronous invoice notifications
- `Line 149`: Transforms XML invoice into HTML email using XSL stylesheet
- `Line 166`: Uses transition delegate pattern to handle workflow progression
### opc/src/com/sun/j2ee/blueprints/opc/customerrelations/ejb/MailOrderApprovalMDB.java

MailOrderApprovalMDB implements a message-driven bean that processes JMS messages containing OrderApproval XML documents. It extracts order information, formats email content using XSL transformation, and sends notification emails to customers about their order status. Key functionality includes parsing XML messages, retrieving purchase order details, generating HTML email content, and forwarding mail messages to a mailer service. Important methods include onMessage(), doWork(), and doTransition(). The class uses ServiceLocator to retrieve configuration settings and dependencies like PurchaseOrderLocalHome.

 **Code Landmarks**
- `Line 118`: Uses XML Document Exchange (XDE) pattern with XSL transformation to convert order data into HTML email content
- `Line 151`: Implements conditional email sending based on configuration parameter 'sendConfirmationMail'
- `Line 168`: Uses TransitionDelegate pattern to decouple message processing from delivery mechanism
- `Line 186`: Processes collections of order approvals in a single message, generating multiple emails
### opc/src/com/sun/j2ee/blueprints/opc/ejb/InvoiceMDB.java

InvoiceMDB implements a message-driven bean that processes JMS messages containing invoice information for customer orders. It updates purchase order status based on shipped items and manages order completion workflow. Key functionality includes extracting invoice data from XML messages, updating purchase orders through the PurchaseOrderLocal interface, and triggering transitions when orders are complete. The class uses ServiceLocator to obtain EJB references and XML validation services. Important methods include onMessage(), doWork(), and doTransition(), with supporting components like TPAInvoiceXDE for XML processing and TransitionDelegate for workflow management.

 **Code Landmarks**
- `Line 119`: Uses a service locator pattern to obtain EJB references and configuration parameters
- `Line 134`: Implements JMS message handling to process XML invoices in an asynchronous manner
- `Line 175`: Implements a workflow state transition system for order processing completion
### opc/src/com/sun/j2ee/blueprints/opc/ejb/JNDINames.java

JNDINames implements a utility class that serves as a central repository for JNDI names used in the Order Processing Center (OPC) component. It defines static final String constants for EJB references (ProcessManager and PurchaseOrder) and XML validation parameters for various document types (PurchaseOrder, Invoice, OrderApproval). The class also includes constants for XML validation configuration, entity catalog URL, and transition delegate references. The class has a private constructor to prevent instantiation, as it's designed to be used statically. These JNDI names must be synchronized with the deployment descriptors to ensure proper component lookup.

 **Code Landmarks**
- `Line 47`: Private constructor prevents instantiation of this utility class
- `Line 49-71`: Constants follow a consistent naming pattern with java:comp/env prefix, indicating they're environment entries in the JNDI context
### opc/src/com/sun/j2ee/blueprints/opc/ejb/OrderApprovalMDB.java

OrderApprovalMDB implements a message-driven bean that processes JMS messages containing order approval status updates. It receives order approval XML messages, updates the purchase order status in the database, generates supplier purchase orders for approved orders, and prepares notifications for customer relations. The class uses a transition delegate pattern to handle the actual sending of messages to suppliers and customer relations. Key methods include onMessage() which processes incoming JMS messages, doWork() which handles the business logic of processing approvals, and doTransition() which delegates the sending of messages. Important variables include processManager for updating order status, transitionDelegate for handling transitions, and supplierOrderXDE for XML document handling.

 **Code Landmarks**
- `Line 90`: Uses an inner class WorkResult to bundle multiple return values from the doWork method
- `Line 152`: Implements JMS message handling with state validation before processing order approvals
- `Line 197`: Uses a transition delegate pattern to abstract the actual sending of messages
- `Line 221`: Converts internal purchase order data to supplier-specific XML format
### opc/src/com/sun/j2ee/blueprints/opc/ejb/PurchaseOrderMDB.java

PurchaseOrderMDB implements a message-driven bean that receives JMS messages containing purchase orders from Java Pet Store customers. It creates PurchaseOrder EJBs to begin order fulfillment and manages the workflow process. For small orders (under $500 for US or ¥50000 for Japan), it automatically approves them and forwards to OrderApproval queue, while larger orders await administrator approval. Key methods include onMessage() which processes incoming JMS messages, doWork() which creates purchase orders and initiates workflow, and doTransition() which handles order approval transitions. The class uses ServiceLocator to obtain references to EJB homes and interacts with the ProcessManager for workflow management.

 **Code Landmarks**
- `Line 124`: Implements automatic order approval logic based on order total and locale
- `Line 94`: Uses TransitionDelegate pattern to handle workflow transitions between system components
- `Line 83`: Leverages ServiceLocator pattern to obtain EJB references and configuration parameters
### opc/src/com/sun/j2ee/blueprints/opc/ejb/TPAInvoiceXDE.java

TPAInvoiceXDE implements an XML document editor for TPA invoices, extending XMLDocumentEditor.DefaultXDE. It parses invoice XML documents to extract order IDs and line item quantities. The class provides methods to set and retrieve documents from various sources, validate against DTD or XSD schemas, and access the extracted order data. Key functionality includes XML parsing, data extraction, and document transformation. Important constants define XML namespaces and element names, while key methods include setDocument(), getOrderId(), getLineItemIds(), and extractData(). The class maintains state through orderId, lineItemIds, and invoiceDocument variables.

 **Code Landmarks**
- `Line 82`: Uses a transformer to deserialize XML from various sources with configurable validation options
- `Line 106`: Extracts structured data from XML document using namespace-aware parsing methods
- `Line 125`: Contains a main() method allowing the class to be used as a standalone invoice parser
### opc/src/com/sun/j2ee/blueprints/opc/transitions/InvoiceTD.java

InvoiceTD implements the TransitionDelegate interface for handling invoice-related transitions in the order processing component. It facilitates sending completed order information to a JMS queue for further processing. The class sets up JMS resources during initialization, including obtaining a QueueConnectionFactory and Queue through the ServiceLocator. The main functionality is in the doTransition method, which takes TransitionInfo containing XML order data and sends it as a message to the queue. Key components include the setup() method for resource initialization and the QueueHelper utility for message sending.

 **Code Landmarks**
- `Line 53`: Uses ServiceLocator pattern to obtain JMS resources, promoting loose coupling
- `Line 67`: Implements TransitionDelegate interface as part of a state transition system for order processing
### opc/src/com/sun/j2ee/blueprints/opc/transitions/JNDINames.java

JNDINames is a utility class that centralizes JNDI name constants for various JMS resources used in the order processing component (OPC) of Java Pet Store. It defines string constants for queue connection factory and several message queues related to order processing, including order approval, customer notification emails for order status and completed orders, supplier purchase orders, and a mail sender queue. The class has a private constructor to prevent instantiation, as it only serves as a container for static constants. These JNDI names must match the corresponding entries in deployment descriptors.

 **Code Landmarks**
- `Line 46`: Private constructor prevents instantiation of this utility class that only contains constants
- `Line 48-65`: JNDI names follow a consistent pattern with java:comp/env prefix, indicating they are environment entries defined in deployment descriptors
### opc/src/com/sun/j2ee/blueprints/opc/transitions/MailCompletedOrderTD.java

MailCompletedOrderTD implements a transition delegate responsible for sending email notifications to customers when their orders are completed. It uses JMS (Java Message Service) to communicate with a mail service. The class sets up JMS resources in the setup() method by obtaining queue connection factory and queue references through a ServiceLocator. The doTransition() method sends the XML message containing order details to the mail queue. Key components include QueueHelper for JMS operations, setup() for resource initialization, and doTransition() for sending the notification message.

 **Code Landmarks**
- `Line 58`: Uses ServiceLocator pattern to obtain JMS resources, promoting loose coupling
- `Line 72`: Implements TransitionDelegate interface as part of a process management framework
### opc/src/com/sun/j2ee/blueprints/opc/transitions/MailInvoiceTransitionDelegate.java

MailInvoiceTransitionDelegate implements a transition delegate responsible for sending invoice emails to customers in the Java Pet Store application. It implements the TransitionDelegate interface with two main methods: setup() which initializes JMS resources by obtaining a queue connection factory and queue through the ServiceLocator, and doTransition() which sends XML-formatted mail messages to the mail queue. The class uses QueueHelper to handle the actual JMS message sending operations. Key components include the QueueHelper, Queue, and QueueConnectionFactory objects that facilitate communication with the mail sender service.

 **Code Landmarks**
- `Line 54`: Uses the Service Locator pattern to obtain JMS resources, reducing direct JNDI lookups
- `Line 71`: Implements asynchronous email notification through JMS messaging, decoupling the order processing from email delivery
### opc/src/com/sun/j2ee/blueprints/opc/transitions/MailOrderApprovalTransitionDelegate.java

MailOrderApprovalTransitionDelegate implements the TransitionDelegate interface to handle email notifications for order approvals in the Java Pet Store application. It establishes JMS connections to send email messages to customers. The class contains methods for setting up resources (setup) and executing the transition (doTransition), which processes XML mail messages from a collection and sends them to a mail queue. Key components include QueueHelper for JMS operations and ServiceLocator for obtaining JMS resources. The class handles exceptions by wrapping them in TransitionException objects.

 **Code Landmarks**
- `Line 58`: Uses the Service Locator pattern to obtain JMS resources, reducing JNDI lookup code
- `Line 73`: Processes a batch of XML messages rather than individual messages, improving efficiency
- `Line 76`: Uses JMS for asynchronous email delivery, decoupling order processing from notification
### opc/src/com/sun/j2ee/blueprints/opc/transitions/OrderApprovalTD.java

OrderApprovalTD implements a transition delegate responsible for processing approved orders in the Java Pet Store application. It manages the communication between the order processing component and external systems by sending purchase orders to suppliers and order approval/denial notifications to customers. The class uses JMS queues to handle these asynchronous communications. Key methods include setup() which initializes queue connections, doTransition() which processes supplier purchase orders and customer notifications, and sendMail() which sends order status notifications. Important variables include qFactory, mailQueue, supplierPoQueue, and the helper classes mailQueueHelper and supplierQueueHelper.

 **Code Landmarks**
- `Line 74`: Uses ServiceLocator pattern to obtain JMS resources, abstracting JNDI lookup complexity
- `Line 89`: Processes a batch of supplier purchase orders and a consolidated customer notification in a single transaction
- `Line 95`: Iterates through collection of purchase orders, sending each to supplier queue individually
### opc/src/com/sun/j2ee/blueprints/opc/transitions/PurchaseOrderTD.java

PurchaseOrderTD implements the TransitionDelegate interface for processing purchase orders in the OPC component. It facilitates communication with the OrderApproval queue by setting up JMS resources and sending XML messages. The setup() method initializes queue connections using ServiceLocator, while doTransition() sends XML order approval messages to the queue. The class handles exceptions by wrapping them as TransitionException instances. Key components include QueueConnectionFactory, Queue, and QueueHelper for JMS operations, making it a critical link in the order fulfillment workflow.

 **Code Landmarks**
- `Line 57`: Uses the Service Locator pattern to obtain JMS resources, promoting loose coupling
- `Line 71`: Implements asynchronous messaging for order processing, enabling system scalability
### opc/src/com/sun/j2ee/blueprints/opc/transitions/QueueHelper.java

QueueHelper implements a serializable utility class that simplifies sending JMS messages to a queue within the Order Processing Center (OPC) component. It encapsulates the JMS connection factory and queue destination, providing a clean interface for sending XML messages. The class manages the lifecycle of JMS resources (connection, session, sender) and ensures proper cleanup through try-finally blocks. The main functionality is in the sendMessage method, which creates a JMS text message containing the provided XML content and sends it to the configured queue. Key components include the QueueConnectionFactory and Queue instance variables, and the sendMessage method that handles all JMS operations.

 **Code Landmarks**
- `Line 66`: Implements proper JMS resource management with try-finally block to ensure connection cleanup even if exceptions occur
- `Line 59`: Creates a transactional JMS session (first parameter true) which ensures message delivery reliability
### opc/src/docroot/WEB-INF/web.xml

This web.xml file serves as the deployment descriptor for the Order Processing Center (OPC) web application component of Java Pet Store. It defines the basic configuration for the web application following the Servlet 2.3 specification. The file contains minimal configuration, only specifying the display name 'OrderProcessingCenterWAR' and a brief description identifying it as the Web Tier deployment descriptor for the OPC application. The document includes Sun Microsystems' copyright notice and redistribution terms.

 **Code Landmarks**
- `Line 44`: Uses the Servlet 2.3 DTD specification, indicating this is designed for J2EE 1.3 compatible application servers
### opc/src/docroot/schemas/EntityCatalog.properties

EntityCatalog.properties is a configuration file that maps XML DTD and schema identifiers to their physical file locations within the application. It defines mappings for TPA-LineItem, TPA-SupplierOrder, and TPA-Invoice document types, both for DTD format (using public identifiers) and XSD format (using HTTP URLs). These mappings enable XML parsers to locate the correct schema definitions when validating XML documents in the Java Pet Store application's order processing component.

 **Code Landmarks**
- `Line 1-3`: Maps DTD public identifiers to local file paths using Sun Microsystems J2EE Blueprints Group namespace
- `Line 4-6`: Maps HTTP URLs to local XSD schema files using blueprints.j2ee.sun.com domain
### opc/src/ejb-jar-manifest.mf

This manifest file defines the Class-Path entries for an Enterprise JavaBeans (EJB) JAR file in the OPC (Order Processing Center) application. It specifies five JAR dependencies that must be available at runtime: po-ejb-client.jar (purchase order client), mailer-ejb-client.jar (email functionality), processmanager-ejb-client.jar (process management), servicelocator.jar (service location utilities), and xmldocuments.jar (XML document handling). These dependencies represent the client interfaces and utilities needed by the EJB components.
### opc/src/sun-j2ee-ri.xml

This XML file is a J2EE Reference Implementation deployment descriptor for the OPC (Order Processing Center) application in Java Pet Store. It defines the configuration for various Enterprise JavaBeans (EJBs), web modules, and their dependencies. The file maps logical EJB names to JNDI names, configures Message-Driven Beans (MDBs) for handling purchase orders, invoices, and customer communications, and establishes database connections for Container-Managed Persistence (CMP). It includes detailed SQL statements for database operations, security configurations, and resource references for JMS queues, mail sessions, and other services. The descriptor organizes the application into multiple modules including opc.war, opc-ejb.jar, mailer-ejb.jar, processmanager-ejb.jar, and po-ejb.jar, each with specific roles in the order processing workflow.

 **Code Landmarks**
- `Line 43`: Defines the context root for the web application as 'opc', determining the URL path for accessing the application
- `Line 76`: Configuration for PurchaseOrderMDB that processes orders from a JMS queue with security settings for authentication
- `Line 280`: Mail service configuration with SMTP settings for customer communications
- `Line 313`: Process Manager component with CMP configuration that handles order workflow states
- `Line 391`: Detailed SQL statements for database operations on purchase order entities with CMP
### petstore/src/application.xml

This application.xml file is a J2EE application deployment descriptor that defines the structure and components of the Java Pet Store application. It specifies the Enterprise Archive (EAR) configuration, listing all the modules that comprise the application. The file declares seven EJB modules (petstore-ejb.jar, customer-ejb.jar, asyncsender-ejb.jar, cart-ejb.jar, signon-ejb.jar, uidgen-ejb.jar, and catalog-ejb.jar) and one web module (petstore.war) with context root 'petstore'. This descriptor enables the J2EE application server to properly deploy and integrate all components of the Pet Store application.

 **Code Landmarks**
- `Line 46`: Uses the J2EE Application 1.3 DTD, indicating compatibility with J2EE 1.3 specification
- `Line 49`: Organizes the application as an EAR with multiple specialized EJB modules following a modular architecture pattern
- `Line 77`: Sets the web context root to 'petstore', defining the URL path for accessing the web application
### petstore/src/build.bat

This batch file serves as the build script for the Java Pet Store application on Windows systems. It configures the environment for Apache Ant by setting essential environment variables including ANT_HOME and constructing the ANT_CLASSPATH with required JAR files. The script then executes the Java command to run Ant with the specified classpath, passing along any command-line arguments to the build process. It ensures that Ant has access to the J2EE environment by setting the j2ee.home property and specifying the script suffix for Windows (.bat).

 **Code Landmarks**
- `Line 43`: Sets up the classpath to include J2EE libraries, enabling the build process to access enterprise Java components
- `Line 44`: Executes Ant with custom properties that configure platform-specific behavior (Windows .bat suffix)
### petstore/src/build.sh

This build script automates the compilation process for the Java Pet Store application. It first checks for required environment variables (JAVA_HOME and J2EE_HOME), setting up Java command paths if needed. The script then configures the Ant build tool by establishing the classpath with necessary JAR files including Ant libraries, Java tools, and J2EE dependencies. Finally, it executes the Ant build process with any passed parameters, allowing developers to compile the application with proper dependencies and environment settings.

 **Code Landmarks**
- `Line 46`: Automatically locates Java installation if JAVA_HOME is not set by finding the java executable in PATH
- `Line 65`: Constructs a comprehensive classpath that includes Ant libraries, Java tools, and J2EE dependencies
- `Line 67`: Executes Ant with system properties for ant.home, j2ee.home, and j2ee-script-suffix
### petstore/src/build.xml

This build.xml file serves as the main Ant build script for the Java Pet Store application. It defines targets for compiling Java classes, creating WAR and EJB JAR files, and packaging everything into an EAR file for deployment. The script manages dependencies between various components including WAF, SignOn, Catalog, Cart, Customer, and other services. It defines properties for file paths, class paths, and deployment tools, and includes targets for cleaning, deploying, undeploying, and generating documentation. Key targets include 'core' (the default), 'components', 'war', 'ejbjar', and 'ear'.

 **Code Landmarks**
- `Line 46`: Defines the main build targets hierarchy with 'core' as the default target
- `Line 237`: WAR file creation combines multiple components' web resources and client libraries
- `Line 285`: EJB JAR packaging combines multiple EJB components with proper manifests
- `Line 308`: EAR assembly process includes all component JARs and deployment descriptors
- `Line 350`: Deployment uses J2EE deploytool with SQL generation for database setup
### petstore/src/com/sun/j2ee/blueprints/petstore/controller/ejb/ShoppingClientFacadeLocal.java

ShoppingClientFacadeLocal defines a local EJB interface that serves as a facade for shopping-related operations in the Pet Store application. It provides methods to access and manage a user's shopping cart and customer information. Key functionality includes retrieving the shopping cart, setting and getting the user ID, retrieving an existing customer, and creating a new customer. The interface extends EJBLocalObject and interacts with ShoppingCartLocal and CustomerLocal components to provide a unified access point for shopping operations.

 **Code Landmarks**
- `Line 53`: Interface extends EJBLocalObject to function as a local EJB component in the J2EE architecture
### petstore/src/com/sun/j2ee/blueprints/petstore/controller/ejb/ShoppingClientFacadeLocalEJB.java

ShoppingClientFacadeLocalEJB implements a session bean that serves as a facade to shopping-related EJBs in the Pet Store application. It manages access to ShoppingCartLocal and CustomerLocal EJBs, providing methods to create and retrieve these components. The class maintains user identification and uses ServiceLocator to obtain EJB homes. Key methods include getUserId(), setUserId(), getCustomer(), createCustomer(), and getShoppingCart(). The implementation handles exceptions by wrapping them in GeneralFailureException and includes standard EJB lifecycle methods.

 **Code Landmarks**
- `Line 94`: Uses ServiceLocator pattern to abstract JNDI lookup complexity for EJB references
- `Line 108`: Creates customer with userId as primary key, demonstrating EJB entity creation pattern
- `Line 123`: Lazy initialization of shopping cart only when needed
### petstore/src/com/sun/j2ee/blueprints/petstore/controller/ejb/ShoppingClientFacadeLocalHome.java

ShoppingClientFacadeLocalHome interface defines the local home interface for the ShoppingClientFacade Enterprise JavaBean in the Pet Store application. It extends EJBLocalHome and declares a single create() method that returns a ShoppingClientFacadeLocal object. This interface follows the EJB design pattern for local interfaces, enabling client components within the same JVM to create and access the ShoppingClientFacade bean without the overhead of remote method invocation. The create() method throws CreateException if bean creation fails.

 **Code Landmarks**
- `Line 45`: Implements EJBLocalHome interface, indicating this is a local component interface for container-managed EJB access
### petstore/src/com/sun/j2ee/blueprints/petstore/controller/ejb/ShoppingControllerEJB.java

ShoppingControllerEJB extends EJBControllerLocalEJB to implement the controller component for shopping operations in the Pet Store application. It initializes a state machine and provides access to the ShoppingClientFacade, which handles shopping-related business logic. The class uses the Service Locator pattern to obtain references to the ShoppingClientFacadeLocalHome. Key methods include ejbCreate() for initialization and getShoppingClientFacade() which lazily creates and returns the client facade. The class serves as an intermediary between the web tier and the business logic components.

 **Code Landmarks**
- `Line 74`: Uses lazy initialization pattern for the client facade reference
- `Line 75`: Implements Service Locator pattern to obtain EJB references, reducing JNDI lookup code
- `Line 79`: Wraps checked exceptions in runtime GeneralFailureException for simplified error handling
### petstore/src/com/sun/j2ee/blueprints/petstore/controller/ejb/ShoppingControllerLocal.java

ShoppingControllerLocal interface extends EJBControllerLocal to provide access to application-specific stateful session beans in the Java Pet Store application. This interface serves as a bridge between the web application framework controller and shopping-related functionality. The interface declares a single method getShoppingClientFacade() that returns a ShoppingClientFacadeLocal object, which likely provides access to shopping cart operations and other e-commerce functionality. As an EJB local interface, it enables efficient local calls within the same JVM.

 **Code Landmarks**
- `Line 52`: This interface demonstrates the Facade pattern by providing a simplified interface to the shopping subsystem through the getShoppingClientFacade() method
### petstore/src/com/sun/j2ee/blueprints/petstore/controller/ejb/ShoppingControllerLocalHome.java

ShoppingControllerLocalHome interface defines the contract for creating instances of the ShoppingController EJB in the Java Pet Store application. As an EJB local home interface, it extends EJBLocalHome and provides a single create() method that returns a ShoppingControllerLocal reference. This method throws CreateException if the creation fails. The interface is part of the controller layer in the application's architecture, facilitating access to shopping functionality through the EJB container.

 **Code Landmarks**
- `Line 46`: Implements EJBLocalHome interface, indicating this is a local interface for container-managed EJB access within the same JVM
### petstore/src/com/sun/j2ee/blueprints/petstore/controller/ejb/actions/CartEJBAction.java

CartEJBAction implements a controller action that processes shopping cart events in the Pet Store application. It extends EJBActionSupport and implements the perform method to handle three types of cart operations: adding items, deleting items, and updating item quantities. The class interacts with ShoppingClientFacadeLocal to access the user's ShoppingCartLocal instance and performs the appropriate cart modifications based on the CartEvent's action type. Key methods include perform(Event) which processes CartEvent objects and returns null after completing the requested cart operations.

 **Code Landmarks**
- `Line 61`: Uses a switch statement to handle different cart action types from a single event handler
- `Line 77`: Processes a map of item IDs and quantities to update multiple cart items in a single operation
### petstore/src/com/sun/j2ee/blueprints/petstore/controller/ejb/actions/ChangeLocaleEJBAction.java

ChangeLocaleEJBAction implements an EJB action that processes locale change requests in the Pet Store application. It extends EJBActionSupport and implements the perform method to handle ChangeLocaleEvent objects. When a locale change is requested, the action updates the locale attribute in the state machine and notifies the shopping cart component of the change by retrieving the ShoppingClientFacade from the state machine and calling setLocale on the associated ShoppingCartLocal object. The class integrates with the Web Application Framework (WAF) event system and the shopping cart component.

 **Code Landmarks**
- `Line 58`: Updates both the application state machine and shopping cart with locale changes, ensuring consistent localization across components
### petstore/src/com/sun/j2ee/blueprints/petstore/controller/ejb/actions/CreateUserEJBAction.java

CreateUserEJBAction implements an EJB action that processes user registration requests in the Pet Store application. It extends EJBActionSupport and handles CreateUserEvent objects by creating user accounts through the SignOn EJB. The class's primary method perform() extracts username and password from the event, obtains a SignOnLocal EJB reference using ServiceLocator, creates the user account, and associates the username with the shopping client facade. It handles various exceptions by throwing DuplicateAccountException when user creation fails.

 **Code Landmarks**
- `Line 75`: Uses ServiceLocator pattern to obtain EJB references, promoting loose coupling
- `Line 86`: Stores username in state machine for future retrieval, showing session state management
### petstore/src/com/sun/j2ee/blueprints/petstore/controller/ejb/actions/CustomerEJBAction.java

CustomerEJBAction implements an EJB action class that processes CustomerEvent objects to create and update customer information in the Pet Store application. It interacts with the ShoppingClientFacadeLocal to access customer data and performs deep copying of customer information between data transfer objects and EJB local interfaces. The class handles two action types: CREATE (creates a new customer and updates their information) and UPDATE (updates an existing customer's information). The updateCustomer method performs detailed mapping of contact information, address, credit card details, and user preferences from event objects to the corresponding EJB local interfaces.

 **Code Landmarks**
- `Line 77`: Deep copying pattern between data transfer objects and EJB local interfaces demonstrates separation between presentation and persistence layers
- `Line 123`: Sets user's locale preference in the state machine, affecting the application's internationalization behavior
### petstore/src/com/sun/j2ee/blueprints/petstore/controller/ejb/actions/OrderEJBAction.java

OrderEJBAction implements an EJB action that processes order submissions in the Java Pet Store application. It extends EJBActionSupport and handles OrderEvents by creating PurchaseOrder objects with customer billing, shipping, and payment information. The class retrieves cart items, generates unique order IDs, calculates order totals, and sends the completed purchase order asynchronously via the AsyncSender component. After successful order submission, it empties the shopping cart and returns an OrderEventResponse with the order details. Key methods include perform() which orchestrates the entire order processing workflow.

 **Code Landmarks**
- `Line 103`: Uses a unique ID generator EJB to create order IDs with a specific prefix (1001)
- `Line 132`: Validates shopping cart contents before processing, throwing ShoppingCartEmptyOrderException if empty
- `Line 155`: Converts the purchase order to XML and sends it asynchronously for processing
- `Line 175`: Empties the shopping cart after successful order submission to prevent duplicate orders
### petstore/src/com/sun/j2ee/blueprints/petstore/controller/ejb/actions/SignOnEJBAction.java

SignOnEJBAction implements an EJB action that processes user sign-on events in the Pet Store application. It extends EJBActionSupport and handles SignOnEvent objects by setting the user ID in the shopping client facade and configuring locale preferences based on the user's profile. The class retrieves the user's preferred language from their profile, converts it to a Locale object, and updates both the state machine and shopping cart with this locale. Key methods include perform() which processes the sign-on event and manages user session setup.

 **Code Landmarks**
- `Line 77`: Sets user's preferred language as application locale, demonstrating internationalization support
- `Line 80`: Notifies shopping cart of locale change, showing cross-component communication pattern
- `Line 81`: Handles case where user profile doesn't exist yet during first-time registration
### petstore/src/com/sun/j2ee/blueprints/petstore/controller/events/CartEvent.java

CartEvent implements an event class that encapsulates shopping cart state changes for the PetStore controller. It supports four action types: ADD_ITEM, DELETE_ITEM, UPDATE_ITEMS, and EMPTY. The class stores information about items being modified including item IDs and quantities. Multiple constructors handle different cart operations, from adding single items to updating multiple items via a HashMap. Key methods include getActionType(), getItemId(), getQuantity(), getItems(), and a static factory method createUpdateItemEvent() that creates events with defensive copies of item collections.

 **Code Landmarks**
- `Line 122`: Creates a defensive copy of the items Map to prevent modification after event creation
- `Line 46`: Extends EventSupport from the Web Application Framework, integrating with the application's event system
### petstore/src/com/sun/j2ee/blueprints/petstore/controller/events/CreateUserEvent.java

CreateUserEvent implements an event class that extends EventSupport to handle user creation operations in the Pet Store application. It stores username and password information needed to create a new user account. The class provides getter methods for accessing the username and password, overrides toString() to display event details, and implements getEventName() to identify the event type. This event is designed to be processed by the EJBController component to initiate user account creation in the system.

 **Code Landmarks**
- `Line 43`: The class comment incorrectly describes this as a Locale change event rather than a user creation event
### petstore/src/com/sun/j2ee/blueprints/petstore/controller/events/CustomerEvent.java

CustomerEvent implements an event class that encapsulates customer information changes in the Pet Store application. It extends EventSupport and carries data about customer profile updates or creation actions. The class stores ContactInfo, ProfileInfo, and CreditCard objects along with an action type flag (UPDATE or CREATE). Key functionality includes retrieving customer data components and identifying the event type. Important elements include the constants UPDATE and CREATE, instance variables for customer data, a constructor that initializes all fields, getter methods for each data component, and toString() and getEventName() methods for event identification.

 **Code Landmarks**
- `Line 49`: Extends EventSupport from the Web Application Framework (WAF) to integrate with the event handling system
- `Line 53-54`: Uses constants to define action types, making the code more maintainable and readable
### petstore/src/com/sun/j2ee/blueprints/petstore/controller/events/OrderEvent.java

OrderEvent implements an event class that extends EventSupport to represent order information in the Pet Store application. It encapsulates billing information, shipping information, and credit card details needed to process an order. The class stores ContactInfo objects for both billing and shipping addresses along with a CreditCard object. It provides getter methods to access these properties and overrides toString() and getEventName() methods from its parent class. This event is likely triggered during checkout to initiate order processing in the application's controller layer.

 **Code Landmarks**
- `Line 48`: Class extends EventSupport from the Web Application Framework (WAF) to integrate with the event handling system
- `Line 55`: Stores both billing and shipping addresses separately, allowing for different delivery locations
### petstore/src/com/sun/j2ee/blueprints/petstore/controller/events/OrderEventResponse.java

OrderEventResponse implements an event response class that encapsulates information about a completed order in the Pet Store application. It stores two key pieces of data: the order ID and the customer's email address. The class implements the EventResponse interface from the Web Application Framework (WAF), providing methods to access the order information and identify the event type. Key methods include getOrderId(), getEmail(), toString() for debugging, and getEventName() which returns the JNDI name used to identify this event type in the application's event handling system.

 **Code Landmarks**
- `Line 54`: Uses JNDI naming convention for event identification in getEventName() method
### petstore/src/com/sun/j2ee/blueprints/petstore/controller/events/SignOnEvent.java

SignOnEvent implements an event class that extends EventSupport to represent user sign-on actions in the Pet Store application. It stores a username provided during sign-on and makes it available through the getUserName() method. The class provides toString() for debugging and getEventName() to identify the event type. This event is designed to notify the EJBController about user authentication, enabling the system to track user sessions and potentially start order processing workflows.

 **Code Landmarks**
- `Line 44`: This class extends EventSupport from the Web Application Framework (WAF), showing the event-driven architecture of the application
### petstore/src/com/sun/j2ee/blueprints/petstore/controller/exceptions/DuplicateAccountException.java

DuplicateAccountException implements a serializable exception class that extends EventException to handle cases where a user attempts to create an account that already exists. The class stores an error message provided during instantiation and provides a getMessage() method to retrieve this message. This exception is part of the controller exception hierarchy in the Pet Store application and is used specifically for user account creation failures due to duplicates.
### petstore/src/com/sun/j2ee/blueprints/petstore/controller/exceptions/ShoppingCartEmptyOrderException.java

ShoppingCartEmptyOrderException implements a custom exception that extends EventException and implements Serializable. It represents a failure to place an order when the shopping cart is empty, typically occurring when users hit the back button and attempt to resubmit an order. The class maintains a message string that describes the exception and provides a getMessage() method to retrieve this information. This exception is part of the controller exception handling framework in the Pet Store application.
### petstore/src/com/sun/j2ee/blueprints/petstore/controller/web/BannerHelper.java

BannerHelper implements a simple web tier display selection utility that determines which banner image to display based on a product category. It contains a single field to store the categoryId and provides methods to set this ID and retrieve the corresponding banner image filename. The getBanner() method maps category IDs (dogs, cats, reptiles, birds, fish) to their respective banner image filenames, returning a default dog banner if no match is found. The class is serializable to support web application state management.

 **Code Landmarks**
- `Line 46`: Class implements Serializable to support state persistence in web applications
- `Line 60`: Hard-coded mapping of category IDs to banner images could be externalized to properties file or database as noted in the class comments
### petstore/src/com/sun/j2ee/blueprints/petstore/controller/web/PetstoreComponentManager.java

PetstoreComponentManager extends DefaultComponentManager to provide access to services in both web and EJB tiers of the Pet Store application. It implements HttpSessionListener to manage session lifecycle events and initializes shopping cart resources for each user session. The class serves as a bridge between web controllers and EJB components, offering methods to retrieve shopping controllers, customer information, and shopping carts. Key methods include init(), sessionCreated(), getCustomer(), getShoppingController(), and getShoppingCart(). It uses ServiceLocator to obtain references to EJB homes and handles exceptions by wrapping them in GeneralFailureException.

 **Code Landmarks**
- `Line 84`: Implements HttpSessionListener to manage session lifecycle events programmatically
- `Line 94`: Uses service locator pattern to abstract away JNDI lookup complexity
- `Line 123`: Maintains shopping controller as session attribute for stateful interactions
### petstore/src/com/sun/j2ee/blueprints/petstore/controller/web/ShoppingWebController.java

ShoppingWebController implements a proxy class that handles shopping-related events in the Pet Store application by delegating them to the EJB tier. It implements the WebController interface with methods for initialization, event handling, and resource cleanup. The handleEvent method synchronously processes events by retrieving the ShoppingControllerLocal EJB from the PetstoreComponentManager and forwarding events to it. The destroy method properly removes the associated EJB when the controller is no longer needed. The class ensures thread safety by synchronizing methods that access the stateful session bean.

 **Code Landmarks**
- `Line 74`: All methods accessing the EJB tier are synchronized to prevent concurrent access to the stateful session bean
- `Line 89`: Uses a component manager pattern to retrieve the shopping controller EJB from the session
- `Line 107`: Deliberately ignores RemoveException during cleanup, showing a design decision to prioritize clean termination over error handling
### petstore/src/com/sun/j2ee/blueprints/petstore/controller/web/SignOnNotifier.java

SignOnNotifier implements an HttpSessionAttributeListener that detects when a user signs on to the Petstore application. It monitors session attribute changes, specifically looking for the SIGNED_ON_USER attribute set by the SignOnFilter. When a sign-on is detected, it creates a SignOnEvent with the username, finds the appropriate EJBAction mapping, and passes the event to the WebController. It also ensures the customer object is in the session and sets locale preferences based on the user's profile. This class enables loose coupling between the SignOn component and the Petstore application.

 **Code Landmarks**
- `Line 97`: Detects sign-on by monitoring session attribute changes rather than direct API calls
- `Line 112`: Uses event-based architecture to notify backend systems of user authentication
- `Line 125`: Sets user preferences and locale based on stored profile after authentication
### petstore/src/com/sun/j2ee/blueprints/petstore/controller/web/actions/CartHTMLAction.java

CartHTMLAction implements a controller action class that processes user interactions with the shopping cart in the Pet Store application. It extends HTMLActionSupport and handles three main cart operations: adding items (purchase), removing items, and updating item quantities. The perform method parses HTTP request parameters to determine the action type and creates appropriate CartEvent objects that will be processed by the application's event handling system. The class works with request parameters to extract item IDs and quantities, particularly handling the special case of batch updates where multiple item quantities are changed simultaneously.

 **Code Landmarks**
- `Line 94`: Creates a map of item quantities from request parameters using a prefix-based naming convention for form fields
- `Line 115`: Uses a factory method pattern with CartEvent.createUpdateItemEvent() for batch updates rather than direct constructor
### petstore/src/com/sun/j2ee/blueprints/petstore/controller/web/actions/CreateUserHTMLAction.java

CreateUserHTMLAction implements a web action handler that processes user registration form submissions in the Java Pet Store application. It extends HTMLActionSupport and implements the perform method to extract username and password parameters from HTTP requests. The class creates and returns a CreateUserEvent containing the user credentials for further processing by the application's event handling system. It also stores the username in the session for future reference using the SignOnFilter.USER_NAME attribute key.

 **Code Landmarks**
- `Line 73`: Stores username in session using SignOnFilter.USER_NAME constant for maintaining user context across requests
- `Line 77`: Returns null event if username or password is missing, which could lead to unexpected behavior in the event handling system
### petstore/src/com/sun/j2ee/blueprints/petstore/controller/web/actions/CustomerHTMLAction.java

CustomerHTMLAction implements an action handler for customer-related operations in the Java Pet Store web application. It processes HTTP requests for creating and updating customer accounts by extracting form data and generating appropriate events. The class extracts contact information, credit card details, and profile preferences from request parameters, validates required fields, and creates CustomerEvent objects. Key methods include perform() which determines the action type and creates the appropriate event, extractContactInfo(), extractCreditCard(), and extractProfileInfo() for form data processing. The class also handles session management for newly created customers through the doEnd() method.

 **Code Landmarks**
- `Line 83`: The perform method determines the action type and creates appropriate CustomerEvent objects based on form submissions
- `Line 186`: Form validation with detailed missing field tracking that builds a list of all missing required fields
- `Line 257`: Session management for newly created customers integrates with SignOnFilter authentication system
### petstore/src/com/sun/j2ee/blueprints/petstore/controller/web/actions/OrderHTMLAction.java

OrderHTMLAction implements a controller action that processes user order submissions in the Pet Store application. It extends HTMLActionSupport and handles HTTP requests containing shipping and billing information. The class extracts contact information from form data, validates required fields, and creates an OrderEvent with shipping, receiving, and credit card details. Key functionality includes form field validation, error handling for missing data, and event creation. Important methods include perform(), extractContactInfo(), and doEnd(). The class uses MissingFormDataException to handle validation errors and stores the order response as a request attribute.

 **Code Landmarks**
- `Line 75`: Creates a hardcoded credit card object rather than extracting it from the form
- `Line 84`: Form validation with detailed error tracking for missing fields
- `Line 153`: Uses request attributes to store exceptions for the view layer to handle
### petstore/src/com/sun/j2ee/blueprints/petstore/controller/web/actions/SignOffHTMLAction.java

SignOffHTMLAction implements a controller action that processes user sign-off requests in the Java Pet Store application. It extends HTMLActionSupport and overrides the perform method to handle session invalidation when a user signs off. The class preserves the user's locale preference across the sign-off process by retrieving it before invalidating the session and setting it in the newly created session. After sign-off, it initializes a new PetstoreComponentManager and returns a ChangeLocaleEvent with the preserved locale to maintain the user's language preference.

 **Code Landmarks**
- `Line 76`: Preserves user locale preference across session invalidation, maintaining user experience consistency
- `Line 81`: Creates a new component manager after sign-off, ensuring clean application state
### petstore/src/com/sun/j2ee/blueprints/petstore/controller/web/exceptions/MissingFormDataException.java

MissingFormDataException implements a custom exception that extends HTMLActionException to handle cases where users fail to provide required form information. The class stores both an error message and a collection of missing form fields. It provides methods to retrieve these values through getMissingFields() and getMessage(). This exception is thrown by the RequestToEventTranslator and used by JSP pages to generate appropriate error messages when form validation fails.

 **Code Landmarks**
- `Line 47`: Implements Serializable to allow the exception to be passed between different JVMs or persisted
### petstore/src/com/sun/j2ee/blueprints/petstore/controller/web/flow/handlers/CreateUserFlowHandler.java

CreateUserFlowHandler implements a flow handler responsible for redirecting users to the appropriate screen after they create a new account. It works with the SignOn component to determine where users should be directed after account creation. The class implements the FlowHandler interface with three methods: doStart() which is empty, processFlow() which retrieves the original URL from the session and determines the appropriate forward destination, and doEnd() which is also empty. The key functionality is in processFlow(), which checks if the forward screen is 'customer.do' and returns 'MAIN_SCREEN' in that case, otherwise returns the original URL.

 **Code Landmarks**
- `Line 60`: The handler specifically checks for 'customer.do' URL and redirects to 'MAIN_SCREEN' instead, showing special case handling in the navigation flow.
### petstore/src/com/sun/j2ee/blueprints/petstore/tools/populate/AccountPopulator.java

AccountPopulator implements a utility class for creating Account entities in the Java Pet Store application. It works with ContactInfoPopulator and CreditCardPopulator to build complete account records from XML data. The class uses EJB local interfaces to create persistent Account objects linked to ContactInfo and CreditCard entities. Key functionality includes parsing XML account data, validating dependencies, and creating Account entities via the EJB container. Important components include the setup() method that configures XML parsing, check() for validation, and createAccount() which handles the actual EJB creation.

 **Code Landmarks**
- `Line 59`: Uses XMLFilter chaining pattern to process nested XML elements through multiple populators
- `Line 67`: Implements anonymous inner class extending XMLDBHandler to handle XML parsing events
- `Line 81`: Uses JNDI lookup to obtain EJB home interface, demonstrating J2EE component lookup pattern
### petstore/src/com/sun/j2ee/blueprints/petstore/tools/populate/AddressPopulator.java

AddressPopulator implements a utility class for creating address entries in the Pet Store database from XML data. It parses XML address information and creates corresponding EJB entities. The class handles XML elements like street names, city, state, zip code, and country, converting them into Address EJB objects. Key functionality includes setting up XML filters, creating address entities, and providing access to the created address objects. Important elements include the setup() method that configures XML parsing, createAddress() method that interacts with the EJB container, and constants defining XML element names and JNDI lookup paths.

 **Code Landmarks**
- `Line 64`: Uses a nested anonymous class extending XMLDBHandler to handle XML parsing and database operations
- `Line 81`: Creates Address EJB entities from parsed XML data with comprehensive error handling
### petstore/src/com/sun/j2ee/blueprints/petstore/tools/populate/CatalogPopulator.java

CatalogPopulator implements a utility class for populating the PetStore catalog database. It orchestrates the population of categories, products, and items by coordinating three specialized populators: CategoryPopulator, ProductPopulator, and ItemPopulator. The class provides methods to set up XML filters, check database state, create tables, and drop tables. Key methods include setup() which configures XML readers and filters, check() to verify database state, createTables() to initialize database schema, and dropTables() to remove existing tables. The class maintains references to the three specialized populators as instance variables.

 **Code Landmarks**
- `Line 53`: Uses a chain of responsibility pattern with XMLFilter objects to process different parts of the catalog XML
- `Line 59`: Implements defensive error handling in dropTables() by catching and suppressing exceptions during cleanup operations
- `Line 78`: Creates tables in a specific order (category → product → item) to maintain referential integrity
### petstore/src/com/sun/j2ee/blueprints/petstore/tools/populate/CategoryDetailsPopulator.java

CategoryDetailsPopulator implements a utility class for populating category details data in the Java Pet Store application. It handles the insertion of category information (name, description, image, locale) into database tables from XML sources. The class provides methods for setting up XML parsing filters, checking table existence, creating and dropping tables, and executing SQL statements. Key functionality includes XML parsing configuration and database operations through the PopulateUtils helper class. Important elements include the setup() method that creates an XMLFilter, check(), dropTables(), and createTables() methods for database operations.

 **Code Landmarks**
- `Line 63`: Creates an anonymous inner class extending XMLDBHandler to handle XML parsing and database operations
- `Line 49`: Constructor allows flexibility by accepting a custom root tag for XML parsing
### petstore/src/com/sun/j2ee/blueprints/petstore/tools/populate/CategoryPopulator.java

CategoryPopulator implements functionality to populate the database with category data from XML sources. It handles inserting category records into database tables, with methods for setup, checking, creating, and dropping tables. The class works with a CategoryDetailsPopulator for handling category details and uses XMLDBHandler for XML parsing. Key methods include setup(), check(), dropTables(), and createTables(). Important variables include XML_CATEGORIES, XML_CATEGORY, and PARAMETER_NAMES which define XML structure and database parameters.

 **Code Landmarks**
- `Line 59`: Uses a nested XMLFilter implementation for database operations
- `Line 63`: Empty update() method suggests this class is designed for initial population only, not updates
- `Line 66`: Supports both actual database insertion and SQL statement printing based on connection parameter
### petstore/src/com/sun/j2ee/blueprints/petstore/tools/populate/ContactInfoPopulator.java

ContactInfoPopulator implements a utility class for creating ContactInfo entities in the PetStore database from XML data. It parses XML elements containing contact information (given name, family name, phone, email) and uses an AddressPopulator to handle address data. The class provides methods to set up an XML filter, check data validity, and create ContactInfo objects through EJB local interfaces. Key methods include setup(), check(), createContactInfo(), and getContactInfo(). Important variables include contactInfoHome, contactInfo, addressPopulator, and XML element constants.

 **Code Landmarks**
- `Line 65`: Uses a nested anonymous class extending XMLDBHandler to implement XML parsing callbacks
- `Line 75`: Delegates address population to a separate AddressPopulator class, showing separation of concerns
- `Line 85`: Uses JNDI lookup to obtain the ContactInfo EJB home interface
### petstore/src/com/sun/j2ee/blueprints/petstore/tools/populate/CreditCardPopulator.java

CreditCardPopulator implements a utility class for the Java Pet Store application that populates credit card data from XML files into the database. It uses SAX parsing to read credit card information (card number, type, and expiry date) from XML and creates corresponding EJB entities. The class provides methods for setting up XML filters, checking data validity, and creating credit card records through the CreditCardLocalHome interface. Key components include the setup() method that returns an XMLFilter implementation, the createCreditCard() method that interfaces with the EJB container, and the getCreditCard() method to retrieve created entities.

 **Code Landmarks**
- `Line 61`: Uses an anonymous inner class extending XMLDBHandler to handle XML parsing events
- `Line 73`: Implements EJB lookup pattern using JNDI to access the CreditCard entity bean
### petstore/src/com/sun/j2ee/blueprints/petstore/tools/populate/CustomerPopulator.java

CustomerPopulator implements a tool for populating the Pet Store database with customer information from XML sources. It creates customer records with associated account and profile data. The class uses SAX parsing with XMLFilter to process customer data, and interacts with EJB components through JNDI lookups. Key functionality includes checking if customers exist, creating new customer entries, and linking them with account and profile information. Important methods include setup(), check(), and createCustomer(). The class works with AccountPopulator and ProfilePopulator to handle related customer data components.

 **Code Landmarks**
- `Line 58`: Uses a nested XMLFilter implementation with anonymous class for XML parsing
- `Line 74`: Implements database existence check before population to prevent duplicate entries
- `Line 107`: Handles entity relationships by properly linking Customer with Account and Profile entities
### petstore/src/com/sun/j2ee/blueprints/petstore/tools/populate/ItemDetailsPopulator.java

ItemDetailsPopulator implements a utility class for populating item details data from XML into database tables. It defines constants for XML element names and SQL parameter mappings. The class provides methods to set up XML parsing filters, check table existence, create and drop tables, and execute SQL statements. It works with XMLDBHandler to parse item details from XML files and insert them into the database using predefined SQL statements. Key methods include setup(), check(), dropTables(), and createTables(). The class handles attributes, prices, descriptions, and images for pet store items.

 **Code Landmarks**
- `Line 72`: Uses XMLFilter and XMLReader for SAX-based XML parsing with database operations
- `Line 74`: Implements anonymous inner class extending XMLDBHandler to handle XML-to-database mapping
- `Line 53`: Maintains a mapping of SQL statements passed in constructor for database operations
### petstore/src/com/sun/j2ee/blueprints/petstore/tools/populate/ItemPopulator.java

ItemPopulator implements a database population tool for the PetStore application that loads item data from XML sources into a database. It handles the creation, checking, and dropping of item-related database tables. The class uses SAX parsing with XMLDBHandler to process XML data and execute corresponding SQL statements. Key functionality includes setting up XML filters, creating database tables, and managing item details through a nested ItemDetailsPopulator. Important components include setup(), check(), dropTables(), and createTables() methods, along with constants for XML tags and SQL parameter names.

 **Code Landmarks**
- `Line 63`: Uses a nested XMLDBHandler with anonymous class implementation for database operations
- `Line 55`: Implements a hierarchical populator pattern with ItemDetailsPopulator for related data
- `Line 76`: Implements graceful error handling in dropTables() to continue even if child table drops fail
### petstore/src/com/sun/j2ee/blueprints/petstore/tools/populate/PopulateException.java

PopulateException implements a custom exception class that can wrap lower-level exceptions in the PetStore database population tool. It provides three constructors: one with a message and wrapped exception, one with just a message, and one with just a wrapped exception. The class offers methods to retrieve the wrapped exception (getException) and to recursively find the root cause exception (getRootCause). This exception handling mechanism enables more informative error reporting during database population operations by preserving the original exception context.

 **Code Landmarks**
- `Line 77`: Implements recursive exception unwrapping to find the root cause of nested exceptions
### petstore/src/com/sun/j2ee/blueprints/petstore/tools/populate/PopulateServlet.java

PopulateServlet is responsible for initializing the PetStore database with sample data. It loads SQL statements from an XML configuration file and executes them to create tables and insert data. The servlet handles both GET and POST requests, supports forced population or checking if data already exists, and manages database connections through JNDI. It works with three populator classes (CatalogPopulator, CustomerPopulator, and UserPopulator) to populate different parts of the database. Key methods include init(), doPost(), populate(), getConnection(), and loadSQLStatements(). Important variables include sqlStatements map and configuration parameters for SQL paths and database type.

 **Code Landmarks**
- `Line 148`: The servlet invalidates the session after population since all EJB references become invalid
- `Line 179`: Uses a chain of responsibility pattern where each populator sets up the next one in sequence
- `Line 246`: Custom ParsingDoneException class used to interrupt XML parsing once needed data is collected
### petstore/src/com/sun/j2ee/blueprints/petstore/tools/populate/PopulateUtils.java

PopulateUtils provides utility methods for executing SQL statements during database population operations for the Pet Store application. It implements functionality to execute SQL statements with parameterized queries, print SQL statements for debugging, and handle database operations like create, insert, drop, and check. The class defines constants for operation types and includes methods for executing SQL statements from a map of predefined queries or directly from string statements. Important methods include executeSQLStatement(), printSQLStatement(), and makeSQLStatementKey(). The class uses PreparedStatement for secure database operations and handles parameter substitution from an XMLDBHandler.

 **Code Landmarks**
- `Line 48`: Private constructor prevents instantiation of this utility class
- `Line 51`: Method supports executing SQL statements from a map using keys, enabling centralized SQL management
- `Line 70`: Uses PreparedStatement for SQL injection prevention when executing database operations
- `Line 77`: Handles both query results and update counts with a unified return approach
### petstore/src/com/sun/j2ee/blueprints/petstore/tools/populate/ProductDetailsPopulator.java

ProductDetailsPopulator implements functionality to populate product details in the database from XML data. It handles database operations for product details including creating, checking, and dropping tables. The class uses XMLDBHandler to parse XML data and execute SQL statements through the PopulateUtils helper class. It defines constants for XML tags and parameter names used in SQL operations. Key methods include setup() which configures an XML parser, check() to verify table existence, dropTables() and createTables() for schema management. The class works with a Connection object and a map of SQL statements to perform database operations.

 **Code Landmarks**
- `Line 63`: Uses XMLFilter and XMLReader for XML parsing, demonstrating integration between SAX parsing and database operations
- `Line 65`: Implements anonymous inner class pattern to handle XML data processing with database updates
- `Line 72`: Conditional execution allows dry-run mode when connection is null by printing SQL instead of executing it
### petstore/src/com/sun/j2ee/blueprints/petstore/tools/populate/ProductPopulator.java

ProductPopulator implements a utility class for loading product data from XML into a database. It handles the creation, checking, and dropping of product-related database tables. The class uses XMLDBHandler to parse product XML data and executes SQL statements to insert products into the database. It works with a companion ProductDetailsPopulator to handle product details. Key methods include setup(), check(), dropTables(), and createTables(). Important variables include XML_PRODUCTS, XML_PRODUCT, PARAMETER_NAMES, and sqlStatements which stores the SQL queries.

 **Code Landmarks**
- `Line 64`: Uses a nested XMLFilter implementation for database operations that separates XML parsing from database operations
- `Line 71`: Supports both database insertion and SQL statement printing modes based on connection availability
- `Line 88`: Implements cascading table operations that handle parent-child relationships between products and product details
### petstore/src/com/sun/j2ee/blueprints/petstore/tools/populate/ProfilePopulator.java

ProfilePopulator implements a utility class for creating Profile EJB entities from XML data in the Pet Store application. It parses XML profile information and creates corresponding Profile entity beans in the database. The class uses SAX parsing with an XMLFilter to process profile data including preferred language, favorite category, and user preferences. Key functionality includes setting up the XML parser, creating Profile entities via EJB home interface, and providing access to the created profile. Important elements include the setup() method that configures the XML handler, createProfile() method that interacts with the EJB container, and getProfile() method that returns the created profile entity.

 **Code Landmarks**
- `Line 55`: Uses an anonymous inner class extending XMLDBHandler to handle XML parsing events
- `Line 69`: Implements EJB lookup pattern using JNDI to obtain the ProfileLocalHome interface
- `Line 61`: Demonstrates separation between XML parsing and EJB creation operations
### petstore/src/com/sun/j2ee/blueprints/petstore/tools/populate/UserPopulator.java

UserPopulator implements a tool for populating user data in the Pet Store application database from XML files. It provides functionality to create user accounts by parsing XML data and interacting with the User EJB. The class includes methods for setting up XML parsing, checking if users already exist in the database, and creating new user entries. Key methods include setup(), check(), createUser(), and main(). Important variables include JNDI_USER_HOME for EJB lookup, XML_USERS and XML_USER for XML parsing, and userHome for database operations.

 **Code Landmarks**
- `Line 60`: Uses an anonymous inner class implementation of XMLDBHandler to handle XML parsing events
- `Line 74`: Implements a check() method that verifies if the database already contains user data before populating
- `Line 92`: Attempts to remove existing users with the same ID before creating new ones, preventing duplicates
### petstore/src/com/sun/j2ee/blueprints/petstore/tools/populate/XMLDBHandler.java

XMLDBHandler implements an abstract SAX filter for parsing XML data and populating a database in the Pet Store application. It processes XML elements matching specified root and element tags, collecting attribute values and element content into context and values maps. The handler supports both immediate and lazy object instantiation modes. Key methods include startElement(), endElement(), characters(), and getValue() variants for retrieving parsed data. Subclasses must implement create() and update() methods to handle the actual database operations based on parsed content.

 **Code Landmarks**
- `Line 65`: Implements a state machine pattern with OFF, READY, and PARSING states to control XML processing flow
- `Line 76`: Supports lazy instantiation mode that defers object creation until all data is collected
- `Line 171`: Maintains indexed values using array-like syntax (key[0], key[1]) for handling multiple occurrences of the same element
### petstore/src/com/sun/j2ee/blueprints/petstore/util/JNDINames.java

JNDINames is a utility class that centralizes the storage of internal JNDI names for various Enterprise JavaBeans (EJB) home objects used throughout the Pet Store application. It prevents instantiation through a private constructor and provides public static final String constants for each EJB home object reference. These constants include references to ShoppingController, ShoppingClientFacade, UniqueIdGenerator, SignOn, Customer, ShoppingCart, and AsyncSender EJBs. The class documentation notes that any changes to these JNDI names must also be reflected in the application's deployment descriptors.

 **Code Landmarks**
- `Line 47`: Private constructor prevents instantiation, enforcing usage as a static utility class
- `Line 51`: All JNDI names use the java:comp/env/ namespace prefix, following J2EE best practices for portable JNDI lookups
### petstore/src/com/sun/j2ee/blueprints/petstore/util/PetstoreKeys.java

PetstoreKeys extends WebKeys and defines constant string keys used throughout the Java Pet Store application for storing and retrieving data in different web tier scopes. The class contains keys for cart, customer, order response, and shopping client facade objects. These constants ensure consistent naming when storing and retrieving objects across JSP pages and Java components. The class has a private constructor to prevent instantiation, as it only serves as a container for static constants.

 **Code Landmarks**
- `Line 45`: Class extends WebKeys from the Web Application Framework (WAF) to inherit base keys while adding application-specific ones
- `Line 48`: Private constructor prevents instantiation, enforcing usage as a pure utility class for constants
### petstore/src/docroot/CatalogDAOSQL.xml

CatalogDAOSQL.xml defines SQL statements for database operations in the Pet Store catalog module. It contains parameterized queries for retrieving categories, products, items, and performing searches across two database types (Cloudscape and Oracle). The file uses an XML structure with a DTD that defines elements for DAO configuration, statements, and SQL fragments. Each SQL statement is associated with a specific method (like GET_CATEGORY, SEARCH_ITEMS) and contains SQL fragments with parameter counts and occurrence attributes. The queries handle multilingual content through locale parameters.

 **Code Landmarks**
- `Line 38-53`: XML DTD definition provides a structured schema for SQL statements with method types and fragment parameters
- `Line 125-137`: SEARCH_ITEMS implementation uses variable occurrence fragments to support dynamic query construction for flexible search criteria
- `Line 55-137`: Database-specific SQL implementations for Cloudscape showing join syntax differences from Oracle's implementation
### petstore/src/docroot/WEB-INF/mappings.xml

This mappings.xml file defines the event-to-action and URL-to-action mappings for the Java Pet Store application's controller framework. It establishes connections between frontend events and their corresponding EJB actions through event-mapping elements, linking classes like OrderEvent to OrderEJBAction. It also configures URL mappings that associate web requests with HTML actions and screen destinations, some using flow handlers for complex navigation. Additionally, it defines exception mappings that direct specific error types to appropriate error screens, creating a comprehensive routing configuration for the MVC architecture.

 **Code Landmarks**
- `Line 45-79`: Implements event-to-EJB action mappings that connect frontend events to backend business logic
- `Line 81-115`: Defines URL mappings with optional flow handlers for complex navigation paths
- `Line 117-119`: Maps exceptions to specific error screens for graceful error handling
### petstore/src/docroot/WEB-INF/screendefinitions_en_US.xml

This XML configuration file defines the screen layouts for the Java Pet Store web application's user interface. It establishes a default template and defines 20 different screens including main, cart, category, product, item, customer, search, and various account-related screens. Each screen definition specifies parameters for page components like title, banner, sidebar, body content, footer, and other UI elements. The file organizes the application's presentation layer by mapping logical screen names to their corresponding JSP components, creating a consistent layout structure throughout the application.

 **Code Landmarks**
- `Line 3`: Uses XML to implement a template-based UI composition pattern, separating screen structure from content
- `Line 42`: Defines a default template that serves as the base for all screens in the application
- `Line 44-51`: Each screen definition uses parameters to compose UI components, enabling modular page construction
### petstore/src/docroot/WEB-INF/screendefinitions_ja_JP.xml

This XML file defines screen layouts for the Japanese version of the Pet Store application. It specifies a default template and defines multiple screens (main, cart, product, etc.) with their respective parameters. Each screen definition includes components like title, banner, sidebar, body content, and footer, all pointing to Japanese-localized JSP files. The file enables consistent page structure across the application while maintaining Japanese language support through the _ja_JP suffix and /ja/ directory paths.

 **Code Landmarks**
- `Line 3`: Copyright notice indicates this is Sun Microsystems code with specific redistribution terms
- `Line 45`: Default template path shows localization structure with /ja/ directory
- `Line 47-54`: Main screen definition demonstrates the template composition pattern with multiple content fragments
### petstore/src/docroot/WEB-INF/screendefinitions_zh_CN.xml

This XML file defines screen layouts for the Chinese (Simplified) localization of the Java Pet Store application. It specifies a default template and defines multiple screens (main, cart, product, etc.) with parameters for various UI components. Each screen definition includes parameters for title, banner, sidebar, body content, footer, and other page elements, all pointing to localized JSP files in the /zh/ directory. The file ensures consistent page structure while providing Chinese language content across the application's different functional areas.

 **Code Landmarks**
- `Line 3`: Copyright notice indicates this is Sun Microsystems code with specific redistribution terms
- `Line 44`: Default template path shows localization structure with /zh/ directory for Chinese content
- `Line 47-54`: Screen definitions use a combination of localized components (/zh/) and shared components (like main.jsp)
### petstore/src/docroot/WEB-INF/signon-config.xml

This XML configuration file defines the authentication and authorization settings for the Java Pet Store application. It specifies the sign-on form login page and error page to be used when authentication fails. The file also declares several security constraints that protect specific web resources, including customer screens, customer actions, order information screens, and the sign-on welcome screen. Each constraint identifies protected URL patterns that require user authentication before access is granted.

 **Code Landmarks**
- `Line 39-42`: Defines the main login page and error page paths for the application's authentication system
- `Line 45-51`: Implements security constraint for customer profile screen, requiring authentication
- `Line 54-60`: Protects customer action endpoints that handle form submissions and data modifications
### petstore/src/docroot/WEB-INF/web.xml

This web.xml deployment descriptor configures the Java Pet Store web application. It defines essential web components including EncodingFilter for UTF-8 encoding, SignOnFilter for authentication, and key servlets like MainServlet (front controller) and TemplateServlet for view rendering. The file configures servlet mappings (.do and .screen extensions), session timeout settings, and welcome files. It establishes numerous EJB local references for components like ShoppingController, ShoppingCart, and Customer entities. The descriptor also configures resources for the Fast Lane Reader pattern to access catalog data directly via JDBC, and defines environment entries for component configuration including the database type (Cloudscape).

 **Code Landmarks**
- `Line 47`: Implements UTF-8 encoding filter to ensure proper character handling across the application
- `Line 127`: Enables Fast Lane Reader pattern for direct JDBC database access from web tier, bypassing EJB layer for read operations
- `Line 166`: Configures component manager and web controller through environment entries rather than hard-coding
- `Line 173`: Establishes local EJB references using ejb-link to connect web tier with business logic components
### petstore/src/docroot/populate/Populate-UTF8.xml

Populate-UTF8.xml is a structured XML data file used to initialize the Java Pet Store database with sample data. It defines the schema and content for users, customers, and a product catalog in multiple languages (English, Japanese, Chinese). The file contains detailed definitions for categories (Fish, Dogs, Reptiles, Cats, Birds), products within those categories, and specific items with attributes like price, description, and images. Each entity includes multilingual variants with appropriate translations and localized pricing. The file serves as the foundation for demonstrating the internationalization capabilities of the Pet Store application while providing realistic sample data for testing and demonstration purposes.

 **Code Landmarks**
- `Line 3`: Copyright notice and license terms for Sun Microsystems code redistribution
- `Line 42`: DOCTYPE declaration defines the structure using multiple external DTD files
- `Line 72`: User definitions include multilingual accounts with identical passwords for demonstration
- `Line 175`: Product categories include multilingual details with translations in English, Japanese and Chinese
- `Line 534`: Item pricing varies by language/region, demonstrating internationalization support for currency differences
### petstore/src/docroot/populate/PopulateSQL.xml

PopulateSQL.xml defines the database schema creation and data manipulation statements for the Java Pet Store application. It contains SQL definitions for two database types: Cloudscape and Oracle. For each database, it defines table structures for the e-commerce catalog including category, product, and item tables along with their detail tables. Each table definition includes check statements to verify existence, create statements with primary and foreign key constraints, insert statements with parameterized queries, and drop statements for cleanup. The file follows a structured XML format with DatabaseStatements as the top-level elements containing TableStatements for each entity in the data model.

 **Code Landmarks**
- `Line 40`: Uses a DTD (Document Type Definition) for XML validation
- `Line 43`: Organizes SQL statements by database type, allowing database-specific implementations
- `Line 44-198`: Implements a complete relational schema with proper foreign key constraints for an e-commerce catalog
- `Line 199`: Duplicates schema definitions with database-specific data types (char vs varchar) for Oracle compatibility
### petstore/src/docroot/populate/dtds/Account.dtd

Account.dtd defines the XML document structure for customer account information in the Java Pet Store application. It specifies that an Account element must contain a ContactInfo element and may optionally include a CreditCard element. The file imports two external DTD entities: ContactInfo and CreditCard, which are referenced from separate files using the SYSTEM keyword. This structure ensures proper validation of XML documents representing customer accounts in the application's data population process.

 **Code Landmarks**
- `Line 37`: Uses external entity references to modularize the DTD structure by importing ContactInfo.dtd and CreditCard.dtd
### petstore/src/docroot/populate/dtds/Address.dtd

Address.dtd defines the Document Type Definition for address data in the Java Pet Store application. It specifies the structure of XML documents containing address information with a root element 'Address' that contains child elements for address components. The DTD requires elements for street name (with an optional second street line), city, state, and zip code, while making the country element optional. This standardized structure ensures consistent address formatting throughout the application when processing customer shipping and billing information.

 **Code Landmarks**
- `Line 39`: The Address element allows for an optional second StreetName element, providing flexibility for multi-line addresses
### petstore/src/docroot/populate/dtds/Category.dtd

Category.dtd defines the Document Type Definition for XML Category data in the Java Pet Store application. It establishes the structure for Category elements, requiring each to have a unique ID attribute and contain one or more CategoryDetails elements. The file imports the CategoryDetails.dtd definition using an external entity reference. This DTD is part of the data population mechanism, providing schema validation for category information used in the pet store catalog system.

 **Code Landmarks**
- `Line 39`: Uses external entity reference to include CategoryDetails.dtd, demonstrating modular DTD design
### petstore/src/docroot/populate/dtds/CategoryDetails.dtd

CategoryDetails.dtd defines the Document Type Definition for category details XML data in the Java Pet Store application. It specifies the structure for CategoryDetails elements, which must contain a Name element and may optionally include Image and Description elements. The DTD requires a mandatory xml:lang attribute for the CategoryDetails element to specify the language. This file ensures that XML documents containing category information conform to a consistent structure when populating the pet store database.

 **Code Landmarks**
- `Line 40`: The DTD defines a simple but flexible structure allowing for multilingual category descriptions through the required xml:lang attribute
### petstore/src/docroot/populate/dtds/CommonElements.dtd

CommonElements.dtd defines three fundamental XML elements used throughout the Java Pet Store application's data structures. The file establishes DTD declarations for Name, Image, and Description elements, all of which contain parsed character data (#PCDATA). These elements likely represent common attributes used across various XML documents in the application, providing standardization for product information display. The file includes Sun Microsystems' copyright notice and redistribution terms, indicating it's part of the official Java Pet Store reference implementation.

 **Code Landmarks**
- `Line 37-39`: Defines three core XML elements (Name, Image, Description) that form the foundation of the application's data model
### petstore/src/docroot/populate/dtds/ContactInfo.dtd

ContactInfo.dtd defines the XML document structure for contact information in the Java Pet Store application. It establishes a hierarchical structure for customer contact details with elements including FamilyName, GivenName, Address, Email, and Phone. The DTD imports the Address structure from an external Address.dtd file through an entity reference. This standardized format ensures consistent representation of customer contact information throughout the application, particularly for data import/export operations and customer record management.

 **Code Landmarks**
- `Line 47`: External entity reference imports Address.dtd, demonstrating modular DTD design for reusable components
### petstore/src/docroot/populate/dtds/CreditCard.dtd

This DTD (Document Type Definition) file defines the XML structure for credit card information used in the Java Pet Store application. It specifies a simple schema with a root element 'CreditCard' that contains three child elements: 'CardNumber', 'CardType', and 'ExpiryDate'. Each child element is defined to contain parsed character data (#PCDATA). This structure provides a standardized format for handling credit card information during order processing in the e-commerce application.

 **Code Landmarks**
- `Line 37`: Simple hierarchical structure with a single parent element and three child elements for credit card data representation
### petstore/src/docroot/populate/dtds/Customer.dtd

Customer.dtd defines the XML document structure for customer data in the Java Pet Store application. It establishes a Customer element that can contain optional Account and Profile child elements. The DTD requires each Customer element to have an 'id' attribute as an IDREF. It imports two external DTD entities: Account.dtd and Profile.dtd, which define the structure of the Account and Profile elements respectively. This file is part of the data population mechanism, providing validation rules for customer information XML documents.

 **Code Landmarks**
- `Line 38`: Uses external entity references to modularize the DTD structure by importing Account.dtd and Profile.dtd definitions
### petstore/src/docroot/populate/dtds/Item.dtd

This DTD file defines the XML structure for Item entities in the Java Pet Store application. It establishes that an Item element must contain one or more ItemDetails elements and requires two attributes: 'id' (a unique identifier) and 'product' (a reference to a product ID). The file imports the ItemDetails DTD using an external entity reference, allowing for modular definition of the complete Item structure. This schema supports the population of the product catalog database with structured item data.

 **Code Landmarks**
- `Line 38`: Uses XML entity reference to modularly include another DTD file (ItemDetails.dtd)
### petstore/src/docroot/populate/dtds/ItemDetails.dtd

ItemDetails.dtd defines the Document Type Definition for XML data representing pet store item details. It specifies the structure for product information including pricing, attributes, images, and descriptions. The DTD establishes that an ItemDetails element must contain ListPrice, UnitCost, optional Attributes (up to 5), Image, and Description elements. Each ItemDetails element requires an xml:lang attribute to specify the language. This file ensures consistent formatting of product data used in the population and display of item information throughout the Pet Store application.

 **Code Landmarks**
- `Line 39`: The DTD allows for up to five optional Attribute elements within each ItemDetails element, providing flexibility for different product types.
- `Line 40`: The xml:lang attribute requirement ensures proper internationalization support for item details.
### petstore/src/docroot/populate/dtds/PopulateSQL.dtd

PopulateSQL.dtd defines the Document Type Definition (DTD) for XML files used to populate the Pet Store database. It structures how SQL statements should be organized for database population. The DTD establishes a hierarchy where PopulateSQL contains multiple DatabaseStatements elements, each containing TableStatements elements. Each TableStatements includes optional CheckStatement and mandatory CreateStatement, InsertStatement, and DropStatement elements. The DTD enforces proper organization of SQL operations by database and table, ensuring consistent database initialization across the application.

 **Code Landmarks**
- `Line 36`: Defines a hierarchical structure for organizing SQL statements by database and table
### petstore/src/docroot/populate/dtds/Product.dtd

This DTD file defines the structure for Product elements in the Java Pet Store application's XML data model. It establishes that a Product element must contain one or more ProductDetails elements and requires two attributes: an 'id' attribute that must be unique (ID type) and a 'category' attribute that references another element's ID (IDREF type). The file also imports additional element definitions from ProductDetails.dtd using an external entity reference, creating a modular DTD structure for the product catalog system.

 **Code Landmarks**
- `Line 39`: Uses external entity reference to modularly include ProductDetails.dtd definitions, demonstrating XML modularization best practices
### petstore/src/docroot/populate/dtds/ProductDetails.dtd

ProductDetails.dtd defines the Document Type Definition for product details XML documents in the Java Pet Store application. It establishes the structure for product information with a required root element 'ProductDetails' that must contain a 'Name' element and may optionally include 'Image' and 'Description' elements. The DTD requires an xml:lang attribute on the ProductDetails element to specify the language of the content. This file ensures consistent formatting of product information across the application's catalog system.

 **Code Landmarks**
- `Line 39`: The DTD enforces a required xml:lang attribute to support internationalization of product details.
### petstore/src/docroot/populate/dtds/Profile.dtd

Profile.dtd defines the Document Type Definition for user profile XML documents in the Java Pet Store application. It specifies the structure and elements required for valid profile documents. The DTD declares a Profile root element containing four child elements: PreferredLanguage for storing language settings, FavoriteCategory for user's preferred product category, MyListPreference for list display preferences, and BannerPreference for banner display settings. Each child element is defined to contain parsed character data (#PCDATA).
### petstore/src/docroot/populate/dtds/User.dtd

User.dtd defines the Document Type Definition for XML user data in the Java Pet Store application. It specifies a simple structure for user information with two main elements: User and Password. The User element contains a Password element and requires an 'id' attribute that serves as a unique identifier. The Password element is defined to contain parsed character data (#PCDATA). This DTD provides the structural validation rules for XML documents representing user accounts in the system.
### petstore/src/ejb-jar-manifest.mf

This manifest file defines the Class-Path entries for the Java Pet Store EJB JAR file. It specifies dependencies on various client JAR files for different EJB components including asyncsender, uidgen, signon, cart, customer, catalog, and purchase order (po) modules. Additionally, it includes utility libraries such as tracer, servicelocator, and xmldocuments that provide supporting functionality for the EJB components.

 **Code Landmarks**
- `Line 1-11`: The manifest structure demonstrates the modular architecture of the Pet Store application, with clear separation between different business domain components.
### petstore/src/ejb-jar.xml

This ejb-jar.xml deployment descriptor configures the Enterprise JavaBeans components for the Java Pet Store application. It defines two session beans: ShoppingControllerEJB and ShoppingClientFacadeEJB. ShoppingControllerEJB is a stateful session bean that processes various events like SignOnEvent, CustomerEvent, OrderEvent, and CartEvent through mapped EJB actions. ShoppingClientFacadeEJB provides a facade for shopping-related operations. The file establishes local EJB references to other components like Catalog, ShoppingCart, SignOn, and AsyncSender beans. It also configures method permissions (all methods are unchecked) and transaction attributes for various methods, ensuring proper transaction management for shopping operations.

 **Code Landmarks**
- `Line 52`: Maps event types to specific EJB action implementation classes through environment entries
- `Line 84`: Uses EJB local references to establish relationships between components rather than remote interfaces for better performance
- `Line 172`: Defines container-managed transactions with Required attribute for shopping operations
- `Line 149`: Implements security through method permissions with unchecked access, allowing any client to access the EJBs
### petstore/src/sun-j2ee-ri.xml

This sun-j2ee-ri.xml file is a J2EE Reference Implementation deployment descriptor for the Java Pet Store application. It defines server-specific configurations for the application's Enterprise JavaBeans (EJBs), including JNDI names, database mappings, and resource references. The file configures multiple EJB modules including customer, signon, catalog, shopping cart, and order processing components. For each EJB, it specifies SQL statements for CMP (Container-Managed Persistence) operations like creating tables, finding records, and managing relationships between entities. The descriptor also defines security configurations, resource references to databases, and message queues used by the application. This configuration file enables the J2EE container to properly deploy and manage the Pet Store application components.

 **Code Landmarks**
- `Line 42`: Defines the context root for the web application as 'petstore'
- `Line 46`: Contains database credentials in plaintext, which would be a security concern in production environments
- `Line 73`: SQL statements for CMP operations are explicitly defined rather than being container-generated
- `Line 578`: Configures JMS messaging infrastructure for asynchronous order processing
- `Line 626`: Demonstrates EJB reference linking between different application modules
### supplier/src/application.xml

This application.xml file serves as the J2EE deployment descriptor for the Supplier component of the Java Pet Store application. It defines the structure and components of the Supplier enterprise application archive (EAR). The file specifies three modules: a supplier purchase order EJB module (supplierpo-ejb.jar), a web module (supplier.war) with context root 'supplier', and a main supplier EJB module (supplier-ejb.jar). It also defines an 'administrator' security role for access control within the application.

 **Code Landmarks**
- `Line 54`: Defines the application's modular structure with three distinct components: two EJB modules and one web module
- `Line 61`: Sets the web context root to 'supplier' which determines the URL path for accessing the web module
- `Line 66`: Establishes an 'administrator' security role for role-based access control
### supplier/src/build.bat

This batch file provides a build script for the supplier component of the Java Pet Store application. It sets up the environment for Apache Ant by configuring necessary environment variables including ANT_HOME and constructing the ANT_CLASSPATH with references to Java tools, Ant libraries, and J2EE dependencies. The script then executes the Java runtime with appropriate classpath settings and system properties to run the Ant build tool, passing along any command-line arguments to Ant. This enables developers to build the supplier application on Windows systems with a standardized environment configuration.

 **Code Landmarks**
- `Line 44`: Sets up a comprehensive classpath that includes Java tools, Ant libraries, and J2EE dependencies
- `Line 47`: Uses parameter passing (%1 %2 %3 %4) to allow flexible Ant target execution
### supplier/src/build.sh

This build script automates the compilation process for the supplier application component of Java Pet Store. It first checks for required environment variables (JAVA_HOME and J2EE_HOME), setting up the Java command path if needed. The script then configures Ant by establishing the classpath with necessary JAR files including Ant libraries, Java tools, and J2EE components. Finally, it executes the Ant build tool with the specified parameters, passing along any command-line arguments. The script ensures all dependencies are properly set before initiating the build process.

 **Code Landmarks**
- `Line 45`: Determines Java location dynamically if JAVA_HOME isn't set by using 'which java'
- `Line 68`: Sets up Ant classpath with three critical components: Ant JAR, Java tools, and J2EE libraries
### supplier/src/build.xml

This build.xml file defines the Ant build process for the Supplier Application component of Java Pet Store. It manages compilation, packaging, and deployment of the application. The script defines targets for initializing properties, compiling source code, creating EJB JARs, building WAR files, assembling EAR packages, and deploying to J2EE servers. It also handles dependencies on other components like supplierpo, xmldocuments, servicelocator, and processmanager. Key targets include init, compile, ejbjar, war, ear, deploy, undeploy, and core. The file establishes classpaths, directory structures, and file locations necessary for the build process.

 **Code Landmarks**
- `Line 76-82`: Defines component dependencies showing the modular architecture of the application
- `Line 166-179`: EAR packaging process that bundles multiple components into a deployable enterprise application
- `Line 186-196`: Deployment targets that integrate with J2EE server tools for application installation
### supplier/src/com/sun/j2ee/blueprints/supplier/inventory/ejb/InventoryEJB.java

InventoryEJB implements an Entity Bean that manages inventory items in the supplier application. It provides container-managed persistence for inventory items with itemId and quantity fields. The class offers methods to access and modify these fields, including a specialized reduceQuantity method to decrease inventory levels. The bean implements standard EJB lifecycle methods required by the EntityBean interface, including ejbCreate for initializing new inventory items, and context management methods. There is also a commented-out addQuantity method that would increase inventory levels.

 **Code Landmarks**
- `Line 75`: The reduceQuantity method demonstrates business logic implementation within an entity bean
- `Line 85`: The addQuantity method is commented out but shows how the component was designed for bidirectional inventory adjustments
- `Line 99`: The ejbCreate method shows the initialization pattern for container-managed persistence fields
### supplier/src/com/sun/j2ee/blueprints/supplier/inventory/ejb/InventoryLocal.java

InventoryLocal defines the local interface for the Inventory CMP (Container-Managed Persistence) Bean in the supplier application. It extends EJBLocalObject and specifies methods for inventory management operations. The interface provides getter methods for retrieving item ID and quantity, a setter for updating quantity, and a business method for reducing inventory quantities. There's also a commented-out addQuantity method that is currently unused. This interface serves as the contract for local clients to interact with the inventory management functionality.

 **Code Landmarks**
- `Line 46`: Interface extends EJBLocalObject, making it a local EJB component interface in the J2EE architecture
- `Line 55`: Contains commented code indicating functionality that was implemented but later disabled
### supplier/src/com/sun/j2ee/blueprints/supplier/inventory/ejb/InventoryLocalHome.java

InventoryLocalHome interface defines the local home interface for the Inventory CMP Bean in the supplier application. It extends EJBLocalHome and provides methods for creating and finding inventory items. The interface includes a create method that takes an itemId and quantity parameter, and two finder methods: findByPrimaryKey to locate inventory by itemId and findAllInventoryItems to retrieve all inventory items. This interface is part of the EJB component architecture for managing inventory in the supplier subsystem.

 **Code Landmarks**
- `Line 46`: Follows the EJB 2.0 pattern for local interfaces, which provides more efficient access than remote interfaces for components deployed in the same container
### supplier/src/com/sun/j2ee/blueprints/supplier/inventory/web/DisplayInventoryBean.java

DisplayInventoryBean implements a simple JavaBean that serves as a bridge between the web layer and the inventory EJB in the supplier application. It provides functionality to retrieve all inventory items with their quantities for display purposes. The class uses the Service Locator pattern to obtain a reference to the InventoryLocalHome interface, which it then uses to query for all inventory items. The main method getInventory() returns a Collection of inventory items that can be displayed in the receiver application's user interface.

 **Code Landmarks**
- `Line 58`: Uses the Service Locator pattern to abstract away JNDI lookup complexity
- `Line 61`: Lazy initialization of the EJB home reference only when needed
### supplier/src/com/sun/j2ee/blueprints/supplier/inventory/web/JNDINames.java

JNDINames provides a centralized repository of JNDI lookup names used throughout the supplier inventory web component. It implements a non-instantiable utility class containing static final String constants that define the JNDI paths for accessing the Inventory EJB, OrderFulfillmentFacade EJB, UserTransaction, and SupplierOrderTD transition delegate. These constants ensure consistent naming across the application and must be synchronized with deployment descriptors. The class prevents instantiation through a private constructor and serves as a single point of maintenance for JNDI path references.

 **Code Landmarks**
- `Line 46`: Uses a private constructor pattern to prevent instantiation of this utility class
- `Line 50-57`: Constants use java:comp/env/ prefix following J2EE naming conventions for resource references
### supplier/src/com/sun/j2ee/blueprints/supplier/inventory/web/RcvrRequestProcessor.java

RcvrRequestProcessor implements a servlet for handling inventory management requests from the supplier component's receiver role. It provides functionality to update inventory quantities and process pending purchase orders. The class connects to inventory and order fulfillment EJBs through service locators, manages transactions, and forwards responses to appropriate JSP pages. Key methods include updateInventory() which modifies item quantities, sendInvoices() which uses a transition delegate to process invoices, and doPost() which handles different screen actions including inventory display and updates. Important variables include inventoryHomeRef, orderFacadeHomeRef, and transitionDelegate.

 **Code Landmarks**
- `Line 88`: Uses service locator pattern to obtain EJB references, abstracting JNDI lookup complexity
- `Line 131`: Implements a transition delegate pattern for sending invoices through a factory-created delegate
- `Line 166`: Uses container-managed transactions with UserTransaction to ensure ACID properties during inventory updates
### supplier/src/com/sun/j2ee/blueprints/supplier/orderfulfillment/ejb/JNDINames.java

JNDINames is a utility class that centralizes JNDI naming constants used throughout the supplier order fulfillment system. It defines static final String constants for various Enterprise JavaBeans (EJBs) including SupplierOrder, LineItem, and Inventory, as well as XML validation parameters and schema locations. The class has a private constructor to prevent instantiation, making it function purely as a container for constants. These JNDI names are critical reference points that must match corresponding entries in deployment descriptors, ensuring proper lookups of EJB components and configuration parameters within the J2EE application.

 **Code Landmarks**
- `Line 47`: Private constructor prevents instantiation of this utility class - a good practice for classes that only contain constants.
- `Line 49-68`: JNDI names follow a consistent naming pattern with java:comp/env prefix, indicating they are environment entries in the application's JNDI context.
### supplier/src/com/sun/j2ee/blueprints/supplier/orderfulfillment/ejb/OrderFulfillmentFacadeEJB.java

OrderFulfillmentFacadeEJB implements a session bean that serves as a facade for processing supplier purchase orders in the Java Pet Store application. It handles persisting purchase orders, checking inventory availability, fulfilling orders by shipping available items, and generating invoices. Key functionality includes processing new purchase orders (processPO), handling pending orders when inventory becomes available (processPendingPO), checking inventory levels, and creating invoice XML documents. The class uses service locators to access other EJBs like SupplierOrderLocal and InventoryLocal, and works with XML document handlers for processing order and invoice documents.

 **Code Landmarks**
- `Line 137`: The checkInventory method silently handles FinderExceptions, allowing partial order fulfillment when items aren't found in inventory
- `Line 156`: Creates structured XML invoices from order data, demonstrating XML document generation in J2EE applications
- `Line 216`: Implements a two-phase process: first persisting the order, then attempting to fulfill it from inventory
- `Line 235`: Processes pending orders when inventory arrives, showing how the system handles asynchronous fulfillment
### supplier/src/com/sun/j2ee/blueprints/supplier/orderfulfillment/ejb/OrderFulfillmentFacadeLocal.java

OrderFulfillmentFacadeLocal defines a local EJB interface for the supplier's order fulfillment system. It provides methods for processing purchase orders within the Java Pet Store application. The interface extends EJBLocalObject and declares two key methods: processPO() for handling individual purchase orders in XML format, and processPendingPO() for batch processing pending purchase orders. Both methods throw appropriate exceptions for creation, finding, and XML document handling errors.

 **Code Landmarks**
- `Line 45`: Interface extends EJBLocalObject, indicating it's designed as a local business interface in the EJB 2.x architecture
### supplier/src/com/sun/j2ee/blueprints/supplier/orderfulfillment/ejb/OrderFulfillmentFacadeLocalHome.java

OrderFulfillmentFacadeLocalHome defines a local home interface for the supplier's order fulfillment facade EJB in the Java Pet Store application. It extends the EJBLocalHome interface from the javax.ejb package, following the EJB design pattern. The interface declares a single create() method that throws CreateException and returns an OrderFulfillmentFacadeLocal object. This interface serves as the factory for creating local EJB instances that handle order fulfillment operations in the supplier component of the application.
### supplier/src/com/sun/j2ee/blueprints/supplier/orderfulfillment/ejb/TPASupplierOrderXDE.java

TPASupplierOrderXDE implements an XML document editor for transforming supplier orders between XML format and Java objects. It extends XMLDocumentEditor.DefaultXDE to handle TPA-SupplierOrder documents. Key functionality includes parsing XML supplier orders, validating against schemas, and transforming documents using XSLT stylesheets. The class manages entity catalogs, validation settings, and schema URIs. Important methods include setDocument() for parsing XML into SupplierOrder objects and getSupplierOrder() for retrieving the parsed object. The class also provides a main() method for command-line testing of XML parsing functionality.

 **Code Landmarks**
- `Line 58`: Uses a properties file to load stylesheet catalog information dynamically
- `Line 83`: Implements flexible XML transformation with conditional XSD support
- `Line 103`: Converts XML documents to domain objects through DOM transformation
### supplier/src/com/sun/j2ee/blueprints/supplier/processpo/ejb/JNDINames.java

JNDINames is a utility class that serves as a central repository for JNDI name constants used in the supplier module of the Java Pet Store application. It defines string constants for JMS resources (topic connection factory and invoice topic) and EJB references (OrderFulfillmentFacade) needed by the purchase order processing components. The class prevents instantiation through a private constructor and provides static final fields that maintain consistency between code references and deployment descriptor configurations. These constants facilitate the lookup of enterprise resources in a standardized way across the supplier application.

 **Code Landmarks**
- `Line 44`: Private constructor prevents instantiation, enforcing the class's role as a static utility
- `Line 48-51`: JMS resource JNDI names follow the java:comp/env naming convention for portable resource references
### supplier/src/com/sun/j2ee/blueprints/supplier/processpo/ejb/SupplierOrderMDB.java

SupplierOrderMDB implements a message-driven bean that handles supplier functionality in the Java Pet Store application. It receives purchase orders as JMS messages from the Order Processing Center (OPC), processes them through the OrderFulfillmentFacade, and sends back invoices when orders are shipped. Key methods include onMessage() which processes incoming JMS messages, doWork() which handles the business logic of processing purchase orders, and doTransition() which manages sending invoices back to OPC. The class uses ServiceLocator to obtain references to EJBs and implements the MessageDrivenBean and MessageListener interfaces to function within the J2EE messaging architecture.

 **Code Landmarks**
- `Line 75`: Uses a TransitionDelegate pattern loaded dynamically through JNDI configuration for flexible message routing
- `Line 94`: Implements asynchronous order processing through JMS messaging, decoupling supplier from order processing center
- `Line 106`: Conditional transition handling - only sends invoice if orders were actually shipped
### supplier/src/com/sun/j2ee/blueprints/supplier/rsrc/SupplierOrderStyleSheetCatalog.properties

This properties file defines mappings between XML document type definitions (DTDs) and their corresponding XSL stylesheets for supplier order processing. It maps both Sun Microsystems J2EE Blueprints DTDs and HTTP namespaces to specific XSL transformation files located in the supplier resource directory. The file also includes boolean flags indicating XSD support status for each document type, which helps the application determine how to process different types of supplier orders.

 **Code Landmarks**
- `Line 1-2`: Maps Sun Microsystems DTD identifiers to specific XSL stylesheet paths with escaped spaces in the identifiers
- `Line 4-5`: Maps HTTP namespace URLs to XSL stylesheets with explicit XSD support flags
### supplier/src/com/sun/j2ee/blueprints/supplier/tools/populate/InventoryPopulator.java

InventoryPopulator implements a tool for populating the supplier's inventory database with data from XML files. It provides functionality to create inventory records by parsing XML data and inserting it into the database through EJB interfaces. The class uses SAX parsing with custom XMLFilter implementation to process inventory entries. Key methods include setup() which configures the XML parser, check() which verifies if inventory data exists, and createInventory() which creates or updates inventory records. Important variables include JNDI_INVENTORY_HOME for EJB lookup, XML_* constants for XML parsing, and inventoryHome for database operations.

 **Code Landmarks**
- `Line 59`: Uses a custom XMLFilter implementation with anonymous inner class for XML parsing
- `Line 89`: Implements idempotent database population by attempting to remove existing records before creating new ones
- `Line 73`: Check method determines if database already contains inventory data to avoid duplicate population
### supplier/src/com/sun/j2ee/blueprints/supplier/tools/populate/PopulateException.java

PopulateException implements a custom exception class that can wrap another exception, providing layered error handling for the supplier database population tool. It offers three constructors: one with a message and wrapped exception, one with just a message, and one with just an exception. The class provides methods to retrieve both the directly wrapped exception (getException) and the root cause exception through recursive unwrapping (getRootCause). This allows for maintaining the complete exception chain while providing specific error context for database population operations.

 **Code Landmarks**
- `Line 85`: The getRootCause() method recursively unwraps nested exceptions to find the original cause, demonstrating a pattern for handling layered exceptions
### supplier/src/com/sun/j2ee/blueprints/supplier/tools/populate/PopulateServlet.java

PopulateServlet implements a servlet that loads inventory data into the supplier database from an XML file. It handles both GET and POST requests to trigger the population process, with options to force repopulation even if data already exists. The servlet uses SAX parsing to process the XML data and delegates the actual database operations to an InventoryPopulator class. Key functionality includes checking if the database is already populated, parsing XML data, and redirecting to success or error pages after completion. Important elements include populateDataPath, populate(), getResource(), and redirect() methods.

 **Code Landmarks**
- `Line 77`: The populate method intelligently checks if data already exists before attempting to populate the database, unless forced.
- `Line 107`: The getResource method handles both URL and relative path resources, providing flexibility in data file location.
- `Line 116`: Custom URL handling in redirect() method supports both absolute and relative paths with special handling for paths starting with '//'
### supplier/src/com/sun/j2ee/blueprints/supplier/tools/populate/XMLDBHandler.java

XMLDBHandler implements an abstract SAX parser handler for populating databases from XML data. It extends XMLFilterImpl to process XML elements and extract values into a context map. The handler tracks parsing state (OFF, READY, PARSING) as it processes root and child elements, storing attribute values and element content. Key functionality includes value extraction, context management, and abstract methods for database operations. Important methods include startElement(), endElement(), characters(), getValue() variants, and abstract create() and update() methods that subclasses must implement to perform actual database operations.

 **Code Landmarks**
- `Line 64`: Implements lazy instantiation pattern through optional parameter to control when database objects are created
- `Line 155`: Smart key naming system that automatically handles duplicate elements by appending array-style indices
- `Line 168`: Hierarchical value lookup that first checks local values then falls back to context values
### supplier/src/com/sun/j2ee/blueprints/supplier/transitions/JNDINames.java

JNDINames is a utility class that centralizes JNDI resource name constants used in the supplier component of the Java Pet Store application. It defines string constants for JMS resources including a topic connection factory and an invoice MDB topic. The class prevents instantiation with a private constructor, ensuring it's used only as a static utility. These JNDI names must be synchronized with the application's deployment descriptors to ensure proper resource lookup in the J2EE environment.

 **Code Landmarks**
- `Line 46`: Private constructor prevents instantiation, enforcing usage as a static utility class
- `Line 49`: Constants are prefixed with java:comp/env/ following J2EE JNDI naming conventions for portable resource references
### supplier/src/com/sun/j2ee/blueprints/supplier/transitions/SupplierOrderTD.java

SupplierOrderTD implements the TransitionDelegate interface for handling supplier order transitions in the Java Pet Store application. It facilitates communication between the supplier system and the order processing system by sending invoice messages to a JMS topic. The class uses ServiceLocator to obtain JMS resources (TopicConnectionFactory and Topic) and contains a TopicSender for publishing messages. Key methods include setup() for initializing JMS resources and doTransition() which sends XML invoice messages to the configured topic. The class handles exceptions by wrapping them in TransitionException.

 **Code Landmarks**
- `Line 53`: Uses the Service Locator pattern to obtain JMS resources, promoting loose coupling
- `Line 69`: Implements the TransitionDelegate interface as part of a state machine pattern for order processing
- `Line 71`: Uses JMS Topic for asynchronous message delivery, enabling decoupled communication between components
### supplier/src/com/sun/j2ee/blueprints/supplier/transitions/TopicSender.java

TopicSender implements a serializable helper class responsible for sending messages to a JMS Topic, specifically for the Invoice MDB Topic in the Supplier application. It manages topic-related resources through a TopicConnectionFactory and Topic instance provided during initialization. The class offers a sendMessage() method that accepts XML messages as strings, creates a JMS connection, publishes the message to the topic, and properly closes the connection afterward. The implementation handles JMS connection lifecycle and provides proper exception handling for JMS operations.

 **Code Landmarks**
- `Line 74`: Uses try-finally block to ensure connection resources are properly closed even if exceptions occur
- `Line 65`: Creates a non-transactional JMS session with AUTO_ACKNOWLEDGE mode for reliable message delivery
### supplier/src/docroot/WEB-INF/web.xml

This web.xml file configures the web application deployment descriptor for the Supplier module of the Java Pet Store. It defines two servlets: RcvrRequestProcessor for handling supplier inventory requests and PopulateServlet for data population. The file establishes servlet mappings, session configuration, JMS resource references, security constraints requiring administrator role for accessing RcvrRequestProcessor, form-based authentication, and EJB local references to OrderFulfillmentFacade and Inventory components. It also includes environment entries for transition delegates and sets a welcome file.

 **Code Landmarks**
- `Line 94`: Security constraint implementation restricting access to RcvrRequestProcessor to administrator role only
- `Line 104`: Form-based authentication configuration with custom login and error pages
- `Line 127`: EJB local references showing the application's component architecture with OrderFulfillmentFacade and Inventory beans
### supplier/src/docroot/index.html

This HTML file serves as the landing page for the Java Pet Store Supplier application. It provides a minimal interface with a title, heading, and a single hyperlink that directs users to the supplier section through the RcvrRequestProcessor servlet. The page has a white background and contains copyright information for Sun Microsystems from 2001.

 **Code Landmarks**
- `Line 18`: The link to '/supplier/RcvrRequestProcessor' indicates this is an entry point to the supplier application functionality through a servlet
### supplier/src/docroot/populate/Populate-UTF8.xml

Populate-UTF8.xml is an XML data file that defines the initial inventory quantities for products in the supplier application of the Java Pet Store. It contains a structured list of inventory items, each with a unique product ID (EST-1 through EST-29) and a quantity value (all set to 10,000). The file includes a Document Type Definition (DTD) that specifies the structure of the inventory list, requiring each inventory element to have both an id and quantity attribute. This file likely serves as seed data for populating the supplier's inventory database during application initialization.

 **Code Landmarks**
- `Line 40`: DTD declaration defines strict structure for inventory items requiring both id and quantity attributes
- `Line 49`: All inventory items are initialized with the same quantity (10000), suggesting this is test/sample data
### supplier/src/ejb-jar-manifest.mf

This manifest file specifies the class path dependencies for the supplier EJB module. It defines four JAR dependencies that the supplier EJB requires: supplierpo-ejb-client.jar, servicelocator.jar, xmldocuments.jar, and processmanager-ejb-client.jar. These dependencies indicate the supplier module integrates with purchase orders, service location, XML processing, and process management components.
### supplier/src/sun-j2ee-ri.xml

This XML configuration file defines the J2EE Reference Implementation specific settings for the supplier application component of Java Pet Store. It configures role mappings, web module settings, resource references, and enterprise bean definitions. The file includes detailed SQL statements for CMP entity beans, defining database operations like create, read, update, and delete for supplier-related entities such as LineItem, Address, SupplierOrder, ContactInfo, and Inventory. It also configures message-driven beans, JNDI names, resource references, and database connection details for the supplier subsystem.

 **Code Landmarks**
- `Line 46`: Role mapping configuration assigns the 'supplier' principal to the 'administrator' role
- `Line 59`: Resource reference configuration for JMS connection factories and topics
- `Line 78`: Detailed SQL statements for CMP entity beans with database schema definitions
- `Line 297`: Configuration of relationship mapping between SupplierOrder and LineItem entities
- `Line 368`: Message-driven bean configuration for order processing via JMS

[Generated by the Sage AI expert workbench: 2025-03-29 21:37:00  https://sage-tech.ai/workbench]: #