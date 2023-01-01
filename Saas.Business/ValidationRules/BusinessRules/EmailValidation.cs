using System.Text.RegularExpressions;
using Saas.Business.Constants;
using Saas.Core.Utilities.Results;

namespace Saas.Business.ValidationRules.BusinessRules;

public static class EmailValidation
{
    public static IResult Run(string mail)
    {
        bool isValidEmail = Regex.IsMatch(mail,@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",RegexOptions.IgnoreCase);
        if (!isValidEmail)
            return new ErrorResult(message: Messages.MailValidationError);
        return new SuccessResult();
    }
}
