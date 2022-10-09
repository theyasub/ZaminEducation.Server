using System.ComponentModel.DataAnnotations;

namespace ZaminEducation.Api.Extensions.Attributes
{
    public class IsNoMoreThenMaxSize : ValidationAttribute
    {
        private readonly int size;

        public IsNoMoreThenMaxSize(int size)
        {
            this.size = size;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if ((value as IFormFile).Length <= size)
                return ValidationResult.Success;
            return new ValidationResult($"File should be no more then {size} bytes");
        }
    }
}
