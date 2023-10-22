using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreasureMap.Entities;
using TreasureMap.Interfaces;
using TreasureMap.Objects;

namespace TreasureMap.ObjectCreators
{
    internal class SimpleAdvenceMovementCreator : MovementCreator,IMovementCreator
    {

        public override IMovement Create()
        {
            return new SimpleAdvenceMovement();
        }
    }
}
