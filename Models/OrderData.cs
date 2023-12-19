using System.ComponentModel.DataAnnotations;

namespace Website_Database_New.Models
{
    public class OrderData
    {
        [Key]
        public int order_id {  get; set; }
        public int c_id { get; set; }
        [Required]
        public DateTime order_date {  get; set; }
        public Decimal order_total {  get; set; }
        public string order_add {  get; set; }
        public string order_status {  get; set; }
        
    }
}
