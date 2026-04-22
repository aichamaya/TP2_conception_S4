using GestionCryo.Model;
using GestionCryo.Model.Interfaces;
using GestionCryo.Utils;
using GestionCryo.Utils.Commands;
using GestionCryo.Utils.Services.Interfaces;
using System.Windows.Input;

namespace GestionCryo.ViewModel
{
    public class ConnectUserViewModel : BaseViewModel
    {
        private readonly IConnectedUserService _connectedUserService;
        private readonly IUtilisateurDalService _userDALService;
        private readonly INavigationService _navigationService;
        private readonly IAfficheService _afficheService;
        private readonly IStationDalService _stationDalService;
        private string? _username;
        public string Username
        {
            get => _username;
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged(nameof(Username));
                    ValidateProperty(nameof(Username), value);
                }
            }
        }
        private string? _password;

        public string Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(Password));
                    ValidateProperty(nameof(Password), value);
                }
            }
        }

        public ConnectUserViewModel(IUtilisateurDalService userDALService, IConnectedUserService connectedUserService, INavigationService navigationService, IAfficheService afficheService, IStationDalService stationDALService)
        {
            _userDALService = userDALService;
            _connectedUserService = connectedUserService;
            _navigationService = navigationService;
            _afficheService = afficheService;
            _stationDalService = stationDALService;
            ConnectCommand = new RelayCommand(Connect, CanConnect);
        }

        public ICommand ConnectCommand { get; set; }

        private bool CanConnect()
        {
            return !string.IsNullOrWhiteSpace(Username)
                && !string.IsNullOrWhiteSpace(Password)
                && !HasErrors;
        }

        private void Connect()
        {
            ClearErrors(nameof(Username));
            ClearErrors(nameof(Password));
            var user = _userDALService.TrouverParNomEtMotDePasse(Username, Password);
            if (user != null)
            {
                _connectedUserService.ConnectedUser = user;
                if (user.Role == Role.Admin)
                {
                    _navigationService.NavigateTo<AdminHomeViewModel>();
                }
                else
                {
                    _afficheService.StationAffichee = _stationDalService.ObtenirStationParId((int)user.StationId);
                    _navigationService.NavigateTo<HomeViewModel>();
                }
            }
            else
            {
                AddError(nameof(ErrorConnexion), "Nom d'utilisateur ou mot de passe incorrect.");
                OnPropertyChanged(nameof(ErrorMessages));
            }
        }

        private void ValidateProperty(string propertyName, string value)
        {
            ClearErrors(propertyName);
            ClearErrors(nameof(ErrorConnexion));
            if (string.IsNullOrWhiteSpace(value))
            {
                AddError(propertyName, $"{propertyName} ne peut pas être vide.");
            }
            OnPropertyChanged(nameof(ErrorMessages));
        }
        public string ErrorConnexion { get; set; }
    }
}