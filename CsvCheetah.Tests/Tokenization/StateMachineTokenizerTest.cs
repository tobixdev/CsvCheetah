using System;
using System.IO;
using System.Linq;
using FakeItEasy;
using NUnit.Framework;
using tobixdev.github.io.CsvCheetah.Tokenization;
using tobixdev.github.io.CsvCheetah.Tokenization.StateMachine;

namespace tobixdev.github.io.CsvCheetah.Tests.Tokenization
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
            void Act() => _sut.Tokenize(null).ToArray();

            var exception = Assert.Throws<ArgumentNullException>(Act);
            Assert.That(exception.Message, Is.EqualTo(@"Value cannot be null. (Parameter 'textReader')"));
        }

        [Test]
        public void Tokenize_WithValidReader_ForwardsAllCharactersToStateMachine()
        {
            var reader = new StringReader("Hello World!");

            _sut.Tokenize(reader).ToArray();

            A.CallTo(() => _tokenizerStateMachine.AcceptNextCharacter('H')).MustHaveHappened(1, Times.Exactly);
            A.CallTo(() => _tokenizerStateMachine.AcceptNextCharacter('!')).MustHaveHappened(1, Times.Exactly);
            A.CallTo(() => _tokenizerStateMachine.AcceptNextCharacter(A<char>._))
                .MustHaveHappened("Hello World!".Length, Times.Exactly);
        }

        [Test]
        public void Tokenize_WithValidReader_ReturnsAllTokensReturnedByStateMachine()
        {
            A.CallTo(() => _tokenizerStateMachine.AcceptNextCharacter('B')).Returns(Token.CreateValueToken("A Token"));
            A.CallTo(() => _tokenizerStateMachine.AcceptNextCharacter('C')).Returns(Token.CreateValueToken("A second Token"));
            var reader = new StringReader("ABC");

            var result = _sut.Tokenize(reader).ToArray();

            Assert.That(result, Has.Length.EqualTo(3));
            Assert.That(result[0].Value, Is.EqualTo("A Token"));
            Assert.That(result[0].TokenType, Is.EqualTo(TokenType.Value));
            Assert.That(result[1].Value, Is.EqualTo("A second Token"));
            Assert.That(result[1].TokenType, Is.EqualTo(TokenType.Value));
            Assert.That(result[2].Value, Is.Empty);
            Assert.That(result[2].TokenType, Is.EqualTo(TokenType.RecordDelimiter));
        }
    }
}