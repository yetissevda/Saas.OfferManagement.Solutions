using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Saas.Entities.Generic;
using Saas.Entities.Models.Branch;

namespace Saas.Entities.Models.User
{
    [Comment("Kullanicinin Bağli oldugu Şubeler")]
    [Table("CompanyUserBranches", Schema = "Company")]
    public class CompanyUserBranches : BaseModel, IEntity
    {




        [Display(Name = "CompanyUser")]
        public virtual Guid CompanyUserId { get; set; }

        [ForeignKey("CompanyUserId"),]

        public virtual CompanyUser CompanyUser { get; set; }


        [Display(Name = "Branch")]
        public virtual Guid BranchId { get; set; }
        [ForeignKey("BranchId")]
        public virtual CompanyBranch Branch { get; set; }


        [Required, DefaultValue(0)]
        public bool IsAdmin { get; set; }


    }
}
