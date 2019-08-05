using System;
using FakeItEasy;
using NUnit.Framework;
using tobixdev.github.io.FastCsv.Tokenization;
using tobixdev.github.io.FastCsv.Tokenization.StateMachine;
using tobixdev.github.io.FastCsv.Tokenization.StateMachine.States;

namespace tobixdev.github.io.FastCsv.Tests.Tokenization.StateMachine.States
{
    [TestFixture]
    public class StringMatcherStateTests : StateTestsBase
    {
        private ITokenizerState _originalState;
        private Func<ITokenizerStateContext, Token?> _onSuccess;

        [SetUp]
        public void SetUp()
        {
            _originalState = A.Fake<ITokenizerState>();
            _onSuccess = A.Fake<Func<ITokenizerStateContext, Token?>>();
        }

        [Test]
        public void AcceptNextCharacter_WithLastCharacterToMatch_ExecutesOnMatch()
        {
            var sut = CreateFor("\n");

            sut.AcceptNextCharacter(TokenizerStateContext, '\n');

            A.CallTo(() => _onSuccess.Invoke(A<ITokenizerStateContext>._)).MustHaveHappened(1, Times.Exactly);
        }

        [Test]
        public void AcceptNextCharacter_WithCharactersLeftToMatch_DoesNotCallOnMatch()
        {
            var sut = CreateFor("\r\n");

            sut.AcceptNextCharacter(TokenizerStateContext, '\r');

            A.CallTo(() => _onSuccess.Invoke(TokenizerStateContext)).MustNotHaveHappened();
        }

        [Test]
        public void AcceptNextCharacter_WithCharacterNotMatchingString_FallbackToOriginialState()
        {
            var sut = CreateFor("abc");
            sut.AcceptNextCharacter(TokenizerStateContext, 'a');
            sut.AcceptNextCharacter(TokenizerStateContext, 'b');
            
            sut.AcceptNextCharacter(TokenizerStateContext, 'x');

            AssertFallbackToOriginal();
        }

        [Test]
        public void AcceptNextCharacter_WithCharacterNotMatchingString_ReturnsTokenOfOriginalState()
        {
            A.CallTo(() => _originalState.AcceptNextCharacter(A<ITokenizerStateContext>._, 'x'))
                .Returns(Token.CreateValueToken("x"));
            var sut = CreateFor("\n");
            
            var result = sut.AcceptNextCharacter(TokenizerStateContext, 'x');

            Assert.That(result, IsA.ValueToken("x"));
        }

        [Test]
        public void Finish_WithPartiallyMatchedString_FallbackToOriginialState()
        {
            var sut = CreateFor("abc");
            sut.AcceptNextCharacter(TokenizerStateContext, 'a');
            sut.AcceptNextCharacter(TokenizerStateContext, 'b');
            
            sut.Finish(TokenizerStateContext);

            AssertFallbackToOriginal();
        }

        private void AssertFallbackToOriginal()
        {
            A.CallTo(() => _onSuccess.Invoke(TokenizerStateContext)).MustNotHaveHappened();
            A.CallTo(() => _originalState.AcceptNextCharacter(A<ITokenizerStateContext>._, 'a'))
                .MustHaveHappened(1, Times.Exactly);
            A.CallTo(() => _originalState.AcceptNextCharacter(A<ITokenizerStateContext>._, 'b'))
                .MustHaveHappened(1, Times.Exactly);
            Assert.That(TokenizerStateContext.State, Is.SameAs(_originalState));
        }

        private ITokenizerState CreateFor(string toMatch)
        {
            return new StringMatchingState(_originalState, toMatch, _onSuccess);
        }
    }
}