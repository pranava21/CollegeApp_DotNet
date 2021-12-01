using CollegeApp_DotNet.DataAccess.Models;
using CollegeApp_DotNet.WebServices;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Templates;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseSerilog((ctx, lc) =>
{
    lc.WriteTo.File("./logs/error/logError_.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: null, restrictedToMinimumLevel: LogEventLevel.Error);
    lc.WriteTo.File("./logs/info/logInformation_.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: null, restrictedToMinimumLevel: LogEventLevel.Information);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

DependencyInjection.RegisterServies(builder.Services);
builder.Services.AddDbContext<collegeDatabaseContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
