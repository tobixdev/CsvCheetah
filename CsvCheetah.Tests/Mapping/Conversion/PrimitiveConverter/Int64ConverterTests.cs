using NUnit.Framework;
using tobixdev.github.io.CsvCheetah.Mapping;
using tobixdev.github.io.CsvCheetah.Mapping.Conversion;
using tobixdev.github.io.CsvCheetah.Mapping.Conversion.PrimitiveConverter;

namespace tobixdev.github.io.CsvCheetah.Tests.Mapping.Conversion.PrimitiveConverter
{
    public class Int64ConverterTests : IntConverterTestBase
    {
        private IConverter _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new Int64Converter();
        }

        protected override object Convert(string value)
        {
            return _sut.Convert(value);
        }

        protected override string ExpectedTypeName => "Int64";
    }
}