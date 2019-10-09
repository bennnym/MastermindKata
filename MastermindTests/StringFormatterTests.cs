using Mastermind.StringMethods;
using Xunit;

namespace MastermindTests
{
    public class StringFormatterTests
    {
        [Theory]
        [InlineData("red", "Red")]
        [InlineData("blue", "Blue")]
        [InlineData("yellow", "Yellow")]
        [InlineData("green", "Green")]
        [InlineData("gReEn", "Green")]
        [InlineData("RED", "Red")]
        [InlineData("OrAnGe", "Orange")]
        public void Should_Capitalise_Word(string input, string expectedOutput)
        {
            // Act
            var capitalisedWord = input.CapitalizeWord();
            
            // Assert
            Assert.Equal(expectedOutput, capitalisedWord);
        }
    }
}