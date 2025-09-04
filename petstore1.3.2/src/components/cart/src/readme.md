# Shopping Cart Source Root Of Java Pet Store

The Shopping Cart Source Root is a Java EE component that implements the shopping cart functionality for the Java Pet Store application. The component utilizes stateful session beans to maintain user-specific cart data throughout a shopping session and provides a comprehensive API for cart manipulation. This sub-project implements the core e-commerce transaction functionality along with integration points to the product catalog system. This provides these capabilities to the Java Pet Store program:

- Stateful shopping cart management across user sessions
- Product item storage with quantity tracking and price calculations
- Integration with the catalog system for product information retrieval
- Locale-aware shopping experience supporting internationalization

## Identified Design Elements

1. Stateful Session Bean Architecture: Uses EJB stateful session beans to maintain user-specific cart state throughout the shopping experience
2. Model-View Separation: Clear separation between the data model (ShoppingCartModel, CartItem) and business logic implementation (ShoppingCartLocalEJB)
3. Catalog Integration: References the Catalog component to retrieve product details when adding items to the cart
4. Transaction Management: Container-managed transactions for all cart operations ensuring data consistency
5. Serializable Data Models: Cart items and models implement Serializable to support session state persistence

## Overview
The architecture emphasizes clean separation between the data model and business logic with well-defined interfaces. The ShoppingCartLocalEJB implements the core functionality while CartItem and ShoppingCartModel provide the data structures. The component is built with container-managed transactions for reliability and includes integration with the catalog system for product information. The design supports internationalization through locale settings and provides comprehensive methods for cart manipulation including adding, removing, and updating items, as well as calculating costs and managing the overall cart state.

## Sub-Projects

### src/components/cart/src/com/sun/j2ee/blueprints/cart

The program implements a complete shopping experience with product browsing, user authentication, and order processing. This sub-project implements the core shopping cart functionality along with session management for tracking user selections throughout the shopping process. This provides these capabilities to the Java Pet Store program:

- Stateful shopping cart management across user sessions
- Product selection and quantity tracking
- Price calculation and cart manipulation operations
- Cart persistence between user sessions
- Integration with the order processing pipeline

#### Identified Design Elements

1. **Session-Based Cart Storage**: The implementation uses HTTP sessions to maintain cart state without requiring user login, allowing anonymous shopping experiences
2. **EJB Integration**: Shopping cart functionality is implemented as stateful session beans that integrate with the broader J2EE component architecture
3. **Value Object Pattern**: Product selections and cart items are represented as serializable value objects that can be passed between application tiers
4. **Transaction Support**: Cart operations support atomic updates within the context of the J2EE transaction model

#### Overview
The architecture follows J2EE best practices with clear separation between presentation, business logic, and persistence layers. The shopping cart core provides a foundation for the e-commerce functionality while maintaining compatibility with the MVC pattern used throughout the application. The implementation balances performance considerations with the need for reliability, ensuring cart data remains consistent even during concurrent access scenarios. The design allows for future extensions such as saved carts, wish lists, and personalized recommendations based on cart contents.

  *For additional detailed information, see the summary for src/components/cart/src/com/sun/j2ee/blueprints/cart.*

## Business Functions

### Shopping Cart Model
- `com/sun/j2ee/blueprints/cart/model/ShoppingCartModel.java` : Implements a model class for shopping cart data in the Java Pet Store application.
- `com/sun/j2ee/blueprints/cart/model/CartItem.java` : Represents a single item in a shopping cart with product details and quantity.

### Shopping Cart EJB Implementation
- `com/sun/j2ee/blueprints/cart/ejb/ShoppingCartLocalEJB.java` : Implements a shopping cart as a stateful session EJB for the Java Pet Store application.
- `com/sun/j2ee/blueprints/cart/ejb/ShoppingCartLocal.java` : Local EJB interface for shopping cart operations in the Java Pet Store application.
- `com/sun/j2ee/blueprints/cart/ejb/ShoppingCartLocalHome.java` : Local home interface for the ShoppingCart EJB defining creation methods.

### Build Configuration
- `build.xml` : Ant build script for the shopping cart component of Java Pet Store application.
- `ejb-jar-manifest.mf` : EJB JAR manifest file defining class dependencies for the cart component.
- `ejb-jar.xml` : EJB deployment descriptor for the ShoppingCart component in Java Pet Store application.

## Files
### build.xml

This build script manages the compilation and packaging of the shopping cart component for the Java Pet Store application. It defines targets for compiling Java classes, creating EJB JAR files, generating documentation, and cleaning build artifacts. The script sets up dependencies on other components like catalog and tracer utilities, configures classpaths, and defines build directories. Key targets include 'compile', 'ejbjar', 'ejbclientjar', 'clean', 'components', 'docs', and 'core'. Important properties include cart.home, cart.src, cart.build, cart.classpath, cart.ejbjar, and cart.ejbclientjar.

 **Code Landmarks**
- `Line 96`: Creates separate client JAR that excludes implementation classes for better separation of concerns
- `Line 114`: Manages component dependencies by triggering builds of required components before building the cart component
- `Line 151`: Defines a complete build sequence that includes component dependencies, compilation, and packaging
### com/sun/j2ee/blueprints/cart/ejb/ShoppingCartLocal.java

