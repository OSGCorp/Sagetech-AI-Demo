# Web Application Framework Source Root

The Web Application Framework (WAF) is a Java-based MVC architecture that provides the structural foundation for the Java Pet Store application. This framework implements a robust controller layer that bridges HTTP requests with business logic, along with a flexible templating system for view rendering. The WAF serves as the architectural backbone of the Pet Store application, enabling separation of concerns and promoting code reusability.

The framework implements a multi-tiered controller architecture with web and EJB components that process user interactions through an event-driven design. This provides these capabilities to the Java Pet Store program:

- Comprehensive MVC implementation with clear separation between presentation, control flow, and business logic
- Event-driven architecture for handling user interactions and system events
- Internationalization support through locale-specific content rendering
- Client-side state management for maintaining application state across requests
- Template-based view rendering with component reuse capabilities

## Identified Design Elements

1. Manages Responses In Both JSON and HTML Format: The base controller class implements an API to allow the requestor to receive the results of the request in either format
2. Complex Request Chaining: More complex requests can be composed of simple atomic elements defined in the widgets folder
3. Automated Form Field Typing: Through introspection of the target data class, the form input controllers are able to validate input types match the intended data format
4. View Chaining: Sub-views do not need to re-query parent data objects, as they are encoded and passed into the chained request

## Overview
The architecture emphasizes DRY principles through partial reuse, maintains consistent styling via Bootstrap integration, and provides comprehensive format support for both traditional web and API consumers. The templates are designed for maintainability and extensibility, with clear separation of concerns and robust error handling.

The framework is structured around several key components:
- **Web Controller Layer**: Processes HTTP requests through MainServlet and RequestProcessor classes
- **EJB Controller Layer**: Handles business logic through the EJBController and StateMachine components
- **Event System**: Facilitates communication between tiers using Event and EventResponse objects
- **Template System**: Manages screen definitions and renders views through TemplateServlet
- **Custom Tag Libraries**: Provides reusable UI components for forms, client-state management, and caching

Configuration is primarily XML-based, with mappings for URL patterns, screen definitions, and event handlers defined in external files. This approach allows for flexible application behavior without code changes.

## Sub-Projects

### src/waf/src/controller/com/sun/j2ee/blueprints/waf

The WAF (Web Application Framework) Controller Components sub-project implements the core MVC controller architecture that processes HTTP requests and coordinates the application's response flow. This provides these capabilities to the Java Pet Store program:

- Request routing and dispatching to appropriate handlers
- View resolution and rendering for different output formats
- Form processing and validation
- Session management and state preservation
- Integration with the business logic layer

#### Identified Design Elements

1. Manages Responses In Both JSON and HTML Format: The base controller class implements an API to allow the requestor to receive the results of the request in either format
2. Complex Request Chaining: More complex requests can be composed of simple atomic elements defined in the widgets folder
3. Automated Form Field Typing: Through introspection of the target data class, the form input controllers are able to validate input types match the intended data format
4. View Chaining: Sub-views do not need to re-query parent data objects, as they are encoded and passed into the chained request

#### Overview
The architecture emphasizes DRY principles through partial reuse, maintains consistent styling via Bootstrap integration, and provides comprehensive format support for both traditional web and API consumers. The templates are designed for maintainability and extensibility, with clear separation of concerns and robust error handling.

  *For additional detailed information, see the summary for src/waf/src/controller/com/sun/j2ee/blueprints/waf.*

### src/waf/src/view/template/com/sun/j2ee/blueprints/waf/view/template

The program implements a complete online shopping experience and showcases J2EE best practices. This sub-project implements a template-based view rendering system along with screen definition management for the MVC architecture.  This provides these capabilities to the Java Pet Store program:

- XML-driven screen definition and templating
- Locale-specific content rendering
- Parameter management for view templates
- Screen state caching and transaction management

#### Identified Design Elements

1. Manages Responses In Both JSON and HTML Format: The base controller class implements an API to allow the requestor to receive the results of the request in either format
2. Complex Request Chaining: More complex requests can be composed of simple atomic elements defined in the widgets folder
3. Automated Form Field Typing: Through introspection of the target data class, the form input controllers are able to validate input types match the intended data format
4. View Chaining: Sub-views do not need to re-query parent data objects, as they are encoded and passed into the chained request

#### Overview
The WAF View Templates system provides a flexible templating framework that separates presentation logic from business logic. It uses XML-based screen definitions loaded by ScreenDefinitionDAO and managed by the Screens class to define UI layouts. The TemplateServlet handles HTTP requests, determines appropriate templates, and renders views with proper locale support. Screen objects store template references and parameters, while the Parameter class manages key-value pairs for template rendering. The architecture emphasizes DRY principles through partial reuse, maintains consistent styling via Bootstrap integration, and provides comprehensive format support for both traditional web and API consumers. The templates are designed for maintainability and extensibility, with clear separation of concerns and robust error handling.

  *For additional detailed information, see the summary for src/waf/src/view/template/com/sun/j2ee/blueprints/waf/view/template.*

### src/waf/src/view/taglibs/com/sun/j2ee/blueprints/taglibs/smart

The library provides reusable UI components that simplify JSP development while enforcing consistent styling and behavior. This sub-project implements advanced form handling with validation capabilities along with client-state management for improved user experience. This provides these capabilities to the Java Pet Store program:

- Custom form components with built-in validation
- Client-state preservation across page requests
- Content caching with configurable scopes and expiration
- Hierarchical tag relationships for complex UI component composition

#### Identified Design Elements

1. **Component Hierarchy System**: Tags are designed to work together in parent-child relationships (e.g., SelectTag with OptionTag, InputTag with ValueTag) to build complex UI components
2. **Automatic Form Validation**: FormTag generates JavaScript validation code for fields registered through its validation system
3. **State Preservation**: ClientStateTag serializes and preserves application state across requests using hidden form fields
4. **Content Caching**: CacheTag provides multi-level caching (context, session, request, page) with configurable expiration policies

#### Overview
The architecture emphasizes component reusability through a hierarchical tag system where child tags communicate with parent tags to build complex UI elements. The library handles common web development challenges like form validation, state management, and content caching. Each tag follows a consistent lifecycle pattern by extending BodyTagSupport and implementing appropriate tag processing methods. The design separates presentation concerns from business logic, allowing developers to create rich interfaces with minimal custom code while maintaining the application's visual consistency.

  *For additional detailed information, see the summary for src/waf/src/view/taglibs/com/sun/j2ee/blueprints/taglibs/smart.*

### src/waf/src/docroot/WEB-INF

The WAF Web Application Configuration sub-project implements the presentation and controller layers of the application through a structured MVC framework. This provides these capabilities to the Java Pet Store program:

- Configurable screen definitions and layouts through XML-based templating
- Event-to-action mappings for handling user interactions
- URL pattern routing to appropriate controller components
- Internationalization support through locale-specific configurations

#### Identified Design Elements

1. MVC Architecture Implementation: Clear separation between presentation (screen definitions), control flow (mappings), and application deployment (web.xml)
2. Template-Based UI Composition: Screen definitions allow for modular UI assembly with reusable components like banners, sidebars, and footers
3. Configurable Event Handling: XML-based event mappings connect user actions to appropriate EJB controller components
4. Internationalization Support: Locale-specific screen definitions enable multilingual presentation

#### Overview
The architecture follows J2EE best practices with MainServlet and TemplateServlet handling request processing and view rendering respectively. The mappings.xml file defines the application's control flow by connecting events to actions and URLs to screens. Screen definitions in screendefinitions_en_US.xml provide a template-based approach to UI composition, promoting reusability and consistent layouts. The web.xml deployment descriptor establishes servlet configurations, session management, and EJB references that bridge the web tier to the business logic layer through the EJBController.

  *For additional detailed information, see the summary for src/waf/src/docroot/WEB-INF.*

## Business Functions

