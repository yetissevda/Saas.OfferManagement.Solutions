using Saas.Entities.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Saas.Entities.Models.Products
{
    [Serializable]
    public class CompanyProductUnits : BaseModel, IEntity
    {
        private Company _company;


        [Required]
        public string UnitName { get; set; }
        [Required]
        public string UnitShortName { get; set; }
        [DefaultValue(1)]
        public double? Calculate { get; set; } = 1; //for calculating unit total like kg 1*1000
        [DefaultValue(1)]
        public double? CalculateCost { get; set; } = 1; //for calculating unit total like kg 1*1000

        [Display(Name = "Company")]
        public virtual Guid CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public virtual Company Company { get => _company; set => _company = value; }
    }
}
