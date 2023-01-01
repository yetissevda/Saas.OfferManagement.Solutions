using Saas.Entities.Generic;

namespace Saas.Entities.Dto
{
    public class CompanyBranchDto : IDto
    {
        public string CompanyId { get; set; }
        //private Company _company;
        public string FullName { get; set; }




        public string? Description { get; set; }
        public string? DescriptionTwo { get; set; }
        public string? DescriptionThree { get; set; }
        public bool Deleted { get; set; }
    }
}
