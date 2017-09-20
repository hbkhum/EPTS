using System;
using System.Collections.Generic;
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
        public virtual TestGroup TestGroup { get; set; }

        [Required]
        public Guid TestGroupId { get; set; }

        [ForeignKey("TestId")]
        public virtual Test Test { get; set; }

        [Required]
        public Guid TestId { get; set; }

        [Required]
        public int Sequence { get; set; }

        [StringLength(15)]
        [Required]
        public string Description { get; set; }


    }
}