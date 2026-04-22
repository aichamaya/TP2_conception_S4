using GestionCryo.Model;
using GestionCryo.Model.Interfaces;
using GestionCryo.Utils.Services.Interfaces;

namespace GestionCryo.Utils.Services
{
    public class BateauDalService: IBateauDalService
    {
        private readonly IBateauDAL _bateauDal;
        public BateauDalService(IBateauDAL bateauDal)
        {
            _bateauDal = bateauDal;
        }

        public void ModifierEtat(Bateau bateau, EtatBateau nouvelEtat)
        {
           _bateauDal.ModifierEtat(bateau, nouvelEtat);
        }
    }
}
