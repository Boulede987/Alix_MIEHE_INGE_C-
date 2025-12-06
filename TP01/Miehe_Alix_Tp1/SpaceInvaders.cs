using Miehe_Alix_Tp1.Models;
using Miehe_Alix_Tp1.Spaceships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Miehe_Alix_Tp1
{
    public class SpaceInvaders
    {
        private List<Spaceship> spaceships; // liste des vaisseaux du jeu, joueurs + ennemis
        private List<Spaceship> enemies; // liste d'ennemis

        public SpaceInvaders()
        {
            //
        }

        // Point d'entrée interactif du jeu (menu principal)
        public void Main()
        {
            Armory.Init();
            PlayersManager.players = new List<Player>();
            InitDefaults();

            ArmeImporteur.Init();
            ArmeImporteur.PrintWords();

            ArmeImporteur.ImportWeapons();

            StartMenu();
        }

        // Variante permettant d'importer un fichier d'armes au démarrage
        public void Main(string[] args)
        {
            Armory.Init();
            PlayersManager.players = new List<Player>();
            InitDefaults();

            if (args != null && args.Length > 0)
            {
                string path = args[0];
                Console.WriteLine($"Import d'armes depuis : {path}");
                try
                {
                    Armory.ImportFromCsv(path);
                    Console.WriteLine("Import d'armes terminé.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erreur d'import : {ex.Message}");
                }
            }

            StartMenu();
        }

        private void InitDefaults()
        {
            PlayersManager.AddPlayer(new Player("jhon", "sigma", "Ancutraxxe"));
            PlayersManager.AddPlayer(new Player("bob", "bobby", "Degeulaxxe"));
            PlayersManager.AddPlayer(new Player("alberto", "ultima", "Putraxxe"));

            PlayersManager.SetAllPlayersAsActive();

            Armory.AddWeapon(new Weapon("Dégomatron 9000", 999999, 9999999, EWeaponType.Explosive, 0));

            // Exemple : affectation d'un vaisseau au 3ème joueur
            PlayersManager.players[2].SetSpaceship(new ViperMKII(999999999, 999999999, 999999999, 999999999, new List<Weapon> { new Weapon("Dégomatron 9000", 999999, 9999999, EWeaponType.Explosive, 0) }, PlayersManager.players[2].Alias));

            this.spaceships = new List<Spaceship>();
            spaceships.Add(PlayersManager.players[0].BattleShip);
            spaceships.Add(PlayersManager.players[1].BattleShip);
            spaceships.Add(PlayersManager.players[2].BattleShip); // joueurs

            this.enemies = new List<Spaceship>();

            enemies.Add(new Dart(1, 1, 1, 1, new List<Weapon>())); // enemies
            enemies.Add(new F_18(1, 1, 1, 1, new List<Weapon>()));
            enemies.Add(new B_Wings(1, 1, 1, 1, new List<Weapon>()));
            enemies.Add(new Rocinante(1, 1, 1, 1, new List<Weapon>()));
            enemies.Add(new Tardis(1, 1, 1, 1, new List<Weapon>()));

            spaceships.AddRange(enemies);

            // show enemies
            foreach (Spaceship enemy in enemies)
            {
                Console.WriteLine($"Ennemi ajouté : {enemy.Name ?? enemy.GetType().Name}");
            }
        }

        private void StartMenu()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("=== Menu Principal ===");
                Console.WriteLine("1) Gérer les joueurs");
                Console.WriteLine("2) Gérer le vaisseau (armes)");
                Console.WriteLine("3) Gérer l'armurerie");
                Console.WriteLine("4) Lancer la partie");
                Console.WriteLine("5) Quitter");
                Console.Write("Choix : ");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ManagePlayersMenu();
                        break;
                    case "2":
                        ManageShipMenu();
                        break;
                    case "3":
                        ManageArmoryMenu();
                        break;
                    case "4":
                        PlayGame();
                        return;
                    case "5":
                        ExitGame();
                        return;
                    default:
                        Console.WriteLine("Choix invalide.");
                        break;
                }
            }
        }

        private void ManagePlayersMenu()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("--- Gestion des joueurs ---");
                Console.WriteLine("1) Lister les joueurs");
                Console.WriteLine("2) Créer un joueur");
                Console.WriteLine("3) Supprimer un joueur");
                Console.WriteLine("4) Choisir le joueur courant (activer/désactiver)");
                Console.WriteLine("5) Retour");
                Console.Write("Choix : ");
                var c = Console.ReadLine();
                switch (c)
                {
                    case "1":
                        PlayersManager.ViewPlayers();
                        break;
                    case "2":
                        Console.Write("Prénom : ");
                        var first = Console.ReadLine();
                        Console.Write("Nom : ");
                        var last = Console.ReadLine();
                        Console.Write("Alias : ");
                        var alias = Console.ReadLine();
                        PlayersManager.AddPlayer(new Player(first, last, alias));
                        Console.WriteLine("Joueur créé.");
                        break;
                    case "3":
                        Console.Write("Alias du joueur à supprimer : ");
                        var rem = Console.ReadLine();
                        var p = PlayersManager.players.FirstOrDefault(x => x.Alias == rem);
                        if (p != null)
                        {
                            PlayersManager.players.Remove(p);
                            Console.WriteLine("Joueur supprimé.");
                        }
                        else Console.WriteLine("Joueur introuvable.");
                        break;
                    case "4":
                        Console.Write("Alias du joueur à activer/désactiver : ");
                        var a = Console.ReadLine();
                        var player = PlayersManager.players.FirstOrDefault(x => x.Alias == a);
                        if (player != null)
                        {
                            player.isActive = !player.isActive;
                            Console.WriteLine($"Joueur {player.Alias} actif = {player.isActive}");
                        }
                        else Console.WriteLine("Joueur introuvable.");
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Choix invalide.");
                        break;
                }
            }
        }

        private void ManageShipMenu()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("--- Gestion du vaisseau ---");
                Console.WriteLine("1) Lister joueurs et vaisseaux");
                Console.WriteLine("2) Ajouter une arme au vaisseau d'un joueur");
                Console.WriteLine("3) Retirer une arme du vaisseau d'un joueur");
                Console.WriteLine("4) Retour");
                Console.Write("Choix : ");
                var c = Console.ReadLine();
                switch (c)
                {
                    case "1":
                        foreach (var pl in PlayersManager.players)
                        {
                            Console.WriteLine($"{pl.Alias} - Vaisseau: {pl.BattleShip?.Name ?? "Aucun"}");
                            pl.BattleShip?.ViewWeapons();
                        }
                        break;
                    case "2":
                        Console.Write("Alias du joueur : ");
                        var alias = Console.ReadLine();
                        var plAdd = PlayersManager.players.FirstOrDefault(x => x.Alias == alias);
                        if (plAdd?.BattleShip == null) { Console.WriteLine("Joueur ou vaisseau introuvable."); break; }
                        Armory.ViewArmory();
                        Console.Write("Nom de l'arme à ajouter (exact) : ");
                        var weaponName = Console.ReadLine();
                        var weapon = Armory.GetWeaponByName(weaponName);
                        if (weapon == null) { Console.WriteLine("Arme introuvable dans l'armurerie."); break; }
                        try
                        {
                            plAdd.BattleShip.AddWeapon(new Weapon(weapon.Name, weapon.MinDamage, weapon.MaxDamage, weapon.Type, weapon.ReloadTime));
                            Console.WriteLine("Arme ajoutée.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Impossible d'ajouter l'arme : {ex.Message}");
                        }
                        break;
                    case "3":
                        Console.Write("Alias du joueur : ");
                        var alias2 = Console.ReadLine();
                        var plRem = PlayersManager.players.FirstOrDefault(x => x.Alias == alias2);
                        if (plRem?.BattleShip == null) { Console.WriteLine("Joueur ou vaisseau introuvable."); break; }
                        plRem.BattleShip.ViewWeapons();
                        Console.Write("Nom de l'arme à retirer (exact) : ");
                        var wname = Console.ReadLine();
                        var wToRemove = plRem.BattleShip.Weapons.FirstOrDefault(w => w.Name == wname);
                        if (wToRemove == null) { Console.WriteLine("Arme introuvable sur le vaisseau."); break; }
                        plRem.BattleShip.RemoveWeapon(wToRemove);
                        Console.WriteLine("Arme retirée.");
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Choix invalide.");
                        break;
                }
            }
        }

        private void ManageArmoryMenu()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("--- Gestion de l'armurerie ---");
                Console.WriteLine("1) Lister l'armurerie");
                Console.WriteLine("2) Ajouter une arme");
                Console.WriteLine("3) Modifier une arme");
                Console.WriteLine("4) Supprimer une arme");
                Console.WriteLine("5) Importer depuis un fichier CSV");
                Console.WriteLine("6) Retour");
                Console.Write("Choix : ");
                var c = Console.ReadLine();
                switch (c)
                {
                    case "1":
                        Armory.ViewArmory();
                        break;
                    case "2":
                        Console.Write("Nom : ");
                        var name = Console.ReadLine();
                        Console.Write("MinDamage (nombre) : ");
                        var minS = Console.ReadLine();
                        Console.Write("MaxDamage (nombre) : ");
                        var maxS = Console.ReadLine();
                        Console.Write("Type (Direct/Explosive/Guided) : ");
                        var typeS = Console.ReadLine();
                        Console.Write("ReloadTime (nombre) : ");
                        var rS = Console.ReadLine();
                        try
                        {
                            var type = (EWeaponType)Enum.Parse(typeof(EWeaponType), typeS, true);
                            var w = new Weapon(name, double.Parse(minS), double.Parse(maxS), type, double.Parse(rS));
                            Armory.AddWeapon(w);
                        }
                        catch (Exception ex) { Console.WriteLine($"Erreur : {ex.Message}"); }
                        break;
                    case "3":
                        Console.Write("Nom de l'arme à modifier : ");
                        var target = Console.ReadLine();
                        var existing = Armory.GetWeaponByName(target);
                        if (existing == null) { Console.WriteLine("Arme introuvable."); break; }
                        Console.WriteLine("Laisser vide pour conserver la valeur.");
                        Console.Write($"Nom ({existing.Name}): ");
                        var nName = Console.ReadLine(); if (string.IsNullOrWhiteSpace(nName)) nName = existing.Name;
                        Console.Write($"MinDamage ({existing.MinDamage}): ");
                        var nMin = Console.ReadLine(); if (string.IsNullOrWhiteSpace(nMin)) nMin = existing.MinDamage.ToString();
                        Console.Write($"MaxDamage ({existing.MaxDamage}): ");
                        var nMax = Console.ReadLine(); if (string.IsNullOrWhiteSpace(nMax)) nMax = existing.MaxDamage.ToString();
                        Console.Write($"Type ({existing.Type}): ");
                        var nType = Console.ReadLine(); if (string.IsNullOrWhiteSpace(nType)) nType = existing.Type.ToString();
                        Console.Write($"ReloadTime ({existing.ReloadTime}): ");
                        var nR = Console.ReadLine(); if (string.IsNullOrWhiteSpace(nR)) nR = existing.ReloadTime.ToString();
                        try
                        {
                            var type = (EWeaponType)Enum.Parse(typeof(EWeaponType), nType, true);
                            var nw = new Weapon(nName, double.Parse(nMin), double.Parse(nMax), type, double.Parse(nR));
                            Armory.ModifyWeapon(existing.Name, nw);
                            Console.WriteLine("Modification effectuée.");
                        }
                        catch (Exception ex) { Console.WriteLine($"Erreur : {ex.Message}"); }
                        break;
                    case "4":
                        Console.Write("Nom de l'arme à supprimer : ");
                        var rem = Console.ReadLine();
                        var wrem = Armory.GetWeaponByName(rem);
                        if (wrem == null) { Console.WriteLine("Arme introuvable."); break; }
                        Armory.RemoveWeapon(wrem);
                        Console.WriteLine("Supprimée.");
                        break;
                    case "5":
                        Console.Write("Chemin CSV (name,min,max,type,reload) : ");
                        var path = Console.ReadLine();
                        try
                        {
                            Armory.ImportFromCsv(path);
                            Console.WriteLine("Import terminé.");
                        }
                        catch (Exception ex) { Console.WriteLine($"Erreur d'import : {ex.Message}"); }
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Choix invalide.");
                        break;
                }
            }
        }

        // --- Le reste du gameplay existant (inchangé, appelé par PlayGame) ---
        public void PlayRound()
        {
            List<Spaceship> turnOrder = new List<Spaceship>();
            bool allEnemiesDestroyed = CheckEnemiesDestroyed();
            bool allPlayersDestroyed = CheckPlayersDestroyedOrInactive();

            turnOrder = MakeTurnOrder(); // on créer l'odre de jeu des vaisseaux

            // les vaisseau à IAbility l'utilisent
            UseAbilities(turnOrder);

            foreach (Spaceship spaceship in turnOrder)
            {
                if (spaceship.IsDestroyed == false)
                {
                    if (enemies.Contains(spaceship)) // si c'est un ennemi
                    {
                        if (CheckPlayersDestroyedOrInactive() == false)
                        {
                            Spaceship playerSpaceship = GetLastLivingPlayer(); // on tire sur le joueur courant
                            spaceship.ShootTarget(playerSpaceship);
                        }
                    }
                    else if (spaceship.GetPlayerFromSpaceship(PlayersManager.players).isActive) // si c'est le joueur, et qu'il est actif
                    {
                        if (CheckEnemiesDestroyed() == false)
                        {
                            Spaceship enemySpace = GetRandomEnemy(); // on tire sur un ennemi au hasard
                            spaceship.ShootTarget(enemySpace);
                        }
                    }
                }
            }

            HealShileds(2);
            reloadAllWeapons();
            displayShipStats();
        }

        private void PlayGame()
        {
            RemoveInactivePlayersFromSpaceships();


            int nbRounds = 0;
            do
            {
                nbRounds++;
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine($"--- Round {nbRounds} ---");
                Console.WriteLine();

                if (this.CheckEnemiesDestroyed() && this.CheckPlayersDestroyedOrInactive() == false)
                {
                    Console.WriteLine();
                    Console.WriteLine("Tous les ennemis ont été détruits ! Vous avez gagné !");
                }
                else if (this.CheckEnemiesDestroyed() == false && this.CheckPlayersDestroyedOrInactive())
                {
                    Console.WriteLine();
                    Console.WriteLine("Tous les joueurs ont été détruits ! Vous avez perdu !");
                }
                else
                {
                    this.PlayRound();
                }

                Console.WriteLine("Appuyez sur Entrée pour continuer...");
                Console.ReadLine();

            } while (this.CheckEnemiesDestroyed() == false && this.CheckPlayersDestroyedOrInactive() == false);
        }

        // --- utilitaires de jeu (inchangés) ---
        private List<Spaceship> MakeTurnOrder()
        {
            List<Spaceship> turnOrder = new List<Spaceship>(); // liste dans l'ordre dans lequel les vaisseau vont tirer

            int playerProba = GetProbability(); // Position aléatoire où insérer le joueur (0 à enemies.Count)
            Spaceship playerSpaceship = GetLastLivingPlayer();

            int enemyIndex = 0;
            for (int i = 0; i <= enemies.Count; i++)
            {
                if (i == playerProba) // 1/nbEnemies chance d'insérer le joueur à cette position
                {
                    turnOrder.Add(playerSpaceship);
                }
                else if (enemyIndex < enemies.Count)
                {
                    turnOrder.Add(enemies[enemyIndex]);
                    enemyIndex++;
                }
            }

            foreach (Spaceship ship in turnOrder)
            {
                Console.WriteLine($"Ordre du tour : {ship.Name ?? ship.GetType().Name}");
            }

            return turnOrder;
        }

        private void HealShileds(int healAmount)
        {
            foreach (Spaceship spaceship in spaceships)
            {
                spaceship.RepairShield(healAmount);
            }
        }

        private bool CheckEnemiesDestroyed()
        {
            bool allEnemiesDestroyed = true;
            foreach (Spaceship enemy in enemies)
            {
                if (enemy.IsDestroyed == false)
                {
                    allEnemiesDestroyed = false;
                }
            }

            return allEnemiesDestroyed;
        }

        private bool CheckPlayersDestroyedOrInactive()
        {
            bool allPlayersDestroyed = true;
            foreach (Player player in PlayersManager.players)
            {
                if (player.BattleShip.IsDestroyed == false && player.isActive)
                {
                    allPlayersDestroyed = false;
                }
            }
            return allPlayersDestroyed;
        }

        private Spaceship GetLastLivingPlayer()
        {
            Spaceship playerSpaceship = new ViperMKII(1, 1, 1, 1, new List<Weapon>(), "default");
            foreach (Player player in PlayersManager.players)
            {
                if (player.BattleShip.IsDestroyed == false && player.isActive)
                {
                    playerSpaceship = player.BattleShip;
                }
            }

            return playerSpaceship;
        }

        private Spaceship GetRandomEnemy()
        {
            Spaceship enemySpaceship = null;
            Random rand = new Random();
            do
            {
                int enemyIndex = rand.Next(0, enemies.Count);
                enemySpaceship = enemies[enemyIndex];
            }
            while (enemySpaceship.IsDestroyed == true);

            return enemySpaceship;
        }

        private int GetProbability()
        {
            int livingEnemies = 0;
            foreach (Spaceship enemy in enemies)
            {
                if (enemy.IsDestroyed == false)
                {
                    livingEnemies++;
                }
            }

            int totalWeight = (livingEnemies * (livingEnemies + 1)) / 2;
            int randomValue = new Random().Next(0, totalWeight);

            int cumulativeWeight = 0;
            int position = 0;

            for (int i = 1; i <= livingEnemies; i++)
            {
                cumulativeWeight += i;
                if (randomValue < cumulativeWeight)
                {
                    position = i - 1;
                    i = livingEnemies + 1;
                }
            }

            return position;
        }

        private void UseAbilities(List<Spaceship> turnOrder)
        {
            foreach (Spaceship spaceship in turnOrder)
            {
                if (spaceship is IAbility abilityShip && spaceship.IsDestroyed == false)
                {
                    abilityShip.UseAbility(turnOrder);
                }
            }
        }

        private void reloadAllWeapons()
        {
            foreach (Spaceship spaceship in spaceships)
            {
                if (spaceship.IsDestroyed == false)
                {
                    spaceship.ReloadWeapons();
                }
            }
        }

        private void displayShipStats()
        {
            foreach (Spaceship ship in spaceships)
            {
                Console.Write($"{ship.Name ?? ship.GetType().Name} - Structure: {ship.CurrentStructure}/{ship.Structure} | Bouclier: {ship.CurrentShield}/{ship.Shield} " +
                    $"| Statut: {(ship.IsDestroyed ? "Détruit" : "En combat")}");

                if (ship.GetPlayerFromSpaceship(PlayersManager.players) is Player plyr)
                {
                    Console.Write($" ({(plyr.isActive ? "Actif" : "Inactif")})");
                }

                Console.WriteLine();
            }
        }

        public void ExitGame()
        {
            Console.WriteLine("Merci d'avoir joué à Space Invaders !");
            Environment.Exit(0);
        }


        public void RemoveInactivePlayersFromSpaceships()
        {
            foreach (Player player in PlayersManager.players)
            {
                if (!player.isActive)
                {
                    spaceships.Remove(player.BattleShip);
                }
            }
        }
    }
}