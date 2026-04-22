using GestionCryo.Model;

namespace GestionCryo.Model
{
    public enum ClasseBateau { Legere, Moyenne, Lourde, Assistance }
    public enum EtatBateau { Pret, EnMission, EnMaintenance, Arrime }

    public class Bateau
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Modele { get; set; }
        public int AnneeConstruction { get; set; }
        public ClasseBateau Classe { get; set; }
        public EtatBateau Etat { get; set; }
        public Parcours? ParcoursEnCours { get; set; }

        public int StationProcheId { get; set; }
        public Station StationProche { get; set; }

        public ICollection<Materiel> Inventaire { get; set; } = new List<Materiel>();
    }
}