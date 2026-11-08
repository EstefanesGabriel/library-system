# Library Management API

A RESTful API for library management, developed with C# and ASP.NET Core. The project provides functionality for managing books, users, institutions, and loan records, with SQL Server persistence and JWT-based authentication.

## Tech Stack

- **Language:** C#
- **Framework:** .NET 10 / ASP.NET Core Web API
- **Database:** Microsoft SQL Server
- **ORM:** Entity Framework Core
- **Authentication:** JSON Web Tokens (JWT)
- **API Documentation:** Swagger / OpenAPI
- **Additional features:** Password hashing, dependency injection, SMTP email integration, and Entity Framework Core migrations

## Features

- Book registration, listing, updating, and deletion.
- Institution and user management.
- Book search and paginated listings.
- Loan record retrieval.
- User registration and login with JWT token generation.
- Password hashing and email integration for account registration.
- Database persistence through Entity Framework Core.

## Project Structure

```text
library-system/
├── Controllers/      # REST API endpoints
├── Data/             # Database context and entity mappings
├── Enums/            # Application enumerations
├── Extensions/       # Helper extensions
├── Migrations/       # Entity Framework Core migrations
├── Models/           # Domain entities
├── Services/         # Authentication and email services
├── ViewModels/       # Request and response models
├── Program.cs        # Application configuration and startup
└── SistemaBiblioteca.csproj
```

## Getting Started

### Prerequisites

- .NET 10 SDK
- Microsoft SQL Server
- Entity Framework Core CLI tools
- SMTP credentials for email-related functionality

### Installation

Clone the repository:

```bash
git clone https://github.com/EstefanesGabriel/library-system.git
cd library-system
```

Restore dependencies:

```bash
dotnet restore
```

### Configuration

The application requires a SQL Server connection string, a JWT signing key, and valid SMTP settings.

Configuration values are read from ASP.NET Core configuration sources. For local development, environment variables or a local configuration provider can be used.

Required configuration keys include:

```text
ConnectionStrings__DefaultConnection
JwtKey
Smtp__Host
Smtp__Port
Smtp__UserName
Smtp__Password
```

Use a sufficiently long, randomly generated JWT signing key. Do not commit real passwords, tokens, or connection credentials.

### Database Setup

Apply the existing Entity Framework Core migrations:

```bash
dotnet ef database update
```

### Run the Application

```bash
dotnet run --launch-profile library
```

The configured development addresses are:

- `https://localhost:7035`
- `http://localhost:5247`

Swagger UI is available in the Development environment at:

`https://localhost:7035/swagger`

## API Endpoints

| Method | Endpoint | Description |
|---|---|---|
| GET | `/v1/books` | List books |
| GET | `/v1/books/{id}` | Get a book by ID |
| GET | `/v1/books/autor/{autor}` | Find books by author |
| POST | `/v1/books` | Create a book |
| PUT | `/v1/books/{id}` | Update a book |
| DELETE | `/v1/books/{id}` | Delete a book |
| GET | `/v1/users` | List users |
| GET | `/v1/institutions` | List institutions |
| GET | `/v1/loans` | Retrieve loan records |
| POST | `/v1/accounts` | Register an account |
| POST | `/v1/accounts/login` | Authenticate a user |

Additional user and institution CRUD endpoints are implemented in their respective controllers.

## Current Status

This repository is a software development and learning project focused on backend engineering, REST API development, and relational database integration.

The implementation requires additional security review and testing before production use. In particular, authorization coverage, sensitive response data, and file upload validation should be reviewed.

Automated test execution and production deployment have not been verified as part of this documentation.
