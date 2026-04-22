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
    public class BateauViewModelTests
    {
        private readonly Mock<IBateauDalService> _bateauDalServiceMock;
        private readonly Mock<IMaterielDalService> _materielDalServiceMock;
        private readonly AfficheService _afficheService;
        private readonly BateauViewModel _viewModel;

        public BateauViewModelTests()
        {
            _bateauDalServiceMock = new Mock<IBateauDalService>();
            _materielDalServiceMock = new Mock<IMaterielDalService>();

            _afficheService = new AfficheService();
            _afficheService.BateauAffiche = new Bateau
            {
                Id = 1,
                Nom = "Bateau test",
                Etat = EtatBateau.Pret
            };

            _viewModel = new BateauViewModel(
                _bateauDalServiceMock.Object,
                _afficheService,
                _materielDalServiceMock.Object
            );
        }

        [Fact]
        public void Constructor_ShouldInitializeEtatFromBateauAffiche()
        {
            Assert.Equal("Pret", _viewModel.Etat);
        }

        [Fact]
        public void Inventaire_ShouldReturnMaterielFromService()
        {
            var fakeMateriels = new List<Materiel>
            {
                new Materiel { Id = 1, Nom = "Oxygene" },
                new Materiel { Id = 2, Nom = "Nourriture" }
            };

            _materielDalServiceMock
                .Setup(m => m.ObtenirMaterielParBateauId(1))
                .Returns(fakeMateriels);

            ObservableCollection<Materiel> result = _viewModel.Inventaire;

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void ModifierEtatCommand_CanExecute_ShouldReturnFalse_WhenEtatIsEmpty()
        {
            _viewModel.Etat = "";

            var result = _viewModel.ModifierEtatCommand.CanExecute(null);

            Assert.False(result);
        }

        [Fact]
        public void ModifierEtatCommand_CanExecute_ShouldReturnTrue_WhenEtatIsFilled()
        {
            _viewModel.Etat = "EnMaintenance";

            var result = _viewModel.ModifierEtatCommand.CanExecute(null);

            Assert.True(result);
        }

        [Fact]
        public void ModifierEtatCommand_Execute_ShouldCallModifierEtat_WhenEtatIsValid()
        {
            _viewModel.Etat = "EnMaintenance";

            _viewModel.ModifierEtatCommand.Execute(null);

            _bateauDalServiceMock.Verify(
                b => b.ModifierEtat(_afficheService.BateauAffiche, EtatBateau.EnMaintenance),
                Times.Once);
        }

        [Fact]
        public void ModifierEtatCommand_Execute_ShouldAddError_WhenEtatIsInvalid()
        {
            _viewModel.Etat = "kvublloi";

            _viewModel.ModifierEtatCommand.Execute(null);

            Assert.True(_viewModel.HasErrors);
        }
    }
}