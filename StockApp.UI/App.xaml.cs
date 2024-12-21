using Microsoft.Extensions.DependencyInjection;
using StockApp.BL.Services;
using StockApp.DAL.Context;
using StockApp.DAL.Repositories;
using System.Windows;

namespace StockApp.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            var services = new ServiceCollection();

            // Services Registration
            ConfigureServices(services);

            _serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            // Register DbContext
            services.AddSingleton<AppDbContext>();

            // Repository Registration
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

            // Register ProductService
            services.AddTransient<ProductService>();

            // MainWindow Registration
            services.AddTransient<MainWindow>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
            base.OnStartup(e);
        }
    }

}
