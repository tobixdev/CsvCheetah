using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using tobixdev.github.io.FastCsv.Tokenization;
using tobixdev.github.io.FastCsv.Tokenization.StateMachine;

namespace tobixdev.github.io.FastCsv.Tests.Tokenization
{
    [TestFixture]
    public class IntegrationTests : IntegrationTestBase
    {
        private ITokenizer _sut;

        [SetUp]
        public void SetUp()
        {
            var stateMachine = new TokenizerStateMachine();
            _sut = new StateMachineTokenizer(stateMachine);
        }

        [Test]
        public void Tokenize_SimpleCsvWithoutHeader_ReturnsCorrectTokens()
        {
            Token[] result;
            using (var reader = CreateReaderForTestFile("SimpleWithoutHeader.csv"))
            {
                result = _sut.Tokenize(reader).ToArray();
            }
            
            Assert.That(result, Has.Length.EqualTo(10));
            AssertValueToken(result[0], "Hallo"); // TODO: Build assertions
            AssertValueToken(result[1], "ich");
            AssertValueToken(result[2], "bin");
            AssertValueToken(result[3], "ein\r");
            AssertRecordDelimiterToken(result[4]);
            AssertValueToken(result[5], "sehr");
            AssertValueToken(result[6], "einfaches");
            AssertValueToken(result[7], "csv");
            AssertValueToken(result[8], "file");
            AssertRecordDelimiterToken(result[9]);
        }

        private void AssertValueToken(Token token, string expectedValue)
        {
            Assert.That(token.TokenType, Is.EqualTo(TokenType.Value));
            Assert.That(token.Value, Is.EqualTo(expectedValue));
        }

        private void AssertRecordDelimiterToken(Token token)
        {
            Assert.That(token.TokenType, Is.EqualTo(TokenType.RecordDelimiter));
            Assert.That(token.Value, Is.EqualTo(null));
        }
    }
}