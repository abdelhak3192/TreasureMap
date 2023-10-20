using TreasureMap.Extensions;
using TreasureMap.Interfaces;

namespace TreasureMap.Services
{
    internal class ImportDataService : IImportDataService
    {
        public IList<object> Imports { get; set; }

        public ImportDataService()
        {
            Imports = new List<object>();
        }

        public IList<object> ImportDataFromFile(string filePath)
        {
            try
            {
                using (var sr = new StreamReader(filePath))
                {
                    foreach (string line in File.ReadLines(filePath))
                    {
                        Imports.Add(line.CreateObjectFromString());
                    }
                }
                return Imports;

            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
