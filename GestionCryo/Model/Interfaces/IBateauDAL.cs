using GestionCryo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCryo.Model.Interfaces
{
    public interface IBateauDAL
    {
        void ModifierEtat(Bateau bateau, EtatBateau nouvelEtat);
    }
}
