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
            Assert.That(result[0], IsA.ValueToken("Hello"));
            Assert.That(result[1], IsA.ValueToken("I"));
            Assert.That(result[2], IsA.ValueToken("am"));
            Assert.That(result[3], IsA.ValueToken("an"));
            Assert.That(result[4], IsA.RecordDelimiterToken());
            Assert.That(result[5], IsA.ValueToken("very"));
            Assert.That(result[6], IsA.ValueToken("simple"));
            Assert.That(result[7], IsA.ValueToken("csv"));
            Assert.That(result[8], IsA.ValueToken("file"));
            Assert.That(result[9], IsA.RecordDelimiterToken());
        }

        [Test]
        public void Tokenize_WithAllEscaped_ReturnsCorrectTokens()
        {
            Token[] result;
            using (var reader = CreateReaderForTestFile("WithAllEscaped.csv"))
            {
                result = _sut.Tokenize(reader).ToArray();
            }
            
            Assert.That(result, Has.Length.EqualTo(6));
            Assert.That(result[0], IsA.ValueToken("Hello"));
            Assert.That(result[1], IsA.ValueToken("I"));
            Assert.That(result[2], IsA.ValueToken("contain"));
            Assert.That(result[3], IsA.ValueToken("a"));
            Assert.That(result[4], IsA.ValueToken(@""""));
            Assert.That(result[5], IsA.RecordDelimiterToken());
        }

        [Test]
        public void Tokenize_WithEscapedNewLine_ReturnsCorrectTokens()
        {
            Token[] result;
            using (var reader = CreateReaderForTestFile("WithEscapedNewLine.csv"))
            {
                result = _sut.Tokenize(reader).ToArray();
            }
            
            Assert.That(result, Has.Length.EqualTo(3));
            Assert.That(result[0], IsA.ValueToken("Hell\r\no"));
            Assert.That(result[1], IsA.ValueToken(@"this is one record"));
            Assert.That(result[2], IsA.RecordDelimiterToken());
        }
    }
}