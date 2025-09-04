# OpenPetra Maintenance Tools

## Overview of OpenPetra Maintenance Tools

OpenPetra's maintenance tools form a comprehensive suite of utilities designed to help administrators and developers manage, monitor, and maintain the system's health and functionality. These tools range from server management scripts that handle database operations and service control, to code analysis utilities that ensure consistent implementation patterns across the codebase. The maintenance ecosystem includes both command-line and graphical interfaces, providing flexibility for different operational contexts and user preferences. These tools collectively enable efficient system administration, quality assurance, and troubleshooting capabilities essential for maintaining a robust enterprise application.

## Server Management Script

The `openpetra-server.sh` script serves as the primary server administration tool for OpenPetra installations on Linux systems. This comprehensive bash script provides administrators with a command-line interface to manage all aspects of OpenPetra server instances.

The script handles a wide range of administrative tasks including:
- Starting and stopping OpenPetra server instances
- Database operations (backup, restore, initialization)
- Instance configuration management
- Multi-tenant deployment support
- System updates and maintenance

Key functions include database backup rotation with intelligent retention policies, secure password generation, and configuration file management. The script supports both MySQL and PostgreSQL database backends, automatically detecting which is in use and applying the appropriate commands.

For multi-tenant deployments, the script enables administrators to manage separate instances for different customers, each with isolated configurations and databases. This isolation improves security and simplifies management of multiple client environments.

```bash
# Example commands
./openpetra-server.sh start       # Start the server
./openpetra-server.sh backup      # Create a database backup
./openpetra-server.sh backupall   # Backup all tenant databases
./openpetra-server.sh init        # Initialize a new instance
./openpetra-server.sh update      # Update binaries and databases
```

The script's modular design allows for easy extension to support additional functionality as the system evolves.

## Database Maintenance Operations

OpenPetra provides robust database maintenance capabilities through both the server management script and specialized tools. These operations are crucial for ensuring data integrity, performance, and recoverability.

### Backup and Restore

The system implements a sophisticated backup strategy with intelligent rotation:
- Hourly backups for active instances (those with recent logins)
- Daily retention for older backups
- Automatic cleanup of outdated backups

For MySQL databases, backups are created using `mysqldump` with transaction-safe options:
```bash
mysqldump --single-transaction --skip-lock-tables --defaults-extra-file=$userHome/etc/my.cnf $OPENPETRA_DBNAME | gzip > $backupfile
```

For PostgreSQL, the system uses `pg_dump`:
```bash
pg_dump -h $OPENPETRA_DBHOST -p $OPENPETRA_DBPORT -U $OPENPETRA_DBUSER $OPENPETRA_DBNAME | gzip > $backupfile
```

Restoration procedures include:
1. Database cleanup (dropping existing tables)
2. Data loading from backup files
3. Verification of restored data

### Database Initialization

The system supports initialization of new databases with proper security settings:

For MySQL:
```bash
echo "CREATE DATABASE IF NOT EXISTS \`$OPENPETRA_DBNAME\`;" >> $OpenPetraPath/tmp/createdb-MySQL.sql
echo "CREATE USER '$OPENPETRA_DBUSER'@'localhost' IDENTIFIED BY '$OPENPETRA_DBPWD';" >> $OpenPetraPath/tmp/createdb-MySQL.sql
echo "GRANT ALL ON \`$OPENPETRA_DBNAME\`.* TO \`$OPENPETRA_DBUSER\`@\`localhost\`;" >> $OpenPetraPath/tmp/createdb-MySQL.sql
```

For PostgreSQL:
```bash
su - postgres -c "psql -q -p $OPENPETRA_DBPORT -c \"CREATE USER \\\"$OPENPETRA_DBUSER\\\" PASSWORD '$OPENPETRA_DBPWD'\""
su - postgres -c "createdb -p $OPENPETRA_DBPORT -T template0 --encoding UTF8 -O $OPENPETRA_DBUSER $OPENPETRA_DBNAME"
```

Additionally, the system supports database migration through YAML-based import/export operations, allowing for schema and data transfers between different database systems.

## Code Quality Tools

OpenPetra includes several specialized tools for analyzing source code quality and ensuring consistent implementation patterns across the application.

### CodeChecker

