using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miehe_Alix_Tp1
{
    static class PlayersManager
    {
        public static List<Player> players; // liste de joueurs

        public static List<Player> GetPlayers()
        {
            return players;
        }

        public static void ViewPlayers()
        {
            Console.WriteLine("Joueurs : ");
            foreach (Player plyr in players)
            {
                Console.WriteLine(plyr.ToString());
            }
            Console.WriteLine();
        }

        public static void AddPlayer(Player aPlayer)
        {
            players.Add(aPlayer);
        }

        public static void RemovePlayer(Player aPlayer)
        {
            players.Remove(aPlayer);
        }

        public static void SetActivePlayer(Player aPlayer)
        {
            foreach (Player plyr in players)
            {
                if (plyr.Equals(aPlayer))
                {
                    plyr.isActive = true;
                }
                else
                {
                    plyr.isActive = false;
                }
            }
        }

        public static void SetAllPlayersAsActive()
        {
            foreach (Player plyr in players)
            {
                plyr.isActive = true;
            }
        }





    }
}
