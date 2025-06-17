using Microsoft.EntityFrameworkCore;
using VisualVibe.Data;
using VisualVibe.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    try
    {
        // Check if 'myadmin' already exists before adding
        if (!context.Users.Any(u => u.Name == "myadmin"))
        {
            var newAdmin = new User
            {
                Name = "myadmin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("mypassword")
                // Role removed to match your current table schema
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

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Admin}/{action=Login}/{id?}");

app.Run();
