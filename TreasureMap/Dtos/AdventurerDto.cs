using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreasureMap.Enums;

namespace TreasureMap.Entities
{
    internal class AdventurerDto
    {
        public string Name { get; set; }
        public string Movements { get; set; }
        public Direction Direction { get; set; }
    }
}
