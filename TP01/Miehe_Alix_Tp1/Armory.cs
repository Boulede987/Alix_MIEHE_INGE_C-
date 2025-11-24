using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miehe_Alix_Tp1
{
    public static class Armory
    {
        private static List<Weapon> WeaponList;

        //public Armory()
        //{
        //    this.Init();
        //}

        public static void Init()
        {
            WeaponList = new List<Weapon>();

            AddWeapon(new Weapon("Laser", 2, 3, EWeaponType.Direct, 2));
            AddWeapon(new Weapon("Hammer", 1, 8, EWeaponType.Explosive, 2));
            AddWeapon(new Weapon("Torpille", 3, 3, EWeaponType.Guided, 2));
            AddWeapon(new Weapon("Mitrailleuse", 6, 8, EWeaponType.Direct, 1));
            AddWeapon(new Weapon("EMG", 1, 7, EWeaponType.Explosive, 1.5));
            AddWeapon(new Weapon("Missile", 4, 100, EWeaponType.Guided, 4));
        }

        public static void ViewArmory()
        {
            Console.WriteLine("Armes : ");
            foreach (Weapon wpon in WeaponList)
            {
                Console.WriteLine(wpon.ToString());
            }
            Console.WriteLine();
        }

        public static void AddWeapon(Weapon aWeapon)
        {
            WeaponList.Add(aWeapon);
            Console.WriteLine($"{aWeapon.Name} a été ajouté à l'armurerie.");
        }

        public static void RemoveWeapon(Weapon aWeapon)
        {
            WeaponList.Remove(aWeapon);
            Console.WriteLine($"{aWeapon.Name} a été retiré de l'armurerie.");
        }

        public static bool Contains(Weapon aWeapon)
        {
            bool rtrn = false;
            foreach (Weapon wpon in WeaponList)
            {
                if (wpon.Equals(aWeapon))
                {
                    rtrn = true;
                }
            }

            return rtrn;
        }


        //premet de retourner les 5 armes avec les plus gros dégats moyens
        public static List<Weapon> GetTop5WeaponsByAverageDamage()
        {
            return WeaponList
                .OrderByDescending(w => w.AverageDamage)
                .Take(5)
                .ToList();
        }


        //premet de retourner les 5 armes avec les plus gros dégats minimums
        public static List<Weapon> GetTop5WeaponsByMinDamage()
        {
            return WeaponList
                .OrderByDescending(w => w.MinDamage)
                .Take(5)
                .ToList();
        }
    }
}
