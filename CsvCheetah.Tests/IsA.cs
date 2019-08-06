using tobixdev.github.io.CsvCheetah.Tests.Tokenization;

namespace tobixdev.github.io.CsvCheetah.Tests
{
    public class IsA
    {
        public static ValueTokenConstraint ValueToken(string expectedValue)
        {
            return new ValueTokenConstraint(expectedValue);
        }

        public static RecordDelimiterTokenConstraint RecordDelimiterToken()
        {
            return new RecordDelimiterTokenConstraint();
        }
    }
}