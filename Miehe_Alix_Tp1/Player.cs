using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Miehe_Alix_Tp1
{
    class Player
    {
        private string firstName { get; } // ne doivent pas êtres modifiables, donc getters seulement
        private string lastName { get; }
        // modifiables
        private string alias;
        private Spaceship spaceship;

        public string name;

        public Player(string aFirstName, string aLastName, string anAlias, Armory anArmory)
        {
            this.firstName = FormatName(aFirstName);
            this.lastName = FormatName(aLastName);
            this.alias = FormatName(anAlias);
            this.name = $"{firstName} {lastName}";
            //SetSpaceship( new Spaceship(100, 50, 100, 50, new List<Weapon> { new Weapon("Durrandal", 20, 32, EWeaponType.Explosive) }, anArmory) );
            SetSpaceship(new Spaceship(100, 50, 100, 50, new List<Weapon> { new Weapon("Cannon à tuer des gens", 10, 16, EWeaponType.Direct) }, anArmory));
        }

        static private string FormatName(string aName)
        {
            return $"{char.ToUpper(aName[0])}{aName.Substring(1)}";
        }

        public override string ToString()
        {
            return $"{alias} ({firstName} {lastName})";
        }

        public bool Equals(Player aPlayer)
        {
            return (this.alias == aPlayer.alias);
        }

        public void SetSpaceship(Spaceship aSpaceship)
        {
            this.spaceship = aSpaceship;
        }
    }
}
