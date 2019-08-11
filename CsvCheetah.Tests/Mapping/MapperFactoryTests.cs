using System.Linq;
using NUnit.Framework;
using tobixdev.github.io.CsvCheetah.Mapping;

namespace tobixdev.github.io.CsvCheetah.Tests.Mapping
{
    [TestFixture]
    public class MapperFactoryTests
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

            Assert.That(result, Is.Not.Null);
            var mapped = result.Map(new[]
                {Token.CreateValueToken("A"), Token.CreateValueToken("B"), Token.DelimiterToken}).ToArray();
            Assert.That(mapped, Has.Length.EqualTo(1));
            Assert.That(mapped[0].FieldA, Is.EqualTo("A"));
            Assert.That(mapped[0].FieldB, Is.EqualTo("B"));
        }
    }
}