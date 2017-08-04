using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Catalogs
{
    /// <summary>
    /// This class contains the all families for each business units
    /// </summary>
 

    public class Family
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid FamilyId { get; set; }

        [Column(TypeName = "ntext")]
        [MaxLength(30)]
        [Required]
        public string FamilyName { get; set; }

        [ForeignKey("BusinessUnitId")]
        
        public virtual BusinessUnit BusinessUnit { get; set; }
        [Required]
        public Guid BusinessUnitId { get; set; }

        //public virtual ICollection<Model> Models { get; set; }
    }
}