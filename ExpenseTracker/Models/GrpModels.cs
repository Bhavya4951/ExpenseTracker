using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Models
{
    public class GrpModels
    {
        public List<Category> cats { get; set; }
        public List<Expense> exps { get; set; }

        public List<TotalExpenseLimit> totalexplims { get; set; }
    }
}
