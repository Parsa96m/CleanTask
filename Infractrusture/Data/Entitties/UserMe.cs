using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Infrastructure.Data.Entitties
{
    public class UserMe
    {
        public UserMe()
        {
            DeleteStatus = false;
            IsRegister = false;
        }
        public int id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(200)]
        public string UserName { get; set; }
        public bool DeleteStatus { get; set; }
        [Phone]
        [Required]
        [MaxLength(20)]
        public string PhoneUser { get; set; }
        [EmailAddress]
        [Required]
        [MaxLength(200)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsRegister { get; set; }
        public List<productModel> products { get; set; }
    }
}
