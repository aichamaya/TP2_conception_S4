using Moq;
using Xunit;
using GestionCryo.Model;
using GestionCryo.Model.Interfaces;
using GestionCryo.Utils.Services;

namespace GestionCryoTests.ServicesTests
{
    public class UtilisateurDalServiceTests
    {
        private readonly Mock<IUtilisateurDAL> _utilisateurDalMock;
        private readonly UtilisateurDalService _service;

        public UtilisateurDalServiceTests()
        {
            _utilisateurDalMock = new Mock<IUtilisateurDAL>();
            _service = new UtilisateurDalService(_utilisateurDalMock.Object);
        }

        [Fact]
        public void TrouverParNomEtMotDePasse_ShouldReturnUserFromDal()
        {
            var fakeUser = new Utilisateur { Username = "moussa", Password = "1234" };

            _utilisateurDalMock
                .Setup(u => u.TrouverParNomEtMotDePasse("moussa", "1234"))
                .Returns(fakeUser);

            var result = _service.TrouverParNomEtMotDePasse("moussa", "1234");

            Assert.Equal(fakeUser, result);
        }

        [Fact]
        public void TrouverParNomEtMotDePasse_ShouldCallDalOnce()
        {
            _service.TrouverParNomEtMotDePasse("test", "test");

            _utilisateurDalMock.Verify(
                u => u.TrouverParNomEtMotDePasse("test", "test"),
                Times.Once);
        }
    }
}