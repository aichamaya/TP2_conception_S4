using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCryoTests.ServicesTests
{
    public class AfficheServiceTests
    {
        [Fact]
        public void StationAffichee_Set_ShouldUpdateValue()
        {
            
            var service = new AfficheService();
            var station = new Station { Id = 1 };

            
            service.StationAffichee = station;

            
            Assert.Equal(station, service.StationAffichee);
        }
        [Fact]
        public void ParcoursAffiche_Set_ShouldUpdateValue()
        {
            
            var service = new AfficheService();
            var parcours = new Parcours { Id = 1 };

            
            service.ParcoursAffiche = parcours;

            
            Assert.Equal(parcours, service.ParcoursAffiche);
        }
        [Fact]
        public void BateauAffiche_Set_ShouldUpdateValue()
        {
            
            var service = new AfficheService();
            var bateau = new Bateau { Id = 1 };

            
            service.BateauAffiche = bateau;

            
            Assert.Equal(bateau, service.BateauAffiche);
        }

    }
}
