# Ruby Getting Started - Claude Context

## Project Overview
This is a barebones Rails 4.2.0 application designed for deployment to Heroku. It's a simple Ruby on Rails demo app with basic CRUD functionality for widgets.

## Technology Stack
- **Framework**: Ruby on Rails 4.2.0
- **Database**: PostgreSQL (production), SQLite (development)
- **Server**: Puma
- **Asset Pipeline**: Sass, CoffeeScript, Uglifier
- **Frontend**: jQuery, Turbolinks, Bootstrap styling

## Key Components
- **Models**: Widget (basic CRUD model)
- **Controllers**: 
  - WelcomeController (homepage)
  - WidgetsController (CRUD operations)
- **Views**: ERB templates with Bootstrap styling
- **Database**: Simple widget schema with name and description fields

## Development Commands
```bash
# Setup
bundle install
bundle exec rake db:create db:migrate

# Start development server
foreman start web
# OR
bundle exec rails server

# Run tests
bundle exec rake test

# Database operations
bundle exec rake db:migrate
bundle exec rake db:seed
```

## Deployment
- Configured for Heroku deployment
- Uses `rails_12factor` gem for production
- Puma server configured via Procfile

## File Structure
- `app/` - Main application code (MVC pattern)
- `config/` - Rails configuration
- `db/` - Database migrations and schema
- `test/` - Test files
- `sage/` - Project documentation and topics

## Testing
- Uses Rails' built-in test framework
- Test files located in `test/` directory
- Fixtures available for widget model

## Notes
- This is a legacy Rails 4.2 application
- Designed as a learning/demo project
- Simple architecture suitable for beginners