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
                    new List<GuessColour>() {GuessColour.Red, GuessColour.Yellow, GuessColour.Purple, GuessColour.Green}
                },
                new object[]
                {
                    "red red red red",
                    new List<GuessColour>() {GuessColour.Red, GuessColour.Red, GuessColour.Red, GuessColour.Red}
                },
                new object[]
                {
                    "red yellow blue green",
                    new List<GuessColour>() {GuessColour.Red, GuessColour.Yellow, GuessColour.Blue, GuessColour.Green}
                },
                new object[]
                {
                    "red orange blue green",
                    new List<GuessColour>() {GuessColour.Red, GuessColour.Orange, GuessColour.Blue, GuessColour.Green}
                },
            };
    }
}