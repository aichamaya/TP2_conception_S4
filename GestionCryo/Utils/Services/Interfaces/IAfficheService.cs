using GestionCryo.Model;

namespace GestionCryo.Utils.Services.Interfaces
{
    public interface IAfficheService
    {
        Station? StationAffichee { get; set; }
        Parcours? ParcoursAffiche { get; set; }
        Bateau? BateauAffiche { get; set; }
    }
}
