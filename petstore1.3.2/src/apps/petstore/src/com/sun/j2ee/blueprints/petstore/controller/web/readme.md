# Pet Store Web Controller Of Java Pet Store

The Java Pet Store is a Java EE reference implementation that demonstrates best practices for building scalable and cross-platform enterprise e-commerce applications. The Pet Store Web Controller sub-project implements the web-tier controller components that handle HTTP requests and manage the presentation layer interaction with the underlying business logic. This controller framework bridges the gap between the user interface and the EJB tier, providing a structured approach to request handling and response generation.

The sub-project delivers these capabilities to the Java Pet Store program:

- Request routing and event handling for shopping and user management operations
- Session management and authentication through sign-on notification
- Component-based architecture with clear separation between web and EJB tiers
- Dynamic content generation with support for multiple output formats
- Flow control for multi-step user interactions like account creation

## Identified Design Elements

1. Manages Responses In Both JSON and HTML Format: The base controller class implements an API to allow the requestor to receive the results of the request in either format
2. Complex Request Chaining: More complex requests can be composed of simple atomic elements defined in the widgets folder
3. Automated Form Field Typing: Through introspection of the target data class, the form input controllers are able to validate input types match the intended data format
4. View Chaining: Sub-views do not need to re-query parent data objects, as they are encoded and passed into the chained request

## Overview
The architecture emphasizes DRY principles through partial reuse, maintains consistent styling via Bootstrap integration, and provides comprehensive format support for both traditional web and API consumers. The templates are designed for maintainability and extensibility, with clear separation of concerns and robust error handling.

## Sub-Projects

### src/apps/petstore/src/com/sun/j2ee/blueprints/petstore/controller/web/exceptions

The program implements a complete online shopping experience and showcases best practices for J2EE development. This sub-project implements specialized exception handling for web interactions along with form validation error management. This provides these capabilities to the Java Pet Store program:

- Custom exception types for web-specific error scenarios
- Form validation error tracking and reporting
- Structured error information for both user interface and programmatic handling
- Integration with the application's exception handling framework

#### Identified Design Elements

1. Form Validation Framework: The exception handlers work with the RequestToEventTranslator to validate user input and report specific missing or invalid fields
2. Layered Exception Hierarchy: Exceptions extend from HTMLActionException to maintain consistent error handling patterns
3. Detailed Error Reporting: Exceptions store both general error messages and specific field-level validation failures
4. JSP Integration: Exception data is structured to be easily consumable by JSP pages for user-friendly error presentation

#### Overview
The architecture emphasizes clean separation between business logic and presentation-layer error handling. The MissingFormDataException specifically tracks form field validation failures, enabling precise error reporting to users. This approach allows the application to maintain state during form submission errors, preserving valid data while highlighting only problematic fields. The exception handlers integrate with both the web presentation layer and the underlying business logic, ensuring consistent error management throughout the application flow.

  *For additional detailed information, see the summary for src/apps/petstore/src/com/sun/j2ee/blueprints/petstore/controller/web/exceptions.*

### src/apps/petstore/src/com/sun/j2ee/blueprints/petstore/controller/web/actions

The program processes web requests through a controller architecture and manages user interactions with the online store. This sub-project implements concrete action handlers for web requests along with form processing and event generation capabilities. This provides these capabilities to the Java Pet Store program:

- HTTP request processing for core e-commerce operations
- Conversion of web form data into application events
- Session management for user authentication
- Shopping cart manipulation through web interfaces
- Customer account management and order processing

#### Identified Design Elements

1. **Event-Based Architecture**: Action classes process HTTP requests and generate specific event objects (CartEvent, OrderEvent, etc.) that are handled by the application's event system
2. **Form Data Extraction**: Specialized methods extract and validate form fields from HTTP requests before converting them to domain objects
3. **Session Management**: Authentication state is maintained across requests with specific handling for sign-on and sign-off operations
4. **Validation Logic**: Required fields are validated with appropriate error handling through MissingFormDataException
5. **Component-Based Design**: Actions extend base support classes (HTMLActionSupport) for common functionality

#### Overview
The architecture follows a controller pattern where each action class handles a specific domain function (cart management, user creation, order processing). These controllers extract data from HTTP requests, perform basic validation, and generate appropriate events for the application's business logic layer. The design emphasizes separation between the web presentation layer and the underlying business logic through this event-based approach. Error handling is implemented through exceptions, while session management provides user context across requests. The controllers maintain a clean separation of concerns by focusing solely on request processing and event generation.

  *For additional detailed information, see the summary for src/apps/petstore/src/com/sun/j2ee/blueprints/petstore/controller/web/actions.*

### src/apps/petstore/src/com/sun/j2ee/blueprints/petstore/controller/web/flow/handlers

This sub-project implements concrete flow handlers for managing navigation and page flow control, along with user interaction patterns throughout the application. This provides these capabilities to the Java Pet Store program:

- Controlled navigation between application screens
- Session-aware user flow management
- Post-action redirection logic
- Preservation of user context during multi-step processes

#### Identified Design Elements

1. **Flow Handler Interface Implementation**: The architecture follows a consistent pattern where handlers implement the FlowHandler interface with processFlow() as the core navigation control method
2. **Session-Based Context Preservation**: Flow handlers maintain user context by storing and retrieving state information from the session
3. **Conditional Navigation Logic**: Navigation decisions are made based on application state and user actions
4. **URL Management**: The system tracks original URLs to enable proper return navigation after completing workflows

