using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saas.Entities.Dto
{
    public class OfferCreateRequestDto
    {

        public CompanyOfferUpdateDto Header { get; set; }
        public List<CompanyOfferRowUpdateDto> Rows { get; set; }

    }
}
