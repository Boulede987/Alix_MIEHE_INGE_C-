//using Models.SpaceShips;
using Miehe_Alix_Tp1.Spaceships;

namespace Models
{
    public interface IPlayer
    {
        Spaceship BattleShip { get; set; }
        string Name { get; }
        string Alias { get; }
    }
}