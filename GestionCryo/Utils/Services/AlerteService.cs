using GestionCryo.Model;
using GestionCryo.Model.Interfaces;
using GestionCryo.Utils.Services.Interfaces;
using System.Collections.Generic;

namespace GestionCryo.Utils.Services
{
    public class AlerteService : IAlerteService
    {
        private readonly IBateauDAL _bateauDAL;
        private readonly ICommandePieceDAL _commandePieceDAL;

        public AlerteService(IBateauDAL bateauDAL, ICommandePieceDAL commandePieceDAL)
        {
            _bateauDAL = bateauDAL;
            _commandePieceDAL = commandePieceDAL;
        }

        public List<Alerte> ObtenirAlertes(int stationId)
        {
            List<Alerte> alertes = new List<Alerte>();
            List<Bateau> bateaux = _bateauDAL.ObtenirBateauxParStationId(stationId);

            foreach (Bateau bateau in bateaux)
            {
                
                if (bateau.ParcoursEnCours != null && bateau.ParcoursEnCours.AutonomiePourcentage < 20)
                {
                    alertes.Add(new Alerte
                    {
                        Message = $"Autonomie critique : {bateau.ParcoursEnCours.AutonomiePourcentage}%",
                        Type = TypeAlerte.Autonomiecritique,
                        NomBateau = bateau.Nom
                    });
                }

                List<CommandePiece> commandes = _commandePieceDAL.ObtenirCommandesParBateauId(bateau.Id);
                foreach (CommandePiece commande in commandes)
                {
                    if (commande.Statut == StatutCommande.Urgente)
                    {
                        alertes.Add(new Alerte
                        {
                            Message = $"Commande urgente : {commande.NomPiece}",
                            Type = TypeAlerte.CommandeUrgente,
                            NomBateau = bateau.Nom
                        });
                    }
                }
            }
            return alertes;
        }
    }
}