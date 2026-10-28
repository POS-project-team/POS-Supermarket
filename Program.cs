using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace POS_Supermarket;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();


        var services = new ServiceCollection();

        IConfiguration configuration =

            new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("secrets.json", false,true)
            .Build();

        string? ConnectionString = configuration.GetConnectionString("Default");

        if (ConnectionString is null)
            throw new NullReferenceException("can't read connection string ");

        services.AddBuisnesServices(ConnectionString);

        using (var serviceProvider = services.BuildServiceProvider())
        {
              
            var mainForm = serviceProvider.GetRequiredService<MainForm>();

       
            Application.Run(mainForm);
        }
    }

}