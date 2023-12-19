using System.ComponentModel.DataAnnotations;

namespace Website_Database_New.Models
{
    public class OrderDetailsData
    {
        [Key]
            public int orderDetails_id {  get; set; }
        [Required]
        public string orderDetails_item_name {  get; set; }
        public int orderDetails_item_qty {  get; set; }
        public int orderDetails_item_subTotal {  get; set; }

    }
}
