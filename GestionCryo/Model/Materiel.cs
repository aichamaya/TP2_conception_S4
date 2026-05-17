using GestionCryo.Model;
namespace GestionCryo.Model
{
    public class Materiel
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public TypeMateriel Type { get; set; }
        public int Quantite { get; set; } = 0;

        public int BateauId { get; set; }   
        public Bateau? Bateau { get; set; }

    }
    public enum TypeMateriel
    {
        Oxygene,
        Nourriture,
        Eau,
        EquipementScientifique,
        PiecesDeRechange,
        Autres
    }
}