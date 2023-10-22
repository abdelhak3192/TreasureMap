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
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsMountainous { get; set; } = false;
        public MountainDto ? Mountain{ get; set; }
        public IList<TreasureDto> Treasures { get; set; } = new List<TreasureDto>();
        public IList<AdventurerDto> Adventurers { get; set; } = new List<AdventurerDto>();
    }
}
