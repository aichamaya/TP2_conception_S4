
using GestionCryo.Model;
using GestionCryo.Model.Interfaces;
using GestionCryo.Utils;
using GestionCryo.Utils.Commands;
using GestionCryo.Utils.Services.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace GestionCryo.ViewModel
{
    public class BateauViewModel : BaseViewModel
    {

        private readonly IBateauDalService _bateauDALService;
        private readonly IAfficheService _afficheService;
        private readonly IMaterielDalService _materielDALService;
        public IAfficheService AfficheService
        {
            get { return _afficheService; }
        }

        private string _etat;
        public string Etat
        {
            get => _etat;
            set
            {
                if (value != _etat)
                {
                    _etat = value;
                    OnPropertyChanged();
                    ValidateProperty(nameof(Etat), value);
                }
            }
        }
        public string EtatActuelAffiche
        {
            get => _afficheService.BateauAffiche?.Etat.ToString() ?? "";
        }
        public ObservableCollection<Materiel> Inventaire
        {
            get
            {
               return new ObservableCollection<Materiel>(_materielDALService.ObtenirMaterielParBateauId(_afficheService.BateauAffiche.Id));
            }
        }
        public ICommand ModifierEtatCommand { get; set; }
        public void ModifierEtat()
        {
            ClearErrors(nameof(Etat));
            if (Enum.TryParse<EtatBateau>(Etat, out var etat))
            {
                _bateauDALService.ModifierEtat(_afficheService.BateauAffiche, etat);
               OnPropertyChanged(nameof(EtatActuelAffiche));
            }
            else
            {
                AddError(nameof(Etat), "L'etat n'est pas valide");
                OnPropertyChanged(nameof(ErrorMessages));
            }
        }
        public bool CanModifierEtat()
        {
            return !String.IsNullOrEmpty(Etat);
        }
        public BateauViewModel(IBateauDalService bateauDALService, IAfficheService afficheService, IMaterielDalService materielDALService)
        {
            _bateauDALService = bateauDALService;
            _afficheService = afficheService;
            _materielDALService = materielDALService;
            _etat =  _afficheService.BateauAffiche.Etat.ToString();
            ModifierEtatCommand = new RelayCommand(ModifierEtat, CanModifierEtat);
        }

        private void ValidateProperty(string propertyName, string value)
        {
                ClearErrors(propertyName);
                if (string.IsNullOrWhiteSpace(value))
                {
                    AddError(propertyName, $"{propertyName} ne peut pas être vide.");
                }
                OnPropertyChanged(nameof(ErrorMessages));
        }

    }
}