using FluentValidation;
using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Filters;
using HospitalManagementSystem.Middlewares;
using HospitalManagementSystem.Repository.UnitofWork;
using HospitalManagementSystem.Service.ClinicService;
using HospitalManagementSystem.Service.DepartmentService;
using HospitalManagementSystem.Service.DoctorService;
using HospitalManagementSystem.Service.SpecialtyService;
using HospitalManagementSystem.Service.MedicineService;
using HospitalManagementSystem.Service.InsuranceProviderService;
using HospitalManagementSystem.Service.InvoiceService;
using HospitalManagementSystem.Service.PaymentService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
using HospitalManagementSystem.Service.OutpatientVisitsService.AppointmentService;
using HospitalManagementSystem.Service.OutpatientVisitsService.MedicalRecordService;
using HospitalManagementSystem.Service.OutpatientVisitsService.PatientService;
using HospitalManagementSystem.Service.PharmacysService.PrescriptionItemService;
using HospitalManagementSystem.Service.PharmacysService.PharmacyService;
using HospitalManagementSystem.Service.PharmacysService.PharmacySaleService;
using HospitalManagementSystem.Service.PharmacysService.PharmacyInventoryService;
using HospitalManagementSystem.Service.PharmacysService.PrescriptionService;
using HospitalManagementSystem.Service.PharmacysService.SaleItemService;
using HospitalManagementSystem.Service.InpatientService.RoomService;
using HospitalManagementSystem.Service.InpatientService.BedService;
using HospitalManagementSystem.Service.InpatientService.AdmissionService;
using HospitalManagementSystem.Service.SurgeryService.OperatingRoomService;
using HospitalManagementSystem.Service.SurgeryService.SurgeryRecordService;
using HospitalManagementSystem.Service.SurgeryService.SurgeryTeamService;
using HospitalManagementSystem.Service.NursingStaffService.StaffService;
using HospitalManagementSystem.Service.NursingStaffService.NurseService;
using HospitalManagementSystem.Service.NursingStaffService.NurseAssignmentService;
using HospitalManagementSystem.Service.EmergencyService.ErVisitService;
using HospitalManagementSystem.Service.LabTestService;
using HospitalManagementSystem.Service.PhysiotherapyService.TherapistService;
using HospitalManagementSystem.Service.PhysiotherapyService.PhysioSessionService;
using HospitalManagementSystem.Service.ReportingService;

namespace HospitalManagementSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.AddRateLimiter(options =>
            {
                options.AddFixedWindowLimiter("Standard", opt =>
                {
                    opt.Window = TimeSpan.FromMinutes(1);
                    opt.PermitLimit = 60;
                    opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    opt.QueueLimit = 2;
                });

                options.AddFixedWindowLimiter("Strict", opt =>
                {
                    opt.Window = TimeSpan.FromMinutes(1);
                    opt.PermitLimit = 15;
                    opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    opt.QueueLimit = 0;
                });

                options.OnRejected = async (context, token) =>
                {
                    context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                    context.HttpContext.Response.ContentType = "text/plain; charset=utf-8";
                    await context.HttpContext.Response.WriteAsync("تم تجاوز الحد المسموح من الطلبات. يرجى المحاولة لاحقاً.", token);
                };
            });

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(cfg =>
                cfg.AddMaps(Assembly.GetExecutingAssembly())
               );

            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IClinicService, ClinicService>();
            builder.Services.AddScoped<ISpecialtyService, SpecialtyService>();
            builder.Services.AddScoped<IDoctorService, DoctorService>();
            builder.Services.AddScoped<IPatientService, PatientService>();
            builder.Services.AddScoped<IAppointmentService, AppointmentService>();
            builder.Services.AddScoped<IMedicalRecordService, MedicalRecordService>();
            builder.Services.AddScoped<IMedicineService, MedicineService>();
            builder.Services.AddScoped<IPharmacyService, PharmacyService>();
            builder.Services.AddScoped<IPharmacyInventoryService, PharmacyInventoryService>();
            builder.Services.AddScoped<IPharmacySaleService, PharmacySaleService>();
            builder.Services.AddScoped<IPrescriptionService, PrescriptionService>();
            builder.Services.AddScoped<IPrescriptionItemService, PrescriptionItemService>();
            builder.Services.AddScoped<ISaleItemService, SaleItemService>();
            builder.Services.AddScoped<IInsuranceProviderService, InsuranceProviderService>();
            builder.Services.AddScoped<IInvoiceService, InvoiceService>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<IRoomService, RoomService>();
            builder.Services.AddScoped<IBedService, BedService>();
            builder.Services.AddScoped<IAdmissionService, AdmissionService>();
            builder.Services.AddScoped<IOperatingRoomService, OperatingRoomService>();
            builder.Services.AddScoped<ISurgeryRecordService, SurgeryRecordService>();
            builder.Services.AddScoped<ISurgeryTeamService, SurgeryTeamService>();
            builder.Services.AddScoped<IStaffService, StaffService>();
            builder.Services.AddScoped<INurseService, NurseService>();
            builder.Services.AddScoped<INurseAssignmentService, NurseAssignmentService>();
            builder.Services.AddScoped<IErVisitService, ErVisitService>();
            builder.Services.AddScoped<ILabTestService, LabTestService>();
            builder.Services.AddScoped<ITherapistService, TherapistService>();
            builder.Services.AddScoped<IPhysioSessionService, PhysioSessionService>();
            builder.Services.AddScoped<IReportingService, ReportingService>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    b => b.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
            });

            builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<ValidationFilter>();
            });

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {

            }

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseHttpsRedirection();

            app.UseCors("AllowAll");

            app.UseRateLimiter();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}