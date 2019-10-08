using System.Collections.Generic;
using Mastermind.Constants;
using Mastermind.Enums;

namespace Mastermind.GameServices.Output
{
    public class MessageFormatter : IMessageFormatter
    {
        public string GetHintMessage(IEnumerable<HintColour> hintColours)
        {
            var hintString = TransformHintColourEnumerableToString(hintColours);
            
            if (hintString == string.Empty)
            {
                return Constant.NoCluesPresent;
            }

            if (hintString == Constant.WinningGuess)
            {
                return Constant.WinningFeedback;
            }

            return Constant.CluePrompt + hintString + Constant.NewLine;
        }

        private static string TransformHintColourEnumerableToString(IEnumerable<HintColour> hintColours)
        {
            return string.Join(Constant.SpaceCommaDelimiter, hintColours);
        }
    }
}