ShoppingCartLocal interface defines the local EJB contract for shopping cart operations in the Java Pet Store application. It extends EJBLocalObject and provides methods to manipulate cart contents including adding items, deleting items, updating quantities, emptying the cart, and retrieving information such as the collection of items, subtotal, and item count. The interface also supports setting the user's locale preference. This interface is part of the cart component and serves as the contract that the ShoppingCart EJB implementation must fulfill.

 **Code Landmarks**
- `Line 47`: Interface extends EJBLocalObject, making it a local EJB component interface that can only be accessed within the same JVM
### com/sun/j2ee/blueprints/cart/ejb/ShoppingCartLocalEJB.java

ShoppingCartLocalEJB implements a stateful session bean that manages a user's shopping cart in the Pet Store application. It stores cart items in a HashMap and provides methods to add, delete, update quantities, and retrieve cart items. The class interacts with the CatalogHelper to fetch item details and calculates subtotals. Key functionality includes managing cart operations, locale settings, and converting catalog items to cart items. Important methods include getItems(), addItem(), deleteItem(), updateItemQuantity(), getCount(), getSubTotal(), and empty(). The class maintains a HashMap cart and Locale locale as its main state variables.

 **Code Landmarks**
- `Line 76`: The getItems() method demonstrates integration between cart and catalog components by converting catalog items to cart items
- `Line 114`: The updateItemQuantity() method intelligently removes items when quantity is zero or negative
- `Line 123`: The getSubTotal() method calculates the cart total by iterating through items and multiplying unit cost by quantity
### com/sun/j2ee/blueprints/cart/ejb/ShoppingCartLocalHome.java

ShoppingCartLocalHome interface defines the home interface for the Shopping Cart EJB component in the Java Pet Store application. It extends EJBLocalHome and provides methods for creating ShoppingCartLocal objects. The interface declares a single create() method that throws CreateException, allowing clients to instantiate new shopping cart instances. There is also a commented-out create method that would accept a HashMap parameter, suggesting a potential feature for initializing a cart with existing items. This interface is part of the cart component's EJB implementation.

 **Code Landmarks**
- `Line 48`: Interface extends EJBLocalHome, making it a local home interface in the EJB 2.0 component model
- `Line 52`: Commented-out create method suggests a planned feature for initializing carts with existing items
### com/sun/j2ee/blueprints/cart/model/CartItem.java

CartItem implements a serializable class representing a line item in a shopping cart. It stores essential product information including itemId, productId, category, name, attribute, quantity, and unitCost. The class provides getter methods for all properties and calculates the total cost by multiplying quantity by unit cost. This simple data model serves as a fundamental building block for the shopping cart functionality in the Java Pet Store application, enabling the storage and manipulation of items selected by users for purchase.

 **Code Landmarks**
- `Line 78`: getTotalCost() method dynamically calculates the total cost by multiplying quantity and unitCost rather than storing it as a field
### com/sun/j2ee/blueprints/cart/model/ShoppingCartModel.java

ShoppingCartModel implements a serializable model class that represents shopping cart data in the Java Pet Store application. It stores a collection of CartItem objects and provides methods to access cart information. Key functionality includes retrieving the cart size, getting the collection of items, iterating through items, calculating the total cost of all items in the cart, and copying data from another ShoppingCartModel instance. Important methods include getSize(), getCart(), getItems(), getTotalCost(), and copy(). The class serves as a value object with fine-grained getter methods for the shopping cart component.

 **Code Landmarks**
- `Line 55`: The class implements Serializable to support state persistence across HTTP sessions
- `Line 61`: Provides two constructors - one that accepts items collection and another no-arg constructor specifically for web tier usage
- `Line 86`: The copy() method performs a shallow copy, which could lead to shared references between cart instances
### ejb-jar-manifest.mf

This manifest file defines the configuration for the cart component's EJB JAR. It specifies the JAR manifest version and declares dependencies on tracer.jar and catalog-ejb-client.jar through the Class-Path attribute. These dependencies are essential for the cart component to function properly within the Java Pet Store application.

 **Code Landmarks**
- `Line 2-3`: Specifies external JAR dependencies needed by the cart component, establishing the relationship between the cart and catalog components.
### ejb-jar.xml

This ejb-jar.xml deployment descriptor configures the ShoppingCart EJB component for the Java Pet Store application. It defines a stateful session bean named ShoppingCartEJB with local interfaces for managing shopping cart functionality. The file specifies container-managed transactions for cart operations like addItem, deleteItem, updateItemQuantity, and empty. It also establishes a local EJB reference to the Catalog component, enabling the shopping cart to access product information. The assembly descriptor defines method permissions (unchecked) and transaction attributes (Required) for all cart operations.

 **Code Landmarks**
- `Line 52`: Defines a stateful session bean which maintains client state across multiple requests
- `Line 56`: Local interfaces are used for EJBs within the same JVM for better performance
- `Line 60`: EJB reference to Catalog component shows component dependencies
- `Line 76`: Container-managed transactions configured with Required attribute for data integrity

[Generated by the Sage AI expert workbench: 2025-03-29 21:37:00  https://sage-tech.ai/workbench]: #