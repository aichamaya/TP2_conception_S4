namespace GestionCryo.Utils.Services.Interfaces
{
    public interface IAlerteService
    {
        void AfficherInformation(string message);
        void AfficherErreur(string message);
        bool Confirmer(string message);
    }
}
