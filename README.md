# 🏥 Hospital Management System (HMS) - API

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![Entity Framework Core](https://img.shields.io/badge/EF_Core-388E3C?style=for-the-badge&logo=nuget&logoColor=white)
![License](https://img.shields.io/badge/License-MIT-yellow?style=for-the-badge)

A robust, highly scalable backend API for managing comprehensive hospital operations. Built with **ASP.NET Core Web API** and clean architecture principles, the system handles everything from outpatient visits to pharmacy inventory and billing.

## 📋 Table of Contents

- [Features & Modules (V1)](#-features--modules-v1)
- [V2 — Hospital Operations](#-v2--hospital-operations)
- [Architecture & Design Patterns](#️-architecture--design-patterns)
- [Technology Stack](#️-technology-stack)
- [Getting Started](#️-getting-started)
- [Roadmap — Planned Updates through V3](#️-roadmap--planned-updates-through-v3)
- [Contributors](#-contributors)
- [Mentorship](#-mentorship)

## 🚀 Features & Modules (V1)

> **✅ V1 Status: Complete.** All four core modules (Clinics & Doctors, Outpatient Visits, Pharmacy, Billing & Insurance) are fully implemented end to end — from the data layer through Controllers — and production-ready with logging, validation, and error handling in place.

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

## 🏥 V2 — Hospital Operations

> **✅ V2 Status: Complete.** Inpatient admission, emergency, surgery, and nursing are fully implemented end to end — from domain models through Controllers — with the same production-grade layers as V1 (validation, logging, error handling, soft deletion).

### ✅ Completed in V2

- **Domain Models** — `Admission`, `Bed`, `Room`, `ERVisit`, `Staff`, `Surgery`, `Nurse`, `NurseAssignment`, plus supporting enums for statuses/roles.
- **Fluent API & Soft Deletion** — `IEntityTypeConfiguration<T>` for every V2 entity, with the same global `IsDeleted` query filters as V1.
- **Repository & Unit of Work** — Repository interfaces/implementations for advanced queries, organized under dedicated sub-namespaces (`InpatientRepositorys`, `OutpatientVisitsRepository`, `PharmacysRepository`, `Nursing_StaffRepositorys`, `SurgeryRepository`), all wired behind `UnitOfWork`.
- **Nursing Module** — `Nurse` modeled as a specialization of staff via a link to the `Staff` table; `NurseAssignment` tracks nurse coverage per shift, tied to either an `Admission` or an `ERVisit`.
- **Service Layer & DTOs** — Full DTOs, AutoMapper profiles, and services for Inpatient, Nursing Staff, Surgery, and Emergency modules, following the same `ApiResponseDto`/paged-result pattern as V1.
- **Validation** — FluentValidation validators for all new DTOs (e.g. bed/OR conflict rules, triage constraints, shift assignment rules).
- **Controllers** — CRUD + workflow endpoints (admit patient, discharge, log ER visit, escalate to admission, schedule surgery, assign surgical team, assign nurse to shift), protected by the same rate limiting policies as V1.
- **Namespace Reorganization** — DTOs, services, controllers, mappings, and validators regrouped into domain-specific namespaces (e.g. `OutpatientVisits`, `Pharmacys`) for clearer separation of concerns; DI in `Program.cs` updated to match.
- **Doctor Model Update** — Added `FullName` to the `Doctor` entity/DTOs (with migration) and fixed related validation in `DoctorService`.
- **API Documentation** — Swagger/OpenAPI enabled and covering all V2 endpoints.

## 🏗️ Architecture & Design Patterns

The project emphasizes maintainability, scalability, and testability through enterprise-level design patterns, applied consistently across every layer:

- **Clean Architecture Separation** — Strict boundaries between Controllers → Services → Repositories, each with a single responsibility and independently testable.
- **Repository & Unit of Work Pattern** — `GenericRepository` handles standard CRUD to keep the codebase DRY, complemented by `SpecificRepositories` for complex domain queries (e.g. `Include`, `AsSplitQuery`). All repositories are wired behind `IUnitOfWork` so a single `SaveChanges` call commits multi-repository operations atomically. Repository interfaces/implementations now also cover the V2 entities (`Admission`, `Bed`, `Room`, `ERVisit`, `Staff`, `Surgery`), registered directly in `UnitOfWork`.
- **Dependency Injection (DI)** — Fully decoupled architecture; controllers and services depend on interfaces (`IUnitOfWork`, service interfaces, repository interfaces) rather than concrete implementations.
- **Service Layer & DTO Mapping** — Business logic lives in dedicated services, not controllers. AutoMapper profiles handle entity ↔ DTO transformation, including paged results and nested detail DTOs, all returned through a unified `ApiResponseDto` wrapper.
- **Validation Pipeline** — A global `ValidationFilter` runs FluentValidation validators before a request reaches the service layer, short-circuiting invalid requests with structured, localized (Arabic) error messages instead of scattering `if` checks across controllers.
- **Soft Deletion** — Every entity carries an `IsDeleted` flag enforced through EF Core global query filters, so deleted records are automatically excluded from queries without needing to filter manually at each call site.
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

V1 and V2 are both complete. V3 is the last version left before the full ERD scope is delivered.

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

## 📄 License

This project is licensed under the MIT License.

---

Developed with ❤️ by **Mostafa Ahmed Soudi** © 2026