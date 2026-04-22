using Moq;
using Xunit;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GestionCryo.Model;
using GestionCryo.Utils.Services;
using GestionCryo.Utils.Services.Interfaces;
using GestionCryo.ViewModel;

namespace GestionCryoTests.ViewModelTests
{
    public class AdminHomeViewModelTests
    {
        private readonly Mock<INavigationService> _navigationServiceMock;
        private readonly Mock<IStationDalService> _stationDalServiceMock;
        private readonly AfficheService _afficheService;
        private readonly AdminHomeViewModel _viewModel;

        public AdminHomeViewModelTests()
        {
            _navigationServiceMock = new Mock<INavigationService>();
            _stationDalServiceMock = new Mock<IStationDalService>();
            _afficheService = new AfficheService();

            var stations = new List<Station>
            {
                new Station { Id = 1, Nom = "Station 1" },
                new Station { Id = 2, Nom = "Station 2" }
            };

            _stationDalServiceMock
                .Setup(s => s.ObtenirToutesLesStations())
                .Returns(stations);

            _viewModel = new AdminHomeViewModel(
                _navigationServiceMock.Object,
                _afficheService,
                _stationDalServiceMock.Object
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

            var result = _viewModel.VoirDetailsStationCommand.CanExecute(null);

            Assert.False(result);
        }

        [Fact]
        public void CanVoirDetailsStation_ShouldReturnTrue_WhenStationSelected()
        {
            _viewModel.SelectedStation = new Station();

            var result = _viewModel.VoirDetailsStationCommand.CanExecute(null);

            Assert.True(result);
        }

        [Fact]
        public void VoirDetailsStation_ShouldSetAfficheService_AndNavigate()
        {
            var station = new Station { Id = 1, Nom = "Station test" };
            _viewModel.SelectedStation = station;

            _viewModel.VoirDetailsStationCommand.Execute(null);

            Assert.Equal(station, _afficheService.StationAffichee);

            _navigationServiceMock.Verify(
                n => n.NavigateTo<HomeViewModel>(),
                Times.Once);
        }
    }
}