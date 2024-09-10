using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TodoAppNew.Models.VMs
{
    public class RegisterVM
    {
        [Required(ErrorMessage ="Ad alanı zorunludur")]
        [DisplayName("İsim")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Soyad alanı zorunludur")]
        [DisplayName("Soyisim")]
        public string LastName { get; set; }


        [Required(ErrorMessage ="Mail adresi zorunludur.")]
        [EmailAddress]
        public string Email { get; set; }


        [Required(ErrorMessage ="Şifre zorunludur")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Şifreler uyuşmuyor.")]
        public string ConfirmPassword { get; set; }

       
    }
}
