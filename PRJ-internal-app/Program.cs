using Application.Behaviours;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PRJ.Application.DTOs.Common;
using PRJ.Business;
using PRJ.Business.BasCountries;
using PRJ.Business.BillingServices;
using PRJ.Business.BillingServiceTrnDetails;
using PRJ.Business.BillingServiceTrnHeader;
using PRJ.Business.CustomerProfile;
using PRJ.Business.EntityProfile;
using PRJ.Business.FacilityProfile;
using PRJ.Business.InternalRole;
using PRJ.Business.InternalScreenRole;
using PRJ.Business.InternalUserMaster;
using PRJ.Business.ItemHierarchyStructure;
using PRJ.Business.LegalRepresentativesProfile;
using PRJ.Business.LicenseProfile;
using PRJ.Business.LookupSet;
using PRJ.Business.Lov;
using PRJ.Business.ManufacturerMaster;
using PRJ.Business.NuclearMaterial;
using PRJ.Business.PermitDetailsProfile;
using PRJ.Business.PractiseProfile;
using PRJ.Business.Radionuclide;
using PRJ.Business.RelatedItems;
using PRJ.Business.SafetyResponsibleOfficersProfile;
using PRJ.Business.Screen;
using PRJ.Business.ScreenField;
using PRJ.Business.TreeControl;
using PRJ.Business.Workers;
using PRJ.DataAccess.MSSQL;
using PRJ_internal_app.Coomon;
using PRJ_internal_app.Middleware;
using Serilog;
using System.Text;
using rep = PRJ.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<Helper>();
builder.Services.AddMemoryCache();
// Add services to the container.
builder.Services.AddDbContext<RepositoryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RepositoryContextConnection") ?? throw new InvalidOperationException("Connection string 'RepositoryContextConnection' not found.")));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<rep.IUnitOfWork, rep.UnitOfWork>();
builder.Services.AddScoped<CommonManager>();
builder.Services.AddScoped<InternalUserMasterManager>();
builder.Services.AddScoped<RadionuclideManager>();
builder.Services.AddScoped<LookupSetManager>();
builder.Services.AddScoped<ManufacturerMasterManager>();
builder.Services.AddScoped<PractiseProfileManager>();
builder.Services.AddScoped<EntityProfileManager>();
builder.Services.AddScoped<FacilityProfileManager>();
builder.Services.AddScoped<LicenseProfileManager>();
builder.Services.AddScoped<LegalRepresentativesProfileManager>();
builder.Services.AddScoped<SafetyResponsibleOfficersProfileManager>();
builder.Services.AddScoped<TreeControlManager>();
builder.Services.AddScoped<ItemHierarchyStructureManager>();
builder.Services.AddScoped<ServiceItemProfileManager>();
builder.Services.AddScoped<ActionCenterManager>();
builder.Services.AddScoped<NotificationManager>();
builder.Services.AddScoped<PermitDetailsProfileManager>();
builder.Services.AddScoped<EnquiryCenterManager>();
builder.Services.AddScoped<ItemSourceManager>();
builder.Services.AddScoped<NuclearMaterialManager>();
builder.Services.AddScoped<RelatedItemsManager>();
builder.Services.AddScoped<CustomerProfileManager>();
builder.Services.AddScoped<BillingServiceTrnDetailsManager>();
builder.Services.AddScoped<BillingServiceTrnHeaderManager>();





#region Screen Management
builder.Services.AddScoped<InternalScreenManager>();
builder.Services.AddScoped<InternalScreenFieldManager>();
builder.Services.AddScoped<LovManager>();
builder.Services.AddScoped<InternalFieldPermissionManager>();

#endregion
#region Internal User Management 
builder.Services.AddScoped<InternalRoleManager>();
builder.Services.AddScoped<InternalScreenRoleManager>();
#endregion


builder.Services.AddScoped<RelatedItemHierarchyManager>();
builder.Services.AddScoped<RelatedItemsManager>();
builder.Services.AddScoped<WorkersManager>();

builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

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
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePagesWithReExecute("/errors/{0}");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
