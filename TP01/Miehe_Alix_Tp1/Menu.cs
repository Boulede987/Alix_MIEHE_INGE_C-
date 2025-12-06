//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Miehe_Alix_Tp1
//{
//    static class Menu
//    {
//        public static void AfficherMenuPrincipale()
//        {
//            Console.WriteLine("===== Menu Principal =====");
//            Console.WriteLine("1. Gestion des joueurs");
//            Console.WriteLine("2. Gestion des armes");
//            Console.WriteLine("3. Gestion des vaisseau");
//            Console.WriteLine("4. Jouer");
//            Console.WriteLine("5. Quitter");
//            Console.WriteLine("===========================");
//            Console.Write("Veuillez choisir une option (1-5) : ");
//        }

//        public static void MenuPrincipale()
//        {
//            while (true)
//            {
//                AfficherMenuPrincipale();
//                int choix = ObtenirChoixUtilisateur(1, 5);
//                switch (choix)
//                {
//                    case 1:
//                        MenuGestionJoueurs();
//                        break;
//                    case 2:
//                        this.MenuGestionArmes();
//                        break;
//                    case 3:
//                        this.MenuGestionVaisseaux();
//                        break;
//                    case 4:
//                        break;
//                    case 5:
//                        Console.WriteLine("Merci d'avoir joué ! Au revoir !");
//                        return;
//                }
//            }
//        }



//        //affichage du menu de gestion des joueurs
//        public static void AfficherMenuGestionJoueurs()
//        {
//            Console.WriteLine("===== Gestion des Joueurs =====");
//            Console.WriteLine("1. Ajouter un joueur");
//            Console.WriteLine("2. Supprimer un joueur");
//            Console.WriteLine("3. Ajouter joueur courant");
//            Console.WriteLine("4. Supprimer joueur courant");
//            Console.WriteLine("5. Retour au menu principal");
//            Console.WriteLine("===============================");
//            Console.Write("Veuillez choisir une option (1-5) : ");
//        }

//        //affichage du menu de gestion des armes
//        public static void AfficherMenuGestionArmes()
//        {
//            Console.WriteLine("===== Gestion des Armes =====");
//            Console.WriteLine("1. Ajouter une arme");
//            Console.WriteLine("2. Supprimer une arme");
//            Console.WriteLine("3. Modifier une arme");
//            Console.WriteLine("4. Retour au menu principal");
//            Console.WriteLine("=============================");
//            Console.Write("Veuillez choisir une option (1-4) : ");
//        }

//        //affichage du menu de gestion des vaisseaux
//        public static void AfficherMenuGestionVaisseaux()
//        {
//            Console.WriteLine("===== Gestion des Vaisseaux =====");
//            Console.WriteLine("1. Modifier un vaisseau");
//            Console.WriteLine("2. Retour au menu principal");
//            Console.WriteLine("=================================");
//            Console.Write("Veuillez choisir une option (1-2) : ");
//        }

//        //menu de modific ation des vaisseaux
//        public static void AfficherMenuModificationVaisseaux()
//        {
//            Console.WriteLine("===== Modification des Vaisseaux =====");
//            Console.WriteLine("1. Ajouter une Arme");
//            Console.WriteLine("2. Supprimer une arme");
//            Console.WriteLine("3. Retour au menu de gestion des vaisseaux");
//            Console.WriteLine("=======================================");
//            Console.Write("Veuillez choisir une option (1-3) : ");
//        }

//        public static int ObtenirChoixUtilisateur(int min, int max)
//        {
//            int choix;
//            while (true)
//            {
//                string input = Console.ReadLine();
//                if (int.TryParse(input, out choix) && choix >= min && choix <= max)
//                {
//                    break;
//                }
//                else
//                {
//                    Console.Write("Choix invalide. Veuillez entrer un nombre entre 1 et 4 : ");
//                }
//            }
//            return choix;
//        }

//        public static string ObtenirChaineUtilisateur(string prompt)
//        {
//            Console.Write(prompt);
//            return Console.ReadLine();
//        }



//    }
//}
