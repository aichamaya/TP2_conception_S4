using GestionCryo.Model;
using System.Collections.Generic;

namespace GestionCryo.Model.Interfaces
{
    public interface IMissionDAL
    {
        void AjouterMission(Mission mission);
        ICollection<Mission> ObtenirToutesLesMissions();
    }
}