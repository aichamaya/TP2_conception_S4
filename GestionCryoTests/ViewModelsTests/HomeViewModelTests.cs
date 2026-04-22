using Moq;
using Xunit;
using System.Collections.ObjectModel;
using GestionCryo.Model;
using GestionCryo.Utils.Services;
using GestionCryo.Utils.Services.Interfaces;
using GestionCryo.ViewModel;

namespace GestionCryoTests.ViewModelTests
{
    public class HomeViewModelTests
    {
        private readonly Mock<INavigationService> _navigationServiceMock;
        private readonly AfficheService _afficheService;
        private readonly HomeViewModel _viewModel;

        public HomeViewModelTests()
        {
            _navigationServiceMock = new Mock<INavigationService>();
            _afficheService = new AfficheService();

            _viewModel = new HomeViewModel(
                _navigationServiceMock.Object,
                _afficheService
            );
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
    }
}