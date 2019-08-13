using NUnit.Framework;
using tobixdev.github.io.CsvCheetah.Mapping.Conversion;
using Int32Converter = tobixdev.github.io.CsvCheetah.Mapping.Conversion.PrimitiveConverter.Int32Converter;

namespace tobixdev.github.io.CsvCheetah.Tests.Mapping.Conversion.PrimitiveConverter
{
    public class Int32ConverterTests : IntConverterTestBase<int>
    {
        private IConverter<int> _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new Int32Converter();
        }

        protected override int Convert(string value)
        {
            return _sut.Convert(value);
        }

        protected override string ExpectedTypeName => "Int32";
    }
}