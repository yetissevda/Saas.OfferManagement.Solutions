using System.ComponentModel;

namespace Saas.Entities.Generic
{
    public interface IDto
    {
        public string? Description { get; set; }
        public string? DescriptionTwo { get; set; }
        public string? DescriptionThree { get; set; }
        [DefaultValue(0)]
        public bool Deleted { get; set; }
    }
}
