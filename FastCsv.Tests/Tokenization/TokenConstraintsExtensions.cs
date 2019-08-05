using NUnit.Framework.Constraints;

namespace tobixdev.github.io.FastCsv.Tests.Tokenization
{
    public static class TokenConstraintsExtensions
    {
        public static ValueTokenConstraint ValueToken(this ConstraintExpression expression, string expectedValue)
        {
            var constraint = new ValueTokenConstraint(expectedValue);
            expression.Append(constraint);
            return constraint;
        }
        
        public static RecordDelimiterTokenConstraint RecordDelimiterToken(this ConstraintExpression expression)
        {
            var constraint = new RecordDelimiterTokenConstraint();
            expression.Append(constraint);
            return constraint;
        }
    }
}