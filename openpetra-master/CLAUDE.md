# OpenPetra Development Guide

## Project Overview

OpenPetra is a comprehensive open-source ERP system designed for non-profit organizations. It provides CRM functionality for contact management and ERP features for financial accounting, with a special focus on donation processing and receipting.

**Key Features:**
- Contact Management (CRM) - Partners, Families, Organizations
- Financial Management - General Ledger, Accounts Payable/Receivable, Banking
- Donation Processing - Gift batches, receipts, statements
- Conference Management - Registration, accommodation, transportation
- Personnel Management - Staff and volunteer tracking
- Multi-currency and multi-language support
- Extensive reporting capabilities

## Technology Stack

### Backend
- **Language:** C# (.NET Framework 4.7)
- **Architecture:** N-tier client-server architecture
- **Build System:** NAnt (custom build scripts)
- **Databases:** PostgreSQL (primary), MySQL (supported)
- **ORM:** Custom code generation from petra.xml

### Frontend
- **Web Client:** JavaScript (ES5), jQuery, Bootstrap 4
- **Bundler:** Browserify
- **Testing:** Cypress for E2E tests
- **Icons:** FontAwesome 5

### Dependencies
- **NUnit** 3.13.3 - Unit testing framework
- **Npgsql** 4.1.10 - PostgreSQL driver
- **MySqlConnector** 2.2.5 - MySQL driver
- **MailKit/MimeKit** 3.5.0 - Email functionality
- **PDFsharp** 1.50 - PDF generation
- **NPOI** 2.6.0 - Excel file manipulation
- **Newtonsoft.Json** 13.0.2 - JSON processing

## Project Structure

```
openpetra-master/
├── csharp/                 # C# source code
│   ├── ICT/               # Core framework (Common, Petra modules)
│   │   ├── Common/        # Base utilities, logging, exceptions
│   │   ├── BuildTools/    # Code generation tools
│   │   ├── Petra/         # Main application code
│   │   │   ├── Client/    # Client-side code
│   │   │   ├── Server/    # Server-side code
│   │   │   ├── Shared/    # Shared libraries
│   │   │   └── Plugins/   # Plugin system
│   │   └── Testing/       # Test framework
│   └── ThirdParty/        # Third-party libraries
├── js-client/             # JavaScript web client
│   ├── src/               # Source files
│   │   ├── forms/         # UI forms
│   │   ├── lib/           # Core libraries
│   │   └── index.js       # Entry point
│   └── cypress/           # E2E tests
├── db/                    # Database schemas and scripts
│   ├── petra.xml          # Master database definition
│   ├── basedata/          # Base data CSV files
│   └── patches/           # Database migration scripts
├── inc/                   # Build infrastructure
│   ├── nant/              # NAnt build scripts
│   ├── nanttasks/         # Custom NAnt tasks
│   └── template/          # Code generation templates
├── i18n/                  # Internationalization
│   ├── *.po               # Translation files
│   └── template.pot       # Translation template
├── XmlReports/            # Report definitions
├── setup/                 # Installation scripts
├── demodata/              # Demo/test data
└── sage/                  # Sage MCP analysis & documentation
    ├── application-topics/ # Business function documentation
    ├── technical-topics/  # Technology subject documentation
    ├── diagrams/          # Architecture diagrams
    └── readme.md          # Comprehensive analysis document
```

## Sage MCP Integration

OpenPetra is cataloged in the Sage MCP system as project `petra` (cycle 12), providing extensive AI-generated documentation and analysis of the codebase.

### Available Sage Resources

The `./sage/` directory contains comprehensive analysis including:

- **Application Topics (30 subjects):** Business function documentation covering CRM Functions, Finance modules, Conference Management, Personnel, Reporting, and System Management
- **Technical Topics (45+ subjects):** Technology documentation covering Build systems, Database layers, Client-Server communication, Infrastructure components, and Testing frameworks
- **Architecture Diagrams:** Visual representations of system architecture and data flows
- **Comprehensive Analysis:** 384KB+ detailed technical documentation

### Key Sage-Documented Business Functions
- **CRM Functions** - Contact and partner relationship management
- **Finance** - Accounting, Banking, Budgeting, Gift Processing
- **Conference Management** - Event planning, accommodations, transportation
- **Personnel** - Staff and volunteer management
- **Sponsorship** - Child and donor management programs
- **System Management** - Security, users, access control

### Key Sage-Documented Technology Subjects
- **Build Systems** - NAnt scripts and CI/CD processes
- **Database Layer** - PostgreSQL/MySQL abstraction and data models
- **Client Technologies** - JavaScript, HTML/CSS, TypeScript
- **Server Infrastructure** - REST/SOAP services, authentication, logging
- **Testing** - Unit tests and Cypress E2E testing
- **Tools** - Backup/restore, data import/export, maintenance

### Using Sage Documentation

