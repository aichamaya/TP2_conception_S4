using System;

namespace GestionCryo.Model
{
    public class Mission
    {
        public int Id { get; set; }

        public int StationDepartId { get; set; }
        public Station? StationDepart { get; set; }

        public int StationArriveeId { get; set; }
        public Station? StationArrivee { get; set; }

        public int BateauId { get; set; }
        public Bateau? Bateau { get; set; }

        public string Cargo { get; set; } = string.Empty;

        public DateTime DateCreation { get; set; } = DateTime.Now;
    }
}