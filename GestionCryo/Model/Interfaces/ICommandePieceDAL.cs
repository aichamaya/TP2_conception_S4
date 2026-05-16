namespace GestionCryo.Model.Interfaces
{
    public interface ICommandePieceDAL
    {
        ICollection<CommandePiece> ObtenirToutesLesCommandes();
        ICollection<CommandePiece> ObtenirCommandesParBateauId(int bateauId);
        CommandePiece? ObtenirCommandeParId(int commandeId);
        void AjouterCommande(CommandePiece commande);
        void ModifierStatut(CommandePiece commande, StatutCommande nouveauStatut);
    }
}
