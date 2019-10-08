using System;
using System.Collections.Generic;
using System.Linq;
using Mastermind.Enums;
using Mastermind.GameServices.Players;

namespace Mastermind.GamePlay
{
    public class Game : IGame
    {
        private readonly IComputerPlayer _computerPlayer;

        public Game(IComputerPlayer computerPlayer)
        {
            _computerPlayer = computerPlayer;
        }

        public IEnumerable<HintColour> Check(GuessColour[] usersGuess)
        {
            var hints = SetExactMatchesToHints(usersGuess);
            SetNonPositionMatchesToHints(usersGuess, hints);
            return ShuffleHints(hints);
        }

        public void SetComputerPlayersCode()
        {
            _computerPlayer.SetHiddenCode();
        }

        private List<HintColour> SetExactMatchesToHints(IEnumerable<GuessColour> usersGuess)
        {
            var numberOfExactMatches = CalculateExactMatchesInUsersGuess(usersGuess);
            return Enumerable.Repeat(HintColour.Black, numberOfExactMatches).ToList();
        }

        private int CalculateExactMatchesInUsersGuess(IEnumerable<GuessColour> userGuess)
        {
            var computerSelection = _computerPlayer.GetCodeSelection();
            return userGuess.Where((colour, index) => colour == computerSelection[index]).Count();
        }

        private void SetNonPositionMatchesToHints(IReadOnlyList<GuessColour> userGuess,
            List<HintColour> hints)
        {
            var computerSelection = _computerPlayer.GetCodeSelection();

            var unmatchedComputerSelection = GetHintSubsetThatDontHaveExactMatches(computerSelection, userGuess);
            var unmatchedUserSelection = GetHintSubsetThatDontHaveExactMatches(userGuess, computerSelection);

            AddWhiteHintsToList(unmatchedComputerSelection, unmatchedUserSelection, hints);
        }

        private static List<GuessColour> GetHintSubsetThatDontHaveExactMatches(IEnumerable<GuessColour> guessColours,
            IReadOnlyList<GuessColour> comparingList)
        {
            return guessColours.Where((colour, index) => colour != comparingList[index]).ToList();
        }

        private static void AddWhiteHintsToList(IEnumerable<GuessColour> userSelection,
            ICollection<GuessColour> computerSelection, ICollection<HintColour> hints)
        {
            foreach (var guess in userSelection)
            {
                if (computerSelection.Contains(guess))
                {
                    hints.Add(HintColour.White);
                    computerSelection.Remove(guess);
                }
            }
        }

        private static IEnumerable<HintColour> ShuffleHints(IEnumerable<HintColour> hints)
        {
            var random = new Random();
            return hints.OrderBy(x => random.Next(int.MaxValue));
        }
    }
}