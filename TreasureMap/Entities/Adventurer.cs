using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreasureMap.ObjectCreators;
using TreasureMap.Enums;

namespace TreasureMap.Entities
{
    internal class Adventurer
    {
        public string Name { get; set; }
        public string Movements { get; set; }
        public Direction Direction { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
