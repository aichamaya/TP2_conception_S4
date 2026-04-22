namespace GestionCryo.Model
{
    public class Utilisateur
    {
        public int Id { get; set; }
        public string Prenom { get; set; }
        public string Nom { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public int? StationId { get; set; }
        public Station? Station { get; set; }
    }
    public enum Role
    {
        Admin,
        Employe
    }
}