### Web Controller Framework
- `application.xml` : J2EE application deployment descriptor for the BluePrints Web Application Framework (WAF).
- `waf_warruntime.xml` : J2EE deployment descriptor for WAF (Web Application Framework) runtime configuration.
- `build.xml` : Ant build file for the Web Application Framework (WAF) component of Java Pet Store, handling compilation, packaging, and deployment.
- `ejb-jar-manifest.mf` : EJB JAR manifest file specifying class path dependencies for the WAF module.
- `sun-j2ee-ri.xml` : J2EE Reference Implementation configuration file for the Web Application Framework component of Java Pet Store.
- `waf_ejbruntime.xml` : J2EE deployment descriptor for WAF EJB runtime configuration
- `build.bat` : Windows batch script for building the WAF component of Java Pet Store using Apache Ant.
- `build.sh` : Shell script that sets up the environment and runs Ant build tasks for the WAF component of Java Pet Store.
- `ejb-jar.xml` : EJB deployment descriptor for the Web Application Framework controller component in Java Pet Store.
- `controller/com/sun/j2ee/blueprints/waf/controller/web/RequestProcessor.java` : Web tier controller that processes HTTP requests and generates events for the Java Pet Store application.
- `controller/com/sun/j2ee/blueprints/waf/controller/web/WebController.java` : Interface defining the web controller component in the WAF framework for handling HTTP requests and events.
- `controller/com/sun/j2ee/blueprints/waf/controller/web/EventMapping.java` : Maps events to EJB action classes in the WAF controller framework.
- `controller/com/sun/j2ee/blueprints/waf/controller/web/util/WebKeys.java` : Defines constant string keys used for data storage across web-tier components in the WAF framework.
- `controller/com/sun/j2ee/blueprints/waf/controller/web/ComponentManager.java` : Defines the ComponentManager interface for accessing web tier services across components.
- `controller/com/sun/j2ee/blueprints/waf/controller/web/URLMapping.java` : Maps URLs to actions, screens, and flow handlers in the web application framework.
- `controller/com/sun/j2ee/blueprints/waf/controller/web/MainServlet.java` : Front controller servlet that handles HTTP requests and manages application flow in the WAF framework.
- `controller/com/sun/j2ee/blueprints/waf/controller/web/DefaultWebController.java` : A proxy controller that delegates web requests to the EJB tier in the Java Pet Store application.
- `controller/com/sun/j2ee/blueprints/waf/controller/web/DefaultComponentManager.java` : Component manager implementation that provides access to web and EJB tier services in the WAF framework.
- `controller/com/sun/j2ee/blueprints/waf/controller/web/URLMappingsXmlDAO.java` : XML data access object for loading URL mappings and screen flow configuration from XML files.

### EJB Controller
- `controller/com/sun/j2ee/blueprints/waf/controller/ejb/EJBControllerLocalEJB.java` : EJB controller implementation for handling events in the Web Application Framework.
- `controller/com/sun/j2ee/blueprints/waf/controller/ejb/StateMachine.java` : A state machine that processes events from the client tier by mapping them to appropriate EJB actions.
- `controller/com/sun/j2ee/blueprints/waf/controller/ejb/action/EJBActionSupport.java` : Abstract base class for EJB actions in the WAF controller framework.
- `controller/com/sun/j2ee/blueprints/waf/controller/ejb/action/actions/ChangeLocaleEJBAction.java` : Handles locale change events in the Java Pet Store application's web application framework.
- `controller/com/sun/j2ee/blueprints/waf/controller/ejb/action/EJBAction.java` : Defines the EJBAction interface for handling events in the WAF controller framework.
- `controller/com/sun/j2ee/blueprints/waf/controller/ejb/EJBControllerLocal.java` : Defines the local EJB interface for the controller component in the Web Application Framework.
- `controller/com/sun/j2ee/blueprints/waf/controller/ejb/EJBControllerLocalHome.java` : Local home interface for EJB controller component in the Web Application Framework.

### Event System
- `controller/com/sun/j2ee/blueprints/waf/event/impl/ChangeLocaleEvent.java` : Event class for notifying the EJBController about locale changes in the web application framework.
- `controller/com/sun/j2ee/blueprints/waf/event/EventException.java` : A base exception class for event-related errors in the WAF controller framework.
- `controller/com/sun/j2ee/blueprints/waf/event/EventSupport.java` : Base class for all application events in the WAF framework with EJB action class support.
- `controller/com/sun/j2ee/blueprints/waf/event/EventResponse.java` : Defines the EventResponse interface for WAF event handling in the Java Pet Store application.
- `controller/com/sun/j2ee/blueprints/waf/event/EventResponseSupport.java` : Abstract support class for event responses in the WAF controller framework.
- `controller/com/sun/j2ee/blueprints/waf/event/Event.java` : Defines the Event interface for the Web Application Framework (WAF) event system.

### HTML Action Framework
- `controller/com/sun/j2ee/blueprints/waf/controller/web/action/HTMLAction.java` : Defines the HTMLAction interface for handling web requests in the WAF controller framework.
- `controller/com/sun/j2ee/blueprints/waf/controller/web/action/HTMLActionException.java` : Custom exception class for HTML action processing errors in the web controller framework.
- `controller/com/sun/j2ee/blueprints/waf/controller/web/action/actions/ChangeLocaleHTMLAction.java` : Handles locale changes in the web application by processing user language selection requests.
- `controller/com/sun/j2ee/blueprints/waf/controller/web/action/HTMLActionSupport.java` : Abstract support class for HTML actions in the web controller framework.

### Flow Control
- `controller/com/sun/j2ee/blueprints/waf/controller/web/flow/FlowHandler.java` : Defines the FlowHandler interface for managing web application flow control in the WAF framework.
- `controller/com/sun/j2ee/blueprints/waf/controller/web/flow/FlowHandlerException.java` : Custom exception class for flow handler processing errors in the web application framework.
- `controller/com/sun/j2ee/blueprints/waf/controller/web/flow/ScreenFlowData.java` : Defines data structure for managing screen flow and exception handling in the web controller.
- `controller/com/sun/j2ee/blueprints/waf/controller/web/flow/handlers/ClientStateFlowHandler.java` : Handles client-side state management by decoding Base64-encoded parameters from web pages.
- `controller/com/sun/j2ee/blueprints/waf/controller/web/flow/ScreenFlowManager.java` : Manages web application screen flow and URL mappings for navigation control

### Template System
- `view/template/com/sun/j2ee/blueprints/waf/view/template/Parameter.java` : Represents a key-value parameter with direct/indirect flag for template processing.
- `view/template/com/sun/j2ee/blueprints/waf/view/template/Screens.java` : Manages screen definitions and their associated templates for the web application framework.
- `view/template/com/sun/j2ee/blueprints/waf/view/template/ScreenDefinitionDAO.java` : Data access object for parsing XML screen definition files that define UI templates and navigation flow in the WAF framework.
- `view/template/com/sun/j2ee/blueprints/waf/view/template/TemplateServlet.java` : Servlet that implements a template system for web page rendering based on screen definitions loaded from XML files.
- `view/template/com/sun/j2ee/blueprints/waf/view/template/tags/InsertTag.java` : Custom JSP tag that inserts template content or direct text into JSP pages.
- `view/template/com/sun/j2ee/blueprints/waf/view/template/Screen.java` : Represents a screen component in the web application framework's templating system.

### Custom Tag Libraries
- `view/taglibs/com/sun/j2ee/blueprints/taglibs/smart/OptionTag.java` : Custom JSP tag implementation for HTML option elements within select dropdowns.
- `view/taglibs/com/sun/j2ee/blueprints/taglibs/smart/SelectedTag.java` : A JSP tag that defines what should be selected for an 'option' tag in HTML forms.
- `view/taglibs/com/sun/j2ee/blueprints/taglibs/smart/NameTag.java` : Custom JSP tag that sets the 'name' attribute for InputTag components.
- `view/taglibs/com/sun/j2ee/blueprints/taglibs/smart/ClientStateValueTag.java` : JSP tag handler for storing name-value pairs in client state management.
- `view/taglibs/com/sun/j2ee/blueprints/taglibs/smart/ClientStateTag.java` : JSP tag that preserves client state by encoding request attributes and parameters as hidden form fields.
- `view/taglibs/com/sun/j2ee/blueprints/taglibs/smart/InputTag.java` : Custom JSP tag for generating HTML input elements with validation support.
- `view/taglibs/com/sun/j2ee/blueprints/taglibs/smart/CheckedTag.java` : A JSP tag that determines whether a checkbox should be checked based on body content evaluation.
- `view/taglibs/com/sun/j2ee/blueprints/taglibs/smart/ValueTag.java` : JSP tag handler that sets the value attribute for an input tag from its body content.
- `view/taglibs/com/sun/j2ee/blueprints/taglibs/smart/FormTag.java` : Custom JSP tag that generates HTML forms with client-side JavaScript validation.
- `view/taglibs/com/sun/j2ee/blueprints/taglibs/smart/CheckboxTag.java` : Custom JSP tag for rendering HTML checkbox inputs in web forms.
- `view/taglibs/com/sun/j2ee/blueprints/taglibs/smart/CacheTag.java` : JSP tag implementation for caching content in different scopes with expiration support.
- `view/taglibs/com/sun/j2ee/blueprints/taglibs/smart/SelectTag.java` : A JSP tag library component that renders HTML select dropdown menus with configurable options.

