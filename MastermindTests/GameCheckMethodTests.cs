using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Mastermind.Enums;
using Mastermind.GamePlay;
using Mastermind.GameServices.Players;
using Moq;
using Xunit;

namespace MastermindTests
{
    public class GameCheckMethodTests
    {
        private readonly Mock<IComputerPlayer> _computerPlayer;
        private readonly Game _game;

        public GameCheckMethodTests()
        {
            _computerPlayer = new Mock<IComputerPlayer>();
            _game = new Game(_computerPlayer.Object);
        }

        [Fact]
        public void Should_Return_An_Empty_Array_When_No_Matches_Are_Found()
        {
            // Arrange
  
            var allRedCode = new[] {GuessColour.Red, GuessColour.Red, GuessColour.Red, GuessColour.Red};
            var allBlueGuess = new[] {GuessColour.Blue, GuessColour.Blue, GuessColour.Blue, GuessColour.Blue};

            // Act
            _computerPlayer.Setup(i => i.GetCodeSelection()).Returns(allRedCode);
            var hints = _game.Check(allBlueGuess);

            // Assert
            Assert.Empty(hints);
        }

        [Theory]
        [MemberData(nameof(ExactMatches))]
        public void Should_Return_Amount_Of_Black_Hints_Related_To_Exact_Matches(GuessColour[] computerGuess,
            GuessColour[] playerGuess, HintColour[] expectedHint)
        {
            // Arrange
            _computerPlayer.Setup(i => i.GetCodeSelection()).Returns(computerGuess);
            
            // Act
            var hints = _game.Check(playerGuess);

            // Assert
            Assert.Equal(expectedHint, hints);
        }

        [Theory]
        [MemberData(nameof(NonExactMatches))]
        public void Should_Return_Amount_Of_White_Hints_Related_To_Non_Exact_Matches(GuessColour[] computerGuess,
            GuessColour[] playerGuess, HintColour[] expectedHint)
        {
            // Arrange
            _computerPlayer.Setup(i => i.GetCodeSelection()).Returns(computerGuess);

            // Act
            var hints = _game.Check(playerGuess);

            // Assert
            Assert.Equal(expectedHint, hints);
        }

        [Theory]
        [MemberData(nameof(MixOfMatches))]
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public void Should_Return_Amount_Of_Black_And_White_Hints_According_To_Matches(GuessColour[] computerGuess,
            GuessColour[] playerGuess, int expectedBlackHints, int expectedWhiteHints)
        {
            // Arrange
            _computerPlayer.Setup(i => i.GetCodeSelection()).Returns(computerGuess);

            // Act
            var hints = _game.Check(playerGuess);
            var numberOfBlackHints = hints.Count(g => g == HintColour.Black);
            var numberOfWhiteHints = hints.Count(g => g == HintColour.White);

            // Assert
            Assert.Equal(expectedBlackHints, numberOfBlackHints);
            Assert.Equal(expectedWhiteHints, numberOfWhiteHints);
        }

        public static IEnumerable<object[]> ExactMatches =>
            new List<object[]>
            {
                new object[]
                {
                    new[] {GuessColour.Red, GuessColour.Red, GuessColour.Red, GuessColour.Red},
                    new[] {GuessColour.Red, GuessColour.Blue, GuessColour.Blue, GuessColour.Blue},
                    new[] {HintColour.Black}
                },
                new object[]
                {
                    new[] {GuessColour.Green, GuessColour.Yellow, GuessColour.Orange, GuessColour.Red},
                    new[] {GuessColour.Blue, GuessColour.Blue, GuessColour.Blue, GuessColour.Red},
                    new[] {HintColour.Black}
                },
                new object[]
                {
                    new[] {GuessColour.Purple, GuessColour.Purple, GuessColour.Purple, GuessColour.Purple},
                    new[] {GuessColour.Blue, GuessColour.Purple, GuessColour.Blue, GuessColour.Red},
                    new[] {HintColour.Black}
                },
                new object[]
                {
                    new[] {GuessColour.Red, GuessColour.Red, GuessColour.Red, GuessColour.Red},
                    new[] {GuessColour.Red, GuessColour.Red, GuessColour.Blue, GuessColour.Blue},
                    new[] {HintColour.Black, HintColour.Black}
                },
                new object[]
                {
                    new[] {GuessColour.Green, GuessColour.Yellow, GuessColour.Blue, GuessColour.Red},
                    new[] {GuessColour.Blue, GuessColour.Blue, GuessColour.Blue, GuessColour.Red},
                    new[] {HintColour.Black, HintColour.Black}
                },
                new object[]
                {
                    new[] {GuessColour.Purple, GuessColour.Purple, GuessColour.Purple, GuessColour.Purple},
                    new[] {GuessColour.Blue, GuessColour.Purple, GuessColour.Blue, GuessColour.Purple},
                    new[] {HintColour.Black, HintColour.Black}
                },
                new object[]
                {
                    new[] {GuessColour.Red, GuessColour.Purple, GuessColour.Purple, GuessColour.Purple},
                    new[] {GuessColour.Red, GuessColour.Purple, GuessColour.Blue, GuessColour.Purple},
                    new[] {HintColour.Black, HintColour.Black, HintColour.Black}
                },
                new object[]
                {
                    new[] {GuessColour.Green, GuessColour.Yellow, GuessColour.Orange, GuessColour.Purple},
                    new[] {GuessColour.Green, GuessColour.Purple, GuessColour.Orange, GuessColour.Purple},
                    new[] {HintColour.Black, HintColour.Black, HintColour.Black}
                },
            };