The CodeChecker utility scans the OpenPetra codebase for potential issues using regular expressions. It focuses primarily on identifying problematic database access patterns, particularly those that might cause transaction handling problems.

Key features include:
- Recursive file scanning across the entire codebase
- Pattern matching with detailed reporting
- False-positive filtering system to avoid flagging acceptable exceptions
- Integration with CI systems through numeric exit codes

```csharp
// Example of a pattern the tool searches for
ReturnValue.Add("*Access.Load.* (no Argument after DB Transaction)", 
    @"Access\.Load.*[\s]*\((([^;]*)[\s]*null\))");
```

The tool is designed to be extensible, allowing developers to add new regex patterns to detect different code issues as they are identified.

### DeleteButtonWiki

This specialized tool analyzes the implementation of Delete button functionality across the OpenPetra application. It examines YAML UI definition files, manual code, and auto-generated files to identify:

- Screens with New/Delete buttons
- Proper implementation of reference counting
- Deletable flag implementation
- Client/server table name matching

The tool generates a wiki-formatted report showing which screens have proper delete functionality implementation and highlights potential issues with color coding.

```
=== Summary ===
  42 potential issues found in 28 files
  There are 87 New/Add buttons in the application
  There are 76 Delete/Remove buttons in the application
   52 of these call the auto-delete function
   24 of these call a manual delete function
     8 of these call the auto-delete function from manual code
```

## Documentation Generation

OpenPetra uses Doxygen for generating comprehensive API documentation from source code comments. The configuration is managed through the `doxygen.cfg` file, which defines various parameters controlling documentation generation.

Key configuration settings include:
- Project information (name, description)
- Input/output settings (source directories, output location)
- Formatting options (HTML style, diagrams, cross-references)

```
PROJECT_NAME           = OpenPetra
PROJECT_BRIEF          = Free Administration Software for Non-Profits
OUTPUT_DIRECTORY       = delivery/API-Doc
```

The documentation system is configured to generate documentation for C# files in the `csharp/ICT` directory with recursive searching enabled. The output is placed in the `delivery/API-Doc` directory with features like class diagrams, search functionality, and cross-referencing between documented elements.

This documentation serves as a crucial resource for developers to understand the API structure, class relationships, and function behaviors, facilitating maintenance and extension of the codebase.

## UI Component Analysis

OpenPetra includes tools for analyzing and maintaining consistency across UI components. The FilterButtonWiki tool is a prime example, tracking the implementation status of filter/find functionality across the application's screens.

This tool:
- Scans client-side YAML UI definition files
- Categorizes screens by template type
- Checks for UI elements like grids, details panels, and button panels
- Tracks related Mantis bug cases for implementation status

The tool generates a wiki-formatted report showing:
- Which screens have filter/find functionality implemented
- Which screens need implementation
- Which screens don't require filter functionality by design

```
=== Summary ===
  78 screens with grdDetails
  65 screens with pnlDetails
  72 screens with pnlButtons
  58 screens have all the above
  32 screens have Filter/Find
  14 screens do not require Filter/Find
  There are 12 screens that are still potential candidates for Filter/Find
```

This systematic approach ensures UI consistency across the application and helps track implementation progress for features that span multiple screens.

## Server Administration Console

The PetraServerAdminConsole provides a command-line interface for server administration, offering both interactive and scripted operation modes. This tool enables administrators to:

- View connected clients and their status
- Disconnect specific clients
- Shut down the server (controlled or immediate)
- Import/export databases using YAML format
- Send administrative messages to connected clients
- Perform database maintenance operations

The console connects to the server using a security token mechanism for authentication:

```csharp
private static string NewSecurityToken()
{
    SecurityToken = Guid.NewGuid().ToString();
    string TokenFilename = TAppSettingsManager.GetValue("Server.PathTemp") +
                           Path.DirectorySeparatorChar + "ServerAdminToken" + SecurityToken + ".txt";

    StreamWriter sw = new StreamWriter(TokenFilename);
    sw.WriteLine(SecurityToken);
    sw.Close();
    return SecurityToken;
}
```

The tool supports both interactive menu-driven operation and command-line parameter-based execution, making it suitable for both manual administration and automated scripts.

## Maintenance Tools Ecosystem

