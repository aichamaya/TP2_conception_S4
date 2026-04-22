using GestionCryo.Model;
namespace GestionCryo.Utils.Services.Interfaces
{
     public interface IUtilisateurDalService
    {
        Utilisateur? TrouverParNomEtMotDePasse(string username, string password);
    }
}
