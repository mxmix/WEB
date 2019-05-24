using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjektDS.Models
{
    [MetadataType(typeof(UsersMetadata))]
    public partial class Users
    {
        public string PowtorzHaslo { get; set; }
    }
    public class UsersMetadata
    {
       
        
        [Display(Name = "Imię")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Musisz podać Imię")]
        public string Imie { get; set; }

        [Display(Name = "Nazwisko")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Musisz podać Nazwisko")]
        public string Nazwisko { get; set; }

        [Display(Name = "Tytuł")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Musisz podać Tytuł")]
        public string Tytul { get; set; }

        [Display(Name = "Instytut")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Musisz wybrać instytut")]
        public string Instytut { get; set; }


        [Display(Name = "E-mail")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Musisz podać adres E-mail")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Musisz podać poprawny E-mail")]
        public string Email { get; set; }

        [Display(Name = "Hasło")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Musisz podać hasło")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Hasło musi posiadać minimum 6 znaków")]
        public string Haslo { get; set; }

        [Display(Name = "Powtorz hasło")]
        [DataType(DataType.Password)]
        [Compare("Haslo", ErrorMessage = "Hasło jest niepoprawne. Musi się ono zgadzać z polem Hasło.")]
        public string PowtorzHaslo { get; set; }
    }
}