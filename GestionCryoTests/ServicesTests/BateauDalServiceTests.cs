using Moq;
using Xunit;
using GestionCryo.Model;
using GestionCryo.Model.Interfaces;
using GestionCryo.Utils.Services;

namespace GestionCryoTests.ServicesTests
{
    public class BateauDalServiceTests
    {
        private readonly Mock<IBateauDAL> _bateauDalMock;
        private readonly BateauDalService _service;

        public BateauDalServiceTests()
        {
            _bateauDalMock = new Mock<IBateauDAL>();
            _service = new BateauDalService(_bateauDalMock.Object);
        }

        [Fact]
        public void ModifierEtat_ShouldCallDalOnce()
        {
            var bateau = new Bateau();

            _service.ModifierEtat(bateau, EtatBateau.EnMaintenance);

            _bateauDalMock.Verify(
                b => b.ModifierEtat(bateau, EtatBateau.EnMaintenance),
                Times.Once);
        }
    }
}