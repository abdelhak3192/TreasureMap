using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreasureMap.Entities;

namespace TreasureMap.Dtos
{
    internal class MapDto
    {
        public IList<CellDto> Cells;
        public int Height;
        public int Width;

        
    }
}
