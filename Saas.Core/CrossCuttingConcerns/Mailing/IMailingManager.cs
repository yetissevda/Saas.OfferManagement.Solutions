using System.Threading.Tasks;
using Saas.Core.Utilities.Results;

namespace Saas.Core.CrossCuttingConcerns.Mailing
{
    public interface IMailingManager
    {
        Task<IResult> SendSample();

       // Task<string> Send(Email mail);

    }
}
