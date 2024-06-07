using MiTutor.Services.GestionUsuarios;
using MiTutor.Services.TutoringManagement;
using MiTutor.Services.UniversityUnitManagement;
using MiTutor.Services.UserManagement;
using MiTutor.Services;
using MiTutor.Controllers;
using MiTutor.DataAccess;
using MiTutor.Middleware;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsProduction())
{    
    Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/info-.txt", rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: LogEventLevel.Information)
    .WriteTo.File("logs/error-.txt", rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: LogEventLevel.Error)
    .CreateLogger();

    builder.Host.UseSerilog();
}

// Add services to the container.
var services = builder.Services;

var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .Build();

services.AddSingleton(configuration);

services.AddTransient<DatabaseManager>();

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("MiTutorPUCPAppDev",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

services.AddScoped<EspecialidadService>();
services.AddScoped<ActionPlanService>();
services.AddScoped<CommitmentService>();
services.AddScoped<StudentService>();
services.AddScoped<UserAccountService>();
services.AddScoped<FacultyService>();
services.AddScoped<SpecialtyService>();
services.AddScoped<TutorService>();
services.AddScoped<TutoringProgramService>();
services.AddScoped<TutorProgramTutorTypeService>();
services.AddScoped<StudentProgramService>();
services.AddScoped<CommentService>();
services.AddScoped<DerivationService>();
services.AddScoped<AppointmentService>();
services.AddScoped<AppointmentResultService>();
services.AddScoped<AvailabilityTutorService>();
services.AddScoped<TutorStudentProgramService>();
services.AddScoped<ArchivosController>();
services.AddScoped<FilesService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("MiTutorPUCPAppDev");

app.UseAuthorization();

app.UseRequestLogging();

app.UseCustomErrorHandler();

app.MapControllers();

app.Run();
