# InvoiceManager

InvoiceManager is a web-based application designed to help accountants and business owners manage invoices efficiently. The application simplifies invoice creation, tracking, and management, making financial administration more streamlined.

## Current State

The application is currently built with ASP.NET Core 2.0 and uses Razor Pages for its UI. While functional, the codebase could benefit from upgrades to modern frameworks and software engineering practices. The application allows users to:

- Create, view, update, and delete invoices
- Filter invoices by date
- Assign invoices to specific accountants
- Track invoice status (Submitted, Approved)

## Motivation for Refactoring

This project was created some time ago and needs modernization. The primary goals for refactoring are:

1. Upgrade to newer .NET Core versions
2. Implement clean architecture patterns
3. Improve code testability
4. Enhance separation of concerns
5. Apply modern software engineering practices

## Development Roadmap

### Phase 1: Framework Upgrades and Core Improvements
- Upgrade from .NET Core 2.0 to .NET Core 3.1, then to .NET 5
- Implement Repository Pattern to better separate data access from business logic
- Add proper unit tests with xUnit
- Refactor authentication and authorization mechanisms

### Phase 2: Architecture Enhancements
- Implement CQRS (Command Query Responsibility Segregation) pattern with MediatR
- Add FluentValidation for improved request validation
- Reorganize solution structure following Clean Architecture principles
- Create proper domain models, separate from entity models

### Phase 3: Feature Extensions
- Build RESTful API endpoints
- Implement better error handling and logging
- Add pagination, filtering, and sorting to listings
- Create reporting functionality
- Implement a more robust security model

### Phase 4: UI/UX Improvements
- Modernize UI components
- Implement responsive design improvements
- Add client-side validations
- Create better user feedback mechanisms

## Architecture Vision

The refactored application will follow a clean architecture approach with clear separation of concerns:

- **Core Layer**: Contains business entities, interfaces, and business logic
- **Infrastructure Layer**: Handles data persistence, external services, and infrastructure concerns
- **Application Layer**: Orchestrates the use cases of the application
- **Presentation Layer**: Handles the UI and API endpoints

## Technologies & Patterns

The refactored application will utilize:

- .NET 5+
- Entity Framework Core
- ASP.NET Core Identity
- CQRS Pattern
- Repository Pattern
- Dependency Injection
- Unit Testing with xUnit and Moq
- FluentValidation
- MediatR
- RESTful APIs

## Getting Started

### Prerequisites
- .NET Core SDK (currently 2.0, will be upgraded)
- SQL Server (LocalDB or full instance)
- Visual Studio 2019+ or VS Code

### Setup
1. Clone the repository
2. Set password with the Secret Manager tool: