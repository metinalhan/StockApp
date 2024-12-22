using Microsoft.Extensions.DependencyInjection;
using StockApp.BL.Abstract;
using StockApp.BL.Mapper;
using StockApp.BL.Services;
using StockApp.DAL.Context;
using StockApp.DAL.Repositories;
using StockApp.UI.ViewModel;
using System.Reflection;
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
            services.AddSingleton<ProductListVM>();

            // Repository Registration
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

            // Register ProductService
            services.AddTransient<ProductService>();
            services.AddTransient<IExcelService,ExcelService>();
           
            services.AddAutoMapper(Assembly.GetAssembly(typeof(ProductProfile)));

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
