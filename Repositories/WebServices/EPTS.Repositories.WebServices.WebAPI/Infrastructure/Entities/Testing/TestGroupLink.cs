using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Testing
{
    public class TestGroupLink
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid TestGroupLinkId { get; set; }


        [ForeignKey("TestGroupId")]
        [Required]

        public virtual TestGroup TestGroup { get; set; }
        public Guid TestGroupId { get; set; }

        [Column(TypeName = "int")]
        [Required]
        public int Sequence { get; set; }

        [Column(TypeName = "ntext")]
        [MaxLength(40)]
        [Required]
        public string Description { get; set; }

    }
}