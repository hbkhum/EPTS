using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Catalogs
{
    /// <summary>
    /// This class contains the all model by part number
    /// </summary>

    public class ModelDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ModelDetailId { get; set; }
        
        [ForeignKey("ModelId")]
        
        public virtual ICollection<Model> Model { get; set; }
        [Required]
        public Guid ModelId { get; set; }

        [ForeignKey("PartNumberId")]
        
        public virtual ICollection<PartNumber> PartNumber { get; set; }
        [Required]
        public Guid PartNumberId { get; set; }
    }
}