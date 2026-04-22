namespace GestionCryo.Model.Interfaces
{
    public interface IMaterielDAL
    {
        List<Materiel> ObtenirMaterielParBateauId(int bateauId);
    }
}