using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Saas.Entities.Generic;

namespace Saas.Entities.Dto
{
    public class CompanyProductUnitDto : IDto
    {
        public string UnitName { get; set; }
      
        public string UnitShortName { get; set; }
      
        public double? Calculate { get; set; } = 1; //for calculating unit total like kg 1*1000
     
        public double? CalculateCost { get; set; } = 1; //for calculating unit total like kg 1*1000
     
        public  Guid CompanyId { get; set; }


        public string? Description { get; set; }
        public string? DescriptionTwo { get; set; }
        public string? DescriptionThree { get; set; }
        public bool Deleted { get; set; }
    }
}
