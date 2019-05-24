using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjektDS.Models
{
    public class Login
    {

        [Display(Name = "E-mail")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Podaj swój adres E-mail")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Podaj Hasło")]
        [DataType(DataType.Password)]
        public string Haslo { get; set; }

        [Display(Name = "Zapamiętaj mnie")]
        public bool Zapamietaj { get; set; }
    }
}