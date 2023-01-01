using Saas.Entities.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Saas.Entities.Models.Invoices.Header;
using Saas.Entities.Types;
using Saas.Entities.Models.Products;

namespace Saas.Entities.Models.Invoices.Rows
{
    public class OfferRow : BaseModel, IEntity
    {
        [Display(Name = "Header")]
        public Guid HeaderId { get; set; }

        [ForeignKey("HeaderId")]
        public virtual CompanyOffer Header { get; set; }



        [Required]
        [Display(Name = "CompanyProduct")]
        public Guid CompanyProductId { get; set; }

        [Required]
        [ForeignKey("CompanyProductId")]
        public virtual CompanyProducts CompanyProduct { get; set; }
       
        [Required]
        [Display(Name = "Unit")]
        public Guid CompanyProductUnitId { get; set; }
        [Required]
        [ForeignKey("CompanyProductUnitId")]
        public virtual CompanyProductUnits CompanyProductUnit { get; set; }
        
        [Required]
        public double Amount { get; set; }
        
        public Guid? ApproveGuid { get; set; }
        public DateTime? ApproveDate { get; set; }
        
        public InvoiceApproveType? InvoiceApproveType { get; set; }
    }
}
