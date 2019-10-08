using System.Collections.Generic;
using System.Linq;
using Mastermind.Constants;
using Mastermind.Enums;
using Mastermind.GameServices.Counter;
using Mastermind.GameServices.Input.Processor;
using Mastermind.GameServices.Output;
using Mastermind.Infrastructure;

namespace Mastermind.GamePlay
{
    public class GameEngine
    {
        private readonly IInputProcessor _inputProcessor;
        private readonly IConsoleDisplayService _consoleDisplayService;
        private readonly IMessageFormatter _messageFormatter;
        private readonly IGuessCounter _guessCounter;

        public GameEngine(IInputProcessor inputProcessor, IConsoleDisplayService consoleDisplayService,
            IMessageFormatter messageFormatter, IGuessCounter guessCounter)
        {
            _inputProcessor = inputProcessor;
            _consoleDisplayService = consoleDisplayService;
            _messageFormatter = messageFormatter;
            _guessCounter = guessCounter;
        }

        public void Mastermind(IGame game)
        {
            _consoleDisplayService.DisplayOutput(Constant.WelcomeInstructions);
            
            var hints = Enumerable.Empty<HintColour>();

            game.SetComputerPlayersCode();

            while (IsNotWinningCombination(hints))
            {
                _consoleDisplayService.DisplayOutput(_guessCounter.GetRemainingGuessMessage());

                if (_guessCounter.IsGuessLimitExceeded()) _consoleDisplayService.ExitApplication();

                hints = game.Check(_inputProcessor.GetUsersColourGuess());

                _guessCounter.IncrementCount();

                _consoleDisplayService.DisplayOutput(_messageFormatter.GetHintMessage(hints));
            }
        }

        private static bool IsNotWinningCombination(IEnumerable<HintColour> hints)
        {
            return hints.Count(hc => hc == HintColour.Black) != Constant.BlackHintsRequiredToWin;
        }
    }
}