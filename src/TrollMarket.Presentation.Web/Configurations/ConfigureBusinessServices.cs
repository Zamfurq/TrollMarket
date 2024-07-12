using TrollMarket.Business.Interfaces;
using TrollMarket.Business.Repositories;

namespace TrollMarket.Presentation.Web.Configurations
{
    public static class ConfigureBusinessServices
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IMerchandiseRepository, MerchandiseRepository>();
            services.AddScoped<IShipmentRepository, ShipmentRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();

            return services;
        }
    }
}
