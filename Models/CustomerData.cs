using System.ComponentModel.DataAnnotations;

namespace Website_Database_New.Models
{
    public class CustomerData
    {
        [Key]
        public int c_id { get; set; }
        [Required]
        public string c_name { get; set; }
        public string c_email { get; set; }
        public string c_address { get; set; }
        public string c_status { get; set; }
        public string c_password { get; set; }
    }
}