### Configuration
- `docroot/WEB-INF/xml/requestmappings.xml` : XML configuration file mapping URL patterns to screens and handlers in the Java Pet Store application.
- `docroot/WEB-INF/xml/screendefinitions.xml` : XML configuration file defining screen layouts and components for the Java Pet Store web application.
- `docroot/WEB-INF/mappings.xml` : XML configuration file defining event and URL mappings for the Web Application Framework (WAF).
- `docroot/WEB-INF/screendefinitions_en_US.xml` : XML configuration file defining screen layouts and templates for the WAF component of Java Pet Store.
- `docroot/WEB-INF/web.xml` : Web application deployment descriptor for the Web Application Framework component of Java Pet Store.

### Utility Classes
- `controller/com/sun/j2ee/blueprints/waf/util/I18nUtil.java` : Utility class providing internationalization functions for formatting and parsing in locale-specific ways.
- `controller/com/sun/j2ee/blueprints/waf/util/JNDINames.java` : Defines JNDI name constants for accessing EJB components in the WAF framework.

### Exception Handling
- `controller/com/sun/j2ee/blueprints/waf/exceptions/GeneralFailureException.java` : A base exception class for web runtime errors in the WAF framework.
- `controller/com/sun/j2ee/blueprints/waf/exceptions/AppException.java` : A simple exception class that extends RuntimeException for application-level error handling.

## Files
### application.xml

This XML file serves as the J2EE application deployment descriptor for the BluePrints Web Application Framework (WAF). It defines the structure and components of the WAF application following the J2EE 1.3 specification. The file declares two modules: an EJB module (waf-ejb.jar) and a web module (waf.war) with context root 'waf'. It includes a display name and description that identifies the WAF as a comprehensive framework for building J2EE applications.

 **Code Landmarks**
- `Line 42`: Uses the J2EE Application 1.3 DTD specification for the deployment descriptor structure
- `Line 45`: Establishes the BluePrints WAF as a complete framework rather than just a component library
### build.bat

This batch script sets up and executes Apache Ant to build the WAF (Web Application Framework) component of the Java Pet Store application on Windows systems. It configures the necessary environment variables including ANT_HOME and constructs the ANT_CLASSPATH with required JAR files from Ant, Java tools, and J2EE. The script then invokes the Java runtime with the configured classpath to execute Ant's Main class, passing along any command-line arguments. This enables developers to build the WAF component consistently in Windows environments with the proper dependencies.

 **Code Landmarks**
- `Line 39`: Sets up a comprehensive classpath that includes Ant libraries, Java tools, and J2EE dependencies
- `Line 41`: Passes environment variables as system properties to the Ant process
### build.sh

This shell script serves as a build utility for the Web Application Framework (WAF) component of Java Pet Store. It checks for required environment variables (JAVA_HOME and J2EE_HOME), sets up the classpath for Apache Ant, and executes the Ant build tool. The script first validates the Java installation, then configures the Ant environment with necessary JAR files including ant.jar, parser.jar, jaxp.jar, tools.jar, and j2ee.jar. Finally, it invokes the Java command to run Ant with the specified command-line arguments, passing along the J2EE home directory as a system property.

 **Code Landmarks**
- `Line 42`: Script uses a fallback mechanism to locate Java by checking PATH if JAVA_HOME isn't set
- `Line 58`: Builds a comprehensive classpath that combines Ant libraries with J2EE libraries for building enterprise components
### build.xml

This build.xml file defines the Ant build process for the Web Application Framework (WAF) component of Java Pet Store. It manages compilation of controller classes, view templates, and tag libraries, and packages them into appropriate JAR, WAR, and EAR files for deployment. Key targets include 'init' (setting up properties), 'compile', 'ejbjar', 'ejbclientjar', 'war', 'ear', 'docs' (generating JavaDocs), 'clean', and 'deploy'. The file defines important properties for directory structures, classpath configurations, and dependency relationships between components like the service locator and tracer.

 **Code Landmarks**
- `Line 145`: Creates a combined WAF web JAR by first unpacking a Base64 encoder/decoder JAR into the class directory
- `Line 184`: Builds a complete J2EE Enterprise Archive (EAR) containing EJB JARs, WAR files, and supporting components
- `Line 202`: Uses the J2EE deployment tool programmatically through a Java task rather than using a dedicated Ant task
### controller/com/sun/j2ee/blueprints/waf/controller/ejb/EJBControllerLocal.java

EJBControllerLocal interface defines the local EJB interface for the controller component in the MVC architecture of the Web Application Framework. It extends EJBLocalObject and serves as the EJB-tier controller that manages client session activities. The interface declares a single method, processEvent(), which accepts an Event object and returns an EventResponse, potentially throwing an EventException. This method is responsible for feeding events to the business logic state machine and processing them accordingly.

 **Code Landmarks**
- `Line 53`: Interface extends EJBLocalObject to function as a local EJB component within the J2EE architecture
### controller/com/sun/j2ee/blueprints/waf/controller/ejb/EJBControllerLocalEJB.java

EJBControllerLocalEJB implements a session bean that serves as part of the controller layer in the Web Application Framework (WAF). It processes application events through a StateMachine, which handles the event flow logic. The class implements the SessionBean interface with standard EJB lifecycle methods (ejbCreate, ejbRemove, ejbActivate, ejbPassivate). The key functionality is the processEvent method that delegates to the StateMachine to handle events and return appropriate responses. The class maintains references to the StateMachine and SessionContext objects.

 **Code Landmarks**
- `Line 56`: The StateMachine is initialized during ejbCreate(), establishing the event processing infrastructure for the controller.
- `Line 62`: The processEvent method serves as the primary entry point for event handling, delegating to the StateMachine component.
### controller/com/sun/j2ee/blueprints/waf/controller/ejb/EJBControllerLocalHome.java

EJBControllerLocalHome interface defines the local home interface for the EJB controller component in the Web Application Framework (WAF). This interface extends EJBLocalHome and provides a single create method that returns an EJBControllerLocal object. The create method throws a CreateException if the controller cannot be created. This interface is part of the controller layer in the WAF architecture, enabling local access to the controller EJB within the same JVM.

 **Code Landmarks**
- `Line 47`: Defines a local EJB interface rather than a remote one, indicating this controller is meant for same-JVM access, optimizing performance by avoiding remote call overhead.
### controller/com/sun/j2ee/blueprints/waf/controller/ejb/StateMachine.java

StateMachine implements a component responsible for processing events received from the client tier in the WAF framework. It dynamically connects EJB components at runtime, enabling reusable component support. The class maintains state in an attributeMap and handles events by looking up appropriate EJBAction implementations based on event names. Key functionality includes processing events through the appropriate action handlers, managing attributes, and providing access to the EJB controller. Important methods include processEvent(), setAttribute(), getAttribute(), getEJBController(), and getSessionContext().

 **Code Landmarks**
- `Line 78`: The class uses a HashMap to cache action instances for better performance rather than instantiating them on each request
- `Line 86`: Dynamic class loading and instantiation pattern allows for extensibility without modifying the state machine code
- `Line 91`: The action lifecycle includes doStart(), perform(), and doEnd() phases, creating a well-defined execution flow
### controller/com/sun/j2ee/blueprints/waf/controller/ejb/action/EJBAction.java

EJBAction interface defines the contract for action classes that handle events within the Web Application Framework (WAF) controller. It provides a lifecycle for event processing with initialization, start, perform, and end methods. The interface requires implementations to interact with a StateMachine for state management and to process Event objects, returning appropriate EventResponse objects. The perform method can throw EventException when errors occur during event processing. This interface is a core component of the controller's event handling mechanism in the J2EE Blueprints architecture.

 **Code Landmarks**
- `Line 47`: The interface defines a clear lifecycle for actions with init, doStart, perform, and doEnd methods
### controller/com/sun/j2ee/blueprints/waf/controller/ejb/action/EJBActionSupport.java

EJBActionSupport implements an abstract base class for EJB actions in the Web Application Framework (WAF) controller. It provides a foundation for action classes that interact with the StateMachine component. The class implements both the java.io.Serializable interface and the EJBAction interface. Key functionality includes initialization with a StateMachine reference and empty lifecycle methods (doStart and doEnd) that subclasses can override to implement specific behavior when actions begin and complete. The protected StateMachine field allows subclasses to access state transition functionality.

 **Code Landmarks**
