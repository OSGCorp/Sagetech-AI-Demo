# Component Source of Java Pet Store

Component Source is a critical module within the Java Pet Store 1.3.2 application that implements the core business components powering the e-commerce functionality. Built on J2EE 1.3 technologies, this sub-project provides a comprehensive set of Enterprise JavaBeans (EJBs) that handle the application's domain logic and persistence requirements. The architecture follows a modular approach with clearly defined components for catalog management, shopping cart functionality, order processing, customer management, and supporting services.

This sub-project implements the following key technical capabilities:

- Entity and Session EJBs for domain model persistence
- Business logic components for e-commerce workflows
- Data access objects for database interaction
- XML-based data exchange between components
- Asynchronous messaging for order processing

## Identified Design Elements

1. **Multi-Tier Component Architecture**: Components are organized into logical business domains with well-defined interfaces between them
2. **Container-Managed Persistence**: Entity beans leverage CMP 2.0 for database operations without explicit SQL
3. **Value Object Pattern**: Data transfer objects separate persistence concerns from business logic
4. **Service Locator Pattern**: Centralized JNDI lookup services reduce coupling between components
5. **XML Document Processing**: Standardized DTD-based document exchange between components

## Overview
The architecture emphasizes separation of concerns through a modular design where each component handles a specific business domain. Components like Catalog, ShoppingCart, and PurchaseOrder communicate through well-defined interfaces, promoting maintainability and extensibility. The persistence layer uses Container-Managed Persistence and Relationships (CMP/CMR) to handle database operations, while the business logic is implemented in stateless and stateful session beans. Supporting utilities like ServiceLocator and XMLDocuments provide cross-cutting functionality used throughout the application. This design allows for flexible deployment options and facilitates future enhancements to the system.

## Sub-Projects

### src/components/signon/src

This component implements a comprehensive authentication framework using Enterprise JavaBeans (EJB) architecture with container-managed persistence. The component handles user registration, credential verification, and resource protection through a combination of EJBs, servlets, and filters.

##### Key Capabilities

- User authentication against stored credentials
- New user registration with validation constraints
- Role-based access control for protected resources
- XML-driven configuration for security constraints
- Session management with optional "remember me" functionality

##### Identified Design Elements

1. **EJB-based Authentication Architecture**: Uses a stateless session bean (SignOnEJB) for authentication services and an entity bean (UserEJB) for persistent credential storage
2. **Container-Managed Persistence**: Leverages J2EE container services for data persistence of user credentials
3. **Servlet Filter Security**: Implements request interception via SignOnFilter to enforce access control policies
4. **XML-Driven Configuration**: Uses XML parsing to define protected resources and role-based access controls
5. **Data Access Object Pattern**: Employs SignOnDAO to abstract configuration data access from business logic

##### Overview

The architecture follows a layered approach with clear separation between data persistence (UserEJB), business logic (SignOnEJB), and presentation/control (servlets and filters). Authentication flows begin at the SignOnFilter, which intercepts requests to protected resources and redirects unauthenticated users to a login page. User credentials are validated against the UserEJB through the SignOnEJB facade. The component enforces security constraints on username and password formats and provides comprehensive error handling for authentication failures.

  *For additional detailed information, see the summary for src/components/signon/src.*

### src/components/lineitem/src

The component uses Container-Managed Persistence (CMP) Entity Beans to represent individual items within customer orders. This subproject provides essential e-commerce functionality by managing product selections, quantities, pricing, and shipping status tracking. The component implements both data persistence and business logic for line items through the J2EE Entity Bean architecture.

This provides these capabilities to the Java Pet Store program:

- Order item management with complete product reference information
- Quantity and pricing calculations for order processing
- Shipping status tracking for fulfillment workflows
- XML serialization/deserialization for data exchange

#### Identified Design Elements

1. **EJB Container-Managed Persistence**: Leverages J2EE CMP for automatic persistence of line item data without manual database operations
2. **Local Interface Architecture**: Implements EJBLocalObject pattern for efficient in-container access to line item entities
3. **XML Data Exchange**: Provides XML serialization through DOM manipulation for interoperability with other system components
4. **Transfer Object Pattern**: Uses LineItem class as a data transfer object to encapsulate line item data for business logic operations

#### Overview
The architecture follows standard J2EE patterns with clear separation between interface and implementation. The LineItemEJB class implements the core business logic while the LineItem class serves as both a data transfer object and XML serialization handler. The component is built using Ant with dependencies on the XMLDocuments component for document processing. This design enables seamless integration with order processing, inventory management, and the overall e-commerce workflow of the Java Pet Store application.

  *For additional detailed information, see the summary for src/components/lineitem/src.*

### src/components/util/tracer/src

The Tracer Component implements a lightweight debugging and tracing utility that provides controlled visibility into application execution across the Pet Store's distributed architecture. This sub-project delivers essential diagnostic capabilities to the Java Pet Store program:

- Configurable debug output control via a central flag mechanism
- Streamlined error and exception reporting with context preservation
- Conditional output routing to standard error or standard output
- Simplified developer interface for consistent logging patterns

#### Identified Design Elements

1. Centralized Debug Control: A single boolean flag controls all debug output, allowing developers to enable/disable tracing globally without code changes
2. Exception Context Enhancement: Methods specifically designed to output exceptions with additional contextual information to aid troubleshooting
3. Stream Selection: Flexibility to direct output to either standard error or standard output based on message importance
4. Minimal Performance Impact: Conditional evaluation prevents debug processing overhead when debugging is disabled

#### Overview
The architecture emphasizes simplicity and minimal overhead, making it suitable for both development and production environments. The component is packaged as a standalone JAR (tracer.jar) that can be included in any part of the Pet Store application. Its design follows the principle of least surprise, providing a consistent API for developers to instrument code with debug statements that can be globally controlled. The build process is integrated with Ant, ensuring proper compilation and packaging within the larger Pet Store build system.

  *For additional detailed information, see the summary for src/components/util/tracer/src.*

### src/components/purchaseorder/src

The component uses Container-Managed Persistence (CMP) entity beans to handle purchase order data and relationships. This sub-project implements comprehensive order management along with XML-based data representation capabilities. This provides these capabilities to the Java Pet Store program:

- Complete purchase order data modeling and persistence
- XML serialization/deserialization with DTD validation
- Relationship management between orders and related entities
- Date-based order querying functionality

#### Identified Design Elements

1. Entity Relationship Management: The component implements a network of related entity beans (PurchaseOrder, ContactInfo, Address, CreditCard, LineItem) with defined one-to-one and one-to-many relationships
2. XML Data Transformation: Comprehensive XML handling with support for serialization, deserialization, DOM manipulation, and DTD validation
3. Invoice Processing: Specialized helper functionality for updating line item shipping status and determining order fulfillment
4. Centralized JNDI Configuration: Standardized JNDI naming through the JNDINames utility class ensures consistent EJB lookups

#### Overview
The architecture follows J2EE best practices with clear separation between entity beans and value objects. The component uses container-managed relationships (CMR) to maintain data integrity across related entities. The XML capabilities enable interoperability with external systems through standardized document formats. The build process manages dependencies on other components like xmldocuments, servicelocator, and related entity modules. The design emphasizes maintainability through proper encapsulation and clear interface definitions for both local EJB access and XML-based data exchange.

  *For additional detailed information, see the summary for src/components/purchaseorder/src.*

### src/components/servicelocator/src

The project implements the Service Locator design pattern to abstract and encapsulate all JNDI lookups and resource acquisition, while improving performance through resource caching. This subproject provides these capabilities to the Java Pet Store program:

- Centralized access to J2EE resources (EJB homes, JMS destinations, DataSources)
- Performance optimization through caching of looked-up resources
- Exception handling and abstraction through custom ServiceLocatorException
- Support for both web tier and EJB tier resource access

#### Identified Design Elements

1. Singleton Pattern Implementation: Both web and EJB ServiceLocator classes use singleton patterns to ensure a single point of access for resource lookups
2. Resource Caching: Uses synchronized HashMap to store previously looked-up resources, reducing JNDI lookup overhead
3. Exception Wrapping: Custom ServiceLocatorException encapsulates lower-level exceptions, providing cleaner error handling
4. Dual-Tier Support: Separate implementations for web and EJB tiers with consistent interfaces

#### Overview
The architecture emphasizes separation of concerns by isolating resource lookup logic from business components. The ServiceLocator classes handle all JNDI context creation, lookups, and exception management, allowing client code to focus on business logic rather than infrastructure concerns. The caching strategy significantly improves performance by avoiding repeated lookups of the same resources. The build process is managed through Ant, with clear targets for compilation, JAR creation, and deployment. This pattern implementation serves as a foundational component that other Pet Store modules depend on for accessing enterprise resources.

  *For additional detailed information, see the summary for src/components/servicelocator/src.*

### src/components/catalog/src

The component implements a multi-layered architecture following J2EE patterns to provide catalog browsing, searching, and retrieval capabilities. This sub-project handles the data model, persistence, and business logic for catalog operations, providing these capabilities to the Java Pet Store program:

- Internationalized catalog data access with locale-specific content
- Paginated retrieval of categories, products, and items
- Flexible search functionality across catalog items
- Database-agnostic data access through DAO pattern

#### Identified Design Elements

1. **Multi-Tier Architecture**: Clear separation between data access (DAO), business logic (EJB), and client interfaces, following J2EE best practices
2. **Pluggable Data Access**: Factory pattern implementation allows swapping database implementations without code changes
3. **Internationalization Support**: All catalog operations accept locale parameters to retrieve language-specific content
4. **Pagination Framework**: Custom Page model class enables efficient retrieval of large result sets

#### Overview
The architecture emphasizes flexibility through its DAO abstraction layer, allowing different database implementations (including Cloudscape) while maintaining a consistent interface. The EJB layer provides transaction management and business logic, exposing local interfaces for efficient in-container access. The client helper class implements the Fast Lane Reader pattern, offering both EJB and direct DAO access paths for performance optimization. Data models (Category, Product, Item) are simple serializable classes that support the catalog hierarchy. The component handles internationalization at the database level by dynamically selecting locale-specific tables, making it well-suited for global deployments.

  *For additional detailed information, see the summary for src/components/catalog/src.*

### src/components/mailer/src

This component implements message-driven architecture using JMS for event handling and JavaMail for email delivery. The service processes XML-formatted email requests from other application components, allowing the Pet Store to send transactional emails such as order confirmations to customers.

This subproject provides these capabilities to the Java Pet Store program:

- Asynchronous email processing through JMS message queues
- XML-based email message representation and validation
- Standardized email formatting and delivery
- Exception handling for mail-related operations

#### Identified Design Elements

1. Message-Driven Architecture: Uses a Message-Driven Bean (MailerMDB) to listen for and process email requests from a JMS queue, decoupling email sending from core business processes
2. XML Document Processing: Implements XML serialization/deserialization for email messages with DTD validation
3. Resource Abstraction: Centralizes JNDI resource references and mail session management
4. Exception Handling: Provides specialized exception types for mail-related failures
5. Utility Components: Includes helper classes for MIME content handling and mail sending operations

#### Overview
The architecture follows J2EE best practices with clear separation between messaging, data representation, and mail delivery concerns. The component is built using Ant with defined targets for compilation, packaging, and documentation. The Mail class serves as the core data model, with XML transformation capabilities for interoperability. The MailerMDB processes incoming requests asynchronously, while the MailHelper handles the actual email creation and delivery through the J2EE mail session. This design ensures scalability and reliability for the application's notification requirements.

  *For additional detailed information, see the summary for src/components/mailer/src.*

### src/components/xmldocuments/src

This subproject implements standardized XML document definitions and manipulation utilities along with Trading Partner Agreement (TPA) support for B2B integration. It provides these capabilities to the Java Pet Store program:

- XML document validation against DTDs and XML schemas
- Serialization and deserialization of business objects to/from XML
- Entity resolution for efficient XML processing
- Trading Partner Agreement (TPA) document handling for B2B integration

#### Identified Design Elements

1. **Document Editor Pattern**: The XMLDocumentEditor interface and factory provide a consistent API for manipulating different types of XML documents
2. **Local Entity Resolution**: CustomEntityResolver maps public identifiers to local resources, eliminating network dependencies during XML parsing
3. **Schema-Based Validation**: Support for both DTD and XSD validation ensures document conformity to business rules
4. **Trading Partner Agreement Support**: Specialized TPA document handlers facilitate B2B integration with standardized document formats

#### Overview

The architecture emphasizes modularity through the factory pattern and interface-based design, allowing for extensible document type support. The component includes comprehensive DTD definitions for core business documents (PurchaseOrder, Invoice, SupplierOrder, LineItem) and their TPA counterparts. Document manipulation utilities provide DOM traversal, attribute access, and serialization/deserialization capabilities. The entity resolution system improves performance by mapping remote references to local resources. This subproject forms the foundation for all XML-based data exchange in the Pet Store application, supporting both internal processing and external B2B communication.

  *For additional detailed information, see the summary for src/components/xmldocuments/src.*

### src/components/asyncsender/src

This sub-project implements asynchronous messaging capabilities through JMS (Java Message Service) along with the necessary EJB components to support decoupled communication between system modules. This provides these capabilities to the Java Pet Store program:

- Asynchronous message sending via JMS queues
- Decoupled communication between application components
- Stateless session bean implementation for message processing
- Standardized JNDI resource access

#### Identified Design Elements

1. JMS Integration: Provides a clean abstraction for asynchronous messaging through a standardized JMS interface
2. Service Locator Pattern: Uses a ServiceLocator to obtain JMS resources from JNDI, promoting loose coupling
3. EJB Component Model: Implements the standard EJB lifecycle and interface patterns for container management
4. Resource Management: Carefully manages JMS connections and sessions to prevent resource leaks
5. Centralized JNDI Configuration: Maintains consistent naming through the JNDINames utility class

#### Overview
The architecture emphasizes separation of concerns by isolating messaging functionality in dedicated components. The AsyncSenderEJB acts as the core implementation, handling the details of JMS connection management and message transmission. The design follows J2EE best practices with clear interface definitions (AsyncSender and AsyncSenderLocalHome) and proper resource handling. The build process is managed through Ant, with targets for compilation, packaging, and documentation generation. This component enables other parts of the Pet Store application to communicate asynchronously, enhancing scalability and responsiveness.

  *For additional detailed information, see the summary for src/components/asyncsender/src.*

### src/components/encodingfilter/src

The program implements a complete online shopping experience and showcases J2EE component integration. This sub-project implements HTTP request character encoding standardization along with filter-based preprocessing of web requests. This provides these capabilities to the Java Pet Store program:

- Consistent character encoding across all HTTP requests
- Configurable encoding settings through filter initialization parameters
- Transparent request processing that requires no changes to application code
- Prevention of character corruption in multi-language content

#### Identified Design Elements

1. Servlet Filter Implementation: Leverages the J2EE Filter interface to intercept and process requests before they reach application servlets
2. Configuration-Driven Encoding: Uses filter initialization parameters to determine which character encoding to apply
3. Request Preprocessing: Modifies incoming HttpServletRequest objects to ensure consistent encoding
4. Fallback Mechanism: Provides default ASCII encoding when no specific configuration is provided

#### Overview
The architecture emphasizes simplicity and transparency by implementing the standard Filter interface to address character encoding issues that commonly affect web applications. The filter is designed to be easily configured through deployment descriptors and requires minimal setup. By standardizing request encoding early in the request processing lifecycle, the component ensures that downstream components can reliably process text data, particularly important for internationalized applications that handle non-ASCII characters.

  *For additional detailed information, see the summary for src/components/encodingfilter/src.*

### src/components/supplierpo/src

The program implements a complete e-commerce system and showcases integration between various J2EE technologies. This sub-project implements supplier purchase order management along with XML-based data exchange capabilities. This provides these capabilities to the Java Pet Store program:

- Purchase order lifecycle management (pending, approved, denied, completed)
- XML document serialization and deserialization for supplier communications
- Entity relationship management between orders, line items, and shipping information
- Container-managed persistence for order data

#### Identified Design Elements

1. Entity Bean Architecture: Uses container-managed persistence (CMP) with four key entity beans (SupplierOrder, ContactInfo, Address, LineItem) with defined relationships
2. XML Integration: Comprehensive XML handling with DTD validation for data exchange with supplier systems
3. Status-based Workflow: Implements a state machine for purchase orders with well-defined transitions
4. Component Modularity: Clear separation of concerns with dependencies managed through manifest declarations

#### Overview
The architecture emphasizes J2EE best practices through its use of entity beans with container-managed persistence and relationships. The component handles the complete lifecycle of supplier purchase orders from creation through fulfillment. XML document handling provides a standardized interface for supplier communications, while the entity relationships maintain data integrity. The build system ensures proper dependency management with other components like XMLDocuments and ServiceLocator. This modular design allows for maintainability while supporting the core business function of supplier order management within the larger Pet Store application.

  *For additional detailed information, see the summary for src/components/supplierpo/src.*

### src/components/address/src

This sub-project implements the address management functionality along with persistence and data transfer capabilities. This provides these capabilities to the Java Pet Store program:

- Entity bean persistence for customer and shipping addresses
- XML serialization and deserialization of address data
- Standardized address format with support for international addresses
- Component-based architecture for address management

#### Identified Design Elements

1. Container-Managed Persistence (CMP 2.x): The address component uses EJB 2.x CMP for data persistence, abstracting database operations from business logic
2. Value Object Pattern: Address.java serves as a data transfer object between presentation and persistence layers
3. XML Integration: Built-in XML serialization/deserialization with DTD validation ensures data integrity and interoperability
4. Flexible Creation Methods: Multiple ejbCreate methods support different initialization scenarios for address objects
5. Separation of Concerns: Clear distinction between entity bean implementation (AddressEJB), local interfaces (AddressLocal, AddressLocalHome), and data transfer objects (Address)

#### Overview
The architecture follows J2EE best practices with a well-defined entity bean for persistence, clean interfaces for local access, and a value object for data transfer. The build process is managed through Ant, supporting compilation, packaging, and documentation generation. The component handles address validation, storage, and retrieval while maintaining compatibility with XML-based data exchange. This modular design allows the address functionality to be used across multiple aspects of the Pet Store application, including customer profiles and order shipping information.

  *For additional detailed information, see the summary for src/components/address/src.*

### src/components/contactinfo/src

The component implements a robust data management system for customer details through Enterprise JavaBeans (EJB) technology, specifically using Container-Managed Persistence (CMP) entity beans. This sub-project implements customer data storage and retrieval along with address management capabilities. This provides these capabilities to the Java Pet Store program:

- Structured storage of customer contact information (name, email, phone)
- Address management with comprehensive field support
- XML serialization/deserialization for data interchange
- EJB-based persistence with transaction management

#### Identified Design Elements

1. Entity Bean Architecture: Uses two primary CMP entity beans (ContactInfoEJB and AddressEJB) with a one-to-one relationship and cascade delete functionality
2. Value Object Pattern: Implements ContactInfo and Address classes as data transfer objects between presentation and persistence layers
3. XML Data Representation: Provides XML serialization/deserialization with defined DTD schemas for data validation
4. Service Locator Pattern: Utilizes JNDI lookups through centralized constants for component discovery

#### Overview
The architecture emphasizes clean separation between data representation and persistence mechanisms. The component is built with standard J2EE patterns, featuring local EJB interfaces for efficient in-container access. The build system supports modular development with clear dependencies, while the deployment descriptor properly configures transaction attributes and security settings. This component serves as the foundation for customer data management throughout the Pet Store application, providing a consistent and reliable way to store and retrieve customer contact information.

  *For additional detailed information, see the summary for src/components/contactinfo/src.*

### src/components/cart/src

The component utilizes stateful session beans to maintain user-specific cart data throughout a shopping session and provides a comprehensive API for cart manipulation. This sub-project implements the core e-commerce transaction functionality along with integration points to the product catalog system. This provides these capabilities to the Java Pet Store program:

- Stateful shopping cart management across user sessions
- Product item storage with quantity tracking and price calculations
- Integration with the catalog system for product information retrieval
- Locale-aware shopping experience supporting internationalization

#### Identified Design Elements

1. Stateful Session Bean Architecture: Uses EJB stateful session beans to maintain user-specific cart state throughout the shopping experience
2. Model-View Separation: Clear separation between the data model (ShoppingCartModel, CartItem) and business logic implementation (ShoppingCartLocalEJB)
3. Catalog Integration: References the Catalog component to retrieve product details when adding items to the cart
4. Transaction Management: Container-managed transactions for all cart operations ensuring data consistency
5. Serializable Data Models: Cart items and models implement Serializable to support session state persistence

#### Overview
The architecture emphasizes clean separation between the data model and business logic with well-defined interfaces. The ShoppingCartLocalEJB implements the core functionality while CartItem and ShoppingCartModel provide the data structures. The component is built with container-managed transactions for reliability and includes integration with the catalog system for product information. The design supports internationalization through locale settings and provides comprehensive methods for cart manipulation including adding, removing, and updating items, as well as calculating costs and managing the overall cart state.

  *For additional detailed information, see the summary for src/components/cart/src.*

### src/components/creditcard/src

The component implements a container-managed persistence (CMP) Entity Bean architecture to securely store and process credit card information. This sub-project handles credit card validation, storage, and retrieval operations, providing these capabilities to the Java Pet Store program:

- Structured credit card data representation and persistence
- XML-based data serialization and deserialization
- Standardized interfaces for credit card entity creation and retrieval
- Formatted access to credit card components (number, type, expiry date)

#### Identified Design Elements

1. Entity Bean Architecture: Implements a CMP 2.x entity bean model for credit card data persistence with local interfaces
2. XML Data Representation: Provides standardized DTD-based schema for credit card information exchange
3. Transfer Object Pattern: Uses the CreditCard class as a data transfer object between application layers
4. Component-Based Build System: Utilizes Ant for building, packaging, and deploying the credit card component

#### Overview
The architecture follows J2EE best practices with clear separation between interface and implementation. The CreditCardEJB entity bean manages persistence concerns while the CreditCard class handles data representation and XML transformation. Local interfaces (CreditCardLocal and CreditCardLocalHome) provide standardized access points for other application components. The build system supports component-level compilation, packaging, and documentation generation, making the credit card processing functionality a well-encapsulated, reusable module within the larger Java Pet Store application.

  *For additional detailed information, see the summary for src/components/creditcard/src.*

### src/components/processmanager/src

