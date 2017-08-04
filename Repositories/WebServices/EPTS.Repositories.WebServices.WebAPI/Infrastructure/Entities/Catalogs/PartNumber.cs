using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Catalogs
{
    /// <summary>
    /// This class contains the all partnumber for each model
    /// </summary>
    public class PartNumber 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PartNumberId { get; set; }

        [Column(TypeName = "ntext")]
        [MaxLength(30)]
        [Required]
        public string PartName { get; set; }

        [Column(TypeName = "ntext")]
        [MaxLength(30)]
        [Required]
        public string Description { get; set; }

        [Column(TypeName = "bit")]
        [Required]
        public bool Revision { get; set; }

        [Column(TypeName ="bit")]
        [Required]
        public bool Active { get; set; }

        public virtual ICollection<ModelDetail> ModelDetail { get; set; }

    }
}