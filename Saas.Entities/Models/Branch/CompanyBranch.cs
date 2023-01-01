using Microsoft.EntityFrameworkCore;
using Saas.Entities.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Saas.Entities.Models.Branch
{
    [Comment("Firma şubeleri")]
    [Table("CompanyBranch", Schema = "Company")]
    public class CompanyBranch : BaseModel, IEntity
    {
        private Company _company;
        private string _fullName;



        [Display(Name = "Company")]
        public virtual Guid CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public virtual Company Company { get => _company; set => _company = value; }

        [Required]
        public string FullName { get => _fullName; set => _fullName = value; }



    }
}
