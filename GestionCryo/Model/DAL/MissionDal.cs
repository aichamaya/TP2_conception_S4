using GestionCryo.Model;
using GestionCryo.Model.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace GestionCryo.Data
{
    public class MissionDAL : IMissionDAL
    {
        private readonly ApplicationDbContext _context;

        public MissionDAL(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AjouterMission(Mission mission)
        {
            _context.Missions.Add(mission);
            _context.SaveChanges();
        }

        public ICollection<Mission> ObtenirToutesLesMissions()
        {
            return _context.Missions
                .Include(m => m.StationDepart)
                .Include(m => m.StationArrivee)
                .Include(m => m.Bateau)
                .ToList();
        }
    }
}