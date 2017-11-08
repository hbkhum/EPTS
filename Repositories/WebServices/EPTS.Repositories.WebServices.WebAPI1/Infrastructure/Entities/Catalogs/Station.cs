using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Catalogs
{
    /// <summary>
    /// This class contains the all Stations for each line
    /// </summary>
    public class Station
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid StationId { get; set; }

        [StringLength(50)]
        [Required]
        public string StationName { get; set; }

        [ForeignKey("LineId")]
        public virtual Line Line { get; set; }

        [Required]
        public Guid LineId { get; set; }


        [ForeignKey("StationTypeId")]
        public virtual StationType StationType { get; set; }

        [Required]
        public Guid StationTypeId { get; set; }



    }
}