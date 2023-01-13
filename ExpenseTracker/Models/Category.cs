using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Models
{
    public class Category
    {
        [Key]
        public int C_Id { get; set; }

        [StringLength(20 ,MinimumLength =3, ErrorMessage ="name length should be beween 3 to 20")]
        [RegularExpression("^[A-Za-z ]+$", ErrorMessage = "Only alphabet")]
        [Required]
        [Display(Name ="Category Name")]
        public string C_Name { get; set; }

        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "Only positive numbers")]
        [Display(Name ="Category Expense Limit")]
        [Required]
        public int C_Expense_Limit { get; set; }

        // relationsheep
        public List<Expense>? expenses { get; set; } 
    }
}
