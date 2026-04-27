using Moq;
using Xunit;
using System.Collections.Generic;
using GestionCryo.Model;
using GestionCryo.Model.Interfaces;
using GestionCryo.Utils.Services;
using GestionCryo.Utils.Services.Interfaces;
using GestionCryo.ViewModel;

namespace GestionCryoTests.ViewModelTests
{
    public class AdminHomeViewModelTests
    {
        private readonly Mock<INavigationService> _navigationServiceMock;
        private readonly Mock<IStationDAL> _stationDALMock;
        private readonly AfficheService _afficheService;
        private readonly AdminHomeViewModel _viewModel;

        public AdminHomeViewModelTests()
        {
            _navigationServiceMock = new Mock<INavigationService>();
            _stationDALMock = new Mock<IStationDAL>();
            _afficheService = new AfficheService();

            List<Station> stations = new List<Station>
            {
                new Station { Id = 1, Nom = "Station 1" },
                new Station { Id = 2, Nom = "Station 2" }
            };

            _stationDALMock
                .Setup(s => s.ObtenirToutesLesStations())
                .Returns(stations);

            _viewModel = new AdminHomeViewModel(
                _navigationServiceMock.Object,
                _afficheService,
                _stationDALMock.Object
            );
        }

        [Fact]
        public void Constructor_ShouldLoadStations()
        {
            Assert.Equal(2, _viewModel.Stations.Count);
        }

        [Fact]
        public void CanVoirDetailsStation_ShouldReturnFalse_WhenNoSelection()
        {
            _viewModel.SelectedStation = null;
            bool result = _viewModel.VoirDetailsStationCommand.CanExecute(null);
            Assert.False(result);
        }

        [Fact]
        public void CanVoirDetailsStation_ShouldReturnTrue_WhenStationSelected()
        {
            _viewModel.SelectedStation = new Station();
            bool result = _viewModel.VoirDetailsStationCommand.CanExecute(null);
            Assert.True(result);
        }

        [Fact]
        public void VoirDetailsStation_ShouldSetAfficheService_AndNavigate()
        {
            Station station = new Station { Id = 1, Nom = "Station test" };
            _viewModel.SelectedStation = station;
            _viewModel.VoirDetailsStationCommand.Execute(null);
            Assert.Equal(station, _afficheService.StationAffichee);
            _navigationServiceMock.Verify(
                n => n.NavigateTo<HomeViewModel>(),
                Times.Once);
        }
    }
}