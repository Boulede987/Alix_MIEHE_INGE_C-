using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miehe_Alix_Tp1.Spaceships
{
    internal class ViperMKII : Spaceship
    {
        public ViperMKII(int aMaxStructure, int aMaxShield, int aCurrentStructure, int aCurrentShield, List<Weapon> someAttachedWeapons)
            : base(10, 15, aCurrentStructure, aCurrentShield, 
                  new List<Weapon> 
                  {
                    new Weapon("Mitrailleuse", 6, 8, EWeaponType.Direct, 1),
                    new Weapon("EMG", 1, 7, EWeaponType.Explosive, 1.5),
                    new Weapon("Missile", 4, 100, EWeaponType.Guided, 4)
                   })
        {
            //
        }


        public void ShootTarget(Spaceship target, Weapon weapon)
        {
            target.TakeDamages(weapon.Shoot());
        }



    }
}
