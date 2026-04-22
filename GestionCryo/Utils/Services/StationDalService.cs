using GestionCryo.Model;
using GestionCryo.Model.Interfaces;
using GestionCryo.Utils.Services.Interfaces;

namespace GestionCryo.Utils.Services
{
    public class StationDalService: IStationDalService
    {
        private readonly IStationDAL _stationDal;

        public StationDalService(IStationDAL stationDal)
        {
            _stationDal = stationDal;
        }

        public Station ObtenirStationParId(int stationId)
        {
            return _stationDal.ObtenirStationParId(stationId);
        }

        public ICollection<Station> ObtenirToutesLesStations()
        {
            return _stationDal.ObtenirToutesLesStations();
        }
    }
}
