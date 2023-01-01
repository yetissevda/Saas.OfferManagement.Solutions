using FluentValidation;
using Saas.Business.Constants;
using Saas.Entities.Dto;

namespace Saas.Business.ValidationRules.FluentValidation
{
    public class AuthValidator :AbstractValidator<CompanyFirstRegisterDto>
    {
        public AuthValidator()
        {
            RuleFor(p => p.TaxNumber).NotEmpty().WithMessage(Messages.TaxNumberValidationError);
            RuleFor(p => p.TaxNumber).Length(10,11).WithMessage(Messages.TaxNumberLengtValidationError);
            RuleFor(p => p.Email).NotEmpty().WithMessage(Messages.EmailCanNotBlank);
            
        }
    }
}
