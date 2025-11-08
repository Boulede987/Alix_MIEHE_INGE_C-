using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miehe_Alix_Tp1.Spaceships
{
    internal class Rocinante : Spaceship
    {
        public Rocinante(int aMaxStructure, int aMaxShield, int aCurrentStructure, int aCurrentShield, List<Weapon> someAttachedWeapons)
            : base(3, 5, 3, 5, someAttachedWeapons)
        {
            EnsureTorpille();
        }

        private void EnsureTorpille()
        {
            bool hasLaser = false;
            foreach (Weapon wpon in Weapons)
            {
                if (wpon == new Weapon("Torpille", 3, 3, EWeaponType.Guided, 2))
                {
                    hasLaser = true;
                }
            }

            if (!hasLaser)
            {
                AddWeapon(new Weapon("Hammer", 1, 8, EWeaponType.Explosive, 2));
            }
        }




        public new void TakeDamages(double damages)
        {
            Random rand = new Random();
            if (rand.Next(0, 100) < 50) // 50% de chance d'esquiver, deux fois plus qu'un autre ou c'est 100% du coup
            {
                base.TakeDamages(damages);
            }
        }


    }
}
