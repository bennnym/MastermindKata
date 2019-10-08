using Mastermind.Constants;

namespace Mastermind.GameServices.Counter
{
    public class GuessCounter : IGuessCounter
    {
        private int _guessCount;

        public void IncrementCount()
        {
            _guessCount += 1;
        }

        public string GetRemainingGuessMessage()
        {
            return IsGuessLimitExceeded() ? Constant.GuessLimitExceededErrorMsg : GetCurrentGuessCountMessage();
        }

        public bool IsGuessLimitExceeded()
        {
            return _guessCount >= Constant.GuessLimit;
        }

        private string GetCurrentGuessCountMessage()
        {
            return Constant.GuessCountPrompt + _guessCount + Constant.NewLine + Constant.RemainingGuessesPrompt +
                   (Constant.GuessLimit - _guessCount) + Constant.NewLine;
        }
    }
}