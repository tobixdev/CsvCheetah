using FakeItEasy;
using NUnit.Framework;
using tobixdev.github.io.CsvCheetah.Mapping;
using tobixdev.github.io.CsvCheetah.Mapping.Conversion;

namespace tobixdev.github.io.CsvCheetah.Tests.Mapping.ConversionRegistry
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
            TestDelegate act = () =>_sut.GetConverter<int>();

            var exception = Assert.Throws<MappingException>(act);
            Assert.That(exception.Message, Is.EqualTo("No converter for type Int32."));
        }

        [Test]
        public void GetConverter_WithExistingEntry_ReturnsCorrectEntry()
        {
            var converter = A.Fake<IConverter<int>>();
            _sut.RegisterConverter(converter);
            
            var result = _sut.GetConverter<int>();

            Assert.That(result, Is.SameAs(converter));
        }

        [Test]
        public void RegisterConverter_WithExistingEntry_OverridesOldEntry()
        {
            var oldConverter = A.Fake<IConverter<int>>();
            _sut.RegisterConverter(oldConverter);
            var newConverter = A.Fake<IConverter<int>>();
            
            _sut.RegisterConverter(newConverter);
            
            var result = _sut.GetConverter<int>();
            Assert.That(result, Is.SameAs(newConverter));
        }
    }
}