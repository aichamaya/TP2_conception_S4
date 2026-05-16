using System;
using GestionCryo.Model;

namespace GestionCryo.Model
{
    public enum StatutCommande { EnAttente, Livree, Annulee, Urgente }

    public class CommandePiece
    {
        public int Id { get; set; }
        public string NomPiece { get; set; }
        public int Quantite { get; set; }
        public StatutCommande Statut { get; set; } = StatutCommande.EnAttente;
        public DateTime DateCommande { get; set; } = DateTime.Now;
        public int BateauId { get; set; }
        public Bateau Bateau { get; set; }
    }
}
