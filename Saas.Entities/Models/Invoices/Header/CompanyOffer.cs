using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Saas.Entities.Models.Invoices.Rows;
using Saas.Entities.Generic;
using Saas.Entities.Models.Branch;

namespace Saas.Entities.Models.Invoices.Header
{
    public class CompanyOffer : BaseModel, IEntity
    {

        private Company _company;
        public CompanyOffer()
        {
            Rows = new List<OfferRow>();
        }



        [Display(Name = "Branch")]
        public virtual Guid? BranchId { get; set; }
        [ForeignKey("BranchId")]
        public virtual CompanyBranch? Branch { get; set; }


        [Display(Name = "Company")]
        public virtual Guid CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public virtual Company Company { get => _company; set => _company = value; }

        public virtual List<OfferRow> Rows { get; set; }
    }
}
