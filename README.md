Aicha Edwige Ouattara
Moussa Dembele
# GestionCryo — TP3 Conception Logicielle

## Équipe
- Aicha Edwige Ouattara
- Moussa Dembele

## Description
GestionCryo est une application WPF de gestion de brise-glaces dans le Grand Nord canadien. 
Elle permet aux employés de station de consulter leurs bateaux et à l'administrateur 
de gérer l'ensemble des stations.

## Fonctionnalités
- Connexion sécurisée avec redirection selon le rôle (employé / admin)
- Consultation des détails d'une station et de ses bateaux
- Modification de l'état d'un bateau
- Commander des pièces pour un bateau en inspection ou réparation
- Notifications et alertes (autonomie critique, commandes urgentes)
- Gestion des stations pour l'administrateur

## Technologies
- C# / WPF
- Architecture MVVM
- Entity Framework Core + SQLite
- Injection de dépendances (Microsoft.Extensions.DependencyInjection)
- BCrypt pour le hashage des mots de passe
- xUnit + Moq pour les tests unitaires
- GitHub Actions pour le CI/CD

## Lancer l'application
1. Cloner le repo
2. Ouvrir `GestionCryo.sln` dans Visual Studio 2022
3. Lancer le projet `GestionCryo`

## Comptes de test
| Rôle | Nom d'utilisateur | Mot de passe |
| Admin | admin | admin123 |
| Employé | johndoe | password123 |
| Employé | janedoe | password123 |

## CI/CD
- CI : les tests s'exécutent automatiquement sur chaque push et Pull Request
- CD : une release avec le `.exe` est générée automatiquement sur chaque tag `GestionCryo-V*`
