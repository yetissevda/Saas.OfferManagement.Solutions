using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Saas.Entities.Generic;

namespace Saas.Entities.Dto
{
    public class CompanyProductUnitUpdateDto : CompanyProductUnitDto
    {
        public Guid ID { get; set; }
    }
}
