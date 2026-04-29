using GestionCryo.Model;
using GestionCryo.Model.Interfaces;
using GestionCryo.Utils;
using GestionCryo.Utils.Commands;
using GestionCryo.Utils.Services.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;//test
using System.Windows.Input;

namespace GestionCryo.ViewModel
{
    public class BateauViewModel : BaseViewModel
    {
        private readonly IBateauDAL _bateauDAL;
        private readonly IAfficheService _afficheService;
        private readonly IMaterielDAL _materielDAL;

        public IAfficheService AfficheService
        {
            get => _afficheService;
        }

        public string EtatActuelAffiche
        {
            get => _afficheService.BateauAffiche?.Etat.ToString() ?? "";
        }

        public ObservableCollection<Materiel> Inventaire
        {
            get => new ObservableCollection<Materiel>(_materielDAL.ObtenirMaterielParBateauId(_afficheService.BateauAffiche.Id));
        }

        
        //public List<EtatBateau> EtatsPossibles
        //{
        //    get => new List<EtatBateau> { EtatBateau.Pret, EtatBateau.Arrime, EtatBateau.EnMaintenance, EtatBateau.Inspection, EtatBateau.Reparation };
        //}

        private EtatBateau _etatSelectionne;
        public EtatBateau EtatSelectionne
        {
            get => _etatSelectionne;
            set
            {
                if (_etatSelectionne != value)
                {
                    _etatSelectionne = value;
                    OnPropertyChanged(nameof(EtatSelectionne));
                }
            }
        }

        
        public BateauViewModel(IBateauDAL bateauDAL, IAfficheService afficheService, IMaterielDAL materielDAL)
        {
            _bateauDAL = bateauDAL;
            _afficheService = afficheService;
            _materielDAL = materielDAL;
            _etatSelectionne = _afficheService.BateauAffiche?.Etat ?? EtatBateau.Arrime;
            ModifierEtatCommand = new RelayCommand(ModifierEtat, CanModifierEtat);
        }

        
        public ICommand ModifierEtatCommand { get; set; }

        
        public void ModifierEtat()
        {
            _bateauDAL.ModifierEtat(_afficheService.BateauAffiche, EtatSelectionne);
            OnPropertyChanged(nameof(EtatActuelAffiche));
        }

        public bool CanModifierEtat()
        {
            return _afficheService.BateauAffiche != null && _afficheService.BateauAffiche.Etat != EtatBateau.EnMission;
        }
    }
}