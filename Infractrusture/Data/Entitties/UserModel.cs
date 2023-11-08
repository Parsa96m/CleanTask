using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Infrastructure.Data.Entitties
{
    public class UserModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(200)]
        [Remote("IsAnyUserName", "User", HttpMethod = "Post", AdditionalFields = "__RequestVerificationToken")]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public string Password_Again { get; set; }
        [EmailAddress]
        [Required]
        [Remote("IsAnyEmailName", "User", HttpMethod = "Post", AdditionalFields = "__RequestVerificationToken")]
        public string Email { get; set; }
        [Phone]
        [Required]
        public string PhoneUser { get; set; }
    }
}
