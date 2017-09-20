using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Catalogs
{
    /// <summary>
    /// This class contains the all Stations type for each line
    /// </summary>
    public class StationType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid StationTypeId { get; set; }

        [StringLength(15)]
        [Index(IsUnique = true)]
        [Required]
        public string StationDescription { get; set; }
    }
}