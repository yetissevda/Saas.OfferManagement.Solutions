using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Saas.Entities.Generic;
using Saas.Entities.Models.Branch;
using Saas.Entities.Models.Invoices.Rows;
using Saas.Entities.Models;

namespace Saas.Entities.Dto
{
    public class CompanyOfferDto :IDto
    {
        

        public  Guid? BranchId { get; set; }
       
        public  Guid CompanyId { get; set; }
        
       //public List<CompanyOfferRowDto> Rows { get; set; }
        


        public string? Description { get; set; }
        public string? DescriptionTwo { get; set; }
        public string? DescriptionThree { get; set; }
        public bool Deleted { get; set; }
    }
}
