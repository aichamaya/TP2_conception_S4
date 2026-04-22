using GestionCryo.Data;
using GestionCryo.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCryo.Model.DAL
{
    public class MaterielDAL: IMaterielDAL
    {
        private readonly ApplicationDbContext _context;

        public MaterielDAL(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Materiel> ObtenirMaterielParBateauId(int bateauId)
        {
            return _context.Materiels.Where(m => m.BateauId == bateauId).ToList();
        }
    }
}
