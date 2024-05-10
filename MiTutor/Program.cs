using MiTutor.Controllers.TutoringManagement;
using MiTutor.Services.GestionUsuarios;
using MiTutor.Services.TutoringManagement;
using MiTutor.Services.UniversityUnitManagement;
using MiTutor.Services.UserManagement;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("MiTutorPUCPAppDev",
        builder =>
        {
            builder.AllowAnyOrigin()//WithOrigins("http://localhost:5173") // Adjust the URL as needed
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Add Services 
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

app.MapControllers();

app.Run();
