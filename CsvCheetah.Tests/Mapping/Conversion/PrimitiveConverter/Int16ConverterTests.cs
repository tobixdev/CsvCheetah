using NUnit.Framework;
using tobixdev.github.io.CsvCheetah.Mapping;
using tobixdev.github.io.CsvCheetah.Mapping.Conversion;
using tobixdev.github.io.CsvCheetah.Mapping.Conversion.PrimitiveConverter;

namespace tobixdev.github.io.CsvCheetah.Tests.Mapping.Conversion.PrimitiveConverter
{
    public class Int16ConverterTests : IntConverterTestBase<short>
    {
        private IConverter<short> _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new Int16Converter();
        }

        protected override short Convert(string value)
        {
            return _sut.Convert(value);
        }

        protected override string ExpectedTypeName => "Int16";
    }
}