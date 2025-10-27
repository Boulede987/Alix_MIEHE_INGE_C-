using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miehe_Alix_Tp1
{
    public class Weapon
    {
        private string name;
        private EWeaponType weaponType;

        public int MinDamage { get; }
        public int MaxDamage { get; }

        public Weapon(string name, int minDamage, int maxDamage, EWeaponType weaponType)
        {
            this.name = name;
            this.MinDamage = minDamage;
            this.MaxDamage = maxDamage;
            this.weaponType = weaponType;
        }

        public override string ToString()
        {
            return $"name: {name};\n min damage: {MinDamage};\n max damage: {MaxDamage};\n weapon type: {weaponType};\n";
        }

        public bool Equals(Weapon aWeapon)
        {
            bool rtrn = false;

            if 
                (
                this.name == aWeapon.name
                &&
                this.MinDamage == aWeapon.MinDamage
                &&
                this.MaxDamage == aWeapon.MaxDamage
                &&
                this.weaponType == aWeapon.weaponType
                )
            {
                rtrn = true;
            }

            return rtrn;
        }
    }
}
