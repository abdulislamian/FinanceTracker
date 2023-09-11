using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.Models
{
    public class ExpenseCategory
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Category")]
        [Required]
        public string CategoryType{ get; set; }
    }
}
