using MiTutor.Services;
using MiTutor.Services.Usuarios;

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
            builder.WithOrigins("http://localhost:3000") // Adjust the URL as needed
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Add Services
services.AddScoped<EspecialidadService>();
services.AddScoped<UsuarioService>();
services.AddScoped<EstudianteService>();


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
