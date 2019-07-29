using System;
using System.IO;
using System.Linq;
using FakeItEasy;
using NUnit.Framework;
using tobixdev.github.io.FastCsv.Tokenization;
using tobixdev.github.io.FastCsv.Tokenization.RFC4180;
using tobixdev.github.io.FastCsv.Tokenization.RFC4180.StateMachine;

namespace tobixdev.github.io.FastCsv.Tests.Tokenization.RFC4180
{
    [TestFixture]
    public class Rfc4180TokenizerTest
    {
        private ITokenizerStateMachine _tokenizerStateMachine;
        private ITokenizer _sut;

        [SetUp]
        public void SetUp()
        {
            _tokenizerStateMachine = A.Fake<ITokenizerStateMachine>();
            _sut = new Rfc4180Tokenizer(_tokenizerStateMachine);
        }

        [Test]
        public void Tokenize_WithNull_ThrowsArgumentNullException()
        {
            TestDelegate act = () => _sut.Tokenize(null).ToList();

            var exception = Assert.Throws<ArgumentNullException>(act);
            Assert.That(exception.Message, Is.EqualTo(@"Value cannot be null.
Parameter name: textReader"));
        }

        [Test]
        public void Tokenize_WithValidReader_ForwardsAllCharactersToStateMachine()
        {
            var reader = new StringReader("Hello World!");

            _sut.Tokenize(reader).ToList();

            A.CallTo(() => _tokenizerStateMachine.AcceptNextChar('H')).MustHaveHappened(1, Times.Exactly);
            A.CallTo(() => _tokenizerStateMachine.AcceptNextChar('!')).MustHaveHappened(1, Times.Exactly);
            A.CallTo(() => _tokenizerStateMachine.AcceptNextChar(A<char>._))
                .MustHaveHappened("Hello World!".Length, Times.Exactly);
        }

        [Test]
        public void Tokenize_WithValidReader_ReturnsAllTokensReturnedByStateMachine()
        {
            A.CallTo(() => _tokenizerStateMachine.AcceptNextChar('A')).Returns(new Token("A Token", TokenType.Value));
            A.CallTo(() => _tokenizerStateMachine.AcceptNextChar('C')).Returns(new Token(null, TokenType.RecordDelimiter));
            var reader = new StringReader("ABC");

            var result = _sut.Tokenize(reader).ToList();

            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].Value, Is.EqualTo("A Token"));
            Assert.That(result[0].TokenType, Is.EqualTo(TokenType.Value));
            Assert.That(result[1].Value, Is.EqualTo(null));
            Assert.That(result[1].TokenType, Is.EqualTo(TokenType.RecordDelimiter));
        }
    }
}