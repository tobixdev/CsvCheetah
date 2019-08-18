using System.Reflection;
using NUnit.Framework;
using tobixdev.github.io.CsvCheetah.Mapping;

namespace tobixdev.github.io.CsvCheetah.Tests.Mapping.Conversion.PrimitiveConverter
{
    public abstract class IntConverterTestBase
    {
        protected abstract object Convert(string value);
        protected abstract string ExpectedTypeName { get; }

        [Test]
        public void Convert_WithValidValue_ReturnsParsedValue()
        {
            var result = Convert("123");

            Assert.That(result, Is.EqualTo(123));
        }

        [Test]
        public void Convert_WithInvalidValue_ThrowsMappingException()
        {
            void Act() => Convert("123abc");

            var exception = Assert.Throws<MappingException>(Act);
            Assert.That(exception.Message, Is.EqualTo($"Cannot convert '123abc' to {ExpectedTypeName}."));
        }
    }
}