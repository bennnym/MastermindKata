using System.Linq;
using System.Text.RegularExpressions;
using Mastermind.Constants;

namespace Mastermind.GameServices.Input.Validations.InputValidations
{
    public class WordCountValidation : IValidation
    {
        public bool IsValid(string userInput)
        {
            var wordSearch = new Regex(Constant.RegexWordSearchPattern);

            return wordSearch.Matches(userInput).Count() == 4;
        }

        public string GetErrorMessage()
        {
            return Constant.IncorrectNumberOfColoursErrorMsg;
        }
    }
}