using Application.Behaviours;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PRJ.Business;
using PRJ.Business.BasCountries;
using PRJ.Business.EntityProfile;
using PRJ.Business.ExternalMaserUser;
using PRJ.Business.FacilityProfile;
using PRJ.Business.LegalRepresentativesProfile;
using PRJ.Business.LicenseInventoryLimits;
using PRJ.Business.LicenseProfile;
using PRJ.Business.LookupSet;
using PRJ.Business.ManufacturerMaster;
using PRJ.Business.NuclearMaterial;
using PRJ.Business.NuclearRelatedItemsProfile;
using PRJ.Business.PermitDetailsProfile;
using PRJ.Business.PermitInventoryLimits;
using PRJ.Business.PractiseProfile;
using PRJ.Business.RadiationGeneratorsProfile;
using PRJ.Business.Radionuclide;
using PRJ.Business.SafetyResponsibleOfficersProfile;
using PRJ.Business.Workers;
using PRJ.DataAccess.MSSQL;
using System.Text;
using ent = PRJ.Domain.Entities;
using rep = PRJ.Repository;
using PRJ.Domain.Entities.BillingServiceTrn;
using PRJ.Business.BillingServiceTrnHeader;
using PRJ.Business.BillingServiceTrnDetails;
using PRJ.Business.ItemHierarchyStructure;
using PRJ.Business.TreeControl;
using PRJ.Business.CustomerProfile;
using PRJ.WebAPI.Middleware;
using Serilog;
using PRJ.Business.DssFiscalYears;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<RepositoryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RepositoryContextConnection") ?? throw new InvalidOperationException("Connection string 'RepositoryContextConnection' not found.")));


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddControllers();


builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddIdentity<ent.ExternalMaserUser, IdentityRole>(
    opt =>
    {
        opt.Password.RequiredLength = 10;
        opt.Password.RequiredUniqueChars = 3;
    }).AddEntityFrameworkStores<RepositoryContext>();

builder.Services.AddScoped<rep.IUnitOfWork, rep.UnitOfWork>();
builder.Services.AddMemoryCache();

