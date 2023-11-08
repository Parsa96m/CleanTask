using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Data.Entitties
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
        [Display(Name = "Remember Me?")]
        public bool RememberMe { get; set; }
    }
}
