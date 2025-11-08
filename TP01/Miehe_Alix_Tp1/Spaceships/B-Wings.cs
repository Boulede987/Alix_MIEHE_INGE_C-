using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miehe_Alix_Tp1.Spaceships
{
    internal class B_Wings : Spaceship
    {
        public B_Wings(int aMaxStructure, int aMaxShield, int aCurrentStructure, int aCurrentShield, List<Weapon> someAttachedWeapons)
            : base(30, 0, 30, 0, someAttachedWeapons)
        {
            EnsureHammer();
            this.Name = "B-Wings";
        }

        private void EnsureHammer()
        {
            bool hasLaser = false;
            foreach (Weapon wpon in Weapons)
            {
                if (wpon == new Weapon("Hammer", 1, 8, EWeaponType.Explosive, 2))
                {
                    hasLaser = true;
                }
            }

            if (!hasLaser)
            {
                AddWeapon(new Weapon("Hammer", 1, 8, EWeaponType.Explosive, 2));
            }
        }




        public new void ReloadWeapons()
        {
            foreach (Weapon wpon in Weapons)
            {
                wpon.TimeBeforReload = wpon.TimeBeforReload - 1;

                if (wpon.TimeBeforReload < 0 || wpon.Type == EWeaponType.Explosive) // si une directe, elle peu tirer tt les tours
                {
                    wpon.TimeBeforReload = 0;
                }
            }

            Console.WriteLine("Les armes du B-Wings se rechargent !");
        }


    }
}
