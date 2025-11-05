# 🧾 Product Requirements Document (PRD)

## Project Name
**BartugWeb – Clean Architecture Backend**

## Owner
**Bartuğ Kaan Çelebi**

## Goal
Develop a **scalable, modular, and production-ready backend** based on **ASP.NET Core 9**, **PostgreSQL**, and **Cloudflare R2**, structured using **Clean Architecture**, **CQRS**, and **Repository Pattern**. This backend will serve as a foundation for future SaaS or AI-integrated systems.

---

## 🎯 Product Vision
BartugWeb is designed as a **Clean Architecture showcase** — a blueprint for building maintainable, testable, and deployable backend systems. It demonstrates how to structure .NET projects for scalability while implementing real-world patterns like CQRS, MediatR, FluentValidation, AutoMapper, and layered separation.

The goal is to combine **enterprise backend quality** with **developer-friendly clarity**, ensuring the project can easily evolve into production-grade systems (e.g., content management, SaaS automation, AI-based agents).

---

## 🧠 Problem Statement
Traditional monolithic or tightly coupled backend projects quickly become hard to maintain. BartugWeb aims to solve this by introducing a **clean separation of concerns**, ensuring each layer has a single responsibility and all dependencies flow in one direction — from outer layers to the core.

---

## 🧩 Folder & Layer Structure

**Solution Overview:**
```
BartugWeb.sln
└── src/
    ├── Core/
    │   ├── BartugWeb.ApplicationLayer
    │   └── BartugWeb.DomainLayer
    ├── External/
    │   ├── BartugWeb.InfrastructureLayer
    │   └── BartugWeb.PersistanceLayer
    └── BartugWeb.WebApi
```

### **Core Layer**
- **BartugWeb.DomainLayer**  → Contains Entities, Enums, and Domain Events.
- **BartugWeb.ApplicationLayer**  → Contains CQRS (Commands, Queries, Handlers), DTOs, Validation, and Business Logic.

### **External Layer**
- **BartugWeb.PersistanceLayer**  → Implements EF Core repositories, configurations, and PostgreSQL context.
- **BartugWeb.InfrastructureLayer**  → Handles external integrations (Cloudflare R2, email, file storage, logging).

### **Presentation Layer**
- **BartugWeb.WebApi** → Entry point for the API. Hosts controllers, middleware, and DI configuration. Includes Swagger, Serilog, CORS policy, and health checks.

---

## 📚 Functional Requirements

| Feature | Description |
|----------|-------------|
| **CRUD Operations** | Implement CRUD for `Post` entity (Title, Content, ImageUrl). |
| **CQRS with MediatR** | Separate read/write operations using Commands and Queries. |
| **Validation** | Validate DTOs using FluentValidation before execution. |
| **Mapping** | AutoMapper for DTO ↔ Entity transformation. |
| **File Uploads** | FileService to upload files to Cloudflare R2 and return CDN URLs. |
| **Configuration Management** | Environment-based setup (Development/Production). |
| **Logging** | Centralized structured logging with Serilog. |
| **Health Check** | `/healthz` endpoint for monitoring. |

---

## 🧰 Tech Stack
- **ASP.NET Core 8** (Web API)
- **Entity Framework Core 8** (PostgreSQL)
- **CQRS + MediatR**
- **FluentValidation**
- **AutoMapper**
- **Serilog** (Console + File sinks)
- **Cloudflare R2 (S3 SDK)** for file storage
- **Docker Compose** (API + PostgreSQL + Caddy)
- **Caddy** for reverse proxy and SSL

---

## ⚙️ Environment Variables

| Key | Description |
|-----|--------------|
| `ASPNETCORE_ENVIRONMENT` | `Development` or `Production` |
| `ConnectionStrings__Default` | PostgreSQL connection string |
| `Storage__Endpoint` | Cloudflare R2 endpoint |
| `Storage__BucketName` | Cloudflare R2 bucket name |
| `Storage__AccessKeyId` | R2 Access key |
| `Storage__SecretAccessKey` | R2 Secret key |
| `Storage__PublicBaseUrl` | Public CDN URL for uploaded files |

---

## 🧭 Milestones & Timeline

| Sprint | Deliverable |
|---------|--------------|
| **Week 1–2** | Project setup, layer structure, domain entities, DI configuration |
| **Week 3–4** | Implement CQRS (MediatR), validation, mapping, and CRUD for Posts |
| **Week 5–6** | Cloudflare R2 integration and file upload service |
| **Week 7** | Docker Compose setup (PostgreSQL + Caddy + API) |
| **Week 8** | Deploy to Hostinger VPS with SSL |
| **Week 9+** | CI/CD (GitHub Actions + GHCR + Watchtower) |

---

## ✅ Acceptance Criteria
- Solution builds successfully with `dotnet build`.
- Runs via `docker compose up -d` with PostgreSQL and API accessible.
- Swagger UI accessible at `/swagger`.
- CRUD endpoints functional and validated.
- Files uploaded successfully to Cloudflare R2.
- Health check endpoint returns 200 OK.
- Logs appear structured in console and file.

---

## 📈 Future Enhancements
- JWT Authentication and Role-based Authorization.
- Global Exception Middleware with custom ProblemDetails.
- Hangfire for background jobs.
- Redis caching for read-heavy queries.
- API versioning.
- R2 pre-signed uploads and file deletion.
- Unit and integration tests (xUnit).

---

## 📦 Deployment
- **VPS:** Hostinger KVM2 or DigitalOcean Droplet
- **Orchestration:** Docker Compose (API + PostgreSQL + Caddy)
- **CI/CD:** GitHub Actions + GHCR + Watchtower
- **Domain & SSL:** Caddy auto SSL via Let’s Encrypt

---

## 👨‍💻 Author
**Bartuğ Kaan Çelebi**  
[GitHub](https://github.com/BartugKaan) • [LinkedIn](https://www.linkedin.com/in/bartugkaan/)  
> Building intelligent backend systems that combine clean architecture with modern automation.