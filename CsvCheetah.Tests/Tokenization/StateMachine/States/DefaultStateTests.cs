using NUnit.Framework;
using tobixdev.github.io.CsvCheetah.Tokenization.StateMachine;
using tobixdev.github.io.CsvCheetah.Tokenization.StateMachine.States;

namespace tobixdev.github.io.CsvCheetah.Tests.Tokenization.StateMachine.States
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
        public void AcceptNextCharacter_WithCarriageReturn_SetsIntermediateState()
        {
            var result = _sut.AcceptNextCharacter(TokenizerStateContext, '\r');

            Assert.That(result, Is.Null);
            Assert.That(TokenizerStateContext.State, Is.InstanceOf<StringMatchingState>());
        }

        [Test]
        public void AcceptNextCharacter_OnIntermediateState_WithNewLine_ReturnsNewValueToken()
        {
            WithCurrentToken("read token");
            _sut.AcceptNextCharacter(TokenizerStateContext, '\r');
            
            var result = TokenizerStateContext.State.AcceptNextCharacter(TokenizerStateContext, '\n');

            Assert.That(result, IsA.ValueToken("read token"));
        }

        [Test]
        public void AcceptNextCharacter_OnIntermediateState_WithNewLine_SetsWasLastTokenInRecord()
        {
            _sut.AcceptNextCharacter(TokenizerStateContext, '\r');
            
            TokenizerStateContext.State.AcceptNextCharacter(TokenizerStateContext, '\n');

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