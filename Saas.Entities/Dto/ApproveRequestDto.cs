using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Saas.Entities.Types;

namespace Saas.Entities.Dto
{
    public class ApproveRequestDto
    {

        public Guid HeaderId { get; set; }
        public InvoiceApproveType approve { get; set; }

        public List<Guid>? RowIdList { get; set; }
    }
}
