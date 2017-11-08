using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Testing;

namespace EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Catalogs
{
    public class StationGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid StationGroupId { get; set; }

        [ForeignKey("StationId")]
        public virtual Station Station { get; set; }

        [Required]
        public Guid StationId { get; set; }

        [ForeignKey("TestPlanId")]
        public virtual TestPlan TestPlan { get; set; }

        [Required]
        public Guid TestPlanId { get; set; }

        [StringLength(15)]
        [Index(IsUnique = true)]
        [Required]
        public string StationCode { get; set; }
    }
}