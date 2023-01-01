using Microsoft.EntityFrameworkCore;
using Saas.Entities.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Saas.Entities.Models
{
    [Comment("Log Kayıtları")]
    [Table("Logs")]
    public class Logs : BaseModel, IEntity
    {
        private string _detail;
        private string _audit;

        public string Detail { get => _detail; set => _detail = value; }
        public DateTime Date { get; set; }
        public string Audit { get => _audit; set => _audit = value; }


    }
}
