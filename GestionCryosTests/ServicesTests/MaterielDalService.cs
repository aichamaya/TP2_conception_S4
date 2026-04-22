using Moq;
using Xunit;
using System.Collections.Generic;
using GestionCryo.Model;
using GestionCryo.Model.Interfaces;
using GestionCryo.Utils.Services;

namespace GestionCryoTests.ServicesTests
{
    public class MaterielDalServiceTests
    {
        private readonly Mock<IMaterielDAL> _materielDalMock;
        private readonly MaterielDalService _service;

        public MaterielDalServiceTests()
        {
            _materielDalMock = new Mock<IMaterielDAL>();
            _service = new MaterielDalService(_materielDalMock.Object);
        }

        [Fact]
        public void ObtenirMaterielParBateauId_ShouldReturnListFromDal()
        {
            var fakeList = new List<Materiel>
            {
                new Materiel { Id = 1 },
                new Materiel { Id = 2 }
            };

            _materielDalMock
                .Setup(m => m.ObtenirMaterielParBateauId(1))
                .Returns(fakeList);

            var result = _service.ObtenirMaterielParBateauId(1);

            Assert.Equal(fakeList, result);
        }

        [Fact]
        public void ObtenirMaterielParBateauId_ShouldCallDalOnce()
        {
            _service.ObtenirMaterielParBateauId(5);

            _materielDalMock.Verify(m => m.ObtenirMaterielParBateauId(5), Times.Once);
        }
    }
}