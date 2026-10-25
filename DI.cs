
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace POS_Supermarket
{
    public static class DI
    {
       public static ServiceCollection addRepositories(this ServiceCollection services)
       {
            services.AddTransient<IUserRepository, UserRepository>();

            return services;
       }
       public static ServiceCollection addConnectionString(this ServiceCollection services)
       {
            services.AddTransient<UserRepository>((IServiceProvider provider) =>
            {
                IConfiguration config = provider.GetRequiredService<IConfiguration>();
               string? Connectionstring = config.GetConnectionString("Default");
                return new UserRepository(Connectionstring!);
            });

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
        public static ServiceCollection AddBuisnesServices(this ServiceCollection services)
        {
            services.addConnectionString();
            services.addRepositories();
            services.AddForms();
            return services;
        }
    }
}
