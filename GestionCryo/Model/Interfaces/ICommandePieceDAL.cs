using System.Collections.Generic;

namespace GestionCryo.Model.Interfaces
{
    public interface ICommandePieceDAL
    {
        List<CommandePiece> ObtenirCommandesParBateauId(int bateauId);
        void AjouterCommande(CommandePiece commande);
    }
}
