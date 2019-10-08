using Mastermind.Enums;

namespace Mastermind.GameServices.Players
{
    public interface IComputerPlayer
    {
        GuessColour[] GetCodeSelection();
        void SetHiddenCode();
    }
}