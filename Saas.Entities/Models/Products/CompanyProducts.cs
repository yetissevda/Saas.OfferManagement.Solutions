using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Saas.Entities.Generic;

namespace Saas.Entities.Models.Products
{
    public class CompanyProducts : BaseModel, IEntity
    {
        private Company _company;

        [Required]
        public string ProductName { get; set; }
        public string ProductShortName { get; set; }
        [MaxLength]
        public string Barcode { get; set; }

        [Display(Name = "Company")]
        public virtual Guid CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public virtual Company Company { get => _company; set => _company = value; }
    }
}
