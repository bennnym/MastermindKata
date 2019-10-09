using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Mastermind.Constants;
using Mastermind.Enums;
using Mastermind.GameServices.Input.Validations.InputValidations;
using Mastermind.GameServices.Input.Validations.ValidationResults;
using Mastermind.StringMethods;

namespace Mastermind.GameServices.Input.Validator
{
    public class InputValidator : IInputValidator
    {
        private readonly List<IValidation> _validations;

        public InputValidator(List<IValidation> validations)
        {
            _validations = validations;
        }

        public IValidationResult GetValidationResults(string usersGuess)
        {
            foreach (var validation in _validations)
            {
                if (!validation.IsValid(usersGuess))
                {
                    return new FailingValidation() 
                    {
                        ErrorMessage = validation.GetErrorMessage()
                    };
                }
            }

            return new SuccessfulValidation();
        }

        public GuessColour[] GetValidColours(string usersGuess)
        {
            var colourMatches = new Regex(Constant.RegexColourPattern);

            return colourMatches
                .Matches(usersGuess)
                .Select(m => (GuessColour) Enum.Parse(typeof(GuessColour), m.Value.CapitalizeWord()))
                .ToArray();
        }
    }
}