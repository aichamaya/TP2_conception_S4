using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCryo.Model.Interfaces
{
    public interface IStationDAL
    {
        ICollection<Station> ObtenirToutesLesStations();
        Station ObtenirStationParId(int stationId);
    }
}
