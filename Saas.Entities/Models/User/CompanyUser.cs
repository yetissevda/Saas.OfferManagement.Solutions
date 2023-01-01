using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Saas.Entities.Generic;

namespace Saas.Entities.Models.User
{
    [Comment("Firma Kullanicilari")]
    [Table("CompanyUser", Schema = "Company")]
    public class CompanyUser : BaseModel, IEntity
    {
        private Company _company;
        private string _fullName;
        private string _email;
        private byte[] _passWordHash;
        private byte[] _passWordSalt;
        private List<CompanyUserBranches> _userBranches;



        [Display(Name = "Company")]
        public virtual Guid CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public virtual Company Company { get => _company; set => _company = value; }


        //[Display(Name = "Branch")]
        //public virtual int? BranchId { get; set; }

        //[ForeignKey("BranchId")]
        //public virtual CompanyBranch Branch { get; set; }


        [Required]
        public string FullName { get => _fullName; init => _fullName = value; }

        [Required]
        public string Email { get => _email; init => _email = value; }

        [Required]
        public byte[] PassWordSalt { get => _passWordSalt; init => _passWordSalt = value; }

        [Required]
        public byte[] PassWordHash { get => _passWordHash; init => _passWordHash = value; }

        [Required, DefaultValue(0), Comment("IsStudent? Yes-No")]
        public bool IsStudent { get; set; }

        [Required, DefaultValue(0), Comment("Company Admin")]
        public bool SysAdmin { get; set; }

        [Required, DefaultValue(0), Comment("Branch Admin")]
        public bool BranchAdmin { get; init; }



        public virtual List<CompanyUserBranches> UserBranches { get => _userBranches; set => _userBranches = value; }

    }
}
