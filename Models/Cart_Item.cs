using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Website_Database_New.Models
{
    public class Cart_Item
    {
        [Key]
      public  int cart_item_id { get; set; }
        public int cart_id { get; set; }
        public int item_id {  get; set; }
        [Required]
        public int quantity {  get; set; }
        public int total { get; set; }

    }
}
