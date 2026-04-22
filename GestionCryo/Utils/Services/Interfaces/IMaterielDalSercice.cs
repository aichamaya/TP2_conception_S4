using GestionCryo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCryo.Utils.Services.Interfaces
{
    public interface IMaterielDalService
    {
        List<Materiel> ObtenirMaterielParBateauId(int bateauId);
    }
}
