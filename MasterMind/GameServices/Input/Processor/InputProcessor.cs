using Mastermind.Enums;
using Mastermind.GameServices.Input.Validator;
using Mastermind.Infrastructure;

namespace Mastermind.GameServices.Input.Processor
{
    public class InputProcessor : IInputProcessor
    {
        private readonly IConsoleDisplayService _consoleIoService;
        private readonly IInputValidator _inputValidator;

        public InputProcessor(IConsoleDisplayService ioService, IInputValidator inputValidator)
        {
            _consoleIoService = ioService;
            _inputValidator = inputValidator;
        }

        public GuessColour[] GetUsersColourGuess()
        {
            var isUserInputValid = false;
            var usersGuess = string.Empty;

            while (isUserInputValid == false)
            {
                usersGuess = _consoleIoService.GetConsoleInput();

                var validationResult = _inputValidator.GetValidationResults(usersGuess);

                isUserInputValid = validationResult.IsValid;

                _consoleIoService.DisplayOutput(validationResult.ErrorMessage);
            }

            return _inputValidator.GetValidColours(usersGuess);
        }

    }
}