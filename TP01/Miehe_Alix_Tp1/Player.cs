using Miehe_Alix_Tp1.Spaceships;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Miehe_Alix_Tp1
{
    public class Player : IPlayer
    {
        private string firstName { get; } // ne doivent pas êtres modifiables, donc getters seulement
        private string lastName { get; }
        public string Alias { get; }
        public string Name { get; }

        public Spaceship BattleShip { get; set; }

        public Player(string aFirstName, string aLastName, string anAlias)
        {
            this.firstName = FormatName(aFirstName);
            this.lastName = FormatName(aLastName);
            this.Alias = FormatName(anAlias);
            this.Name = $"{firstName} {lastName}";
            //SetSpaceship( new Spaceship(100, 50, 100, 50, new List<Weapon> { new Weapon("Durrandal", 20, 32, EWeaponType.Explosive) }, anArmory) );
            SetSpaceship(new ViperMKII(100, 50, 100, 50, new List<Weapon> { new Weapon("Missile", 4, 100, EWeaponType.Guided, 4) }, this.Alias));
        }

        static private string FormatName(string aName)
        {
            return $"{char.ToUpper(aName[0])}{aName.Substring(1)}";
        }

        public override string ToString()
        {
            return $"{Alias} ({firstName} {lastName})";
        }

        public bool Equals(Player aPlayer)
        {
            return (this.Alias == aPlayer.Alias);
        }

        public void SetSpaceship(Spaceship aSpaceship)
        {
            this.BattleShip = aSpaceship;
            Console.WriteLine($"{Alias} a choisi le vaisseau {aSpaceship.Name} !");
        }
    }
}
