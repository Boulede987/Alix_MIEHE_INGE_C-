using SpaceInvadersArmory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miehe_Alix_Tp1
{
    public class Weapon : IWeapon
    {
        public string Name { get; set; }
        public EWeaponType Type { get; set; }
        public double MinDamage { get; set; }
        public double MaxDamage { get; set; }
        public double AverageDamage
        {
            get
            {
                return (MinDamage + MaxDamage) / 2;
            }
        }
        public double ReloadTime { get; set; }
        public double TimeBeforReload { get; set; } // permet de savoir si l'arme est utilisable
        public bool IsReload
        {
            get
            {
                return TimeBeforReload > 0;
            }
        }

        public Weapon()
        {
            //
        }

        public Weapon(string name, double minDamage, double maxDamage, EWeaponType weaponType, double reloadTime)
        {
            this.Name = name;
            this.MinDamage = minDamage;
            this.MaxDamage = maxDamage;
            this.Type = weaponType;
            this.ReloadTime = reloadTime;
            this.TimeBeforReload = 0;
        }

        public override string ToString()
        {
            return $"name: {Name};\n min damage: {MinDamage};\n max damage: {MaxDamage};\n weapon type: {Type};\n";
        }

        public bool Equals(Weapon aWeapon)
        {
            bool rtrn = false;

            if 
                (
                this.Name == aWeapon.Name
                &&
                this.MinDamage == aWeapon.MinDamage
                &&
                this.MaxDamage == aWeapon.MaxDamage
                &&
                this.Type == aWeapon.Type
                )
            {
                rtrn = true;
            }

            return rtrn;
        }



        public double Shoot()
        {
            double rtrn = 0;

            if (this.TimeBeforReload != 0)
            {
                rtrn = 0;
            }
            else
            {
                Random rand = new Random();
                rtrn = rand.Next((int)MinDamage, (int)MaxDamage);
                this.TimeBeforReload = this.ReloadTime;

                switch(this.Type)
                {
                    case EWeaponType.Direct:
                        if ( rand.Next(1, 11) == 1 ) // 1 chance sur 10 de rater
                        {
                            rtrn = 0;
                        }
                        break;
                    case EWeaponType.Explosive:
                        if (rand.Next(1, 5) == 1) // 1 chance sur 10 de rater
                        {
                            rtrn = 0;
                        } // double le temps de rechargement
                        this.TimeBeforReload = this.ReloadTime * 2;
                            break;
                    case EWeaponType.Guided:
                        rtrn = this.MinDamage; // touche toujours, mais avec degats minimum
                        break;
                }
            }

            return rtrn;
        }




    }
}
