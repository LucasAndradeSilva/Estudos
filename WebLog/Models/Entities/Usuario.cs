using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebLog.Models.Entities
{
    public class Usuario
    {
        [Key]
        [Display(Name ="Id")]
        public int idUser { get; set; }

        [Required]
        [Display(Name ="Nome")]
        public string Nome { get; set; }

        [Required]
        [Display(Name ="Idade")]
        public int Idade { get; set; }

        [Required]
        [Display(Name ="CPF")]
        public string CPF { get; set; }

    }
}
