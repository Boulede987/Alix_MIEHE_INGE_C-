using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Miehe_Alix_Tp1.Spaceships
{
    public abstract class Spaceship : ISpaceship
    {
        public string Name { get; set; }
        public double CurrentStructure { get; set; }
        public double CurrentShield { get; set; }
        public double Structure { get; set; }
        public double Shield { get; set; }
        public List<Weapon> Weapons { get; }
        public int MaxWeapons { get; }
        public double AverageDamages 
        {
            get
            {
                double avg = 0;
                foreach (Weapon wpon in Weapons)
                {
                    avg += wpon.AverageDamage;
                }

                avg = avg / Weapons.Count;

                return avg;
            }
        }
        public bool IsDestroyed { get; }
        public bool BelongsPlayer { get; }

        public Spaceship(int aMaxStructure, int aMaxShield, int aCurrentStructure, int aCurrentShield, List<Weapon> someAttachedWeapons)
        {
            Structure = aMaxStructure;
            Shield = aMaxShield;
            CurrentStructure = aCurrentStructure;
            CurrentShield = aCurrentShield;
            Weapons = new List<Weapon>();
            MaxWeapons = 3;

            foreach (Weapon wpon in someAttachedWeapons)
            {
                AddWeapon(wpon);
            }
        }

        public void TakeDamages(double damages)
        {
            if (CurrentShield >= damages)
            {
                CurrentShield -= damages;
            }
            else
            {
                double remainingDamages = damages - CurrentShield;
                CurrentShield = 0;
                if (CurrentStructure >= remainingDamages)
                {
                    CurrentStructure -= remainingDamages;
                }
                else
                {
                    CurrentStructure = 0;
                }
            }
        }

        public void RepairShield(double repair)
        {
            if (CurrentShield + repair <= Shield)
            {
                CurrentShield += repair;
            }
            else
            {
                CurrentShield = Shield;
            }
        }

        public void ShootTarget(Spaceship target)
        {
            double totalDamage = 0;
            foreach (Weapon wpon in Weapons)
            {
                totalDamage += wpon.AverageDamage;
            }
            target.TakeDamages(totalDamage);
        }

        public void AddWeapon(Weapon aWeapon)
        {
            if (Weapons.Count > MaxWeapons)
            {
                throw new Exception("Trop de dakka!");
            }
            else if (  Armory.Contains(aWeapon) == false )
            {
                throw new ArmoryException();
            }
            else
            {
                Weapons.Add(aWeapon);
            }
        }

        public void RemoveWeapon(Weapon aWeapon)
        {
            if (Weapons.Contains(aWeapon))
            {
                Weapons.Remove(aWeapon);
            }
            else
            {
                throw new Exception("L'arme n'est pas attachée.");
            }
        }

        public void ClearWeapons()
        {
            Weapons.Clear();
        }

        public void ViewWeapons()
        {
            foreach(Weapon wpon in Weapons)
            {
                Console.WriteLine(wpon.ToString());
            }
        }

        public void ViewShip()
        {
            Console.WriteLine("Informations du vaisseau : ");
            ViewWeapons();
            Console.WriteLine(
                $"maxStructure: {Structure};\n " +
                $"maxShield: {Shield};\n " +
                $"currentStructure: {CurrentStructure};\n " +
                $"currentShield: {CurrentShield};\n" +
                $"maximumNumberOfAttachedWeapons: {MaxWeapons}"
            );
        }

        public void ReloadWeapons()
        {
            foreach (Weapon wpon in Weapons)
            {
                wpon.TimeBeforReload = 0;
            }
        }
    }
}
