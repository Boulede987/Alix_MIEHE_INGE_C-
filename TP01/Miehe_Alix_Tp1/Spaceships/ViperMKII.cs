using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miehe_Alix_Tp1.Spaceships
{
    internal class ViperMKII : Spaceship
    {
        public ViperMKII(int aMaxStructure, int aMaxShield, int aCurrentStructure, int aCurrentShield, List<Weapon> someAttachedWeapons, string aName)
            : base(10, 15, 10, 15,
                  new List<Weapon>
                  {
                    new Weapon("Mitrailleuse", 6, 8, EWeaponType.Direct, 1),
                    new Weapon("EMG", 1, 7, EWeaponType.Explosive, 1.5),
                    new Weapon("Missile", 4, 100, EWeaponType.Guided, 4)
                   })
        {
            this.BelongsPlayer = true;
            this.Name = $"Viper MK II - {aName}";
        }


        public override void ShootTarget(Spaceship target) // Tirer avec une arme spécifique
        {

            // on choisit une arme au hasard parmi celles du vaisseau
            // pour qe le joueur n'utilise pas toujours la mitrailleuse
            List<Weapon> availableWeapons = new List<Weapon>();

            foreach (Weapon weapon in this.Weapons)
            {
                if (weapon.TimeBeforReload == 0)
                {
                    availableWeapons.Add(weapon);
                }
            }

            if (availableWeapons.Count > 0)
            {
                Weapon weaponToUse = availableWeapons[new Random().Next(availableWeapons.Count)]; // on utilise una arme au hasard
                Console.Write($"{Name} tire avec son arme {weaponToUse.Name} :");
                target.TakeDamages(weaponToUse.Shoot());
            }
        }

    }
}
