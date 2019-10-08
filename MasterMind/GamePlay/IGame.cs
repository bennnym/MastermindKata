using System.Collections.Generic;
using Mastermind.Enums;

namespace Mastermind.GamePlay
{
    public interface IGame
    {
        void SetComputerPlayersCode();
        IEnumerable<HintColour> Check(GuessColour[] usersGuess);
    }
}