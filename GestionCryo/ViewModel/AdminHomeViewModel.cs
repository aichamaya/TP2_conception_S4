using GestionCryo.Model;
using GestionCryo.Utils;
using GestionCryo.Utils.Commands;
using GestionCryo.Utils.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;
namespace GestionCryo.ViewModel
{
    public class AdminHomeViewModel : BaseViewModel
    {
       private readonly INavigationService _navigationService;
        private readonly IStationDalService _stationDalService;
        private readonly IAfficheService _afficheService;

        private ObservableCollection<Station> _stations;
        public ObservableCollection<Station> Stations
        {
            get
            {
                return _stations;
            }
        }
        private Station? _selectedStation;
        public Station? SelectedStation
        {
            get
            {
                return _selectedStation;
            }
            set
            {
                if (_selectedStation != value)
                {
                    _selectedStation = value;
                    OnPropertyChanged();
                }
            }
        }
        public ICommand VoirDetailsStationCommand { get; set; }
        public void VoirDetailsStation()
        {
            _afficheService.StationAffichee = _selectedStation;
            _navigationService.NavigateTo<HomeViewModel>();

        }
        public bool CanVoirDetailsStation()
        {
            return _selectedStation != null;
        }

        public AdminHomeViewModel(INavigationService navigationService, IAfficheService afficheService, IStationDalService stationDALService)
        {
            _navigationService = navigationService;
            _stationDalService = stationDALService;
            _afficheService = afficheService;
            _stations = new ObservableCollection<Station>(_stationDalService.ObtenirToutesLesStations());
            VoirDetailsStationCommand = new RelayCommand(VoirDetailsStation, CanVoirDetailsStation);
        }
    }
}