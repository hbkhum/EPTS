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

        [Column(TypeName = "ntext")]
        [MaxLength(15)]
        [Required]
        public string StationName { get; set; }


        [ForeignKey("LineId")]
        [Required]
        public virtual Line Line { get; set; }
        public Guid LineId { get; set; }

        [ForeignKey("StationTypeId")]
        [Required]
        public virtual StationType StationType { get; set; }
        public Guid StationTypeId { get; set; }


    }
}