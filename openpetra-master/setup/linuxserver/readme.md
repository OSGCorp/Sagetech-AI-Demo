# Linux Server Setup Subproject Of OpenPetra

The Linux Server Setup subproject provides the deployment and operational infrastructure for OpenPetra on Linux-based systems. This subproject implements server instance management and web server configuration, enabling OpenPetra to function as a robust, multi-tenant web application. The components work together to create a complete server environment that supports the core OpenPetra functionality for non-profit organizations.

Key capabilities provided to the OpenPetra program:

- Multi-tenant deployment architecture supporting isolated instances for different customers
- Comprehensive server lifecycle management (start, stop, backup, restore)
- Database operations for both MySQL and PostgreSQL backends
- Web server configuration with proper routing and API endpoints

## Identified Design Elements

1. **Instance Isolation**: Each OpenPetra instance has separate configurations, databases, and runtime environments to ensure security and stability
2. **Unified Management Interface**: The `openpetra-server.sh` script provides a consistent interface for all server administration tasks
3. **Flexible Database Support**: The architecture accommodates multiple database backends with standardized operations
4. **Reverse Proxy Configuration**: Nginx handles request routing, static content serving, and API proxying to internal services

## Overview
The architecture follows a layered approach with clear separation between web-facing components and backend services. The server management script provides comprehensive administration capabilities while the Nginx configuration handles HTTP request routing and security. The system supports both traditional web interfaces and API access, with proper URL rewriting and FastCGI proxying. The multi-tenant design allows for efficient resource utilization while maintaining isolation between customer instances, making it suitable for both single-organization deployments and service provider scenarios.

## Sub-Projects

### setup/linuxserver/postgresql

This component handles database initialization, user management, and configuration integration between OpenPetra and PostgreSQL. The setup scripts automate the deployment process, reducing manual configuration and ensuring consistent installations across different Linux distributions.

#### Key Technical Features

- Automated PostgreSQL database initialization and schema creation
- User permission management for secure database access
- Configuration file generation for OpenPetra-PostgreSQL integration
- Database backup and restoration utilities
- Migration support from other database backends to PostgreSQL
- Environment-specific configuration options for development, testing, and production

#### Identified Design Elements

1. **Distribution-Aware Configuration**: Scripts detect the Linux distribution and adapt installation procedures accordingly
2. **Idempotent Operations**: Setup can be safely re-run without corrupting existing installations
3. **Separation of Concerns**: Clear distinction between database setup, application configuration, and runtime management
4. **Security-First Approach**: Implements best practices for database security including least-privilege access and encrypted connections

#### Architecture Overview

The setup process follows a layered approach, first establishing the PostgreSQL environment, then configuring database users and permissions, followed by schema initialization, and finally connecting the OpenPetra application to the database. This modular design allows for flexible deployment scenarios and simplifies troubleshooting. The subproject integrates with OpenPetra's configuration management system to ensure database connection details are properly propagated throughout the application.

  *For additional detailed information, see the summary for setup/linuxserver/postgresql.*

### setup/linuxserver/mysql

This component handles the database initialization, configuration management, and system integration necessary for OpenPetra's data persistence layer on Linux platforms.

The subproject implements automated database provisioning along with security configuration management, enabling streamlined deployment of the OpenPetra application stack. This provides these capabilities to the Petra program:

- Automated MySQL database initialization and schema creation
- User permission management and security configuration
- Database backup and restoration utilities
- Configuration file management for MySQL integration
- Cross-platform compatibility layer for Linux environments

#### Identified Design Elements

1. **Database Abstraction Layer**: Provides a consistent interface between OpenPetra's application logic and the underlying MySQL database system
2. **Configuration Management**: Handles environment-specific settings through templated configuration files
3. **Installation Automation**: Scripts that manage the complete setup process from database creation to application configuration
4. **Security Implementation**: Manages user credentials, connection encryption, and access control mechanisms
5. **Maintenance Utilities**: Tools for database backup, restoration, and health monitoring

#### Overview
The architecture follows a modular approach that separates database configuration from application logic, enabling easier maintenance and updates. The setup process is designed to be reproducible and idempotent, allowing for consistent deployments across different Linux environments. This subproject serves as the foundation for OpenPetra's data persistence strategy, ensuring reliable storage for the application's contact management, accounting, and sponsorship features.

  *For additional detailed information, see the summary for setup/linuxserver/mysql.*

## Business Functions

### Server Management
- `openpetra-server.sh` : Bash script for managing OpenPetra server instances, providing commands for installation, configuration, and maintenance.

### Web Server Configuration
- `nginx.conf` : Nginx configuration file for OpenPetra server deployment with URL rewriting and API proxying.

## Files
### nginx.conf

This Nginx configuration file defines the web server setup for OpenPetra, specifying how HTTP requests are handled. It configures the server to listen on a specific port, sets the document root to the client directory, and implements URL rewriting rules that redirect various application paths. The file establishes FastCGI proxying for the API endpoint to an internal service on port 6700, with text substitution for proper URL handling. It also includes configuration for phpMyAdmin access, routing PHP requests to a FastCGI processor on port 8080.

 **Code Landmarks**
- `Line 9`: Sets a 30MB maximum upload size limit for the server
- `Line 12-24`: Implements URL rewriting that redirects application paths back to root, enhancing single-page application functionality
- `Line 30`: Uses sub_filter to dynamically replace server URLs in API responses
- `Line 34-44`: Configures phpMyAdmin access through a nested location block with specific FastCGI parameters
### openpetra-server.sh

This script manages OpenPetra server instances running on Linux systems. It provides comprehensive functionality for server administration including starting/stopping services, database operations (MySQL/PostgreSQL), backup/restore capabilities, and instance configuration. The script handles user management, database initialization, configuration file generation, and system updates. It supports multi-tenant deployments with separate instances for different customers, each with isolated configurations and databases. Key functions include start(), stop(), backup/restore operations, loadYmlGz()/dumpYmlGz() for database transfers, and init()/initdb() for new instance setup. Environment variables control configuration parameters like database credentials, ports, and email settings.

 **Code Landmarks**
- `Line 45`: Function to generate secure random passwords using urandom
- `Line 49`: Configuration extraction from XML-style config files using grep and awk
- `Line 370`: Smart backup rotation system that keeps hourly backups for recent days but only daily backups for older data
- `Line 420`: Dynamic nginx configuration generation from templates with variable substitution
- `Line 483`: Database initialization with proper security settings for both MySQL and PostgreSQL

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #