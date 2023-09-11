using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.Models
{
    public class Items
    {
        [Key]
        public int ID { get; set; }
        public string Borrower { get; set; }
        public string Lendor { get; set; }
        [DisplayName("Item Name")]
        public string ItemName { get; set; }
    }
}
