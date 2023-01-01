using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Saas.Entities.Generic;

namespace Saas.Entities.Models.UserClaims
{
    [Comment("Yetkiler")]
    [Table("CompanyOperationClaim",Schema = "CompanyRoles")]
    public class CompanyOperationClaim :BaseModel,IEntity
    {
        [Required]
        public string Name { get; set; } //   { get => _name; init => _name = value; }
    }
}
