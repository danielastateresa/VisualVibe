using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using VisualVibe.Data;
using VisualVibe.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

// Add DbContext with MySQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

// Add cookie authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

// Enable authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();

// Seed admin user
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    try
    {
        if (!context.Users.Any(u => u.Name == "myadmin"))
        {
            var newAdmin = new User
            {
                Name = "myadmin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("mypassword")
            };
            context.Users.Add(newAdmin);
            context.SaveChanges();
        }
    }
    catch (DbUpdateException ex)
    {
        Console.WriteLine("DbUpdateException: " + ex.InnerException?.Message);
    }
}

// Map default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Admin}/{action=Login}/{id?}");

app.Run();
