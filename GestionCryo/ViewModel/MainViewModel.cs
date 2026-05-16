using GestionCryo.Model;
using GestionCryo.Model.Interfaces;
using GestionCryo.Utils;
using GestionCryo.Utils.Commands;
using GestionCryo.Utils.Services.Interfaces;
using System.Reflection;
using System.Windows.Input;

namespace GestionCryo.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IAfficheService _afficheService;
        private readonly IStationDAL _stationDAL;
        private readonly IConnectedUserService _connectedUserService;

        public INavigationService NavigationService
        {
            get => _navigationService;
        }

        public IConnectedUserService ConnectedUserService
        {
            get => _connectedUserService;
        }

        public string ApplicationVersion
        {
            get
            {
                Version? version = Assembly.GetExecutingAssembly().GetName().Version;
                return version == null ? "Version 1.0.0" : $"Version {version.Major}.{version.Minor}.{version.Build}";
            }
        }

        
        public MainViewModel(INavigationService navigationService, IConnectedUserService connectedUserService, IAfficheService afficheService, IStationDAL stationDAL)
        {
            _navigationService = navigationService;
            _connectedUserService = connectedUserService;
            _afficheService = afficheService;
            _stationDAL = stationDAL;
            RetourAccueilCommande = new RelayCommand(GoHome, CanGoHome);
            SeDeconnecterCommande = new RelayCommand(Deconnexion, CanDisconnect);
            PageConnexionCommande = new RelayCommand(() => _navigationService.NavigateTo<ConnectUserViewModel>(), CanConnect);
            _navigationService.NavigateTo<ConnectUserViewModel>();
        }

        
        public ICommand RetourAccueilCommande { get; set; }

        
        public ICommand SeDeconnecterCommande { get; set; }

        
        public ICommand PageConnexionCommande { get; set; }

        
        public bool CanGoHome()
        {
            return _connectedUserService.ConnectedUser != null;
        }

        
        public void GoHome()
        {
            if (_connectedUserService.ConnectedUser.Role == Role.Admin)
            {
                _navigationService.NavigateTo<AdminHomeViewModel>();
            }
            else
            {
                _afficheService.StationAffichee = _stationDAL.ObtenirStationParId((int)ConnectedUserService.ConnectedUser?.StationId);
                _navigationService.NavigateTo<HomeViewModel>();
            }
        }

        
        public bool CanDisconnect()
        {
            return _connectedUserService.ConnectedUser != null;
        }

        
        public void Deconnexion()
        {
            _connectedUserService.ConnectedUser = null;
            _navigationService.NavigateTo<ConnectUserViewModel>();
        }

        
        public bool CanConnect()
        {
            return _connectedUserService.ConnectedUser == null;
        }
    }
}
