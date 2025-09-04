## Sign-on Component of Java Pet Store

The Sign-on Component is a Java EE authentication module that provides secure user management and access control services for the Java Pet Store application. This component implements a comprehensive authentication framework using Enterprise JavaBeans (EJB) architecture with container-managed persistence. The component handles user registration, credential verification, and resource protection through a combination of EJBs, servlets, and filters.

### Key Capabilities

- User authentication against stored credentials
- New user registration with validation constraints
- Role-based access control for protected resources
- XML-driven configuration for security constraints
- Session management with optional "remember me" functionality

### Identified Design Elements

1. **EJB-based Authentication Architecture**: Uses a stateless session bean (SignOnEJB) for authentication services and an entity bean (UserEJB) for persistent credential storage
2. **Container-Managed Persistence**: Leverages J2EE container services for data persistence of user credentials
3. **Servlet Filter Security**: Implements request interception via SignOnFilter to enforce access control policies
4. **XML-Driven Configuration**: Uses XML parsing to define protected resources and role-based access controls
5. **Data Access Object Pattern**: Employs SignOnDAO to abstract configuration data access from business logic

### Overview

The architecture follows a layered approach with clear separation between data persistence (UserEJB), business logic (SignOnEJB), and presentation/control (servlets and filters). Authentication flows begin at the SignOnFilter, which intercepts requests to protected resources and redirects unauthenticated users to a login page. User credentials are validated against the UserEJB through the SignOnEJB facade. The component enforces security constraints on username and password formats and provides comprehensive error handling for authentication failures.

## Sub-Projects

### src/components/signon/src/com/sun/j2ee/blueprints/signon

The subproject implements user authentication and credential management through Enterprise JavaBeans (EJB) architecture. This provides these capabilities to the Java Pet Store program:

- Secure user authentication and validation
- Container-managed persistence for user credentials
- Standardized user entity access through local interfaces
- Password matching and verification services

#### Identified Design Elements

1. **Entity Bean Architecture**: Implements a container-managed persistence (CMP) Entity Bean model for user data storage and retrieval
2. **Credential Validation Logic**: Enforces security constraints including character restrictions and maximum length validation (25 characters)
3. **Local Interface Pattern**: Uses the EJB local interface pattern to optimize performance for same-JVM component interactions
4. **Separation of Interface and Implementation**: Clear separation between interface contracts (UserLocal, UserLocalHome) and implementation (UserEJB)

#### Overview
The architecture follows standard J2EE patterns with a three-part EJB design: implementation class (UserEJB), local interface (UserLocal), and local home interface (UserLocalHome). The Entity Bean manages persistence automatically through container services, while business logic focuses on credential validation and authentication. The design emphasizes security through input validation and provides a clean API for other application components to authenticate users. This modular approach allows the authentication system to be maintained independently while providing essential services to the broader Pet Store application.

  *For additional detailed information, see the summary for src/components/signon/src/com/sun/j2ee/blueprints/signon.*

## Business Functions

### Authentication Components
- `com/sun/j2ee/blueprints/signon/ejb/SignOnEJB.java` : Authentication component that verifies user credentials for the Java Pet Store application.
- `com/sun/j2ee/blueprints/signon/ejb/SignOnLocal.java` : Local EJB interface for sign-on authentication and user creation functionality.
- `com/sun/j2ee/blueprints/signon/ejb/SignOnLocalHome.java` : Defines the local home interface for SignOn Entity EJB in the authentication component.

### User Management
- `com/sun/j2ee/blueprints/signon/user/ejb/UserEJB.java` : Entity bean for user authentication in the signon component of Java Pet Store.
- `com/sun/j2ee/blueprints/signon/user/ejb/UserLocalHome.java` : Local home interface for the User entity EJB in the signon component.
- `com/sun/j2ee/blueprints/signon/user/ejb/UserLocal.java` : Local interface for the User EJB that defines methods for user authentication and password management.

### Web Security
- `com/sun/j2ee/blueprints/signon/web/SignOnDAO.java` : Data access object for sign-on functionality that parses XML configuration for protected web resources.
- `com/sun/j2ee/blueprints/signon/web/ProtectedResource.java` : Defines a serializable class representing a protected resource with access control information.
- `com/sun/j2ee/blueprints/signon/web/CreateUserServlet.java` : Servlet that handles user registration in the Java Pet Store application.
- `com/sun/j2ee/blueprints/signon/web/SignOnFilter.java` : Authentication filter that manages user sign-on process for protected resources in the Java Pet Store application.

### Configuration
- `ejb-jar.xml` : EJB deployment descriptor defining user authentication components for Java Pet Store application.

