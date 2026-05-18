using Moq;
using Xunit;
using System.Collections.ObjectModel;
using GestionCryo.Model;
using GestionCryo.Utils.Services;
using GestionCryo.Utils.Services.Interfaces;
using GestionCryo.ViewModel;
using GestionCryo.Model.Interfaces;

namespace GestionCryoTests.ViewModelTests
{
    public class HomeViewModelTests
    {
        private readonly Mock<INavigationService> _navigationServiceMock;
        private readonly Mock<IAlerteService> _alerteServiceMock;
        private readonly Mock<IStationDAL> _stationDal;
        private readonly AfficheService _afficheService;
        private readonly HomeViewModel _viewModel;


        public HomeViewModelTests()
        {
            _navigationServiceMock = new Mock<INavigationService>();
            _alerteServiceMock = new Mock<IAlerteService>();
            _afficheService = new AfficheService();
            _stationDal = new Mock<IStationDAL>();
            _viewModel = new HomeViewModel(
                _navigationServiceMock.Object,
                _afficheService,
                _alerteServiceMock.Object,
                _stationDal.Object
            );
        }
        [Fact]
        public void MissionsStation_ShouldReturnEmpty_WhenNoStation()
        {
            var result = _viewModel.MissionsStation;

            Assert.Empty(result);
        }

        [Fact]
        public void MissionsStation_ShouldReturnMissions_WhenStationExists()
        {
            _afficheService.StationAffichee = new Station { Id = 1, Nom = "Station Test" };

            List<Mission> missions = new List<Mission>
    {
        new Mission { Id = 1, Cargo = "Cargo 1" },
        new Mission { Id = 2, Cargo = "Cargo 2" }
    };

            _stationDal
                .Setup(s => s.ObtenirMissionsParStationId(1))
                .Returns(missions);

            var result = _viewModel.MissionsStation;

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void NomStation_ShouldReturnDefault_WhenNoStation()
        {
            var result = _viewModel.NomStation;

            Assert.Equal("Aucune station assignée", result);
        }

        [Fact]
        public void NomStation_ShouldReturnStationName()
        {
            _afficheService.StationAffichee = new Station { Nom = "Station Test" };

            var result = _viewModel.NomStation;

            Assert.Contains("Station Test", result);
        }

        [Fact]
        public void BateauxStation_ShouldReturnEmpty_WhenNoStation()
        {
            var result = _viewModel.BateauxStation;

            Assert.Empty(result);
        }

        [Fact]
        public void BateauxStation_ShouldReturnBateaux()
        {
            _afficheService.StationAffichee = new Station
            {
                Bateaux = new ObservableCollection<Bateau>
                {
                    new Bateau(),
                    new Bateau()
                }
            };

            var result = _viewModel.BateauxStation;

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void CanVoirDetailsBateau_ShouldReturnFalse_WhenNoSelection()
        {
            _viewModel.SelectedBateau = null;

            var result = _viewModel.VoirDetailsBateauCommand.CanExecute(null);

            Assert.False(result);
        }

        [Fact]
        public void CanVoirDetailsBateau_ShouldReturnTrue_WhenSelected()
        {
            _viewModel.SelectedBateau = new Bateau();

            var result = _viewModel.VoirDetailsBateauCommand.CanExecute(null);

            Assert.True(result);
        }

        [Fact]
        public void VoirDetailsBateau_ShouldSetAfficheService_AndNavigate()
        {
            var bateau = new Bateau();
            _viewModel.SelectedBateau = bateau;

            _viewModel.VoirDetailsBateauCommand.Execute(null);

            Assert.Equal(bateau, _afficheService.BateauAffiche);

            _navigationServiceMock.Verify(
                n => n.NavigateTo<BateauViewModel>(),
                Times.Once);
        }
        [Fact]
        public void Alertes_ShouldReturnEmpty_WhenNoStation()
        {
            var result = _viewModel.Alertes;

            Assert.Empty(result);
        }

        [Fact]
        public void Alertes_ShouldReturnAlertes_WhenStationExists()
        {
            _afficheService.StationAffichee = new Station { Id = 1, Nom = "Station Test" };

            List<Alerte> alertes = new List<Alerte>
    {
        new Alerte
        {
            NomBateau = "Bateau test",
            Type = "Inspection",
            Message = "Le bateau est en inspection."
        }
    };

            _alerteServiceMock
                .Setup(a => a.ObtenirAlertes(1))
                .Returns(alertes);

            var result = _viewModel.Alertes;

            Assert.Single(result);
            Assert.Equal("Inspection", result[0].Type);
        }
    }
}
