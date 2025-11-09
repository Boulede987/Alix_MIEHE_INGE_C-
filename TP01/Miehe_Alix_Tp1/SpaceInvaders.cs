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

        private List<Player> players;

        private List<Spaceship> spaceships;
        private List<Spaceship> enemies;

        public SpaceInvaders()
        {
            //
        }

        public void Main()
        {
            SpaceInvaders aSpaceInvaders = new SpaceInvaders();

            Armory.Init();

            aSpaceInvaders.Init();
            aSpaceInvaders.ViewPlayers();
            Armory.ViewArmory();

            int nbRounds = 0;
            do
            {


                nbRounds++;
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine($"--- Round {nbRounds} ---");
                Console.WriteLine();
                aSpaceInvaders.PlayRound();



                if (aSpaceInvaders.CheckEnemiesDestroyed() && aSpaceInvaders.CheckPlayersDestroyed() == false)
                {
                    Console.WriteLine();
                    Console.WriteLine("Tous les ennemis ont été détruits ! Vous avez gagné !");
                }
                else if (aSpaceInvaders.CheckEnemiesDestroyed() == false && aSpaceInvaders.CheckPlayersDestroyed())
                {
                    Console.WriteLine();
                    Console.WriteLine("Tous les joueurs ont été détruits ! Vous avez perdu !");
                }



                Console.ReadLine();

            } while (aSpaceInvaders.CheckEnemiesDestroyed() == false && aSpaceInvaders.CheckPlayersDestroyed() == false);
        }

        private void Init()
        {
            this.players = new List<Player>();

            players.Add(new Player("jhon", "sigma", "Ancutraxxe"));
            players.Add(new Player("bob", "bobby", "Degeulaxxe"));
            players.Add(new Player("alberto", "ultima", "Putraxxe"));

            Armory.AddWeapon(new Weapon("Dégomatron 9000", 999999, 9999999, EWeaponType.Explosive, 0));

            // c mon petit cousin il pue la merde
            players[2].SetSpaceship(new ViperMKII(999999999, 999999999, 999999999, 999999999, new List<Weapon> { new Weapon("Dégomatron 9000", 999999, 9999999, EWeaponType.Explosive, 0) }, players[2].Alias));



            this.spaceships = new List<Spaceship>();
            spaceships.Add(players[0].BattleShip);
            spaceships.Add(players[1].BattleShip);
            spaceships.Add(players[2].BattleShip); // joueur

            this.enemies = new List<Spaceship>();


            enemies.Add(new Dart(1, 1, 1, 1, new List<Weapon>())); // enemies
            enemies.Add(new F_18(1, 1, 1, 1, new List<Weapon>()));
            enemies.Add(new B_Wings(1, 1, 1, 1, new List<Weapon>()));
            enemies.Add(new Rocinante(1, 1, 1, 1, new List<Weapon>()));
            enemies.Add(new Tardis(1, 1, 1, 1, new List<Weapon>()));

            spaceships.Add(enemies[0]); // enemies
            spaceships.Add(enemies[1]);
            spaceships.Add(enemies[2]);
            spaceships.Add(enemies[3]);
            spaceships.Add(enemies[4]);

            // show enemies
            foreach (Spaceship enemy in enemies)
            {
                Console.WriteLine($"Ennemi ajouté : {enemy.Name}");
            }

        }

        public List<Player> GetPlayers()
        {
            return players;
        }

        private void ViewPlayers()
        {
            Console.WriteLine("Joueurs : ");
            foreach (Player plyr in players)
            {
                Console.WriteLine(plyr.ToString());
            }
            Console.WriteLine();
        }


        public void PlayRound()
        {
            List<Spaceship> turnOrder = new List<Spaceship>();
            // on verifie que les ennemies ne sontpas tous détruits
            bool allEnemiesDestroyed = CheckEnemiesDestroyed();
            bool allPlayersDestroyed = CheckPlayersDestroyed();


            //a.    Elle devra faire jouer chaque vaisseau l’un après l’autre dans l’ordre de la liste d'ennemis.
            

            turnOrder = MakeTurnOrder();


            // les vaisseau à IAbility l'utilisent
            UseAbilities(turnOrder);


            foreach (Spaceship spaceship in turnOrder)
            {
                if (spaceship.IsDestroyed == false)
                {
                    if (enemies.Contains(spaceship)) // si c'est un ennemi
                    {
                        if (CheckPlayersDestroyed() == false)
                        {
                            Spaceship playerSpaceship = GetLastLivingPlayer();
                            spaceship.ShootTarget(playerSpaceship);
                        }
                    }
                    else // si c'est le joueur
                    {
                        if (CheckEnemiesDestroyed() == false)
                        {
                            // on choisit une arme au hasard parmi celles du vaisseau
                            // pour qe le joueur n'utilise pas toujours la mitrailleuse
                            Weapon weaponToUse = spaceship.Weapons[new Random().Next(spaceship.Weapons.Count)];

                            Spaceship enemySpaceship = GetRandomEnemy();
                            ((ViperMKII)spaceship).ShootTarget(enemySpaceship, weaponToUse); // le vaisseau joueur est toujours un ViperMKII
                        }
                    }
                }
            }



            //d.    Chaque début de tour les vaisseaux ayant perdu des points de bouclier en regagne maximum 2.
            HealShileds(2);


            foreach (Spaceship spaceship in turnOrder)
            {
                if (spaceship.IsDestroyed == false)
                {
                    if (enemies.Contains(spaceship)) // si c'est un ennemi
                    {
                        if (CheckPlayersDestroyed() == false)
                        {
                            Spaceship playerSpaceship = GetLastLivingPlayer();
                            spaceship.ReloadWeapons();
                        }
                    }
                    else // si c'est le joueur
                    {
                        if (CheckEnemiesDestroyed() == false)
                        {
                            Spaceship enemySpaceship = GetRandomEnemy();
                            spaceship.ReloadWeapons();
                        }
                    }
                }
            }




            displayShipStats();

        }


        private List<Spaceship> MakeTurnOrder()
        {
            List<Spaceship> turnOrder = new List<Spaceship>();
            int playerProba = GetProbability(); // Position aléatoire où insérer le joueur (0 à enemies.Count)
            Spaceship playerSpaceship = GetLastLivingPlayer();

            int enemyIndex = 0;
            for (int i = 0; i <= enemies.Count; i++)
            {
                if (i == playerProba)
                {
                    // Insérer le joueur à cette position
                    turnOrder.Add(playerSpaceship);
                }
                else if (enemyIndex < enemies.Count)
                {
                    // Ajouter l'ennemi suivant
                    turnOrder.Add(enemies[enemyIndex]);
                    enemyIndex++;
                }
            }

            //show turn order
            foreach (Spaceship ship in turnOrder)
            {
                Console.WriteLine($"Ordre du tour : {ship.Name}");
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

        private bool CheckPlayersDestroyed()
        {
            bool allPlayersDestroyed = true;
            foreach (Player player in players)
            {
                if (player.BattleShip.IsDestroyed == false)
                {
                    allPlayersDestroyed = false;
                }
            }
            return allPlayersDestroyed;
        }


        private Spaceship GetLastLivingPlayer()
        {
            // les ennemies tirent sur le premier joueur vivant
            Spaceship playerSpaceship = new ViperMKII(1, 1, 1, 1, new List<Weapon>(), "default");
            // on recup le dernier joueur vivant, qui est le premier à être visé par les ennemies
            foreach (Player player in players)
            {
                if (player.BattleShip.IsDestroyed == false)
                {
                    playerSpaceship = player.BattleShip;
                }
            }

            return playerSpaceship;
        }


        private Spaceship GetRandomEnemy()
        {
            Spaceship enemySpaceship = null; // Initialisation à null pour éviter CS0165
            Random rand = new Random();
            do
            {
                int enemyIndex = rand.Next(0, enemies.Count); // on choisit un ennemi au hasard
                enemySpaceship = enemies[enemyIndex];
            }
            while (enemySpaceship.IsDestroyed == true); // tant qu'on a pas un vaisseau ennemi vivant

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
            Random rand = new Random();
            int playerChance = rand.Next(1, livingEnemies + 1); // on tire un nombre entre 1 et le nombre d'ennemis vivants

            return playerChance;
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



        private void displayShipStats()
        {
            foreach (Spaceship ship in spaceships)
            {
                Console.WriteLine($"{ship.Name} - Structure: {ship.CurrentStructure}/{ship.Structure} | Bouclier: {ship.CurrentShield}/{ship.Shield} " +
                    $"| Statut: {(ship.IsDestroyed ? "Détruit" : "En combat")}");
            }
        }
    }
}
