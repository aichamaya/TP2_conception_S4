using GestionCryo.Model;
using GestionCryo.Model.Interfaces;
using GestionCryo.Utils;
using GestionCryo.Utils.Commands;
using GestionCryo.Utils.Services;
using GestionCryo.Utils.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GestionCryo.ViewModel
{
    public class CommandeMissionViewModel : BaseViewModel
    {
        private readonly IMissionDAL _missionDAL;
        private readonly IStationDAL _stationDALService;
        private readonly IBateauDAL _bateauDALService;
        private readonly INavigationService _navigationService;

        public ObservableCollection<Station> Stations { get; }
        public ObservableCollection<Bateau> Bateaux { get; }

        private Station? _stationDepartSelectionnee;
        public Station? StationDepartSelectionnee
        {
            get => _stationDepartSelectionnee;
            set
            {
                _stationDepartSelectionnee = value;
                OnPropertyChanged(nameof(StationDepartSelectionnee));
            }
        }

        private Station? _stationArriveeSelectionnee;
        public Station? StationArriveeSelectionnee
        {
            get => _stationArriveeSelectionnee;
            set
            {
                _stationArriveeSelectionnee = value;
                OnPropertyChanged(nameof(StationArriveeSelectionnee));
            }
        }

        private Bateau? _bateauSelectionne;
        public Bateau? BateauSelectionne
        {
            get => _bateauSelectionne;
            set
            {
                _bateauSelectionne = value;
                OnPropertyChanged(nameof(BateauSelectionne));
            }
        }

        private string _cargo = string.Empty;
        public string Cargo
        {
            get => _cargo;
            set
            {
                if (_cargo != value)
                {
                    _cargo = value;
                    OnPropertyChanged(nameof(Cargo));
                    ValidateProperty(nameof(Cargo), value);
                }
            }
        }

        public ICommand CreerMissionCommand { get; set; }
        public ICommand RetourCommand { get; set; }

        public CommandeMissionViewModel(
            IMissionDAL missionDAL,
            IStationDAL stationDALService,
            IBateauDAL bateauDALService,
            INavigationService navigationService)
        {
            _missionDAL = missionDAL;
            _stationDALService = stationDALService;
            _bateauDALService = bateauDALService;
            _navigationService = navigationService;

            Stations = new ObservableCollection<Station>(_stationDALService.ObtenirToutesLesStations());
            Bateaux = new ObservableCollection<Bateau>();

            foreach (Station station in Stations)
            {
                foreach (Bateau bateau in station.Bateaux)
                {
                    Bateaux.Add(bateau);
                }
            }

            CreerMissionCommand = new RelayCommand(CreerMission, CanCreerMission);
            RetourCommand = new RelayCommand(Retour);
        }

        private void CreerMission()
        {
            if (StationDepartSelectionnee == null || StationArriveeSelectionnee == null || BateauSelectionne == null)
                return;

            Mission mission = new Mission
            {
                StationDepartId = StationDepartSelectionnee.Id,
                StationArriveeId = StationArriveeSelectionnee.Id,
                BateauId = BateauSelectionne.Id,
                Cargo = Cargo
            };

            _missionDAL.AjouterMission(mission);

            _cargo = string.Empty;
            StationDepartSelectionnee = null;
            StationArriveeSelectionnee = null;
            BateauSelectionne = null;
            OnPropertyChanged(nameof(StationArriveeSelectionnee));
            OnPropertyChanged(nameof(BateauSelectionne));
            OnPropertyChanged(nameof(Cargo));
        }

        private bool CanCreerMission()
        {
            return StationDepartSelectionnee != null
                && StationArriveeSelectionnee != null
                && BateauSelectionne != null
                && !string.IsNullOrWhiteSpace(Cargo)
                && !HasErrors;
        }

        private void Retour()
        {
            _navigationService.NavigateTo<AdminHomeViewModel>();
        }

        private void ValidateProperty(string propertyName, string value)
        {
            ClearErrors(propertyName);

            if (string.IsNullOrWhiteSpace(value))
            {
                AddError(propertyName, $"{propertyName} ne peut pas être vide.");
            }

            OnPropertyChanged(nameof(ErrorMessages));
        }
    }
}