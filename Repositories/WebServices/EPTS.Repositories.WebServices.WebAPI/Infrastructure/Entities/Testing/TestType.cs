using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Testing
{
    public class TestType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid TestTypeId { get; set; }

        [Column(TypeName = "ntext")]
        [MaxLength(15)]
        [Required]
        public string TestTypeName { get; set; }
    }
}