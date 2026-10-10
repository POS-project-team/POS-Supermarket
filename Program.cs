using System;
using System.Windows.Forms;
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
       
        services.AddTransient<MainForm>();


        using (var serviceProvider = services.BuildServiceProvider())
        {
          
            var mainForm = serviceProvider.GetRequiredService<MainForm>();

       
            Application.Run(mainForm);
        }
    }

}