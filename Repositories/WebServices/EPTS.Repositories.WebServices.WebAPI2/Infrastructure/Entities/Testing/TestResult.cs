using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Testing
{
    public class TestResult:test
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid TestResultId { get; set; }
        [ForeignKey("TestId")]
        public virtual Test Test { get; set; }
        [Required]
        public Guid TestId { get; set; }

        [Required]
        public string Result { get; set; }

        [Required]
        public DateTime? StarTime { get; set; }

        [StringLength(15)]
        [Required]
        public string TestUnitName { get; set; }

        [StringLength(15)]
        [Required]
        public string TestTypeName { get; set; }

        [Required]
        public DateTime? FinishTime { get; set; }
        [Required]
        public string Status { get; set; }
    }
}