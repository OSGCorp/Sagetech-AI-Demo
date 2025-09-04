# Inventory Web Interface Of Java Pet Store

The Java Pet Store is a Java EE application that demonstrates enterprise application architecture and best practices using the J2EE 1.3 platform. This sub-project implements the supplier inventory management interface along with web-based inventory control capabilities. This provides these capabilities to the Java Pet Store program:

- Web-based inventory management for supplier users
- Purchase order processing and fulfillment
- Inventory quantity tracking and updates
- Integration with EJB-based business components

## Identified Design Elements

1. Manages Responses In Both JSON and HTML Format: The base controller class implements an API to allow the requestor to receive the results of the request in either format
2. Complex Request Chaining: More complex requests can be composed of simple atomic elements defined in the widgets folder
3. Automated Form Field Typing: Through introspection of the target data class, the form input controllers are able to validate input types match the intended data format
4. View Chaining: Sub-views do not need to re-query parent data objects, as they are encoded and passed into the chained request

## Overview
The architecture emphasizes DRY principles through partial reuse, maintains consistent styling via Bootstrap integration, and provides comprehensive format support for both traditional web and API consumers. The templates are designed for maintainability and extensibility, with clear separation of concerns and robust error handling.

The system follows a layered architecture with clear separation between presentation controllers (RcvrRequestProcessor), data access components (DisplayInventoryBean), and integration services (JNDINames). The web interface connects to backend EJBs through service locators, providing a clean separation between the web tier and business logic. Transaction management is handled within the servlet controllers, which process inventory updates and purchase order fulfillment through appropriate delegate classes.

## Business Functions

### Inventory Management
- `RcvrRequestProcessor.java` : Servlet that processes inventory management requests from the supplier's receiver role in the Pet Store application.

### System Configuration
- `JNDINames.java` : Defines JNDI name constants for accessing EJBs and resources in the supplier inventory system.

### Data Access
- `DisplayInventoryBean.java` : A JavaBean that provides access to inventory data for the supplier application's web interface.

## Files
### DisplayInventoryBean.java

DisplayInventoryBean implements a simple JavaBean that serves as a bridge between the web layer and the inventory EJB in the supplier application. It provides functionality to retrieve all inventory items with their quantities for display purposes. The class uses the Service Locator pattern to obtain a reference to the InventoryLocalHome interface, which it then uses to query for all inventory items. The main method getInventory() returns a Collection of inventory items that can be displayed in the receiver application's user interface.

 **Code Landmarks**
- `Line 58`: Uses the Service Locator pattern to abstract away JNDI lookup complexity
- `Line 61`: Lazy initialization of the EJB home reference only when needed
### JNDINames.java

JNDINames provides a centralized repository of JNDI lookup names used throughout the supplier inventory web component. It implements a non-instantiable utility class containing static final String constants that define the JNDI paths for accessing the Inventory EJB, OrderFulfillmentFacade EJB, UserTransaction, and SupplierOrderTD transition delegate. These constants ensure consistent naming across the application and must be synchronized with deployment descriptors. The class prevents instantiation through a private constructor and serves as a single point of maintenance for JNDI path references.

 **Code Landmarks**
- `Line 46`: Uses a private constructor pattern to prevent instantiation of this utility class
- `Line 50-57`: Constants use java:comp/env/ prefix following J2EE naming conventions for resource references
### RcvrRequestProcessor.java

RcvrRequestProcessor implements a servlet for handling inventory management requests from the supplier component's receiver role. It provides functionality to update inventory quantities and process pending purchase orders. The class connects to inventory and order fulfillment EJBs through service locators, manages transactions, and forwards responses to appropriate JSP pages. Key methods include updateInventory() which modifies item quantities, sendInvoices() which uses a transition delegate to process invoices, and doPost() which handles different screen actions including inventory display and updates. Important variables include inventoryHomeRef, orderFacadeHomeRef, and transitionDelegate.

 **Code Landmarks**
- `Line 88`: Uses service locator pattern to obtain EJB references, abstracting JNDI lookup complexity
- `Line 131`: Implements a transition delegate pattern for sending invoices through a factory-created delegate
- `Line 166`: Uses container-managed transactions with UserTransaction to ensure ACID properties during inventory updates

[Generated by the Sage AI expert workbench: 2025-03-29 21:37:00  https://sage-tech.ai/workbench]: #