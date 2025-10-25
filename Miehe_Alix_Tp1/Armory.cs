using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miehe_Alix_Tp1
{
    class Armory
    {
        private List<Weapon> WeaponList;

        public Armory()
        {
            this.Init();
        }

        private void Init()
        {
            this.WeaponList = new List<Weapon>();

            this.AddWeapon(new Weapon("Cannon à tuer des gens", 10, 16, EWeaponType.Direct));
            this.AddWeapon(new Weapon("Cannon à tuer beaucoup de gens", 20, 40, EWeaponType.Explosive));
            this.AddWeapon(new Weapon("Cannon à tuer un gen en particulier", 100, 400, EWeaponType.Guided));
        }

        public void ViewArmory()
        {
            Console.WriteLine("Armes : ");
            foreach (Weapon wpon in this.WeaponList)
            {
                Console.WriteLine(wpon.ToString());
            }
            Console.WriteLine();
        }

        public void AddWeapon(Weapon aWeapon)
        {
            this.WeaponList.Add(aWeapon);
        }

        public void RemoveWeapon(Weapon aWeapon)
        {
            this.WeaponList.Remove(aWeapon);
        }

        public bool Contains(Weapon aWeapon)
        {
            bool rtrn = false;
            foreach (Weapon wpon in this.WeaponList)
            {
                if (wpon.Equals(aWeapon))
                {
                    rtrn = true;
                }
            }

            return rtrn;
        }
    }
}
