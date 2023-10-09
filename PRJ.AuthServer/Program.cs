using DNTCaptcha.Core;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PRJ.AuthServer;
using PRJ.DataAccess.MSSQL;
using PRJ.Domain.Entities;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("RepositoryContextConnection") ?? throw new InvalidOperationException("Connection string 'WebApplication22ContextConnection' not found.");
builder.Services.AddDbContext<RepositoryContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddIdentity<ExternalMaserUser, IdentityRole>(opt =>
{

	opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
	opt.Lockout.MaxFailedAccessAttempts = 5;
	opt.SignIn.RequireConfirmedEmail = true;

}).AddEntityFrameworkStores<RepositoryContext>().AddDefaultTokenProviders();


builder.Services.AddScoped<Common>();
builder.Services.Configure<DataProtectionTokenProviderOptions>(
	o => o.TokenLifespan = TimeSpan.FromMinutes(15));

builder.Services.AddMvc(opt =>
{
	var Policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
	opt.Filters.Add(new AuthorizeFilter(Policy));

}).AddXmlSerializerFormatters();
builder.Services.AddDNTCaptcha(opt => opt.UseCookieStorageProvider().ShowThousandsSeparators(false));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Account}/{action=LogIn}/{id?}");



app.Run();