The component tracks order status throughout the fulfillment lifecycle and orchestrates transitions between different states. This sub-project implements entity persistence for order tracking along with a stateless service layer for process management. It provides these capabilities to the Java Pet Store program:

- Order status tracking and persistence
- Workflow state transition management
- Status-based order querying
- Standardized order status definitions

#### Identified Design Elements

1. Entity-Based Status Tracking: Uses a container-managed persistence (CMP) entity bean (ManagerEJB) to store and retrieve order status information
2. Delegate Pattern for Transitions: Implements a flexible transition system using the TransitionDelegate interface and factory pattern
3. XML-Based Communication: Employs XML messages for data exchange during transition operations
4. Standardized Status Constants: Defines consistent order status values (PENDING, APPROVED, DENIED, SHIPPED_PART, COMPLETED) through the OrderStatusNames class

#### Overview
The architecture follows standard J2EE patterns with clear separation between entity and session beans. The ManagerEJB provides persistence for order status data, while ProcessManagerEJB offers the business logic interface for the workflow engine. The transition system uses a delegate pattern with factory instantiation to allow for flexible workflow implementation. Error handling is managed through custom exceptions that support exception chaining. The component is built using Ant with targets for compilation, packaging, and documentation generation, making it maintainable and extensible within the larger Pet Store application.

  *For additional detailed information, see the summary for src/components/processmanager/src.*

### src/components/customer/src

The program implements customer account management and product catalog functionality along with order processing capabilities. This sub-project implements the customer data management layer along with profile preference handling. This provides these capabilities to the Java Pet Store program:

- Customer account persistence and retrieval
- User profile preferences management
- Contact information and address storage
- Credit card data handling and association with accounts
- Entity relationships with cascading delete operations

#### Identified Design Elements

1. Container-Managed Persistence (CMP): Entity beans leverage the J2EE container for automatic persistence of customer data, profiles, accounts, and related information
2. Container-Managed Relationships (CMR): Defined relationships between entities (Customer-Profile, Customer-Account, Account-ContactInfo, etc.) with appropriate cascade operations
3. Local Interface Architecture: Uses EJBLocalObject and EJBLocalHome interfaces to optimize performance by avoiding remote call overhead
4. Data Transfer Objects: Implements serializable classes like ProfileInfo to transfer data between EJB layer and client applications

#### Overview
The architecture follows J2EE best practices with clear separation between entity definitions and their implementations. The customer management component is structured around six key entity beans (CustomerEJB, ProfileEJB, AccountEJB, ContactInfoEJB, AddressEJB, and CreditCardEJB) that work together to provide comprehensive customer data management. Each entity has well-defined relationships, allowing for efficient data access patterns and maintaining data integrity through appropriate cascade operations. The build process is managed through Ant, with clear dependencies and component packaging defined for both server and client deployments.

  *For additional detailed information, see the summary for src/components/customer/src.*

### src/components/uidgen/src

The Unique ID Generator Source Root sub-project implements a critical infrastructure component for generating sequential, unique identifiers throughout the application. This provides these capabilities to the Java Pet Store program:

- Consistent ID generation across distributed application components
- Prefix-based identifier creation for different entity types (orders, customers, etc.)
- Persistent counter storage using container-managed persistence (CMP)
- Thread-safe increment operations for concurrent application usage

#### Identified Design Elements

1. Two-Tier EJB Architecture: Separates the unique ID generation logic (UniqueIdGeneratorEJB) from the persistent counter storage (CounterEJB)
2. Local EJB Interfaces: Uses local interfaces (CounterLocal, UniqueIdGeneratorLocal) to optimize performance for in-container calls
3. Container-Managed Persistence: Leverages J2EE container services for entity persistence rather than implementing custom data access
4. Stateless Service Layer: Implements the generator as a stateless session bean for scalability and efficient resource usage

#### Overview
The architecture follows standard J2EE patterns with clear separation between the entity layer (CounterEJB) and service layer (UniqueIdGeneratorEJB). The CounterEJB maintains persistent named counters that increment with each request, while the UniqueIdGeneratorEJB provides a simplified client-facing API that concatenates prefixes with counter values. The build process creates both implementation and client JAR files, supporting clean dependency management. This design ensures unique identifiers can be reliably generated across the distributed application components while maintaining transactional integrity.

  *For additional detailed information, see the summary for src/components/uidgen/src.*

## Business Functions

### Documentation
- `signon/src/build.xml` : Ant build script for the SignOn component of Java Pet Store, managing compilation and packaging of EJB modules.
- `signon/src/ejb-jar.xml` : EJB deployment descriptor defining user authentication components for Java Pet Store application.
- `lineitem/src/build.xml` : Ant build script for the LineItem component in Java Pet Store application.
- `lineitem/src/ejb-jar.xml` : EJB deployment descriptor for LineItem entity bean in Java Pet Store application.
- `util/tracer/src/build.xml` : Ant build script for the tracer component in Java Pet Store.
- `purchaseorder/src/build.xml` : Ant build script for the PurchaseOrder component in Java Pet Store, managing compilation and packaging of EJB modules.
- `purchaseorder/src/ejb-jar-manifest.mf` : Manifest file defining class path dependencies for the purchase order EJB component.
- `purchaseorder/src/ejb-jar.xml` : EJB deployment descriptor defining purchase order component entities and relationships for Java Pet Store.
- `servicelocator/src/build.xml` : Ant build script for the ServiceLocator component in Java Pet Store.
- `catalog/src/build.xml` : Ant build file for the catalog component of Java Pet Store application.
- `catalog/src/ejb-jar-manifest.mf` : Manifest file defining class path dependencies for the catalog EJB component.
- `catalog/src/ejb-jar.xml` : EJB deployment descriptor for the catalog component of Java Pet Store.
- `mailer/src/build.xml` : Ant build script for compiling and packaging the Mailer component of Java Pet Store.
- `mailer/src/ejb-jar-manifest.mf` : EJB JAR manifest file for the mailer component specifying XML documents dependency.
- `mailer/src/ejb-jar.xml` : EJB deployment descriptor for the Mailer component that processes mail messages via JMS queue.
- `xmldocuments/src/build.xml` : Ant build script for the xmldocuments component of Java Pet Store.
- `encodingfilter/src/build.xml` : Ant build script for compiling the encoding filter component in Java Pet Store.
- `supplierpo/src/build.xml` : Ant build script for the SupplierPO component that manages purchase orders in the Java Pet Store application.
- `supplierpo/src/ejb-jar-manifest.mf` : Manifest file for the supplierpo EJB JAR component declaring dependencies on XML documents and service locator libraries.
- `supplierpo/src/ejb-jar.xml` : EJB deployment descriptor defining supplier purchase order components and relationships.
- `address/src/build.xml` : Ant build script for the Address component in Java Pet Store application.
- `address/src/ejb-jar.xml` : EJB deployment descriptor defining the AddressEJB entity bean with container-managed persistence for the Java Pet Store application.
- `contactinfo/src/build.xml` : Ant build script for the ContactInfo component in Java Pet Store, managing compilation and packaging of EJB modules.
- `contactinfo/src/ejb-jar.xml` : EJB deployment descriptor defining ContactInfo and Address entity beans with their relationships and transaction attributes.
- `cart/src/build.xml` : Ant build script for the shopping cart component of Java Pet Store application.
- `cart/src/ejb-jar-manifest.mf` : EJB JAR manifest file defining class dependencies for the cart component.
- `cart/src/ejb-jar.xml` : EJB deployment descriptor for the ShoppingCart component in Java Pet Store application.
- `creditcard/src/build.xml` : Ant build script for the CreditCard component of Java Pet Store.
- `creditcard/src/ejb-jar.xml` : EJB deployment descriptor for the CreditCard component defining entity bean structure and transaction attributes.
- `processmanager/src/build.xml` : Ant build script for the ProcessManager component in Java Pet Store application.
- `processmanager/src/ejb-jar.xml` : EJB deployment descriptor defining the ProcessManager component with entity and session beans for order status management.
- `customer/src/build.xml` : Ant build script for the customer component of Java Pet Store application.
- `customer/src/ejb-jar-manifest.mf` : EJB JAR manifest file specifying dependencies for the customer component.
- `customer/src/ejb-jar.xml` : EJB deployment descriptor defining customer component entities and their relationships for Java Pet Store.
- `uidgen/src/build.xml` : Ant build script for the UniqueIdGenerator component in Java Pet Store.
- `uidgen/src/ejb-jar.xml` : EJB deployment descriptor defining UniqueIdGenerator and Counter EJBs for generating unique identifiers in the Java Pet Store application.

### Authentication and Authorization
- `signon/src/com/sun/j2ee/blueprints/signon/web/SignOnDAO.java` : Data access object for sign-on functionality that parses XML configuration for protected web resources.
- `signon/src/com/sun/j2ee/blueprints/signon/web/ProtectedResource.java` : Defines a serializable class representing a protected resource with access control information.
- `signon/src/com/sun/j2ee/blueprints/signon/web/CreateUserServlet.java` : Servlet that handles user registration in the Java Pet Store application.
- `signon/src/com/sun/j2ee/blueprints/signon/web/SignOnFilter.java` : Authentication filter that manages user sign-on process for protected resources in the Java Pet Store application.
- `signon/src/com/sun/j2ee/blueprints/signon/user/ejb/UserEJB.java` : Entity bean for user authentication in the signon component of Java Pet Store.
- `signon/src/com/sun/j2ee/blueprints/signon/user/ejb/UserLocalHome.java` : Local home interface for the User entity EJB in the signon component.
- `signon/src/com/sun/j2ee/blueprints/signon/user/ejb/UserLocal.java` : Local interface for the User EJB that defines methods for user authentication and password management.
- `signon/src/com/sun/j2ee/blueprints/signon/ejb/SignOnEJB.java` : Authentication component that verifies user credentials for the Java Pet Store application.
- `signon/src/com/sun/j2ee/blueprints/signon/ejb/SignOnLocal.java` : Local EJB interface for sign-on authentication and user creation functionality.
- `signon/src/com/sun/j2ee/blueprints/signon/ejb/SignOnLocalHome.java` : Defines the local home interface for SignOn Entity EJB in the authentication component.

### Order Management
- `lineitem/src/com/sun/j2ee/blueprints/lineitem/rsrc/schemas/LineItem.dtd` : DTD schema definition for LineItem elements in the Java Pet Store application.
- `lineitem/src/com/sun/j2ee/blueprints/lineitem/ejb/LineItemEJB.java` : Entity bean implementation for line items in an order, managing product details and quantities.
- `lineitem/src/com/sun/j2ee/blueprints/lineitem/ejb/LineItemLocal.java` : Local interface for LineItem EJB defining methods to access order line item data in the Java Pet Store application.
- `lineitem/src/com/sun/j2ee/blueprints/lineitem/ejb/LineItem.java` : Represents a line item in an order with product details and quantity information.
- `lineitem/src/com/sun/j2ee/blueprints/lineitem/ejb/LineItemLocalHome.java` : Local home interface for LineItem EJB defining creation and finder methods.
- `purchaseorder/src/com/sun/j2ee/blueprints/purchaseorder/rsrc/schemas/PurchaseOrder.dtd` : DTD schema definition for purchase order data structure in the Java Pet Store application.
- `purchaseorder/src/com/sun/j2ee/blueprints/purchaseorder/ejb/PurchaseOrder.java` : Represents a purchase order entity with XML serialization/deserialization capabilities for the Java Pet Store application.
- `purchaseorder/src/com/sun/j2ee/blueprints/purchaseorder/ejb/PurchaseOrderHelper.java` : Helper class for processing purchase order invoices and tracking order fulfillment status.
- `purchaseorder/src/com/sun/j2ee/blueprints/purchaseorder/ejb/JNDINames.java` : Defines JNDI names for EJB components in the purchase order system.
- `purchaseorder/src/com/sun/j2ee/blueprints/purchaseorder/ejb/PurchaseOrderEJB.java` : Entity bean for managing purchase orders with relationships to line items, contact info, and credit card data.
- `purchaseorder/src/com/sun/j2ee/blueprints/purchaseorder/ejb/PurchaseOrderLocal.java` : Local interface for PurchaseOrderEJB defining methods to access purchase order data in the Java Pet Store application.
- `purchaseorder/src/com/sun/j2ee/blueprints/purchaseorder/ejb/PurchaseOrderLocalHome.java` : Local home interface for PurchaseOrder EJB defining creation and finder methods.
- `xmldocuments/src/rsrc/schemas/PurchaseOrder.dtd` : DTD schema defining the structure of purchase order XML documents in the Java Pet Store application.
- `xmldocuments/src/rsrc/schemas/OrderApproval.dtd` : DTD schema defining XML structure for order approval in the Java Pet Store application.
- `xmldocuments/src/com/sun/j2ee/blueprints/xmldocuments/ChangedOrder.java` : Represents a changed order with ID and status, providing XML serialization and deserialization capabilities.
- `xmldocuments/src/com/sun/j2ee/blueprints/xmldocuments/OrderApproval.java` : Handles order approval data representation and XML serialization/deserialization for the Java Pet Store application.

### Utility and Infrastructure
- `util/tracer/src/com/sun/j2ee/blueprints/util/tracer/Debug.java` : Utility class providing debug logging functionality with configurable output.
- `servicelocator/src/com/sun/j2ee/blueprints/servicelocator/web/ServiceLocator.java` : Implements the Service Locator pattern for J2EE resources, providing cached access to EJB homes, JMS resources, and other services.
- `servicelocator/src/com/sun/j2ee/blueprints/servicelocator/ServiceLocatorException.java` : Custom exception class for the service locator pattern that can wrap lower-level exceptions.
- `servicelocator/src/com/sun/j2ee/blueprints/servicelocator/ejb/ServiceLocator.java` : Service Locator implementation for accessing J2EE resources like EJB homes, JMS destinations, and data sources.
- `encodingfilter/src/com/sun/j2ee/blueprints/encodingfilter/web/EncodingFilter.java` : A servlet filter that sets character encoding for HTTP requests.

### Catalog Management
- `catalog/src/com/sun/j2ee/blueprints/catalog/util/DatabaseNames.java` : Defines database table names and provides locale-specific table name resolution for the catalog component.
- `catalog/src/com/sun/j2ee/blueprints/catalog/util/JNDINames.java` : Defines JNDI naming constants for the catalog component of Java Pet Store.
- `catalog/src/com/sun/j2ee/blueprints/catalog/dao/CatalogDAO.java` : Interface for catalog data access operations in the Java Pet Store application.
- `catalog/src/com/sun/j2ee/blueprints/catalog/dao/GenericCatalogDAO.java` : Data access object implementation for catalog operations in Java Pet Store, handling database queries for categories, products, and items.
- `catalog/src/com/sun/j2ee/blueprints/catalog/dao/CatalogDAOFactory.java` : Factory class that creates CatalogDAO instances based on deployment configuration.
- `catalog/src/com/sun/j2ee/blueprints/catalog/dao/CloudscapeCatalogDAO.java` : Database access implementation for catalog data using Cloudscape database in the Java Pet Store application.
- `catalog/src/com/sun/j2ee/blueprints/catalog/exceptions/CatalogDAOSysException.java` : Custom runtime exception for catalog component data access errors
- `catalog/src/com/sun/j2ee/blueprints/catalog/model/Page.java` : Represents a paginated collection of results for iterating through data page by page.
- `catalog/src/com/sun/j2ee/blueprints/catalog/model/Product.java` : Defines a Product class representing different types of pets within a category in the Java Pet Store application.
- `catalog/src/com/sun/j2ee/blueprints/catalog/model/Category.java` : Represents a category of pets in the Java Pet Store application.
- `catalog/src/com/sun/j2ee/blueprints/catalog/model/Item.java` : Represents a catalog item with product details and pricing information.
- `catalog/src/com/sun/j2ee/blueprints/catalog/client/CatalogHelper.java` : Helper class that provides access to catalog data through either EJB or direct DAO access using the Fast Lane Reader pattern.
- `catalog/src/com/sun/j2ee/blueprints/catalog/client/CatalogException.java` : Custom exception class for catalog-related errors in the Java Pet Store application.
- `catalog/src/com/sun/j2ee/blueprints/catalog/ejb/CatalogEJB.java` : Stateless session EJB providing catalog data access for the Java Pet Store application.
- `catalog/src/com/sun/j2ee/blueprints/catalog/ejb/CatalogLocalHome.java` : Defines the local home interface for the Catalog EJB component in the Pet Store application.
- `catalog/src/com/sun/j2ee/blueprints/catalog/ejb/CatalogLocal.java` : Local interface for the catalog EJB providing catalog functionality for the Java Pet Store application.

### Email Notifications
- `mailer/src/com/sun/j2ee/blueprints/mailer/util/JNDINames.java` : Defines JNDI name constants for mail resources in the Java Pet Store mailer component.
- `mailer/src/com/sun/j2ee/blueprints/mailer/exceptions/MailerAppException.java` : Custom exception class for the mailer component to handle mail sending failures.
- `mailer/src/com/sun/j2ee/blueprints/mailer/rsrc/schemas/Mail.dtd` : XML DTD schema definition for email messages in the mailer component
- `mailer/src/com/sun/j2ee/blueprints/mailer/ejb/ByteArrayDataSource.java` : A utility class that implements a DataSource for email attachments from byte arrays or strings.
- `mailer/src/com/sun/j2ee/blueprints/mailer/ejb/MailerMDB.java` : Message-driven bean that processes email messages from a JMS queue and sends them using a mail service.
- `mailer/src/com/sun/j2ee/blueprints/mailer/ejb/Mail.java` : Represents an email message with XML serialization/deserialization capabilities for the mailer component.
- `mailer/src/com/sun/j2ee/blueprints/mailer/ejb/MailHelper.java` : Helper class for creating and sending emails using J2EE mail services.
- `xmldocuments/src/rsrc/schemas/Mail.dtd` : DTD schema defining the structure of Mail XML documents with Address, Subject, and Content elements.

### XML Document Processing
- `xmldocuments/src/rsrc/schemas/EntityCatalog.properties` : Entity catalog mapping DTD identifiers to their physical locations in the Java Pet Store application.
- `xmldocuments/src/rsrc/schemas/LineItem.dtd` : DTD schema defining the structure of LineItem XML documents in the Java Pet Store application.
- `xmldocuments/src/rsrc/schemas/SupplierOrder.dtd` : DTD schema defining the structure of supplier order XML documents in the Java Pet Store application.
- `xmldocuments/src/rsrc/schemas/Invoice.dtd` : DTD schema defining the structure of Invoice XML documents for the Java Pet Store application.
- `xmldocuments/src/com/sun/j2ee/blueprints/xmldocuments/XMLDocumentEditor.java` : Defines an interface for XML document manipulation with validation and entity catalog support.
- `xmldocuments/src/com/sun/j2ee/blueprints/xmldocuments/XMLDocumentEditorFactory.java` : Factory class for creating XML document editors based on schema URIs.
- `xmldocuments/src/com/sun/j2ee/blueprints/xmldocuments/XMLDocumentException.java` : Custom exception class for XML document processing that can wrap lower-level exceptions.
- `xmldocuments/src/com/sun/j2ee/blueprints/xmldocuments/CustomEntityResolver.java` : Custom XML entity resolver that maps entity URIs to local resources using a properties catalog
- `xmldocuments/src/com/sun/j2ee/blueprints/xmldocuments/rsrc/EntityCatalog.properties` : Entity catalog properties file mapping XML DTDs and schemas to their physical locations.
- `xmldocuments/src/com/sun/j2ee/blueprints/xmldocuments/rsrc/schemas/TPALineItem.dtd` : XML DTD schema defining the structure of TPA line items for e-commerce transactions.
- `xmldocuments/src/com/sun/j2ee/blueprints/xmldocuments/rsrc/schemas/TPASupplierOrder.dtd` : DTD schema defining XML structure for supplier orders in the TPA system.
- `xmldocuments/src/com/sun/j2ee/blueprints/xmldocuments/rsrc/schemas/TPAInvoice.dtd` : DTD schema defining the structure of TPA invoices for XML document validation in the Pet Store application.
- `xmldocuments/src/com/sun/j2ee/blueprints/xmldocuments/tpa/TPAInvoiceXDE.java` : XML document editor for TPA invoices with methods to create, manipulate and serialize invoice data.
- `xmldocuments/src/com/sun/j2ee/blueprints/xmldocuments/tpa/TPALineItemUtils.java` : Utility class for creating XML line item elements in TPA documents
- `xmldocuments/src/com/sun/j2ee/blueprints/xmldocuments/tpa/TPASupplierOrderXDE.java` : XML document editor for TPA supplier orders in the Java Pet Store application.
- `xmldocuments/src/com/sun/j2ee/blueprints/xmldocuments/XMLDocumentUtils.java` : Utility class providing XML document manipulation methods for parsing, validation, transformation, and DOM operations.

### Asynchronous Messaging
- `asyncsender/src/com/sun/j2ee/blueprints/asyncsender/util/JNDINames.java` : Defines JNDI name constants for AsyncSender component resources.
- `asyncsender/src/com/sun/j2ee/blueprints/asyncsender/ejb/AsyncSenderEJB.java` : Stateless session bean that sends asynchronous messages to a JMS queue.
- `asyncsender/src/com/sun/j2ee/blueprints/asyncsender/ejb/AsyncSender.java` : Defines the AsyncSender EJB local interface for asynchronous message sending functionality.
- `asyncsender/src/com/sun/j2ee/blueprints/asyncsender/ejb/AsyncSenderLocalHome.java` : Defines the local home interface for the AsyncSender EJB component in the Java Pet Store application.

### Supplier Management
- `supplierpo/src/com/sun/j2ee/blueprints/supplierpo/rsrc/schemas/SupplierOrder.dtd` : DTD schema defining the structure of supplier purchase orders in XML format.
- `supplierpo/src/com/sun/j2ee/blueprints/supplierpo/ejb/SupplierOrderEJB.java` : Entity bean for managing supplier purchase orders in the Java Pet Store application.
- `supplierpo/src/com/sun/j2ee/blueprints/supplierpo/ejb/SupplierOrderLocal.java` : Local interface for SupplierOrder EJB defining methods to manage purchase orders to suppliers.
- `supplierpo/src/com/sun/j2ee/blueprints/supplierpo/ejb/OrderStatusNames.java` : Defines constants for supplier purchase order status values in the Java Pet Store application.
- `supplierpo/src/com/sun/j2ee/blueprints/supplierpo/ejb/JNDINames.java` : Defines JNDI names for EJB components in the supplier purchase order system.
- `supplierpo/src/com/sun/j2ee/blueprints/supplierpo/ejb/SupplierOrder.java` : SupplierOrder class represents purchase orders sent to suppliers with order details, shipping information, and line items.
- `supplierpo/src/com/sun/j2ee/blueprints/supplierpo/ejb/SupplierOrderLocalHome.java` : Local home interface for the SupplierOrder EJB defining creation and finder methods.

