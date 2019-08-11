using FakeItEasy;
using NUnit.Framework;
using tobixdev.github.io.CsvCheetah.Tokenization.StateMachine;
using tobixdev.github.io.CsvCheetah.Tokenization.StateMachine.States;

namespace tobixdev.github.io.CsvCheetah.Tests.Tokenization.StateMachine.States
{
    [TestFixture]
    public class EscapedStateTests : StateTestsBase
    {
        private ITokenizerState _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new EscapedState(A.Fake<StateHolder>());
        }
        
        [Test]
        public void AcceptNextCharacter_WithComma_AddsCharacterToCurrentToken()
        {
            _sut.AcceptNextCharacter(TokenizerStateContext, ',');

            AssertCharacterAppendedToToken(',');
        }

        [Test]
        public void AcceptNextCharacter_WithQuote_SetsNewState()
        {
            var result = _sut.AcceptNextCharacter(TokenizerStateContext, '"');

            Assert.That(result.HasValue, Is.False);
            Assert.That(TokenizerStateContext.State, Is.InstanceOf<EscapedStateWithOneQuote>());
        }

        [Test]
        public void AcceptNextCharacter_WithNewLine_AddsCharacterToCurrentToken()
        {
            var result = _sut.AcceptNextCharacter(TokenizerStateContext, '\n');

            Assert.That(result, Is.Null);
            AssertCharacterAppendedToToken('\n');
        }

        [Test]
        public void AcceptNextCharacter_WithNonSpecialCharacter_AddsCharacterToCurrentToken()
        {
            _sut.AcceptNextCharacter(TokenizerStateContext, 'x');

            AssertCharacterAppendedToToken('x');
        }
    }
}