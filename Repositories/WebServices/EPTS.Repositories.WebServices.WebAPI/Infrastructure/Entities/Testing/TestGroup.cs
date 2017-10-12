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

        [ForeignKey("TestPlanId")]
        public virtual TestPlan TestPlan { get; set; }
        [Required]
        public Guid TestPlanId { get; set; }

        [StringLength(50)]
        [Required]
        public string TestGroupName { get; set; }

        [StringLength(50)]
        //[Required]
        public string Description { get; set; }

        [Required]
        public int Sequence { get; set; }


        public virtual ICollection<Test> Test { get; set; }

    }
}