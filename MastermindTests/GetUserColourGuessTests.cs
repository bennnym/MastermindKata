using System.Collections.Generic;
using Mastermind.Enums;
using Mastermind.GameServices.Input.Processor;
using Mastermind.GameServices.Input.Validations.InputValidations;
using Mastermind.GameServices.Input.Validator;
using Mastermind.Infrastructure;
using Moq;
using Xunit;

namespace MastermindTests
{
    public class GetUserColourGuessTests
    {
        [Theory]
        [MemberData(nameof(ValidEntryData))]
        public void Should_Return_A_List_Of_Colours_Entered_As_GuessColours_When_Valid_Colours_Are_Entered(
            string userInput,
            List<GuessColour> expectedOutput)
        {
            // Arrange
            var validations = new List<IValidation>() {new WordCountValidation(), new ColourValidation()};
            var inputValidator = new InputValidator(validations);
            var mockConsoleService = new Mock<IConsoleDisplayService>();
            var inputReader = new InputProcessor(mockConsoleService.Object, inputValidator);
            mockConsoleService.Setup(i => i.GetConsoleInput()).Returns(userInput);

            // Act
            var guesses = inputReader.GetUsersColourGuess();

            // Assert
            Assert.Equal(expectedOutput, guesses);
        }

        public static IEnumerable<object[]> ValidEntryData =>
            new List<object[]>
            {
                new object[]
                {
                    "red yellow purple green",
                    new List<GuessColour>() {GuessColour.RED, GuessColour.YELLOW, GuessColour.PURPLE, GuessColour.GREEN}
                },
                new object[]
                {
                    "red red red red",
                    new List<GuessColour>() {GuessColour.RED, GuessColour.RED, GuessColour.RED, GuessColour.RED}
                },
                new object[]
                {
                    "red yellow blue green",
                    new List<GuessColour>() {GuessColour.RED, GuessColour.YELLOW, GuessColour.BLUE, GuessColour.GREEN}
                },
                new object[]
                {
                    "red orange blue green",
                    new List<GuessColour>() {GuessColour.RED, GuessColour.ORANGE, GuessColour.BLUE, GuessColour.GREEN}
                },
            };
    }
}