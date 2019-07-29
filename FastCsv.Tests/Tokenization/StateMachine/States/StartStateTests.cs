using FakeItEasy;
using NUnit.Framework;
using tobixdev.github.io.FastCsv.Tokenization.StateMachine;
using tobixdev.github.io.FastCsv.Tokenization.StateMachine.States;

namespace tobixdev.github.io.FastCsv.Tests.Tokenization.StateMachine.States
{
    [TestFixture]
    public class StartStateTests
    {
        private ITokenizerStateContext _tokenizerStateContext;
        private ITokenizerState _sut;

        [SetUp]
        public void SetUp()
        {
            _tokenizerStateContext = A.Fake<ITokenizerStateContext>();
            _sut = new DefaultState();
        }

        [Test]
        public void AcceptNextCharacter_WithComma_ReturnsNewValueToken()
        {
            A.CallTo(() => _tokenizerStateContext.ResetToken()).Returns("");
            var result = _sut.AcceptNextCharacter(_tokenizerStateContext, ',');
            
            Assert.That(result.HasValue, Is.True);
            Assert.That(result.Value.Value, Is.EqualTo(""));
            Assert.That(result.Value.TokenType, Is.EqualTo(TokenType.Value));
        }

    }
}