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
        [Required]
        public Model Model { get; set; }
        public Guid ModelId { get; set; }


        [ForeignKey("LineId")]
        [Required]
        public Line Line { get; set; }
        public Guid LineId { get; set; }

        [Column(TypeName = "ntext")]
        [MaxLength(15)]
        [Required]
        public string FlowName { get; set; }

    }
}