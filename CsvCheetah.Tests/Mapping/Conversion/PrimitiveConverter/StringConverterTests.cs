using NUnit.Framework;
using tobixdev.github.io.CsvCheetah.Mapping;
using tobixdev.github.io.CsvCheetah.Mapping.Conversion;
using tobixdev.github.io.CsvCheetah.Mapping.Conversion.PrimitiveConverter;

namespace tobixdev.github.io.CsvCheetah.Tests.Mapping.Conversion.PrimitiveConverter
{
    public class StringConverterTests
    {
        private IConverter<string> _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new StringConverter();
        }

        [Test]
        public void Convert_WithValidValue_ReturnsSameString()
        {
            const string value = "i am a string";
            
            var result = _sut.Convert(value);
            
            Assert.That(result, Is.SameAs(value));
        }
    }
}