- `Line 42`: Implementation of both Serializable and EJBAction interfaces suggests these actions need to be persisted and follow a specific contract
### controller/com/sun/j2ee/blueprints/waf/controller/ejb/action/actions/ChangeLocaleEJBAction.java

ChangeLocaleEJBAction implements an action handler for locale changes in the web application framework. It extends EJBActionSupport and overrides the perform method to process ChangeLocaleEvent objects. When a locale change event is received, the action extracts the new locale from the event and stores it as an attribute named 'locale' in the state machine. The class is part of the controller layer in the MVC architecture of the web application framework, enabling internationalization support throughout the application.

 **Code Landmarks**
- `Line 47`: The action doesn't return an EventResponse (returns null), indicating this action doesn't require any specific response handling.
### controller/com/sun/j2ee/blueprints/waf/controller/web/ComponentManager.java

ComponentManager interface defines services that need to be accessed from multiple components in the web tier. It extends HttpSessionListener to ensure it's loaded at startup. The interface is designed for implementations that initialize objects used in the presentation tier. It declares a single method getWebController() that retrieves a WebController instance from an HttpSession, with the controller intended to be stateless and not tied to any specific user.

 **Code Landmarks**
- `Line 53`: Interface extends HttpSessionListener specifically to ensure the implementing class is loaded at application startup
### controller/com/sun/j2ee/blueprints/waf/controller/web/DefaultComponentManager.java

DefaultComponentManager implements a component manager that provides access to web and EJB tier services in the Java Pet Store application. It manages the creation and retrieval of WebController and EJBControllerLocal instances tied to HTTP sessions. Key functionality includes initializing controllers, handling session lifecycle events, and providing a centralized access point for application components. Important methods include getWebController(), getEJBController(), sessionCreated(), and sessionDestroyed(). The class uses ServiceLocator to obtain configuration values and EJB homes, and implements HttpSessionListener to respond to session lifecycle events.

 **Code Landmarks**
- `Line 75`: Uses Java Beans instantiation pattern to dynamically create controller instances based on configuration
- `Line 89`: Implements session-scoped caching of EJB controller references to improve performance
- `Line 114`: Silently suppresses exceptions during session destruction to prevent errors during application shutdown
### controller/com/sun/j2ee/blueprints/waf/controller/web/DefaultWebController.java

DefaultWebController implements a proxy class that bridges HTTP requests to the EJB tier in the WAF (Web Application Framework). It handles web events by delegating them to an EJBControllerLocal instance managed by a ComponentManager. The class provides initialization with an HttpSession, synchronized event handling to prevent concurrent requests to the stateful session bean, and proper resource cleanup through the destroy method. Key methods include init(HttpSession), handleEvent(Event, HttpSession), and destroy(HttpSession). The controller retrieves the EJBController from the session's ComponentManager to process events and manage the application's business logic.

 **Code Landmarks**
- `Line 82`: Synchronized method prevents concurrent requests to the stateful session bean
- `Line 95`: Ignores RemoveException during cleanup, showing a pragmatic approach to error handling during session termination
### controller/com/sun/j2ee/blueprints/waf/controller/web/EventMapping.java

EventMapping implements a simple mapping class that associates event class names with their corresponding EJB action class names in the Web Application Framework (WAF) controller. The class stores two string properties: eventClass and ejbActionClass. It provides a constructor to initialize these properties and getter methods to retrieve them. The class also implements java.io.Serializable to support object serialization and includes a toString() method for debugging purposes. This mapping is likely used by the controller to determine which EJB action to invoke when processing specific events.
### controller/com/sun/j2ee/blueprints/waf/controller/web/MainServlet.java

MainServlet implements a front controller servlet for the Web Application Framework (WAF). It processes HTTP requests, manages URL mappings, and controls screen flow. The servlet initializes by loading URL and event mappings from XML configuration, then processes requests through a RequestProcessor and forwards to appropriate views via ScreenFlowManager. Key functionality includes locale handling, exception management, and maintaining application context attributes. Important components include the doProcess method, getRequestProcessor, getScreenFlowManager, and getURLMapping methods, along with urlMappings and eventMappings HashMaps.

 **Code Landmarks**
- `Line 76`: Loads URL and event mappings from XML configuration during initialization
- `Line 105`: Implements session-based locale management with configurable defaults
- `Line 110`: Uses a chain of responsibility pattern with RequestProcessor and ScreenFlowManager
- `Line 115`: Implements centralized exception handling with dynamic error screen resolution
- `Line 132`: Uses lazy initialization pattern for application components
### controller/com/sun/j2ee/blueprints/waf/controller/web/RequestProcessor.java

RequestProcessor implements a web tier controller responsible for processing HTTP requests from Main.jsp and generating events to modify data. It maps URLs to appropriate actions and events to EJB actions using configuration from ServletContext. Key functionality includes initializing URL and event mappings, processing requests by finding the appropriate HTMLAction class, executing the action, handling any resulting events through the WebController, and managing the request lifecycle. Important methods include processRequest(), getURLMapping(), getEventMapping(), and getAction(). The class uses URLMapping and EventMapping objects to determine which actions to execute for specific URLs and events.

 **Code Landmarks**
- `Line 115`: Core request processing method that handles the complete request lifecycle from URL parsing to event handling
- `Line 94`: URL pattern matching system that maps incoming request URLs to appropriate action handlers
- `Line 106`: Event mapping system that associates event classes with corresponding EJB action classes
### controller/com/sun/j2ee/blueprints/waf/controller/web/URLMapping.java

URLMapping implements a class that maps URLs to specific actions and screens in the web application framework. It stores information about whether a URL represents an action, requires a flow handler, and maintains mappings between action results and target screens. The class provides methods to retrieve the associated web action class, flow handler, screen information, and result mappings. Key fields include url, isAction, useFlowHandler, webActionClass, flowHandler, resultMappings, and screen. Important methods include getWebAction(), getFlowHandler(), getScreen(), and getResultScreen(String).

 **Code Landmarks**
- `Line 52`: Constructor overloading allows creating both simple screen mappings and complex action mappings
- `Line 84`: Result mapping lookup allows dynamic navigation based on action outcomes
### controller/com/sun/j2ee/blueprints/waf/controller/web/URLMappingsXmlDAO.java

URLMappingsXmlDAO provides functionality to parse and load URL mappings, event mappings, and exception mappings from XML configuration files for the web application framework. It uses DOM parsing to extract mapping information that defines the application's navigation flow, screen transitions, and exception handling. Key methods include loadDocument(), loadScreenFlowData(), loadRequestMappings(), loadEventMappings(), and loadExceptionMappings(). The class defines numerous constants for XML element and attribute names, and implements helper methods for XML node traversal and value extraction. The parsed data is used by the ScreenFlowManager to control application navigation flow.

 **Code Landmarks**
- `Line 116`: Uses DOM parsing to extract configuration data from XML files that define the application's navigation flow
- `Line 179`: Creates a HashMap of exception mappings that link exception class names to screen identifiers for error handling
- `Line 198`: Builds event mappings that connect event class names to EJB action classes
- `Line 209`: Complex method that parses URL mappings with support for flow handlers and result mappings
### controller/com/sun/j2ee/blueprints/waf/controller/web/WebController.java

WebController interface defines the contract for web controllers in the Web Application Framework (WAF). It serves as a proxy between HTTP requests and the EJB tier, handling events from the web tier and translating them to business logic operations. The interface declares three essential methods: init() for initializing the controller with an HTTP session, handleEvent() for processing events and returning appropriate responses, and destroy() for cleaning up resources. The controller ensures thread safety by synchronizing access to the EJB tier, preventing concurrent requests to stateful session beans.

 **Code Landmarks**
- `Line 59`: The interface extends java.io.Serializable, suggesting controllers need to be serializable for session storage
- `Line 51`: Comment indicates this is a proxy pattern implementation to the EJB tier with synchronized methods for thread safety
- `Line 75`: handleEvent method serves as the core event processing mechanism, connecting web events to business logic
### controller/com/sun/j2ee/blueprints/waf/controller/web/action/HTMLAction.java

HTMLAction interface defines the base contract for request handlers in the web tier of the Web Application Framework (WAF). It specifies four key methods that implementing classes must provide: setServletContext for context initialization, doStart for pre-processing requests, perform for executing the main action logic and returning an Event, and doEnd for post-processing after event handling. The interface extends Serializable, allowing implementing classes to be serialized if needed. This interface serves as the foundation for the command pattern implementation in the web controller framework.

 **Code Landmarks**