### Build System
- `build.xml` : Ant build script for the SignOn component of Java Pet Store, managing compilation and packaging of EJB modules.

## Files
### build.xml

This build.xml file defines the Ant build process for the SignOn component of Java Pet Store. It establishes targets for compiling Java classes, packaging EJB JAR files (both implementation and client), generating documentation, and cleaning build artifacts. The script sets up properties for build directories, source locations, and classpaths, then defines targets that compile source code, create EJB JARs with proper metadata, generate Javadoc documentation, and perform cleanup operations. Key targets include 'compile', 'ejbjar', 'ejbclientjar', 'clean', 'docs', and the default target 'core'.

 **Code Landmarks**
- `Line 73`: Creates separate client JAR without implementation classes for proper EJB deployment architecture
- `Line 45`: Imports user-specific properties first, allowing for developer customization of build settings
- `Line 61`: Defines J2EE classpath including locale directory for internationalization support
### com/sun/j2ee/blueprints/signon/ejb/SignOnEJB.java

SignOnEJB implements a stateless session bean that handles user authentication and creation in the Java Pet Store application. It provides two key business methods: authenticate() to verify user credentials by checking username and password against stored values, and createUser() to register new users. The EJB connects to a UserLocal EJB through JNDI lookup to perform these operations. The class implements standard EJB lifecycle methods (ejbCreate, ejbRemove, ejbActivate, ejbPassivate) with minimal implementation as appropriate for a stateless session bean.

 **Code Landmarks**
- `Line 65`: Authentication method returns false on FinderException rather than propagating the exception, providing a clean failure path
- `Line 56`: EJB initialization uses JNDI lookup to access the User EJB, demonstrating standard J2EE component wiring
### com/sun/j2ee/blueprints/signon/ejb/SignOnLocal.java

SignOnLocal interface defines the local EJB contract for the sign-on component in the Java Pet Store application. It extends javax.ejb.EJBLocalObject and provides two key methods: authenticate() to validate user credentials by checking if a username/password combination is valid, and createUser() to register new users in the system. The interface serves as part of the authentication mechanism, allowing other components to verify user identities and create new user accounts through local EJB calls.

 **Code Landmarks**
- `Line 47`: Interface extends EJBLocalObject indicating it's designed for efficient local container calls rather than remote access
### com/sun/j2ee/blueprints/signon/ejb/SignOnLocalHome.java

SignOnLocalHome interface defines the local home interface for the SignOn Entity EJB in the Java Pet Store application's authentication component. It extends javax.ejb.EJBLocalHome and provides a single create method that returns a SignOnLocal object. This interface is part of the sign-on mechanism that handles user authentication within the application. The create method throws CreateException if there are issues during entity bean creation. The interface is designed to work within the local container environment, avoiding remote method invocation overhead.

 **Code Landmarks**
- `Line 46`: Uses EJBLocalHome interface rather than remote interface, indicating this component is designed for local container access only
### com/sun/j2ee/blueprints/signon/user/ejb/UserEJB.java

UserEJB implements an Entity Bean for user authentication in the Java Pet Store application. It manages user credentials with container-managed persistence (CMP) fields for username and password. The class provides methods for creating users with validation logic that enforces maximum length constraints and character restrictions. It includes a matchPassword method to verify user credentials and implements standard EntityBean lifecycle methods required by the EJB specification. Key elements include ejbCreate for user creation with validation, abstract getter/setter methods for CMP fields, and the matchPassword business method.

 **Code Landmarks**
- `Line 59`: Implements validation logic for username and password during user creation
- `Line 79`: Simple password matching method that compares plain text passwords
### com/sun/j2ee/blueprints/signon/user/ejb/UserLocal.java

UserLocal interface defines the local EJB contract for user authentication in the Java Pet Store's sign-on component. It provides methods to retrieve and modify user credentials, including getUserName(), getPassword(), setPassword(), and matchPassword() for password validation. The interface also defines constants for maximum username and password lengths (25 characters each). As part of the EJB architecture, it extends javax.ejb.EJBLocalObject, making it accessible to other components within the same JVM for user authentication operations.

 **Code Landmarks**
- `Line 46`: Interface extends javax.ejb.EJBLocalObject, making it a local EJB component interface that can only be accessed within the same JVM
### com/sun/j2ee/blueprints/signon/user/ejb/UserLocalHome.java

