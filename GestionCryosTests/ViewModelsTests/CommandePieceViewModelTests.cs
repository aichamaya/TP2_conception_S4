using GestionCryo.Model;
using GestionCryo.Model.Interfaces;
using GestionCryo.Utils.Services;
using GestionCryo.Utils.Services.Interfaces;
using GestionCryo.ViewModel;
using Moq;
using Xunit;

namespace GestionCryoTests.ViewModelTests
{
    public class CommandePieceViewModelTests
    {
        private static CommandePieceViewModel CreerViewModel(
            AfficheService afficheService,
            Mock<ICommandePieceDAL> commandePieceDALMock,
            Mock<INavigationService>? navigationServiceMock = null)
        {
            navigationServiceMock ??= new Mock<INavigationService>();
            return new CommandePieceViewModel(
                commandePieceDALMock.Object,
                afficheService,
                navigationServiceMock.Object);
        }

        [Fact]
        public void Commandes_ShouldReturnEmptyCollection_WhenNoBateauSelected()
        {
            AfficheService afficheService = new AfficheService();
            Mock<ICommandePieceDAL> commandePieceDALMock = new Mock<ICommandePieceDAL>();
            CommandePieceViewModel viewModel = CreerViewModel(afficheService, commandePieceDALMock);

            Assert.Empty(viewModel.Commandes);
            commandePieceDALMock.Verify(
                dal => dal.ObtenirCommandesParBateauId(It.IsAny<int>()),
                Times.Never);
        }

        [Fact]
        public void CommanderCommand_CanExecute_ShouldReturnFalse_WhenNoBateauSelected()
        {
            AfficheService afficheService = new AfficheService();
            Mock<ICommandePieceDAL> commandePieceDALMock = new Mock<ICommandePieceDAL>();
            CommandePieceViewModel viewModel = CreerViewModel(afficheService, commandePieceDALMock);
            viewModel.NomPiece = "Moteur";
            viewModel.Quantite = 1;

            Assert.False(viewModel.CommanderCommand.CanExecute(null));
        }

        [Fact]
        public void CommanderCommand_Execute_ShouldAddCommande_WhenDataIsValid()
        {
            AfficheService afficheService = new AfficheService
            {
                BateauAffiche = new Bateau { Id = 4, Nom = "Bateau test" }
            };
            Mock<ICommandePieceDAL> commandePieceDALMock = new Mock<ICommandePieceDAL>();
            CommandePieceViewModel viewModel = CreerViewModel(afficheService, commandePieceDALMock);
            viewModel.NomPiece = "Pompe";
            viewModel.Quantite = 2;
            viewModel.StatutSelectionne = StatutCommande.Urgente;

            viewModel.CommanderCommand.Execute(null);

            commandePieceDALMock.Verify(
                dal => dal.AjouterCommande(It.Is<CommandePiece>(commande =>
                    commande.NomPiece == "Pompe" &&
                    commande.Quantite == 2 &&
                    commande.Statut == StatutCommande.Urgente &&
                    commande.BateauId == 4)),
                Times.Once);
            Assert.Equal(string.Empty, viewModel.NomPiece);
            Assert.Equal(1, viewModel.Quantite);
        }

        [Fact]
        public void RetourCommand_Execute_ShouldNavigateToBateauViewModel()
        {
            AfficheService afficheService = new AfficheService();
            Mock<ICommandePieceDAL> commandePieceDALMock = new Mock<ICommandePieceDAL>();
            Mock<INavigationService> navigationServiceMock = new Mock<INavigationService>();
            CommandePieceViewModel viewModel = CreerViewModel(
                afficheService,
                commandePieceDALMock,
                navigationServiceMock);

            viewModel.RetourCommand.Execute(null);

            navigationServiceMock.Verify(
                navigation => navigation.NavigateTo<BateauViewModel>(),
                Times.Once);
        }
    }
}