- `Line 52`: The interface extends Serializable, enabling implementing action classes to be persisted across HTTP sessions if needed.
- `Line 55`: The perform method returns an Event object, demonstrating the event-driven architecture of the WAF framework.
### controller/com/sun/j2ee/blueprints/waf/controller/web/action/HTMLActionException.java

HTMLActionException implements a serializable exception class used in the web application framework (WAF) controller component. This exception is specifically thrown when errors occur during flow handler processing in HTML actions. The class extends the standard Java Exception class and implements Serializable interface for proper exception handling across distributed systems. It provides two constructors: a default no-argument constructor and one that accepts a string message parameter to provide error details. This exception helps in managing error conditions within the action processing pipeline of the web controller framework.
### controller/com/sun/j2ee/blueprints/waf/controller/web/action/HTMLActionSupport.java

HTMLActionSupport implements an abstract base class that serves as the default implementation of the HTMLAction interface in the web application framework (WAF). It provides a foundation for handling HTML-based actions in the controller layer. The class defines core functionality including servlet context management and lifecycle methods for action processing. Key methods include setServletContext() to store the servlet context, and empty implementations of doStart() and doEnd() methods that subclasses can override to implement specific action behavior before and after event processing.

 **Code Landmarks**
- `Line 47`: This abstract class implements the HTMLAction interface but provides empty implementations of lifecycle methods, following the Template Method pattern
### controller/com/sun/j2ee/blueprints/waf/controller/web/action/actions/ChangeLocaleHTMLAction.java

ChangeLocaleHTMLAction implements an action class that processes user requests to change the application's locale/language. It extends HTMLActionSupport and implements the perform method which extracts the locale parameter from HTTP requests, converts it to a Locale object using I18nUtil, stores it in the user's session, and returns a ChangeLocaleEvent. If the locale conversion fails, it throws an HTMLActionException. The class is part of the WAF (Web Application Framework) controller component that manages locale-related user interactions in the Java Pet Store application.

 **Code Landmarks**
- `Line 71`: Uses I18nUtil utility class to convert string representation of locale to a Locale object
- `Line 75`: Stores locale in session with a constant key for application-wide access
- `Line 76`: Returns a ChangeLocaleEvent that propagates the locale change throughout the application
### controller/com/sun/j2ee/blueprints/waf/controller/web/flow/FlowHandler.java

FlowHandler interface defines the core contract for flow handlers in the web tier of the Web Application Framework (WAF). It extends Serializable and provides three essential methods: doStart() for initialization, processFlow() for handling request processing and determining the next view, and doEnd() for cleanup operations. These methods enable structured navigation flow control within web applications, with processFlow() potentially throwing FlowHandlerException when errors occur during flow processing.

 **Code Landmarks**
- `Line 51`: The interface extends Serializable, allowing flow handlers to be persisted across HTTP sessions
### controller/com/sun/j2ee/blueprints/waf/controller/web/flow/FlowHandlerException.java

FlowHandlerException implements a simple exception class used within the web application framework's flow control system. This serializable exception extends the standard Java Exception class to represent errors that occur during flow handler processing. The class provides two constructors: a default no-argument constructor and one that accepts a string message to be passed to the parent Exception class. This specialized exception helps identify and handle flow-related errors within the controller component of the web application framework.
### controller/com/sun/j2ee/blueprints/waf/controller/web/flow/ScreenFlowData.java

ScreenFlowData implements a serializable class that stores information needed for managing screen flow in the web controller framework. It maintains a mapping between exceptions and their corresponding screen destinations, along with a default screen to display. The class provides methods to access the default screen and exception mappings. Key components include the exceptionMappings HashMap that stores exception-to-screen relationships, the defaultScreen String for fallback navigation, and accessor methods getDefaultScreen() and getExceptionMappings() to retrieve these values.

 **Code Landmarks**
- `Line 43`: Implements Serializable to support state persistence across HTTP requests in the web application flow
### controller/com/sun/j2ee/blueprints/waf/controller/web/flow/ScreenFlowManager.java

ScreenFlowManager implements navigation control for a web application by mapping URLs to screens and flow handlers. It loads URL mappings from XML configuration files, determines which screen to display based on request URLs, and handles navigation flow. The class supports both direct screen mappings and dynamic flow handling through FlowHandler implementations. It also provides exception handling by mapping exception types to appropriate error screens. Key methods include forwardToNextScreen(), getURLMapping(), getExceptionScreen(), and getCurrentScreen(). Important variables include urlMappings, exceptionMappings, and defaultScreen.

 **Code Landmarks**
- `Line 94`: Uses XML configuration files to define URL mappings and screen flow, enabling declarative navigation control
- `Line 126`: Dynamic class loading of FlowHandler implementations allows for extensible navigation logic
- `Line 167`: Uses Java reflection (isAssignableFrom) to match exception hierarchies for error handling
### controller/com/sun/j2ee/blueprints/waf/controller/web/flow/handlers/ClientStateFlowHandler.java

ClientStateFlowHandler implements FlowHandler to manage client-side state persistence in web applications. It primarily decodes and deserializes Base64-encoded parameters that were previously embedded in web pages using ClientCacheLinkTag. The class processes HTTP requests by extracting encoded attributes, deserializing them, and restoring them as request attributes. Key methods include doStart(), processFlow(), and doEnd(), with processFlow() containing the core functionality for parameter extraction and deserialization. The class uses Apache Commons Base64 for decoding and standard Java serialization for object reconstruction.

 **Code Landmarks**
- `Line 65`: Uses Apache Commons Base64 decoder to handle encoded state parameters
- `Line 71`: Implements object deserialization from Base64-encoded strings to restore client state
- `Line 74`: Dynamically reconstructs request attribute names by stripping prefix patterns
### controller/com/sun/j2ee/blueprints/waf/controller/web/util/WebKeys.java

WebKeys defines a collection of public static final String constants that serve as standardized keys for storing and retrieving data across different scopes of the web-tier in the WAF framework. These keys are used to access components like managers, controllers, screen information, URL data, and user preferences in various application contexts. The class prevents instantiation through a protected constructor. The constants defined include references to component managers, screen flow managers, request processors, current/previous screens, URL mappings, locale settings, and controller references.

 **Code Landmarks**
- `Line 45`: Protected constructor prevents instantiation, ensuring the class functions purely as a constants container
- `Line 47-67`: Constants follow a consistent naming convention with fully-qualified package prefixes to avoid namespace collisions
### controller/com/sun/j2ee/blueprints/waf/event/Event.java

Event interface defines the contract for events in the Web Application Framework (WAF) event system. It extends Serializable to enable events to be passed between system tiers. The interface requires implementing classes to provide methods for setting and retrieving an EJB action class name that processes the event in the EJB tier, as well as a method to get the event name. This interface serves as the foundation for the event-driven communication mechanism in the application.

 **Code Landmarks**
- `Line 43`: The interface extends Serializable, enabling events to be transmitted across JVM boundaries or persisted
### controller/com/sun/j2ee/blueprints/waf/event/EventException.java

EventException implements a base exception class for all event-related exceptions in the Web Application Framework (WAF) controller. This simple class extends Java's standard Exception class and implements the Serializable interface to support serialization. It provides two constructors: a default no-argument constructor and one that accepts a String message parameter which is passed to the parent Exception class. The class serves as a foundation for more specific event exceptions within the application's event handling system.
### controller/com/sun/j2ee/blueprints/waf/event/EventResponse.java

EventResponse interface establishes the contract for event responses within the Web Application Framework (WAF) of Java Pet Store. It extends Serializable to enable event responses to be serialized across the application. The interface defines a single method, getEventName(), which returns a logical name that maps to events in the StateMachine. This mapping facilitates event handling and processing within the application's state management system. The interface serves as a foundation for implementing various event response types in the application's event-driven architecture.

 **Code Landmarks**
- `Line 42`: The interface extends Serializable, enabling event responses to be transmitted across JVM boundaries or persisted
### controller/com/sun/j2ee/blueprints/waf/event/EventResponseSupport.java

EventResponseSupport implements a convenience abstract class for event responses in the Web Application Framework (WAF). It provides a basic implementation of the EventResponse interface with payload management functionality. The class maintains a private payload object that can be set through the constructor and retrieved via the getPayload() method. This simple implementation serves as a foundation for concrete event response classes to extend, providing a standard way to handle response data in the event-driven architecture of the application.

 **Code Landmarks**
