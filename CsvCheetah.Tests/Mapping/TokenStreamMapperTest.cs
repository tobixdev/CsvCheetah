using System.Linq;
using NUnit.Framework;
using tobixdev.github.io.CsvCheetah.Mapping;
using tobixdev.github.io.CsvCheetah.Mapping.Mappers;
using tobixdev.github.io.CsvCheetah.Mapping.Maps;

namespace tobixdev.github.io.CsvCheetah.Tests.Mapping
{
    [TestFixture]
    public class TokenStreamMapperTest : MapperUnitTest
    {
        private ITokenStreamMapper<TestDataClass> _sut;

        [SetUp]
        public void SetUp()
        {
            var map = new ColumnMapBuilder<TestDataClass>()
                .WithColumn(0, t => t.FieldA)
                .WithColumn(1, t => t.FieldB)
                .Build();
            _sut = CreateTokenStreamMapper(map);
        }

        [Test]
        public void Map_WithNoValueTokens_ReturnsRecordWithNullValues()
        {
            var emptyTokenStream = CreateTokenStreamWithDelimiter();
            
            var result = _sut.Map(emptyTokenStream).ToArray();
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Length.EqualTo(1));
            AssertTestDataClass(result[0], null, null);
        }

        [Test]
        public void Map_WithSingleValueTokens_ReturnsCorrectRecord()
        {
            var tokenStream = CreateTokenStreamWithDelimiter("ABC");
            
            var result = _sut.Map(tokenStream).ToArray();
            
            AssertTestDataClass(result[0], "ABC", null);
        }

        [Test]
        public void Map_WithAllTokensForRecord_ReturnsCorrectRecord()
        {
            var tokenStream = CreateTokenStreamWithDelimiter("Some Value", "Second Value");
            
            var result = _sut.Map(tokenStream).ToArray();
            
            AssertTestDataClass(result[0], "Some Value", "Second Value");
        }

        [Test]
        public void Map_WithMoreTokensAsPropertiesRecord_ReturnsCorrectRecord()
        {
            var tokenStream = CreateTokenStreamWithDelimiter("1", "2", "3", "4", "5");
            
            var result = _sut.Map(tokenStream).ToArray();
            
            AssertTestDataClass(result[0], "1", "2");
        }

        [Test]
        public void Map_WithMultipleRecords_ReturnsCorrectRecords()
        {
            var firstRecord = CreateTokenStreamWithDelimiter("A1", "B1");
            var secondRecord = CreateTokenStreamWithDelimiter("A2", "B2");
            
            var result = _sut.Map(firstRecord.Concat(secondRecord)).ToArray();
            
            Assert.That(result, Has.Length.EqualTo(2));
            AssertTestDataClass(result[0], "A1", "B1");
            AssertTestDataClass(result[1], "A2", "B2");
        }

        [Test]
        public void Map_WithClassWithoutDefaultCtor_ThrowsException()
        {
            var map = new SimpleMap<ClassWithoutDefaultCtor>();
            var sut = CreateTokenStreamMapper(map);
            
            TestDelegate act = () => sut.Map(CreateTokenStreamWithDelimiter()).ToArray();

            var exception = Assert.Throws<MappingException>(act);
            Assert.That(exception.Message, Is.EqualTo("No default constructor found for ClassWithoutDefaultCtor."));
        }

        private ITokenStreamMapper<T> CreateTokenStreamMapper<T>(IMap<T> map) where T : class
        {
            var tokenStreamMapperFactory = new MapperFactory<T>();
            return tokenStreamMapperFactory.CreateForMap(map);
        }
        
        private class ClassWithoutDefaultCtor
        {
            public ClassWithoutDefaultCtor(string someValue)
            {
            }
        }
    }
}