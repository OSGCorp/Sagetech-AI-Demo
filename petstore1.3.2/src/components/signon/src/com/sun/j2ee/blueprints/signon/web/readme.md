# Src Web of Java Pet Store

The Java Pet Store is a J2EE application that demonstrates enterprise application architecture and best practices for e-commerce systems. The Src Web sub-project implements the web-facing authentication and security components that control access to protected resources within the application. This provides these capabilities to the Java Pet Store program:

- User authentication and session management
- Role-based access control for protected resources
- XML-driven security configuration
- User registration and account creation

## Identified Design Elements

1. XML-Based Security Configuration: Security constraints, protected resources, and authentication pages are defined in XML and loaded dynamically
2. EJB Integration: Authentication logic is delegated to SignOn EJBs, maintaining separation between web and business tiers
3. Filter-Based Security: Uses servlet filters to intercept requests and enforce security constraints before reaching protected resources
4. Session-Based Authentication: Maintains user authentication state in HTTP sessions with optional "remember me" cookie functionality

## Overview

The architecture follows J2EE best practices with clear separation between presentation, business logic, and data access layers. The SignOnFilter intercepts incoming requests and validates them against protected resource definitions loaded by SignOnDAO from XML configuration. The CreateUserServlet handles new user registration, while the ProtectedResource class models security constraints. This design provides flexible, declarative security that can be modified without code changes, supporting both form-based authentication and programmatic access control throughout the application.

## Business Functions

### Authentication and Security
- `SignOnDAO.java` : Data access object for sign-on functionality that parses XML configuration for protected web resources.
- `ProtectedResource.java` : Defines a serializable class representing a protected resource with access control information.
- `CreateUserServlet.java` : Servlet that handles user registration in the Java Pet Store application.
- `SignOnFilter.java` : Authentication filter that manages user sign-on process for protected resources in the Java Pet Store application.

## Files
### CreateUserServlet.java

CreateUserServlet implements a servlet that processes user registration requests in the Java Pet Store application. It handles HTTP POST requests containing username and password parameters, creates new user accounts through the SignOnLocal EJB, and manages authentication flow. The servlet extracts form data, calls the EJB's createUser method, and redirects users to their originally requested URL upon successful registration or to an error page if creation fails. Key methods include doPost() for handling form submissions and getSignOnEjb() for obtaining the EJB reference through JNDI lookup.

 **Code Landmarks**
- `Line 57`: Uses SignOnFilter.ORIGINAL_URL from session to redirect users back to their originally requested page after successful registration
- `Line 58`: Sets a Boolean attribute in session to indicate successful authentication state
- `Line 75`: Uses JNDI lookup to obtain EJB reference through java:comp/env context
### ProtectedResource.java

ProtectedResource implements a serializable class that represents a protected resource in the Java Pet Store's sign-on component. It stores essential access control information including the resource name, URL pattern for matching requests, and a list of roles that are authorized to access the resource. The class provides getter methods to retrieve these properties and includes a toString() method for debugging. Key fields include name (String), urlPattern (String), and roles (ArrayList).

 **Code Landmarks**
- `Line 50`: The class implements Serializable to support state persistence across HTTP sessions or for remote method invocation
### SignOnDAO.java

SignOnDAO implements a data access object that loads and parses XML configuration for the sign-on component. It extracts login page paths, error page paths, and protected web resources with their associated role-based access controls. The class provides methods to retrieve sign-on pages and a map of protected resources. Key functionality includes parsing XML documents using JAXP, extracting tag values, and building a security constraint model. Important elements include the constructor taking a configuration URL, getSignOnPage(), getSignOnErrorPage(), getProtectedResources(), and helper methods for XML parsing.

 **Code Landmarks**
- `Line 76`: Uses DOM parsing to extract security configuration from XML files
- `Line 101`: HashMap stores protected resources indexed by resource name for efficient lookup
- `Line 123`: Builds a security model by extracting URL patterns and role-based access controls
### SignOnFilter.java

SignOnFilter implements a servlet filter that controls access to protected resources by validating user credentials. It intercepts HTTP requests, checks if the requested resource requires authentication, and redirects unauthenticated users to a sign-on page. Key functionality includes validating user credentials against a SignOn EJB, managing user sessions, handling cookies for remembering usernames, and configuring protected resources via XML. Important elements include constants for request parameters, session attributes, methods doFilter() for request interception, validateSignOn() for credential verification, and getSignOnEJB() for EJB lookup.

 **Code Landmarks**
- `Line 75`: Uses XML configuration file to define protected resources and sign-on pages
- `Line 107`: Implements URL pattern matching to determine which resources require authentication
- `Line 149`: Manages persistent user authentication with cookies that expire after one month
- `Line 175`: Uses EJB for authentication logic, separating presentation from business logic

[Generated by the Sage AI expert workbench: 2025-03-29 21:37:00  https://sage-tech.ai/workbench]: #