- `Line 46`: The class uses a generic Object type for payload, allowing flexibility in the types of data that can be passed through event responses
### controller/com/sun/j2ee/blueprints/waf/event/EventSupport.java

EventSupport implements the base class for all events used in the application, serving as the foundation for the event system in the Web Application Framework (WAF). It implements the Event interface and provides functionality to associate events with EJB action classes (commands) that will process them. The class maintains an ejbActionClassName field that specifies which EJB command should handle the event, with null indicating no EJB command processing is needed. Key methods include getEJBActionClassName(), setEJBActionClassName(), and getEventName(), which returns null by default and is meant to be overridden by subclasses.

 **Code Landmarks**
- `Line 48`: The class implements Serializable (via Event interface) specifically to support RMI-IIOP communication with EJB containers
- `Line 54`: The ejbActionClassName field enables a command pattern implementation where events are associated with their handlers
### controller/com/sun/j2ee/blueprints/waf/event/impl/ChangeLocaleEvent.java

ChangeLocaleEvent implements an event class that extends EventSupport to notify the EJBController when a user changes their locale preference. The class stores a Locale object and provides methods to retrieve it. It's part of the web application framework's event system for handling internationalization changes. Key functionality includes storing the locale information and providing access to it through getLocale(). The class also implements getEventName() which returns a JNDI name used to identify this event type in the event handling system. Important elements include the locale instance variable, the constructor that initializes it, and the two getter methods.

 **Code Landmarks**
- `Line 47`: Uses JNDI naming convention for event identification, showing integration with J2EE component architecture
### controller/com/sun/j2ee/blueprints/waf/exceptions/AppException.java

AppException implements a basic exception class that extends RuntimeException for the Web Application Framework (WAF) component of the Java Pet Store application. This class provides a minimal implementation with just a single constructor that accepts a string message parameter and passes it to the parent RuntimeException class. The class serves as a foundation for application-specific exception handling within the WAF controller layer, allowing developers to throw runtime exceptions with custom error messages.
### controller/com/sun/j2ee/blueprints/waf/exceptions/GeneralFailureException.java

GeneralFailureException implements a base exception class for web runtime errors in the Java Pet Store's Web Application Framework (WAF). It extends RuntimeException and implements Serializable, providing a foundation for all web-related runtime exceptions. The class offers two constructors: one accepting only an error message string and another accepting both a message and a Throwable object. It also includes a getThrowable() method that returns a formatted message containing the nested exception's message. This class serves as a common error handling mechanism throughout the WAF component.

 **Code Landmarks**
- `Line 46`: The class implements Serializable to allow exception objects to be passed between JVM instances or persisted
### controller/com/sun/j2ee/blueprints/waf/util/I18nUtil.java

I18nUtil implements a utility class for internationalization operations in the Web Application Framework. It provides methods for currency and number formatting with locale-specific patterns, Japanese character encoding conversion, keyword parsing from strings using locale-aware word boundaries, and locale object creation from string representations. Key methods include convertJISEncoding(), formatCurrency(), formatNumber(), parseKeywords(), and getLocaleFromString(). The class handles precision control for numeric displays and supports word boundary detection across different languages, making it essential for multilingual application support.

 **Code Landmarks**
- `Line 54`: Converts Japanese character encodings (SJIS/JIS) to Unicode strings with proper encoding detection
- `Line 76`: Implements locale-specific currency formatting with custom precision and pattern control
- `Line 112`: Uses BreakIterator for locale-aware word boundary detection when parsing keywords
- `Line 156`: Parses locale strings in language_country_variant format to create Locale objects
### controller/com/sun/j2ee/blueprints/waf/util/JNDINames.java

JNDINames is a utility class that centralizes JNDI name constants used throughout the Web Application Framework (WAF). It provides static final String constants for accessing various enterprise components via JNDI lookups. The class includes constants for the EJB controller home interface, component manager, and default web controller. A private constructor prevents instantiation of this utility class. These constants ensure consistent JNDI naming across the application and must be synchronized with deployment descriptors when modified.

 **Code Landmarks**
- `Line 45`: Private constructor prevents instantiation of this utility class, enforcing its static-only usage pattern
- `Line 48-49`: Constants use the java:comp/env/ namespace prefix, following J2EE best practices for portable JNDI lookups
### docroot/WEB-INF/mappings.xml

This mappings.xml file configures the Web Application Framework (WAF) component of Java Pet Store by defining event-to-action and URL-to-screen mappings. It establishes relationships between event classes and their corresponding EJB action handlers, as well as URL patterns and their web action classes. The file includes mappings for locale change functionality through different endpoints ('changelocale.do' and 'changeclientchachelinklocale.do'), with the latter using a flow handler. It also defines an exception mapping that routes GeneralFailureException to an error screen.

 **Code Landmarks**
- `Line 39`: Defines event-mapping that connects ChangeLocaleEvent to its EJB action implementation
- `Line 49`: Implements flow handler integration for client-side state management with the 'useFlowHandler' attribute
### docroot/WEB-INF/screendefinitions_en_US.xml

This XML configuration file defines screen layouts for the Web Application Framework (WAF) component of Java Pet Store. It establishes a default template and defines multiple screen configurations that specify UI components like banner, sidebar, body, and footer JSPs for different application pages. The file includes a special template definition and screen definitions for various functionality including locale handling, tag libraries, templating, and form components. Each screen definition contains parameters that map UI elements to specific JSP files, with titles provided as direct values.

 **Code Landmarks**
- `Line 39`: Defines a default template that applies to all screens unless overridden
- `Line 41-43`: Defines a named template 'special' that can be referenced by specific screens
- `Line 77`: Screen definition explicitly references the 'special' template, overriding the default
### docroot/WEB-INF/web.xml

This web.xml deployment descriptor configures the Web Application Framework (WAF) component of Java Pet Store. It defines servlet mappings, listeners, and environment entries essential for the web tier functionality. Key configurations include the MainServlet as the web tier entry point, TemplateServlet for screen rendering, and component manager setup. The file establishes URL patterns (.do and .screen extensions), session timeout settings, and EJB references that connect the web tier to the business logic layer through the EJBController.

 **Code Landmarks**
- `Line 50`: DefaultComponentManager listener registration enables application-wide component management
- `Line 60`: MainServlet configuration with default locale parameter establishes the central controller for handling web requests
- `Line 71`: TemplateServlet configuration with caching parameters controls the template-based view rendering system
- `Line 123`: Component Manager implementation class is defined as an environment entry for flexible implementation switching
- `Line 138`: EJB local reference establishes connection between web tier and EJB controller
### docroot/WEB-INF/xml/requestmappings.xml

This XML configuration file defines the request mappings for the Java Pet Store web application. It maps URL patterns to specific screens and handlers, controlling navigation flow throughout the application. The file specifies default screens, sign-in requirements, language-specific screen definitions, and error handling. It configures request handlers and flow handlers for processing user actions like sign-in, checkout, account creation, and order placement. Each URL mapping can specify whether authentication is required and which Java handler classes should process the request. The file also includes exception mappings to direct users to appropriate error screens when exceptions occur.

 **Code Landmarks**
- `Line 42-43`: Defines language-specific screen definition files, supporting internationalization
- `Line 73-80`: Implements flow control with conditional navigation based on handler results
- `Line 126-128`: Exception mapping system routes application errors to appropriate screens
### docroot/WEB-INF/xml/screendefinitions.xml

This XML file defines the screen layouts and UI components for the Java Pet Store web application. It serves as a central configuration for the web application's presentation layer, mapping screen names to their corresponding JSP components. Each screen definition includes parameters for HTML title, banner, body content, footer, and annotation references. The file organizes the application's navigation flow by defining screens for main pages, product catalog browsing, shopping cart, account management, checkout process, and error handling. These definitions enable a consistent template-based approach to rendering different sections of the e-commerce application.

 **Code Landmarks**
- `Line 3`: Uses a template-based approach with a single template JSP that loads different content based on screen definitions
- `Line 8`: Parameters use 'direct' attribute to determine if values are literal strings or JSP file paths
- `Line 42`: Implements annotation references that likely provide documentation or contextual help for each screen
### ejb-jar-manifest.mf

This manifest file defines the Class-Path entries for an EJB JAR file in the Web Application Framework (WAF) module of Java Pet Store. It specifies two JAR dependencies that should be available at runtime: tracer.jar and servicelocator.jar. These dependencies provide tracing functionality and service location capabilities needed by the EJB components in this module.

 **Code Landmarks**
- `Line 2`: Specifies external dependencies that must be available in the EJB container's classpath for proper functioning of the WAF module
### ejb-jar.xml

