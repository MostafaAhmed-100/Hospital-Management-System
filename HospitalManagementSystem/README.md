# 🏥 Hospital Management System (HMS) - API

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![Entity Framework Core](https://img.shields.io/badge/EF_Core-388E3C?style=for-the-badge&logo=nuget&logoColor=white)
![License](https://img.shields.io/badge/License-MIT-yellow?style=for-the-badge)

A robust, highly scalable backend API for managing comprehensive hospital operations. Built with **ASP.NET Core Web API** and clean architecture principles, the system handles everything from outpatient visits to pharmacy inventory and billing.

## 📋 Table of Contents

- [Features & Modules (V1)](#-features--modules-v1)
- [Architecture & Design Patterns](#️-architecture--design-patterns)
- [Technology Stack](#️-technology-stack)
- [Getting Started](#️-getting-started)
- [Roadmap — Planned Updates through V3](#️-roadmap--planned-updates-through-v3)
- [Contributors](#-contributors)
- [Mentorship](#-mentorship)

## 🚀 Features & Modules (V1)

The first version (V1) is complete: every foundational module needed for daily hospital workflows is live, behind a fully decoupled architecture and secure RESTful endpoints.

### ✅ Completed in V1

- **Data Layer** — Full EF Core entity models for every module, organized by feature folder:
  - **Clinics & Doctors** — `Clinic`, `Department`, `Doctor`, `Specialty`
  - **Outpatient Visits** — `Patient`, `Appointment`, `MedicalRecord`
  - **Pharmacy** — `Medicine`, `Pharmacy`, `PharmacyInventory`, `Prescription`, `PrescriptionItem`, `PharmacySale`, `SaleItem`
  - **Billing & Insurance** — `InsuranceProvider`, `Invoice`, `Payment`
  - **Enums & Identity** — Standardized enums (`AppointmentStatus`, `ClinicType`, `DoctorType`, `InvoiceStatus`, `PrescriptionStatus`) and ASP.NET Core Identity wired to `ApplicationUser`.
- **Fluent API & Soft Deletion** — Full `IEntityTypeConfiguration<T>` set for every entity, equipped with global query filters for `IsDeleted` to ensure non-destructive record management.
- **Repository & Unit of Work** — `GenericRepository` + `SpecificRepositories`, wired behind `IUnitOfWork` for transactional `SaveChanges`.
- **Service Layer & DTOs** — Business logic encapsulation using AutoMapper for entity-DTO transformations and a unified `ApiResponseDto` wrapper for all responses with pagination support.
- **Robust Validation** — Global validation filters powered by FluentValidation (featuring localized Arabic error messages).
- **Controllers & Rate Limiting** — Fully implemented CRUD REST endpoints protected by intelligent rate limiting policies (Standard for queries, Strict for mutations).
- **Error Handling & Logging** — Centralized `ExceptionMiddleware` and structured, queryable logging powered by Serilog.
- **API Documentation** — Swagger/OpenAPI UI fully configured and enabled for seamless testing.

## 🏗️ Architecture & Design Patterns

The project emphasizes maintainability, scalability, and testability through enterprise-level design patterns, applied consistently across every layer:

- **Clean Architecture Separation** — Strict boundaries between Controllers → Services → Repositories, each with a single responsibility and independently testable.
- **Repository & Unit of Work Pattern** — `GenericRepository` handles standard CRUD to keep the codebase DRY, complemented by `SpecificRepositories` for complex domain queries (e.g. `Include`, `AsSplitQuery`). All repositories are wired behind `IUnitOfWork` so a single `SaveChanges` call commits multi-repository operations atomically. Newly added repository interfaces/implementations cover `PharmacySale`, `PrescriptionItem`, and `SaleItem`, registered directly in `UnitOfWork`.
- **Dependency Injection (DI)** — Fully decoupled architecture; controllers and services depend on interfaces (`IUnitOfWork`, service interfaces, repository interfaces) rather than concrete implementations.
- **Service Layer & DTO Mapping** — Business logic lives in dedicated services, not controllers. AutoMapper profiles handle entity ↔ DTO transformation, including paged results and nested detail DTOs, all returned through a unified `ApiResponseDto` wrapper.
- **Validation Pipeline** — A global `ValidationFilter` runs FluentValidation validators before a request reaches the service layer, short-circuiting invalid requests with structured, localized (Arabic) error messages instead of scattering `if` checks across controllers.
- **Soft Deletion** — Every entity carries an `IsDeleted` flag enforced through EF Core global query filters, so deleted records are automatically excluded from queries without needing to filter manually at each call site. A dedicated EF Core migration introduces the `IsDeleted` columns and updates the model snapshot accordingly.
- **Global Exception Handling** — A centralized `ExceptionMiddleware` catches unhandled exceptions anywhere in the pipeline and converts them into consistent, structured error responses instead of leaking stack traces.
- **Structured Logging** — Serilog is wired into the service layer and middleware, producing structured, queryable log events (rather than plain text) for easier debugging and monitoring in production.
- **Performance Optimization** — Strategic use of `AsNoTracking`, `AsNoTrackingWithIdentityResolution`, and explicit query splitting (`AsSplitQuery`) to prevent Cartesian explosions and reduce memory overhead on read-heavy endpoints.
- **Rate Limiting** — Endpoint-level policies distinguish read traffic (Standard policy) from write traffic (Strict policy) to protect the API from abuse without throttling normal browsing.

## 🛠️ Technology Stack

| Layer | Technology |
|---|---|
| Framework | .NET 8.0 (ASP.NET Core Web API) |
| Language | C# |
| ORM | Entity Framework Core |
| Database | Microsoft SQL Server |
| Mapping & Validation | AutoMapper, FluentValidation |
| Auth | ASP.NET Core Identity |
| Logging | Serilog |
| API Docs | Swagger / OpenAPI |

## ⚙️ Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server Management Studio (SSMS) or any compatible SQL Server instance
- Visual Studio 2022 (recommended)

### Installation & Setup

1. **Clone the repository:**
   ```bash
   git clone https://github.com/YourUsername/HospitalManagementSystem.git
   cd HospitalManagementSystem
   ```

2. **Configure the database connection:**

   Open `appsettings.json` and update the `DefaultConnection` string to match your local SQL Server instance:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=.;Database=HospitalDB_V1;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
   }
   ```

3. **Apply database migrations:**

   Open the Package Manager Console (PMC) in Visual Studio and run:
   ```powershell
   Update-Database
   ```
   *(This automatically creates the `HospitalDB_V1` database and applies all schema tables, including the Soft Delete `IsDeleted` columns.)*

4. **Run the application:**

   Press `F5` in Visual Studio or run `dotnet run` in the CLI. Swagger UI launches automatically in development mode for easy API testing.

## 🗺️ Roadmap — Planned Updates through V3

This is V1. The plan is to extend the system in two more versions, each a complete, demoable milestone on its own.

### 🔜 V2 — Hospital Operations
*Inpatient admission, emergency, surgery, and nursing.*

- **🛏️ Inpatient Admission** — `Rooms`, `Beds`, `Admissions`. A bed holds at most one active admission at a time; cost accrues daily and settles at discharge.
- **🚑 Emergency (ER)** — `ER_Visits` with a 5-level triage queue (priority first, arrival time second); can escalate directly into a full admission.
- **🔪 Surgery** — `OperatingRooms`, `Surgeries`, `SurgeryTeam`. No overlapping bookings per operating room; many-to-many surgical team with a role per participant.
- **👩‍⚕️ Nursing** — `Nurses`, `NurseAssignments`. Nurses modeled as a specialization of staff, with assignments tracked against admissions and ER visits per shift.

### 🔮 V3 — Care Extensions & Platform Hardening
*Physiotherapy, lab tests, reporting, and access-control hardening.*

- **🧘 Physiotherapy** — `Therapists`, `PhysioSessions`. Sessions are only ever created against a medical record that prescribes them.
- **🧪 Lab Tests** — `LabTests`, ordered against a medical record, closing the loop from visit → diagnosis → test result.
- **📊 Reporting & Dashboards** — Occupancy dashboards (beds/rooms/ORs) and revenue reports built as cross-cutting views over existing tables.
- **🔐 Access Control Hardening** — Finalized role-based access: patients see only their own data, doctors see only their own patients, admins see everything within their clinic.
- **⚡ Performance Tuning** — Query and indexing pass across the full schema once all modules are live.

## 👥 Contributors

| Name | Role | LinkedIn |
|---|---|---|
| **Mostafa Ahmed Soudi** | Backend Developer | [linkedin.com/in/mostafa-ahmed-745497326](https://www.linkedin.com/in/mostafa-ahmed-745497326/) |

*Computer Software Engineering, Egyptian Chinese University (ECU)*

## 🙏 Mentorship

Special thanks to the following mentors for their valuable guidance and technical mentorship throughout this project:

| Name | LinkedIn |
|---|---|
| **AbdALlatif Hossni** | [linkedin.com/in/abdallatif-hossni](https://linkedin.com/in/abdallatif-hossni) |
| **Omar Ahmed** | ADD_LINKEDIN_URL_HERE |

## 📄 License

This project is licensed under the MIT License.

---

Developed with ❤️ by **Mostafa Ahmed Soudi** © 2026