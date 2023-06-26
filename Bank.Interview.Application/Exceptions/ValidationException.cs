using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Interview.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public List<string> ErrorMessages { get;} = new List<string>();

        public bool HasErrorMessages { get;  }

        public ValidationException(ValidationResult validationResult)
        {
            HasErrorMessages = validationResult.IsValid;

            foreach (var error in validationResult.Errors)
            {
                ErrorMessages.Add(error.ErrorMessage);
            }
        }
    }
}
