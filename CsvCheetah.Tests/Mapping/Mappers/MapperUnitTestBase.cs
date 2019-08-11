using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace tobixdev.github.io.CsvCheetah.Tests.Mapping.Mappers
{
    public abstract class MapperUnitTest
    {
        protected void AssertTestDataClass(TestDataClass toAssert, string expectedFieldA, string expectedFieldB)
        {   
            Assert.That(toAssert.FieldA, Is.EqualTo(expectedFieldA));
            Assert.That(toAssert.FieldB, Is.EqualTo(expectedFieldB));
        }

        protected static IEnumerable<Token> CreateTokenStreamWithDelimiter(params string[] values)
        {
            return values.Select(Token.CreateValueToken).Append(Token.DelimiterToken);
        }
    }
}