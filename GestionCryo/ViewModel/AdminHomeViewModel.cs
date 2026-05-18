using GestionCryo.Model;
using GestionCryo.Model.Interfaces;
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
        private readonly IStationDAL _stationDAL;
        private readonly IAfficheService _afficheService;

        private ObservableCollection<Station> _stations;
        public ObservableCollection<Station> Stations
        {
            get => _stations;
        }

        private Station? _selectedStation;
        public Station? SelectedStation
        {
            get => _selectedStation;
            set
            {
                if (_selectedStation != value)
                {
                    _selectedStation = value;
                    OnPropertyChanged();
                }
            }
        }
        public ICommand CreerMissionCommand { get; set; }
        public void CreerMission()
        {
            _navigationService.NavigateTo<CommandeMissionViewModel>();
        }

        public AdminHomeViewModel(INavigationService navigationService, IAfficheService afficheService, IStationDAL stationDAL)
        {
            _navigationService = navigationService;
            _stationDAL = stationDAL;
            _afficheService = afficheService;
            _stations = new ObservableCollection<Station>(_stationDAL.ObtenirToutesLesStations());
            VoirDetailsStationCommand = new RelayCommand(VoirDetailsStation, CanVoirDetailsStation);
            CreerMissionCommand = new RelayCommand(CreerMission);
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
    }
}