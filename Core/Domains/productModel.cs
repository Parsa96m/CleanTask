using System.ComponentModel.DataAnnotations;

namespace CleanTask.Domains
{
    public class productModel
    {
        public productModel()
        {
            DeleteStatus = false;
        }
        
        public int id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public DateTime ProduceDate { get; set; }
        [MaxLength(100)]
        public string Price { get; set; }
        public bool DeleteStatus { get; set; }
        [Phone]
        [MaxLength(20)]
        [Required]
        public string PhoneUser { get; set; }
        public bool Is_A_Valiable { get; set; }
        public UserMe user { get; set; }
    }
}
