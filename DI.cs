
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using POS_Supermarket.Interface;
namespace POS_Supermarket
{
    public static class DI
    {
       public static ServiceCollection AddRepositories(this ServiceCollection services, string ConnectionString)
       {
            services.AddTransient<IUserRepository>(_ => new UserRepository(ConnectionString));
            
             return services;
       }
       public static ServiceCollection AddServices(this ServiceCollection services)
        {
            services.AddTransient<IUserServices, UserService>();
            return services;
        }
        public static ServiceCollection AddUserForms(this ServiceCollection services)
        {
            services.AddTransient<MainForm>();
            return services;
        }
        public static ServiceCollection AddForms(this ServiceCollection services)
        {
            services.AddUserForms();


            return services;
        }
        public static ServiceCollection AddBuisnesServices(this ServiceCollection services,string ConnectionString)
        {
             
            services.AddRepositories(ConnectionString);
            services.AddServices();
            services.AddForms();
            return services;
        }
    }
}
