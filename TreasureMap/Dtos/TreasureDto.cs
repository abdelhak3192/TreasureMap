using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreasureMap.Dtos
{
    internal class TreasureDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Found { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
