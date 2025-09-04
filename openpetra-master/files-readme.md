# OpenPetra

OpenPetra is a free, open-source software system designed to streamline administrative operations for non-profit organizations. Written primarily in C# with a web-based interface, this platform implements comprehensive CRM and ERP functionality with a special focus on donation processing. This sub-project provides the core infrastructure for the OpenPetra application, establishing the foundation upon which specialized modules operate.

The system architecture delivers these key technical capabilities:

- Cross-platform compatibility through web-based interface
- Integrated CRM and financial management systems
- Donation processing and tracking workflows
- Multi-currency support with International Clearing House functionality
- Extensible data export mechanisms

## Identified Design Elements

1. **Modular Architecture**: Core functionality is separated into distinct modules (contact management, accounting, sponsorship) that interact through well-defined interfaces
2. **Web-First Design**: Modern web interface built on responsive frameworks ensures accessibility across devices
3. **Continuous Integration Pipeline**: AppVeyor configuration maintains build integrity across Ubuntu environments with automated testing
4. **Open Source Ecosystem**: GPL v3 licensing ensures code remains freely available while protecting user freedoms
5. **Security-Focused Development**: Structured vulnerability reporting and version support strategy prioritizes maintaining secure deployments

## Overview
The architecture emphasizes maintainability through modular design, cross-platform compatibility via web technologies, and comprehensive administrative functionality for non-profit organizations. The codebase follows open-source best practices with clear documentation and security policies. The system is designed to reduce operational costs through efficient administrative workflows while providing robust financial management capabilities that accommodate international operations.

## Business Functions

### Documentation
- `LICENSE` : GNU General Public License v3 (GPLv3) document defining the legal terms for free software distribution and modification.
- `README.md` : Project README for OpenPetra, an open-source administration system for non-profit organizations.
- `SECURITY.md` : Security policy document outlining vulnerability reporting procedures and version support for OpenPetra.

### Continuous Integration
- `appveyor.yml` : AppVeyor CI configuration file for building and testing OpenPetra on Ubuntu 20.04 with MySQL.

## Files
### LICENSE

This file contains the complete text of the GNU General Public License version 3 (GPLv3), which is a free, copyleft license for software and other works. The license guarantees users' freedom to share, modify, and distribute software while ensuring it remains free for all users. It outlines specific terms and conditions for copying, distribution, and modification of covered works, including requirements for preserving copyright notices, providing source code, and handling patents. The license includes sections on definitions, basic permissions, protecting users' legal rights, conveying verbatim and modified copies, additional terms, termination conditions, automatic licensing of downstream recipients, and disclaimers of warranty and liability.
### README.md

This README file provides an introduction to OpenPetra, an open-source software system designed for non-profit organization administration. It explains that OpenPetra offers CRM and ERP functionality with a focus on processing donations. The file includes links to the project website, demo site, and community forums. It also provides instructions for setting up a development environment on various Linux distributions using a simple curl command, along with basic usage instructions for developers and information about accessing the web interface. The document concludes with license information, noting that OpenPetra is licensed under GPL v3.

 **Code Landmarks**
- `Line 16`: Provides a link to a public demo instance for testing the application
- `Line 21`: References a free test installation service for users
- `Line 31`: Single-command development environment setup using curl pipe to bash
- `Line 43`: Default credentials for the system are provided for local testing
### SECURITY.md

This security policy document outlines OpenPetra's version support strategy and vulnerability reporting process. It clarifies that only the latest release (on the prod branch) is supported, with new releases created for security updates. Development occurs on test and dev branches. The document provides instructions for reporting vulnerabilities to info@openpetra.org and expresses gratitude to those who report issues, emphasizing the project's commitment to security.
### appveyor.yml

This AppVeyor configuration file defines the continuous integration setup for OpenPetra. It specifies Ubuntu 20.04 as the build environment with MySQL and Node.js 14. The file orchestrates the installation process using the OpenPetra installer script, runs tests with NUnit, and builds an RPM package for deployment. Key sections include environment setup, installation commands, test execution with a demo database, and package creation for deployment.

 **Code Landmarks**
- `Line 10-13`: Defines specific environment requirements including MySQL, Node.js 14, and Mono for the cross-platform .NET application
- `Line 17`: Uses the OpenPetra installer script to set up a development environment with a single command
- `Line 21-22`: Downloads a specific demo database for testing from GitHub and runs tests with 'nant test-without-display'
- `Line 24`: Builds an RPM package before deployment, indicating Linux distribution packaging

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #