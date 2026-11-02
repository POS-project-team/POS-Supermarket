
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

        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("secrets.json")
            .Build();

        string? ConnectionString = config.GetConnectionString("Default");

        if (ConnectionString is null)
            throw new ArgumentNullException("Cant Read Connection string");

        services.AddBuisnesServices(ConnectionString);

        using (var serviceProvider = services.BuildServiceProvider())
        {
          
            var mainForm = serviceProvider.GetRequiredService<MainForm>();

       
            Application.Run(mainForm);
        }
    }

}