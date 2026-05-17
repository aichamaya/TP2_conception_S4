using GestionCryo.Model;
using System.Collections.Generic;

namespace GestionCryo.Utils.Services.Interfaces
{
    public interface IAlerteService
    {
        List<Alerte> ObtenirAlertes(int stationId);
    }
}
