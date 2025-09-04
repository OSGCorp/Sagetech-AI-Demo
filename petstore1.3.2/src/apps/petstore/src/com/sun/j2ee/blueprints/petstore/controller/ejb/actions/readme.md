# Pet Store EJB Action Implementations Of Java Pet Store

The Java Pet Store is a Java EE application that demonstrates enterprise application architecture patterns and best practices. The program implements a complete e-commerce solution and showcases J2EE component integration within a web application framework. This sub-project implements the Enterprise JavaBean (EJB) action controllers along with the event handling mechanisms that drive the core business logic of the application. This provides these capabilities to the Java Pet Store program:

- Event-driven business logic processing
- Shopping cart and order management
- User authentication and registration
- Customer profile management
- Internationalization support through locale handling

## Identified Design Elements

1. EJB Action Framework: Controllers extend EJBActionSupport to process specific event types and interact with appropriate EJB components
2. Facade Pattern Implementation: Actions use client facades like ShoppingClientFacadeLocal to access underlying business services
3. Event-Response Architecture: Each action processes specific event types and returns appropriate responses
4. Service Locator Pattern: Components use ServiceLocator to obtain references to EJB interfaces
5. Asynchronous Processing: Order submission uses AsyncSender for non-blocking order processing

## Overview
The architecture emphasizes separation of concerns by dividing functionality into specialized action classes that handle specific business processes. The controllers bridge the web presentation layer with the EJB business components, providing a clean interface for event processing. The design supports internationalization through locale management, maintains shopping state across sessions, and implements secure user authentication. The order processing system demonstrates integration with asynchronous messaging for improved scalability and performance.

## Business Functions

### Shopping Cart Management
- `CartEJBAction.java` : Handles shopping cart operations in the Java Pet Store application.

### Customer Management
- `CustomerEJBAction.java` : EJB action class that handles customer creation and update operations in the Pet Store application.

### Authentication and User Management
- `SignOnEJBAction.java` : Handles user sign-on authentication and session setup in the Pet Store application.
- `CreateUserEJBAction.java` : Handles user account creation in the Java Pet Store application by interfacing with SignOn EJB.

### Order Processing
- `OrderEJBAction.java` : Processes order submissions by creating purchase orders and sending them asynchronously for fulfillment.

### Localization
- `ChangeLocaleEJBAction.java` : Handles locale change events in the Java Pet Store application by updating user preferences.

## Files
### CartEJBAction.java

CartEJBAction implements a controller action that processes shopping cart events in the Pet Store application. It extends EJBActionSupport and implements the perform method to handle three types of cart operations: adding items, deleting items, and updating item quantities. The class interacts with ShoppingClientFacadeLocal to access the user's ShoppingCartLocal instance and performs the appropriate cart modifications based on the CartEvent's action type. Key methods include perform(Event) which processes CartEvent objects and returns null after completing the requested cart operations.

 **Code Landmarks**
- `Line 61`: Uses a switch statement to handle different cart action types from a single event handler
- `Line 77`: Processes a map of item IDs and quantities to update multiple cart items in a single operation
### ChangeLocaleEJBAction.java

ChangeLocaleEJBAction implements an EJB action that processes locale change requests in the Pet Store application. It extends EJBActionSupport and implements the perform method to handle ChangeLocaleEvent objects. When a locale change is requested, the action updates the locale attribute in the state machine and notifies the shopping cart component of the change by retrieving the ShoppingClientFacade from the state machine and calling setLocale on the associated ShoppingCartLocal object. The class integrates with the Web Application Framework (WAF) event system and the shopping cart component.

 **Code Landmarks**
- `Line 58`: Updates both the application state machine and shopping cart with locale changes, ensuring consistent localization across components
### CreateUserEJBAction.java

CreateUserEJBAction implements an EJB action that processes user registration requests in the Pet Store application. It extends EJBActionSupport and handles CreateUserEvent objects by creating user accounts through the SignOn EJB. The class's primary method perform() extracts username and password from the event, obtains a SignOnLocal EJB reference using ServiceLocator, creates the user account, and associates the username with the shopping client facade. It handles various exceptions by throwing DuplicateAccountException when user creation fails.

 **Code Landmarks**
- `Line 75`: Uses ServiceLocator pattern to obtain EJB references, promoting loose coupling
- `Line 86`: Stores username in state machine for future retrieval, showing session state management
### CustomerEJBAction.java

CustomerEJBAction implements an EJB action class that processes CustomerEvent objects to create and update customer information in the Pet Store application. It interacts with the ShoppingClientFacadeLocal to access customer data and performs deep copying of customer information between data transfer objects and EJB local interfaces. The class handles two action types: CREATE (creates a new customer and updates their information) and UPDATE (updates an existing customer's information). The updateCustomer method performs detailed mapping of contact information, address, credit card details, and user preferences from event objects to the corresponding EJB local interfaces.

 **Code Landmarks**
- `Line 77`: Deep copying pattern between data transfer objects and EJB local interfaces demonstrates separation between presentation and persistence layers
- `Line 123`: Sets user's locale preference in the state machine, affecting the application's internationalization behavior
### OrderEJBAction.java

OrderEJBAction implements an EJB action that processes order submissions in the Java Pet Store application. It extends EJBActionSupport and handles OrderEvents by creating PurchaseOrder objects with customer billing, shipping, and payment information. The class retrieves cart items, generates unique order IDs, calculates order totals, and sends the completed purchase order asynchronously via the AsyncSender component. After successful order submission, it empties the shopping cart and returns an OrderEventResponse with the order details. Key methods include perform() which orchestrates the entire order processing workflow.

 **Code Landmarks**
- `Line 103`: Uses a unique ID generator EJB to create order IDs with a specific prefix (1001)
- `Line 132`: Validates shopping cart contents before processing, throwing ShoppingCartEmptyOrderException if empty
- `Line 155`: Converts the purchase order to XML and sends it asynchronously for processing
- `Line 175`: Empties the shopping cart after successful order submission to prevent duplicate orders
### SignOnEJBAction.java

SignOnEJBAction implements an EJB action that processes user sign-on events in the Pet Store application. It extends EJBActionSupport and handles SignOnEvent objects by setting the user ID in the shopping client facade and configuring locale preferences based on the user's profile. The class retrieves the user's preferred language from their profile, converts it to a Locale object, and updates both the state machine and shopping cart with this locale. Key methods include perform() which processes the sign-on event and manages user session setup.

 **Code Landmarks**
- `Line 77`: Sets user's preferred language as application locale, demonstrating internationalization support
- `Line 80`: Notifies shopping cart of locale change, showing cross-component communication pattern
- `Line 81`: Handles case where user profile doesn't exist yet during first-time registration

[Generated by the Sage AI expert workbench: 2025-03-29 21:37:00  https://sage-tech.ai/workbench]: #