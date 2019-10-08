using Mastermind.Enums;
using Mastermind.GameServices.Input.Validations.ValidationResults;

namespace Mastermind.GameServices.Input.Validator
{
    public interface IInputValidator
    {
        IValidationResult GetValidationResults(string userInput);
        GuessColour[] GetValidColours(string userGuess);
    }
}