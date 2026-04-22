using Moq;
using Xunit;
using GestionCryo.Model;
using GestionCryo.Model.Interfaces;
using GestionCryo.Utils.Services;

namespace GestionCryoTests.ServicesTests
{
    public class StationDalServiceTests
    {
        private readonly Mock<IStationDAL> _stationDalMock;
        private readonly StationDalService _service;

        public StationDalServiceTests()
        {
            _stationDalMock = new Mock<IStationDAL>();
            _service = new StationDalService(_stationDalMock.Object);
        }

        [Fact]
        public void ObtenirStationParId_ShouldReturnStationFromDal()
        {
            var fakeStation = new Station();

            _stationDalMock
                .Setup(s => s.ObtenirStationParId(1))
                .Returns(fakeStation);

            var result = _service.ObtenirStationParId(1);

            Assert.Equal(fakeStation, result);
        }

        [Fact]
        public void ObtenirStationParId_ShouldCallDalOnce()
        {
            _service.ObtenirStationParId(1);

            _stationDalMock.Verify(s => s.ObtenirStationParId(1), Times.Once);
        }

        [Fact]
        public void ObtenirToutesLesStations_ShouldCallDalOnce()
        {
            _service.ObtenirToutesLesStations();

            _stationDalMock.Verify(s => s.ObtenirToutesLesStations(), Times.Once);
        }
    }
}