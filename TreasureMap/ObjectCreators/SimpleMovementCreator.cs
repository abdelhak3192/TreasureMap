using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreasureMap.Interfaces;

namespace TreasureMap.ObjectCreators
{
    internal class SimpleMovementCreator : MovementCreator
    {

        public override IMovement FactoryMethod()
        {
            return new SimpleMovement();
        }
    }
}
