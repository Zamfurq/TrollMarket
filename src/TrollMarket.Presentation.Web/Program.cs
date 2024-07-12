using Microsoft.AspNetCore.Authentication.Cookies;
using TrollMarket.DataAccess;
using TrollMarket.Presentation.Web.Configurations;
using TrollMarket.Presentation.Web.Services;

namespace TrollMarket.Presentation.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Logging.AddConsole();

            IServiceCollection services = builder.Services;
            services.AddScoped<AuthService>();
            services.AddScoped<ProfileService>();
            services.AddScoped<MerchandiseService>();
            services.AddScoped<ShopService>();
            services.AddScoped<ShipmentService>();
            services.AddScoped<CartService>();
            services.AddScoped<AdminService>();
            services.AddScoped<TransactionService>();

            services.AddBusinessServices();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                    options.AccessDeniedPath = "/AccessDenied";
                });

            // Add services to the container.
            services.AddControllersWithViews();

            Dependencies.ConfigureServices(builder.Configuration, services);

            var app = builder.Build();


            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Auth}/{action=Login}/{id?}");

            app.Run();
        }
    }
}