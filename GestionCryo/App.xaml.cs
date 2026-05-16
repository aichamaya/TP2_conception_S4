using GestionCryo.Data;
using GestionCryo.Model.DAL;
using GestionCryo.Model.Interfaces;
using GestionCryo.Utils;
using GestionCryo.Utils.Services;
using GestionCryo.Utils.Services.Interfaces;
using GestionCryo.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace GestionCryo
{
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            IServiceCollection services = new ServiceCollection();
            ConfigurerServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigurerServices(IServiceCollection services)
        {
            services.AddSingleton<MainWindow>(provider => new MainWindow
            {
                DataContext = provider.GetRequiredService<MainViewModel>()
            });
            ConfigurerViewModels(services);
            ConfigurerDAL(services);
            ConfigurerServices2(services);
        }

        
        private void ConfigurerViewModels(IServiceCollection services)
        {
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<HomeViewModel>();
            services.AddSingleton<ConnectUserViewModel>();
            services.AddSingleton<AdminHomeViewModel>();
            services.AddSingleton<BateauViewModel>();
            services.AddSingleton<CommandePieceViewModel>();
            services.AddSingleton<Func<Type, BaseViewModel>>(serviceProvider =>
            {
                BaseViewModel ViewModelFactory(Type viewModelType)
                {
                    return (BaseViewModel)serviceProvider.GetRequiredService(viewModelType);
                }
                return ViewModelFactory;
            });
        }

        
        private void ConfigurerDAL(IServiceCollection services)
        {
            services.AddSingleton<IUtilisateurDAL, UtilisateurDAL>();
            services.AddSingleton<IBateauDAL, BateauDAL>();
            services.AddSingleton<IStationDAL, StationDAL>();
            services.AddSingleton<IMaterielDAL, MaterielDAL>();
            services.AddDbContext<ApplicationDbContext>();
            services.AddSingleton<ICommandePieceDAL, CommandePieceDAL>();
        }

        
        private void ConfigurerServices2(IServiceCollection services)
        {
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IConnectedUserService, ConnectedUserService>();
            services.AddSingleton<IAfficheService, AfficheService>();
            services.AddSingleton<IAlerteService, AlerteService>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                ApplicationDbContext dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                if (dbContext.Database.EnsureCreated())
                {
                    dbContext.SeedData();
                }
            }
            MainWindow mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
            base.OnStartup(e);
        }
    }
}