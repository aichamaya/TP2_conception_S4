using GestionCryo.Model;
using GestionCryo.Model.Interfaces;
using GestionCryo.Utils.Services.Interfaces;


namespace GestionCryo.Utils.Services
{
    public class MaterielDalService: IMaterielDalService
    {
        private readonly IMaterielDAL _materielDal;
        public MaterielDalService(IMaterielDAL materielDal)
        {
            _materielDal = materielDal;
        }

        public List<Materiel> ObtenirMaterielParBateauId(int bateauId)
        {
            return _materielDal.ObtenirMaterielParBateauId(bateauId);
        }
    }
}
