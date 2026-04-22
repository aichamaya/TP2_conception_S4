using Xunit;
using GestionCryo.Model;
using GestionCryo.Utils.Services;

namespace GestionCryoTests.ServicesTests
{
    public class ConnectedUserServiceTests
    {
        [Fact]
        public void ConnectedUser_Set_ShouldUpdateValue()
        {
            var service = new ConnectedUserService();
            var user = new Utilisateur { Username = "moussa" };

            service.ConnectedUser = user;

            Assert.Equal(user, service.ConnectedUser);
        }

        [Fact]
        public void ConnectedUserName_ShouldReturnFormattedName_WhenUserIsNotNull()
        {
            var service = new ConnectedUserService();
            var user = new Utilisateur { Username = "moussa" };
            service.ConnectedUser = user;

            var result = service.ConnectedUserName;

            Assert.Equal("Connecté en tant que : moussa", result);
        }

        [Fact]
        public void ConnectedUserName_ShouldReturnDefaultMessage_WhenUserIsNull()
        {
            var service = new ConnectedUserService();

            var result = service.ConnectedUserName;

            Assert.Equal("Aucun utilisateur connecté", result);
        }
    }
}