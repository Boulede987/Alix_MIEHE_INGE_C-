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
        private Armory armory;

        public SpaceInvaders()
        {
            //
        }

        public void Main()
        {
            SpaceInvaders aSpaceInvaders = new SpaceInvaders();

            aSpaceInvaders.Init();
            aSpaceInvaders.ViewPlayers();
            aSpaceInvaders.armory.ViewArmory();
        }

        private void Init()
        {
            this.armory = new Armory();
            this.players = new List<Player>();

            players.Add(new Player("jhon", "sigma", "Ancutraxxe", this.armory));
            players.Add(new Player("bob", "bobby", "Degeulaxxe", this.armory));
            players.Add(new Player("alberto", "ultima", "Putraxxe", this.armory));

            armory.AddWeapon(new Weapon("Dégomatron 9000", 999999, 9999999, EWeaponType.Explosive));

            // c mon petit cousin il pue la merde
            players[2].SetSpaceship(new Spaceship(999999999, 999999999, 999999999, 999999999, new List<Weapon> { new Weapon("Dégomatron 9000", 999999, 9999999, EWeaponType.Explosive) }, this.armory));
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
