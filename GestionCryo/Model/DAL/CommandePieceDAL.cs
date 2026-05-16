using GestionCryo.Data;
using GestionCryo.Model.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GestionCryo.Model.DAL
{
    public class CommandePieceDAL : ICommandePieceDAL
    {
        private readonly ApplicationDbContext _context;

        public CommandePieceDAL(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<CommandePiece> ObtenirCommandesParBateauId(int bateauId)
        {
            return _context.CommandesPieces
                .Where(c => c.BateauId == bateauId)
                .ToList();
        }

        public void AjouterCommande(CommandePiece commande)
        {
            _context.CommandesPieces.Add(commande);
            _context.SaveChanges();
        }
    }
}
