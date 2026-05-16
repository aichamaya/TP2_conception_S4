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
    public class CommandePieceViewModel : BaseViewModel
    {
        private readonly ICommandePieceDAL _commandePieceDAL;
        private readonly IAfficheService _afficheService;
        private readonly INavigationService _navigationService;

        public Bateau? Bateau
        {
            get => _afficheService.BateauAffiche;
        }

        public ObservableCollection<CommandePiece> Commandes
        {
            get
            {
                if (Bateau == null)
                    return new ObservableCollection<CommandePiece>();
                return new ObservableCollection<CommandePiece>(_commandePieceDAL.ObtenirCommandesParBateauId(Bateau.Id));
            }
        }

        private string _nomPiece = string.Empty;
        public string NomPiece
        {
            get => _nomPiece;
            set
            {
                if (_nomPiece != value)
                {
                    _nomPiece = value;
                    OnPropertyChanged(nameof(NomPiece));
                    ValidateProperty(nameof(NomPiece), value);
                }
            }
        }

        private int _quantite = 1;
        public int Quantite
        {
            get => _quantite;
            set
            {
                if (_quantite != value)
                {
                    _quantite = value;
                    OnPropertyChanged(nameof(Quantite));
                }
            }
        }

        private StatutCommande _statutSelectionne = StatutCommande.EnAttente;
        public StatutCommande StatutSelectionne
        {
            get => _statutSelectionne;
            set
            {
                if (_statutSelectionne != value)
                {
                    _statutSelectionne = value;
                    OnPropertyChanged(nameof(StatutSelectionne));
                }
            }
        }

        public List<StatutCommande> StatutsPossibles
        {
            get => new List<StatutCommande> { StatutCommande.EnAttente, StatutCommande.Urgente };
        }

       
        public CommandePieceViewModel(ICommandePieceDAL commandePieceDAL, IAfficheService afficheService, INavigationService navigationService)
        {
            _commandePieceDAL = commandePieceDAL;
            _afficheService = afficheService;
            _navigationService = navigationService;
            CommanderCommand = new RelayCommand(Commander, CanCommander);
            RetourCommand = new RelayCommand(Retour);
        }

        
        public ICommand CommanderCommand { get; set; }

       
        public ICommand RetourCommand { get; set; }

      
        private void Commander()
        {
            if (Bateau == null)
                return;

            CommandePiece commande = new CommandePiece
            {
                NomPiece = NomPiece,
                Quantite = Quantite,
                Statut = StatutSelectionne,
                BateauId = Bateau.Id
            };
            _commandePieceDAL.AjouterCommande(commande);
            NomPiece = string.Empty;
            Quantite = 1;
            OnPropertyChanged(nameof(Commandes));
        }

      
        private bool CanCommander()
        {
            return Bateau != null && !string.IsNullOrWhiteSpace(NomPiece) && Quantite > 0 && !HasErrors;
        }

        
        private void Retour()
        {
            _navigationService.NavigateTo<BateauViewModel>();
        }

        
        private void ValidateProperty(string propertyName, string value)
        {
            ClearErrors(propertyName);
            if (string.IsNullOrWhiteSpace(value))
                AddError(propertyName, $"{propertyName} ne peut pas être vide.");
            OnPropertyChanged(nameof(ErrorMessages));
        }
    }
}
