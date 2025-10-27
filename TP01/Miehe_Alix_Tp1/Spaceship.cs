using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Miehe_Alix_Tp1
{
    public class Spaceship
    {
        private int maxStructure;
        private int maxShield;
        private int currentStructure;
        private int currentShield;
        private List<Weapon> attachedWeapons;
        private int maximumNumberOfAttachedWeapons;

        public bool isDetsroyed { get; set; }

        public Spaceship(int aMaxStructure, int aMaxShield, int aCurrentStructure, int aCurrentShield, List<Weapon> someAttachedWeapons, Armory anArmory)
        {
            this.maxStructure = aMaxStructure;
            this.maxShield = aMaxShield;
            this.currentStructure = aCurrentStructure;
            this.currentShield = aCurrentShield;
            this.attachedWeapons = new List<Weapon>();
            this.maximumNumberOfAttachedWeapons = 3;

            foreach (Weapon wpon in someAttachedWeapons)
            {
                AddWeapon(wpon, anArmory);
            }
        }

        public void AddWeapon(Weapon aWeapon, Armory anArmory)
        {
            if (attachedWeapons.Count > maximumNumberOfAttachedWeapons)
            {
                throw new Exception("Trop de dakka!");
            }
            else if (  anArmory.Contains(aWeapon) == false )
            {
                throw new ArmoryException();
            }
            else
            {
                attachedWeapons.Add(aWeapon);
            }
        }

        public void RemoveWeapon(Weapon aWeapon)
        {
            if (attachedWeapons.Contains(aWeapon))
            {
                attachedWeapons.Remove(aWeapon);
            }
            else
            {
                throw new Exception("L'arme n'est pas attachée.");
            }
        }

        public void ClearWeapons()
        {
            this.attachedWeapons.Clear();
        }

        public void ViewWeapons()
        {
            foreach(Weapon wpon in attachedWeapons)
            {
                Console.WriteLine(wpon.ToString());
            }
        }

        public double AverageDamages()
        {
            float avg = 0;
            foreach(Weapon wpon in attachedWeapons)
            {
                avg += (wpon.MinDamage + wpon.MaxDamage) / 2;
            }

            avg = avg / attachedWeapons.Count;

            return avg;
        }

        public void ViewSship()
        {
            Console.WriteLine("Informations du vaisseau : ");
            this.ViewWeapons();
            Console.WriteLine(
                $"maxStructure: {maxStructure};\n " +
                $"maxShield: {maxShield};\n " +
                $"currentStructure: {currentStructure};\n " +
                $"currentShield: {currentShield};\n" +
                $"maximumNumberOfAttachedWeapons: {maximumNumberOfAttachedWeapons}"
            );
        }
    }
}
