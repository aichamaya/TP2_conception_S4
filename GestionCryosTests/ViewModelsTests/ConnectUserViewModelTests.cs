using Moq;
using Xunit;
using GestionCryo.Model;
using GestionCryo.Model.Interfaces;
using GestionCryo.Utils.Services.Interfaces;
using GestionCryo.ViewModel;

namespace GestionCryoTests.ViewModelTests
{
    public class ConnectUserViewModelTests
    {
        private readonly Mock<IUtilisateurDAL> _userDALMock;
        private readonly Mock<IConnectedUserService> _connectedUserMock;
        private readonly Mock<INavigationService> _navigationMock;
        private readonly Mock<IAfficheService> _afficheMock;
        private readonly Mock<IStationDAL> _stationMock;
        private readonly ConnectUserViewModel _vm;

        public ConnectUserViewModelTests()
        {
            _userDALMock = new Mock<IUtilisateurDAL>();
            _connectedUserMock = new Mock<IConnectedUserService>();
            _navigationMock = new Mock<INavigationService>();
            _afficheMock = new Mock<IAfficheService>();
            _stationMock = new Mock<IStationDAL>();

            _vm = new ConnectUserViewModel(
                _userDALMock.Object,
                _connectedUserMock.Object,
                _navigationMock.Object,
                _afficheMock.Object,
                _stationMock.Object
            );
        }

        [Fact]
        public void ConnectCommand_CanExecute_ShouldReturnFalse_WhenFieldsEmpty()
        {
            _vm.Username = "";
            _vm.Password = "";
            bool result = _vm.ConnectCommand.CanExecute(null);
            Assert.False(result);
        }

        [Fact]
        public void ConnectCommand_CanExecute_ShouldReturnTrue_WhenFieldsFilled()
        {
            _vm.Username = "test";
            _vm.Password = "test";
            bool result = _vm.ConnectCommand.CanExecute(null);
            Assert.True(result);
        }

        [Fact]
        public void ConnectCommand_Execute_ShouldNavigateToAdminHomeViewModel_WhenUserIsAdmin()
        {
            Utilisateur user = new Utilisateur
            {
                Username = "admin",
                Role = Role.Admin
            };
            _userDALMock
                .Setup(u => u.TrouverParNomEtMotDePasse("admin", "1234"))
                .Returns(user);
            _vm.Username = "admin";
            _vm.Password = "1234";
            _vm.ConnectCommand.Execute(null);
            _navigationMock.Verify(n => n.NavigateTo<AdminHomeViewModel>(), Times.Once);
        }

        [Fact]
        public void ConnectCommand_Execute_ShouldNavigateToHomeViewModel_WhenUserIsNotAdmin()
        {
            Utilisateur user = new Utilisateur
            {
                Username = "user",
                Role = Role.Employe,
                StationId = 1
            };
            Station station = new Station { Id = 1 };
            _userDALMock
                .Setup(u => u.TrouverParNomEtMotDePasse("user", "1234"))
                .Returns(user);
            _stationMock
                .Setup(s => s.ObtenirStationParId(1))
                .Returns(station);
            _vm.Username = "user";
            _vm.Password = "1234";
            _vm.ConnectCommand.Execute(null);
            _navigationMock.Verify(n => n.NavigateTo<HomeViewModel>(), Times.Once);
        }

        [Fact]
        public void ConnectCommand_Execute_ShouldSetConnectedUser_WhenUserExists()
        {
            Utilisateur user = new Utilisateur
            {
                Username = "user",
                Role = Role.Admin
            };
            _userDALMock
                .Setup(u => u.TrouverParNomEtMotDePasse("user", "1234"))
                .Returns(user);
            _vm.Username = "user";
            _vm.Password = "1234";
            _vm.ConnectCommand.Execute(null);
            _connectedUserMock.VerifySet(c => c.ConnectedUser = user, Times.Once);
        }
    }
}