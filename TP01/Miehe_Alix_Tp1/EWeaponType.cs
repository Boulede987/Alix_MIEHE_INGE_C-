using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miehe_Alix_Tp1
{

    public enum EWeaponType
    {
        Direct, // 1 chance sur 10 de rater sa cible
        Explosive, // 1 chance sur 4 de rater, double le temps de rechargement
        Guided // touche toujours, mais avec degats minimum
    }
}
