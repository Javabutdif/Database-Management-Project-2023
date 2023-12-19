using System.ComponentModel.DataAnnotations;

namespace Website_Database_New.Models
{
    public class CartData
    {
        [Key]
        public int cart_id { get; set; }
        [Required]
       public DateTime date_created { get; set; }
    }
}
