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

        [ForeignKey("TestPlanId")]
        public virtual TestPlan TestPlan { get; set; }

        [Required]
        public Guid TestPlanId { get; set; }

        [ForeignKey("TestGroupId")]
        public virtual TestGroup TestGroup { get; set; }

        [Required]
        public Guid TestGroupId { get; set; }
    }
}