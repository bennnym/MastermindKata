using System.Collections.Generic;
using Mastermind.GameServices.Input.Validations.InputValidations;
using Mastermind.GameServices.Input.Validations.ValidationResults;
using Mastermind.GameServices.Input.Validator;
using Xunit;

namespace MastermindTests
{
    public class InputValidatorTests
    {
        private readonly InputValidator _inputValidator;

        public InputValidatorTests()
        {
            var validations = new List<IValidation>
            {
                new ColourValidation(), new WordCountValidation()
            };

            _inputValidator = new InputValidator(validations);
        }

        [Theory]
        [InlineData("red red red red")]
        [InlineData("purple green orange red")]
        [InlineData("blue,purple,orange,red")]
        [InlineData("blue,yellow,orange,yellow")]
        public void Should_Return_SuccessfulValidation_When_Input_Is_Valid(string userGuess)
        {
            // Act
            var validationResult = _inputValidator.GetValidationResults(userGuess);

            // Assert
            Assert.IsType<SuccessfulValidation>(validationResult);
        }
        
        [Theory]
        [InlineData("red orange PINK green")]
        [InlineData("red orange ")]
        [InlineData("red ")]
        [InlineData("")]
        public void Should_Return_FailingValidation_When_Input_Is_Invalid(string userGuess)
        {
            // Act
            var validationResult = _inputValidator.GetValidationResults(userGuess);

            // Assert
            Assert.IsType<FailingValidation>(validationResult);
        }
        
    }
}