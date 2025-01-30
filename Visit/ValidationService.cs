using System.Text;
using System.Text.RegularExpressions;

using QueueProgram.Popup;


namespace QueueProgram.Visit
{
    public interface IValidationService
    {
        ValidationResult Validate(string SelectedOption, string Email, string Phone);
    }
    
    public class ValidationService : IValidationService
    {
        private readonly IPopupService service;

        public ValidationService(IPopupService service)
        {
            this.service = service;
        }
        private readonly string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        private readonly string phonePattern = @"^\+?[0-9]{7,15}$";
        private bool IsOptionValid(string SelectedOption)
        {
            var aviliableOptions = service.GetOptions().options;
            return aviliableOptions.Contains(SelectedOption);
        }

        private bool IsValidEmail(string Email)
        {
            return Regex.IsMatch(Email, emailPattern);
        }

        private bool IsValidPhoneNumber(string Phone)
        {
            return Regex.IsMatch(Phone, phonePattern);
        }
        public ValidationResult Validate(string SelectedOption, string Email, string Phone)
        {
            var result = new ValidationResult(true, "");
            var errorMessageBuilder = new StringBuilder();

            if (!IsOptionValid(SelectedOption))
            {
                errorMessageBuilder.Append(" Selected option does not exist.");
                result.IsValid = false;
            }

            if (string.IsNullOrEmpty(SelectedOption))
            {
                errorMessageBuilder.Append(" Please select an option.");
                result.IsValid = false;
            }

            if (string.IsNullOrEmpty(Email) || !IsValidEmail(Email))
            {
                errorMessageBuilder.Append("Please enter a valid email address. ");
                result.IsValid = false;
            }

            if (string.IsNullOrEmpty(Phone) || !IsValidPhoneNumber(Phone))
            {
                errorMessageBuilder.Append("Please enter a valid phone number. ");
                result.IsValid = false;
            }

            result.ErrorMessage = errorMessageBuilder.ToString().Trim();

            return result;
        }
    }

    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public string ErrorMessage { get; set; }
        public ValidationResult(bool IsValid, string ErrorMessage)
        {
            this.IsValid = IsValid;
            this.ErrorMessage = ErrorMessage;

        }
    }
}
