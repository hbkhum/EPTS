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

        [Column(TypeName = "ntext")]
        [MaxLength(15)]
        [Required]
        public string TestUnitName { get; set; }
    }
}