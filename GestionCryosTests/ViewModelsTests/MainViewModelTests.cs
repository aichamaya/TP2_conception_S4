using Moq;
using Xunit;
using GestionCryo.Model;
using GestionCryo.Model.Interfaces;
using GestionCryo.Utils.Services;
using GestionCryo.Utils.Services.Interfaces;
using GestionCryo.ViewModel;

namespace GestionCryoTests.ViewModelTests
{
    public class MainViewModelTests
    {
        private readonly Mock<INavigationService> _navigationServiceMock;
        private readonly Mock<IStationDAL> _stationDALMock;
        private readonly AfficheService _afficheService;
        private readonly ConnectedUserService _connectedUserService;
        private readonly MainViewModel _viewModel;

        public MainViewModelTests()
        {
            _navigationServiceMock = new Mock<INavigationService>();
            _stationDALMock = new Mock<IStationDAL>();
            _afficheService = new AfficheService();
            _connectedUserService = new ConnectedUserService();

            _viewModel = new MainViewModel(
                _navigationServiceMock.Object,
                _connectedUserService,
                _afficheService,
                _stationDALMock.Object
            );
        }

        [Fact]
        public void CanGoHome_ShouldReturnFalse_WhenNoUser()
        {
            _connectedUserService.ConnectedUser = null;
            bool result = _viewModel.RetourAccueilCommande.CanExecute(null);
            Assert.False(result);
        }

        [Fact]
        public void CanGoHome_ShouldReturnTrue_WhenUserExists()
        {
            _connectedUserService.ConnectedUser = new Utilisateur();
            bool result = _viewModel.RetourAccueilCommande.CanExecute(null);
            Assert.True(result);
        }

        [Fact]
        public void GoHome_ShouldNavigateToAdmin_WhenUserIsAdmin()
        {
            _connectedUserService.ConnectedUser = new Utilisateur { Role = Role.Admin };
            _viewModel.RetourAccueilCommande.Execute(null);
            _navigationServiceMock.Verify(
                n => n.NavigateTo<AdminHomeViewModel>(),
                Times.Once);
        }

        [Fact]
        public void GoHome_ShouldNavigateToHome_WhenUserIsNotAdmin()
        {
            Utilisateur user = new Utilisateur { Role = Role.Employe, StationId = 1 };
            _connectedUserService.ConnectedUser = user;
            _stationDALMock
                .Setup(s => s.ObtenirStationParId(1))
                .Returns(new Station());
            _viewModel.RetourAccueilCommande.Execute(null);
            _navigationServiceMock.Verify(
                n => n.NavigateTo<HomeViewModel>(),
                Times.Once);
        }

        [Fact]
        public void CanDisconnect_ShouldReturnFalse_WhenNoUser()
        {
            _connectedUserService.ConnectedUser = null;
            bool result = _viewModel.SeDeconnecterCommande.CanExecute(null);
            Assert.False(result);
        }

        [Fact]
        public void CanDisconnect_ShouldReturnTrue_WhenUserExists()
        {
            _connectedUserService.ConnectedUser = new Utilisateur();
            bool result = _viewModel.SeDeconnecterCommande.CanExecute(null);
            Assert.True(result);
        }

        [Fact]
        public void Deconnexion_ShouldClearUser_AndNavigate()
        {
            _connectedUserService.ConnectedUser = new Utilisateur();
            _viewModel.SeDeconnecterCommande.Execute(null);
            Assert.Null(_connectedUserService.ConnectedUser);
            _navigationServiceMock.Verify(
                n => n.NavigateTo<ConnectUserViewModel>(),
                Times.Exactly(2)); 
        }

        [Fact]
        public void CanConnect_ShouldReturnTrue_WhenNoUser()
        {
            _connectedUserService.ConnectedUser = null;
            bool result = _viewModel.PageConnexionCommande.CanExecute(null);
            Assert.True(result);
        }

        [Fact]
        public void CanConnect_ShouldReturnFalse_WhenUserExists()
        {
            _connectedUserService.ConnectedUser = new Utilisateur();
            bool result = _viewModel.PageConnexionCommande.CanExecute(null);
            Assert.False(result);
        }
    }
}