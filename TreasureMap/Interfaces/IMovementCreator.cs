using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreasureMap.Enums;

namespace TreasureMap.Interfaces
{
    internal interface IMovementCreator
    {
        public (int, int,Direction) Move((int, int) position, Direction direction);
        
    }
}
