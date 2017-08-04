using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Testing
{
    public class Test
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid TestId { get; set; }

        [ForeignKey("TestGroupId")]
        [Required]

        public virtual TestGroup TestGroup { get; set; }
        public Guid TestGroupId { get; set; }

        [Column(TypeName = "ntext")]
        [MaxLength(50)]
        [Required]

        public string TestName { get; set; }

        [Column(TypeName = "ntext")]
        [MaxLength(50)]
        [Required]
        public string TestDesciption { get; set; }


        public virtual ICollection<TestLink> TestLink { get; set; }
    }
}