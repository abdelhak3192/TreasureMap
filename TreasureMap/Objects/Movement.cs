using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreasureMap.Enums;
using TreasureMap.Interfaces;


namespace TreasureMap.Objects
{
    internal abstract class Movement : IMovement
    {

        public abstract (int, int) Move((int, int) position, Direction direction);

    }
}
