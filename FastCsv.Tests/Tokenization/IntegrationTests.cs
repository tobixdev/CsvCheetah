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
            Assert.That(result[0], Is.ValueToken("Hallo")); // TODO: Build assertions
            Assert.That(result[1], Is.ValueToken("ich"));
            Assert.That(result[2], Is.ValueToken("bin"));
            Assert.That(result[3], Is.ValueToken("ein\r"));
            Assert.That(result[4], Is.RecordDelimiterToken());
            Assert.That(result[5], Is.ValueToken("sehr"));
            Assert.That(result[6], Is.ValueToken("einfaches"));
            Assert.That(result[7], Is.ValueToken("csv"));
            Assert.That(result[8], Is.ValueToken("file"));
            Assert.That(result[9], Is.RecordDelimiterToken());
        }
    }
}