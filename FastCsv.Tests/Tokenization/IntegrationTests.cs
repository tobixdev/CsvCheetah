using System.Linq;
using NUnit.Framework;
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
            Assert.That(result[0], IsA.ValueToken("Hallo")); // TODO: Build assertions
            Assert.That(result[1], IsA.ValueToken("ich"));
            Assert.That(result[2], IsA.ValueToken("bin"));
            Assert.That(result[3], IsA.ValueToken("ein\r"));
            Assert.That(result[4], IsA.RecordDelimiterToken());
            Assert.That(result[5], IsA.ValueToken("sehr"));
            Assert.That(result[6], IsA.ValueToken("einfaches"));
            Assert.That(result[7], IsA.ValueToken("csv"));
            Assert.That(result[8], IsA.ValueToken("file"));
            Assert.That(result[9], IsA.RecordDelimiterToken());
        }
    }
}