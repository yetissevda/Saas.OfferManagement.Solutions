using FluentValidation;
using Saas.Business.Constants;
using Saas.Entities.Models;

namespace Saas.Business.ValidationRules.FluentValidation
{
    public class CompanyValidator : AbstractValidator<Company>
    {
        public CompanyValidator()
        {
            RuleFor(p => p.TaxNumber).NotEmpty().WithMessage(Messages.TaxNumberValidationError);
            RuleFor(p => p.TaxNumber).Length(10, 11).WithMessage(Messages.TaxNumberLengtValidationError);
            //  .When(p => p.FullName == "");
            // RuleFor(p => p.ID).GreaterThanOrEqualTo(10).When(p => p.FullName == "");

            #region örnek kullanımlar commentli

            //RuleFor(p => p.TaxNumber).Must(StarWithWithA); 

            #endregion
        }

        //private static bool StarWithWithA(string arg)
        //{
        //    return arg.StartsWith("A");
        //}
    }
}
