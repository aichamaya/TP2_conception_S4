using GestionCryo.Model;
using GestionCryo.Utils.Services.Interfaces;
namespace GestionCryo.Utils.Services
{
    public class ConnectedUserService: BaseViewModel,  IConnectedUserService 
    {
       private Utilisateur? _connectedUser;
       public Utilisateur? ConnectedUser
       {
           get => _connectedUser;
           set
           {
               _connectedUser = value;
               OnPropertyChanged();
                OnPropertyChanged(nameof(ConnectedUserName));
           }
       }
        public string ConnectedUserName
        {
            get
            {
                if (ConnectedUser != null)
                {
                    return $"Connecté en tant que : {ConnectedUser.Username}";
                }
                else
                {
                    return "Aucun utilisateur connecté";
                }
            }
        
        }
    }
}
