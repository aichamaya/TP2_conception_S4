using GestionCryo.Model;
using GestionCryo.Model.Interfaces;
using GestionCryo.Utils;
using GestionCryo.Utils.Commands;
using GestionCryo.Utils.Services.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GestionCryo.ViewModel
{
    public class BateauViewModel : BaseViewModel
    {
        private readonly IBateauDAL _bateauDAL;
        private readonly IAfficheService _afficheService;
        private readonly IMaterielDAL _materielDAL;
        private readonly INavigationService _navigationService;

        public Bateau? Bateau
        {
            get => _afficheService.BateauAffiche;
        }

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

        public List<EtatBateau> EtatsPossibles
        {
            get => new List<EtatBateau> { EtatBateau.Pret, EtatBateau.Arrime, EtatBateau.EnMaintenance, EtatBateau.Inspection, EtatBateau.Reparation };
        }

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

       
        public BateauViewModel(IBateauDAL bateauDAL, IAfficheService afficheService, IMaterielDAL materielDAL, INavigationService navigationService)
        {
            _bateauDAL = bateauDAL;
            _afficheService = afficheService;
            _materielDAL = materielDAL;
            _navigationService = navigationService;
            _etatSelectionne = _afficheService.BateauAffiche?.Etat ?? EtatBateau.Arrime;
            ModifierEtatCommand = new RelayCommand(ModifierEtat, CanModifierEtat);
            CommanderPiecesCommand = new RelayCommand(CommanderPieces, CanCommanderPieces);
        }

       
        public ICommand ModifierEtatCommand { get; set; }

      
        public ICommand CommanderPiecesCommand { get; set; }

       
        public void ModifierEtat()
        {
            _bateauDAL.ModifierEtat(_afficheService.BateauAffiche, EtatSelectionne);
            OnPropertyChanged(nameof(EtatActuelAffiche));
        }

       
        public bool CanModifierEtat()
        {
            return _afficheService.BateauAffiche != null && _afficheService.BateauAffiche.Etat != EtatBateau.EnMission;
        }

        
        private void CommanderPieces()
        {
            _navigationService.NavigateTo<CommandePieceViewModel>();
        }

        
        private bool CanCommanderPieces()
        {
            return Bateau != null &&
                   (Bateau.Etat == EtatBateau.Inspection ||
                    Bateau.Etat == EtatBateau.Reparation);
        }
    }
}