using Microsoft.Extensions.DependencyInjection;
using POS_Supermarket.Controller;
using POS_Supermarket.Interface;
using POS_Supermarket.Interfaces;
using POS_Supermarket.Views.UserForms;

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
        public static ServiceCollection AddForms(this ServiceCollection services)
        {
            services.AddUserForms();


            return services;
        }
        public static ServiceCollection addControllers(this ServiceCollection services)
        {
            services.AddTransient<UserController>(provider =>
            {
                var IuserService = provider.GetRequiredService<IUserServices>();
                return new UserController(IuserService);

            });
            return services;
        }
        public static ServiceCollection AddUserForms(this ServiceCollection services)
        {
            services.AddTransient<Login>();
            services.AddTransient<MainForm>();
            services.AddTransient<test_addNewUser>();
            services.AddTransient<ILoginView>(provider => provider.GetRequiredService<Login>());
            services.AddTransient<IAddNewUserView>(provider => provider.GetRequiredService<test_addNewUser>());

            return services;
        }
        public static ServiceCollection AddBuisnesServices(this ServiceCollection services, string ConnectionString)
        {
            services.AddRepositories(ConnectionString);
            services.AddServices();
            services.AddForms();
            services.addControllers();
            return services;
        }
    }
}