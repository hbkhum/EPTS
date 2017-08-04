using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Testing
{
    public class TestGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid TestGroupId { get; set; }

        [Column(TypeName = "ntext")]
        [MaxLength(15)]
        [Required]
        public string TestGroupName { get; set; }

        public virtual ICollection<TestGroupLink> TestGroupLink { get; set; }
        
        public virtual ICollection<TestPlanLink> TestPlanLink { get; set; }

    }
}