using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreasureMap.Interfaces;

namespace TreasureMap.Entities
{
    internal class SimpleMovement : Movement, IMovement
    {
        public override int move(int position)
        {
            throw new NotImplementedException();
        }
    }
}
