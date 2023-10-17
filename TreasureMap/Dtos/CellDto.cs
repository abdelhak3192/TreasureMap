using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreasureMap.Entities;

namespace TreasureMap.Dtos
{
    internal class CellDto
    {
        public int x;
        public int y;
        public bool isMountainous = false;
        public IList<Treasure> treasures = new List<Treasure>();
    }
}
