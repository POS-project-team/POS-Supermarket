using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

        // 1. ÅÚÏÇÏ æÅäÔÇÁ ÇáÜ Host ÇáÎÇÕ ÈÇáÊØÈíÞ
        var host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.AddTransient<MainForm>();

            })
            .Build();

    
        using (host)
        {
            host.Start();

    
            var mainForm = host.Services.GetRequiredService<MainForm>();

            Application.Run(mainForm);
        }
    }
}