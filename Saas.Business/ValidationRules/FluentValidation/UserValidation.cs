using FluentValidation;
using Saas.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Saas.Business.Constants;

namespace Saas.Business.ValidationRules.FluentValidation
{
    public class UserValidation : AbstractValidator<CompanyUserDto>
    {
        public UserValidation()
        {
            RuleFor(p => p.Email).NotEmpty().WithMessage(Messages.UserValidationEmailError);
            RuleFor(p => p.Password).NotEmpty().WithMessage(Messages.UserValidatonPasswordError);
        }
        
    }
}
