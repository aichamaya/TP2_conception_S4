using GestionCryo.Model;
using GestionCryo.Model.Interfaces;
using GestionCryo.Utils.Services;
using Moq;
using Xunit;

namespace GestionCryoTests.ServicesTests
{
    public class AlerteServiceTests
    {
        private readonly Mock<IStationDAL> _stationDALMock;
        private readonly AlerteService _alerteService;

        public AlerteServiceTests()
        {
            _stationDALMock = new Mock<IStationDAL>();
            _alerteService = new AlerteService(_stationDALMock.Object);
        }

        [Fact]
        public void ObtenirAlertes_ShouldReturnEmpty_WhenStationDoesNotExist()
        {
            _stationDALMock
                .Setup(s => s.ObtenirStationParId(1))
                .Returns((Station?)null);

            List<Alerte> result = _alerteService.ObtenirAlertes(1);

            Assert.Empty(result);
        }

        [Fact]
        public void ObtenirAlertes_ShouldAddInspectionAlert_WhenBateauIsInInspection()
        {
            Station station = new Station
            {
                Id = 1,
                Bateaux = new List<Bateau>
                {
                    new Bateau
                    {
                        Id = 1,
                        Nom = "Bateau test",
                        Etat = EtatBateau.Inspection
                    }
                }
            };

            _stationDALMock
                .Setup(s => s.ObtenirStationParId(1))
                .Returns(station);

            _stationDALMock
                .Setup(s => s.ObtenirMissionsParStationId(1))
                .Returns(new List<Mission>());

            List<Alerte> result = _alerteService.ObtenirAlertes(1);

            Assert.Single(result);
            Assert.Equal("Inspection", result[0].Type);
            Assert.Equal("Bateau test", result[0].NomBateau);
        }

        [Fact]
        public void ObtenirAlertes_ShouldAddChargeAlert_WhenMoreThanTwoMissions()
        {
            Station station = new Station { Id = 1 };

            List<Mission> missions = new List<Mission>
            {
                new Mission(),
                new Mission(),
                new Mission()
            };

            _stationDALMock
                .Setup(s => s.ObtenirStationParId(1))
                .Returns(station);

            _stationDALMock
                .Setup(s => s.ObtenirMissionsParStationId(1))
                .Returns(missions);

            List<Alerte> result = _alerteService.ObtenirAlertes(1);

            Assert.Single(result);
            Assert.Equal("Charge élevée", result[0].Type);
        }

        [Fact]
        public void ObtenirAlertes_ShouldAddAchalandageAlert_WhenMoreThanFiveBateaux()
        {
            Station station = new Station
            {
                Id = 1,
                Bateaux = new List<Bateau>
                {
                    new Bateau(),
                    new Bateau(),
                    new Bateau(),
                    new Bateau(),
                    new Bateau(),
                    new Bateau()
                }
            };

            _stationDALMock
                .Setup(s => s.ObtenirStationParId(1))
                .Returns(station);

            _stationDALMock
                .Setup(s => s.ObtenirMissionsParStationId(1))
                .Returns(new List<Mission>());

            List<Alerte> result = _alerteService.ObtenirAlertes(1);

            Assert.Single(result);
            Assert.Equal("Achalandage", result[0].Type);
        }

        [Fact]
        public void ObtenirAlertes_ShouldReturnThreeAlertes_WhenAllConditionsAreTrue()
        {
            Station station = new Station
            {
                Id = 1,
                Bateaux = new List<Bateau>
                {
                    new Bateau { Nom = "Bateau 1", Etat = EtatBateau.Inspection },
                    new Bateau(),
                    new Bateau(),
                    new Bateau(),
                    new Bateau(),
                    new Bateau()
                }
            };

            List<Mission> missions = new List<Mission>
            {
                new Mission(),
                new Mission(),
                new Mission()
            };

            _stationDALMock
                .Setup(s => s.ObtenirStationParId(1))
                .Returns(station);

            _stationDALMock
                .Setup(s => s.ObtenirMissionsParStationId(1))
                .Returns(missions);

            List<Alerte> result = _alerteService.ObtenirAlertes(1);

            Assert.Equal(3, result.Count);
        }
    }
}