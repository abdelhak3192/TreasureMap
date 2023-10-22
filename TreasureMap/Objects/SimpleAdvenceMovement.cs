using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreasureMap.Enums;
using TreasureMap.Interfaces;

namespace TreasureMap.Objects
{
    internal class SimpleAdvenceMovement : Movement, IMovement
    {
        private readonly IDictionary<Direction, Func<(int, int), (int, int,Direction)>> _movements = new Dictionary<Direction, Func<(int, int), (int, int,Direction)>>()
            {
                { Direction.North, (position)=> { return (position.Item1-1,position.Item2,Direction.North); } },
                { Direction.South, (position)=> { return (position.Item1+1,position.Item2,Direction.South); } },
                { Direction.East, (position)=> { return (position.Item1,position.Item2+1,Direction.East); } },
                { Direction.West, (position)=> { return (position.Item1,position.Item2-1,Direction.West); } },
            };

        public override (int, int,Direction) Move((int, int) position, Direction direction)
        {
            return _movements[direction].Invoke(position);
        }
    }
}
