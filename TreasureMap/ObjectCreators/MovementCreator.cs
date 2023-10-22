
using TreasureMap.Enums;
using TreasureMap.Interfaces;

namespace TreasureMap.ObjectCreators
{
    internal abstract class MovementCreator : IMovementCreator
    {
        public abstract IMovement Create();
        public (int, int,Direction) Move((int, int) position,Direction direction)
        {
            // Call the factory method to create a Movement object.
            var movement = Create();
            return movement.Move(position,direction);

        }
    }

}
