using GestionCryo.Model;

namespace GestionCryo.Utils.Services.Interfaces
{
    public interface IAlerteService
    {
        List<Alerte> ObtenirAlertes(int stationId);
    }
}