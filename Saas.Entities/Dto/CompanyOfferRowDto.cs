using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Saas.Entities.Generic;
using Saas.Entities.Models.Invoices.Header;
using Saas.Entities.Models.Products;
using Saas.Entities.Types;

namespace Saas.Entities.Dto
{
    public class CompanyOfferRowDto : IDto
    {
        public Guid CompanyProductId { get; set; }


        public Guid CompanyProductUnitId { get; set; }

        public double Amount { get; set; }

       

  

        // public InvoiceApproveType? InvoiceApproveType { get; set; }


        public string? Description { get; set; }
        public string? DescriptionTwo { get; set; }
        public string? DescriptionThree { get; set; }
        public bool Deleted { get; set; }
    }
}
