
using GestionCryo.Model;
using GestionCryo.Model.Interfaces;
using GestionCryo.Utils.Services.Interfaces;

namespace GestionCryo.Utils.Services
{
    public class UtilisateurDalService : IUtilisateurDalService
    {
        private readonly IUtilisateurDAL _utilisateurDal;

        public Utilisateur? TrouverParNomEtMotDePasse(string username, string password)
        {
            return _utilisateurDal.TrouverParNomEtMotDePasse(username, password);
        }
        public UtilisateurDalService(IUtilisateurDAL utilisateurDal)
        {
            _utilisateurDal = utilisateurDal;
        }
    }
}
