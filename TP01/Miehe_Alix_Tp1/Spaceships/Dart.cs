using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miehe_Alix_Tp1.Spaceships
{
    internal class Dart : Spaceship
    {
        public Dart(int aMaxStructure, int aMaxShield, int aCurrentStructure, int aCurrentShield, List<Weapon> someAttachedWeapons)
            : base(10, 3, aCurrentStructure, aCurrentShield, someAttachedWeapons)
        {
            EnsureLaser();
        }

        private void EnsureLaser()
        {             
            bool hasLaser = false;
            foreach (Weapon wpon in Weapons)
            {
                if (wpon == new Weapon("Laser", 2, 3, EWeaponType.Direct, 2))
                {
                    hasLaser = true;
                }
            }

            if (!hasLaser)
            {
                AddWeapon(new Weapon("Laser", 2, 3, EWeaponType.Direct, 2));
            }
        }




        public new void ReloadWeapons()
        {
            foreach (Weapon wpon in Weapons)
            {
                wpon.TimeBeforReload = wpon.TimeBeforReload - 1;

                if (wpon.TimeBeforReload < 0 || wpon.Type == EWeaponType.Direct) // si une directe, elle peu tirer tt les tours
                {
                    wpon.TimeBeforReload = 0;
                }
            }
        }



    }
}
