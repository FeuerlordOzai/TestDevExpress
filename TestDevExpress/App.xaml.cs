using Microsoft.Extensions.DependencyInjection;

namespace TestDevExpress
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var services = new ServiceCollection();
            services.AddSingleton<DataService>();
            services.AddTransient<MainViewModel>();
            services.AddTransient<MainPage>();

            var serviceProvider = services.BuildServiceProvider();

            MainPage = serviceProvider.GetRequiredService<MainPage>();
        }
    }
}