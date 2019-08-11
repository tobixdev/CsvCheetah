using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using tobixdev.github.io.CsvCheetah.Mapping;

namespace tobixdev.github.io.CsvCheetah.Tests.Mapping
{
    [TestFixture]
    public class MapperFactoryTests : MapperUnitTest
    {
        private IMapperFactory<TestDataClass> _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new MapperFactory<TestDataClass>();
        }

        [Test]
        public void CreateForMap_WithEmptyMap_CreatesMapper()
        {
            var map = new SimpleMap<TestDataClass>();

            var result = _sut.CreateForMap(map);
            
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void CreateForMap_WithNonEmptyMap_CreatesMapperWhichMapsFieldsCorrectly()
        {
            var map = new SimpleMap<TestDataClass>();
            map.AddMapping(0, TestDataClass.PropertyNameFieldA);
            map.AddMapping(1, TestDataClass.PropertyNameFieldB);

            var result = _sut.CreateForMap(map);

            var mapped = result.Map(CreateTokenStreamWithDelimiter("A", "B")).ToArray();
            Assert.That(mapped, Has.Length.EqualTo(1));
            AssertTestDataClass(mapped[0], "A", "B");
        }
    }
}