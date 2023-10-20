using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreasureMap.Dtos;

namespace TreasureMap.Interfaces
{
    internal interface IImportDataService
    {
        public IList<Object> ImportDataFromFile(string filePath);
    }
}
