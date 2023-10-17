using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreasureMap.Interfaces;


namespace TreasureMap.Entities
{
    internal abstract class Movement : IMovement
    {
        public abstract int move(int position);
    }
}
