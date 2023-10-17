using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreasureMap.Entities
{
    internal class Cell
    {
        public int x { get; set; }
        public int y { get; set; }
        public bool isMountainous  { get; set; } = false;
        public IList<Treasure> treasures { get; set; }  = new List<Treasure>();

        public Cell(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Cell(int x, int y, bool isMountainous) : this(x, y)
        {
            this.isMountainous = isMountainous;
        }

        public Cell(int x, int y, bool isMountainous, IList<Treasure> treasures) : this(x, y, isMountainous)
        {
            this.treasures = treasures;
        }

        

        public void setPosition(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public bool hasMountains()
        {
            return isMountainous;
        }

    }
}