UserLocalHome interface defines the local home interface for the User Entity EJB in the Java Pet Store's signon component. It extends javax.ejb.EJBLocalHome and provides methods for creating and finding User entities in the database. The interface declares three key methods: create() for creating a new user with username and password, findByPrimaryKey() for retrieving a specific user by username, and findAllUsers() for retrieving all users. These methods are essential for managing user authentication data in the application.

 **Code Landmarks**
- `Line 44`: This interface is part of the EJB 2.0 local interface pattern, providing more efficient access than remote interfaces for components within the same JVM.
### com/sun/j2ee/blueprints/signon/web/CreateUserServlet.java

CreateUserServlet implements a servlet that processes user registration requests in the Java Pet Store application. It handles HTTP POST requests containing username and password parameters, creates new user accounts through the SignOnLocal EJB, and manages authentication flow. The servlet extracts form data, calls the EJB's createUser method, and redirects users to their originally requested URL upon successful registration or to an error page if creation fails. Key methods include doPost() for handling form submissions and getSignOnEjb() for obtaining the EJB reference through JNDI lookup.

 **Code Landmarks**
- `Line 57`: Uses SignOnFilter.ORIGINAL_URL from session to redirect users back to their originally requested page after successful registration
- `Line 58`: Sets a Boolean attribute in session to indicate successful authentication state
- `Line 75`: Uses JNDI lookup to obtain EJB reference through java:comp/env context
### com/sun/j2ee/blueprints/signon/web/ProtectedResource.java

ProtectedResource implements a serializable class that represents a protected resource in the Java Pet Store's sign-on component. It stores essential access control information including the resource name, URL pattern for matching requests, and a list of roles that are authorized to access the resource. The class provides getter methods to retrieve these properties and includes a toString() method for debugging. Key fields include name (String), urlPattern (String), and roles (ArrayList).

 **Code Landmarks**
- `Line 50`: The class implements Serializable to support state persistence across HTTP sessions or for remote method invocation
### com/sun/j2ee/blueprints/signon/web/SignOnDAO.java

SignOnDAO implements a data access object that loads and parses XML configuration for the sign-on component. It extracts login page paths, error page paths, and protected web resources with their associated role-based access controls. The class provides methods to retrieve sign-on pages and a map of protected resources. Key functionality includes parsing XML documents using JAXP, extracting tag values, and building a security constraint model. Important elements include the constructor taking a configuration URL, getSignOnPage(), getSignOnErrorPage(), getProtectedResources(), and helper methods for XML parsing.

 **Code Landmarks**
- `Line 76`: Uses DOM parsing to extract security configuration from XML files
- `Line 101`: HashMap stores protected resources indexed by resource name for efficient lookup
- `Line 123`: Builds a security model by extracting URL patterns and role-based access controls
### com/sun/j2ee/blueprints/signon/web/SignOnFilter.java

SignOnFilter implements a servlet filter that controls access to protected resources by validating user credentials. It intercepts HTTP requests, checks if the requested resource requires authentication, and redirects unauthenticated users to a sign-on page. Key functionality includes validating user credentials against a SignOn EJB, managing user sessions, handling cookies for remembering usernames, and configuring protected resources via XML. Important elements include constants for request parameters, session attributes, methods doFilter() for request interception, validateSignOn() for credential verification, and getSignOnEJB() for EJB lookup.

 **Code Landmarks**
- `Line 75`: Uses XML configuration file to define protected resources and sign-on pages
- `Line 107`: Implements URL pattern matching to determine which resources require authentication
- `Line 149`: Manages persistent user authentication with cookies that expire after one month
- `Line 175`: Uses EJB for authentication logic, separating presentation from business logic
### ejb-jar.xml

This ejb-jar.xml file defines the Enterprise JavaBeans deployment descriptor for the sign-on component of the Java Pet Store application. It configures two EJBs: a container-managed persistence UserEJB entity bean that stores username and password data, and a stateless SignOnEJB session bean that provides authentication services. The file specifies local interfaces, container transaction attributes, method permissions, and EJB relationships. The UserEJB includes fields for username (primary key) and password, while the SignOnEJB provides methods for user authentication and creation. All methods are configured with Required transaction attributes and unchecked security permissions.

 **Code Landmarks**
- `Line 48`: Defines UserEJB as a container-managed persistence entity bean with String primary key
- `Line 77`: EJBQL query implementation for finding all users in the system
- `Line 91`: Local EJB reference linking SignOnEJB to UserEJB for authentication operations
- `Line 102`: Method permissions section grants unchecked access to all authentication methods
- `Line 262`: Container transaction configuration enforcing Required transaction attribute for authentication operations

[Generated by the Sage AI expert workbench: 2025-03-29 21:37:00  https://sage-tech.ai/workbench]: #