using Saas.Entities.Generic;
using System.ComponentModel.DataAnnotations;

namespace Saas.WebCoreApi.Dto
{
    public class CompanyDto : IDto
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

        public string? Description { get; set; }
        public string? DescriptionTwo { get; set; }
        public string? DescriptionThree { get; set; }
        public bool Deleted { get; set; }
    }
}
