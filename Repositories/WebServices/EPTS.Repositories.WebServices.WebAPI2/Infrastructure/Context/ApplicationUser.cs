using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EPTS.Repositories.WebServices.WebAPI.Infrastructure.Context
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        //[Required]
        //public byte Level { get; set; }

        //[Required]
        //public DateTime JoinDate { get; set; }
    }
}