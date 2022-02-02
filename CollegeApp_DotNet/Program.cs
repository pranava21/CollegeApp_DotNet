using AutoMapper;
using CollegeApp_DotNet.BusinessDomain;
using CollegeApp_DotNet.DataAccess.Models;
using CollegeApp_DotNet.WebServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);    
// Add services to the container.
#region Serilog
builder.Host.UseSerilog((ctx, lc) =>
{
    lc.WriteTo.File("./logs/error/logError_.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: null, restrictedToMinimumLevel: LogEventLevel.Error);
    lc.WriteTo.File("./logs/info/logInformation_.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: null, restrictedToMinimumLevel: LogEventLevel.Information);
});
#endregion

#region AutoMapper
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new AutoMapperConfig());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddMvc();
#endregion

builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => {
        options.AllowAnyOrigin();
        options.AllowAnyHeader();
        options.AllowAnyMethod();
     });
});
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
string connString = builder.Configuration.GetConnectionString("CollegeAppConnectionString");
builder.Services.AddDbContext<collegeDatabaseContext>(options => options.UseNpgsql(connString));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://securetoken.google.com/college-app-4d7d2";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "https://securetoken.google.com/college-app-4d7d2",
            ValidateAudience = true,
            ValidAudience = "college-app-4d7d2",
            ValidateLifetime = true
        };
    });

DependencyInjection.RegisterServies(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
app.UseAuthentication();
app.UseAuthorization();
app.UseCors();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
