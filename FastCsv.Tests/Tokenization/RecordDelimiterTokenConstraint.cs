using System;
using NUnit.Framework.Constraints;

namespace tobixdev.github.io.FastCsv.Tests.Tokenization
{
    public class RecordDelimiterTokenConstraint : Constraint
    {

        public override ConstraintResult ApplyTo<TActual>(TActual actual)
        {
            var token = actual as Token?;
            if (token == null) 
                return new ConstraintResult(this, actual, ConstraintStatus.Error);
            
            var isSuccess = token.Value.TokenType == TokenType.RecordDelimiter;
            return new ConstraintResult(this, token, isSuccess);
        }
    }
}