        public static IEnumerable<object[]> NonExactMatches =>
            new List<object[]>
            {
                new object[]
                {
                    new[] {GuessColour.Red, GuessColour.Yellow, GuessColour.Red, GuessColour.Red},
                    new[] {GuessColour.Yellow, GuessColour.Blue, GuessColour.Green, GuessColour.Orange},
                    new[] {HintColour.White}
                },
                new object[]
                {
                    new[] {GuessColour.Red, GuessColour.Yellow, GuessColour.Blue, GuessColour.Red},
                    new[] {GuessColour.Yellow, GuessColour.Blue, GuessColour.Green, GuessColour.Orange},
                    new[] {HintColour.White, HintColour.White}
                },
                new object[]
                {
                    new[] {GuessColour.Red, GuessColour.Yellow, GuessColour.Blue, GuessColour.Red},
                    new[] {GuessColour.Yellow, GuessColour.Blue, GuessColour.Red, GuessColour.Orange},
                    new[] {HintColour.White, HintColour.White, HintColour.White}
                },
                new object[]
                {
                    new[] {GuessColour.Red, GuessColour.Yellow, GuessColour.Blue, GuessColour.Red},
                    new[] {GuessColour.Yellow, GuessColour.Red, GuessColour.Red, GuessColour.Orange},
                    new[] {HintColour.White, HintColour.White, HintColour.White}
                },
                new object[]
                {
                    new[] {GuessColour.Red, GuessColour.Yellow, GuessColour.Orange, GuessColour.Blue},
                    new[] {GuessColour.Yellow, GuessColour.Blue, GuessColour.Red, GuessColour.Orange},
                    new[] {HintColour.White, HintColour.White, HintColour.White, HintColour.White}
                },
                new object[]
                {
                    new[] {GuessColour.Red, GuessColour.Purple, GuessColour.Blue, GuessColour.Green},
                    new[] {GuessColour.Blue, GuessColour.Blue, GuessColour.Orange, GuessColour.Yellow},
                    new[] {HintColour.White}
                },
            };

        public static IEnumerable<object[]> MixOfMatches =>
            new List<object[]>
            {
                new object[]
                {
                    new[] {GuessColour.Red, GuessColour.Yellow, GuessColour.Red, GuessColour.Blue},
                    new[] {GuessColour.Red, GuessColour.Blue, GuessColour.Green, GuessColour.Orange},
                    1, 1
                },
                new object[]
                {
                    new[] {GuessColour.Red, GuessColour.Yellow, GuessColour.Blue, GuessColour.Red},
                    new[] {GuessColour.Yellow, GuessColour.Blue, GuessColour.Green, GuessColour.Red},
                    1, 2
                },
                new object[]
                {
                    new[] {GuessColour.Red, GuessColour.Yellow, GuessColour.Blue, GuessColour.Red},
                    new[] {GuessColour.Red, GuessColour.Blue, GuessColour.Green, GuessColour.Red},
                    2, 1
                },
                new object[]
                {
                    new[] {GuessColour.Red, GuessColour.Green, GuessColour.Blue, GuessColour.Red},
                    new[] {GuessColour.Red, GuessColour.Blue, GuessColour.Green, GuessColour.Red},
                    2, 2
                },
                new object[]
                {
                    new[] {GuessColour.Red, GuessColour.Green, GuessColour.Blue, GuessColour.Red},
                    new[] {GuessColour.Red, GuessColour.Blue, GuessColour.Green, GuessColour.Yellow},
                    1, 2
                },
            };
    }
}