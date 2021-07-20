using Skylight.DataAccess.IContract;
using Skylight.DataAccess.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SkylightDesktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        IGlobalService service = new GlobalService();
        async Task initGlobalStateData()
        {
            var data = await service.LoadStates();
            GlobalService.GlobalState = data.data;

        }
           async Task initGlobalProviderData()
        {
            var data = await service.LoadProviders();
            GlobalService.GlobalProvider = data.data;

        }  
        
        async Task initGlobalCompanyData()
        {
            var data = await service.LoadCompanies();
            GlobalService.GlobalCompany = data.data;

        } 
        
        async Task initGlobalCompanyPolicyData()
        {
            var data = await service.LoadCompanyPolicy();
            GlobalService.GlobalCompanyPolicy = data.data;

        } 
        
        async Task initGlobalPlanData()
        {
            var data = await service.LoadPolicy();
            GlobalService.GlobalPlan = data.data;

        }



        void App_Startup(object sender, StartupEventArgs e)
        {
            // Application is running
            // Process command line args
            bool startMinimized = false;
            for (int i = 0; i != e.Args.Length; ++i)
            {
                if (e.Args[i] == "/StartMinimized")
                {
                    startMinimized = true;
                }
            }
            var tasks = new List<Task>();
            tasks.Add(initGlobalStateData());
            tasks.Add(initGlobalCompanyData());
            tasks.Add(initGlobalProviderData());
            tasks.Add(initGlobalCompanyPolicyData());
            tasks.Add(initGlobalPlanData());
            Task result = Task.WhenAll(tasks); ;
            try
            {
                 result.ConfigureAwait(false);
            }
            catch { }

            // Create main application window, starting minimized if specified
            MainWindow mainWindow = new MainWindow();
            if (startMinimized)
            {
                mainWindow.WindowState = WindowState.Minimized;
            }
            mainWindow.Show();
        }

    }



}
