using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreasureMap.Entities
{
    internal sealed class Map
    {
        private readonly Cell[][] _map;
        private Map(Cell[][] map)
        {
            this._map = map;
        }

        private static Map _instance;


        public static Map GetInstance(Cell[][] map)
        {
            if (_instance == null)
            {
                _instance = new Map(map);
            }
            return _instance;
        }


    }
}
