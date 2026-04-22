using GestionCryo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCryo.Utils.Services.Interfaces
{
    public interface IStationDalService
    {
        ICollection<Station> ObtenirToutesLesStations();
        Station ObtenirStationParId(int stationId);
    }
}
