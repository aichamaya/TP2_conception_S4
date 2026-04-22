
using GestionCryo.Model;
using GestionCryo.Model.Interfaces;
using GestionCryo.Utils;
using GestionCryo.Utils.Commands;
using GestionCryo.Utils.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GestionCryo.ViewModel
{
    public class HomeViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IAfficheService _afficheService;

       public string NomStation
        {
            get =>_afficheService.StationAffichee != null ? $"Station : {_afficheService.StationAffichee.Nom}" : "Aucune station assignée";
        }
        public string LocalisationStation
        {
            get => _afficheService.StationAffichee != null ? $"Localisation : {_afficheService.StationAffichee.Localisation}" : "Aucune station assignée";
        }
        public double ? LatitudeStation
        {
            get => _afficheService.StationAffichee != null ? _afficheService.StationAffichee.Latitude : (double?)null;
        }
        public double ? LongitudeStation
        {
            get => _afficheService.StationAffichee != null ? _afficheService.StationAffichee.Longitude : (double?)null;
        }

        public ObservableCollection<Bateau> BateauxStation
        {
            get
            {
                if (_afficheService.StationAffichee != null)
                {
                    return new ObservableCollection<Bateau>(_afficheService.StationAffichee.Bateaux);
                }
                else
                {
                    return new ObservableCollection<Bateau>();
                }
            }
        }
        private Bateau? _selectedBateau;
        public Bateau? SelectedBateau
        {
            get => _selectedBateau;
            set
            {
                if (_selectedBateau != value)
                {
                    _selectedBateau = value;
                    OnPropertyChanged(nameof(SelectedBateau));
                }
            }
        }
        public ICommand VoirDetailsBateauCommand{ get; set; }
        public void VoirDetailsBateau()
        {
            _afficheService.BateauAffiche = SelectedBateau;
            _navigationService.NavigateTo<BateauViewModel>();
        }
        public bool CanVoirDetailsBateau()
        {
            return SelectedBateau != null;
        }

        public HomeViewModel(INavigationService navigationService, IAfficheService afficheService)
        {
            _navigationService = navigationService;
            _afficheService = afficheService;
            VoirDetailsBateauCommand = new RelayCommand(VoirDetailsBateau, CanVoirDetailsBateau);
        }
    }
}