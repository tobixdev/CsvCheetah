using System;
using NUnit.Framework.Constraints;

namespace tobixdev.github.io.CsvCheetah.Tests.Tokenization
{
    public class ValueTokenConstraint : Constraint
    {
        private readonly string _expectedValue;

        public ValueTokenConstraint(string expectedValue)
        {
            _expectedValue = expectedValue;
        }

        public override ConstraintResult ApplyTo<TActual>(TActual actual)
        {
            if (!(actual is Token token)) 
                return new ConstraintResult(this, actual, ConstraintStatus.Error);

            var isSuccess = string.Equals(token.Value, _expectedValue, StringComparison.InvariantCulture) &&
                            token.TokenType == TokenType.Value;
            return new ConstraintResult(this, token, isSuccess);
        }
    }
}