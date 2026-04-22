using GestionCryo.Data;
using GestionCryo.Model.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace GestionCryo.Model.DAL
{
    public class StationDAL : IStationDAL
    {
        private readonly ApplicationDbContext _context;

        public StationDAL(ApplicationDbContext context)
        {
            _context = context;
        }


        public ICollection<Station> ObtenirToutesLesStations()
        {
            return _context.Stations.Include(s => s.Bateaux).ThenInclude(b => b.ParcoursEnCours).ToList();
        }
        public Station? ObtenirStationParId(int id)
        {
            return _context.Stations.Include(s => s.Bateaux).ThenInclude(b => b.ParcoursEnCours).FirstOrDefault(s => s.Id == id);
        }
    }
}