using System.Collections.Generic;
using Mastermind.Enums;

namespace Mastermind.GameServices.Output
{
    public interface IMessageFormatter
    {
        string GetHintMessage(IEnumerable<HintColour> hintColours);
    }
}