![Maintenance Tools Ecosystem](https://mermaid.ink/img/pako:eNqVVE1v2zAM_SuETg2QBVnTbOtlwIABG7Zh2A5DL7RFO0JlUZDoNEX-eyXZTuI6xbZeYvHxkY-PpDvBXBfAEtZoV6pGWvGgdGXFXZVrLVZK-qdaGrHVRbGVRjwqVYpfUqmtWCvZSPvYiLVxQlvxQ5fKOmGdMlp8k9qKB-mEfVJO3Cv9qIwQd9JJoYx1Qhm5E9-VFXdK_5YbZYUyW2lFLVvlxLXUG-nEVhpRKCeUdmKjK6nFRjnxVVZSi1JWUotKPkgtbqXZyUqLnTRCG-XEVjnxTZZai0JpUcqNdKKQlRa3qhBbZUQpjdgpLe6V3kkjCmWEVlbcKyO0MuJGGXGjzFYZcaOMuJXmQRpxI82DMuJWmZ0y4k6ZB2XErTIPyohbZR6VEXfKPCoj7pR5UkbcK_OkjLhX5lkZca_MszLiXplnZcSDMs_KiAdlXpQRD8q8KCMelHlVRjwo86qMeFTmVRnxqMybMuJRmTdlxKMyb8qIJ2XelBFPyrwrI56UeVdGPCnzoYx4VuZDGfGszIcy4lmZT2XEszKfyohnZb6UEc_KfCkjXpT5Ukb8Bx9QMGzQlbKGnbFcNqVjZ9Kw3GHpvGfnDDfgwDPHLmRVyxw8O4fKQg0-Z2fSVVDBGbsEXzroYMbOoMEGHLuzDnJdQQMzNgdfQwcOZuwCGvDQwYxdQgMWOpixK2jAQAczdg0NaOhgxm6gAQUdzNgcGrDQwYxdQwMGOpixW2hAQwczdgcNSOhgxu6hAQ4dzNgDNMChgxl7hAYYdDBjT9AAhQ5m7BkaYNDBjL1AAwQ6mLFXaIBABzP2Bg0w6GDG3qEBAh3M2Ac0wKCDGfsEDTDoYMa-QAMMOPAP_NXwlQ?type=png)

The OpenPetra maintenance tools ecosystem consists of interconnected components that work together to support the overall health and functionality of the system. The diagram illustrates how these tools interact with different parts of the OpenPetra infrastructure.

At the core of the ecosystem is the OpenPetra Server, which is managed by the openpetra-server.sh script. This script interfaces with both MySQL and PostgreSQL databases, handling operations like backups, restores, and initialization.

The PetraServerAdminConsole provides a command-line interface for administrators to interact with the server, offering functionality for client management, database operations, and server control.

Code analysis tools like CodeChecker and DeleteButtonWiki analyze the source code and UI components, providing feedback on implementation quality and consistency. These tools help maintain code standards and identify potential issues before they cause problems in production.

Documentation generation tools create reference materials for developers, while update mechanisms ensure that application components can be safely updated while preserving user settings.

This integrated ecosystem ensures that administrators and developers have the tools they need to effectively maintain and extend the OpenPetra system.

## Application Update Mechanisms

The DevelopersAssistantUpdater tool facilitates updating application components while preserving user settings. This utility is designed to update running applications by replacing their executable files with newer versions.

Key features include:
- Waiting for the target application to shut down before updating
- Copying both executable and PDB files for debugging support
- Logging operation results
- Relaunching the updated application with status information

```csharp
// Wait for the existing application to shut down
Thread.Sleep(4000);

// Copy the executable file
File.Copy(sourcePath, targetPath, true);

// Also copy the PDB file for debugging
File.Copy(sourcePath.Replace(".exe", ".pdb"), targetFilename.Replace(".exe", ".pdb"), true);
```

The tool uses command-line arguments to determine the target path and displays success/failure messages to the user. After updating, it relaunches the application with a command-line parameter indicating whether the update was successful:

```csharp
// Launch the target application with status parameter
msg = "\"/M:Update";
msg += (success) ? "Success\"" : "Fail\"";
ProcessStartInfo si = new ProcessStartInfo(targetPath, msg);
Process p = new Process();
p.StartInfo = si;
p.Start();
```

This mechanism ensures that applications can be updated without losing user settings or requiring manual reconfiguration, streamlining the update process for both developers and end users.

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #