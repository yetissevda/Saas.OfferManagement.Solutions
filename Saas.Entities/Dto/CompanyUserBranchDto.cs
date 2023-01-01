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
using Saas.Entities.Models.User;

namespace Saas.Entities.Dto
{
    public class CompanyUserBranchDto : IDto
    {
        public  Guid CompanyUserId { get; set; }
        public virtual Guid BranchId { get; set; }

        public string? Description { get; set; }
        public string? DescriptionTwo { get; set; }
        public string? DescriptionThree { get; set; }
        public bool Deleted { get; set; }
    }
}
