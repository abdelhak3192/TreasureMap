using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreasureMap.Interfaces;

namespace TreasureMap.ObjectCreators
{
    internal abstract class MovementCreator
    {

        public abstract IMovement FactoryMethod();


        public int Move(int position)
        {
            // Call the factory method to create a Movement object.
            var movement = FactoryMethod();

            return movement.move(position);

        }
    }

}
