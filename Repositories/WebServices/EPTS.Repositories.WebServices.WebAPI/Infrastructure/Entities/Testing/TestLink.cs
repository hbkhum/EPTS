using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Testing;

namespace EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Testing
{
    public class TestLink
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid TestLinkId { get; set; }


        [ForeignKey("TestId")]
        [Required]
        public virtual Test Test { get; set; }
        public Guid TestId { get; set; }

        [Column(TypeName = "ntext")]
        [MaxLength(15)]
        [Required]
        public string LoLimit { get; set; }

        [Column(TypeName = "ntext")]
        [MaxLength(15)]
        [Required]
        public string HiLimit { get; set; }


        [ForeignKey("TestUnitId")]
        [Required]
        public virtual TestUnit TestUnit { get; set; }
        public Guid TestUnitId { get; set; }

        [ForeignKey("TestTypeId")]
        [Required]
        public virtual TestType TestType { get; set; }
        public Guid TestTypeId { get; set; }

    }
}