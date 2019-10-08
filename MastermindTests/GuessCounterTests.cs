using Mastermind.Constants;
using Mastermind.GameServices.Counter;
using Moq;
using Xunit;

namespace MastermindTests
{
    public class GuessCounterTests
    {
        [Fact]
        public void Should_Increase_Count_Of_Guesses()
        {
            // Arrange
            var guessCounter = new GuessCounter();

            // Act
            guessCounter.IncrementCount();
            var countMessage = guessCounter.GetRemainingGuessMessage();
            var expectedOutput = Constant.GuessCountPrompt + 1 + Constant.NewLine + Constant.RemainingGuessesPrompt +
                                 (Constant.GuessLimit - 1) + Constant.NewLine;

            // Assert
            Assert.Equal(expectedOutput, countMessage);
        }
    }
}