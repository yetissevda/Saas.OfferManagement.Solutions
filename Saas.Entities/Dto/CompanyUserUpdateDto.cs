using Saas.Entities.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saas.Entities.Dto
{
    public class CompanyUserUpdateDto : CompanyUserDto
    {
        public Guid UserId  { get; set; }
    }
}
