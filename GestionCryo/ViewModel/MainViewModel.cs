using GestionCryo.Model;

using GestionCryo.Utils;
using GestionCryo.Utils.Commands;

using GestionCryo.Utils.Services.Interfaces;

using System.Windows.Input;

namespace GestionCryo.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IAfficheService _afficheService;
        private readonly IStationDalService _stationDALService;
        public INavigationService NavigationService
        {
            get => _navigationService;
        }
        private readonly IConnectedUserService _connectedUserService;
        public IConnectedUserService ConnectedUserService
        {
            get => _connectedUserService;
        }

        public ICommand RetourAccueilCommande { get; set; }
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

                _afficheService.StationAffichee = _stationDALService.ObtenirStationParId((int)(ConnectedUserService.ConnectedUser?.StationId));
                _navigationService.NavigateTo<HomeViewModel>();
            }
        }
        public ICommand SeDeconnecterCommande { get; set; }
        public bool CanDisconnect()
        {
            return _connectedUserService.ConnectedUser != null;
        }
      
        public void Deconnexion()
        {
            _connectedUserService.ConnectedUser = null;
            _navigationService.NavigateTo<ConnectUserViewModel>();
        }
        public ICommand PageConnexionCommande { get; set; }
        public bool CanConnect()
        {
            return _connectedUserService.ConnectedUser == null;
        }
        public MainViewModel(INavigationService navigationService, IConnectedUserService connectedUserService, IAfficheService afficheService, IStationDalService stationDALService)
        {
            _navigationService = navigationService;
            _connectedUserService = connectedUserService;
            _afficheService = afficheService;
            _stationDALService = stationDALService;
            //Initiation des boutons de navigation
            RetourAccueilCommande = new RelayCommand( GoHome, CanGoHome);
            SeDeconnecterCommande = new RelayCommand(Deconnexion, CanDisconnect);
            PageConnexionCommande = new RelayCommand(() => _navigationService.NavigateTo<ConnectUserViewModel>(), CanConnect);
            // Initial navigation to the ConnectUserViewModel
            _navigationService.NavigateTo<ConnectUserViewModel>();
        }
    }
}
