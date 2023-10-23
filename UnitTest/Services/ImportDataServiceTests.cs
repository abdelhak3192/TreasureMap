
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using TreasureMap.Interfaces;

namespace TreasureMap.Services.Tests
{
    [TestClass()]
    public class ImportDataServiceTests
    {
        private readonly IImportDataService _importDataService;
        private readonly IList<object> _readList = new List<object>();

        ImportDataServiceTests() {
            var services = new ServiceCollection()
                  .AddLogging()
                  .AddScoped<IImportDataService>();
            var serviceProvider = services.BuildServiceProvider();
            _importDataService =serviceProvider.GetRequiredService<IImportDataService>();

        }

        [TestMethod(), ExpectedException(typeof(ArgumentNullException))]
        [DataRow(null)]
        public void TestImportDataFromFile_NullPath_ShouldThrow(string path)
        {
            _importDataService.ImportDataFromFile(path);
        }

        [TestMethod(), ExpectedException(typeof(FileNotFoundException))]
        [DataRow("/")]
        public void TestImportDataFromFile_UnexistingPath_ShouldThrow(string path)
        {
            _importDataService.ImportDataFromFile(path);
        }

        [TestMethod()]
        [DataRow("C:\\Users\\a.benmazouza_adm\\source\\repos\\TreasureMap1\\TreasureMap\\TreasureMap.txt")]
        public void TestImportDataFromFile_ExistingPath_ShouldReturnListofObjects(string path)
        {
            IList<object> objs=_importDataService.ImportDataFromFile(path);
            Assert.IsNotNull(objs);
            Assert.IsTrue(objs.Count>0);
            
        }

        [TestMethod()]
        [DataRow("C:\\Users\\a.benmazouza_adm\\source\\repos\\TreasureMap1\\TreasureMap\\TreasureMap.txt")]
        public void TestImportDataFromFile_ExistingPath_ShouldReturnListofSameObjects(string path)
        {
            IList<object> objs = _importDataService.ImportDataFromFile(path);
            
                objs.Should().BeEquivalentTo(_readList);

        }
    }
}