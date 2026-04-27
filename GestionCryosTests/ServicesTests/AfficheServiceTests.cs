using Xunit;
using GestionCryo.Model;
using GestionCryo.Utils.Services;

namespace GestionCryoTests.ServicesTests
{
    public class AfficheServiceTests
    {
        [Fact]
        public void StationAffichee_Set_ShouldUpdateValue()
        {
            AfficheService service = new AfficheService();
            Station station = new Station { Id = 1 };

            service.StationAffichee = station;

            Assert.Equal(station, service.StationAffichee);
        }

        [Fact]
        public void ParcoursAffiche_Set_ShouldUpdateValue()
        {
            AfficheService service = new AfficheService();
            Parcours parcours = new Parcours { Id = 1 };

            service.ParcoursAffiche = parcours;

            Assert.Equal(parcours, service.ParcoursAffiche);
        }

        [Fact]
        public void BateauAffiche_Set_ShouldUpdateValue()
        {
            AfficheService service = new AfficheService();
            Bateau bateau = new Bateau { Id = 1 };

            service.BateauAffiche = bateau;

            Assert.Equal(bateau, service.BateauAffiche);
        }
    }
}