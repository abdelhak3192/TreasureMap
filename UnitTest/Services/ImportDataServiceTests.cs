using Microsoft.VisualStudio.TestTools.UnitTesting;
using TreasureMap.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace TreasureMap.Services.Tests
{
    [TestClass()]
    public class ImportDataServiceTests
    {
        private readonly IImportDataService _importDataService;
        ImportDataServiceTests() {
            var services = new ServiceCollection()
                  .AddLogging()
                  .AddScoped<IImportDataService>();

        }

        [TestMethod()]
        public void TestImportDataFromFile_ShouldThrow()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void TestImportDataFromFile_ShouldReturnListofObjects()
        {
            Assert.Fail();
        }
    }
}