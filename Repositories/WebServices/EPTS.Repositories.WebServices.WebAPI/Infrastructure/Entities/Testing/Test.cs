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

        [StringLength(50)]
        [Required]
        //[Index(IsUnique = true)]
        public string TestName { get; set; }

        [StringLength(50)]
        [Required]
        public string TestDesciption { get; set; }

        [StringLength(15)]
        [Required]
        public string LoLimit { get; set; }

        [StringLength(15)]
        [Required]
        public string HiLimit { get; set; }


        [ForeignKey("TestUnitId")]
        public virtual TestUnit TestUnit { get; set; }

        [Required]
        public Guid TestUnitId { get; set; }

        [ForeignKey("TestTypeId")]
        public virtual TestType TestType { get; set; }

        [Required]
        public Guid TestTypeId { get; set; }


        //public virtual ICollection<TestLink> TestLink { get; set; }
    }
}