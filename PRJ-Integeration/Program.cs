using AmanAPI.Middleware;
using Application.Behaviours;
using Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Ocsp;
using wap = PRJ.Business.AmanBusiness;


using PRJ.Business.BillingServiceTrnDetails;
using PRJ.Business.BillingServiceTrnHeader;
using PRJ.Business.EntityProfile;
using PRJ.Business.FacilityProfile;
using PRJ.Business.LegalRepresentativesProfile;
using PRJ.Business.LicenseProfile;
using PRJ.Business.LookupSet;
using PRJ.DataAccess.MSSQL;
using PRJ.Repository;
using Serilog;
using System.Text;
using Microsoft.OpenApi.Models;
using PRJ.Business.BillingServices;
using PRJ_internal_app.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RepositoryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RepositoryContextConnection") ?? throw new InvalidOperationException("Connection string 'RepositoryContextConnection' not found.")));


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<BillingServiceTrnDetailsManager>();
builder.Services.AddScoped<BillingServiceTrnHeaderManager>();
builder.Services.AddScoped<wap.EntityManager>();
builder.Services.AddScoped<wap.FacilityManager>();
builder.Services.AddScoped<wap.PermiteManager>();
builder.Services.AddScoped<wap.LicenseManager>();
builder.Services.AddScoped<wap.LegalRepresentiveManager>();
builder.Services.AddScoped<wap.RsoManager>();
//builder.Services.AddScoped<wap.LicenseInventoryManager>();
builder.Services.AddScoped<ServiceEntryFeesManager>();
builder.Services.AddScoped<BillingServiceController>();




builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = actionContext =>
    {
        var errors = actionContext.ModelState
            .Where(e => e.Value.Errors.Count > 0)
            .SelectMany(x => x.Value.Errors)
            .Select(x => x.ErrorMessage).ToArray();

        var errorResponse = new ApiValidationErrorResponse
        {
            Errors = errors
        };

        return new BadRequestObjectResult(errorResponse);
    };
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                           .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policy =>
    {
        // policy.AllowAnyHeader().AllowAnyMethod().WithOrigins(_config["ClientSettings:URL"]);
        policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();

    });
});

builder.Services.AddSwaggerGen(c =>
{
    var securitySchema = new OpenApiSecurityScheme
    {
        Description = "Using the Authorization header with the Bearer scheme.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };

    c.AddSecurityDefinition("Bearer", securitySchema);
});
//// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Verbose()
    .WriteTo.MSSqlServer(connectionString: builder.Configuration.GetSection("Serilog:ConnectionStrings:LogDatabase").Value,
    tableName: builder.Configuration.GetSection("Serilog:TableName").Value,
    appConfiguration: builder.Configuration,
    autoCreateSqlTable: true,
    columnOptionsSection: builder.Configuration.GetSection("Serilog:ColumnOptions"),
    schemaName: builder.Configuration.GetSection("Serilog:SchemaName").Value)
    .MinimumLevel.Error()
    .CreateLogger();


builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePagesWithReExecute("/errors/{0}");
app.UseHttpsRedirection();
app.UseRouting();
app.UseStaticFiles();
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
