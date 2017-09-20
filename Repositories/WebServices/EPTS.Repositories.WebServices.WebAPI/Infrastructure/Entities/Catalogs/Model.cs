using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Catalogs;

namespace EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Catalogs
{
    /// <summary>
    /// This class contains the all models for each families
    /// </summary>

    public class Model
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ModelId { get; set; }

        [StringLength(30)]
        [Index(IsUnique = true)]
        [Required]
        public string ModelName { get; set; }

        [ForeignKey("FamilyId")]
        public virtual Family Family { get; set; }
        [Required]
        public Guid FamilyId { get; set; }
        public virtual ICollection<ModelDetail> ModelDetails { get; set; }
    }
}