using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IntegraAdmin.Core.Resources
{
    public class ApplicationUserModel
    {
        public ApplicationUserModel()
        {
            Roles = new List<string>();
        }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public int SponsorId { get; set; }
        public List<string> Roles { get; set; }
    }
}
