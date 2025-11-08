using Miehe_Alix_Tp1.Spaceships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miehe_Alix_Tp1.Models
{
    public interface IAbility
    {
        void UseAbility(List<Spaceship> spaceships);
    }
}
