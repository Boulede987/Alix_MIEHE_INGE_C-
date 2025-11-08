using Miehe_Alix_Tp1.Spaceships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miehe_Alix_Tp1
{
    public class SpaceInvaders
    {

        private List<Player> players;

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
        }

        private void Init()
        {
            this.players = new List<Player>();

            players.Add(new Player("jhon", "sigma", "Ancutraxxe"));
            players.Add(new Player("bob", "bobby", "Degeulaxxe"));
            players.Add(new Player("alberto", "ultima", "Putraxxe"));

            Armory.AddWeapon(new Weapon("Dégomatron 9000", 999999, 9999999, EWeaponType.Explosive, 0));

            // c mon petit cousin il pue la merde
            players[2].SetSpaceship(new ViperMKII(999999999, 999999999, 999999999, 999999999, new List<Weapon> { new Weapon("Dégomatron 9000", 999999, 9999999, EWeaponType.Explosive, 0) }));
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
    }
}
