using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCApp.Models
{
    public class CustomerModel
    { 
        public string CustomerCode { get; set; }
        [Display(Name ="Ime")]
        [Required(ErrorMessage ="Treba nam Vaše ime.")]
        public string FirstName { get; set; }

        [Display(Name = "Prezime")]
        [Required(ErrorMessage = "Treba nam Vaše prezime.")]
        public string LastName { get; set; }

        [Display(Name = "Broj Telefona")]
        [Required(ErrorMessage = "Treba nam Vaš broj telefona.")]
        public string PhoneNumber { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Adresa E-Pošte")]
        [Required(ErrorMessage = "Treba nam Vaša adresa E-Pošte.")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Lozinka")]
        [Required(ErrorMessage = "Treba nam Vaša lozinka.")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potvrdi Lozinku")]
        [Required(ErrorMessage = "Treba nam Vaša lozinka.")]
        [Compare("Password",ErrorMessage ="Lozinke se ne poklapaju!")]
        public string ConfirmPassword { get; set; }





    }
}