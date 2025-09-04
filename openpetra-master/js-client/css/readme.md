## JavaScript Client CSS

The JavaScript Client CSS subproject of OpenPetra provides the styling foundation for the web-based interface of this open-source non-profit management system. This collection of CSS files establishes a consistent visual language and responsive behavior across the application through a carefully structured approach to frontend styling.

The subproject implements a Bootstrap-based design system with custom extensions for OpenPetra-specific components. It provides these capabilities to the Petra program:

- Responsive layout management for various screen sizes
- Consistent styling for UI components including forms, navigation, and modals
- Specialized styling for financial data presentation (debit/credit coloring)
- Support for interactive components like autocomplete fields
- Hierarchical navigation through a collapsible sidebar system

## Identified Design Elements

1. **Bootstrap Integration**: The core styling leverages Bootstrap's framework (imported in app.css) for responsive grid layouts and component styling
2. **Component-Specific Styling**: Separate CSS files target specific functional areas (login, sidebar, autocomplete) for modular maintenance
3. **Responsive Design Patterns**: Implementation of off-canvas navigation and adaptive layouts for mobile and desktop experiences
4. **Visual Hierarchy**: Consistent spacing, typography, and color schemes establish clear information hierarchy
5. **Application Mode Awareness**: Utility classes for showing/hiding elements based on the current application context

## Architecture

The CSS architecture follows a component-based approach with clear separation of concerns. The main.css file establishes core layout and shared styling, while specialized components have dedicated files (sidebar.css, autocomplete.css). The styling supports both traditional web interfaces and specialized report formatting, with careful attention to responsive behavior across device sizes.

## Business Functions

### User Interface Components
- `login.css` : CSS stylesheet for OpenPetra login page defining form styling and callout notification components.

### Core Styling
- `app.css` : CSS import file that includes Bootstrap's CSS framework for the OpenPetra JS client.
- `main.css` : Main CSS stylesheet for the OpenPetra web client interface defining layout, navigation, and visual styling.

### Form Controls
- `autocomplete.css` : CSS styling for autocomplete functionality in the OpenPetra web client interface.

### Reporting
- `report.css` : CSS file defining minimal styling for reports in the OpenPetra system.

### Navigation
- `sidebar.css` : CSS styling for the sidebar navigation component in the OpenPetra web client interface.

## Files
### app.css

This CSS file serves as the main stylesheet entry point for the OpenPetra JavaScript client. It imports Bootstrap's minified CSS framework from the node_modules directory, enabling the application to utilize Bootstrap's responsive grid system, components, and styling utilities. The file is minimal, containing only a single import statement, suggesting that Bootstrap provides the core styling for the application interface.

 **Code Landmarks**
- `Line 1`: Uses CSS import directive to include external Bootstrap CSS rather than embedding styles directly, allowing for cleaner separation of concerns.
### autocomplete.css

This CSS file defines styling for autocomplete functionality in the OpenPetra web client. It styles the autocomplete container, input fields, dropdown items, and interactive states. The file establishes positioning, colors, borders, and hover effects for the autocomplete component. Key selectors include .autocomplete for the container, .__input for form fields, .autocomplete-items for the dropdown list, and .autocomplete-active for keyboard navigation highlighting. It also includes a utility class for modal dialogs with visible overflow.

 **Code Landmarks**
- `Line 1`: Uses relative positioning for the autocomplete container to properly position the dropdown results
- `Line 18`: Sets z-index to 99 to ensure dropdown appears above other page elements
- `Line 38`: Includes a separate modal-wide class that modifies Bootstrap modal behavior to support visible overflow
### login.css

This CSS file defines the styling for the OpenPetra login page. It sets up the basic page layout with padding and background color, and implements a centered sign-in form with specific styling for form elements. The file contains two main sections: form-signin styles that control the appearance of the login form including input fields, and bs-callout styles that define notification boxes in various contextual colors (default, primary, success, danger, warning, and info). Each callout style includes specific border colors and heading text colors.

 **Code Landmarks**
- `Line 8`: Implements a centered, fixed-width form container that's optimized for login screens
- `Line 22`: Uses vendor prefixes for box-sizing to ensure cross-browser compatibility
- `Line 47`: Includes a complete callout component system borrowed from Twitter Bootstrap with citation
### main.css

This CSS file defines the core styling for the OpenPetra web client interface. It establishes layout rules for the main application structure including navigation, modals, toolbars, and content areas. Key styling includes responsive off-canvas navigation, modal dialog configurations, form layouts, and visual treatments for financial data (debit/credit coloring). The file also contains utility classes for showing/hiding elements based on application mode, styling for dashboard elements, tab navigation, and specialized containers for lists and scrollable content. Notable styling focuses on the top navigation bar, sidebar behavior, and consistent spacing throughout the interface.

 **Code Landmarks**
- `Line 17`: Z-index management for dropdown menus ensures proper layering in the UI
- `Line 31`: Responsive off-canvas navigation pattern implementation for sidebar toggle functionality
- `Line 69`: Modal dialog width configuration to support wide-format dialogs (90% width)
- `Line 124`: Mode-based display control using attribute selectors for create/edit contexts
- `Line 141`: Anchor positioning adjustment to account for fixed top navigation bar
### report.css

This CSS file provides minimal styling for reports in the OpenPetra system. It contains only two rules: one that hides the head element and another that applies bold formatting to column headings. The file is extremely simple, suggesting that either report styling is handled elsewhere or that reports have intentionally minimal styling.
### sidebar.css

This CSS file defines the styling for the sidebar navigation component in the OpenPetra web client. It implements a collapsible, hierarchical menu system with dark theme styling. The file handles sidebar positioning, menu item appearance, collapse/expand indicators, indentation for nested menu levels, and responsive behavior for mobile devices. Key styling includes fixed positioning, color schemes for the dark theme, hover states, expansion indicators, and padding for hierarchical menu levels. The CSS also manages the sidebar's responsive behavior, collapsing to a minimal width with icon-only display on smaller screens.

 **Code Landmarks**
- `Line 91`: Implements text overflow handling for sidebar items with ellipsis
- `Line 107`: Responsive design transforms sidebar to minimal width on mobile devices
- `Line 142`: Z-index management for proper overlay of collapsed submenus on small screens
- `Line 165`: Custom visibility handling for collapse/expand animations

[Generated by the Sage AI expert workbench: 2025-03-30 02:22:57  https://sage-tech.ai/workbench]: #