### Address Management
- `address/src/com/sun/j2ee/blueprints/address/rsrc/schemas/Address.dtd` : XML DTD schema definition for Address structure in the Java Pet Store application.
- `address/src/com/sun/j2ee/blueprints/address/ejb/AddressEJB.java` : Entity bean implementation for managing address information in the Java Pet Store application.
- `address/src/com/sun/j2ee/blueprints/address/ejb/AddressLocalHome.java` : EJB local home interface for Address entity bean in the Java Pet Store application.
- `address/src/com/sun/j2ee/blueprints/address/ejb/Address.java` : Represents a physical address with street, city, state, country and zipcode information.
- `address/src/com/sun/j2ee/blueprints/address/ejb/AddressLocal.java` : Local interface for the Address EJB component defining address data access methods.

### Contact Information Management
- `contactinfo/src/com/sun/j2ee/blueprints/contactinfo/rsrc/schemas/ContactInfo.dtd` : DTD schema definition for ContactInfo element structure in the Java Pet Store application.
- `contactinfo/src/com/sun/j2ee/blueprints/contactinfo/ejb/ContactInfoLocal.java` : Local EJB interface defining contact information operations for the Java Pet Store application.
- `contactinfo/src/com/sun/j2ee/blueprints/contactinfo/ejb/ContactInfoLocalHome.java` : Local home interface for ContactInfo EJB defining creation and finder methods.
- `contactinfo/src/com/sun/j2ee/blueprints/contactinfo/ejb/JNDINames.java` : Defines JNDI name constants for the ContactInfo component in the Java Pet Store application.
- `contactinfo/src/com/sun/j2ee/blueprints/contactinfo/ejb/ContactInfo.java` : Data model class representing contact information with XML serialization capabilities.
- `contactinfo/src/com/sun/j2ee/blueprints/contactinfo/ejb/ContactInfoEJB.java` : Entity bean for managing contact information in the Java Pet Store application

### Shopping Cart
- `cart/src/com/sun/j2ee/blueprints/cart/model/ShoppingCartModel.java` : Implements a model class for shopping cart data in the Java Pet Store application.
- `cart/src/com/sun/j2ee/blueprints/cart/model/CartItem.java` : Represents a single item in a shopping cart with product details and quantity.
- `cart/src/com/sun/j2ee/blueprints/cart/ejb/ShoppingCartLocalEJB.java` : Implements a shopping cart as a stateful session EJB for the Java Pet Store application.
- `cart/src/com/sun/j2ee/blueprints/cart/ejb/ShoppingCartLocal.java` : Local EJB interface for shopping cart operations in the Java Pet Store application.
- `cart/src/com/sun/j2ee/blueprints/cart/ejb/ShoppingCartLocalHome.java` : Local home interface for the ShoppingCart EJB defining creation methods.

### Payment Processing
- `creditcard/src/com/sun/j2ee/blueprints/creditcard/rsrc/schemas/CreditCard.dtd` : DTD schema definition for credit card data structure in the Java Pet Store application.
- `creditcard/src/com/sun/j2ee/blueprints/creditcard/ejb/CreditCardEJB.java` : Entity bean implementation for credit card data storage and retrieval in the Java Pet Store application.
- `creditcard/src/com/sun/j2ee/blueprints/creditcard/ejb/CreditCard.java` : Implements a credit card data model with XML serialization capabilities for the Java Pet Store application.
- `creditcard/src/com/sun/j2ee/blueprints/creditcard/ejb/CreditCardLocalHome.java` : Defines the local home interface for the CreditCard EJB component in the Java Pet Store application.
- `creditcard/src/com/sun/j2ee/blueprints/creditcard/ejb/CreditCardLocal.java` : Local interface for the CreditCard EJB defining credit card data access methods.

### Process Management
- `processmanager/src/com/sun/j2ee/blueprints/processmanager/manager/ejb/ManagerEJB.java` : Entity bean for managing order processing status in the Java Pet Store application.
- `processmanager/src/com/sun/j2ee/blueprints/processmanager/manager/ejb/ManagerLocalHome.java` : Local home interface for the ProcessManager Entity EJB that defines creation and finder methods.
- `processmanager/src/com/sun/j2ee/blueprints/processmanager/manager/ejb/ManagerLocal.java` : Local interface for Manager EJB defining order status operations in the process manager component.
- `processmanager/src/com/sun/j2ee/blueprints/processmanager/transitions/TransitionInfo.java` : Encapsulates parameters passed to transition delegates in the process manager component.
- `processmanager/src/com/sun/j2ee/blueprints/processmanager/transitions/TransitionException.java` : Custom exception class for handling transition errors in the process manager component.
- `processmanager/src/com/sun/j2ee/blueprints/processmanager/transitions/TransitionDelegate.java` : Defines the interface for transition delegates in the process manager component.
- `processmanager/src/com/sun/j2ee/blueprints/processmanager/transitions/TransitionDelegateFactory.java` : Factory class for creating TransitionDelegate instances in the process manager component.
- `processmanager/src/com/sun/j2ee/blueprints/processmanager/ejb/ProcessManagerEJB.java` : EJB implementation for managing order processing workflow in the Java Pet Store application.
- `processmanager/src/com/sun/j2ee/blueprints/processmanager/ejb/ProcessManagerLocal.java` : Local EJB interface for managing order workflow processes in Java Pet Store.
- `processmanager/src/com/sun/j2ee/blueprints/processmanager/ejb/OrderStatusNames.java` : Defines constants for order status names used in the Java Pet Store order processing system.
- `processmanager/src/com/sun/j2ee/blueprints/processmanager/ejb/ProcessManagerLocalHome.java` : Local home interface for the ProcessManager EJB component in the Java Pet Store application.

### Customer Management
- `customer/src/com/sun/j2ee/blueprints/customer/profile/ejb/ProfileInfo.java` : Serializable data class for storing user profile preferences in the Java Pet Store application.
- `customer/src/com/sun/j2ee/blueprints/customer/profile/ejb/ProfileEJB.java` : Entity bean for managing user profile preferences in the Java Pet Store application.
- `customer/src/com/sun/j2ee/blueprints/customer/profile/ejb/ProfileLocal.java` : Local interface for the Profile EJB defining user preferences in the Java Pet Store application.
- `customer/src/com/sun/j2ee/blueprints/customer/profile/ejb/ProfileLocalHome.java` : Local home interface for Profile EJB defining creation and finder methods with default preferences.
- `customer/src/com/sun/j2ee/blueprints/customer/account/ejb/AccountLocalHome.java` : Local home interface for Account EJB defining creation and finder methods in the customer component.
- `customer/src/com/sun/j2ee/blueprints/customer/account/ejb/AccountLocal.java` : Local EJB interface for customer account management in Java Pet Store.
- `customer/src/com/sun/j2ee/blueprints/customer/account/ejb/AccountEJB.java` : Entity bean implementation for managing customer account data in the Java Pet Store application.
- `customer/src/com/sun/j2ee/blueprints/customer/ejb/CustomerEJB.java` : Entity bean implementation for customer management in the Java Pet Store application.
- `customer/src/com/sun/j2ee/blueprints/customer/ejb/CustomerLocal.java` : Local EJB interface defining customer access methods for the Java Pet Store application.
- `customer/src/com/sun/j2ee/blueprints/customer/ejb/CustomerLocalHome.java` : Local home interface for Customer EJB defining creation and finder methods.

### ID Generation
- `uidgen/src/com/sun/j2ee/blueprints/uidgen/counter/ejb/CounterEJB.java` : Entity bean implementation for generating unique identifiers with a counter mechanism.
- `uidgen/src/com/sun/j2ee/blueprints/uidgen/counter/ejb/CounterLocal.java` : Local EJB interface for a counter component that generates unique sequential values.
- `uidgen/src/com/sun/j2ee/blueprints/uidgen/counter/ejb/CounterLocalHome.java` : Local EJB home interface for Counter component in unique ID generation system.
- `uidgen/src/com/sun/j2ee/blueprints/uidgen/ejb/UniqueIdGeneratorLocal.java` : Local interface for a unique ID generator EJB component in the Java Pet Store application.
- `uidgen/src/com/sun/j2ee/blueprints/uidgen/ejb/UniqueIdGeneratorLocalHome.java` : Local home interface for the UniqueIdGenerator EJB component.
- `uidgen/src/com/sun/j2ee/blueprints/uidgen/ejb/UniqueIdGeneratorEJB.java` : Stateless session EJB that generates unique identifiers with customizable prefixes.

### Invoice Processing
- `xmldocuments/src/rsrc/schemas/Invoice.dtd` : DTD schema defining the structure of Invoice XML documents for the Java Pet Store application.
- `xmldocuments/src/com/sun/j2ee/blueprints/xmldocuments/rsrc/schemas/TPAInvoice.dtd` : DTD schema defining the structure of TPA invoices for XML document validation in the Pet Store application.
- `xmldocuments/src/com/sun/j2ee/blueprints/xmldocuments/tpa/TPAInvoiceXDE.java` : XML document editor for TPA invoices with methods to create, manipulate and serialize invoice data.

## Files
### address/src/build.xml

This build.xml file defines the build process for the Address component in the Java Pet Store application. It sets up properties for directories, dependencies, and build targets. Key functionality includes compiling Java classes, creating EJB JAR files, generating documentation, and managing dependencies with other components like XMLDocuments. Important targets include init, compile, ejbjar, ejbclientjar, clean, components, docs, and core. The script manages paths for source code, compiled classes, and documentation while handling EJB packaging requirements.

 **Code Landmarks**
- `Line 102`: Creates separate client JAR by excluding implementation classes, following EJB best practices for deployment
- `Line 65`: Establishes dependency on XMLDocuments component for PO/Invoice classes, showing component architecture
- `Line 76`: Configures classpath to include J2EE libraries and dependent components for proper compilation
### address/src/com/sun/j2ee/blueprints/address/ejb/Address.java

Address implements a class for storing and managing physical address information in the Java Pet Store application. It contains fields for street names, city, state, zip code, and country, with corresponding getter and setter methods. The class provides XML serialization and deserialization capabilities through toDOM() and fromDOM() methods, allowing addresses to be converted to and from XML DOM nodes. Constants define XML element names and DTD information for validation. The class supports two-line street addresses and includes a toString() method for debugging.

 **Code Landmarks**
- `Line 127`: XML deserialization handles optional second street name element gracefully
- `Line 118`: XML serialization conditionally includes second street name only when it exists
### address/src/com/sun/j2ee/blueprints/address/ejb/AddressEJB.java

AddressEJB implements an entity bean that manages address information in the Java Pet Store application. It provides container-managed persistence (CMP) for address fields including street names, city, state, zip code, and country. The class defines abstract getter and setter methods for these fields, multiple ejbCreate methods for different initialization scenarios, and a getData method that returns an Address value object. It also implements standard EntityBean lifecycle methods required by the EJB specification. The bean serves as the persistence layer for customer and shipping addresses in the e-commerce application.

 **Code Landmarks**
- `Line 77`: Uses Container-Managed Persistence (CMP) with abstract getter/setter methods rather than managing database operations directly
- `Line 95`: Provides an overloaded ejbCreate method that accepts an Address value object for convenient initialization
- `Line 106`: getData method implements the Value Object pattern to transfer address data between tiers
### address/src/com/sun/j2ee/blueprints/address/ejb/AddressLocal.java

AddressLocal interface extends EJB's EJBLocalObject to define the local interface for the Address Enterprise JavaBean component. It provides getter and setter methods for address attributes including street names, city, state, zip code, and country. The interface also includes a getData() method that returns an Address object containing all the address information. This interface serves as the contract for local clients to interact with the Address EJB within the Java Pet Store application's address component.

 **Code Landmarks**
- `Line 39`: Uses EJBLocalObject interface for local container-managed persistence access, avoiding remote call overhead
### address/src/com/sun/j2ee/blueprints/address/ejb/AddressLocalHome.java

AddressLocalHome interface defines the local home interface for the Address entity bean in the Java Pet Store application. It extends javax.ejb.EJBLocalHome and provides methods for creating and finding Address entity beans. Key functionality includes three create methods: one accepting individual address components (streetName1, streetName2, city, state, zipCode, country), another accepting an Address object, and a no-argument create method. It also includes a findByPrimaryKey method to locate existing Address entities by their primary key.

 **Code Landmarks**
- `Line 44`: This interface follows the EJB design pattern where home interfaces provide factory methods for entity beans
### address/src/com/sun/j2ee/blueprints/address/rsrc/schemas/Address.dtd

