using GestionCryo.Data;
using GestionCryo.Model.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace GestionCryo.Model.DAL
{
    public class BateauDAL : IBateauDAL
    {
        private readonly ApplicationDbContext _context;

        public BateauDAL(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public void ModifierEtat(Bateau bateau, EtatBateau nouvelEtat)
        {
            Bateau bateauBd = _context.Bateaux.Find(bateau.Id);
            if (bateauBd != null)
            {
                bateauBd.Etat = nouvelEtat;
                _context.SaveChanges();
            }
        }

        
        public List<Bateau> ObtenirBateauxParStationId(int stationId)
        {
            return _context.Bateaux
                .Include(b => b.ParcoursEnCours)
                .Where(b => b.StationProcheId == stationId)
                .ToList();
        }
    }
}