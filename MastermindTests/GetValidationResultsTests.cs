using System.Collections.Generic;
using Mastermind.GameServices.Input.Validations.InputValidations;
using Mastermind.GameServices.Input.Validator;
using Xunit;

namespace MastermindTests
{
    public class GetValidationResultsTests
    {
        private readonly InputValidator _inputValidatorWordLength;

        public GetValidationResultsTests()
        {
            _inputValidatorWordLength = new InputValidator(
                new List<IValidation>()
                {
                    new WordCountValidation()
                });

        }
        [Theory]
        [InlineData("red yellow blue, orange")]
        [InlineData("red,red,blue,orange")]
        [InlineData("red,   red,blue//orange")]
        [InlineData("red yellow purple green")]
        public void Should_Return_True_If_The_User_Has_Entered_Four_Words(string input)
        {
            // Act
            var validationResult =
                _inputValidatorWordLength.GetValidationResults(input);

            // Assert
            Assert.True(validationResult.IsValid);
        }

        [Theory]
        [InlineData("red blue, orange, red ,red")]
        [InlineData("redred,blue,orange")]
        [InlineData("red")]
        [InlineData("")]
        public void Should_Return_False_If_The_User_Has_Entered_Less_Or_More_Than_Four_Words(string input)
        {
            // Act
            var validationResults =
                _inputValidatorWordLength.GetValidationResults(input);

            // Assert
            Assert.False(validationResults.IsValid);
        }
    }
}