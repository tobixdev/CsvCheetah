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
            var token = actual as Token?;
            if (token == null) 
                return new ConstraintResult(this, actual, ConstraintStatus.Error);

            var isSuccess = string.Equals(token.Value.Value, _expectedValue, StringComparison.InvariantCulture) &&
                            token.Value.TokenType == TokenType.Value;
            return new ConstraintResult(this, token, isSuccess);
        }
    }
}