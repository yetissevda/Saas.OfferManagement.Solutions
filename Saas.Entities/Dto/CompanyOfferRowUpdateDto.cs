using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saas.Entities.Dto
{
    public class CompanyOfferRowUpdateDto : CompanyOfferRowDto
    {
        [Required]
        public Guid ID { get; set; }
    }
}
