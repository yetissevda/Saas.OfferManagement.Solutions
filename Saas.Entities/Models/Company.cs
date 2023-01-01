using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Saas.Entities.Generic;

namespace Saas.Entities.Models
{
    [Comment("FirmaBilgileri")]
    [Table("Company", Schema = "Company")]
    public class Company : BaseModel, IEntity
    {
        private string _fullName;
        private string _taxNumber;
        [Required]
        public string FullName { get => _fullName; init => _fullName = value; }
        public string? Adress { get; set; }
        [Required, MaxLength(11)]
        public string TaxNumber { get => _taxNumber; init => _taxNumber = value; }
        public string? TaxOffice { get; set; }
        public string? PhoneNumberOne { get; set; }
        public string? PhoneNumberTwo { get; set; }

    }
}
