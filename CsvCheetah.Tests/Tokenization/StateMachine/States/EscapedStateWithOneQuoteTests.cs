using FakeItEasy;
using NUnit.Framework;
using tobixdev.github.io.CsvCheetah.Tokenization;
using tobixdev.github.io.CsvCheetah.Tokenization.StateMachine;
using tobixdev.github.io.CsvCheetah.Tokenization.StateMachine.States;

namespace tobixdev.github.io.CsvCheetah.Tests.Tokenization.StateMachine.States
{
    [TestFixture]
    public class EscapedStateWithOneQuoteTests : StateTestsBase
    {
        private ITokenizerState _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new EscapedStateWithOneQuote(A.Fake<StateHolder>());
        }
        
        [Test]
        public void AcceptNextCharacter_WithComma_AddsCharacterToCurrentToken()
        {
            WithCurrentToken("read token");
            var result = _sut.AcceptNextCharacter(TokenizerStateContext, ',');

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, IsA.ValueToken("read token"));
        }
        
        [Test]
        public void AcceptNextCharacter_WithComma_SetsNewState()
        {
           _sut.AcceptNextCharacter(TokenizerStateContext, ',');

           Assert.That(TokenizerStateContext.State, Is.InstanceOf<DefaultState>());
        }

        [Test]
        public void AcceptNextCharacter_WithQuote_SetsNewState()
        {
            var result = _sut.AcceptNextCharacter(TokenizerStateContext, '"');

            Assert.That(result, Is.Null);
            Assert.That(TokenizerStateContext.State, Is.InstanceOf<EscapedState>());
        }
        
        [Test]
        public void AcceptNextCharacter_WithQuote_AddsSingleQuoteToCurrentToken()
        {
            _sut.AcceptNextCharacter(TokenizerStateContext, '"');

            AssertCharacterAppendedToToken('"');
        }
        
        [Test]
        public void AcceptNextCharacter_WithNonSpecialCharacter_AddsCharacterToCurrentToken()
        {
            void Act() => _sut.AcceptNextCharacter(TokenizerStateContext, 'x');

            var exception = Assert.Throws<TokenizationException>(Act);
           Assert.That(exception.Message, Is.EqualTo("Text followed closing qoute, in an escaped field."));
        }
        
        [Test]
        public void AcceptNextCharacter_WithNewLine_ReturnsNewValueToken()
        {
            WithCurrentToken("read token");
            var result = _sut.AcceptNextCharacter(TokenizerStateContext, '\n');

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, IsA.ValueToken("read token"));
        }
    }
}