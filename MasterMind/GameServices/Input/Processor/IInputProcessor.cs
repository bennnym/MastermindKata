using Mastermind.Enums;

namespace Mastermind.GameServices.Input.Processor
{
    public interface IInputProcessor
    {
        GuessColour[] GetUsersColourGuess();
    }
}