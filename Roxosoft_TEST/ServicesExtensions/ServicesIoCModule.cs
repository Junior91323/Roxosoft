namespace Roxosoft_TEST.ServicesExtensions
{
    using Microsoft.Extensions.DependencyInjection;
    using Roxosoft.BLL.Services.Abstract;
    using Roxosoft.BLL.Services.Implement;
    using Roxosoft.DAL.Repositories.Abstract;
    using Roxosoft.DAL.Repositories.Implement;

    public static class ServicesIoCModule
    {
        public static void AddDALServices(this IServiceCollection services)
        {
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<ICartRepository, CartRepository>();
        }

        public static void AddBLLServices(this IServiceCollection services)
        {
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<ICartService, CartService>();

        }
    }
}
