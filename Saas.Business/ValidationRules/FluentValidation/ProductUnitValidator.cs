using FluentValidation;
using Saas.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Saas.Entities.Models.Products;

namespace Saas.Business.ValidationRules.FluentValidation
{
    public class ProductUnitValidator:AbstractValidator<CompanyProductUnits>
    {
        public ProductUnitValidator()
        {
            RuleFor(x=>x.CompanyId).NotEmpty().NotNull().WithMessage("CompanyId cannot be null");
        }
    }
}
