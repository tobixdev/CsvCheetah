using FakeItEasy;
using NUnit.Framework;
using tobixdev.github.io.CsvCheetah.Mapping;
using tobixdev.github.io.CsvCheetah.Mapping.Conversion;

namespace tobixdev.github.io.CsvCheetah.Tests.Mapping.Conversion
{
    [TestFixture]
    public class ConverterRegistryTests
    {
        private IConverterRegistry _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new ConverterRegistry();
        }

        [Test]
        public void GetConverter_WithNonExistingEntry_ThrowsMappingException()
        {
            void Act() => _sut.GetConverter(typeof(int));

            var exception = Assert.Throws<MappingException>(Act);
            Assert.That(exception.Message, Is.EqualTo("No converter for type Int32."));
        }

        [Test]
        public void GetConverter_WithExistingEntry_ReturnsCorrectEntry()
        {
            var converter = A.Fake<IConverter>();
            _sut.RegisterConverter(typeof(int), converter);
            
            var result = _sut.GetConverter(typeof(int));

            Assert.That(result, Is.SameAs(converter));
        }

        [Test]
        public void RegisterConverter_WithExistingEntry_OverridesOldEntry()
        {
            var oldConverter = A.Fake<IConverter>();
            _sut.RegisterConverter(typeof(int), oldConverter);
            var newConverter = A.Fake<IConverter>();
            
            _sut.RegisterConverter(typeof(int), newConverter);
            
            var result = _sut.GetConverter(typeof(int));
            Assert.That(result, Is.SameAs(newConverter));
        }
    }
}