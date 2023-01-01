using Saas.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Saas.Entities.Generic;

namespace Saas.Entities.Dto
{
    public class CompanyProductDto : IDto
    {
        public string ProductName { get; set; }
        public string ProductShortName { get; set; }

        public string Barcode { get; set; }

        public Guid CompanyId { get; set; }

        public string? Description { get; set; }
        public string? DescriptionTwo { get; set; }
        public string? DescriptionThree { get; set; }
        public bool Deleted { get; set; }
    }
}
