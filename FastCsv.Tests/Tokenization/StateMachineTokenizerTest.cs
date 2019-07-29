using System;
using System.IO;
using System.Linq;
using FakeItEasy;
using NUnit.Framework;
using tobixdev.github.io.FastCsv.Tokenization;
using tobixdev.github.io.FastCsv.Tokenization.StateMachine;

namespace tobixdev.github.io.FastCsv.Tests.Tokenization
{
    [TestFixture]
    public class StateMachineTokenizerTest
    {
        private ITokenizerStateMachine _tokenizerStateMachine;
        private ITokenizer _sut;

        [SetUp]
        public void SetUp()
        {
            _tokenizerStateMachine = A.Fake<ITokenizerStateMachine>();
            _sut = new StateMachineTokenizer(_tokenizerStateMachine);
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

            A.CallTo(() => _tokenizerStateMachine.AcceptNextCharacter('H')).MustHaveHappened(1, Times.Exactly);
            A.CallTo(() => _tokenizerStateMachine.AcceptNextCharacter('!')).MustHaveHappened(1, Times.Exactly);
            A.CallTo(() => _tokenizerStateMachine.AcceptNextCharacter(A<char>._))
                .MustHaveHappened("Hello World!".Length, Times.Exactly);
        }

        [Test]
        public void Tokenize_WithValidReader_ReturnsAllTokensReturnedByStateMachine()
        {
            A.CallTo(() => _tokenizerStateMachine.AcceptNextCharacter('A')).Returns(new Token("A Token", TokenType.Value));
            A.CallTo(() => _tokenizerStateMachine.AcceptNextCharacter('C')).Returns(new Token(null, TokenType.RecordDelimiter));
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