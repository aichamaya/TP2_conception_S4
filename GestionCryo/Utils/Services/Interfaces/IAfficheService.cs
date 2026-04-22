using GestionCryo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCryo.Utils.Services.Interfaces
{
    public interface IAfficheService
    {
        public Station StationAffichee { get; set; }
        public Parcours ParcoursAffiche { get; set; }
        public Bateau BateauAffiche { get; set; }
    }
}
