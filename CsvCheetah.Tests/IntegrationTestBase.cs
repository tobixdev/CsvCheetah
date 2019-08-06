using System.IO;
using System.Reflection;

namespace tobixdev.github.io.CsvCheetah.Tests
{
    public class IntegrationTestBase
    {
        protected TextReader CreateReaderForTestFile(string filename)
        {
            var assemblyLocation = Assembly.GetAssembly(typeof(IntegrationTestBase)).Location;
            var basePath = Path.GetDirectoryName(assemblyLocation);
            var fullPath = Path.Combine(basePath, "TestData", filename);
            
            return new StreamReader(fullPath);
        }
    }
}