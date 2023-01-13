using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Models
{
    public class TotalExpenseLimit
    {
        [Key]
        public int Total_ExpenseLimit_Id { get; set; }
        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "Only positive numbers")]
        [Display(Name = "Total Expense Limit")]
        [Required]
        public int Total_ExpenseLimit { get; set; }
    }
}
