using GestionCryo.Model;
using GestionCryo.Model.Interfaces;
using GestionCryo.Utils.Services.Interfaces;

namespace GestionCryo.Utils.Services
{
    public class AlerteService : IAlerteService
    {
        private readonly IStationDAL _stationDAL;

        public AlerteService(IStationDAL stationDAL)
        {
            _stationDAL = stationDAL;
        }

        public List<Alerte> ObtenirAlertes(int stationId)
        {
            List<Alerte> alertes = new List<Alerte>();

            Station? station = _stationDAL.ObtenirStationParId(stationId);

            if (station == null)
                return alertes;

            foreach (Bateau bateau in station.Bateaux)
            {
                if (bateau.Etat == EtatBateau.Inspection)
                {
                    alertes.Add(new Alerte
                    {
                        NomBateau = bateau.Nom,
                        Type = "Inspection",
                        Message = $"Le bateau {bateau.Nom} est en inspection. Restez vigilants."
                    });
                }
            }

            List<Mission> missions = _stationDAL.ObtenirMissionsParStationId(stationId);

            if (missions.Count > 2)
            {
                alertes.Add(new Alerte
                {
                    NomBateau = "-",
                    Type = "Charge élevée",
                    Message = "Plus de deux missions concernent cette station. Charge de travail élevée."
                });
            }

            if (station.Bateaux.Count > 5)
            {
                alertes.Add(new Alerte
                {
                    NomBateau = "-",
                    Type = "Achalandage",
                    Message = "Plus de cinq bateaux sont à proximité. Attention à l’achalandage."
                });
            }

            return alertes;
        }
    }
}