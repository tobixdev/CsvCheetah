using tobixdev.github.io.FastCsv.Tests.Tokenization;

namespace tobixdev.github.io.FastCsv.Tests
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