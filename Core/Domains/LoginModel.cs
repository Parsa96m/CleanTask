using System.ComponentModel.DataAnnotations;

namespace CleanTask.Domains
{
    public class LoginModel
    {
        [MaxLength(200)]
        [Required]
        public string UserName { get; set; }
        //public string PhoneNumber { get; set; }

        [MaxLength(20)]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name ="Remember Me?")]
        public bool RememberMe { get; set; }
    }
}
