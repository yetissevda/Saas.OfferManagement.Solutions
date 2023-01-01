using FluentValidation;
using Saas.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Saas.Entities.Models.Invoices.Rows;
using Saas.Business.Constants;

namespace Saas.Business.ValidationRules.FluentValidation
{
    public class OfferRowValidator : AbstractValidator<OfferRow>
    {
        public OfferRowValidator()
        {
            RuleFor(p => p.HeaderId).NotEmpty().WithMessage("Header Value needed");
            RuleFor(p => p.CompanyProductId).NotEmpty().NotNull().WithMessage("CompanyProductId Value needed");
            RuleFor(x=>x.CompanyProductUnitId).NotEmpty().NotNull().WithMessage("CompanyProductUnitId cannot be null");
        }
    }
}
