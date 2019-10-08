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
            var blackHints = SetExactMatchesToHints(usersGuess);
            var whiteHints = SetNonPositionMatchesToHints(usersGuess);
            var allHints = blackHints.Concat(whiteHints);
            
            return ShuffleHints(allHints);
        }

        public void SetComputerPlayersCode()
        {
            _computerPlayer.SetHiddenCode();
        }

        private IEnumerable<HintColour> SetExactMatchesToHints(IEnumerable<GuessColour> usersGuess)
        {
            var numberOfExactMatches = CalculateExactMatchesInUsersGuess(usersGuess);
            return Enumerable.Repeat(HintColour.Black, numberOfExactMatches).ToList();
        }

        private int CalculateExactMatchesInUsersGuess(IEnumerable<GuessColour> userGuess)
        {
            var computerSelection = _computerPlayer.GetCodeSelection();
            return userGuess.Where((colour, index) => colour == computerSelection[index]).Count();
        }

        private IEnumerable<HintColour> SetNonPositionMatchesToHints(IReadOnlyList<GuessColour> userGuess)
        {
            var computerSelection = _computerPlayer.GetCodeSelection();

            var unmatchedComputerSelection = GetHintSubsetThatDontHaveExactMatches(computerSelection, userGuess);
            var unmatchedUserSelection = GetHintSubsetThatDontHaveExactMatches(userGuess, computerSelection);

            return AddWhiteHintsToList(unmatchedComputerSelection, unmatchedUserSelection);
        }

        private static List<GuessColour> GetHintSubsetThatDontHaveExactMatches(IEnumerable<GuessColour> guessColours,
            IReadOnlyList<GuessColour> comparingList)
        {
            return guessColours.Where((colour, index) => colour != comparingList[index]).ToList();
        }

        private static IEnumerable<HintColour> AddWhiteHintsToList(IEnumerable<GuessColour> userSelection,
            ICollection<GuessColour> computerSelection)
        {
            var whiteHints = new List<HintColour>();
            foreach (var guess in userSelection)
            {
                if (computerSelection.Contains(guess))
                {
                    whiteHints.Add(HintColour.White);
                    computerSelection.Remove(guess);
                }
            }
            return whiteHints;
        }

        private static IEnumerable<HintColour> ShuffleHints(IEnumerable<HintColour> hints)
        {
            var random = new Random();
            return hints.OrderBy(x => random.Next(int.MaxValue));
        }
    }
}