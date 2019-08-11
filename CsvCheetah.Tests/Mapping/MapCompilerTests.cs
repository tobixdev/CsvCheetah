using NUnit.Framework;
using tobixdev.github.io.CsvCheetah.Mapping;
using tobixdev.github.io.CsvCheetah.Mapping.Mappers;
using tobixdev.github.io.CsvCheetah.Mapping.Maps;

namespace tobixdev.github.io.CsvCheetah.Tests.Mapping
{
    [TestFixture]
    public class MapCompilerTests
    {
        private IMapperFactory<TestDataClass> _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new MapperFactory<TestDataClass>();
        }

        [Test]
        public void CompileMap_WithEmptyMap_DoesNotThrow()
        {
            var map = new SimpleMap<TestDataClass>();

            void Act() => _sut.CreateForMap(map);
            
            Assert.DoesNotThrow(Act);
        }
    }
}