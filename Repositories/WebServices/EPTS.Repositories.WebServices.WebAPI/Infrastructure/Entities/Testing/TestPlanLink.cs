using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Testing
{
    public class TestPlanLink
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid TestPlanLinkId { get; set; }


        [ForeignKey("TestGroupId")]
        [Required]
        public virtual TestGroup TestGroup { get; set; }
        public Guid TestGroupId { get; set; }


        [ForeignKey("TestPlanId")]
        [Required]
        public virtual TestPlan TestPlan { get; set; }
        public Guid TestPlanId { get; set; }

    }
}