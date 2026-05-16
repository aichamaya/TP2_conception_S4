using Moq;
using Xunit;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GestionCryo.Model;
using GestionCryo.Model.Interfaces;
using GestionCryo.Utils.Services;
using GestionCryo.Utils.Services.Interfaces;
using GestionCryo.ViewModel;

namespace GestionCryoTests.ViewModelTests
{
    public class BateauViewModelTests
    {
        private readonly Mock<IBateauDAL> _bateauDALMock;
        private readonly Mock<IMaterielDAL> _materielDALMock;
        private readonly Mock<INavigationService> _navigationServiceMock;
        private readonly AfficheService _afficheService;
        private readonly BateauViewModel _viewModel;

        public BateauViewModelTests()
        {
            _bateauDALMock = new Mock<IBateauDAL>();
            _materielDALMock = new Mock<IMaterielDAL>();
            _navigationServiceMock = new Mock<INavigationService>();
            _afficheService = new AfficheService();
            _afficheService.BateauAffiche = new Bateau
            {
                Id = 1,
                Nom = "Bateau test",
                Etat = EtatBateau.Pret
            };

            _viewModel = new BateauViewModel(
                _bateauDALMock.Object,
                _afficheService,
                _materielDALMock.Object,
                _navigationServiceMock.Object
            );
        }

        [Fact]
        public void Constructor_ShouldInitializeEtatFromBateauAffiche()
        {
            Assert.Equal(EtatBateau.Pret, _viewModel.EtatSelectionne);
        }

        [Fact]
        public void Inventaire_ShouldReturnMaterielFromDAL()
        {
            List<Materiel> fakeMateriels = new List<Materiel>
            {
                new Materiel { Id = 1, Nom = "Oxygene" },
                new Materiel { Id = 2, Nom = "Nourriture" }
            };
            _materielDALMock
                .Setup(m => m.ObtenirMaterielParBateauId(1))
                .Returns(fakeMateriels);

            ObservableCollection<Materiel> result = _viewModel.Inventaire;

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void ModifierEtatCommand_CanExecute_ShouldReturnFalse_WhenBateauEnMission()
        {
            _afficheService.BateauAffiche.Etat = EtatBateau.EnMission;
            bool result = _viewModel.ModifierEtatCommand.CanExecute(null);
            Assert.False(result);
        }

        [Fact]
        public void ModifierEtatCommand_CanExecute_ShouldReturnTrue_WhenBateauArrime()
        {
            _afficheService.BateauAffiche.Etat = EtatBateau.Arrime;
            bool result = _viewModel.ModifierEtatCommand.CanExecute(null);
            Assert.True(result);
        }

        [Fact]
        public void ModifierEtatCommand_Execute_ShouldCallModifierEtat()
        {
            _viewModel.EtatSelectionne = EtatBateau.EnMaintenance;
            _viewModel.ModifierEtatCommand.Execute(null);
            _bateauDALMock.Verify(
                b => b.ModifierEtat(_afficheService.BateauAffiche, EtatBateau.EnMaintenance),
                Times.Once);
        }
        [Fact]
        public void CommanderPiecesCommand_CanExecute_ShouldReturnFalse_WhenBateauPret()
        {
            _afficheService.BateauAffiche.Etat = EtatBateau.Pret;
            bool result = _viewModel.CommanderPiecesCommand.CanExecute(null);
            Assert.False(result);
        }

        [Fact]
        public void CommanderPiecesCommand_CanExecute_ShouldReturnTrue_WhenBateauInspection()
        {
            _afficheService.BateauAffiche.Etat = EtatBateau.Inspection;
            bool result = _viewModel.CommanderPiecesCommand.CanExecute(null);
            Assert.True(result);
        }
    }
}
