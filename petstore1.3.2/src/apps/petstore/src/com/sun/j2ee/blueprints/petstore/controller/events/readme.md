# Pet Store Event System Of Java Pet Store

The Pet Store Event System is a Java-based event handling framework that facilitates communication between components in the Java Pet Store application. This sub-project implements a robust event-driven architecture along with standardized response handling mechanisms. The system provides these capabilities to the Java Pet Store program:

- Encapsulated event objects for various business operations (user creation, cart management, orders)
- Standardized event handling patterns through EventSupport extension
- Clean separation between event generation and processing
- Type-safe data transfer between application layers

## Identified Design Elements

1. Event Type Identification: Each event class implements getEventName() to provide JNDI-compatible identifiers for event routing
2. Immutable Event Pattern: Events store all required data at creation time with getter-only access
3. Action Type Flags: Events like CartEvent use enumerated action types to specify the intended operation
4. Defensive Copying: Collection-based events implement defensive copying to prevent external modification
5. Hierarchical Event Structure: All events extend from a common EventSupport base class

## Overview
The architecture emphasizes clean separation of concerns by encapsulating business operations as discrete events. The system supports user management (CreateUserEvent, SignOnEvent), shopping operations (CartEvent), and order processing (OrderEvent, OrderEventResponse). Each event type contains precisely the data needed for its specific operation, promoting loose coupling between components. The consistent implementation pattern makes the system easily extensible for new business operations while maintaining backward compatibility with existing event handlers.

## Business Functions

### User Authentication Events
- `SignOnEvent.java` : Event class representing user sign-on actions in the Java Pet Store application.
- `CreateUserEvent.java` : Event class for user creation in the Java Pet Store application.

### Customer Management Events
- `CustomerEvent.java` : Event class representing customer data changes for the Pet Store controller

### Shopping Cart Events
- `CartEvent.java` : Defines a CartEvent class that represents shopping cart operations in the Java Pet Store application.

### Order Processing Events
- `OrderEvent.java` : Event class representing order information for the Pet Store application's checkout process.
- `OrderEventResponse.java` : Represents a response event containing order information after an order has been placed.

## Files
### CartEvent.java

CartEvent implements an event class that encapsulates shopping cart state changes for the PetStore controller. It supports four action types: ADD_ITEM, DELETE_ITEM, UPDATE_ITEMS, and EMPTY. The class stores information about items being modified including item IDs and quantities. Multiple constructors handle different cart operations, from adding single items to updating multiple items via a HashMap. Key methods include getActionType(), getItemId(), getQuantity(), getItems(), and a static factory method createUpdateItemEvent() that creates events with defensive copies of item collections.

 **Code Landmarks**
- `Line 122`: Creates a defensive copy of the items Map to prevent modification after event creation
- `Line 46`: Extends EventSupport from the Web Application Framework, integrating with the application's event system
### CreateUserEvent.java

CreateUserEvent implements an event class that extends EventSupport to handle user creation operations in the Pet Store application. It stores username and password information needed to create a new user account. The class provides getter methods for accessing the username and password, overrides toString() to display event details, and implements getEventName() to identify the event type. This event is designed to be processed by the EJBController component to initiate user account creation in the system.

 **Code Landmarks**
- `Line 43`: The class comment incorrectly describes this as a Locale change event rather than a user creation event
### CustomerEvent.java

CustomerEvent implements an event class that encapsulates customer information changes in the Pet Store application. It extends EventSupport and carries data about customer profile updates or creation actions. The class stores ContactInfo, ProfileInfo, and CreditCard objects along with an action type flag (UPDATE or CREATE). Key functionality includes retrieving customer data components and identifying the event type. Important elements include the constants UPDATE and CREATE, instance variables for customer data, a constructor that initializes all fields, getter methods for each data component, and toString() and getEventName() methods for event identification.

 **Code Landmarks**
- `Line 49`: Extends EventSupport from the Web Application Framework (WAF) to integrate with the event handling system
- `Line 53-54`: Uses constants to define action types, making the code more maintainable and readable
### OrderEvent.java

OrderEvent implements an event class that extends EventSupport to represent order information in the Pet Store application. It encapsulates billing information, shipping information, and credit card details needed to process an order. The class stores ContactInfo objects for both billing and shipping addresses along with a CreditCard object. It provides getter methods to access these properties and overrides toString() and getEventName() methods from its parent class. This event is likely triggered during checkout to initiate order processing in the application's controller layer.

 **Code Landmarks**
- `Line 48`: Class extends EventSupport from the Web Application Framework (WAF) to integrate with the event handling system
- `Line 55`: Stores both billing and shipping addresses separately, allowing for different delivery locations
### OrderEventResponse.java

OrderEventResponse implements an event response class that encapsulates information about a completed order in the Pet Store application. It stores two key pieces of data: the order ID and the customer's email address. The class implements the EventResponse interface from the Web Application Framework (WAF), providing methods to access the order information and identify the event type. Key methods include getOrderId(), getEmail(), toString() for debugging, and getEventName() which returns the JNDI name used to identify this event type in the application's event handling system.

 **Code Landmarks**
- `Line 54`: Uses JNDI naming convention for event identification in getEventName() method
### SignOnEvent.java

SignOnEvent implements an event class that extends EventSupport to represent user sign-on actions in the Pet Store application. It stores a username provided during sign-on and makes it available through the getUserName() method. The class provides toString() for debugging and getEventName() to identify the event type. This event is designed to notify the EJBController about user authentication, enabling the system to track user sessions and potentially start order processing workflows.

 **Code Landmarks**
- `Line 44`: This class extends EventSupport from the Web Application Framework (WAF), showing the event-driven architecture of the application

[Generated by the Sage AI expert workbench: 2025-03-29 21:37:00  https://sage-tech.ai/workbench]: #