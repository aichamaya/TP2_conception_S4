namespace GestionCryo.Model
{
    public enum TypeAlerte { Autonomiecritique, CommandeUrgente }

    public class Alerte
    {
        public string Message { get; set; }
        public TypeAlerte Type { get; set; }
        public string NomBateau { get; set; }
    }
}