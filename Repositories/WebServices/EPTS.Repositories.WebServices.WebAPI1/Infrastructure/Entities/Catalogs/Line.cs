using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Catalogs
{
    /// <summary>
    /// This class contains the all Lines
    /// </summary>

    public class Line
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid LineId { get; set; }

        [StringLength(15)]
        [Index(IsUnique = true)]
        [Required]
        public string LineName { get; set; }
    }
}