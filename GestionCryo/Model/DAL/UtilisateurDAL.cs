using BCrypt.Net;
using GestionCryo.Data;
using GestionCryo.Model.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestionCryo.Model.DAL
{
    public class UtilisateurDAL : IUtilisateurDAL
    {
        private readonly ApplicationDbContext _context;

        public UtilisateurDAL(ApplicationDbContext context)
        {
            _context = context;
        }

        public Utilisateur? TrouverParNomEtMotDePasse(string username, string password)
        {
            Utilisateur? utilisateur = _context.Users
                .Include(x => x.Station)
                .ThenInclude(s => s.Bateaux)
                .ThenInclude(b => b.ParcoursEnCours)
                .FirstOrDefault(u => u.Username == username);

            if (utilisateur != null && BCrypt.Net.BCrypt.Verify(password, utilisateur.Password))
                return utilisateur;

            return null;
        }
    }
}