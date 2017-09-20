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

        [StringLength(30)]
        [Index(IsUnique = true)]
        [Required]
        public string PartNumberName { get; set; }

        [StringLength(30)]
        [Required]
        public string Description { get; set; }

        [Required]
        public bool Revision { get; set; }

        [Required]
        public bool Active { get; set; }

        public virtual ICollection<ModelDetail> ModelDetail { get; set; }

    }
}