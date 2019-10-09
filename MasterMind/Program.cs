using System.Collections.Generic;
using Mastermind.GamePlay;
using Mastermind.GameServices.Counter;
using Mastermind.GameServices.Input.Processor;
using Mastermind.GameServices.Input.Validations.InputValidations;
using Mastermind.GameServices.Input.Validator;
using Mastermind.GameServices.Output;
using Mastermind.GameServices.Players;
using Mastermind.Infrastructure;

namespace Mastermind
{
    class Program
    {
        static void Main()
        {
            var validations = new List<IValidation>()
            {
                new WordCountValidation(),
                new ColourValidation()
            };

            var inputValidator = new InputValidator(validations);
            var consoleService = new ConsoleIoService();
            
            var inputProcessor = new InputProcessor(consoleService, validator);
            
            var computerPlayer = new ComputerPlayer();
            var game = new Game(computerPlayer);

            var messageFormatter = new MessageFormatter();
            var guessCounter = new GuessCounter();
            
            var gameEngine = new GameEngine(inputProcessor, consoleService, messageFormatter, guessCounter);
            
            gameEngine.Mastermind(game); // method should be a verb!
        }
        
    }
}