#### Overview
The architecture emphasizes clean separation between business logic and navigation control through the flow handler pattern. Each handler is responsible for a specific user workflow, such as account creation, with clear entry and exit points. The flow management system provides consistent user experience by maintaining context across multiple screens while allowing for conditional branching based on user actions or system state. This design enables the application to guide users through complex processes while maintaining their session state and ensuring they reach appropriate destinations upon completion.

  *For additional detailed information, see the summary for src/apps/petstore/src/com/sun/j2ee/blueprints/petstore/controller/web/flow/handlers.*

## Business Functions

### Shopping Management
- `ShoppingWebController.java` : Web controller that proxies shopping-related events to the EJB tier in the Java Pet Store application.

### Authentication
- `SignOnNotifier.java` : Implements a session attribute listener that notifies the Petstore backend when a user signs on.

### User Interface
- `BannerHelper.java` : Helper class that selects appropriate banner images based on product categories.

### Component Management
- `PetstoreComponentManager.java` : Component manager for the Java Pet Store web tier that manages session resources and provides access to EJB services.

### User Registration
- `flow/handlers/CreateUserFlowHandler.java` : Flow handler that directs users to appropriate screens after account creation.

## Files
### BannerHelper.java

BannerHelper implements a simple web tier display selection utility that determines which banner image to display based on a product category. It contains a single field to store the categoryId and provides methods to set this ID and retrieve the corresponding banner image filename. The getBanner() method maps category IDs (dogs, cats, reptiles, birds, fish) to their respective banner image filenames, returning a default dog banner if no match is found. The class is serializable to support web application state management.

 **Code Landmarks**
- `Line 46`: Class implements Serializable to support state persistence in web applications
- `Line 60`: Hard-coded mapping of category IDs to banner images could be externalized to properties file or database as noted in the class comments
### PetstoreComponentManager.java

PetstoreComponentManager extends DefaultComponentManager to provide access to services in both web and EJB tiers of the Pet Store application. It implements HttpSessionListener to manage session lifecycle events and initializes shopping cart resources for each user session. The class serves as a bridge between web controllers and EJB components, offering methods to retrieve shopping controllers, customer information, and shopping carts. Key methods include init(), sessionCreated(), getCustomer(), getShoppingController(), and getShoppingCart(). It uses ServiceLocator to obtain references to EJB homes and handles exceptions by wrapping them in GeneralFailureException.

 **Code Landmarks**
- `Line 84`: Implements HttpSessionListener to manage session lifecycle events programmatically
- `Line 94`: Uses service locator pattern to abstract away JNDI lookup complexity
- `Line 123`: Maintains shopping controller as session attribute for stateful interactions
### ShoppingWebController.java

ShoppingWebController implements a proxy class that handles shopping-related events in the Pet Store application by delegating them to the EJB tier. It implements the WebController interface with methods for initialization, event handling, and resource cleanup. The handleEvent method synchronously processes events by retrieving the ShoppingControllerLocal EJB from the PetstoreComponentManager and forwarding events to it. The destroy method properly removes the associated EJB when the controller is no longer needed. The class ensures thread safety by synchronizing methods that access the stateful session bean.

 **Code Landmarks**
- `Line 74`: All methods accessing the EJB tier are synchronized to prevent concurrent access to the stateful session bean
- `Line 89`: Uses a component manager pattern to retrieve the shopping controller EJB from the session
- `Line 107`: Deliberately ignores RemoveException during cleanup, showing a design decision to prioritize clean termination over error handling
### SignOnNotifier.java

SignOnNotifier implements an HttpSessionAttributeListener that detects when a user signs on to the Petstore application. It monitors session attribute changes, specifically looking for the SIGNED_ON_USER attribute set by the SignOnFilter. When a sign-on is detected, it creates a SignOnEvent with the username, finds the appropriate EJBAction mapping, and passes the event to the WebController. It also ensures the customer object is in the session and sets locale preferences based on the user's profile. This class enables loose coupling between the SignOn component and the Petstore application.

 **Code Landmarks**
- `Line 97`: Detects sign-on by monitoring session attribute changes rather than direct API calls
- `Line 112`: Uses event-based architecture to notify backend systems of user authentication
- `Line 125`: Sets user preferences and locale based on stored profile after authentication
### flow/handlers/CreateUserFlowHandler.java

CreateUserFlowHandler implements a flow handler responsible for redirecting users to the appropriate screen after they create a new account. It works with the SignOn component to determine where users should be directed after account creation. The class implements the FlowHandler interface with three methods: doStart() which is empty, processFlow() which retrieves the original URL from the session and determines the appropriate forward destination, and doEnd() which is also empty. The key functionality is in processFlow(), which checks if the forward screen is 'customer.do' and returns 'MAIN_SCREEN' in that case, otherwise returns the original URL.

 **Code Landmarks**
- `Line 60`: The handler specifically checks for 'customer.do' URL and redirects to 'MAIN_SCREEN' instead, showing special case handling in the navigation flow.

[Generated by the Sage AI expert workbench: 2025-03-29 21:37:00  https://sage-tech.ai/workbench]: #