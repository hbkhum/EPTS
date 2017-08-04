using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Catalogs
{
    /// <summary>
    /// This class contains the all business units
    /// </summary>
    public class BusinessUnit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid BusinessUnitId { get; set; }

        [Column(TypeName = "ntext")]
        [MaxLength(30)]
        [Required]
        public string BusinessUnitName { get; set; }

        [Column(TypeName = "ntext")]
        [MaxLength(50)]
        public string BusinessUnitDescription { get; set; }

        [Column(TypeName = "varbinary")]
        public byte[] BusinessUnitImage { get; set; }

    }
}
