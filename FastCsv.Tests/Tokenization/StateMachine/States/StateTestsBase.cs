using FakeItEasy;
using NUnit.Framework;
using tobixdev.github.io.FastCsv.Tokenization.StateMachine;

namespace tobixdev.github.io.FastCsv.Tests.Tokenization.StateMachine.States
{
    public class StateTestsBase
    {
        protected ITokenizerStateContext TokenizerStateContext;

        [SetUp]
        public void SetUpBase()
        {
            TokenizerStateContext = A.Fake<ITokenizerStateContext>();
        }
        
        protected void WithCurrentToken(string tokenValue)
        {
            A.CallTo(() => TokenizerStateContext.ResetToken()).Returns(tokenValue);
        }

        protected void AssertCharacterAppendedToToken(char character)
        {
            A.CallTo(() => TokenizerStateContext.AppendCharacter(character)).MustHaveHappened(1, Times.Exactly);
        }
    }
}