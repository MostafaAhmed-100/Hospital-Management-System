# 🏥 Hospital Management System (HMS) - API

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![Entity Framework Core](https://img.shields.io/badge/EF_Core-388E3C?style=for-the-badge&logo=nuget&logoColor=white)
![License](https://img.shields.io/badge/License-MIT-yellow?style=for-the-badge)

A robust, highly scalable backend API for managing comprehensive hospital operations. Built with **ASP.NET Core Web API** and clean architecture principles, the system handles everything from outpatient visits to inpatient care, surgery, physiotherapy, pharmacy inventory, billing, and reporting.

> **🎉 Project Status: Complete (V1 + V2 + V3).** Every module in the full ERD is implemented end to end — data layer, repositories, services, DTOs, validation, controllers, auth, and reporting.

## 📋 Table of Contents

- [Features & Modules (V1)](#-features--modules-v1)
- [V2 — Hospital Operations](#-v2--hospital-operations)
- [V3 — Care Extensions & Platform Hardening](#-v3--care-extensions--platform-hardening)
- [Architecture & Design Patterns](#️-architecture--design-patterns)
- [Technology Stack](#️-technology-stack)
- [Getting Started](#️-getting-started)
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

## 🔮 V3 — Care Extensions & Platform Hardening

> **✅ V3 Status: Complete.** Physiotherapy, lab tests, reporting, JWT authentication, and access-control hardening are all fully implemented — closing out the full ERD scope across V1, V2, and V3.

### ✅ Completed in V3

- **Domain Models** — `Therapist`, `PhysioSession`, `LabTest`, plus the `LabTestStatus` enum; `ApplicationUser` extended to link with `Staff` and `Therapist`.
- **Fluent API & Migrations** — `IEntityTypeConfiguration<T>` for all new entities, new `DbSet`s on `AppDbContext`, and migrations covering the new tables, relationships, and `AspNetUsers` columns.
- **Repository & Unit of Work** — Repositories and interfaces for `Therapist`, `PhysioSession`, and `LabTest`, registered in `IUnitOfWork`/`UnitOfWork`.
- **Service Layer & DTOs** — Full CRUD and query services, DTOs, and AutoMapper profiles for Lab Tests, Physiotherapy Sessions, and Therapists.
- **Reporting & Dashboards** — Dedicated dashboard reporting controllers, services, and DTOs for occupancy, revenue, and staff-utilization queries.
- **Advanced Statistics Layer** — DTOs, services, repositories, and controllers for cross-domain statistics (clinics, doctors, ER, inpatient, lab, nursing, outpatient, pharmacy, physiotherapy, surgery, and more), exposing endpoints for top entities, distributions, and counts — all standardized through `ApiResponseDto`.
- **Validation** — FluentValidation for all new DTOs, with the same localized Arabic error messages used across V1/V2; `InvoiceStatus` enum extended to match new billing states.
- **Authentication & API Docs** — JWT authentication configured end to end, with Swagger security and UI updated (custom title/version) to reflect the finished API surface.
- **Access Control Hardening** — Authorization policies (`PatientOwnsRecord`, `DoctorOwnsAppointment`, `AdminWithinClinic`) defined and applied across every V1/V2/V3 endpoint, with an audit pass confirming no cross-patient or cross-clinic data leakage.
- **Performance Tuning** — Query and indexing pass completed across the full, final schema (V1 + V2 + V3), including `AsNoTracking`/`AsSplitQuery` audits on the larger join-heavy queries introduced by reporting and inpatient modules.
- **DTO & Route Cleanup** — Response DTOs trimmed of redundant fields (`DepartmentName`, `SpecialtyName`, `ClinicName`), `DoctorController` routes clarified, `Department.Description` made required, and `ClinicType` added to Clinic DTOs with validation.

## 🏗️ Architecture & Design Patterns

The project emphasizes maintainability, scalability, and testability through enterprise-level design patterns, applied consistently across every layer:

- **Clean Architecture Separation** — Strict boundaries between Controllers → Services → Repositories, each with a single responsibility and independently testable.
- **Repository & Unit of Work Pattern** — `GenericRepository` handles standard CRUD to keep the codebase DRY, complemented by `SpecificRepositories` for complex domain queries (e.g. `Include`, `AsSplitQuery`). All repositories are wired behind `IUnitOfWork` so a single `SaveChanges` call commits multi-repository operations atomically, across every module from V1 through V3.
- **Dependency Injection (DI)** — Fully decoupled architecture; controllers and services depend on interfaces (`IUnitOfWork`, service interfaces, repository interfaces) rather than concrete implementations.
- **Service Layer & DTO Mapping** — Business logic lives in dedicated services, not controllers. AutoMapper profiles handle entity ↔ DTO transformation, including paged results and nested detail DTOs, all returned through a unified `ApiResponseDto` wrapper.
- **Validation Pipeline** — A global `ValidationFilter` runs FluentValidation validators before a request reaches the service layer, short-circuiting invalid requests with structured, localized (Arabic) error messages instead of scattering `if` checks across controllers.
- **Soft Deletion** — Every entity carries an `IsDeleted` flag enforced through EF Core global query filters, so deleted records are automatically excluded from queries without needing to filter manually at each call site.
- **Global Exception Handling** — A centralized `ExceptionMiddleware` catches unhandled exceptions anywhere in the pipeline and converts them into consistent, structured error responses instead of leaking stack traces.
- **Structured Logging** — Serilog is wired into the service layer and middleware, producing structured, queryable log events (rather than plain text) for easier debugging and monitoring in production.
- **Authentication & Authorization** — JWT-based authentication paired with policy-based authorization (`PatientOwnsRecord`, `DoctorOwnsAppointment`, `AdminWithinClinic`) enforced consistently across the entire API surface.
- **Performance Optimization** — Strategic use of `AsNoTracking`, `AsNoTrackingWithIdentityResolution`, and explicit query splitting (`AsSplitQuery`) to prevent Cartesian explosions and reduce memory overhead on read-heavy endpoints, including the new reporting/dashboard queries.
- **Rate Limiting** — Endpoint-level policies distinguish read traffic (Standard policy) from write traffic (Strict policy) to protect the API from abuse without throttling normal browsing.

## 🛠️ Technology Stack

| Layer | Technology |
|---|---|
| Framework | .NET 8.0 (ASP.NET Core Web API) |
| Language | C# |
| ORM | Entity Framework Core |
| Database | Microsoft SQL Server |
| Mapping & Validation | AutoMapper, FluentValidation |
| Auth | ASP.NET Core Identity, JWT |
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
   *(This automatically creates the database and applies all schema tables across V1, V2, and V3, including Soft Delete `IsDeleted` columns and the new `AspNetUsers` columns.)*

4. **Run the application:**

   Press `F5` in Visual Studio or run `dotnet run` in the CLI. Swagger UI launches automatically in development mode for easy API testing, with JWT auth configured in the Swagger security scheme.

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