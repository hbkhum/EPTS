using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Testing
{
    public class TestUnit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid TestUnitId { get; set; }

        [StringLength(15)]
        [Required]
        [Index(IsUnique = true)]
        public string TestUnitName { get; set; }
    }
}