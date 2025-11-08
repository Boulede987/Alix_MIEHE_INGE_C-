using Miehe_Alix_Tp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miehe_Alix_Tp1.Spaceships
{
    internal class F_18 : Spaceship, IAbility
    {
        public F_18(int aMaxStructure, int aMaxShield, int aCurrentStructure, int aCurrentShield, List<Weapon> someAttachedWeapons)
            : base(15, 0, 15, 0, new List<Weapon>())
        {
            this.Name = "F-18 Hornet";
        }

        public void UseAbility(List<Spaceship> spaceships)
        {

            Console.WriteLine($"{Name} utilise son ability : Suicide !");

            int i = 0;
            foreach (Spaceship ship in spaceships)
            {
                if (ship == this)
                {
                    if (spaceships[i+1].BelongsPlayer)
                    {
                        spaceships[i+1].TakeDamages(10);
                    }

                    if (spaceships[i-1].BelongsPlayer)
                    {
                        spaceships[i-1].TakeDamages(10);
                    }
                }
                i++;
            }

        }
    }
}
