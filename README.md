# Portfolio Web API

[![.NET](https://img.shields.io/badge/.NET-8-blue.svg)](https://dotnet.microsoft.com/download/dotnet/8.0)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

A robust and scalable backend API for a personal portfolio and blog platform, built with .NET 8 and Clean Architecture principles.

---

## 🌟 Introduction

Portfolio Web API provides a complete backend solution for managing portfolio content, blog posts, and user interactions. It's designed with a focus on separation of concerns, scalability, and modern .NET practices, making it a strong foundation for any personal or professional web presence.

## ✨ Key Features

- **Clean Architecture:** A clear separation between Domain, Application, and Infrastructure layers.
- **CQRS with MediatR:** Decouples read and write operations for improved performance and maintainability.
- **Cloud-Native File Storage:** Seamlessly integrated with S3-compatible object storage (like Cloudflare R2 or MinIO) for media uploads.
- **JWT Authentication:** Secure endpoints using JSON Web Token-based authentication and authorization.
- **Entity Framework Core:** Utilizes EF Core for data persistence with a PostgreSQL database.
- **Minimal APIs:** Modern, clean, and high-performance API endpoints.
- **Centralized Exception Handling:** Custom middleware for consistent error responses.

## 🏗️ Architectural Overview

The project strictly follows **Clean Architecture** principles, ensuring the core business logic (Domain) is independent of any external frameworks or technologies.

- **`Core/DomainLayer`**: Contains the core of the application, including all entities, enums, and domain-level abstractions. It has no external dependencies.
- **`Core/ApplicationLayer`**: Orchestrates the business logic. It contains CQRS command/query handlers, DTOs, validation logic (using FluentValidation), and interfaces for repositories and services (`IFileStorageService`, `IJwtService`, etc.).
- **`External/InfrastructureLayer`**: Provides concrete implementations for the services defined in the Application Layer, such as `JwtService` and `FileStorageService`.
- **`External/PersistenceLayer`**: Implements data access logic using Entity Framework Core. It contains the `DbContext`, repository implementations, and database configurations.
- **`WebApi`**: The presentation layer and the application's entry point. It's responsible for exposing the API endpoints, handling HTTP requests, and managing application configuration and middleware.

## 🛠️ Technology Stack

| Category           | Technology / Library                               |
| ------------------ | -------------------------------------------------- |
| **Framework**      | ASP.NET Core 8                                     |
| **Architecture**   | Clean Architecture, CQRS, Minimal APIs             |
| **Mediation**      | MediatR                                            |
| **Database**       | PostgreSQL, Entity Framework Core 8                |
| **Authentication** | JWT (JSON Web Tokens)                              |
| **File Storage**   | S3-Compatible (Cloudflare R2, AWS S3, MinIO)       |
| **Validation**     | FluentValidation                                   |
| **Mapping**        | AutoMapper                                         |

## 🚀 Getting Started

Follow these instructions to get a local copy of the project up and running for development and testing purposes.

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/download/) (or any other database compatible with EF Core)
- An S3-compatible object storage provider (or a local instance of [MinIO](https://min.io/)).

### 1. Installation & Setup

Clone the repository to your local machine:

```bash
git clone https://github.com/your-username/BartugWeb.git
cd BartugWeb
```

### 2. Configuration (User Secrets)

This project uses the .NET User Secrets manager to handle sensitive data during development. **Do not store secrets in `appsettings.json`.**

Navigate to the WebApi project directory to initialize secrets if needed:
```bash
cd src/BartugWeb.WebApi
```

Set the following secrets using the .NET CLI:

- **Database Connection String:**
  ```bash
  dotnet user-secrets set "ConnectionStrings:PostgreSqlDefault" "Your_PostgreSQL_Connection_String"
  ```

- **JWT Secret Key:**
  ```bash
  dotnet user-secrets set "JwtSettings:SecretKey" "Your_Super_Secret_And_Long_Enough_Key_For_HS256"
  ```

- **Storage Credentials:**
  ```bash
  dotnet user-secrets set "Storage:Endpoint" "Your_R2_S3_Endpoint"
  dotnet user-secrets set "Storage:AccessKeyId" "Your_R2_Access_Key_Id"
        dotnet user-secrets set "Storage:SecretAccessKey" "Your_R2_Secret_Access_Key" --project src/BartugWeb.WebApi
        dotnet user-secrets set "Storage:PublicBaseUrl" "Your_R2_Public_Base_Url" --project src/BartugWeb.WebApi
        ```
  
    *(Note: The `BucketName` can be kept in the main `appsettings.json` as it is not typically secret.)*

### 3. Database Migrations

Once your connection string is configured, apply the Entity Framework migrations to create the database schema. Run this command from the **root directory** of the project:

```bash
dotnet ef database update --project src/External/BartugWeb.PersistanceLayer --startup-project src/BartugWeb.WebApi
```

### 4. Running the Application

Run the API from the **root directory**:

```bash
dotnet run --project src/BartugWeb.WebApi
```

The API will be available at `https://localhost:7024` (or a similar port). The Swagger UI, for exploring and testing the endpoints, can be accessed at `https://localhost:7024/swagger`.

## 📜 License

Distributed under the MIT License. See `LICENSE` for more information.
