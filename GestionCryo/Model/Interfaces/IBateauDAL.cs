using GestionCryo.Model;
using System.Collections.Generic;

namespace GestionCryo.Model.Interfaces
{
    public interface IBateauDAL
    {
        void ModifierEtat(Bateau bateau, EtatBateau nouvelEtat);
        List<Bateau> ObtenirBateauxParStationId(int stationId);
    }
}