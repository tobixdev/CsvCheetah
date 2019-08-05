using FakeItEasy;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using tobixdev.github.io.FastCsv.Tokenization.StateMachine;
using tobixdev.github.io.FastCsv.Tokenization.StateMachine.States;

namespace tobixdev.github.io.FastCsv.Tests.Tokenization.StateMachine.States
{
    [TestFixture]
    public class DefaultStateTests : StateTestsBase
    {
        private ITokenizerState _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new DefaultState();
        }

        [Test]
        public void AcceptNextCharacter_WithComma_ReturnsNewValueToken()
        {
            WithCurrentToken("read token");
            var result = _sut.AcceptNextCharacter(TokenizerStateContext, ',');

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, IsA.ValueToken("read token"));
        }

        [Test]
        public void AcceptNextCharacter_WithQuote_SetsNewState()
        {
            var result = _sut.AcceptNextCharacter(TokenizerStateContext, '"');

            Assert.That(result.HasValue, Is.False);
            Assert.That(TokenizerStateContext.State, Is.InstanceOf<EscapedState>());
        }

        [Test]
        public void AcceptNextCharacter_WithNewLine_ReturnsNewValueToken()
        {
            WithCurrentToken("read token");
            var result = _sut.AcceptNextCharacter(TokenizerStateContext, '\n');

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, IsA.ValueToken("read token"));
        }

        [Test]
        public void AcceptNextCharacter_WithNewLine_SetsWasLastTokenInRecord()
        {
            _sut.AcceptNextCharacter(TokenizerStateContext, '\n');

            Assert.That(TokenizerStateContext.WasLastTokenInRecord, Is.True);
        }

        [Test]
        public void AcceptNextCharacter_WithNonSpecialCharacter_AddsCharacterToCurrentToken()
        {
            _sut.AcceptNextCharacter(TokenizerStateContext, 'x');

            AssertCharacterAppendedToToken('x');
        }
    }
}