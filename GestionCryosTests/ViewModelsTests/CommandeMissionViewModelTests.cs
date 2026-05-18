using GestionCryo.Model;
using GestionCryo.Model.Interfaces;
using GestionCryo.Utils.Services.Interfaces;
using GestionCryo.ViewModel;
using Moq;
using System.Collections.ObjectModel;
using Xunit;

namespace GestionCryoTests.ViewModelTests
{
    public class CommandeMissionViewModelTests
    {
        private readonly Mock<IMissionDAL> _missionDALMock;
        private readonly Mock<IStationDAL> _stationDALMock;
        private readonly Mock<IBateauDAL> _bateauDALMock;
        private readonly Mock<INavigationService> _navigationServiceMock;

        private readonly CommandeMissionViewModel _viewModel;

        public CommandeMissionViewModelTests()
        {
            _missionDALMock = new Mock<IMissionDAL>();
            _stationDALMock = new Mock<IStationDAL>();
            _bateauDALMock = new Mock<IBateauDAL>();
            _navigationServiceMock = new Mock<INavigationService>();

            ObservableCollection<Bateau> bateauxStation1 =
            [
                new Bateau
                {
                    Id = 1,
                    Nom = "Bateau 1"
                }
            ];

            ObservableCollection<Bateau> bateauxStation2 =
            [
                new Bateau
                {
                    Id = 2,
                    Nom = "Bateau 2"
                }
            ];

            List<Station> stations =
            [
                new Station
                {
                    Id = 1,
                    Nom = "Station 1",
                    Bateaux = bateauxStation1
                },

                new Station
                {
                    Id = 2,
                    Nom = "Station 2",
                    Bateaux = bateauxStation2
                }
            ];

            _stationDALMock
                .Setup(s => s.ObtenirToutesLesStations())
                .Returns(stations);

            _viewModel = new CommandeMissionViewModel(
                _missionDALMock.Object,
                _stationDALMock.Object,
                _bateauDALMock.Object,
                _navigationServiceMock.Object
            );
        }

        [Fact]
        public void Constructor_ShouldLoadStations()
        {
            Assert.Equal(2, _viewModel.Stations.Count);
        }

        [Fact]
        public void Constructor_ShouldLoadBateaux()
        {
            Assert.Equal(2, _viewModel.Bateaux.Count);
        }

        [Fact]
        public void CreerMissionCommand_CanExecute_ShouldReturnFalse_WhenNothingIsSelected()
        {
            bool result = _viewModel.CreerMissionCommand.CanExecute(null);

            Assert.False(result);
        }

        [Fact]
        public void CreerMissionCommand_CanExecute_ShouldReturnFalse_WhenCargoIsEmpty()
        {
            _viewModel.StationDepartSelectionnee = new Station { Id = 1 };
            _viewModel.StationArriveeSelectionnee = new Station { Id = 2 };
            _viewModel.BateauSelectionne = new Bateau { Id = 1 };
            _viewModel.Cargo = "";

            bool result = _viewModel.CreerMissionCommand.CanExecute(null);

            Assert.False(result);
        }

        [Fact]
        public void CreerMissionCommand_CanExecute_ShouldReturnTrue_WhenDataIsValid()
        {
            _viewModel.StationDepartSelectionnee = new Station { Id = 1 };
            _viewModel.StationArriveeSelectionnee = new Station { Id = 2 };
            _viewModel.BateauSelectionne = new Bateau { Id = 1 };
            _viewModel.Cargo = "Materiel scientifique";

            bool result = _viewModel.CreerMissionCommand.CanExecute(null);

            Assert.True(result);
        }

        [Fact]
        public void CreerMissionCommand_Execute_ShouldAddMission()
        {
            _viewModel.StationDepartSelectionnee = new Station { Id = 1 };
            _viewModel.StationArriveeSelectionnee = new Station { Id = 2 };
            _viewModel.BateauSelectionne = new Bateau { Id = 1 };
            _viewModel.Cargo = "Materiel scientifique";

            _viewModel.CreerMissionCommand.Execute(null);

            _missionDALMock.Verify(
                dal => dal.AjouterMission(It.Is<Mission>(mission =>
                    mission.StationDepartId == 1 &&
                    mission.StationArriveeId == 2 &&
                    mission.BateauId == 1 &&
                    mission.Cargo == "Materiel scientifique"
                )),
                Times.Once
            );
        }

        [Fact]
        public void CreerMissionCommand_Execute_ShouldResetFields()
        {
            _viewModel.StationDepartSelectionnee = new Station { Id = 1 };
            _viewModel.StationArriveeSelectionnee = new Station { Id = 2 };
            _viewModel.BateauSelectionne = new Bateau { Id = 1 };
            _viewModel.Cargo = "Materiel scientifique";

            _viewModel.CreerMissionCommand.Execute(null);

            Assert.Null(_viewModel.StationDepartSelectionnee);
            Assert.Null(_viewModel.StationArriveeSelectionnee);
            Assert.Null(_viewModel.BateauSelectionne);
            Assert.Equal(string.Empty, _viewModel.Cargo);
        }

        [Fact]
        public void CreerMissionCommand_Execute_ShouldNotAddMission_WhenDataIsInvalid()
        {
            _viewModel.StationDepartSelectionnee = new Station { Id = 1 };
            _viewModel.StationArriveeSelectionnee = null;
            _viewModel.BateauSelectionne = new Bateau { Id = 1 };
            _viewModel.Cargo = "Materiel scientifique";

            _viewModel.CreerMissionCommand.Execute(null);

            _missionDALMock.Verify(
                dal => dal.AjouterMission(It.IsAny<Mission>()),
                Times.Never
            );
        }

        [Fact]
        public void RetourCommand_Execute_ShouldNavigateToAdminHomeViewModel()
        {
            _viewModel.RetourCommand.Execute(null);

            _navigationServiceMock.Verify(
                nav => nav.NavigateTo<AdminHomeViewModel>(),
                Times.Once
            );
        }
    }
}