using NUnit.Framework;
using tobixdev.github.io.CsvCheetah.Mapping;

namespace tobixdev.github.io.CsvCheetah.Tests.Mapping
{
    [TestFixture]
    public class MapCompilerTests
    {
        private IMapCompiler<TestDataClass> _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new MapCompiler<TestDataClass>();
        }

        [Test]
        public void CompileMap_WithEmptyMap_DoesNotThrow()
        {
            var map = new SimpleMap<TestDataClass>();

            void Act() => _sut.CompileMap(map);
            
            Assert.DoesNotThrow(Act);
        }
    }
}