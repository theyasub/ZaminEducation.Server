using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;
using ZaminEducation.Api;
using ZaminEducation.Api.Middlewares;
using ZaminEducation.Data.DbContexts;
using ZaminEducation.Service.Helpers;
using ZaminEducation.Service.Mappers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

builder.Services.AddControllers();

// Add db context
builder.Services.AddDbContext<ZaminEducationDbContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Serilog
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// Add custom services
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.AddSwaggerService();
builder.Services.AddCustomServices();
builder.Services.AddHttpContextAccessor();

//Convert  Api url name to dash case 
builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new RouteTokenTransformerConvention(
                                 new ConfigureApiUrlName()));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Set helpers
EnvironmentHelper.WebRootPath = app.Services.GetService<IWebHostEnvironment>()?.WebRootPath;

if (app.Services.GetService<IHttpContextAccessor>() != null)
    HttpContextHelper.Accessor = app.Services.GetRequiredService<IHttpContextAccessor>();

app.UseMiddleware<ZaminEducationExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
