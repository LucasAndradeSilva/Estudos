using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebLog.Models.Entities
{
    public class Login
    {
        [Key]
        [Display(Name = "Id")]
        public int idLogin { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        public int Id_User { get; set; }

    }
}
