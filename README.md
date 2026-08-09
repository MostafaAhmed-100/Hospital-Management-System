# 🏥 Hospital Management System (HMS) - Backend API

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![Entity Framework Core](https://img.shields.io/badge/EF_Core-388E3C?style=for-the-badge&logo=nuget&logoColor=white)
![License](https://img.shields.io/badge/License-MIT-yellow?style=for-the-badge)

A comprehensive, production-grade hospital management backend spanning **31 database tables** across three iterative releases (V1→V2→V3). Built with **ASP.NET Core 8 Web API**, the system manages the complete healthcare operation lifecycle: from patient registration and outpatient clinic appointments, through emergency room triage and inpatient admission, surgical scheduling and execution, nursing shift assignment, physiotherapy sessions, laboratory test management, to pharmacy inventory and billing/insurance processing.

The architecture emphasizes **enterprise-grade patterns** (Clean Architecture, Repository + Unit of Work, Dependency Injection), **data integrity** (soft deletion, transactional consistency across modules), **security** (JWT auth with policy-based authorization, role isolation), and **observability** (structured logging, centralized error handling, health monitoring).

> **🎉 Project Status: Complete (V1 + V2 + V3).** All 31 domain entities implemented end-to-end: data models, Fluent API configurations, repositories, services, DTOs, FluentValidation rules, authorization policies, and controllers. Zero shortcuts — production-ready with comprehensive logging, exception handling, and performance tuning.

---

## 📋 Table of Contents

- [Domain Model Overview](#-domain-model-overview)
- [V1 — Clinic & Outpatient Operations](#-v1--clinic--outpatient-operations)
- [V2 — Inpatient, Emergency & Surgery](#-v2--inpatient-emergency--surgery)
- [V3 — Care Extensions & Platform Hardening](#-v3--care-extensions--platform-hardening)
- [Architecture Deep Dive](#️-architecture-deep-dive)
- [Technology Stack](#️-technology-stack)
- [Database Schema Highlights](#-database-schema-highlights)
- [Authentication & Authorization](#-authentication--authorization)
- [API Workflows (Real-World Scenarios)](#-api-workflows-real-world-scenarios)
- [Getting Started](#️-getting-started)
- [Contributors & Mentorship](#-contributors--mentorship)

---

## 🗂️ Domain Model Overview

The system is organized around **7 core business domains**, each with dedicated repositories, services, and controllers:

```
Hospital Management System (31 Tables)
├── Clinic & Doctor Management (4 tables)
│   ├── Clinic (id, name, type, location)
│   ├── Department (id, name, description)
│   ├── Doctor (id, fullName, specialtyId, departmentId)
│   └── Specialty (id, name)
│
├── Outpatient Operations (3 tables)
│   ├── Patient (id, firstName, lastName, email, phone, dateOfBirth)
│   ├── Appointment (id, patientId, doctorId, clinicId, appointmentDate, status)
│   └── MedicalRecord (id, patientId, diagnosis, treatment)
│
├── Emergency & Inpatient (5 tables)
│   ├── ERVisit (id, patientId, triageLevel, arrivalTime, status)
│   ├── Admission (id, patientId, bedId, admissionDate, dischargeDate)
│   ├── Bed (id, roomId, bedNumber, status)
│   ├── Room (id, roomNumber, capacity, type)
│   └── NurseAssignment (id, nurseId, admissionId/erVisitId, shiftDate, hours)
│
├── Surgery & Surgical Team (2 tables)
│   ├── Surgery (id, patientId, admissionId, surgeryType, scheduledDate, status)
│   └── SurgicalTeam (surgeryId, doctorId, role) [junction table]
│
├── Nursing Staff (2 tables)
│   ├── Nurse (id, staffId) [specialization of Staff]
│   └── Staff (id, firstName, lastName, role, department)
│
├── Pharmacy & Inventory (5 tables)
│   ├── Medicine (id, name, dosage, manufacturer)
│   ├── Pharmacy (id, name, location)
│   ├── PharmacyInventory (id, pharmacyId, medicineId, quantityInStock)
│   ├── Prescription (id, patientId, doctorId, issueDate, status)
│   ├── PrescriptionItem (id, prescriptionId, medicineId, dosage, duration)
│   ├── PharmacySale (id, pharmacyId, prescriptionId, saleDate)
│   └── SaleItem (id, saleId, medicineId, quantitySold)
│
├── Physiotherapy (2 tables)
│   ├── Therapist (id, staffId) [specialization of Staff]
│   └── PhysioSession (id, patientId, therapistId, sessionDate, duration, status)
│
├── Lab Services (1 table)
│   └── LabTest (id, patientId, testType, resultDate, result, status)
│
└── Billing & Insurance (3 tables)
    ├── InsuranceProvider (id, name, contactInfo)
    ├── Invoice (id, patientId, insuranceId, totalAmount, status)
    └── Payment (id, invoiceId, paymentDate, amount, method)
```

---

## 🚀 V1 — Clinic & Outpatient Operations

> **Status:** ✅ Complete. Foundation layer covering healthcare facility setup and patient outpatient workflows.

### Core Entities (7 tables)

| Entity | Purpose | Key Fields |
|---|---|---|
| **Clinic** | Healthcare facility branch | name, type (Private/Government/Hybrid), location, capacity |
| **Department** | Medical specialization unit | name, description, parentDepartmentId (supports hierarchy) |
| **Doctor** | Medical practitioners | fullName, specialtyId (cardiology, surgery, etc.), departmentId, licenseNumber |
| **Specialty** | Medical specializations | name (Cardiology, Orthopedics, Neurology, etc.) |
| **Patient** | System user records | firstName, lastName, email, phone, dateOfBirth, nationalId, bloodType |
| **Appointment** | Clinic visit booking | patientId, doctorId, clinicId, appointmentDate, status (Scheduled/Completed/Cancelled) |
| **MedicalRecord** | Clinical encounter documentation | patientId, doctorId, appointmentId, diagnosis, treatment, notes |

### V1 Features Implemented

- **Clinic Hierarchy** — Support parent/sub-clinics for multi-branch hospitals
- **Doctor-Specialty Mapping** — Doctors linked to multiple specialties with experience levels
- **Appointment Workflow** — Book → Confirm → Check-in → Complete workflow with conflict detection
- **Medical Record Audit** — Every diagnosis/treatment tied to specific doctor and appointment
- **Soft Deletion Enforcement** — All deletions are logical (IsDeleted flag); no permanent data loss
- **Pagination Support** — All list endpoints support `PageNumber`, `PageSize` for large datasets

### V1 API Endpoints (Sample)

```
GET    /api/Clinic/Get-All              # Paginated clinic list
POST   /api/Clinic/Create               # Create new clinic
GET    /api/Doctor/{doctorId}           # Get doctor profile + specialties
GET    /api/Appointment/My-Appointments # Get patient's appointment history
POST   /api/Appointment/Book            # Schedule appointment (conflict detection)
PUT    /api/MedicalRecord/{recordId}    # Update diagnosis/treatment notes
```

---

## 🏥 V2 — Inpatient, Emergency & Surgery

> **Status:** ✅ Complete. Adds hospital bed management, emergency triage, inpatient admission workflows, and surgical operations.

### New Entities (8 tables)

| Entity | Purpose | Key Fields |
|---|---|---|
| **ERVisit** | Emergency room encounter | patientId, triageLevel (Critical/Urgent/Moderate/Minor), arrivalTime, status (Waiting/InProgress/Admitted/Discharged) |
| **Admission** | Patient hospitalization record | patientId, bedId, roomId, admissionDate, expectedDischargeDate, actualDischargeDate, reasonForAdmission |
| **Bed** | Physical bed in hospital | roomId, bedNumber, type (Standard/ICU/Isolation), status (Available/Occupied/Maintenance) |
| **Room** | Hospital ward unit | roomNumber, ward (ICU/General/Pediatric), capacity, type |
| **Staff** | Hospital employees (base entity) | firstName, lastName, role (Doctor/Nurse/Technician/Admin), department, hireDate |
| **Nurse** | Nursing specialization | staffId (FK to Staff), licenseNumber, shift (Day/Night) |
| **Surgery** | Surgical procedure | patientId, admissionId, surgeryType (Emergency/Elective), scheduledDate, status (Scheduled/InProgress/Completed/Cancelled), operatingRoomId |
| **SurgicalTeam** | Surgery staff assignment | surgeryId, doctorId, role (Lead Surgeon/Anesthetist/Scrub Nurse) |
| **NurseAssignment** | Nursing coverage tracking | nurseId, admissionId/erVisitId, shiftDate, shiftType (Day/Night/Evening), hoursWorked |

### V2 Business Logic

**Admission Workflow:**
1. Patient arrives via ER or outpatient referral → `ERVisit` created with triage level
2. Triage assessment determines if admission needed
3. Bed availability check → `Admission` record created
4. Assign nursing staff via `NurseAssignment`
5. Daily vitals/treatment updates via medical records
6. Discharge creates `DischargeRecord` with final status

**Surgical Scheduling:**
1. Doctor orders surgery → `Surgery` entity created with type (Emergency/Elective)
2. Operating room availability check
3. Surgical team assembled → `SurgicalTeam` entries created (Lead/Anesthetist/Nurses)
4. Pre-operative checklist validation
5. Post-op recovery bed assignment
6. Surgical team performance tracking

**Bed Management:**
- Real-time bed availability tracking (Available/Occupied/Maintenance)
- Automatic status updates on admission/discharge
- Prevent overbooking (conflict detection)
- Support for specialized rooms (ICU/Isolation)

### V2 API Endpoints (Sample)

```
POST   /api/ERVisit/Log-Visit           # Triage patient, set level
POST   /api/Admission/Admit-Patient     # Admit from ER, assign bed
GET    /api/Bed/Available-Beds          # Check bed availability per room type
POST   /api/Surgery/Schedule            # Schedule surgery + assign team
PUT    /api/Admission/{admissionId}     # Update patient status (vitals, meds)
POST   /api/NurseAssignment/Assign      # Assign nurse to shift
DELETE /api/Admission/{admissionId}     # Discharge patient (soft delete)
```

---

## 🔮 V3 — Care Extensions & Platform Hardening

> **Status:** ✅ Complete. Adds physiotherapy, lab testing, comprehensive reporting, JWT authentication, and platform-wide hardening.

### New Entities (3 tables)

| Entity | Purpose | Key Fields |
|---|---|---|
| **Therapist** | Physiotherapy specialist | staffId (FK to Staff), licenseNumber, specialization (Sports/Orthopedic/Neurological) |
| **PhysioSession** | Physiotherapy treatment | patientId, therapistId, sessionDate, duration (minutes), type (Assessment/Treatment/Follow-up), notes |
| **LabTest** | Laboratory test order & results | patientId, doctorId, testType (Blood/Imaging/Pathology), orderDate, resultDate, result (value/image), status (Pending/Completed/Abnormal) |

### V3 Features Implemented

**Authentication & Authorization:**
- JWT Bearer tokens with role-based claims (Doctor/Nurse/Admin/Therapist/LabTechnician)
- Policy-based authorization enforced across all endpoints:
  - `PatientOwnsRecord` — Patient can only view own data
  - `DoctorOwnsAppointment` — Doctor can only modify own appointments/medical records
  - `AdminWithinClinic` — Admin restricted to assigned clinic only
  - `NurseInAssignedUnit` — Nurse can only access assigned ward/shift patients
- Email verification as prerequisites for sensitive operations
- Audit trail for all data modifications

**Reporting & Analytics:**
- **Occupancy Dashboard** — Real-time bed occupancy rates, average length of stay
- **Revenue Report** — Billing summary by clinic/doctor/procedure type
- **Staff Utilization** — Doctor hours, nurse shift distribution, OR utilization rate
- **Patient Statistics** — Admission trends, readmission rates, emergency vs elective breakdown
- **Cross-Domain Analytics** — Correlate ER triage levels with admission rates, surgery complications with lab abnormalities

**Lab Test Integration:**
- Order tracking (Pending → Processing → Completed)
- Result storage with reference ranges
- Abnormal flag detection with doctor notification
- Historical test comparisons

**Physiotherapy Module:**
- Session scheduling with therapist availability
- Treatment plan tracking
- Progress notes with outcome metrics
- Insurance billing support

**Performance & Resilience:**
- `AsNoTracking` on all read-heavy queries (staff dashboards, analytics)
- `AsSplitQuery` to avoid Cartesian explosions (patient with multiple admissions, surgeries, lab tests)
- Query optimization for reporting endpoints (aggregations at DB level, not in-memory)
- Connection pooling with SQL Server for high concurrency

**Validation & Error Handling:**
- FluentValidation with bilingual error messages (Arabic + English)
- Specialized exceptions: `NotFoundException`, `ConflictException` (bed conflict), `UnauthorizedException` (cross-clinic access)
- Request/response logging with correlation IDs for debugging
- Centralized middleware capturing all exceptions and converting to standardized responses

### V3 API Endpoints (Sample)

```
POST   /api/Auth/Login                  # JWT token issuance
GET    /api/LabTest/{testId}            # Retrieve lab result (with auth check)
POST   /api/PhysioSession/Schedule      # Book physiotherapy session
GET    /api/Reporting/Occupancy         # Occupancy dashboard data
GET    /api/Reporting/Revenue           # Revenue by clinic/doctor
GET    /api/Statistics/Admissions       # Admission rate trends
```

---

## 🏗️ Architecture Deep Dive

### Clean Architecture Layers

**1. Controllers Layer** — Thin HTTP handlers
- Parse requests, validate JWT, delegate to services
- Return responses via `ApiResponseDto` wrapper
- No business logic; act as anti-corruption layer

**2. Service Layer** — Pure business logic
- All domain rules (bed conflict detection, triage validation, surgical team composition)
- Dependency on repositories via `IUnitOfWork`
- Completely HTTP-agnostic (testable without mocking HTTP)
- Examples:
  - `AdmissionService.AdmitPatientAsync()` — Check bed availability, assign bed, create admission record, notify nursing
  - `SurgeryService.ScheduleSurgeryAsync()` — Validate OR availability, assemble team, create surgical kit order
  - `LaboratoryService.ProcessTestResultAsync()` — Parse result, detect abnormalities, trigger doctor notification

**3. Repository Layer** — Data access abstraction
- `GenericRepository<T>` — Standard CRUD (Create, Read, Update, Delete via soft deletion)
- `SpecificRepositories` — Complex queries:
  - `AdmissionRepository.GetActiveAdmissionsByClinicAsync()` — Join with beds, rooms, patients
  - `SurgeryRepository.GetSurgicalTeamAsync()` — Multi-join across surgeries, doctors, staff roles
  - `LabTestRepository.GetAbnormalResultsAsync()` — Filter by date range, abnormal flag, doctor assignment
- Query optimization: `.Include()`, `.AsNoTracking()`, `.AsSplitQuery()`

**4. Unit of Work Pattern** — Transaction management
- Single `IUnitOfWork` interface exposing all repositories
- All service methods operate through UOW; single `await unitOfWork.SaveChangesAsync()` at end
- Atomic multi-repository commits (admission + bed assignment + nurse assignment all succeed or all fail)

### Data Access Patterns

**Eager Loading (Prevent N+1):**
```csharp
// ❌ Bad: 100 queries for 100 patients
var patients = await _patientRepository.GetAllAsync();
foreach (var p in patients) {
    var admissions = await _admissionRepository.GetByPatientIdAsync(p.Id); // N+1
}

// ✅ Good: Single query
var patients = await _patientRepository.GetAllWithAdmissionsAsync(); // .Include(p => p.Admissions)
```

**AsNoTracking (Read-Heavy):**
```csharp
// Occupancy dashboard: 10,000 beds, 5,000 patients — no change tracking needed
var occupancy = await dbContext.Beds
    .AsNoTracking()
    .Include(b => b.Room)
    .Include(b => b.CurrentAdmission)
    .Select(b => new OccupancyDTO { BedNumber = b.BedNumber, Status = b.Status })
    .ToListAsync();
```

**Split Queries (Cartesian Prevention):**
```csharp
// Without split: 1 patient * 5 admissions * 3 surgeries per admission = 15 rows
// With split: Separate queries = 1 patient + 5 admissions + 15 surgeries = clean data

var patients = await dbContext.Patients
    .AsSplitQuery()
    .Include(p => p.Admissions)
        .ThenInclude(a => a.Surgeries)
    .Include(p => p.MedicalRecords)
    .ToListAsync();
```

### Validation Pipeline

**FluentValidation (Separated from DTOs):**
```csharp
public class AdmissionValidator : AbstractValidator<AdmissionCreateDTO> {
    public AdmissionValidator(IUnitOfWork unitOfWork) {
        RuleFor(x => x.PatientId)
            .MustAsync(async (id, ct) => await unitOfWork.PatientRepository.ExistsAsync(id))
            .WithMessage("Patient does not exist");
        
        RuleFor(x => x.BedId)
            .MustAsync(async (bedId, ct) => {
                var bed = await unitOfWork.BedRepository.GetByIdAsync(bedId);
                return bed?.Status == BedStatus.Available;
            })
            .WithMessage("Bed is not available");
    }
}

// Global ValidationFilter intercepts all requests before reaching controller
```

### Soft Deletion Strategy

**Global Query Filters (Automatic Exclusion):**
```csharp
// Configure in DbContext
modelBuilder.Entity<Patient>()
    .HasQueryFilter(p => !p.IsDeleted);

modelBuilder.Entity<Doctor>()
    .HasQueryFilter(d => !d.IsDeleted);

// Usage: No manual filtering needed
var activeDoctors = await dbContext.Doctors.ToListAsync(); // Automatically excludes IsDeleted=true
```

**Soft Delete on Discharge:**
```csharp
// Discharge admission = logical delete
var admission = await _admissionRepository.GetByIdAsync(admissionId);
admission.IsDeleted = true;
admission.DischargeDate = DateTime.UtcNow;
await _unitOfWork.SaveChangesAsync(); // Single commit
```

---

## 🛠️ Technology Stack

| Layer | Technology | Purpose |
|---|---|---|
| **Framework** | ASP.NET Core 8 Web API | HTTP server, routing, middleware |
| **Language** | C# 11 | Type-safe, async/await |
| **ORM** | Entity Framework Core 8 | LINQ-based data access, migrations, query optimization |
| **Database** | SQL Server 2022 | Enterprise RDBMS, transaction support, spatial queries for location |
| **Mapping** | AutoMapper | Entity ↔ DTO transformations, nested mapping |
| **Validation** | FluentValidation | Separated, reusable validation rules with bilingual messages |
| **Auth** | ASP.NET Core Identity + JWT | User management, role assignment, token-based auth |
| **Logging** | Serilog | Structured logging to file + console with request correlation |
| **API Docs** | Swagger/OpenAPI | Interactive endpoint documentation + JWT auth UI |
| **Testing** | XUnit/NUnit (optional) | Unit testing service layer with mocked repositories |

---

## 🗄️ Database Schema Highlights

### Key Constraints & Relationships

```sql
-- Appointment conflict prevention (unique clustered index)
CREATE UNIQUE NONCLUSTERED INDEX IX_Appointment_Doctor_Date
ON Appointments(DoctorId, AppointmentDate)
WHERE IsDeleted = 0;

-- Bed occupancy constraint (no duplicate active admissions per bed)
CREATE UNIQUE NONCLUSTERED INDEX IX_Admission_Bed_Active
ON Admissions(BedId)
WHERE IsDeleted = 0 AND DischargeDate IS NULL;

-- Surgical team composition (doctor can have one role per surgery)
CREATE UNIQUE NONCLUSTERED INDEX IX_SurgicalTeam_Doctor_Surgery
ON SurgicalTeams(SurgeryId, DoctorId)
WHERE IsDeleted = 0;

-- Nurse shift coverage (nurse assigned once per shift per ward)
CREATE UNIQUE NONCLUSTERED INDEX IX_NurseAssignment_Nurse_Shift
ON NurseAssignments(NurseId, ShiftDate, AssignedWard)
WHERE IsDeleted = 0;
```

### Hierarchical Data

- **Department Hierarchy** — Parent department (Orthopedics) with sub-departments (Sports Medicine, Joint Replacement)
- **Room Classification** — Room → Bed (1-to-many) with type constraints (ICU beds only in ICU rooms)
- **Staff Specialization** — Base Staff entity with Nurse/Therapist specializations via FK relationships

---

## 🔐 Authentication & Authorization

### JWT Token Structure

```json
{
  "sub": "doctor-id-uuid",
  "email": "dr.ahmed@hospital.com",
  "role": "Doctor",
  "clinic_id": "clinic-001",
  "department_id": "dept-cardiology",
  "exp": 1735689600,
  "iat": 1704067200
}
```

### Authorization Policies

| Policy | Enforcement | Example |
|---|---|---|
| **PatientOwnsRecord** | `PatientId == UserId` | Patient can only view own appointments, medical records |
| **DoctorOwnsAppointment** | `DoctorId == UserId` | Doctor can only modify own appointments' notes |
| **AdminWithinClinic** | `ClinicId == UserClinicId` | Admin A cannot modify clinic B's appointments |
| **NurseInAssignedUnit** | Ward assignment check | Nurse can only access patients in assigned ward |
| **SurgeonsOnly** | Role-based | Only doctors with Surgeon role can create surgeries |

### Implementation

```csharp
// In Program.cs
services.AddAuthorization(options => {
    options.AddPolicy("PatientOwnsRecord", policy =>
        policy.Requirements.Add(new PatientOwnershipRequirement()));
    
    options.AddPolicy("DoctorOwnsAppointment", policy =>
        policy.Requirements.Add(new DoctorOwnershipRequirement()));
});

// In Controller
[Authorize(Policy = "PatientOwnsRecord")]
[HttpGet("{patientId}/medical-records")]
public async Task<IActionResult> GetMedicalRecords(string patientId) {
    // User can only access own records
}
```

---

## 🔄 API Workflows (Real-World Scenarios)

### Scenario 1: Emergency Room Triage → Admission → Surgery

```
1. Patient arrives at ER
   POST /api/ERVisit/Log-Visit
   {
     "patientId": "P001",
     "triageLevel": "Urgent",
     "chiefComplaint": "Acute abdominal pain"
   }
   → ERVisit created, status = Waiting

2. Triage nurse assesses patient, determines admission needed
   PUT /api/ERVisit/Update-Status
   {
     "erVisitId": "ER001",
     "status": "Admitted"
   }

3. Bed assignment system checks availability
   GET /api/Bed/Available-Beds?roomType=General
   → Returns available beds

4. Admit patient to bed
   POST /api/Admission/Admit-Patient
   {
     "patientId": "P001",
     "bedId": "B042",
     "reasonForAdmission": "Acute appendicitis"
   }
   → Admission created, Bed status = Occupied, NurseAssignment created

5. Doctor orders emergency surgery
   POST /api/Surgery/Schedule
   {
     "admissionId": "ADM001",
     "surgeryType": "Emergency",
     "surgeryType": "Appendectomy",
     "scheduledDate": "2024-01-15T14:30:00"
   }
   → Surgery created, OR reserved

6. Surgical team assembled
   POST /api/SurgicalTeam/Assign
   {
     "surgeryId": "SURG001",
     "doctorId": "DOC023", // Lead Surgeon
     "role": "LeadSurgeon"
   }
   (Repeat for Anesthetist, Scrub Nurse)

7. Post-operative recovery
   PUT /api/Admission/Update-Recovery
   {
     "admissionId": "ADM001",
     "status": "PostOperative",
     "vitalSigns": { HR: 78, BP: "120/80", O2: 98 }
   }

8. Discharge after recovery
   DELETE /api/Admission/{admissionId}
   → IsDeleted = true, DischargeDate set
   → Bed status = Available
   → Invoice generated
```

### Scenario 2: Outpatient Clinic Appointment → Lab Test → Physiotherapy

```
1. Patient books appointment with cardiologist
   POST /api/Appointment/Book
   {
     "patientId": "P002",
     "doctorId": "DOC015",
     "clinicId": "CLINIC-CARDIO",
     "appointmentDate": "2024-01-20T10:00:00"
   }
   → Appointment created, status = Scheduled

2. Doctor examines patient
   GET /api/Appointment/{appointmentId}
   → Retrieve appointment details

3. Doctor orders lab tests
   POST /api/LabTest/Order
   {
     "patientId": "P002",
     "testType": "BloodWork",
     "testCode": "CBC",
     "orderedByDoctorId": "DOC015"
   }
   → LabTest created, status = Pending

4. Lab technician processes sample
   PUT /api/LabTest/Process
   {
     "labTestId": "LAB001",
     "result": "WBC: 7.2, RBC: 4.8, Hemoglobin: 14.5",
     "status": "Completed"
   }

5. Doctor reviews results and orders physiotherapy
   POST /api/PhysioSession/Schedule
   {
     "patientId": "P002",
     "therapistId": "THER008",
     "sessionType": "Cardiac Rehabilitation",
     "duration": 60
   }
   → PhysioSession created, status = Scheduled

6. Therapist documents session outcomes
   PUT /api/PhysioSession/{sessionId}
   {
     "notes": "Patient completed 30 min treadmill, HR within limits",
     "improvementScore": 8
   }
```

---

## ⚙️ Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server 2019+ (Local or Azure)
- Visual Studio 2022 Community or higher

### Installation

```bash
# Clone repository
git clone https://github.com/MostafaAhmed-100/Hospital-Management-System.git
cd Hospital-Management-System

# Restore dependencies
dotnet restore

# Configure database connection in appsettings.json
# Update "DefaultConnection" with your SQL Server instance

# Apply migrations
dotnet ef database update

# Run application
dotnet run

# Access Swagger UI
# Navigate to: https://localhost:7132/swagger
```

### appsettings.json Configuration

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=HospitalDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
  },
  "Jwt": {
    "Key": "your-256-bit-secret-key-minimum-32-characters-long",
    "Issuer": "https://localhost:7132",
    "Audience": "HospitalAPIUsers",
    "ExpirationInMinutes": 1440
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.EntityFrameworkCore": "Warning"
    }
  }
}
```

---

## 👥 Contributors

| Name | Role | LinkedIn |
|---|---|---|
| **Mostafa Ahmed Soudi** | Backend Developer | [linkedin.com/in/mostafa-ahmed-745497326](https://www.linkedin.com/in/mostafa-ahmed-745497326/) |

*Computer Software Engineering, Egyptian Chinese University (ECU)*  
*B.Sc. Expected September 2029*

---

## 🙏 Mentorship

Special thanks to:

| Name | Contribution | LinkedIn |
|---|---|---|
| **AbdALlatif Hossni** | Architecture guidance, design patterns, enterprise patterns | [linkedin.com/in/abdallatif-hossni](https://www.linkedin.com/in/abdallatif-hossni/) |

---

## 📊 Project Statistics

- **Total Entities:** 31
- **Database Tables:** 31
- **Controllers:** 12+
- **Services:** 12+
- **Repositories:** 15+
- **Validators:** 20+
- **API Endpoints:** 60+
- **Lines of Code:** ~15,000+
- **Development Time:** 6+ months (iterative across 3 releases)

---

## 📄 License

This project is licensed under the MIT License — see LICENSE file for details.

---

**Developed with ❤️ by Mostafa Ahmed Soudi © 2026**
