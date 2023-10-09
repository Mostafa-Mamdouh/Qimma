using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using PRJ.Domain.Entities;
using PRJ_ID4Server.DataContext;
using System.Web.Mvc;

var builder = WebApplication.CreateBuilder(args);

var ConnectionString = builder.Configuration.GetConnectionString("RepositoryContextConnection");

var assembly = typeof(Program).Assembly.GetName().Name;

builder.Services.AddDbContext<IdentityServerDbContext>(options => options.UseSqlServer(ConnectionString, sql => sql.MigrationsAssembly(assembly)));


builder.Services.AddLocalization(options => { options.ResourcesPath = "Resources"; });

builder.Services.AddMvc()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


var supportedCulters = new[] { "en", "ar" };
var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCulters[0])
    .AddSupportedCultures(supportedCulters)
    .AddSupportedUICultures(supportedCulters);

app.UseRequestLocalization(localizationOptions);


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}/{id}",
    defaults: new { controller = "Home", action = "Login", id = UrlParameter.Optional }
);

app.Run();
