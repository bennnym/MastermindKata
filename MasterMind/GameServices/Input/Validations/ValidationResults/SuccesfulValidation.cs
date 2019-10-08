using Mastermind.Constants;

namespace Mastermind.GameServices.Input.Validations.ValidationResults
{
    public class SuccessfulValidation : IValidationResult
    {
        public SuccessfulValidation()
        {
            IsValid = true;
            ErrorMessage = Constant.ValidGuessMsg;
        }
        public bool IsValid { get; }
        public string ErrorMessage { get; set; }
    }
}