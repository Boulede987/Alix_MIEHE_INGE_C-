using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Miehe_Alix_Tp1
{
    public static class Armory
    {
        private static List<Weapon> WeaponList;

        public static void Init()
        {
            WeaponList = new List<Weapon>();

            AddWeapon(new Weapon("Laser", 2, 3, EWeaponType.Direct, 2));
            AddWeapon(new Weapon("Hammer", 1, 8, EWeaponType.Explosive, 2));
            AddWeapon(new Weapon("Torpille", 3, 3, EWeaponType.Guided, 2));
            AddWeapon(new Weapon("Mitrailleuse", 6, 8, EWeaponType.Direct, 1));
            AddWeapon(new Weapon("EMG", 1, 7, EWeaponType.Explosive, 1.5));
            AddWeapon(new Weapon("Missile", 4, 100, EWeaponType.Guided, 4));

            Console.WriteLine("[Armory] initialisée avec " + WeaponList.Count + " armes.");
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

        // Retourne l'arme par nom (exact)
        public static Weapon GetWeaponByName(string name)
        {
            return WeaponList.FirstOrDefault(w => string.Equals(w.Name, name, StringComparison.OrdinalIgnoreCase));
        }

        // Modifie une arme existante (recherche par nom original)
        public static void ModifyWeapon(string originalName, Weapon newWeapon)
        {
            var existing = GetWeaponByName(originalName);
            if (existing == null) throw new Exception("Arme introuvable");
            // Remplacer l'objet (conserver position)
            int idx = WeaponList.IndexOf(existing);
            WeaponList[idx] = newWeapon;
            Console.WriteLine($"Arme '{originalName}' modifiée -> '{newWeapon.Name}'");
        }

        // Import simple CSV : name,min,max,type,reload
        public static void ImportFromCsv(string path)
        {
            if (!File.Exists(path)) throw new FileNotFoundException("Fichier introuvable", path);
            var lines = File.ReadAllLines(path);
            foreach (var line in lines)
            {
                var trimmed = line.Trim();
                if (string.IsNullOrWhiteSpace(trimmed)) continue;
                // skip comments starting with #
                if (trimmed.StartsWith("#")) continue;
                var parts = trimmed.Split(',');
                if (parts.Length < 5) continue;
                var name = parts[0].Trim();
                if (!double.TryParse(parts[1], out double min)) continue;
                if (!double.TryParse(parts[2], out double max)) continue;
                var typeStr = parts[3].Trim();
                if (!Enum.TryParse<EWeaponType>(typeStr, true, out var type)) continue;
                if (!double.TryParse(parts[4], out double reload)) continue;
                AddWeapon(new Weapon(name, min, max, type, reload));
            }
            Console.WriteLine($"[Armory] Import CSV terminé : {Armory.WeaponList.Last().ToString()}");
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

        public static void ViewTopWeapons()
        {
            Console.WriteLine("Top 5 des armes par dégâts moyens :");
            var topAverageDamageWeapons = GetTop5WeaponsByAverageDamage();
            foreach (var weapon in topAverageDamageWeapons)
            {
                Console.WriteLine(weapon.ToString());
            }
            Console.WriteLine("Top 5 des armes par dégâts minimums :");
            var topMinDamageWeapons = GetTop5WeaponsByMinDamage();
            foreach (var weapon in topMinDamageWeapons)
            {
                Console.WriteLine(weapon.ToString());
            }
        }
    }
}