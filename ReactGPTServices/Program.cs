using Microsoft.Extensions.Logging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
builder.Logging.ClearProviders(); // Clear existing logging providers
Log.Logger = new LoggerConfiguration().WriteTo.File("Logs/MiTutorPUCP_.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Logging.AddSerilog();

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
