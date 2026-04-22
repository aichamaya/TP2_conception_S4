using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCryo.Model.Interfaces
{
    public interface IUtilisateurDAL
    {
        Utilisateur? TrouverParNomEtMotDePasse(string username, string password);
    }
}

