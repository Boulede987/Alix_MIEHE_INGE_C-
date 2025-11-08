using Miehe_Alix_Tp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miehe_Alix_Tp1.Spaceships
{
    internal class Tardis : Spaceship, IAbility
    {
        public Tardis(int aMaxStructure, int aMaxShield, int aCurrentStructure, int aCurrentShield, List<Weapon> someAttachedWeapons)
            : base(1, 0, 1, 0, new List<Weapon>())
        {
            this.Name = "Tardis";
        }

        public void UseAbility(List<Spaceship> spaceships)
        {
            Console.WriteLine($"{Name} utilise son ability : Téléportation !");
            if (spaceships != null && spaceships.Count > 0)
            {
                Random random = new Random();

                // Trouver tous les indices contenant un vaisseau (non null)
                List<int> occupiedIndices = new List<int>();
                for (int i = 0; i < spaceships.Count; i++)
                {
                    if (spaceships[i] != null)
                        occupiedIndices.Add(i);
                }

                // Trouver tous les indices vides (null)
                List<int> emptyIndices = new List<int>();
                for (int i = 0; i < spaceships.Count; i++)
                {
                    if (spaceships[i] == null)
                        emptyIndices.Add(i);
                }

                // Vérifier qu'il y a au moins un vaisseau et une case vide
                if (occupiedIndices.Count > 0 && emptyIndices.Count > 0)
                {
                    // Sélectionner un vaisseau au hasard
                    int randomShipIndex = occupiedIndices[random.Next(occupiedIndices.Count)];
                    Spaceship selectedShip = spaceships[randomShipIndex];

                    // Sélectionner une position vide au hasard
                    int randomEmptyIndex = emptyIndices[random.Next(emptyIndices.Count)];

                    // Déplacer le vaisseau
                    spaceships[randomEmptyIndex] = selectedShip;
                    spaceships[randomShipIndex] = null;
                }
            }

        }





    }
}
