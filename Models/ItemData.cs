using System.ComponentModel.DataAnnotations;

namespace Website_Database_New.Models
{
    public class ItemData
    {

        [Key]
            public int item_id { get; set; }
        [Required]
            public int item_isbn { get; set; }
            public string item_title { get; set; }
            public string item_author { get; set; }
            public string item_genre { get; set; }
            public decimal item_price { get; set; }
            public string item_type { get; set; }
        
    }
}