To leverage the extensive Sage analysis:

```bash
# View comprehensive project analysis
cat sage/readme.md

# Explore specific business functions
ls sage/application-topics/

# Review technical implementation details
ls sage/technical-topics/

# View architecture diagrams
ls sage/diagrams/
```

## Build System

OpenPetra uses NAnt as its build system with extensive custom tasks:

### Common Build Commands

```bash
# Clean build
nant quickClean

# Generate solution and compile
nant generateSolution

# Quick compile (after initial setup)
nant quickCompile

# Compile specific project
nant compileProject -D:name=Ict.Common

# Run tests
nant test-without-display
nant test -D:file=Ict.Testing.lib.Common.dll

# Database operations
nant recreateDatabase
nant resetDatabase
nant patchDatabase
```

## Code Generation

OpenPetra uses extensive code generation from XML definitions:

1. **Database Layer** - Generated from `petra.xml`:
   - Table definitions
   - Data access objects
   - Typed datasets

2. **Client-Server Glue** - Auto-generated interfaces:
   - RPC interfaces
   - Service contracts
   - Instantiators

3. **ORM Layer** - Custom object-relational mapping:
   - Generated via `generateORM` targets
   - Cached tables support
   - Database abstraction

## Key Development Patterns

### Namespace Convention
- `Ict.*` - Infrastructure and framework code
- `Ict.Petra.Client.*` - Client-side modules
- `Ict.Petra.Server.*` - Server-side modules
- `Ict.Petra.Shared.*` - Shared data structures

### Module Structure
- **Finance** - GL, AP, AR, Gift processing
- **MPartner** - Partner/contact management
- **MPersonnel** - HR and personnel
- **MConference** - Conference management
- **MSysMan** - System administration

### Database Access Pattern
1. Use generated table access classes
2. Transactions via `TDBTransaction`
3. Database abstraction through `DBAccess`
4. Type-safe datasets for data transfer

## Testing Strategy

### Unit Tests
- NUnit 3.x framework
- Located in `*/test/` directories
- Run via `nant test`

### Integration Tests
- Server API tests
- Database operation tests
- Cross-module functionality

### E2E Tests
- Cypress tests in `js-client/cypress/`
- Cover main user workflows
- Run via `nant test-client`

## Development Workflow

1. **Initial Setup**
   ```bash
   nant generateSolution    # First time setup
   nant initConfigFiles    # Configure server
   ```

2. **Making Changes**
   - Edit code files
   - Run `nant quickCompile`
   - Test changes locally

3. **Database Changes**
   - Modify `db/petra.xml`
   - Run `nant generateORM`
   - Run `nant recreateDatabase`

4. **Adding New Files**
   - Create .cs file in appropriate directory
   - Run `nant generateProjectFiles`
   - Compile with `nant quickCompile`

## Common Tasks

### Adding a New Form
1. Create form in `js-client/src/forms/`
2. Add navigation entry in `UINavigation.yml`
3. Implement server-side logic if needed
4. Add tests

### Creating Reports
1. Define report in `XmlReports/`
2. Create settings in `XmlReports/Settings/`
3. Implement data retrieval methods
4. Test report generation

### Database Migrations
1. Create patch file in `db/patches/`
2. Update version in `db/version.txt`
3. Test with `nant patchDatabase`

## Configuration

### Server Configuration
- Main config: `Server-postgresql.config`
- Located in `etc/` after setup
- Key settings: database, ports, paths

### Client Configuration
- Web client uses `ajax.js` for API calls
- Server URL configured in client code
- Authentication via session cookies

## Debugging Tips

1. **Enable Logging**
   - Set log levels in server config
   - Check `log/` directory for output

2. **Database Queries**
   - Enable SQL logging in config
   - Use database query profiler

3. **Client-Server Communication**
   - Browser dev tools for API calls
   - Server logs for request handling

## Best Practices

1. **Code Style**
   - Follow existing patterns
   - Use generated code where possible
   - Keep manual edits minimal

2. **Database Changes**
   - Always update `petra.xml`
   - Create migration scripts
   - Test upgrades thoroughly

3. **Testing**
   - Write unit tests for new features
   - Update E2E tests for UI changes
   - Run full test suite before commits

4. **Documentation Research**
   - Consult Sage analysis in `./sage/` for architectural insights
   - Review relevant application-topics for business context
   - Check technical-topics for implementation patterns

## Resources

- [Official Website](http://www.openpetra.org)
- [Demo Server](https://demo.openpetra.org)
- [Forum (English)](https://forum.openpetra.org)
- [Forum (German)](https://forum.openpetra.de)
- [Hosted Service](https://openpetra.ossaas.de)
- **Sage Documentation:** `./sage/readme.md` - Comprehensive technical analysis

## License

OpenPetra is licensed under GPL v3 or later. Third-party components may have different licenses - check `csharp/ThirdParty/` for details.