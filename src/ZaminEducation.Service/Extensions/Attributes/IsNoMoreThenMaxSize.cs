using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ZaminEducation.Service.Extensions.Attributes
{
    public class IsNoMoreThenMaxSize : ValidationAttribute
    {
        private readonly long size;
        public IsNoMoreThenMaxSize(int size)
        {
            this.size = size * 1024 * 1024;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if ((value as IFormFile).Length <= size)
                return ValidationResult.Success;
            return new ValidationResult($"file should be no more then {size} bytes");
        }
    }
}
