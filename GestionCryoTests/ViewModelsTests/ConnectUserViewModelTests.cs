using Moq;
using Xunit;
using GestionCryo.Model;
using GestionCryo.Utils.Services.Interfaces;
using GestionCryo.ViewModel;

namespace GestionCryoTests.ViewModelTests
{
    public class ConnectUserViewModelTests
    {
        private readonly Mock<IUtilisateurDalService> _userDalMock;
        private readonly Mock<IConnectedUserService> _connectedUserMock;
        private readonly Mock<INavigationService> _navigationMock;
        private readonly Mock<IAfficheService> _afficheMock;
        private readonly Mock<IStationDalService> _stationMock;
        private readonly ConnectUserViewModel _vm;

        public ConnectUserViewModelTests()
        {
            _userDalMock = new Mock<IUtilisateurDalService>();
            _connectedUserMock = new Mock<IConnectedUserService>();
            _navigationMock = new Mock<INavigationService>();
            _afficheMock = new Mock<IAfficheService>();
            _stationMock = new Mock<IStationDalService>();

            _vm = new ConnectUserViewModel(
                _userDalMock.Object,
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

            var result = _vm.ConnectCommand.CanExecute(null);

            Assert.False(result);
        }

        [Fact]
        public void ConnectCommand_CanExecute_ShouldReturnTrue_WhenFieldsFilled()
        {
            _vm.Username = "test";
            _vm.Password = "test";

            var result = _vm.ConnectCommand.CanExecute(null);

            Assert.True(result);
        }

        [Fact]
        public void ConnectCommand_Execute_ShouldNavigateToAdminHomeViewModel_WhenUserIsAdmin()
        {
            var user = new Utilisateur
            {
                Username = "admin",
                Role = Role.Admin
            };

            _userDalMock
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
            var user = new Utilisateur
            {
                Username = "user",
                Role = Role.Employe,
                StationId = 1
            };

            var station = new Station { Id = 1 };

            _userDalMock
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
            var user = new Utilisateur
            {
                Username = "user",
                Role = Role.Admin
            };

            _userDalMock
                .Setup(u => u.TrouverParNomEtMotDePasse("user", "1234"))
                .Returns(user);

            _vm.Username = "user";
            _vm.Password = "1234";

            _vm.ConnectCommand.Execute(null);

            _connectedUserMock.VerifySet(c => c.ConnectedUser = user, Times.Once);
        }
    }
}