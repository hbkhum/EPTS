using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Catalogs
{
    public class Flow
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid FlowId { get; set; }

        [ForeignKey("ModelId")]
        public Model Model { get; set; }

        [Required]
        public Guid ModelId { get; set; }

        [ForeignKey("LineId")]
        public Line Line { get; set; }

        [Required]
        public Guid LineId { get; set; }

        [StringLength(15)]
        [Index(IsUnique = true)]
        [Required]
        public string FlowName { get; set; }

    }
}