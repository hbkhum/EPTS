using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

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

        [StringLength(30)]
        [Index(IsUnique = true)]
        [Required]
        public string BusinessUnitName { get; set; }

        [StringLength(50)]
        public string BusinessUnitDescription { get; set; }

        public byte[] BusinessUnitImage { get; set; }

    }
}