This ejb-jar.xml deployment descriptor defines the EJB Controller component for the BluePrints Web Application Framework. It configures a stateful session bean named 'EJBController' that processes events in the application. The descriptor specifies the local interfaces, implementation class, and transaction attributes for the controller. The EJBController uses container-managed transactions with a 'Required' attribute for the processEvent method, which accepts Event objects as parameters. This component serves as the central event processing mechanism within the Web Application Framework.

 **Code Landmarks**
- `Line 51`: Defines a stateful session bean, indicating the controller maintains conversational state across client interactions
- `Line 65`: Container transaction configuration requires transactions for event processing, ensuring data integrity during event handling
### sun-j2ee-ri.xml

This XML configuration file defines J2EE Reference Implementation specific settings for the Web Application Framework (WAF) component of Java Pet Store. It establishes the web application's display name, context root (/waf), and EJB reference mappings. The file configures the mapping between the logical EJB reference name 'ejb/EJBController' and its JNDI implementation name 'ejb/waf/waf/EJBController'. It also defines enterprise bean configurations, specifically for the EJBController bean with its corresponding JNDI name.

 **Code Landmarks**
- `Line 47`: Defines the context root as 'waf' which determines the URL path for accessing the web application
- `Line 48-51`: Maps the logical EJB reference to its actual JNDI implementation name, critical for dependency injection
### view/taglibs/com/sun/j2ee/blueprints/taglibs/smart/CacheTag.java

CacheTag implements a custom JSP tag for caching content in different scopes (context, session, request, page) with configurable expiration. It stores content in an inner Entry class that tracks creation time and duration. The tag evaluates its body only when no valid cached content exists, otherwise it outputs the cached content. Key functionality includes checking cache expiration, storing content with scope-appropriate attributes, and managing the cache lifecycle. Important methods include doStartTag(), doEndTag(), getEntry(), and removeEntry(). The inner Entry class handles content storage and expiration checking.

 **Code Landmarks**
- `Line 51`: Creates a unique cache key by combining request URL, tag name, and query string
- `Line 104`: Implements scope-based attribute storage using the JSP pageContext object
- `Line 159`: Inner class Entry implements expiration logic using system timestamps
### view/taglibs/com/sun/j2ee/blueprints/taglibs/smart/CheckboxTag.java

CheckboxTag implements a custom JSP tag that renders HTML checkbox input elements within forms. It extends BodyTagSupport and manages checkbox attributes including name, value, and checked state. The tag generates the appropriate HTML markup for checkbox inputs and supports integration with FormTag ancestors. Key methods include setName(), setValue(), setChecked(), doStartTag(), and doEndTag(), which handles the actual HTML generation and output. The class properly resets its state after each use to ensure clean reuse within JSP pages.

 **Code Landmarks**
- `Line 77`: The doEndTag() method constructs HTML dynamically using StringBuffer for efficiency
- `Line 89`: The tag properly resets all attributes in the finally block to ensure clean state for reuse
- `Line 79`: The tag finds its ancestor FormTag using findAncestorWithClass but doesn't actually use it
### view/taglibs/com/sun/j2ee/blueprints/taglibs/smart/CheckedTag.java

CheckedTag implements a JSP custom tag that works in conjunction with CheckboxTag to determine if a checkbox should be checked. It extends BodyTagSupport and overrides the doAfterBody method to evaluate its body content. The tag examines the string value from its body content and sets the parent CheckboxTag's checked state to true if the value is not null and not equal to 'true' (case-insensitive). After processing, it clears the body content and skips further body evaluation.

 **Code Landmarks**
- `Line 52`: The tag uses findAncestorWithClass to locate its parent CheckboxTag, demonstrating JSP tag collaboration pattern
- `Line 55`: Unusual logic where the checkbox is checked when the value is NOT 'true', which is counter-intuitive
### view/taglibs/com/sun/j2ee/blueprints/taglibs/smart/ClientStateTag.java

ClientStateTag implements a custom JSP tag that preserves client state across page requests by serializing request attributes and parameters as hidden form fields. It generates a form with a button or image that, when clicked, submits the preserved state to a target URL. The tag encodes serializable request attributes using Base64 encoding, preserves request parameters, and adds metadata like the referring URL and screen. Key functionality includes configurable encoding of request attributes and parameters, support for both button and image submission, and parameter addition through sub-tags. Important methods include doStartTag(), doEndTag(), and putParameter().

 **Code Landmarks**
- `Line 137`: Uses Base64 encoding to serialize Java objects into hidden form fields for client-side state preservation
- `Line 163`: Implements selective serialization by checking if objects implement java.io.Serializable before encoding
- `Line 91`: Integrates with WAF controller by preserving referring_URL and referring_screen for navigation flow
### view/taglibs/com/sun/j2ee/blueprints/taglibs/smart/ClientStateValueTag.java

ClientStateValueTag implements a JSP tag handler that works as a child tag for ClientStateTag. It captures name-value pairs that are passed to the parent ClientStateTag for client-side state management. The class extends BodyTagSupport and overrides doStartTag() and doEndTag() methods. In doEndTag(), it finds its parent ClientStateTag and calls putParameter() to store the name-value pair. Key instance variables include name and value strings, with corresponding setter methods setName() and setValue().

 **Code Landmarks**
- `Line 65`: Tag implementation finds its parent tag using findAncestorWithClass() to establish parent-child relationship for parameter passing
- `Line 67`: Tag cleans up its state by nullifying name and value fields after processing to prevent data leakage between requests
### view/taglibs/com/sun/j2ee/blueprints/taglibs/smart/FormTag.java

FormTag implements a custom JSP tag that generates HTML forms with built-in JavaScript validation. It extends BodyTagSupport to process form content and automatically adds client-side validation for fields registered through putValidatedField(). The tag supports standard form attributes (name, action, method) and generates JavaScript that validates required fields before submission. Key methods include doStartTag(), doAfterBody(), and doEndTag() which handle the tag lifecycle and generate both the form HTML and validation script. Important variables include validatedFields (stores field validation requirements), name, action, method, and formHTML.

 **Code Landmarks**
- `Line 49`: Uses TreeMap to store validated fields, ensuring consistent ordering of validation rules
- `Line 86`: Dynamically generates JavaScript validation function specific to the form's fields
- `Line 121`: Properly resets all instance variables after tag processing to prevent data leakage between requests
### view/taglibs/com/sun/j2ee/blueprints/taglibs/smart/InputTag.java

InputTag implements a custom JSP tag that generates HTML input elements for web forms. It extends BodyTagSupport and provides attributes for common input properties like type, name, value, size, maxlength, and CSS class. The tag integrates with FormTag for validation by registering validated fields. During rendering, it constructs an HTML input element with all specified attributes. Key methods include doStartTag() and doEndTag(), which handle the tag lifecycle and HTML generation. The class resets all attributes after each use to maintain a clean state for subsequent invocations.

 **Code Landmarks**
- `Line 74`: Finds ancestor FormTag to register this field for validation when validation attribute is set
- `Line 79`: Dynamically builds HTML input element with conditional attributes based on set properties
- `Line 97`: Resets all attributes to null/0 after rendering to prevent state persistence between tag usages
### view/taglibs/com/sun/j2ee/blueprints/taglibs/smart/NameTag.java

NameTag implements a custom JSP tag that defines the 'name' attribute for InputTag components in the Java Pet Store application. The class extends BodyTagSupport and overrides the doAfterBody() method to extract content from the tag body. It locates its parent InputTag using findAncestorWithClass(), sets the name attribute with the body content if not empty, clears the body content, and returns SKIP_BODY to prevent further processing. This tag is part of the smart taglib collection used for form handling in the Web Application Framework (WAF).

 **Code Landmarks**
- `Line 51`: Uses findAncestorWithClass() to locate parent InputTag, demonstrating JSP tag hierarchy navigation
- `Line 54`: Implements conditional logic to only set name attribute when body content exists and isn't empty
### view/taglibs/com/sun/j2ee/blueprints/taglibs/smart/OptionTag.java

OptionTag implements a custom JSP tag that represents an HTML option element within a select dropdown. It extends BodyTagSupport to process the content between opening and closing tags. The class captures both the value attribute and the body content text of the option element, then passes this information to its parent SelectTag. The doAfterBody() method handles the core functionality by retrieving the parent SelectTag, extracting the body content, and registering the option with the parent using putOption(). The class ensures proper handling of empty values and text content.

 **Code Landmarks**
