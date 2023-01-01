using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Saas.Entities.Generic;
using Saas.Entities.Models.User;

namespace Saas.Entities.Models.UserClaims
{
    [Comment("Kullanici Yetkileri")]
    [Table("CompanyOperationUserClaim",Schema = "CompanyRoles")]
    public class CompanyOperationUserClaim :BaseModel,IEntity
    {
        [Display(Name = "CompanyUser")]
        public virtual Guid CompanyUserId { get; set; }

        [ForeignKey("CompanyUserId")]
        public virtual CompanyUser CompanyUser { get; set; }


        [Display(Name = "CompanyOperationClaim")]
        public virtual Guid CompanyOperationClaimId { get; set; }

        [ForeignKey("CompanyOperationClaimId")]
        public virtual CompanyOperationClaim OperationClaim { get; set; }

        
    }
}
