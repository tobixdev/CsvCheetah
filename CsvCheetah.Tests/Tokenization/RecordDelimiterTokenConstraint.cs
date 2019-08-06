using NUnit.Framework.Constraints;

namespace tobixdev.github.io.CsvCheetah.Tests.Tokenization
{
    public class RecordDelimiterTokenConstraint : Constraint
    {

        public override ConstraintResult ApplyTo<TActual>(TActual actual)
        {
            if (!(actual is Token token)) 
                return new ConstraintResult(this, actual, ConstraintStatus.Error);
            
            var isSuccess = token.TokenType == TokenType.RecordDelimiter;
            return new ConstraintResult(this, token, isSuccess);
        }
    }
}