- `Line 57`: Uses findAncestorWithClass to locate parent SelectTag, demonstrating JSP tag hierarchy navigation
- `Line 60`: Performs validation checks on both value attribute and body content before adding to parent
### view/taglibs/com/sun/j2ee/blueprints/taglibs/smart/SelectTag.java

SelectTag implements a JSP custom tag that generates HTML select dropdown menus. It extends BodyTagSupport and manages the rendering of select elements with options. The tag supports setting a selected value, size, name attribute, and editability. Key functionality includes storing option values and their display text in a TreeMap, generating the appropriate HTML markup based on properties, and handling the tag lifecycle. Important methods include doStartTag(), doEndTag(), and setter methods for tag attributes. The class resets its state after rendering to ensure proper reuse.

 **Code Landmarks**
- `Line 49`: Uses TreeMap to maintain sorted order of options in the dropdown menu
- `Line 84`: Conditionally renders either an interactive select dropdown or static text based on the editable property
- `Line 107`: Properly resets all instance variables after rendering to prevent state leakage between tag usages
### view/taglibs/com/sun/j2ee/blueprints/taglibs/smart/SelectedTag.java

SelectedTag implements a JSP tag that works with SelectTag to determine which option should be selected in an HTML select element. It extends BodyTagSupport and processes its body content to extract the selected value. The doAfterBody() method finds the parent SelectTag, retrieves the body content as a string, and if valid, sets this as the selected value in the parent tag. After processing, it clears the body content and skips further body evaluation. This tag is part of the smart taglib collection in the WAF (Web Application Framework) component.

 **Code Landmarks**
- `Line 56`: The tag finds its parent SelectTag using findAncestorWithClass, demonstrating JSP tag hierarchy navigation
### view/taglibs/com/sun/j2ee/blueprints/taglibs/smart/ValueTag.java

ValueTag implements a JSP tag handler that extracts content from its body and sets it as the value attribute of its parent InputTag. The class extends BodyTagSupport and overrides the doAfterBody() method to find its ancestor InputTag, retrieve the body content, and if the content is not empty, set it as the value of the input tag. After processing, it clears the body content and skips further body evaluation. This provides a convenient way to set input field values in JSP pages.

 **Code Landmarks**
- `Line 53`: Uses findAncestorWithClass to locate parent InputTag, demonstrating JSP tag collaboration pattern
### view/template/com/sun/j2ee/blueprints/waf/view/template/Parameter.java

Parameter implements a simple serializable class that stores a key-value pair along with a boolean flag indicating whether the value should be used directly or requires further processing. The class provides basic getter methods to access the key, value, and direct flag properties. It also includes a toString() method that returns a string representation of the Parameter object for debugging purposes. This class is part of the WAF (Web Application Framework) template system used in the Java Pet Store application.

 **Code Landmarks**
- `Line 42`: Implements Serializable to allow Parameter objects to be persisted or transferred across networks
### view/template/com/sun/j2ee/blueprints/waf/view/template/Screen.java

Screen implements a serializable class that represents a screen in the web application framework's view templating system. It stores a screen name, an optional template name, and a collection of parameters as a HashMap. The class provides methods to retrieve the template name, access all parameters as a HashMap, or get a specific parameter by key. It also includes a toString() method that returns a string representation of the screen object with its name and parameters.

 **Code Landmarks**
- `Line 47`: The class implements Serializable, allowing Screen objects to be persisted or transmitted across networks.
- `Line 51`: Parameters are stored in a HashMap, providing flexible key-value storage for screen configuration.
### view/template/com/sun/j2ee/blueprints/waf/view/template/ScreenDefinitionDAO.java

ScreenDefinitionDAO provides XML parsing functionality for screen definition files in the web application framework. It loads and parses XML documents that define screen layouts, templates, and parameters for the application's UI. Key functionality includes loading XML documents, extracting screen definitions with their associated templates and parameters, and handling language-specific configurations. The class contains methods like loadScreenDefinitions(), getScreens(), getTemplates(), and getParameters() that transform XML elements into Java objects representing screens, templates, and parameters. It maintains constants for XML element names and attributes used in parsing the configuration files.

 **Code Landmarks**
- `Line 72`: Uses JAXP (Java API for XML Processing) to parse XML configuration files
- `Line 103`: Implements error handling that logs errors but allows processing to continue when possible
- `Line 126`: Supports internationalization through language-specific screen definitions
- `Line 168`: Implements parameter handling with a 'direct' attribute flag for template rendering control
- `Line 194`: Uses recursive node traversal to extract nested XML configuration values
### view/template/com/sun/j2ee/blueprints/waf/view/template/Screens.java

Screens implements a class that manages screen definitions and their associated templates for the web application framework. It maintains two HashMaps: screenMap stores Screen objects keyed by screen names, and templateMap stores template URLs keyed by template names. The class provides methods to add screens and templates, retrieve screen objects, check if screens or templates exist, and get template URLs for specific screens. If a screen doesn't have a defined template, it falls back to the default template. Key methods include addScreen(), addTemplate(), getScreen(), containsScreen(), containsTemplate(), and getTemplate().

 **Code Landmarks**
- `Line 49`: The class implements Serializable, allowing screen configurations to be persisted or transmitted across the network.
- `Line 83`: Error handling prints to standard error rather than throwing exceptions, which could lead to silent failures in production.
- `Line 94`: Template resolution implements a fallback mechanism to the default template when a specific template is not defined.
### view/template/com/sun/j2ee/blueprints/waf/view/template/TemplateServlet.java

TemplateServlet implements a template-based view rendering system for the WAF (Web Application Framework). It loads screen definitions from XML files for different locales and manages the rendering of web pages using these templates. Key functionality includes initializing screen definitions for multiple languages, handling HTTP requests by determining the appropriate screen template, supporting locale-specific content, and managing previous screen state caching. Important methods include init(), process(), initScreens(), and insertTemplate(). The class maintains maps of screen definitions and supports transaction management when forwarding requests to templates.

 **Code Landmarks**
- `Line 77`: Implements caching configuration for previous screen attributes and parameters based on servlet initialization parameters
- `Line 159`: Handles special 'PREVIOUS_SCREEN' functionality to return to previous screens with state restoration
- `Line 242`: Wraps request dispatcher calls in UserTransaction for efficient local EJB access
### view/template/com/sun/j2ee/blueprints/waf/view/template/tags/InsertTag.java

InsertTag implements a custom JSP tag that facilitates template-based content insertion in JSP pages. It retrieves a Screen object from the request and uses it to fetch Parameter objects based on a specified parameter name. The tag supports two insertion modes: direct inclusion of text content and inclusion of JSP fragments via RequestDispatcher. Key functionality includes parameter retrieval from the Screen object and conditional rendering based on the Parameter's direct flag. Important methods include doStartTag() and doEndTag(), with fields for parameter name, Parameter reference, Screen object, and directInclude flag.

 **Code Landmarks**
- `Line 76`: Uses pageContext.getOut().flush() to ensure any buffered content is written before tag processing
- `Line 89`: Implements conditional rendering logic based on Parameter.isDirect() flag
- `Line 99`: Uses RequestDispatcher.include() for including JSP fragments dynamically
### waf_ejbruntime.xml

This XML file configures the J2EE Reference Implementation server for the Web Application Framework (WAF) EJB components. It defines JNDI naming and role mapping for enterprise beans used in the Java Pet Store application. Specifically, it registers the ClientController EJB with the JNDI name 'ejb/waf/waf/ClientController'. The file follows the standard J2EE deployment descriptor format with Sun Microsystems copyright and redistribution terms. This configuration enables the application server to properly locate and manage the WAF controller components at runtime.

 **Code Landmarks**
- `Line 54`: Defines JNDI name for ClientController EJB which is a key component in the WAF architecture
### waf_warruntime.xml

This XML file configures the J2EE-specific deployment settings for the Web Application Framework (WAF) component of Java Pet Store. It defines server-specific information needed for deploying the WAF web module, including the context root 'waf' and an EJB reference mapping. The file establishes how the ClientController EJB is referenced within the web application by mapping the logical name 'ejb/ClientController' to the JNDI name 'ejb/waf/waf/ClientController'. This configuration enables the web tier components to locate and interact with the business logic implemented in EJBs.

 **Code Landmarks**
- `Line 46`: Maps the web application's context root to 'waf', determining the URL path for accessing the application
- `Line 49-52`: Defines EJB reference mapping that connects web components to the ClientController EJB through JNDI lookup

[Generated by the Sage AI expert workbench: 2025-03-29 21:37:00  https://sage-tech.ai/workbench]: #