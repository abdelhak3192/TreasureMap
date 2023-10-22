using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreasureMap.Entities
{
    internal class Cell
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool isMountainous  { get; set; } = false;
        public IList<Treasure> treasures { get; set; }  = new List<Treasure>();
        public IList<Adventurer> Adventurers { get; set; } = new List<Adventurer>();

        public Cell(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public Cell(int x, int y, bool isMountainous) : this(x, y)
        {
            this.isMountainous = isMountainous;
        }

        public Cell(int x, int y, bool isMountainous, IList<Treasure> treasures) : this(x, y, isMountainous)
        {
            this.treasures = treasures;
        }
        

        public void SetPosition(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public bool HasMountains()
        {
            return isMountainous;
        }

    }
}
