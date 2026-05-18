
using GestionCryo.Model;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace GestionCryo.Data
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "GestionCryo", "GestionCryo.db");
            Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!);
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }

        public DbSet<Utilisateur> Users { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Bateau> Bateaux { get; set; }
        public DbSet<Parcours> Parcours { get; set; }
        public DbSet<CommandePiece> CommandesPieces { get; set; }
        public DbSet<Materiel> Materiels { get; set; }
        public DbSet<Mission> Missions { get; set; }

        public void SeedData()
        {
            if (Stations.Any()) return;
            SeedStationsEtUtilisateurs();
            SeedBateauxParcoursEtMateriels();
        }

        private void SeedStationsEtUtilisateurs()
        {
            List<Station> stations = new List<Station>
            {
                new Station { Nom = "Cap-Prince",  Localisation = "Yukon",                Latitude = 69.5, Longitude = -140.5 },
                new Station { Nom = "Herschel",    Localisation = "Mer de Beaufort",       Latitude = 69.6, Longitude = -138.9 },
                new Station { Nom = "Banks",       Localisation = "Île Banks",             Latitude = 73.0, Longitude = -121.0 },
                new Station { Nom = "Parry",       Localisation = "Île Parry",             Latitude = 75.0, Longitude = -110.0 },
                new Station { Nom = "Lancaster",   Localisation = "Détroit de Lancaster",  Latitude = 74.2, Longitude = -84.0  },
                new Station { Nom = "Baffin",      Localisation = "Île de Baffin",         Latitude = 72.0, Longitude = -75.0  }
            };
            Stations.AddRange(stations);
            SaveChanges();

            
            Users.AddRange(
                new Utilisateur
                {
                    Prenom = "Admin",
                    Nom = "System",
                    Username = "admin",
                    Password = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    Role = Role.Admin,
                    StationId = null
                },
                new Utilisateur
                {
                    Prenom = "John",
                    Nom = "Doe",
                    Username = "johndoe",
                    Password = BCrypt.Net.BCrypt.HashPassword("password123"),
                    Role = Role.Employe,
                    StationId = stations[0].Id
                },
                new Utilisateur
                {
                    Prenom = "Jane",
                    Nom = "Doe",
                    Username = "janedoe",
                    Password = BCrypt.Net.BCrypt.HashPassword("password123"),
                    Role = Role.Employe,
                    StationId = stations[1].Id
                }
            );
            SaveChanges();
        }

        private void SeedBateauxParcoursEtMateriels()
        {
            List<Station> stations = Stations.ToList();

            List<Bateau> bateaux = new List<Bateau>
            {
                new Bateau { Nom = "Arctique I",    Modele = "Polar 3000", AnneeConstruction = 2015, Classe = ClasseBateau.Lourde,     Etat = EtatBateau.Arrime,    StationProcheId = stations[0].Id },
                new Bateau { Nom = "Nordique II",   Modele = "Nordic 200", AnneeConstruction = 2018, Classe = ClasseBateau.Moyenne,    Etat = EtatBateau.Pret,      StationProcheId = stations[0].Id },
                new Bateau { Nom = "Glace Rapide",  Modele = "Swift 100",  AnneeConstruction = 2020, Classe = ClasseBateau.Assistance, Etat = EtatBateau.EnMission, StationProcheId = stations[1].Id },
                new Bateau { Nom = "Brise-Nord",    Modele = "Polar 3000", AnneeConstruction = 2016, Classe = ClasseBateau.Lourde,     Etat = EtatBateau.Arrime,    StationProcheId = stations[1].Id },
                new Bateau { Nom = "Cap-Glace",     Modele = "Nordic 200", AnneeConstruction = 2019, Classe = ClasseBateau.Moyenne,    Etat = EtatBateau.Pret,      StationProcheId = stations[2].Id },
                new Bateau { Nom = "Polaris",       Modele = "Light 50",   AnneeConstruction = 2021, Classe = ClasseBateau.Legere,     Etat = EtatBateau.Arrime,    StationProcheId = stations[2].Id },
                new Bateau { Nom = "Parry Express", Modele = "Swift 100",  AnneeConstruction = 2017, Classe = ClasseBateau.Assistance, Etat = EtatBateau.Pret,      StationProcheId = stations[3].Id },
                new Bateau { Nom = "Lancaster I",   Modele = "Polar 3000", AnneeConstruction = 2014, Classe = ClasseBateau.Lourde,     Etat = EtatBateau.EnMission, StationProcheId = stations[4].Id },
                new Bateau { Nom = "Baffin King",   Modele = "Nordic 200", AnneeConstruction = 2022, Classe = ClasseBateau.Moyenne,    Etat = EtatBateau.Arrime,    StationProcheId = stations[5].Id }
            };
            Bateaux.AddRange(bateaux);
            SaveChanges();

            List<Parcours> parcours = new List<Parcours>
            {
                new Parcours { BateauId = bateaux[2].Id, StationDestinationId = stations[3].Id, AutonomiePourcentage = 65, DerniereMiseAJour = DateTime.Now.AddHours(-3), TypeMission = TypeMission.Assistance },
                new Parcours { BateauId = bateaux[7].Id, StationDestinationId = stations[5].Id, AutonomiePourcentage = 42, DerniereMiseAJour = DateTime.Now.AddHours(-6), TypeMission = TypeMission.Ravitaillement }
            };
            Parcours.AddRange(parcours);
            SaveChanges();

            bateaux[2].ParcoursEnCours = parcours[0];
            bateaux[7].ParcoursEnCours = parcours[1];
            SaveChanges();

            List<Materiel> materiels = new List<Materiel>
            {
                new Materiel { Nom = "Réserve O2",            Type = TypeMateriel.Oxygene,              Quantite = 12,  BateauId = bateaux[0].Id },
                new Materiel { Nom = "Rations arctiques",     Type = TypeMateriel.Nourriture,           Quantite = 80,  BateauId = bateaux[0].Id },
                new Materiel { Nom = "Eau potable",           Type = TypeMateriel.Eau,                  Quantite = 150, BateauId = bateaux[0].Id },
                new Materiel { Nom = "Kit médical",           Type = TypeMateriel.Autres,               Quantite = 5,   BateauId = bateaux[2].Id },
                new Materiel { Nom = "Oxygène secours",       Type = TypeMateriel.Oxygene,              Quantite = 8,   BateauId = bateaux[2].Id },
                new Materiel { Nom = "Vivres rapides",        Type = TypeMateriel.Nourriture,           Quantite = 40,  BateauId = bateaux[2].Id },
                new Materiel { Nom = "Carburant technique",   Type = TypeMateriel.Autres,               Quantite = 20,  BateauId = bateaux[3].Id },
                new Materiel { Nom = "Pièces moteur",         Type = TypeMateriel.PiecesDeRechange,     Quantite = 14,  BateauId = bateaux[3].Id },
                new Materiel { Nom = "Capteurs météo",        Type = TypeMateriel.EquipementScientifique, Quantite = 6, BateauId = bateaux[7].Id },
                new Materiel { Nom = "Nourriture longue durée", Type = TypeMateriel.Nourriture,         Quantite = 120, BateauId = bateaux[7].Id },
                new Materiel { Nom = "Eau potable",           Type = TypeMateriel.Eau,                  Quantite = 90,  BateauId = bateaux[8].Id },
                new Materiel { Nom = "Pièces hydrauliques",   Type = TypeMateriel.PiecesDeRechange,     Quantite = 10,  BateauId = bateaux[8].Id }
            };
            Materiels.AddRange(materiels);
            SaveChanges();
        }
    }
}