using System;
using System.Linq;
using System.Text.RegularExpressions;
using Mastermind.Constants;
using Mastermind.Enums;
using Mastermind.StringMethods;

namespace Mastermind.GameServices.Input.Validations.InputValidations
{
    public class ColourValidation : IValidation
    {
        public bool IsValid(string userInput)
        {
            var colourMatches = new Regex(Constant.RegexColourPattern);

            var validColoursMatched = colourMatches
                .Matches(userInput)
                .Select(m => (GuessColour) Enum.Parse(typeof(GuessColour), m.Value.CapitalizeWord()));
            
            return validColoursMatched.Count() == Constant.ValidNumberOfColours;
        }

        public string GetErrorMessage()
        {
            return Constant.InvalidColourErrorMsg;
        }
    }
}