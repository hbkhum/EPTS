using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Testing
{
    public class TestPlan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid TestPlanId { get; set; }

        [StringLength(50)]
        [Required]
        [Index(IsUnique = true)]
        public string TestPlanName { get; set; }

        [StringLength(50)]
        //[Required]
        public string Desciption { get; set; }

        public virtual ICollection<TestGroup> TestGroup { get; set; }
    }
}