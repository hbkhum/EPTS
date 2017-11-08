using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Testing
{

    public abstract class test
    {
        [StringLength(50)]
        [Required]
        //[Index(IsUnique = true)]
        public string TestName { get; set; }

        [Required]
        public int Sequence { get; set; }

        [StringLength(15)]
        [Required]
        public string LoLimit { get; set; }

        [Required]
        public Guid TestUnitId { get; set; }

        [ForeignKey("TestTypeId")]
        public virtual TestType TestType { get; set; }

        [StringLength(15)]
        [Required]
        public string HiLimit { get; set; }


        [ForeignKey("TestUnitId")]
        public virtual TestUnit TestUnit { get; set; }

        [Required]
        public Guid TestTypeId { get; set; }
    }
    public class Test:test
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid TestId { get; set; }

        [ForeignKey("TestGroupId")]
        public virtual TestGroup TestGroup { get; set; }
        [Required]
        public Guid TestGroupId { get; set; }

        [StringLength(50)]
        [Required]
        public string TestDesciption { get; set; }









    }
}