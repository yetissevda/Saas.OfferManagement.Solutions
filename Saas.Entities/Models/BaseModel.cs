using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Saas.Entities.Models
{
    //Todo
    /// <summary>
    ///  remove this class props in swagger documents
    /// </summary>


    public class BaseModel
    {
        [Key]
        public Guid ID { get; set; }
        public string? Description { get; set; }
        public string? DescriptionTwo { get; set; }
        public string? DescriptionThree { get; set; }
        [DefaultValue(0)]
        public bool Deleted { get; set; } = false;
        public Guid? CreatedByGui { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedByGui { get; set; }
        public string? UpdatedBy { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
