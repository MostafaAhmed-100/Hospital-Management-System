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
- [Contributors](#-contributors)

## 🚀 Features & Modules (V1)

The current version (V1) targets the foundational modules required for daily hospital workflows.

### ✅ Done so far
- **Data layer** — Full EF Core entity models for every V1 module, organized by feature folder:
  - **Clinics & Doctors** — `Clinic`, `Department`, `Doctor`, `Specialty`
  - **Outpatient Visits** — `Patient`, `Appointment`, `MedicalRecord`
  - **Pharmacy** — `Medicine`, `Pharmacy`, `PharmacyInventory`, `Prescription`, `PrescriptionItem`, `PharmacySale`, `SaleItem`
  - **Billing & Insurance** — `InsuranceProvider`, `Invoice`, `Payment`
  - **Enums** — `AppointmentStatus`, `ClinicType`, `DoctorType`, `InvoiceStatus`, `PrescriptionStatus`
  - **Identity** — `ApplicationUser`
- **Fluent API configuration** — Full `IEntityTypeConfiguration<T>` set for every entity above, wired into `AppDbContext`.
- **Repository & Unit of Work** — `GenericRepository` + `SpecificRepositories`, wired behind `IUnitOfWork` for transactional `SaveChanges`.

### 🚧 Still needed to close out V1
- **Controllers/endpoints** — REST endpoints for all four modules (Clinics & Doctors, Outpatient Visits, Pharmacy, Billing & Insurance).
- **Service layer** — Business logic and validation sitting between controllers and the Unit of Work.
- **DTOs & AutoMapper/mapping** — Request/response shaping so entities aren't exposed directly through the API.
- **Auth** — ASP.NET Core Identity wired to `ApplicationUser`, linked to doctors and patients, with role-based authorization.
- **API docs** — Swagger/OpenAPI (currently disabled due to the .NET 10.0.10 bug).

## 🏗️ Architecture & Design Patterns

The project emphasizes maintainability, scalability, and testability through enterprise-level design patterns:

- **Repository Pattern** — A `GenericRepository` handles standard CRUD operations to keep the codebase DRY, complemented by `SpecificRepositories` for complex, domain-specific queries (e.g. `Include`, `AsSplitQuery`).
- **Unit of Work Pattern** — Centralized transaction management ensuring data integrity across multiple repository operations through a single `SaveChanges` context.
- **Dependency Injection (DI)** — Decoupled architecture, injecting interfaces (e.g. `IUnitOfWork`) rather than concrete implementations.
- **Performance Optimization** — Strategic use of `AsNoTracking`, `AsNoTrackingWithIdentityResolution`, and explicit query splitting (`AsSplitQuery`) to prevent Cartesian explosions and reduce memory overhead.

## 🛠️ Technology Stack

| Layer | Technology |
|---|---|
| Framework | .NET 8.0 (ASP.NET Core Web API) |
| Language | C# |
| ORM | Entity Framework Core |
| Database | Microsoft SQL Server |
| Auth | ASP.NET Core Identity |
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
   *(This automatically creates the `HospitalDB_V1` database and applies all schema tables.)*

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

Special thanks to the following mentor for their guidance throughout this project:

| Name | LinkedIn |
|---|---|
| **AbdALlatif Hossni** | [linkedin.com/in/abdallatif-hossni](https://linkedin.com/in/abdallatif-hossni) |

## 📄 License

This project is licensed under the MIT License.

---

Developed with ❤️ by **Mostafa Ahmed Soudi** © 2026