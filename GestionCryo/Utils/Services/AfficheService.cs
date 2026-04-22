using GestionCryo.Model;
using GestionCryo.Utils.Services.Interfaces;
namespace GestionCryo.Utils.Services
{
    public class AfficheService : BaseViewModel, IAfficheService
    {
        private Station? _stationAffichee;
        public Station? StationAffichee
        {
            get => _stationAffichee;
            set
            {
                _stationAffichee = value;
                OnPropertyChanged();
            }
        }

        private Parcours? _parcoursAffiche;
        public Parcours? ParcoursAffiche
        {
            get => _parcoursAffiche;
            set
            {
                _parcoursAffiche = value;
                OnPropertyChanged();
            }
        }

        private Bateau? _bateauAffiche;
        public Bateau? BateauAffiche
        {
            get => _bateauAffiche;
            set
            {
                _bateauAffiche = value;
                OnPropertyChanged();
            }
        }
    }
}