builder.Services.AddScoped<ExternalMaserUserManager>();
builder.Services.AddScoped<CommonManager>();
builder.Services.AddScoped<EntityProfileManager>();
builder.Services.AddScoped<FacilityProfileManager>();
builder.Services.AddScoped<LegalRepresentativesProfileManager>();
builder.Services.AddScoped<LicenseInventoryLimitsManager>();
builder.Services.AddScoped<LicenseProfileManager>();
builder.Services.AddScoped<LookupSetManager>();
builder.Services.AddScoped<ManufacturerMasterManager>();
builder.Services.AddScoped<NuclearRelatedItemsProfileManager>();
builder.Services.AddScoped<PermitDetailsProfileManager>();
builder.Services.AddScoped<PermitInventoryLimitsManager>();
builder.Services.AddScoped<PractiseProfileManager>();
builder.Services.AddScoped<RadiationGeneratorsProfileManager>();
builder.Services.AddScoped<SafetyResponsibleOfficersProfileManager>();
builder.Services.AddScoped<RadionuclideManager>();
builder.Services.AddScoped<WorkersManager>();
builder.Services.AddScoped<ItemSourceManager>();
builder.Services.AddScoped<NuclearMaterialManager>();
builder.Services.AddScoped<WorkersDosimetersManager>();
builder.Services.AddScoped<WorkersExposuresDosesManager>();
builder.Services.AddScoped<BillingServiceTrnHeaderManager>();
builder.Services.AddScoped<BillingServiceTrnDetailsManager>();
builder.Services.AddScoped<ItemHierarchyStructureManager>();
builder.Services.AddScoped<TreeControlManager>();
builder.Services.AddScoped<CustomerProfileManager>();
builder.Services.AddScoped<NuclearMaterialManager>();
builder.Services.AddScoped<DssFiscalYearsManager>();


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



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("CommonAPI", new OpenApiInfo { Title = "NRRC Common API", Version = "v1" });
    c.SwaggerDoc("EntityProfileAPI", new OpenApiInfo { Title = "Entity Profile API", Version = "v1" });
    c.SwaggerDoc("FacilityProfileAPI", new OpenApiInfo { Title = "Facility Profile API", Version = "v1" });
    c.SwaggerDoc("ManufacturerMasterAPI", new OpenApiInfo { Title = "Manufacturer Master API", Version = "v1" });
    c.SwaggerDoc("NuclearRelatedItemsProfileAPI", new OpenApiInfo { Title = "Nuclear RelatedItems Profile API", Version = "v1" });
    c.SwaggerDoc("RadiationGeneratorsProfileAPI", new OpenApiInfo { Title = "Radiation Generators Profile API", Version = "v1" });
    c.SwaggerDoc("SafetyResponsibleOfficersProfileAPI", new OpenApiInfo { Title = "Safety Responsible Officers Profile API", Version = "v1" });
    c.SwaggerDoc("LegalRepresentativesProfileApi", new OpenApiInfo { Title = "LegalRepresentativesProfile API", Version = "v1" });
    c.SwaggerDoc("LicenseInventoryLimitsApi", new OpenApiInfo { Title = "LicenseInventoryLimitsApi", Version = "v1" });
    c.SwaggerDoc("LicenseProfilepi", new OpenApiInfo { Title = "LicenseProfilepi", Version = "v1" });
    c.SwaggerDoc("LookupApi", new OpenApiInfo { Title = "LookupSet API", Version = "v1" });
    c.SwaggerDoc("PermitDetailsProfileApi", new OpenApiInfo { Title = "PermitDetailsProfileApi Responsible Profile API", Version = "v1" });
    c.SwaggerDoc("PermitInventoryLimitsApi", new OpenApiInfo { Title = "PermitInventoryLimitsApi Responsible Officers Profile API", Version = "v1" });
    c.SwaggerDoc("PractiseProfileApi", new OpenApiInfo { Title = "PractiseProfileApi Responsible Officers Profile API", Version = "v1" });
    c.SwaggerDoc("RadionuclideApi", new OpenApiInfo { Title = "Radionuclide API", Version = "v1" });
    c.SwaggerDoc("ItemSourcesApi", new OpenApiInfo { Title = "Item Sources Api", Version = "v1" });
    c.SwaggerDoc("Workers", new OpenApiInfo { Title = "Workers API", Version = "v1" });
    c.SwaggerDoc("WorkersExposuresDoses", new OpenApiInfo { Title = "WorkersExposuresDoses API", Version = "v1" });
    c.SwaggerDoc("WorkersDosimeters", new OpenApiInfo { Title = "WorkersDosimeters API", Version = "v1" });
    c.SwaggerDoc("BillingServiceTrnHeader", new OpenApiInfo { Title = "BillingServiceTrnHeader", Version = "v1" });
    c.SwaggerDoc("ItemHierarchyStructure", new OpenApiInfo { Title = "ItemHierarchyStructureController", Version = "v1" });
    c.SwaggerDoc("CustomerProfile", new OpenApiInfo { Title = "CustomerProfileController", Version = "v1" });



    c.SwaggerDoc("UserInfoApi", new OpenApiInfo { Title = "User Info Api", Version = "v1" });
    c.SwaggerDoc("NuclearMaterial", new OpenApiInfo { Title = "Nuclear Material Api", Version = "v1" });
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
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/CommonAPI/swagger.json", "NRRC Common API v1");
        c.SwaggerEndpoint("/swagger/EntityProfileAPI/swagger.json", "Entity Profile API v1");
        c.SwaggerEndpoint("/swagger/FacilityProfileAPI/swagger.json", "Facility Profile API v1");
        c.SwaggerEndpoint("/swagger/ManufacturerMasterAPI/swagger.json", "Manufacturer Master API v1");
        c.SwaggerEndpoint("/swagger/NuclearRelatedItemsProfileAPI/swagger.json", "Nuclear Related Items ProfileAPI v1");
        c.SwaggerEndpoint("/swagger/RadiationGeneratorsProfileAPI/swagger.json", "Radiation Generators Profile API v1");
        c.SwaggerEndpoint("/swagger/SafetyResponsibleOfficersProfileAPI/swagger.json", "Safety Responsible Officers Profile API v1");
        c.SwaggerEndpoint("/swagger/LegalRepresentativesProfileApi/swagger.json", "LegalRepresentativesProfile");
        c.SwaggerEndpoint("/swagger/LicenseInventoryLimitsApi/swagger.json", "LicenseInventoryLimitsApi");
        c.SwaggerEndpoint("/swagger/LicenseProfilepi/swagger.json", "LicenseProfilepi");
        c.SwaggerEndpoint("/swagger/LookupApi/swagger.json", "LookupSet APIs");
        c.SwaggerEndpoint("/swagger/PermitDetailsProfileApi/swagger.json", "PermitDetailsProfileApi Responsible Profile API");
        c.SwaggerEndpoint("/swagger/PermitInventoryLimitsApi/swagger.json", "PermitInventoryLimitsApi Responsible Officers Profile API");
        c.SwaggerEndpoint("/swagger/PractiseProfileApi/swagger.json", "PractiseProfileApi Responsible Officers Profile API");
        c.SwaggerEndpoint("/swagger/UserInfoApi/swagger.json", "User Info Api");
        c.SwaggerEndpoint("/swagger/RadionuclideApi/swagger.json", "Radionuclide Api");
        c.SwaggerEndpoint("/swagger/ItemSourcesApi/swagger.json", "Item Sources Api API");
        c.SwaggerEndpoint("/swagger/Workers/swagger.json", "Workers API");
        c.SwaggerEndpoint("/swagger/NuclearMaterial/swagger.json", "NuclearMaterial API");
        c.SwaggerEndpoint("/swagger/WorkersDosimeters/swagger.json", "WorkersDosimeters Api API");
        c.SwaggerEndpoint("/swagger/WorkersExposuresDoses/swagger.json", "WorkersExposuresDoses API");
        c.SwaggerEndpoint("/swagger/NuclearMaterial/swagger.json", "NuclearMaterial API");


        c.SwaggerEndpoint("/swagger/BillingServiceTrnHeader/swagger.json", "BillingServiceTrnHeader");
        c.SwaggerEndpoint("/swagger/ItemHierarchyStructure/swagger.json", "ItemHierarchyStructure");
        c.SwaggerEndpoint("/swagger/CustomerProfile/swagger.json", "CustomerProfile");


        
    });
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePagesWithReExecute("/errors/{0}");
app.UseCors(x => x
           .AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader());
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;


app.MapControllers();

app.Run();
