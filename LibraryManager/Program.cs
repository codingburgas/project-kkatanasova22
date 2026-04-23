using LibraryManager.Data; // Увери се, че това е твоят namespace за DbContext
using LibraryManager.Models;
using LibraryManager.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1. Добавяне на базата данни (SQLite)
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? "Data Source=library.db";
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(connectionString));

            // 2. AddControllersWithViews - Регистрация на MVC услугите
            builder.Services.AddControllersWithViews();

            // 3. Конфигурация на Identity с Guid
            builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // 4. Твоите кастъм услуги (BookService и т.н.)
            builder.Services.AddServices();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Account/AccessDenied"; // Увери се, че имаш такъв Action в AccountController
                options.LoginPath = "/Account/Login";
            });

            var app = builder.Build();

            app.UseStaticFiles(); // Позволява ползването на CSS, JS и Bootstrap

            app.UseRouting();

            // 5. Важно: Редът на тези два метода е критичен!
            app.UseAuthentication(); // Кой си ти?
            app.UseAuthorization();  // Какво можеш да правиш?

            // 6. MapControllerRoute - Default pattern
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}