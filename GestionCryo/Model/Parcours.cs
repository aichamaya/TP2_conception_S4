using System;
using GestionCryo.Model;

namespace GestionCryo.Model
{
    public enum TypeMission { Surveillance, Ravitaillement, Assistance }

    public class Parcours
    {
        public int Id { get; set; }
        public int BateauId { get; set; }
        public Bateau Bateau { get; set; }
        public int StationDestinationId { get; set; }
        public Station StationDestination { get; set; }
        public double AutonomiePourcentage { get; set; }
        public DateTime DerniereMiseAJour { get; set; } = DateTime.Now;
        public TypeMission TypeMission { get; set; }
    }
}