using System.Collections.Generic;

namespace GestionCryo.Model
{
    public class Station
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Localisation { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public ICollection<Bateau> Bateaux { get; set; } = new List<Bateau>();
    }
}