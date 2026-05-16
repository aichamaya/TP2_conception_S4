using GestionCryo.Model;
using GestionCryo.Model.Interfaces;
using GestionCryo.Utils;
using GestionCryo.Utils.Commands;
using GestionCryo.Utils.Services.Interfaces;
using System;
using System.Diagnostics;
using System.Windows.Input;

namespace GestionCryo.ViewModel
{
    public class ConnectUserViewModel : BaseViewModel
    {
        private readonly IConnectedUserService _connectedUserService;
        private readonly IUtilisateurDAL _utilisateurDAL;
        private readonly INavigationService _navigationService;
        private readonly IAfficheService _afficheService;
        private readonly IStationDAL _stationDAL;

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

        
        public ConnectUserViewModel(IUtilisateurDAL utilisateurDAL, IConnectedUserService connectedUserService, INavigationService navigationService, IAfficheService afficheService, IStationDAL stationDAL)
        {
            _utilisateurDAL = utilisateurDAL;
            _connectedUserService = connectedUserService;
            _navigationService = navigationService;
            _afficheService = afficheService;
            _stationDAL = stationDAL;
            ConnectCommand = new RelayCommand(Connect, CanConnect);
        }

        
        public ICommand ConnectCommand { get; set; }

        public string ErrorConnexion { get; set; }

      
        private bool CanConnect()
        {
            return !string.IsNullOrWhiteSpace(Username)
                && !string.IsNullOrWhiteSpace(Password)
                && !HasErrors;
        }

        
        private void Connect()
        {
            try
            {
                ClearErrors(nameof(Username));
                ClearErrors(nameof(Password));

                Utilisateur? user = _utilisateurDAL.TrouverParNomEtMotDePasse(Username, Password);

                if (user != null)
                {
                    _connectedUserService.ConnectedUser = user;
                    Trace.TraceInformation($"Connexion réussie pour l'utilisateur : {user.Username}");

                    if (user.Role == Role.Admin)
                    {
                        _navigationService.NavigateTo<AdminHomeViewModel>();
                    }
                    else
                    {
                        _afficheService.StationAffichee = _stationDAL.ObtenirStationParId((int)user.StationId);
                        _navigationService.NavigateTo<HomeViewModel>();
                    }
                }
                else
                {
                    Trace.TraceWarning($"Tentative de connexion échouée pour l'utilisateur : {Username}");
                    AddError(nameof(ErrorConnexion), "Nom d'utilisateur ou mot de passe incorrect.");
                    OnPropertyChanged(nameof(ErrorMessages));
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Erreur lors de la connexion : {ex.Message}");
                AddError(nameof(ErrorConnexion), "Une erreur est survenue. Veuillez réessayer.");
                OnPropertyChanged(nameof(ErrorMessages));
            }
        }

        
        private void ValidateProperty(string propertyName, string value)
        {
            ClearErrors(propertyName);
            ClearErrors(nameof(ErrorConnexion));
            if (string.IsNullOrWhiteSpace(value))
                AddError(propertyName, $"{propertyName} ne peut pas être vide.");
            OnPropertyChanged(nameof(ErrorMessages));
        }
    }
}