This DTD file defines the XML schema for the Address structure used in the Java Pet Store application. It establishes the structure and elements required for representing postal addresses. The schema defines an Address element containing child elements for address components: StreetName (which can appear up to twice), City, State, ZipCode, and an optional Country element. Each child element is defined to contain parsed character data (#PCDATA). This schema provides standardization for address data representation throughout the application.

 **Code Landmarks**
- `Line 41`: The DTD allows for two StreetName elements, supporting multi-line street addresses
### address/src/ejb-jar.xml

This ejb-jar.xml file defines the deployment descriptor for the Address component in the Java Pet Store application. It configures an entity bean named AddressEJB with container-managed persistence (CMP 2.x) that stores address information. The file declares six CMP fields: zipCode, streetName1, streetName2, city, state, and country. It specifies local interfaces (AddressLocalHome and AddressLocal) and sets security permissions allowing unchecked access to all bean methods. The assembly descriptor configures transaction attributes, requiring transactions for all data access methods. The bean supports multiple create methods, including one that accepts an Address object and another that takes individual address components.

 **Code Landmarks**
- `Line 58`: Uses container-managed persistence (CMP) 2.x, indicating a J2EE 1.3+ compliant entity bean implementation
- `Line 59`: Abstract schema name 'Address' is used for EJB QL queries in a CMP entity bean
- `Line 91`: Security configuration uses unchecked permissions, allowing any authenticated user to access all methods
- `Line 179`: Multiple create methods with different signatures provide flexible ways to instantiate address objects
### asyncsender/src/com/sun/j2ee/blueprints/asyncsender/ejb/AsyncSender.java

AsyncSender interface defines a local EJB interface that provides asynchronous messaging capabilities within the Java Pet Store application. It extends EJBLocalObject and declares a single method sendAMessage() that takes a String message parameter. This interface serves as the contract for EJB components that need to send messages asynchronously, allowing for decoupled communication between system components. The implementation of this interface would handle the actual message delivery mechanism, likely using JMS or another messaging system.

 **Code Landmarks**
- `Line 41`: The interface extends EJBLocalObject rather than EJBObject, indicating it's designed for local (same-JVM) access rather than remote calls
### asyncsender/src/com/sun/j2ee/blueprints/asyncsender/ejb/AsyncSenderEJB.java

AsyncSenderEJB implements a stateless session bean that facilitates asynchronous message sending via JMS. It initializes queue connections during ejbCreate() using ServiceLocator to obtain the queue and connection factory from JNDI. The primary functionality is in sendAMessage() which creates a JMS connection, session, and sender to transmit text messages to the configured queue. The class handles proper resource cleanup and implements standard EJB lifecycle methods (most are empty as appropriate for stateless beans). Key components include the sendAMessage() method, QueueConnectionFactory, Queue, and SessionContext objects.

 **Code Landmarks**
- `Line 56`: Uses ServiceLocator pattern to obtain JMS resources, abstracting JNDI lookup complexity
- `Line 65`: Implements proper JMS resource management with try-finally block to ensure connection cleanup
- `Line 77`: Wraps exceptions in EJBException to maintain EJB contract while preserving original error
### asyncsender/src/com/sun/j2ee/blueprints/asyncsender/ejb/AsyncSenderLocalHome.java

AsyncSenderLocalHome interface defines the local home interface for the AsyncSender Enterprise JavaBean component in the Java Pet Store application. This interface extends EJBLocalHome and provides a single create() method that returns an AsyncSender local interface. The create method throws CreateException if the bean creation fails. This interface is part of the asynchronous messaging infrastructure that allows components to send messages without waiting for responses, facilitating loose coupling between system components.

 **Code Landmarks**
- `Line 42`: The interface extends EJBLocalHome, indicating it's designed for local (same JVM) access rather than remote clients
### asyncsender/src/com/sun/j2ee/blueprints/asyncsender/util/JNDINames.java

JNDINames is a utility class that centralizes JNDI name constants used throughout the AsyncSender component. It defines string constants for EJB home objects and JMS queue resources that are referenced in the application. The class contains three important constants: ASYNCSENDER_LOCAL_EJB_HOME for the AsyncSender EJB, QUEUE_CONNECTION_FACTORY for JMS connection factory, and ASYNC_SENDER_QUEUE for the AsyncSender message queue. These constants ensure consistent JNDI naming across the application and must match the names specified in deployment descriptors.

 **Code Landmarks**
- `Line 43`: Class serves as a central repository for JNDI names, enforcing consistency between code and deployment descriptors
### cart/src/build.xml

This build script manages the compilation and packaging of the shopping cart component for the Java Pet Store application. It defines targets for compiling Java classes, creating EJB JAR files, generating documentation, and cleaning build artifacts. The script sets up dependencies on other components like catalog and tracer utilities, configures classpaths, and defines build directories. Key targets include 'compile', 'ejbjar', 'ejbclientjar', 'clean', 'components', 'docs', and 'core'. Important properties include cart.home, cart.src, cart.build, cart.classpath, cart.ejbjar, and cart.ejbclientjar.

 **Code Landmarks**
- `Line 96`: Creates separate client JAR that excludes implementation classes for better separation of concerns
- `Line 114`: Manages component dependencies by triggering builds of required components before building the cart component
- `Line 151`: Defines a complete build sequence that includes component dependencies, compilation, and packaging
### cart/src/com/sun/j2ee/blueprints/cart/ejb/ShoppingCartLocal.java

ShoppingCartLocal interface defines the local EJB contract for shopping cart operations in the Java Pet Store application. It extends EJBLocalObject and provides methods to manipulate cart contents including adding items, deleting items, updating quantities, emptying the cart, and retrieving information such as the collection of items, subtotal, and item count. The interface also supports setting the user's locale preference. This interface is part of the cart component and serves as the contract that the ShoppingCart EJB implementation must fulfill.

 **Code Landmarks**
- `Line 47`: Interface extends EJBLocalObject, making it a local EJB component interface that can only be accessed within the same JVM
### cart/src/com/sun/j2ee/blueprints/cart/ejb/ShoppingCartLocalEJB.java

ShoppingCartLocalEJB implements a stateful session bean that manages a user's shopping cart in the Pet Store application. It stores cart items in a HashMap and provides methods to add, delete, update quantities, and retrieve cart items. The class interacts with the CatalogHelper to fetch item details and calculates subtotals. Key functionality includes managing cart operations, locale settings, and converting catalog items to cart items. Important methods include getItems(), addItem(), deleteItem(), updateItemQuantity(), getCount(), getSubTotal(), and empty(). The class maintains a HashMap cart and Locale locale as its main state variables.

 **Code Landmarks**
- `Line 76`: The getItems() method demonstrates integration between cart and catalog components by converting catalog items to cart items
- `Line 114`: The updateItemQuantity() method intelligently removes items when quantity is zero or negative
- `Line 123`: The getSubTotal() method calculates the cart total by iterating through items and multiplying unit cost by quantity
### cart/src/com/sun/j2ee/blueprints/cart/ejb/ShoppingCartLocalHome.java

ShoppingCartLocalHome interface defines the home interface for the Shopping Cart EJB component in the Java Pet Store application. It extends EJBLocalHome and provides methods for creating ShoppingCartLocal objects. The interface declares a single create() method that throws CreateException, allowing clients to instantiate new shopping cart instances. There is also a commented-out create method that would accept a HashMap parameter, suggesting a potential feature for initializing a cart with existing items. This interface is part of the cart component's EJB implementation.

 **Code Landmarks**
- `Line 48`: Interface extends EJBLocalHome, making it a local home interface in the EJB 2.0 component model
- `Line 52`: Commented-out create method suggests a planned feature for initializing carts with existing items
### cart/src/com/sun/j2ee/blueprints/cart/model/CartItem.java

CartItem implements a serializable class representing a line item in a shopping cart. It stores essential product information including itemId, productId, category, name, attribute, quantity, and unitCost. The class provides getter methods for all properties and calculates the total cost by multiplying quantity by unit cost. This simple data model serves as a fundamental building block for the shopping cart functionality in the Java Pet Store application, enabling the storage and manipulation of items selected by users for purchase.

 **Code Landmarks**
- `Line 78`: getTotalCost() method dynamically calculates the total cost by multiplying quantity and unitCost rather than storing it as a field
### cart/src/com/sun/j2ee/blueprints/cart/model/ShoppingCartModel.java

ShoppingCartModel implements a serializable model class that represents shopping cart data in the Java Pet Store application. It stores a collection of CartItem objects and provides methods to access cart information. Key functionality includes retrieving the cart size, getting the collection of items, iterating through items, calculating the total cost of all items in the cart, and copying data from another ShoppingCartModel instance. Important methods include getSize(), getCart(), getItems(), getTotalCost(), and copy(). The class serves as a value object with fine-grained getter methods for the shopping cart component.

 **Code Landmarks**
- `Line 55`: The class implements Serializable to support state persistence across HTTP sessions
- `Line 61`: Provides two constructors - one that accepts items collection and another no-arg constructor specifically for web tier usage
- `Line 86`: The copy() method performs a shallow copy, which could lead to shared references between cart instances
### cart/src/ejb-jar-manifest.mf

This manifest file defines the configuration for the cart component's EJB JAR. It specifies the JAR manifest version and declares dependencies on tracer.jar and catalog-ejb-client.jar through the Class-Path attribute. These dependencies are essential for the cart component to function properly within the Java Pet Store application.

 **Code Landmarks**
- `Line 2-3`: Specifies external JAR dependencies needed by the cart component, establishing the relationship between the cart and catalog components.
### cart/src/ejb-jar.xml

This ejb-jar.xml deployment descriptor configures the ShoppingCart EJB component for the Java Pet Store application. It defines a stateful session bean named ShoppingCartEJB with local interfaces for managing shopping cart functionality. The file specifies container-managed transactions for cart operations like addItem, deleteItem, updateItemQuantity, and empty. It also establishes a local EJB reference to the Catalog component, enabling the shopping cart to access product information. The assembly descriptor defines method permissions (unchecked) and transaction attributes (Required) for all cart operations.

 **Code Landmarks**
- `Line 52`: Defines a stateful session bean which maintains client state across multiple requests
- `Line 56`: Local interfaces are used for EJBs within the same JVM for better performance
- `Line 60`: EJB reference to Catalog component shows component dependencies
- `Line 76`: Container-managed transactions configured with Required attribute for data integrity
### catalog/src/build.xml

This build.xml file defines the build process for the catalog component of the Java Pet Store application. It sets up properties for directory paths, dependencies, and classpath configurations. Key targets include compile (compiling Java source files), ejbjar (creating EJB JAR), ejbclientjar (creating client JAR), clean (removing build artifacts), docs (generating JavaDocs), and core (the main build process). The file manages dependencies on other components like servicelocator and tracer utilities, and configures proper classpath settings for J2EE development.

 **Code Landmarks**
- `Line 93`: Creates separate client JAR that excludes server-side implementation classes
- `Line 74`: Builds EJB JAR with manifest file and deployment descriptor
- `Line 112`: Establishes component dependency chain ensuring proper build order
### catalog/src/com/sun/j2ee/blueprints/catalog/client/CatalogException.java

CatalogException implements a simple custom exception class that serves as the base class for all catalog-related exceptions in the Java Pet Store application. The class extends the standard Java Exception class and implements the Serializable interface to support serialization. It provides two constructors: a default no-argument constructor and one that accepts a string message to be passed to the parent Exception class. This exception is designed to be thrown when catalog operations encounter errors.
### catalog/src/com/sun/j2ee/blueprints/catalog/client/CatalogHelper.java

CatalogHelper implements a client-side interface for accessing catalog data in the Java Pet Store application. It provides dual access methods - either through local EJB calls or direct database access via DAO (Fast Lane Reader pattern). The class manages catalog operations including searching items, retrieving categories, products, and item details. It maintains state through bean-style properties (searchQuery, categoryId, productId, itemId, locale, count, start) and offers methods to retrieve catalog data in paginated form. Key methods include getSearchItems(), getCategories(), getProducts(), getItems(), and getItem(), each with both EJB and DAO implementations. The helper also includes utility methods for locale conversion and service location.

 **Code Landmarks**
- `Line 75`: Implements Fast Lane Reader pattern, allowing direct database access to bypass EJB container for performance-critical operations
- `Line 87`: Uses bean-style setters to maintain state between method calls, enabling flexible usage in JSP pages
- `Line 331`: Uses Service Locator pattern to abstract away JNDI lookup complexity for EJB access
- `Line 350`: Custom locale parsing logic to handle locale strings in format 'language_country_variant'
### catalog/src/com/sun/j2ee/blueprints/catalog/dao/CatalogDAO.java

CatalogDAO interface defines the data access operations for the catalog component of the Java Pet Store application. It serves as a contract for database-specific implementations to retrieve catalog data including categories, products, and items. The interface provides methods for fetching individual entities by ID, paginated collections, and search functionality. All methods accept locale parameters for internationalization support and throw CatalogDAOSysException for system-level errors. Key methods include getCategory, getCategories, getProduct, getProducts, getItem, getItems, and searchItems.

 **Code Landmarks**
- `Line 47`: Interface design pattern used to abstract database operations from business logic
- `Line 53`: All methods include Locale parameter enabling internationalization of catalog data
- `Line 71`: Page object used for pagination across all collection retrieval methods
### catalog/src/com/sun/j2ee/blueprints/catalog/dao/CatalogDAOFactory.java

CatalogDAOFactory implements a factory pattern for creating data access objects for the catalog component. It provides a static getDAO() method that instantiates the appropriate CatalogDAO implementation based on configuration stored in JNDI. The factory looks up the implementation class name from JNDINames.CATALOG_DAO_CLASS and uses reflection to create an instance. If errors occur during lookup or instantiation, the factory throws CatalogDAOSysException with detailed error messages. This approach allows the application to switch DAO implementations without code changes.

 **Code Landmarks**
- `Line 51`: Uses reflection (Class.forName().newInstance()) to dynamically instantiate the DAO implementation class specified in JNDI configuration
- `Line 49`: Implements the Factory pattern to abstract the creation of data access objects from their implementation
### catalog/src/com/sun/j2ee/blueprints/catalog/dao/CloudscapeCatalogDAO.java

CloudscapeCatalogDAO implements the CatalogDAO interface to provide database access for the catalog component of Java Pet Store. It handles SQL operations for retrieving catalog data including categories, products, and items from a Cloudscape database. The class contains methods for fetching individual records (getCategory, getProduct, getItem) and paginated collections (getCategories, getProducts, getItems), plus a search function. It uses prepared SQL statements and a DataSource obtained through ServiceLocator to establish database connections. Each method maps relational data to domain objects like Category, Product, and Item, handling locale-specific content.

 **Code Landmarks**
- `Line 113`: Uses ServiceLocator pattern to obtain database connections, abstracting JNDI lookup details
- `Line 302`: Implements pagination with absolute positioning in ResultSet for efficient data retrieval
- `Line 359`: Complex search implementation that dynamically builds SQL queries based on tokenized keywords
### catalog/src/com/sun/j2ee/blueprints/catalog/dao/GenericCatalogDAO.java

GenericCatalogDAO implements the CatalogDAO interface to provide database access for the Catalog component of the Java Pet Store application. It loads SQL statements from an XML configuration file and dynamically builds queries based on the database type. The class handles database operations for retrieving categories, products, and items, including search functionality. It uses JNDI to obtain database connections and supports pagination through the Page model class. Key methods include getCategory(), getCategories(), getProduct(), getProducts(), getItem(), getItems(), and searchItems(), all of which accept locale parameters for internationalization. The class includes helper methods for building and executing SQL statements, with support for variable parameter substitution.

 **Code Landmarks**
- `Line 75`: Uses XML configuration to load database-specific SQL statements, enabling database portability
- `Line 357`: Implements dynamic SQL statement building with variable fragments for flexible query construction
- `Line 265`: Search implementation tokenizes query string and builds SQL with multiple LIKE clauses
- `Line 397`: Custom SAX parser handler with ParsingDoneException to optimize XML parsing
- `Line 129`: Connection management pattern with proper resource cleanup in closeAll() method
### catalog/src/com/sun/j2ee/blueprints/catalog/ejb/CatalogEJB.java

CatalogEJB implements a stateless session bean that serves as the business interface for accessing catalog data in the Java Pet Store application. It acts as a facade to the data access layer by delegating calls to a CatalogDAO instance. The bean provides methods to retrieve categories, products, and items with pagination support and locale-specific information. Key methods include getCategory, getCategories, getProducts, getProduct, getItems, getItem, and searchItems. The class handles CatalogDAOSysException by wrapping them in EJBException instances.

 **Code Landmarks**
- `Line 70`: The EJB implements a facade pattern, shielding clients from direct interaction with the DAO layer
- `Line 75`: All data access methods support localization through Locale parameter, enabling internationalization
- `Line 92`: Pagination is implemented through start and count parameters for efficient data retrieval
### catalog/src/com/sun/j2ee/blueprints/catalog/ejb/CatalogLocal.java

CatalogLocal interface defines the local EJB interface for catalog operations in the Pet Store application. It extends EJBLocalObject and provides methods to retrieve catalog data with locale support. Key functionality includes retrieving categories, products, and items, as well as supporting pagination through the Page model object and search capabilities. Important methods include getCategory(), getCategories(), getProducts(), getProduct(), getItems(), getItem(), and searchItems(). All methods accept a Locale parameter to support internationalization and most pagination methods take start and count parameters for result set management.

 **Code Landmarks**
- `Line 54`: All methods support internationalization by accepting a Locale parameter
- `Line 56`: Pagination is implemented through start and count parameters with Page return objects
- `Line 67`: Search functionality combines free-text search with pagination capabilities
### catalog/src/com/sun/j2ee/blueprints/catalog/ejb/CatalogLocalHome.java

CatalogLocalHome interface defines the local home interface for the Catalog Enterprise JavaBean (EJB) in the Pet Store application's catalog component. It extends EJBLocalHome and declares a single create() method that returns a CatalogLocal object, potentially throwing a CreateException. This interface follows the EJB design pattern for local interfaces, providing a way for other components within the same JVM to locate and instantiate the Catalog EJB without the overhead of remote method invocation.

 **Code Landmarks**
- `Line 42`: Implements the EJB local component model pattern, which provides more efficient access than remote interfaces for components in the same JVM.
### catalog/src/com/sun/j2ee/blueprints/catalog/exceptions/CatalogDAOSysException.java

CatalogDAOSysException implements a runtime exception class used by the catalog component's Data Access Objects (DAOs) to handle system-level, irrecoverable errors such as SQLExceptions. The class extends Java's standard RuntimeException and provides two constructors: a default no-argument constructor and one that accepts a string parameter explaining the exception condition. This exception serves as a mechanism for propagating critical database or system errors that occur during catalog data operations.
### catalog/src/com/sun/j2ee/blueprints/catalog/model/Category.java

Category implements a serializable class representing different categories of pets in the Java Pet Store Demo. It stores basic category information including an ID, name, and description. The class provides a constructor that initializes all fields, a no-argument constructor for web tier usage, getter methods for all properties, and a toString() method for string representation. Categories serve as top-level organizational elements in the product hierarchy, with each category containing multiple products, which in turn contain inventory items.

 **Code Landmarks**
- `Line 47`: Class implements Serializable interface, enabling it to be converted to a byte stream for persistence or network transmission
- `Line 59`: No-argument constructor specifically provided for web tier usage, facilitating JavaBean pattern compliance
### catalog/src/com/sun/j2ee/blueprints/catalog/model/Item.java

Item implements a serializable class representing a specific item in the catalog component. Each item belongs to a product category and stores essential attributes including category, productId, productName, itemId, description, pricing information (listPrice, unitCost), and various attributes. The class provides a comprehensive constructor to initialize all properties and getter methods to access item details. Key methods include getCategory(), getProductId(), getProductName(), getAttribute() with optional index parameter, getDescription(), getItemId(), getUnitCost(), getListCost(), and getImageLocation().

 **Code Landmarks**
- `Line 77`: Constructor accepts 13 parameters to fully initialize an item, suggesting a complex data model
- `Line 110`: getAttribute() method provides overloaded functionality with both parameterless and indexed versions
### catalog/src/com/sun/j2ee/blueprints/catalog/model/Page.java

Page implements a serializable class that manages paginated results for the catalog component. It stores a list of objects, the starting index, and a flag indicating if more pages exist. The class provides methods to check if next/previous pages are available, retrieve the starting indices of adjacent pages, and get the page size. Key functionality includes pagination control for catalog browsing. Important elements include the EMPTY_PAGE constant, getList(), isNextPageAvailable(), isPreviousPageAvailable(), getStartOfNextPage(), getStartOfPreviousPage(), and getSize() methods.

 **Code Landmarks**
- `Line 52`: The class contains commented code showing evolution from size-based pagination to a simpler boolean flag approach for determining if more pages exist.
### catalog/src/com/sun/j2ee/blueprints/catalog/model/Product.java

Product implements a serializable class representing different types of pets within a category in the Java Pet Store Demo. It stores basic product information including an ID, name, and description. The class provides two constructors - one that initializes all fields and another no-argument constructor for web tier usage. It includes getter methods for all properties and overrides toString() to display the product information in a formatted string. This simple model class serves as a fundamental data structure in the catalog component.
### catalog/src/com/sun/j2ee/blueprints/catalog/util/DatabaseNames.java

DatabaseNames implements a utility class that centralizes database table name constants for the catalog component of the Java Pet Store application. It defines three important table name constants: PRODUCT_TABLE, CATEGORY_TABLE, and ITEM_TABLE. The class also provides a getTableName method that handles internationalization by appending locale-specific suffixes to table names based on the provided Locale parameter, supporting US (default), Japanese, and Chinese locales. This approach prevents hardcoding table names throughout the application and centralizes the naming logic for database tables.

 **Code Landmarks**
- `Line 53`: Implements locale-specific database table naming with suffix pattern (_ja for Japan, _zh for China)
### catalog/src/com/sun/j2ee/blueprints/catalog/util/JNDINames.java

JNDINames is a utility class that centralizes JNDI naming constants for the catalog component. It provides static final String constants for EJB home objects, data sources, and application properties that are used throughout the catalog component. The class includes references to the Catalog EJB home, CatalogDB data source, and configuration parameters like CatalogDAOClass and CatalogDAOSQLURL. A private constructor prevents instantiation, enforcing the class's role as a static constant repository. These JNDI names must be synchronized with the application's deployment descriptors to ensure proper resource lookup.

 **Code Landmarks**
- `Line 45`: Private constructor prevents instantiation, enforcing usage as a static utility class
- `Line 47`: Constants are organized by resource type (EJB homes, datasources, application properties) for better organization
- `Line 42`: Class documentation explicitly mentions that changes here must be reflected in deployment descriptors
### catalog/src/ejb-jar-manifest.mf

This manifest file specifies the JAR dependencies for the catalog EJB component in the Java Pet Store application. It declares the manifest version as 1.0 and defines the Class-Path attribute to include tracer.jar and servicelocator.jar, which are required libraries for the catalog component's functionality.
### catalog/src/ejb-jar.xml

This XML file defines the Enterprise JavaBeans (EJB) deployment descriptor for the catalog component of Java Pet Store. It configures a stateless session bean named CatalogEJB that provides catalog functionality through a local interface. The file specifies environment entries for DAO class configuration, database resource references, and transaction attributes for various catalog methods including getItem, searchItems, getProduct, getProducts, getCategory, getItems, and getCategories. It also defines method permissions and container-managed transactions for all catalog operations.

 **Code Landmarks**
- `Line 67`: Configures DAO implementation class with commented alternative option showing flexibility in database backend selection
- `Line 73`: Database configuration shows support for multiple databases (Oracle/Cloudscape) through environment entries
- `Line 82`: Resource reference for SQL URL suggests externalized SQL queries for database portability
- `Line 93`: Security configuration uses 'unchecked' permission, allowing any authenticated user to access catalog methods
- `Line 172`: Client JAR specification indicates separation of interface from implementation for remote clients
### contactinfo/src/build.xml

This build.xml file defines the Ant build process for the ContactInfo component in Java Pet Store. It manages compilation, packaging, and documentation generation for the component's EJB modules. The script defines targets for initializing properties, compiling source files, building dependent components, creating EJB JARs, generating JavaDocs, and cleaning build artifacts. Key targets include 'init', 'compile', 'components', 'ejbjar', 'ejbclientjar', 'clean', 'docs', and 'core'. Important properties include paths for source, build directories, dependent components, and classpath definitions.

 **Code Landmarks**
- `Line 94`: Copies compiled classes from the Address component, showing component dependencies
- `Line 115`: Creates separate client JAR by excluding EJB implementation classes
- `Line 143`: JavaDoc configuration includes multiple component source paths for comprehensive API documentation
### contactinfo/src/com/sun/j2ee/blueprints/contactinfo/ejb/ContactInfo.java

ContactInfo implements a data model class for storing personal contact information including name, address, email, and phone number. It provides constructors, getters, and setters for all fields, and implements XML serialization/deserialization through toDOM() and fromDOM() methods. The class works with the Address class for structured address information and uses XMLDocumentUtils for DOM manipulation. Constants define XML element names and DTD identifiers used in the serialization process.

 **Code Landmarks**
- `Line 131`: XML deserialization method uses a structured approach to parse DOM elements in a specific order
- `Line 114`: toDOM method demonstrates how to build a structured XML document from object properties
### contactinfo/src/com/sun/j2ee/blueprints/contactinfo/ejb/ContactInfoEJB.java

ContactInfoEJB implements an entity bean that manages customer contact information including name, telephone, email, and address details. It provides abstract getter/setter methods for CMP fields, maintains a relationship with AddressLocal through CMR, and offers multiple ejbCreate methods for different initialization scenarios. The class includes a getData() method that returns a ContactInfo value object containing all contact information. Standard EJB lifecycle methods are implemented to handle entity bean operations. Key components include abstract CMP field methods, CMR field for address relationship, and ServiceLocator usage for address creation.

 **Code Landmarks**
- `Line 76`: Uses ServiceLocator pattern to obtain EJB references rather than direct JNDI lookups
- `Line 115`: Implements a data transfer object pattern with getData() to transfer entity data to client tier
- `Line 53`: Demonstrates Container-Managed Relationships (CMR) with AddressLocal entity
### contactinfo/src/com/sun/j2ee/blueprints/contactinfo/ejb/ContactInfoLocal.java

ContactInfoLocal defines a local EJB interface for managing customer contact information in the Pet Store application. It extends javax.ejb.EJBLocalObject and provides getter and setter methods for contact details including family name, given name, address, telephone number, and email address. The interface also includes a getData() method to retrieve the complete ContactInfo object. It interacts with the AddressLocal interface to manage address information, forming part of the customer data management system.

 **Code Landmarks**
- `Line 48`: The interface extends javax.ejb.EJBLocalObject, making it a local EJB component accessible only within the same JVM
### contactinfo/src/com/sun/j2ee/blueprints/contactinfo/ejb/ContactInfoLocalHome.java

ContactInfoLocalHome interface defines the local home interface for the ContactInfo Enterprise JavaBean in the Java Pet Store application. It extends javax.ejb.EJBLocalHome and provides methods for creating and finding ContactInfo entities. The interface includes three create methods: one with no parameters, another accepting individual contact information fields (name, telephone, email) and an address object, and a third accepting a ContactInfo value object. It also defines a standard findByPrimaryKey method to retrieve existing ContactInfo entities by their primary key.

 **Code Landmarks**
- `Line 45`: The interface uses AddressLocal as a parameter type, showing composition relationship between contact info and address components
- `Line 49`: Provides multiple creation methods with different parameter sets, demonstrating flexible entity instantiation patterns
### contactinfo/src/com/sun/j2ee/blueprints/contactinfo/ejb/JNDINames.java

JNDINames implements a utility class that serves as a central repository for JNDI name constants used in the ContactInfo component. The class prevents instantiation through a private constructor and defines a single public static final constant ADDR_EJB that stores the JNDI name for the Address EJB. This constant is used to look up the Address EJB in the application's JNDI context. The class documentation notes that any changes to these constants should be reflected in the deployment descriptors to maintain consistency.

 **Code Landmarks**
- `Line 46`: Private constructor prevents instantiation of this utility class, enforcing its use as a static constants container
- `Line 49`: The JNDI name uses the java:comp/env namespace, following J2EE best practices for portable component references
### contactinfo/src/com/sun/j2ee/blueprints/contactinfo/rsrc/schemas/ContactInfo.dtd

This DTD file defines the XML structure for contact information in the Java Pet Store application. It establishes the ContactInfo element which contains child elements for FamilyName, GivenName, Address, Email, and Phone. The file imports an external Address DTD using an entity reference, allowing for standardized address formatting across the application. This schema provides the structural validation rules for contact information data used throughout the application, ensuring consistency in customer data representation.

 **Code Landmarks**
- `Line 46`: Uses entity reference to import an external Address DTD, demonstrating modular XML schema design
### contactinfo/src/ejb-jar.xml

This ejb-jar.xml deployment descriptor configures the ContactInfo component of the Java Pet Store application. It defines two container-managed persistence (CMP) entity beans: ContactInfoEJB and AddressEJB. ContactInfoEJB stores customer contact information including telephone, name, and email fields, while AddressEJB manages address details like street, city, state, and country. The file establishes a one-to-one relationship between these entities with cascade delete functionality. It also specifies method permissions (all methods are unchecked) and container transaction attributes (all set to 'Required') for each bean's methods, ensuring proper transaction management during database operations.

 **Code Landmarks**
- `Line 133`: One-to-one relationship between ContactInfo and Address with cascade delete, ensuring address records are automatically removed when contact info is deleted
- `Line 47`: Container-managed persistence (CMP 2.x) is used for both entity beans, delegating persistence responsibilities to the EJB container
- `Line 81`: EJB local reference establishes dependency between ContactInfo and Address components
- `Line 147`: Comprehensive method permissions configuration grants unchecked access to all ContactInfo methods
### creditcard/src/build.xml

This build.xml file defines the build process for the CreditCard component in the Java Pet Store application. It configures targets for compiling Java classes, creating EJB JAR files, generating documentation, and cleaning build artifacts. The script defines properties for directory paths, dependencies, and classpath settings. Key targets include 'compile', 'ejbjar', 'ejbclientjar', 'clean', 'components', 'docs', and 'core'. The build process handles compilation of source files, packaging of EJB components, and generation of client libraries while managing dependencies on other components like xmldocuments.

 **Code Landmarks**
- `Line 106`: Creates separate client JAR by removing implementation classes from the build
- `Line 91`: Preserves resource files during compilation by copying them to the class output directory
- `Line 139`: Builds documentation that includes both this component and its dependencies
### creditcard/src/com/sun/j2ee/blueprints/creditcard/ejb/CreditCard.java

CreditCard implements a data model class for credit card information in the Java Pet Store application. It stores credit card details including card number, expiry date, and card type. The class provides getter and setter methods for these properties and implements XML serialization/deserialization functionality through toDOM() and fromDOM() methods. It defines XML constants for document structure and includes DTD identifiers for validation. The class interacts with XMLDocumentUtils for DOM manipulation and handles XML document exceptions when parsing credit card data from DOM nodes.

 **Code Landmarks**
- `Line 101`: XML serialization method converts credit card data to DOM structure
- `Line 108`: Static fromDOM method provides factory pattern implementation for creating CreditCard objects from XML nodes
- `Line 46`: Class defines DTD constants for XML validation of credit card data
### creditcard/src/com/sun/j2ee/blueprints/creditcard/ejb/CreditCardEJB.java

CreditCardEJB implements an entity bean for managing credit card information in the Java Pet Store application. It provides container-managed persistence (CMP) for credit card data including card number, type, and expiry date. The class offers methods to create credit card entities, retrieve formatted expiry month/year values, and access the complete credit card data through a transfer object. It implements the standard EntityBean lifecycle methods required by the EJB specification and provides utility methods for parsing expiry date strings.

 **Code Landmarks**
- `Line 62`: Uses a transfer object pattern with CreditCard class to encapsulate entity data for client access
- `Line 87`: Implements helper methods to parse expiry date string into month and year components
- `Line 45`: Uses Container-Managed Persistence (CMP) with abstract accessor methods
### creditcard/src/com/sun/j2ee/blueprints/creditcard/ejb/CreditCardLocal.java

CreditCardLocal defines a local interface for the CreditCard Enterprise JavaBean in the Java Pet Store application. It extends javax.ejb.EJBLocalObject and provides methods to access and modify credit card information. The interface includes getter and setter methods for card number, card type, and expiry date, with additional methods to retrieve expiry month and year separately. It also includes a getData() method that returns a CreditCard object containing all the credit card information. This interface is part of the credit card processing component of the application.

 **Code Landmarks**
- `Line 47`: The interface extends javax.ejb.EJBLocalObject, indicating it's designed for local (same JVM) EJB access rather than remote calls
### creditcard/src/com/sun/j2ee/blueprints/creditcard/ejb/CreditCardLocalHome.java

CreditCardLocalHome interface defines the local home interface for the CreditCard Enterprise JavaBean (EJB) component in the Java Pet Store application. It extends javax.ejb.EJBLocalHome and provides methods for creating and finding CreditCard EJB instances. The interface includes three create methods with different parameter options: one with no parameters, one accepting card details (number, type, expiry date), and one accepting a CreditCard object. It also provides a findByPrimaryKey method to locate existing credit card entities by their primary key.

 **Code Landmarks**
- `Line 44`: Interface extends EJBLocalHome, indicating this is a local component interface for container-managed EJBs in the J2EE architecture
### creditcard/src/com/sun/j2ee/blueprints/creditcard/rsrc/schemas/CreditCard.dtd

This DTD file defines the XML structure for credit card information used in the Java Pet Store application. It establishes a schema with a root element 'CreditCard' that contains three child elements: 'CardNumber', 'CardType', and 'ExpiryDate'. Each child element is defined to contain parsed character data (#PCDATA). This schema provides a standardized format for representing credit card information within the application, ensuring consistent data structure for credit card processing functionality.

 **Code Landmarks**
- `Line 38`: Simple DTD structure defines a credit card format with three essential elements, providing a clean data model for payment processing
### creditcard/src/ejb-jar.xml

This ejb-jar.xml deployment descriptor defines the CreditCardEJB entity bean for the credit card component. It specifies container-managed persistence (CMP 2.x) with fields for expiryDate, cardType, and cardNumber. The file configures local interfaces, security settings allowing unchecked access to all methods, and transaction attributes for all bean methods. It establishes required transaction attributes for operations like create, remove, find, and getter/setter methods, ensuring proper transaction management for credit card data operations within the Pet Store application.

 **Code Landmarks**
- `Line 53`: Uses container-managed persistence (CMP 2.x) for the entity bean, simplifying data access
- `Line 54`: Uses java.lang.Object as the primary key class, indicating a generated primary key
- `Line 83`: Implements unchecked method permissions, allowing any authenticated user to access all methods
- `Line 107`: Defines multiple create methods with different parameter signatures for flexibility
### customer/src/build.xml

This build.xml file defines the build process for the customer component in Java Pet Store. It configures compilation, packaging, and documentation generation tasks. The script creates EJB JAR files for both server and client components, manages dependencies on contactinfo and creditcard components, and handles compilation of Java source files. Key targets include init, compile, components, ejbjar, ejbclientjar, clean, docs, and core. Important properties define paths for source files, build directories, and dependent components.

 **Code Landmarks**
- `Line 87`: Builds dependent components (contactinfo and creditcard) before building the customer component
- `Line 96`: Creates separate client JAR by excluding server implementation classes
- `Line 107`: Explicitly removes EJB implementation classes from client JAR to enforce separation of concerns
### customer/src/com/sun/j2ee/blueprints/customer/account/ejb/AccountEJB.java

AccountEJB implements an entity bean that manages customer account data in the Java Pet Store application. It provides Container-Managed Persistence (CMP) for account status and Container-Managed Relationships (CMR) with ContactInfoLocal and CreditCardLocal entities. The class defines abstract getter/setter methods for CMP fields, implements two ejbCreate methods with corresponding ejbPostCreate methods for account creation with different parameters, and includes standard EntityBean lifecycle methods. Key functionality includes creating accounts with associated contact information and credit card details through EJB relationships.

 **Code Landmarks**
- `Line 74`: Uses Container-Managed Relationships (CMR) to establish relationships with ContactInfo and CreditCard entities
- `Line 85`: Implements two different creation paths - one requiring pre-existing related entities and another that creates them
- `Line 93`: JNDI lookups in ejbPostCreate method to create related entities when not provided
### customer/src/com/sun/j2ee/blueprints/customer/account/ejb/AccountLocal.java

AccountLocal interface defines a local EJB interface for customer account management in the Java Pet Store application. It extends javax.ejb.EJBLocalObject and provides methods to access and modify account information. The interface includes methods for managing account status, credit card information, and contact details. Key methods include getStatus(), setStatus(), getCreditCard(), setCreditCard(), getContactInfo(), and setContactInfo(), which allow interaction with associated CreditCardLocal and ContactInfoLocal EJB components.

 **Code Landmarks**
- `Line 41`: Uses EJB local interfaces (introduced in EJB 2.0) for efficient in-container communication
### customer/src/com/sun/j2ee/blueprints/customer/account/ejb/AccountLocalHome.java

AccountLocalHome interface defines the local home interface for the Account EJB in the customer account component. It extends javax.ejb.EJBLocalHome and provides methods for creating and finding Account entities. The interface declares two status constants (Active and Disabled) and two create methods: one that takes only a status parameter and another that additionally accepts ContactInfoLocal and CreditCardLocal objects. It also includes a standard findByPrimaryKey method for retrieving Account entities by their primary key.

 **Code Landmarks**
- `Line 46`: The interface defines status constants (Active, Disabled) directly in the interface rather than in an enum or separate constants class
### customer/src/com/sun/j2ee/blueprints/customer/ejb/CustomerEJB.java

CustomerEJB implements an entity bean that manages customer data in the Java Pet Store application. It uses Container-Managed Persistence (CMP) to store customer information and Container-Managed Relationships (CMR) to associate customers with their accounts and profiles. The class defines abstract getter/setter methods for the userId field and relationships with AccountLocal and ProfileLocal entities. The ejbCreate and ejbPostCreate methods initialize a new customer by creating associated Account and Profile entities through their respective local home interfaces. Standard EntityBean lifecycle methods are also implemented.

 **Code Landmarks**
- `Line 69`: Uses Container-Managed Relationships (CMR) to maintain associations between customer, account, and profile entities
- `Line 78`: ejbPostCreate method demonstrates J2EE dependency lookup pattern using JNDI to obtain local EJB references
- `Line 81`: Creates default account and profile entities with predefined constants during customer creation
### customer/src/com/sun/j2ee/blueprints/customer/ejb/CustomerLocal.java

CustomerLocal interface defines the local EJB interface for customer data access in the Pet Store application. It extends javax.ejb.EJBLocalObject and provides methods to access customer information through three key methods: getUserId() to retrieve the customer identifier, getAccount() to access the customer's account information through AccountLocal, and getProfile() to access the customer's profile information through ProfileLocal. This interface serves as a contract for local EJB client interactions with customer data.

 **Code Landmarks**
- `Line 43`: Uses EJB local interfaces (introduced in EJB 2.0) for optimized same-JVM component communication
### customer/src/com/sun/j2ee/blueprints/customer/ejb/CustomerLocalHome.java

CustomerLocalHome interface defines the local home interface for the Customer Enterprise JavaBean in the Java Pet Store application. It extends javax.ejb.EJBLocalHome and provides methods for creating and finding Customer entities within the application's persistence layer. The interface declares three key methods: create() for instantiating new customer entities with a userId, findByPrimaryKey() to locate specific customers by their userId, and findAllCustomers() to retrieve all customer records. These methods throw standard EJB exceptions (CreateException and FinderException) to handle error conditions during entity creation and lookup operations.

 **Code Landmarks**
- `Line 45`: Extends javax.ejb.EJBLocalHome, indicating this is a local interface for container-managed EJB access
### customer/src/com/sun/j2ee/blueprints/customer/profile/ejb/ProfileEJB.java

ProfileEJB implements a container-managed persistence (CMP) entity bean that stores user profile preferences for the Java Pet Store application. It manages four key user preferences: preferred language, favorite category, myList preference, and banner preference. The class provides abstract getter and setter methods for these CMP fields, which are automatically implemented by the EJB container. It also implements required EntityBean lifecycle methods including ejbCreate for initialization, ejbPostCreate, and standard methods for entity context management. This bean serves as the persistence layer for storing customer profile preferences in the database.

 **Code Landmarks**
- `Line 46`: Uses abstract methods for CMP fields, letting the EJB container generate the implementation
- `Line 59`: EntityBean implementation with minimal business logic, focusing on data persistence
### customer/src/com/sun/j2ee/blueprints/customer/profile/ejb/ProfileInfo.java

ProfileInfo implements a serializable data class that encapsulates a customer's profile preferences in the Java Pet Store application. It stores four key user preferences: preferred language, favorite product category, and boolean flags for MyList and banner display preferences. The class provides a constructor to initialize these preferences and getter methods to access them. It also includes a toString() method for debugging and display purposes. This class serves as a data transfer object between the customer profile EJB and client applications.

 **Code Landmarks**
- `Line 39`: Class implements Serializable interface to support EJB remote method calls and persistence
### customer/src/com/sun/j2ee/blueprints/customer/profile/ejb/ProfileLocal.java

ProfileLocal defines a local interface for the Profile Enterprise JavaBean in the customer component of Java Pet Store. It provides methods to access and modify user preferences including language preference, favorite product category, and boolean flags for MyList and Banner features. The interface extends javax.ejb.EJBLocalObject, making it a local EJB component accessible within the same JVM. Key methods include getPreferredLanguage(), setPreferredLanguage(), getFavoriteCategory(), setFavoriteCategory(), getMyListPreference(), setMyListPreference(), getBannerPreference(), and setBannerPreference().

 **Code Landmarks**
- `Line 39`: Extends javax.ejb.EJBLocalObject indicating this is a local EJB interface that can only be accessed within the same JVM
### customer/src/com/sun/j2ee/blueprints/customer/profile/ejb/ProfileLocalHome.java

ProfileLocalHome interface defines the local home interface for the Profile Enterprise JavaBean in the customer profile component. It extends EJBLocalHome and declares default values for user preferences including language, favorite category, and display preferences. The interface provides two key methods: create() for instantiating new Profile beans with specified preference parameters, and findByPrimaryKey() for retrieving existing Profile beans. These methods enable client code to interact with Profile EJBs through the local interface within the same JVM, avoiding remote call overhead.

 **Code Landmarks**
- `Line 43`: Defines default values as public static final constants that can be referenced by other classes without instantiation
### customer/src/ejb-jar-manifest.mf

This manifest file defines the Java Archive (JAR) metadata for the customer EJB component. It specifies the manifest version and declares dependencies on two external JAR files: xmldocuments.jar and servicelocator.jar. These dependencies are essential for the customer component to access XML document handling functionality and service location capabilities during runtime.

 **Code Landmarks**
- `Line 2`: The Class-Path entry enables the EJB container to locate and load dependent libraries at runtime without explicit coding in the application.
### customer/src/ejb-jar.xml

This ejb-jar.xml file defines the Enterprise JavaBeans (EJB) deployment descriptor for the customer component of Java Pet Store. It configures six container-managed persistence (CMP) entity beans: CustomerEJB, ProfileEJB, CreditCardEJB, ContactInfoEJB, AddressEJB, and AccountEJB. Each entity bean is defined with its local interfaces, implementation class, persistence fields, and relationships. The file establishes key relationships between these entities, such as Customer-to-Profile, Customer-to-Account, Account-to-ContactInfo, Account-to-CreditCard, and ContactInfo-to-Address, with cascade delete operations configured. It also specifies transaction attributes for all entity bean methods, setting them to 'Required', and defines security permissions with unchecked access to all methods. The descriptor includes an EJB query language (EJB-QL) statement for finding all customers.

 **Code Landmarks**
- `Line 53`: CustomerEJB uses String as primary key class while other entities use Object, showing different identification strategies
- `Line 349`: Relationships section defines one-to-one mappings with cascade-delete operations to ensure proper data cleanup
- `Line 84`: EJBQL query defined for finding all customers demonstrates container-managed query capabilities
- `Line 453`: Comprehensive transaction attributes configuration ensures data integrity across all entity operations
### encodingfilter/src/build.xml

This build.xml file defines the Ant build process for the encoding filter component of the Java Pet Store application. It establishes properties for source, build, and documentation directories, sets up classpaths for compilation, and defines several targets including 'init', 'compile', 'clean', 'banner', 'core', and 'all'. The script compiles Java classes from the com/** package into a specified build directory, using J2EE libraries in the classpath. The default target 'core' depends on 'banner' and 'compile' targets, providing a complete build process for the encoding filter component.

 **Code Landmarks**
- `Line 44`: References a version ID suggesting this file is under version control with specific release tracking
- `Line 59`: Imports user-specific properties first, allowing for developer customization without modifying the main build file
### encodingfilter/src/com/sun/j2ee/blueprints/encodingfilter/web/EncodingFilter.java

EncodingFilter implements a J2EE servlet filter that ensures consistent character encoding for HTTP requests. It reads the target encoding from filter configuration parameters and applies it to each incoming request. The filter implements the standard Filter interface with init(), destroy(), and doFilter() methods. The doFilter() method casts the ServletRequest to HttpServletRequest and sets its character encoding to the configured value before passing the request down the filter chain. By default, the encoding is set to ASCII if no configuration is provided.

 **Code Landmarks**
- `Line 69`: Sets character encoding on the request before passing it down the filter chain, preventing encoding issues with form submissions
### lineitem/src/build.xml

This build.xml file defines the Ant build process for the LineItem component in the Java Pet Store application. It establishes build targets for compiling, cleaning, generating documentation, and building dependent components. The script sets up directory structures, classpaths, and compilation parameters, with dependencies on the XMLDocuments component. Key targets include 'init' for property setup, 'compile' for source compilation, 'components' for building dependencies, 'docs' for JavaDoc generation, and 'core' as the default target that builds the complete component.

 **Code Landmarks**
- `Line 47`: Defines component dependencies showing the hierarchical structure of the Pet Store application
- `Line 73`: Creates a classpath that combines local classes with external J2EE dependencies
- `Line 87`: Includes resource files in the build output alongside compiled classes
### lineitem/src/com/sun/j2ee/blueprints/lineitem/ejb/LineItem.java

LineItem implements a class representing a single item in an order, storing product identification (categoryId, productId, itemId), line number, quantity, and unit price. The class provides getters and setters for all properties, and implements XML serialization/deserialization through toDOM() and fromDOM() methods. It defines XML constants for element and attribute names used in the document structure, and includes DTD identifiers for validation. The class works with XMLDocumentUtils for DOM manipulation, enabling conversion between Java objects and XML representations for persistence and data exchange.

 **Code Landmarks**
- `Line 136`: Uses XMLDocumentUtils helper class to build DOM nodes from object properties
- `Line 145`: Static factory method parses XML DOM into LineItem objects with validation
### lineitem/src/com/sun/j2ee/blueprints/lineitem/ejb/LineItemEJB.java

LineItemEJB implements an Entity Bean representing a line item in an order within the Java Pet Store application. It manages product information (category, product, item IDs), quantities, pricing, and shipping status. The class provides abstract getter/setter methods for all properties and implements standard EJB lifecycle methods. Key functionality includes creating line items, retrieving line item data as a transfer object, and tracking shipped quantities. Important methods include ejbCreate(), getData(), and the various abstract accessors/mutators for line item properties.

 **Code Landmarks**
- `Line 134`: Uses a Data Transfer Object pattern to convert entity bean data to a serializable object
- `Line 114`: Provides overloaded ejbCreate methods to support different creation scenarios
- `Line 91`: Tracks both total quantity and shipped quantity separately for order fulfillment
### lineitem/src/com/sun/j2ee/blueprints/lineitem/ejb/LineItemLocal.java

LineItemLocal defines a local interface for the LineItem Enterprise JavaBean that represents an individual item in an order. It extends EJBLocalObject and provides methods to access line item properties such as category ID, product ID, item ID, line number, quantity, unit price, and quantity shipped. The interface also includes methods to set the quantity shipped and retrieve the complete line item data as a LineItem object. This interface is part of the line item component in the Java Pet Store application and has no remote interface.

 **Code Landmarks**
- `Line 45`: The interface is explicitly designed as local-only (no remote interface) which optimizes for performance in the same JVM
### lineitem/src/com/sun/j2ee/blueprints/lineitem/ejb/LineItemLocalHome.java

LineItemLocalHome interface defines the local home interface for the LineItem EJB in the Java Pet Store application. It extends EJBLocalHome and provides methods for creating and finding LineItem entities. The interface includes two create methods: one that takes individual parameters (catId, prodId, itemId, lineNo, qty, price, qtyShipped) and another that accepts a LineItem object with quantity. It also defines a findByPrimaryKey method to locate LineItem entities by their primary key. This interface is part of the EJB component architecture and has no remote interface.

 **Code Landmarks**
- `Line 46`: This EJB explicitly declares it has no remote interface, indicating it's designed for local container-managed access only
### lineitem/src/com/sun/j2ee/blueprints/lineitem/rsrc/schemas/LineItem.dtd

This DTD file defines the structure and elements of a LineItem entity in the Java Pet Store application. It specifies the XML document structure for line items in orders, establishing required elements including CategoryId, ProductId, ItemId, LineNum, Quantity, and UnitPrice. The schema ensures that line item data follows a consistent format for processing within the e-commerce application's order management system. This schema definition supports XML validation for line item data exchanged between components.
### lineitem/src/ejb-jar.xml

This ejb-jar.xml file defines the deployment descriptor for the LineItem Container-Managed Persistence (CMP) Entity Bean in the Java Pet Store application. It specifies the bean's interfaces, persistence fields (categoryId, productId, itemId, lineNumber, quantity, unitPrice, quantityShipped), and transaction attributes. The file configures security permissions (unchecked for all methods) and transaction requirements for all getter/setter methods and create operations. The LineItem entity represents order line items with product information, quantities, and pricing details.

 **Code Landmarks**
- `Line 59`: Uses container-managed persistence (CMP) version 2.x, allowing the container to handle all database operations automatically
- `Line 61`: Uses Object as the primary key class, indicating a compound or generated primary key strategy
- `Line 94`: Assembly descriptor defines transaction attributes for all bean methods, ensuring data integrity
### mailer/src/build.xml

This build.xml file defines the Ant build process for the Mailer component in the Java Pet Store application. It establishes targets for compiling Java classes, creating EJB JAR files, generating documentation, and cleaning build artifacts. The script sets up properties for source directories, build locations, and dependencies including the XMLDocuments component. Key targets include 'init' for property setup, 'compile' for building classes, 'ejbjar' and 'ejbclientjar' for packaging, 'docs' for JavaDoc generation, and 'core' as the main build target that orchestrates the component build process.

 **Code Landmarks**
- `Line 73`: Establishes dependency on XMLDocuments component for PO/Invoice classes
- `Line 79`: Creates a specific classpath that includes J2EE libraries and dependent components
- `Line 167`: The 'core' target orchestrates the complete build process with dependencies
### mailer/src/com/sun/j2ee/blueprints/mailer/ejb/ByteArrayDataSource.java

ByteArrayDataSource implements a utility class for the Java Pet Store mailer component that creates a DataSource from string data for email messages. It implements the javax.activation.DataSource interface to provide data handling for email content. The class stores byte array data and MIME type information, offering methods to access the input stream, content type, and name. Key functionality includes converting string data to bytes with UTF-8 encoding and providing an input stream for the mail message content. The class deliberately throws an exception when getOutputStream() is called as it's designed for read-only access.

 **Code Landmarks**
- `Line 64`: Silently catches UnsupportedEncodingException when converting to UTF-8, which could lead to undetected encoding issues
- `Line 73`: Implements defensive programming by checking for null data before creating an input stream
### mailer/src/com/sun/j2ee/blueprints/mailer/ejb/Mail.java

Mail implements a class representing an email message with address, subject, and content fields. It provides methods for XML serialization and deserialization using DOM manipulation. The class includes functionality to convert Mail objects to XML format (toXML methods) and create Mail objects from XML (fromXML methods). It supports validation against a DTD and handles entity resolution. The class defines constants for XML element names and DTD identifiers. Important methods include constructors, getters, toXML(), fromXML(), toDOM(), and fromDOM(). A main method allows testing XML conversion from command line.

 **Code Landmarks**
- `Line 91`: Provides multiple XML serialization methods with different output formats (String or Result)
- `Line 119`: Implements XML deserialization with configurable validation and entity resolution
- `Line 146`: Uses DOM manipulation for XML representation of the mail object
### mailer/src/com/sun/j2ee/blueprints/mailer/ejb/MailHelper.java

MailHelper implements a utility class for sending emails in the Java Pet Store application. It provides functionality to create and send email messages using J2EE mail services. The class contains a single method, createAndSendMail(), which takes recipient email address, subject, mail content, and locale as parameters. It uses JNDI to look up the mail session, creates a MimeMessage with HTML content type, sets the necessary headers and properties, and sends the message using Transport.send(). The class handles exceptions by wrapping them in a MailerAppException.

 **Code Landmarks**
- `Line 65`: Uses JNDI lookup to obtain the mail session from the application server
- `Line 72`: Creates HTML-formatted email content with text/html content type
- `Line 74`: Uses ByteArrayDataSource which is referenced but not imported in the file
### mailer/src/com/sun/j2ee/blueprints/mailer/ejb/MailerMDB.java

MailerMDB implements a message-driven bean that listens for JMS messages containing XML-formatted email information. When a message arrives in the mailer queue, the onMessage method extracts the email address, subject, and content from the XML message, then delegates to a MailHelper to send the email. The class handles exceptions related to mail server configuration, XML parsing, and JMS operations. Key methods include onMessage(), sendMail(), and getMailHelper(). The bean implements standard EJB lifecycle methods like ejbCreate(), setMessageDrivenContext(), and ejbRemove().

 **Code Landmarks**
- `Line 65`: The onMessage method silently catches MailerAppException, allowing the application to continue even if mail server setup is incomplete
- `Line 69`: The method converts XML message content to a Mail object using a static fromXML method, demonstrating XML-to-object deserialization
### mailer/src/com/sun/j2ee/blueprints/mailer/exceptions/MailerAppException.java

MailerAppException implements a custom exception class for the mailer component in the Java Pet Store application. It extends the standard Java Exception class and is thrown when there are failures during mail sending operations. The class provides two constructors: a default constructor that takes no arguments and another constructor that accepts a string parameter to explain the exception condition. This exception helps in error handling and propagation within the mailer component's workflow.
### mailer/src/com/sun/j2ee/blueprints/mailer/rsrc/schemas/Mail.dtd

Mail.dtd defines the XML schema structure for email messages in the Java Pet Store mailer component. It specifies a simple email document format with three required elements: Mail (the root element), Address (recipient email address), Subject (email subject line), and Content (the body of the email message). Each child element contains character data (#PCDATA). This DTD provides the structural validation rules for XML documents representing emails processed by the mailer component.

 **Code Landmarks**
- `Line 37`: Simple but complete email structure definition with just three essential elements
### mailer/src/com/sun/j2ee/blueprints/mailer/util/JNDINames.java

JNDINames implements a utility class that centralizes JNDI name constants used throughout the mailer component of the Java Pet Store application. It defines a single constant MAIL_SESSION that points to the mail session resource with the JNDI path 'java:comp/env/mail/MailSession'. The class has a private constructor to prevent instantiation, functioning purely as a container for constants. This centralization ensures consistency across the application and simplifies maintenance, as any changes to JNDI paths only need to be updated in this single location and corresponding deployment descriptors.

 **Code Landmarks**
- `Line 46`: Private constructor prevents instantiation, enforcing the class's role as a static utility
### mailer/src/ejb-jar-manifest.mf

This manifest file defines the configuration for the mailer component's EJB JAR file. It specifies the manifest version (1.0) and declares a dependency on xmldocuments.jar through the Class-Path attribute. This ensures that when the mailer EJB component is deployed, it has access to the required XML document processing functionality.
### mailer/src/ejb-jar.xml

This ejb-jar.xml is a deployment descriptor for the Mailer component in Java Pet Store. It defines a Message-Driven Bean (MDB) named MailerMDB that processes purchase orders from a JMS queue. The MDB is configured with container-managed transactions and requires a mail session resource reference (mail/MailSession) for sending emails. The descriptor specifies that the onMessage method requires a transaction attribute and accepts a javax.jms.Message parameter.

 **Code Landmarks**
- `Line 52`: Defines a message-driven bean that listens to a JMS Queue for asynchronous processing of mail messages
- `Line 61`: Configures a mail session resource reference that will be injected by the container for sending emails
- `Line 76`: Sets transaction attribute to 'Required' ensuring mail processing occurs within a transaction
### processmanager/src/build.xml

This build.xml file defines the Ant build process for the ProcessManager component of the Java Pet Store application. It configures build properties, directory structures, and implements targets for compiling Java classes, creating EJB JARs, generating documentation, and cleaning build artifacts. Key targets include 'compile', 'ejbjar', 'ejbclientjar', 'clean', 'docs', and 'core'. The script manages dependencies between targets and sets up the necessary classpaths for compilation against the J2EE framework.

 **Code Landmarks**
- `Line 94`: Creates separate client JAR by excluding implementation classes, following EJB best practices for deployment
- `Line 48`: Imports user-specific properties from home directory, enabling developer-specific configurations
- `Line 127`: Defines a modular build with core and all targets, supporting different build scenarios
### processmanager/src/com/sun/j2ee/blueprints/processmanager/ejb/OrderStatusNames.java

OrderStatusNames implements a utility class that centralizes the definition of constants representing the various states an order can have in the Pet Store application. It defines five string constants: PENDING (for orders placed but not approved), APPROVED (for approved orders), DENIED (for rejected orders), SHIPPED_PART (for partially shipped orders), and COMPLETED (for fully shipped orders). These constants are used throughout the application to track and manage the order lifecycle, providing a consistent way to reference order statuses across different components of the process management system.

 **Code Landmarks**
- `Line 45`: The class comment documents the order state transition flow: pending→approved→shippedPart→completed or pending→denied
### processmanager/src/com/sun/j2ee/blueprints/processmanager/ejb/ProcessManagerEJB.java

ProcessManagerEJB implements a stateless session bean that manages the workflow process for order fulfillment in the Java Pet Store application. It provides methods to create, update, and query order processing status through a local EJB interface. Key functionality includes creating new workflow managers for orders, updating order status, retrieving status for specific orders, and querying orders by status. The class interacts with a ManagerLocal EJB through JNDI lookup. Important methods include createManager(), updateStatus(), getStatus(), and getOrdersByStatus(). The class follows standard EJB lifecycle methods with minimal implementation for stateless session beans.

 **Code Landmarks**
- `Line 67`: Creates workflow manager instances to track order processing through the system
- `Line 74`: Updates order status in the workflow process, enabling order tracking
- `Line 90`: Administrative query method to find all orders with a specific status
### processmanager/src/com/sun/j2ee/blueprints/processmanager/ejb/ProcessManagerLocal.java

ProcessManagerLocal defines a local EJB interface for managing order workflow processes in the Java Pet Store application. It provides methods to create, update, and query order workflow statuses. Key functionality includes creating new workflow processes for orders, updating order statuses, retrieving the status of a specific order, and querying orders by status. The interface extends javax.ejb.EJBLocalObject and includes four methods: createManager(), updateStatus(), getStatus(), and getOrdersByStatus().

 **Code Landmarks**
- `Line 46`: Interface extends EJBLocalObject to function as a local EJB component, enabling efficient in-container calls without remote overhead.
- `Line 56`: The createManager method establishes workflow tracking for new orders, demonstrating the application's event-driven architecture.
### processmanager/src/com/sun/j2ee/blueprints/processmanager/ejb/ProcessManagerLocalHome.java

ProcessManagerLocalHome defines the local home interface for the ProcessManager Entity EJB in the Java Pet Store application. This interface extends javax.ejb.EJBLocalHome and specifies the contract for creating instances of the ProcessManager bean within the same JVM. It declares a single create() method that returns a ProcessManagerLocal reference, potentially throwing CreateException if the creation fails. As part of the EJB architecture, this interface serves as the factory for obtaining local references to the ProcessManager component.

 **Code Landmarks**
- `Line 47`: Implements EJBLocalHome rather than EJBHome, indicating this is for local (same-JVM) access rather than remote clients
### processmanager/src/com/sun/j2ee/blueprints/processmanager/manager/ejb/ManagerEJB.java

ManagerEJB implements an entity bean that tracks order processing status within the process manager component. It provides container-managed persistence (CMP) for order tracking with fields for orderId and status. The class implements required EntityBean lifecycle methods including ejbCreate, ejbPostCreate, and context management methods. The bean serves as a persistent data store for order processing state, allowing the system to track order status throughout the fulfillment workflow.

 **Code Landmarks**
- `Line 53-54`: Uses Container-Managed Persistence (CMP) with abstract getter/setter methods that the EJB container implements at runtime
- `Line 57-61`: Implementation of ejbCreate returns null rather than the primary key, as the container handles key generation for CMP entity beans
### processmanager/src/com/sun/j2ee/blueprints/processmanager/manager/ejb/ManagerLocal.java

ManagerLocal defines a local interface for the Manager EJB in the process manager component. This interface extends EJBLocalObject and provides methods for accessing and modifying order information. It includes three key methods: getOrderId() to retrieve the order identifier, getStatus() to retrieve the current status of an order, and setStatus() to update the order status. The interface is part of the process management functionality in the Java Pet Store application, allowing local EJB clients to interact with order processing operations.

 **Code Landmarks**
- `Line 42`: This interface extends javax.ejb.EJBLocalObject, indicating it's designed for local (same JVM) EJB access rather than remote calls
### processmanager/src/com/sun/j2ee/blueprints/processmanager/manager/ejb/ManagerLocalHome.java

ManagerLocalHome interface defines the local home interface for the ProcessManager Entity EJB in the Java Pet Store application. It extends javax.ejb.EJBLocalHome and provides methods for creating and finding process manager entities. The interface includes create() method to instantiate a new process manager with an order ID and status, findByPrimaryKey() to locate a specific process manager by order ID, and findOrdersByStatus() to retrieve collections of orders with a particular status. These methods throw standard EJB exceptions like CreateException and FinderException when operations fail.

 **Code Landmarks**
- `Line 48`: This interface is part of the EJB local component model, which provides higher performance for components deployed in the same container
### processmanager/src/com/sun/j2ee/blueprints/processmanager/transitions/TransitionDelegate.java

TransitionDelegate interface defines the contract for classes that handle transitions in the process manager component. It requires implementing classes to provide two methods: setup() for initialization and doTransition(TransitionInfo) for executing the actual transition logic. Both methods can throw TransitionException when errors occur. This interface is part of the transitions package within the process manager component and serves as a foundation for implementing various transition handlers in the Java Pet Store application.
### processmanager/src/com/sun/j2ee/blueprints/processmanager/transitions/TransitionDelegateFactory.java

TransitionDelegateFactory implements a simple factory class for creating TransitionDelegate instances in the Java Pet Store application's process manager component. The class provides a method to instantiate TransitionDelegate objects dynamically based on a provided class name using Java reflection. It contains a default constructor and a single method getTransitionDelegate() that takes a class name as a string parameter, attempts to instantiate the class, and returns the created delegate object. If instantiation fails, it wraps the exception in a TransitionException.

 **Code Landmarks**
- `Line 49`: Uses Java reflection (Class.forName().newInstance()) to dynamically instantiate delegate classes based on their string class names
### processmanager/src/com/sun/j2ee/blueprints/processmanager/transitions/TransitionException.java

TransitionException implements a custom exception class for the process manager's transition system. It extends the standard Java Exception class with the ability to wrap another exception, providing a mechanism for chaining exceptions. The class offers three constructors: one with a message and wrapped exception, one with just a message, and one with just a wrapped exception. Key methods include getException() to retrieve the wrapped exception and getRootCause() to recursively find the original cause. The toString() method is overridden to display the wrapped exception's details when available.

 **Code Landmarks**
- `Line 87`: Implements recursive exception unwrapping to find the root cause of an error
- `Line 96`: Custom toString() implementation that delegates to the wrapped exception for better error reporting
### processmanager/src/com/sun/j2ee/blueprints/processmanager/transitions/TransitionInfo.java

TransitionInfo implements a class that encapsulates parameters passed to transition delegates in the process management system. It stores XML messages either as individual strings or as collections (batches). The class provides three constructors to initialize with either a single XML message, a batch of XML messages, or both. It offers two getter methods: getXMLMessage() to retrieve the single XML message and getXMLMessageBatch() to retrieve the collection of XML messages. This simple data container facilitates the transfer of XML-formatted data through the process manager's transition system.
### processmanager/src/ejb-jar.xml

This ejb-jar.xml deployment descriptor configures the ProcessManager component of the Java Pet Store application. It defines two Enterprise JavaBeans: a container-managed persistence (CMP) entity bean called ManagerEJB that tracks order status, and a stateless session bean called ProcessManagerEJB that provides the business interface for managing order processes. The entity bean stores order IDs and their statuses with finder methods for retrieving orders by status. The session bean provides methods for creating managers, updating status, and retrieving orders. The file also specifies method permissions (all methods are unchecked) and transaction attributes (all methods require transactions).

 **Code Landmarks**
- `Line 50`: Uses Container-Managed Persistence (CMP) 2.x for the entity bean, allowing the container to handle persistence operations automatically
- `Line 73`: Defines an EJB-QL query to find orders by status, demonstrating the object-oriented query language for entity beans
- `Line 90`: Local interfaces are used throughout, optimizing for in-container calls with reduced overhead
- `Line 105`: EJB reference linking connects the session bean to the entity bean through dependency injection
- `Line 115`: Assembly descriptor grants unchecked permissions to all methods, allowing any authenticated user to access them
### purchaseorder/src/build.xml

This build.xml file defines the build process for the PurchaseOrder component in the Java Pet Store application. It manages compilation, packaging, and documentation generation for the purchase order functionality. The script defines targets for initializing properties, compiling source code, creating EJB JAR files, generating client JARs, cleaning build artifacts, and producing documentation. It handles dependencies on other components like xmldocuments, servicelocator, address, contactinfo, creditcard, and lineitem. Key targets include 'core' (main build), 'compile', 'ejbjar', 'ejbclientjar', 'clean', and 'docs'.

 **Code Landmarks**
- `Line 116`: Creates separate client JAR by removing server-side EJB implementation classes, following J2EE best practices for deployment optimization
- `Line 75`: Builds component dependencies in correct order before main compilation, demonstrating modular architecture
- `Line 66`: Copies resources from source directory to maintain file structure in compiled output
- `Line 87`: Packages EJB deployment descriptor (ejb-jar.xml) into META-INF directory following J2EE specifications
### purchaseorder/src/com/sun/j2ee/blueprints/purchaseorder/ejb/JNDINames.java

JNDINames implements a utility class that centralizes the JNDI names used for Enterprise JavaBeans (EJBs) in the purchase order component. It defines three public static final String constants: CINFO_EJB for ContactInfo EJB, CARD_EJB for CreditCard EJB, and LI_EJB for LineItem EJB. Each constant stores the standardized JNDI lookup path using the java:comp/env/ejb/ namespace. The class serves as a single point of reference for JNDI names that must be synchronized with deployment descriptors.

 **Code Landmarks**
- `Line 44`: Centralizes JNDI names to ensure consistency between code and deployment descriptors
### purchaseorder/src/com/sun/j2ee/blueprints/purchaseorder/ejb/PurchaseOrder.java

PurchaseOrder implements a class representing an e-commerce purchase order with comprehensive XML serialization and deserialization capabilities. It stores order details including order ID, user information, shipping and billing addresses, credit card data, line items, and pricing. The class provides getter/setter methods for all properties and extensive XML handling methods (toXML, fromXML, toDOM, fromDOM) that support various input/output formats. It includes DTD validation, entity resolution, and locale handling. Important fields include orderId, userId, shippingInfo, billingInfo, creditCard, and lineItems. The class integrates with ContactInfo, CreditCard, and LineItem components to form a complete order representation.

 **Code Landmarks**
- `Line 170`: Implements flexible XML serialization with multiple output formats (Result, String) and optional entity catalog URL support
- `Line 204`: Provides static factory methods for XML deserialization with configurable validation
- `Line 262`: DOM conversion methods handle complex nested object structures with ContactInfo, CreditCard and LineItem components
- `Line 286`: Sophisticated DOM parsing with careful error handling and optional elements
### purchaseorder/src/com/sun/j2ee/blueprints/purchaseorder/ejb/PurchaseOrderEJB.java

PurchaseOrderEJB implements an entity bean that manages purchase order data in the Java Pet Store application. It maintains relationships with LineItemEJB (one-to-many), ContactInfoEJB and CreditCardEJB (one-to-one). The class provides methods to create purchase orders, add line items, and retrieve order data. Key functionality includes persisting purchase order details, managing CMR fields, and converting between entity beans and value objects. Important methods include ejbCreate(), ejbPostCreate(), addLineItem(), getAllItems(), and getData().

 **Code Landmarks**
- `Line 181`: Uses ServiceLocator pattern to obtain EJB home interfaces, reducing JNDI lookup code duplication
- `Line 194`: Demonstrates Container-Managed Relationships (CMR) with line items through the addLineItem method
- `Line 225`: Converts entity beans to value objects to allow data transfer across transaction boundaries
### purchaseorder/src/com/sun/j2ee/blueprints/purchaseorder/ejb/PurchaseOrderHelper.java

PurchaseOrderHelper implements a utility class for the purchase order component that processes invoice information received from suppliers. Its key functionality is updating LineItem fields for received invoices and determining if all items in a purchase order have been shipped. The class contains a single method, processInvoice(), which takes a PurchaseOrderLocal object and a Map of line item IDs, updates the quantity shipped for each line item, and returns a boolean indicating whether the entire order has been fulfilled.

 **Code Landmarks**
- `Line 83`: The method implements a two-pass algorithm: first updating shipped quantities, then verifying complete fulfillment
- `Line 77`: Uses a Map data structure to efficiently match incoming invoice line items with purchase order line items
### purchaseorder/src/com/sun/j2ee/blueprints/purchaseorder/ejb/PurchaseOrderLocal.java

PurchaseOrderLocal interface defines the local EJB interface for the PurchaseOrder entity bean in the Java Pet Store application. It extends EJBLocalObject and provides methods for accessing and manipulating purchase order data. The interface includes getters for purchase order fields (ID, user ID, email, date, locale, value), methods to manage relationships with ContactInfo and CreditCard entities, and functionality to handle LineItem collections. It also provides methods to retrieve all items and get the complete purchase order data through the getData() method.

 **Code Landmarks**
- `Line 48`: Interface extends EJBLocalObject, making it a local EJB component interface in the J2EE architecture
- `Line 60-71`: Demonstrates EJB relationship management through accessor methods for associated entities (ContactInfo, CreditCard)
- `Line 73-74`: Collection-based relationship management for LineItems showing one-to-many association pattern
### purchaseorder/src/com/sun/j2ee/blueprints/purchaseorder/ejb/PurchaseOrderLocalHome.java

PurchaseOrderLocalHome interface defines the local home interface for the PurchaseOrder EJB in the Java Pet Store application. It extends EJBLocalHome and provides methods for creating and finding purchase order entities in the database. The interface declares three key methods: create() for instantiating a new purchase order from a PurchaseOrder object, findByPrimaryKey() for retrieving a specific purchase order by its ID, and findPOBetweenDates() for querying purchase orders within a specified date range. This interface is part of the EJB component architecture and serves as the factory for PurchaseOrderLocal objects.

 **Code Landmarks**
- `Line 47`: The interface is explicitly marked as local-only with no remote interface, following EJB best practices for internal components
### purchaseorder/src/com/sun/j2ee/blueprints/purchaseorder/rsrc/schemas/PurchaseOrder.dtd

PurchaseOrder.dtd defines the XML document structure for purchase orders in the Java Pet Store application. It specifies the elements required for a complete purchase order, including OrderId, UserId, EmailId, OrderDate, shipping and billing information, total price, credit card details, and line items. The DTD imports external entity definitions for ContactInfo, LineItem, and CreditCard from other schema files. It establishes a hierarchical structure where a purchase order must contain at least one line item and defines all required elements for order processing.

 **Code Landmarks**
- `Line 35`: Defines a locale attribute with default value 'en_US' for internationalization support
- `Line 53-58`: Uses external entity references to incorporate related schemas, promoting modular design and reusability
### purchaseorder/src/ejb-jar-manifest.mf

This manifest file specifies the JAR dependencies for the purchase order Enterprise JavaBean (EJB) component in the Java Pet Store application. It defines the Class-Path attribute that lists required JAR files: xmldocuments.jar and servicelocator.jar. These dependencies are essential for the purchase order component to function properly within the J2EE container by ensuring access to XML document handling and service location functionality.
### purchaseorder/src/ejb-jar.xml

This ejb-jar.xml deployment descriptor configures the purchase order component of the Java Pet Store application. It defines five container-managed persistence (CMP) entity beans: PurchaseOrderEJB, ContactInfoEJB, AddressEJB, CreditCardEJB, and LineItemEJB. The file establishes relationships between these entities, including one-to-one relationships between PurchaseOrder and ContactInfo, ContactInfo and Address, PurchaseOrder and CreditCard, and a one-to-many relationship between PurchaseOrder and LineItem. Each entity bean has its fields, methods, and transaction attributes meticulously defined. The descriptor also specifies EJB queries, such as findPOBetweenDates for retrieving purchase orders within date ranges. Security permissions are set to 'unchecked' for all methods, allowing unrestricted access. The file comprehensively maps the data model and business logic for the purchase order processing functionality of the application.

 **Code Landmarks**
- `Line 60`: Defines the abstract schema name for container-managed persistence mapping to database tables
- `Line 138`: Implements cascade delete functionality to ensure proper cleanup of related entities when a parent entity is removed
- `Line 363`: Defines an EJB-QL query for finding purchase orders between specified dates
- `Line 453`: Establishes one-to-many relationship between purchase orders and line items with collection field type
### servicelocator/src/build.xml

This build.xml file defines the Ant build process for the ServiceLocator component in the Java Pet Store application. It establishes build properties, directory structures, and compilation targets. Key functionality includes compiling Java source files, creating JAR files, and cleaning build artifacts. The script defines targets for initialization, compilation, JAR creation, and cleanup. Important properties include servicelocator.home, servicelocator.build, servicelocator.src, j2ee.classpath, and servicelocator.client.jar. Main targets are init, compile, clientjar, clean, core, and all.

 **Code Landmarks**
- `Line 69`: Creates a classpath that combines the component's compiled classes with J2EE libraries
- `Line 80`: Compiles only files in the com/** package structure, limiting scope to component code
- `Line 97`: Defines 'core' as a composite target that runs both compile and clientjar targets
### servicelocator/src/com/sun/j2ee/blueprints/servicelocator/ServiceLocatorException.java

ServiceLocatorException implements a custom exception class for the service locator pattern in the Java Pet Store application. It extends the standard Java Exception class and provides functionality to wrap lower-level exceptions. The class offers three constructors for different initialization scenarios: with a message and wrapped exception, with only a message, or with only a wrapped exception. Key methods include getException() to retrieve the wrapped exception and getRootCause() to recursively find the root cause exception. The class also overrides toString() to properly display the exception chain.

 **Code Landmarks**
- `Line 89`: The getRootCause() method uses recursion to traverse the exception chain to find the original cause of the error.
- `Line 98`: The toString() method delegates to the wrapped exception when possible, maintaining the exception chain's integrity in string representation.
### servicelocator/src/com/sun/j2ee/blueprints/servicelocator/ejb/ServiceLocator.java

ServiceLocator implements the Service Locator pattern to provide centralized JNDI lookup services for J2EE resources. It offers methods to retrieve EJB homes (both local and remote), JMS resources (queues, topics, and their connection factories), DataSources, and environment entries (URLs, strings, booleans). The class encapsulates JNDI context creation and error handling, wrapping exceptions in ServiceLocatorException. Key methods include getLocalHome(), getRemoteHome(), getQueue(), getTopic(), getDataSource(), and various environment entry getters. The class maintains an InitialContext instance for performing lookups and provides a singleton-like static instance variable.

 **Code Landmarks**
- `Line 46`: Implementation of the Service Locator design pattern for J2EE resource access
- `Line 82`: Uses PortableRemoteObject.narrow() for type-safe EJB remote interface casting
- `Line 49`: Class maintains a static instance variable 'me' but doesn't implement full singleton pattern
### servicelocator/src/com/sun/j2ee/blueprints/servicelocator/web/ServiceLocator.java

ServiceLocator implements the Service Locator design pattern for web tier applications in the Java Pet Store. It provides centralized access to J2EE resources like EJB homes, JMS destinations, DataSources, and environment entries. The class uses both singleton and caching strategies to improve performance by storing looked-up resources in a synchronized HashMap. Key functionality includes methods for retrieving local and remote EJB homes, JMS connection factories, queues, topics, DataSources, and environment entries. The class handles all JNDI lookups and wraps exceptions in ServiceLocatorException. Important methods include getInstance(), getLocalHome(), getRemoteHome(), getQueue(), getTopic(), getDataSource(), and various getters for environment entries.

 **Code Landmarks**
- `Line 52`: Implements singleton pattern with static initialization block to ensure single instance
- `Line 71`: Uses Collections.synchronizedMap to make the cache thread-safe
- `Line 91`: Caching strategy improves performance by storing previously looked-up resources
- `Line 116`: Uses PortableRemoteObject.narrow for type-safe EJB remote home lookups
### signon/src/build.xml

This build.xml file defines the Ant build process for the SignOn component of Java Pet Store. It establishes targets for compiling Java classes, packaging EJB JAR files (both implementation and client), generating documentation, and cleaning build artifacts. The script sets up properties for build directories, source locations, and classpaths, then defines targets that compile source code, create EJB JARs with proper metadata, generate Javadoc documentation, and perform cleanup operations. Key targets include 'compile', 'ejbjar', 'ejbclientjar', 'clean', 'docs', and the default target 'core'.

 **Code Landmarks**
- `Line 73`: Creates separate client JAR without implementation classes for proper EJB deployment architecture
- `Line 45`: Imports user-specific properties first, allowing for developer customization of build settings
- `Line 61`: Defines J2EE classpath including locale directory for internationalization support
### signon/src/com/sun/j2ee/blueprints/signon/ejb/SignOnEJB.java

SignOnEJB implements a stateless session bean that handles user authentication and creation in the Java Pet Store application. It provides two key business methods: authenticate() to verify user credentials by checking username and password against stored values, and createUser() to register new users. The EJB connects to a UserLocal EJB through JNDI lookup to perform these operations. The class implements standard EJB lifecycle methods (ejbCreate, ejbRemove, ejbActivate, ejbPassivate) with minimal implementation as appropriate for a stateless session bean.

 **Code Landmarks**
- `Line 65`: Authentication method returns false on FinderException rather than propagating the exception, providing a clean failure path
- `Line 56`: EJB initialization uses JNDI lookup to access the User EJB, demonstrating standard J2EE component wiring
### signon/src/com/sun/j2ee/blueprints/signon/ejb/SignOnLocal.java

SignOnLocal interface defines the local EJB contract for the sign-on component in the Java Pet Store application. It extends javax.ejb.EJBLocalObject and provides two key methods: authenticate() to validate user credentials by checking if a username/password combination is valid, and createUser() to register new users in the system. The interface serves as part of the authentication mechanism, allowing other components to verify user identities and create new user accounts through local EJB calls.

 **Code Landmarks**
- `Line 47`: Interface extends EJBLocalObject indicating it's designed for efficient local container calls rather than remote access
### signon/src/com/sun/j2ee/blueprints/signon/ejb/SignOnLocalHome.java

SignOnLocalHome interface defines the local home interface for the SignOn Entity EJB in the Java Pet Store application's authentication component. It extends javax.ejb.EJBLocalHome and provides a single create method that returns a SignOnLocal object. This interface is part of the sign-on mechanism that handles user authentication within the application. The create method throws CreateException if there are issues during entity bean creation. The interface is designed to work within the local container environment, avoiding remote method invocation overhead.

 **Code Landmarks**
- `Line 46`: Uses EJBLocalHome interface rather than remote interface, indicating this component is designed for local container access only
### signon/src/com/sun/j2ee/blueprints/signon/user/ejb/UserEJB.java

UserEJB implements an Entity Bean for user authentication in the Java Pet Store application. It manages user credentials with container-managed persistence (CMP) fields for username and password. The class provides methods for creating users with validation logic that enforces maximum length constraints and character restrictions. It includes a matchPassword method to verify user credentials and implements standard EntityBean lifecycle methods required by the EJB specification. Key elements include ejbCreate for user creation with validation, abstract getter/setter methods for CMP fields, and the matchPassword business method.

 **Code Landmarks**
- `Line 59`: Implements validation logic for username and password during user creation
- `Line 79`: Simple password matching method that compares plain text passwords
### signon/src/com/sun/j2ee/blueprints/signon/user/ejb/UserLocal.java

UserLocal interface defines the local EJB contract for user authentication in the Java Pet Store's sign-on component. It provides methods to retrieve and modify user credentials, including getUserName(), getPassword(), setPassword(), and matchPassword() for password validation. The interface also defines constants for maximum username and password lengths (25 characters each). As part of the EJB architecture, it extends javax.ejb.EJBLocalObject, making it accessible to other components within the same JVM for user authentication operations.

 **Code Landmarks**
- `Line 46`: Interface extends javax.ejb.EJBLocalObject, making it a local EJB component interface that can only be accessed within the same JVM
### signon/src/com/sun/j2ee/blueprints/signon/user/ejb/UserLocalHome.java

UserLocalHome interface defines the local home interface for the User Entity EJB in the Java Pet Store's signon component. It extends javax.ejb.EJBLocalHome and provides methods for creating and finding User entities in the database. The interface declares three key methods: create() for creating a new user with username and password, findByPrimaryKey() for retrieving a specific user by username, and findAllUsers() for retrieving all users. These methods are essential for managing user authentication data in the application.

 **Code Landmarks**
- `Line 44`: This interface is part of the EJB 2.0 local interface pattern, providing more efficient access than remote interfaces for components within the same JVM.
### signon/src/com/sun/j2ee/blueprints/signon/web/CreateUserServlet.java

CreateUserServlet implements a servlet that processes user registration requests in the Java Pet Store application. It handles HTTP POST requests containing username and password parameters, creates new user accounts through the SignOnLocal EJB, and manages authentication flow. The servlet extracts form data, calls the EJB's createUser method, and redirects users to their originally requested URL upon successful registration or to an error page if creation fails. Key methods include doPost() for handling form submissions and getSignOnEjb() for obtaining the EJB reference through JNDI lookup.

 **Code Landmarks**
- `Line 57`: Uses SignOnFilter.ORIGINAL_URL from session to redirect users back to their originally requested page after successful registration
- `Line 58`: Sets a Boolean attribute in session to indicate successful authentication state
- `Line 75`: Uses JNDI lookup to obtain EJB reference through java:comp/env context
### signon/src/com/sun/j2ee/blueprints/signon/web/ProtectedResource.java

ProtectedResource implements a serializable class that represents a protected resource in the Java Pet Store's sign-on component. It stores essential access control information including the resource name, URL pattern for matching requests, and a list of roles that are authorized to access the resource. The class provides getter methods to retrieve these properties and includes a toString() method for debugging. Key fields include name (String), urlPattern (String), and roles (ArrayList).

 **Code Landmarks**
- `Line 50`: The class implements Serializable to support state persistence across HTTP sessions or for remote method invocation
### signon/src/com/sun/j2ee/blueprints/signon/web/SignOnDAO.java

SignOnDAO implements a data access object that loads and parses XML configuration for the sign-on component. It extracts login page paths, error page paths, and protected web resources with their associated role-based access controls. The class provides methods to retrieve sign-on pages and a map of protected resources. Key functionality includes parsing XML documents using JAXP, extracting tag values, and building a security constraint model. Important elements include the constructor taking a configuration URL, getSignOnPage(), getSignOnErrorPage(), getProtectedResources(), and helper methods for XML parsing.

 **Code Landmarks**
- `Line 76`: Uses DOM parsing to extract security configuration from XML files
- `Line 101`: HashMap stores protected resources indexed by resource name for efficient lookup
- `Line 123`: Builds a security model by extracting URL patterns and role-based access controls
### signon/src/com/sun/j2ee/blueprints/signon/web/SignOnFilter.java

SignOnFilter implements a servlet filter that controls access to protected resources by validating user credentials. It intercepts HTTP requests, checks if the requested resource requires authentication, and redirects unauthenticated users to a sign-on page. Key functionality includes validating user credentials against a SignOn EJB, managing user sessions, handling cookies for remembering usernames, and configuring protected resources via XML. Important elements include constants for request parameters, session attributes, methods doFilter() for request interception, validateSignOn() for credential verification, and getSignOnEJB() for EJB lookup.

 **Code Landmarks**
- `Line 75`: Uses XML configuration file to define protected resources and sign-on pages
- `Line 107`: Implements URL pattern matching to determine which resources require authentication
- `Line 149`: Manages persistent user authentication with cookies that expire after one month
- `Line 175`: Uses EJB for authentication logic, separating presentation from business logic
### signon/src/ejb-jar.xml

This ejb-jar.xml file defines the Enterprise JavaBeans deployment descriptor for the sign-on component of the Java Pet Store application. It configures two EJBs: a container-managed persistence UserEJB entity bean that stores username and password data, and a stateless SignOnEJB session bean that provides authentication services. The file specifies local interfaces, container transaction attributes, method permissions, and EJB relationships. The UserEJB includes fields for username (primary key) and password, while the SignOnEJB provides methods for user authentication and creation. All methods are configured with Required transaction attributes and unchecked security permissions.

 **Code Landmarks**
- `Line 48`: Defines UserEJB as a container-managed persistence entity bean with String primary key
- `Line 77`: EJBQL query implementation for finding all users in the system
- `Line 91`: Local EJB reference linking SignOnEJB to UserEJB for authentication operations
- `Line 102`: Method permissions section grants unchecked access to all authentication methods
- `Line 262`: Container transaction configuration enforcing Required transaction attribute for authentication operations
### supplierpo/src/build.xml

This build.xml file defines the Ant build process for the SupplierPO component, which handles supplier purchase orders in the Java Pet Store application. It establishes targets for initialization, compilation, EJB JAR creation, client JAR creation, documentation generation, and cleanup. The script manages dependencies on other components like XMLDocuments, ServiceLocator, ContactInfo, Address, and LineItem. Key targets include 'init' (setting properties), 'compile', 'ejbjar', 'ejbclientjar', 'clean', 'components' (building dependencies), 'docs' (generating JavaDocs), and 'core' (main build target).

 **Code Landmarks**
- `Line 104`: Copies classes from dependent components into the build directory rather than referencing them as external dependencies
- `Line 130`: Creates separate client JAR by selectively excluding server-side implementation classes
- `Line 172`: Builds documentation that includes source from multiple component directories
### supplierpo/src/com/sun/j2ee/blueprints/supplierpo/ejb/JNDINames.java

JNDINames is a utility class that centralizes the JNDI naming constants used throughout the supplier purchase order system. It defines static final String constants for various Enterprise JavaBeans (EJBs) including ContactInfo, Address, LineItem, and SupplierOrder. The class has a private constructor to prevent instantiation since it only serves as a container for constants. These JNDI names are critical reference points that must match the corresponding entries in deployment descriptors, ensuring consistent lookup of EJB resources across the application.

 **Code Landmarks**
- `Line 47`: Private constructor prevents instantiation of this utility class that only contains constants
- `Line 51-56`: All JNDI names use the java:comp/env/ namespace prefix, following J2EE best practices for portable EJB references
### supplierpo/src/com/sun/j2ee/blueprints/supplierpo/ejb/OrderStatusNames.java

OrderStatusNames implements a utility class that centralizes the definition of constants representing the possible states of a supplier purchase order in the system. It defines four string constants: PENDING (for orders placed but not approved), APPROVED (for orders that have been approved), DENIED (for rejected orders), and COMPLETED (for fulfilled orders). The class documentation explains the typical order state transitions: orders start as pending, then either move to approved and eventually completed, or are denied.
### supplierpo/src/com/sun/j2ee/blueprints/supplierpo/ejb/SupplierOrder.java

SupplierOrder implements a data model for purchase orders sent to suppliers in the Pet Store application. It stores order information including order ID, date, shipping details, and line items. The class provides comprehensive XML serialization and deserialization capabilities using DOM and SAX, with validation against a DTD schema. Key functionality includes creating, manipulating, and converting supplier orders between object and XML representations. Important methods include toXML(), fromXML(), toDOM(), and fromDOM() for XML handling, along with standard getters and setters for order properties. The class uses ContactInfo and LineItem classes to represent shipping information and ordered items respectively.

 **Code Landmarks**
- `Line 66`: Uses SimpleDateFormat for consistent date formatting in XML serialization
- `Line 141`: Implements multiple XML serialization methods with different output targets (Result, String)
- `Line 187`: Provides static factory methods for creating SupplierOrder objects from XML sources
- `Line 226`: DOM manipulation for XML serialization without relying on JAXB
- `Line 243`: Complex DOM parsing logic to reconstruct object hierarchy from XML elements
### supplierpo/src/com/sun/j2ee/blueprints/supplierpo/ejb/SupplierOrderEJB.java

SupplierOrderEJB implements an entity bean that manages supplier purchase orders in the Java Pet Store application. It maintains relationships with LineItemEJB (one-to-many) and ContactInfoEJB (one-to-one). The class provides methods for accessing and modifying purchase order details including ID, date, status, contact information, and line items. Key functionality includes creating purchase orders, adding line items, and retrieving order data. Important methods include ejbCreate(), ejbPostCreate(), addLineItem(), getAllItems(), and getData(). The class also implements standard EntityBean lifecycle methods.

 **Code Landmarks**
- `Line 156`: Creates and manages complex relationships between entities during ejbPostCreate, establishing connections to ContactInfo and LineItem entities
- `Line 184`: getAllItems() method provides a workaround for web tier access to CMR fields without transactions
- `Line 199`: getData() method implements a transfer object pattern to expose entity data to clients
### supplierpo/src/com/sun/j2ee/blueprints/supplierpo/ejb/SupplierOrderLocal.java

SupplierOrderLocal interface defines the local EJB interface for managing supplier purchase orders in the Java Pet Store application. It extends EJBLocalObject and provides methods to access and modify purchase order data including ID, date, status, shipping information, and line items. The interface includes getters and setters for purchase order fields, methods to manage associated ContactInfo objects, and functionality to add and retrieve line items. Key methods include getPoId(), setPoStatus(), getContactInfo(), addLineItem(), getData(), and getAllItems(), enabling comprehensive management of supplier orders within the application.

 **Code Landmarks**
- `Line 53`: Interface extends EJBLocalObject, making it a local EJB component interface accessible only within the same container
- `Line 67`: Uses ContactInfoLocal interface showing composition relationship between supplier orders and contact information
- `Line 72`: Collection return type for line items demonstrates a one-to-many relationship pattern
### supplierpo/src/com/sun/j2ee/blueprints/supplierpo/ejb/SupplierOrderLocalHome.java

SupplierOrderLocalHome interface defines the local home interface for the SupplierOrder EJB in the Java Pet Store application. It extends EJBLocalHome and provides methods for creating and finding supplier orders in the system. The interface includes three key methods: create() for creating new supplier orders, findByPrimaryKey() for retrieving orders by their ID, and findOrdersByStatus() for querying orders based on their current status. This interface is part of the supplier purchase order component and has no remote interface.

 **Code Landmarks**
- `Line 45`: This EJB is explicitly designed with only a local interface, indicating it's meant for internal component access rather than remote client access.
### supplierpo/src/com/sun/j2ee/blueprints/supplierpo/rsrc/schemas/SupplierOrder.dtd

SupplierOrder.dtd defines the XML document structure for supplier purchase orders in the Java Pet Store application. It specifies the elements and their relationships for representing orders sent to suppliers. The DTD declares four main elements: SupplierOrder (the root element containing order details), OrderId (order identifier), OrderDate (when order was placed), and ShippingInfo (delivery information). It also imports external DTD definitions for ContactInfo and LineItem elements through entity references. The schema ensures that supplier orders follow a consistent structure with required order metadata and one or more line items.

 **Code Landmarks**
- `Line 38`: Uses entity references to import external DTD definitions, promoting modular schema design
- `Line 36`: Requires at least one LineItem element with the '+' occurrence indicator, ensuring valid orders always contain items
### supplierpo/src/ejb-jar-manifest.mf

This manifest file defines the configuration for the supplierpo EJB JAR component. It specifies the manifest version (1.0) and declares dependencies on two external JAR files: xmldocuments.jar and servicelocator.jar. These dependencies are essential for the supplierpo component to function properly within the Java Pet Store application.
### supplierpo/src/ejb-jar.xml

This ejb-jar.xml deployment descriptor defines the Enterprise JavaBeans (EJBs) for the supplier purchase order component of Java Pet Store. It configures four main entity beans: SupplierOrderEJB, ContactInfoEJB, AddressEJB, and LineItemEJB. The file establishes container-managed persistence (CMP) for these beans, defining their fields, relationships, and transaction attributes. Key relationships include one-to-one mappings between SupplierOrder and ContactInfo, ContactInfo and Address, and a one-to-many relationship between SupplierOrder and LineItem. The descriptor also specifies method permissions (all unchecked) and detailed transaction attributes for each bean's methods. The deployment descriptor enables finding orders by status through an EJB-QL query and implements cascade delete for dependent relationships.

 **Code Landmarks**
- `Line 83`: One-to-one relationship between SupplierOrder and ContactInfo with cascade-delete, ensuring dependent objects are removed when parent is deleted.
- `Line 12`: EJB-QL query implementation for finding orders by status, demonstrating container query language usage.
- `Line 209`: Relationship definitions show how complex object graphs are maintained through container-managed relationships.
### uidgen/src/build.xml

This build.xml file defines the build process for the UniqueIdGenerator component in the Java Pet Store application. It configures targets for compiling Java classes, creating EJB JAR files, generating documentation, and cleaning build artifacts. The script defines properties for directory paths, sets up classpaths, and implements targets for compiling source code, creating both the main EJB JAR and a client JAR (which excludes implementation classes). Key targets include init, compile, ejbjar, ejbclientjar, clean, docs, and core, with the default target being 'core' which builds the complete component.

 **Code Landmarks**
- `Line 96`: Creates separate client JAR by excluding implementation classes, following EJB best practices for deployment
- `Line 76`: Builds the EJB JAR with proper META-INF structure required for J2EE deployment
- `Line 46`: Loads user-specific properties first, allowing for developer-specific configurations
### uidgen/src/com/sun/j2ee/blueprints/uidgen/counter/ejb/CounterEJB.java

CounterEJB implements an entity bean that provides a unique identifier generation service. It maintains a counter associated with a name prefix, incrementing the counter each time a new ID is requested. The class defines container-managed persistence (CMP) fields for the counter value and name, along with standard EJB lifecycle methods. The key business method getNextValue() increments the counter and returns a concatenated string of the name and new counter value. The implementation follows the EJB specification with required methods for entity context management and lifecycle events.

 **Code Landmarks**
- `Line 55`: Uses Container-Managed Persistence (CMP) with abstract getter/setter methods that the EJB container implements at runtime
- `Line 67`: Business method getNextValue() provides atomic counter increment and string concatenation for unique ID generation
- `Line 61`: ejbCreate initializes counter to zero, establishing the starting point for sequence generation
### uidgen/src/com/sun/j2ee/blueprints/uidgen/counter/ejb/CounterLocal.java

CounterLocal defines a local EJB interface for the counter component in the UID generation system. This interface extends javax.ejb.EJBLocalObject and provides a single method getNextValue() that returns a String representing the next sequential value from the counter. The interface is part of the unique identifier generation subsystem in the Java Pet Store application, allowing other components to obtain unique sequential values for various application needs like order IDs or customer IDs.

 **Code Landmarks**
- `Line 39`: Interface extends javax.ejb.EJBLocalObject, making it a local EJB component interface that can only be accessed within the same JVM
### uidgen/src/com/sun/j2ee/blueprints/uidgen/counter/ejb/CounterLocalHome.java

CounterLocalHome interface defines the local EJB home interface for the Counter component in the unique ID generation system. It extends javax.ejb.EJBLocalHome and provides methods for creating and finding Counter EJB instances. The interface declares two methods: create() for instantiating a new Counter with a specified name, and findByPrimaryKey() for retrieving an existing Counter by its name. These methods throw standard EJB exceptions (CreateException and FinderException) when operations fail. This interface is part of the uidgen component which likely provides unique identifier generation services for the Java Pet Store application.
### uidgen/src/com/sun/j2ee/blueprints/uidgen/ejb/UniqueIdGeneratorEJB.java

UniqueIdGeneratorEJB implements a stateless session bean that provides unique ID generation functionality for the Java Pet Store application. It manages the creation of sequential, prefixed identifiers by delegating to a Counter EJB component. The class initializes a connection to the Counter EJB during creation and exposes a getUniqueId method that accepts a string prefix parameter. Key methods include ejbCreate() for initialization, getUniqueId() as the main business method, and getCounter() which either retrieves an existing counter or creates a new one based on the provided name. The implementation follows EJB lifecycle patterns with standard callback methods.

 **Code Landmarks**
- `Line 56`: Uses a pattern of trying to find an existing counter first, then creating one if not found
- `Line 45`: Implements a stateless session bean that encapsulates counter management behind a simple interface
### uidgen/src/com/sun/j2ee/blueprints/uidgen/ejb/UniqueIdGeneratorLocal.java

UniqueIdGeneratorLocal defines a local interface for an Enterprise JavaBean (EJB) that generates unique identifiers. This interface extends javax.ejb.EJBLocalObject, making it accessible only within the same JVM. It declares a single method getUniqueId() that takes a string prefix parameter and returns a unique identifier string. The interface is part of the unique ID generation component in the Java Pet Store application, providing a way for other components to obtain unique identifiers for various entities like orders or customers.

 **Code Landmarks**
- `Line 43`: The interface extends javax.ejb.EJBLocalObject, indicating it's designed for local (same-JVM) access rather than remote calls, optimizing performance for internal component communication.
### uidgen/src/com/sun/j2ee/blueprints/uidgen/ejb/UniqueIdGeneratorLocalHome.java

UniqueIdGeneratorLocalHome defines the local home interface for the UniqueIdGenerator Enterprise JavaBean component in the Java Pet Store application. This interface extends javax.ejb.EJBLocalHome and specifies a single create() method that returns a UniqueIdGeneratorLocal object. The create method throws CreateException if the bean creation fails. This interface is part of the unique ID generation component that likely provides unique identifiers for various entities in the application, such as orders or customers.

 **Code Landmarks**
- `Line 40`: Uses EJBLocalHome interface rather than remote interface, indicating this component is designed for local container access only
### uidgen/src/ejb-jar.xml

This ejb-jar.xml deployment descriptor configures two Enterprise JavaBeans for unique ID generation in the Pet Store application. It defines a container-managed persistent entity bean (CounterEJB) that stores and increments counter values, and a stateless session bean (UniqueIdGeneratorEJB) that uses CounterEJB to generate unique identifiers. The file specifies bean interfaces, classes, persistence details, security settings, and transaction attributes. It establishes a local EJB reference from UniqueIdGeneratorEJB to CounterEJB and grants unchecked permissions for all bean methods.

 **Code Landmarks**
- `Line 42`: Defines CounterEJB as a container-managed persistence entity bean with String primary key and counter/name fields
- `Line 69`: Configures UniqueIdGeneratorEJB as a stateless session bean with a local reference to CounterEJB
- `Line 81`: Uses ejb-link element to establish the relationship between UniqueIdGeneratorEJB and CounterEJB
- `Line 91`: Assembly descriptor grants unchecked permissions to all methods, allowing any authenticated user to access them
- `Line 198`: Transaction attributes set to 'Required' for all business methods, ensuring ACID properties during ID generation
### util/tracer/src/build.xml

This build.xml file defines the Ant build process for the tracer utility component in Java Pet Store. It establishes build properties, directory structures, and compilation targets. The script defines several targets including 'init' (sets up properties), 'compile' (compiles Java source files), 'clientjar' (packages compiled classes into tracer.jar), 'clean' (removes build artifacts), and 'core'/'all' (main build targets). Key properties include paths for source code, build directories, and dependencies on J2EE libraries.

 **Code Landmarks**
- `Line 60`: Defines the classpath that includes J2EE libraries, showing the component's enterprise application integration
- `Line 71`: Compilation target specifically includes only com/** packages, suggesting a focused component structure
### util/tracer/src/com/sun/j2ee/blueprints/util/tracer/Debug.java

Debug implements a helper class for convenient debug statement printing in the Java Pet Store application. It provides static methods to output debug messages to standard error or standard output streams, with output controlled by a single debuggingOn flag. The class includes methods for printing regular messages (print, println) and exception/throwable information with optional context messages. All output is conditionally displayed based on the debuggingOn boolean flag, which is set to false by default, allowing for easy enabling/disabling of debug output throughout the application.

 **Code Landmarks**
- `Line 43`: Simple boolean flag controls all debug output application-wide
- `Line 65`: Method overloading provides flexible error reporting with optional context messages
### xmldocuments/src/build.xml

This build.xml file defines the build process for the xmldocuments component in the Java Pet Store application. It establishes targets for compiling Java classes, creating JAR files, and cleaning build artifacts. The script sets up properties for directory paths, defines classpath dependencies including J2EE libraries, and implements tasks for compiling source code, copying resources, and packaging the component. Key targets include init, compile, clientjar, clean, core, and all, with core being the default target that compiles code and creates the xmldocuments.jar file.

 **Code Landmarks**
- `Line 46`: Includes user-specific properties first, allowing for developer customization
- `Line 73`: Defines J2EE classpath with both JAR files and locale resources
- `Line 85`: Copies resource files from multiple source locations to maintain structure in build
### xmldocuments/src/com/sun/j2ee/blueprints/xmldocuments/ChangedOrder.java

ChangedOrder implements a class that represents an order that has been updated with a new status. It encapsulates order ID and status information, providing methods for XML serialization (toDOM) and deserialization (fromDOM). The class uses XMLDocumentUtils for DOM manipulation and defines constants for XML element names. Key methods include constructors, getters for order ID and status, and methods to convert between ChangedOrder objects and DOM structures. The fromDOM static factory method parses XML nodes to create ChangedOrder instances, while toDOM methods generate XML representations of the order data.

 **Code Landmarks**
- `Line 73`: Static factory method pattern used for XML deserialization instead of constructor
- `Line 59`: Two-level DOM conversion with both document-level and node-level toDOM methods
### xmldocuments/src/com/sun/j2ee/blueprints/xmldocuments/CustomEntityResolver.java

CustomEntityResolver implements an EntityResolver that resolves XML entities by mapping URIs to local resources. It loads entity mappings from a properties file or URL, allowing XML parsers to locate DTDs and schemas locally rather than fetching them remotely. The resolver can chain to a parent resolver and attempts multiple resolution strategies: first mapping through the catalog, then trying direct URL access, and finally falling back to resource loading. Key methods include resolveEntity(), mapEntityURI(), and resolveEntityFromURL(). Important variables include entityCatalog (Properties) and ENTITY_CATALOG (path to default mappings).

 **Code Landmarks**
- `Line 47`: Uses a properties file to map public entity URIs to local resources
- `Line 108`: Implements a fallback chain for entity resolution with multiple strategies
- `Line 168`: Supports chaining to a parent resolver for delegation
### xmldocuments/src/com/sun/j2ee/blueprints/xmldocuments/OrderApproval.java

OrderApproval implements a class that manages collections of changed orders for approval processing in the Pet Store application. It provides functionality for creating, manipulating, and serializing/deserializing order approval data to and from XML format. The class supports XML validation against a DTD, DOM conversion, and various XML input/output methods. Key methods include toXML(), fromXML(), toDOM(), fromDOM(), addOrder(), and getOrdersList(). Important variables include orderList (ArrayList), DTD_PUBLIC_ID, DTD_SYSTEM_ID, and XML_ORDERAPPROVAL constant.

 **Code Landmarks**
- `Line 77`: Supports multiple XML serialization methods with optional entity catalog URL parameter
- `Line 116`: Implements static factory methods for creating OrderApproval objects from various XML sources
- `Line 149`: DOM conversion methods enable integration with XML processing libraries
### xmldocuments/src/com/sun/j2ee/blueprints/xmldocuments/XMLDocumentEditor.java

XMLDocumentEditor defines an interface for XML document manipulation with methods to set, get, and copy XML documents. It includes a DefaultXDE implementation class that provides basic functionality for validation settings, entity catalog URL management, and XML Schema Definition (XSD) support. The interface declares methods for document operations that throw XMLDocumentException when errors occur. DefaultXDE implements these methods with placeholder implementations that throw UnsupportedOperationException, suggesting that concrete implementations should override these methods. Key methods include setDocument(), getDocument(), copyDocument(), setValidating(), and setEntityCatalogURL().

 **Code Landmarks**
- `Line 43`: Uses JAXP (Java API for XML Processing) transformation interfaces for XML document manipulation
- `Line 50`: Provides a default implementation class within the interface definition, which is an interesting design pattern
### xmldocuments/src/com/sun/j2ee/blueprints/xmldocuments/XMLDocumentEditorFactory.java

XMLDocumentEditorFactory implements a factory pattern for creating XMLDocumentEditor instances based on schema URIs. It maintains a catalog of schema-to-editor mappings loaded from a URL. The class provides methods to instantiate editors either by schema URI lookup or direct class name specification. Key functionality includes loading editor mappings from a URL and creating editor instances through reflection. Important methods include getXDE(String) which retrieves an editor for a specific schema, and createXDE(String) which instantiates an editor from a class name. The class handles errors by throwing XMLDocumentException.

 **Code Landmarks**
- `Line 52`: Uses Java reflection to dynamically instantiate editor classes based on configuration
- `Line 43`: Implements a catalog-based lookup system using Properties to map schema URIs to implementation classes
### xmldocuments/src/com/sun/j2ee/blueprints/xmldocuments/XMLDocumentException.java

XMLDocumentException implements a custom exception class for XML document processing in the Java Pet Store application. It extends the standard Java Exception class with the ability to wrap another exception as its root cause. The class provides three constructors for different initialization scenarios: with a message and wrapped exception, with only a message, or with only a wrapped exception. Key methods include getException() to retrieve the wrapped exception and getRootCause() to recursively find the original cause of the error. The class also overrides toString() to properly display the exception chain.

 **Code Landmarks**
- `Line 87`: Implements recursive root cause detection by checking if the wrapped exception is also an XMLDocumentException
- `Line 96`: toString() implementation delegates to the wrapped exception for consistent error reporting
### xmldocuments/src/com/sun/j2ee/blueprints/xmldocuments/XMLDocumentUtils.java

XMLDocumentUtils is a utility class providing methods for XML document manipulation in the Java Pet Store application. It offers functionality for parsing XML documents, validating against DTDs or XML schemas, transforming documents, and manipulating DOM elements. The class includes methods for retrieving attributes and child elements from DOM nodes, creating new elements, serializing documents to XML, and deserializing XML to DOM documents. Key methods include getAttribute/getChild methods for DOM navigation, toXML/fromXML for serialization/deserialization, and createParser/createDocument/createTransformer for XML processing infrastructure. The class also defines constants for XML namespaces, schema locations, and encoding defaults, and includes utility methods for locale handling.

 **Code Landmarks**
- `Line 68`: Comprehensive DOM navigation methods with optional parameters that throw XMLDocumentException when required elements are missing
- `Line 293`: Serialization method supporting both DTD and XSD validation approaches
- `Line 348`: Custom entity resolver integration for handling DTD and schema references
- `Line 408`: Error handler implementation that logs warnings and errors but only throws exceptions for fatal errors
- `Line 526`: Locale parsing utility that handles language_country_variant format strings
### xmldocuments/src/com/sun/j2ee/blueprints/xmldocuments/rsrc/EntityCatalog.properties

EntityCatalog.properties serves as a mapping configuration file for XML document type definitions (DTDs) and XML schemas (XSDs) used throughout the Java Pet Store application. It defines the relationship between public identifiers or URIs and their corresponding physical file locations in the system. The file organizes mappings into three sections: old DTDs, new Trading Partner Agreement (TPA) DTDs and XSDs, and new component/composite DTDs. These mappings enable XML parsers to locate the correct schema definitions when validating XML documents used for purchase orders, supplier orders, invoices, line items, and other business documents within the application.

 **Code Landmarks**
- `Line 9`: Transition from old DTDs to new TPA-based XML schemas shows evolution of the application's data interchange format
- `Line 14`: Use of both DTD and XSD formats for the same document types indicates support for multiple XML validation approaches
- `Line 19`: Component/Composite DTDs section demonstrates modular document structure with separate schemas for reusable components like Address and ContactInfo
### xmldocuments/src/com/sun/j2ee/blueprints/xmldocuments/rsrc/schemas/TPAInvoice.dtd

TPAInvoice.dtd defines the Document Type Definition for XML invoice documents in the Pet Store application. It establishes the structure and validation rules for invoice data exchanged between system components. The DTD defines elements including Invoice (root element), OrderId, UserId, OrderDate, ShippingDate, and LineItems. Each element has namespace attributes with a fixed URI pointing to blueprints.j2ee.sun.com/TPAInvoice. The schema imports LineItem definitions from TPALineItem.dtd using an external entity reference, allowing for modular document structure definition.

 **Code Landmarks**
- `Line 33`: Defines a namespace attribute entity that's reused across all elements for consistent XML namespace declaration
- `Line 36-40`: Root element definition includes locale attribute with default value 'en_US', supporting internationalization
- `Line 72`: Uses external entity reference to import LineItem definitions from separate DTD file, enabling modular schema design
### xmldocuments/src/com/sun/j2ee/blueprints/xmldocuments/rsrc/schemas/TPAInvoice.dtd

TPAInvoice.dtd defines the Document Type Definition for XML invoice documents in the Pet Store application. It establishes the structure and validation rules for invoice data exchanged between system components. The DTD defines elements including Invoice (root element), OrderId, UserId, OrderDate, ShippingDate, and LineItems. Each element has namespace attributes with a fixed URI pointing to blueprints.j2ee.sun.com/TPAInvoice. The schema imports LineItem definitions from TPALineItem.dtd using an external entity reference, allowing for modular document structure definition.

 **Code Landmarks**
- `Line 33`: Defines a namespace attribute entity that's reused across all elements for consistent XML namespace declaration
- `Line 36-40`: Root element definition includes locale attribute with default value 'en_US', supporting internationalization
- `Line 72`: Uses external entity reference to import LineItem definitions from separate DTD file, enabling modular schema design
### xmldocuments/src/com/sun/j2ee/blueprints/xmldocuments/rsrc/schemas/TPALineItem.dtd

This DTD file defines the XML schema for Trading Partner Agreement (TPA) line items used in the Java Pet Store application. It specifies an empty LineItem element with required attributes including categoryId, productId, itemId, lineNo, quantity, and unitPrice. The schema establishes a namespace (xmlns:tpali) fixed to "http://blueprints.j2ee.sun.com/TPALineItem" for all line items. This structure enables standardized representation of order line items when exchanging data between trading partners in the e-commerce system.

 **Code Landmarks**
- `Line 40`: Uses empty element pattern (EMPTY) with attributes rather than nested elements for data efficiency
### xmldocuments/src/com/sun/j2ee/blueprints/xmldocuments/rsrc/schemas/TPASupplierOrder.dtd

TPASupplierOrder.dtd defines the Document Type Definition (DTD) schema for XML documents representing supplier orders in the Trading Partner Agreement (TPA) system. It establishes the structure for supplier orders including order ID, date, shipping address details (name, address, contact information), and line items. The schema uses a namespace prefix 'tpaso' with a fixed URI and imports the TPALineItem.dtd schema for line item definitions. Each element is carefully defined with appropriate attributes and content models to ensure valid XML document creation for supplier order processing.

 **Code Landmarks**
- `Line 39`: Uses entity declaration to define a namespace attribute that's reused across all elements
- `Line 106`: External entity reference imports the TPALineItem DTD for line item definitions
### xmldocuments/src/com/sun/j2ee/blueprints/xmldocuments/tpa/TPAInvoiceXDE.java

TPAInvoiceXDE implements an XML document editor for Trading Partner Agreement (TPA) invoices in the Pet Store application. It provides functionality to create, manipulate, and serialize invoice documents that conform to a specific XML schema. The class handles document creation, setting invoice properties (order ID, user ID, dates), adding line items, and serializing to various formats. Key methods include newDocument(), copyDocument(), getDocument(), setOrderId(), setUserId(), setOrderDate(), setShippingDate(), and addLineItem(). Important constants define XML namespaces, element names, and schema locations for validation.

 **Code Landmarks**
- `Line 94`: Supports both DTD and XSD validation through a configurable flag
- `Line 118`: Uses DOM manipulation to construct XML documents programmatically rather than string concatenation
- `Line 126`: Implements a flexible serialization system supporting multiple output formats (DOM Source and String)
### xmldocuments/src/com/sun/j2ee/blueprints/xmldocuments/tpa/TPAInvoiceXDE.java

TPAInvoiceXDE implements an XML document editor for Trading Partner Agreement (TPA) invoices in the Pet Store application. It provides functionality to create, manipulate, and serialize invoice documents that conform to a specific XML schema. The class handles document creation, setting invoice properties (order ID, user ID, dates), adding line items, and serializing to various formats. Key methods include newDocument(), copyDocument(), getDocument(), setOrderId(), setUserId(), setOrderDate(), setShippingDate(), and addLineItem(). Important constants define XML namespaces, element names, and schema locations for validation.

 **Code Landmarks**
- `Line 94`: Supports both DTD and XSD validation through a configurable flag
- `Line 118`: Uses DOM manipulation to construct XML documents programmatically rather than string concatenation
- `Line 126`: Implements a flexible serialization system supporting multiple output formats (DOM Source and String)
### xmldocuments/src/com/sun/j2ee/blueprints/xmldocuments/tpa/TPALineItemUtils.java

TPALineItemUtils provides functionality for creating XML line item elements within TPA (Trading Partner Agreement) documents. The class defines XML namespace constants and element/attribute names used in TPA line item representations. It implements a single static method, addLineItem(), which creates a properly formatted line item element with specified product details (category, product, item IDs, line number, quantity, and price) and appends it to a parent element in an XML document. The class is designed as a utility with a private constructor to prevent instantiation.

 **Code Landmarks**
- `Line 67`: Creates XML elements with proper namespace handling for TPA line items
### xmldocuments/src/com/sun/j2ee/blueprints/xmldocuments/tpa/TPASupplierOrderXDE.java

TPASupplierOrderXDE implements an XML document editor for Trading Partner Agreement (TPA) supplier orders. It provides functionality to create, manipulate, and serialize XML documents representing supplier orders with shipping addresses and line items. The class defines XML constants, handles document creation, and offers methods to set order details like ID, date, shipping address, and line items. Important methods include newDocument(), copyDocument(), getDocument(), setOrderId(), setOrderDate(), setShippingAddress(), and addLineItem(). It supports both DTD and XSD validation through configuration options.

 **Code Landmarks**
- `Line 84`: Uses dual validation support (DTD/XSD) through configuration parameters
- `Line 124`: Document serialization with proper namespace handling for XML standards compliance
- `Line 173`: Reuses TPALineItemUtils for line item creation, showing component modularity
### xmldocuments/src/rsrc/schemas/EntityCatalog.properties

EntityCatalog.properties serves as a mapping file that associates Document Type Definition (DTD) public identifiers with their corresponding physical file locations within the Java Pet Store application. It contains three sections: old DTDs, new TPA (Trading Partner Agreement) DTDs, and new Component/Composite DTDs. Each entry maps a formal public identifier to a relative path where the DTD file can be found. This catalog enables XML parsers to resolve external entity references without requiring network access, improving parsing performance and reliability by providing local copies of all required DTDs.

 **Code Landmarks**
- `Line 1-6`: Legacy DTD mappings that point to simplified paths, suggesting backward compatibility support
- `Line 9-11`: TPA-specific DTDs indicate support for business-to-business integration through Trading Partner Agreements
- `Line 14-19`: Component-based architecture evident in the organization of DTDs by business domain objects
### xmldocuments/src/rsrc/schemas/Invoice.dtd

This DTD file defines the XML structure for Invoice documents in the Java Pet Store application. It imports the LineItem DTD and specifies that an Invoice element must contain OrderId, UserId, OrderDate, ShippingDate elements, and one or more LineItem elements. The Invoice element has an optional locale attribute with a default value of 'en_US'. Each child element is defined to contain parsed character data (#PCDATA), establishing a clear structure for representing order invoices in the application's XML documents.

 **Code Landmarks**
- `Line 35`: Uses entity reference to import and include the LineItem DTD, demonstrating modular DTD design
### xmldocuments/src/rsrc/schemas/Invoice.dtd

This DTD file defines the XML structure for Invoice documents in the Java Pet Store application. It imports the LineItem DTD and specifies that an Invoice element must contain OrderId, UserId, OrderDate, ShippingDate elements, and one or more LineItem elements. The Invoice element has an optional locale attribute with a default value of 'en_US'. Each child element is defined to contain parsed character data (#PCDATA), establishing a clear structure for representing order invoices in the application's XML documents.

 **Code Landmarks**
- `Line 35`: Uses entity reference to import and include the LineItem DTD, demonstrating modular DTD design
### xmldocuments/src/rsrc/schemas/LineItem.dtd

LineItem.dtd defines the Document Type Definition (DTD) schema for LineItem XML documents used in the Java Pet Store application. It specifies the structure and elements required for representing order line items in XML format. The DTD declares that a LineItem element must contain six child elements: CategoryId, ProductId, ItemId, LineNum, Quantity, and UnitPrice. Each of these child elements is defined to contain parsed character data (#PCDATA). This schema ensures consistent formatting of line item data for order processing within the e-commerce application.
### xmldocuments/src/rsrc/schemas/Mail.dtd

Mail.dtd defines the Document Type Definition (DTD) for XML documents representing email messages in the Java Pet Store application. It establishes a simple structure for mail messages with three required elements: Address (recipient email), Subject (email subject line), and Content (email body text). Each element is defined to contain parsed character data (#PCDATA). This schema ensures that XML documents representing emails conform to a consistent structure for processing within the application's messaging components.
### xmldocuments/src/rsrc/schemas/OrderApproval.dtd

OrderApproval.dtd defines the Document Type Definition (DTD) schema for XML documents related to order approval in the Java Pet Store application. The schema establishes a simple hierarchical structure where OrderApproval is the root element that contains one or more Order elements. Each Order element consists of two child elements: OrderId, which holds the order identifier as parsed character data, and OrderStatus, which contains the approval status. The file includes a comment noting that OrderStatus should be implemented as an attribute rather than an element.

 **Code Landmarks**
- `Line 50`: Contains a FIXME comment indicating OrderStatus should be implemented as an attribute rather than an element, suggesting potential future refactoring.
### xmldocuments/src/rsrc/schemas/PurchaseOrder.dtd

PurchaseOrder.dtd defines the Document Type Definition (DTD) schema for purchase order XML documents in the Java Pet Store application. It specifies the structure and constraints for purchase order data, including elements for order details (OrderId, UserId, EmailId, OrderDate), shipping and billing addresses, total price, credit card information, and line items. The schema uses entity references to define reusable address components and imports the LineItem DTD. Each element is carefully defined with its content model, with the root PurchaseOrder element having a locale attribute defaulting to 'en_US'.

 **Code Landmarks**
- `Line 37`: Uses entity declaration (%Address;) to define reusable address components for both shipping and billing addresses
- `Line 65`: External entity reference imports LineItem.dtd using PUBLIC identifier, demonstrating modular DTD design
### xmldocuments/src/rsrc/schemas/SupplierOrder.dtd

SupplierOrder.dtd defines the Document Type Definition (DTD) schema for supplier order XML documents in the Java Pet Store application. It establishes the structure and validation rules for supplier orders, including elements for order ID, order date, shipping address, and line items. The schema defines a reusable Address entity pattern containing customer information fields (name, street, city, state, country, zip) and imports the LineItem DTD from an external source. This DTD ensures that XML documents representing supplier orders conform to a standardized structure for processing within the application's supply chain management functionality.

 **Code Landmarks**
- `Line 43`: Uses entity declaration (%Address;) to create a reusable group of address-related elements
- `Line 59`: Imports external LineItem DTD using PUBLIC identifier, demonstrating modular DTD design

[Generated by the Sage AI expert workbench: 2025-03-29 21:37:00  https://sage-tech.ai/workbench]: #