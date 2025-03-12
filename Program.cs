using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.Models;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Invalid Connection String");

builder.Services.AddDbContext<ApplicationDbContext>(
    option => option.UseSqlServer(connectionString));


builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultUI().AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

using var scope = builder.Services.BuildServiceProvider().CreateScope();//////
var services = scope.ServiceProvider;

var loggerFactory = services.GetRequiredService<ILoggerProvider>();
var logger = loggerFactory.CreateLogger("app");

try
{
    var userManager = services.GetRequiredService<UserManager<User>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    await ProductCatalog.Seeds.DefaultRoles.SeedRoles(roleManager);
    await ProductCatalog.Seeds.DefaultUsers.SeedUser(userManager, roleManager);
    await ProductCatalog.Seeds.DefaultUsers.SeedAdmin(userManager, roleManager);

    logger.LogInformation("Data Seeded");
}
catch
{
    logger.LogInformation("Erroe Occure While Seeding Data");
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
