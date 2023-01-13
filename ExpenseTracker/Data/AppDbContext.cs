using ExpenseTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)// defalt constructor
        {

        }
        public DbSet<Category> Categories { get; set; }  
        public DbSet<Expense> Expenses { get; set; }  
        public DbSet<TotalExpenseLimit> TotalExpenseLimit { get; set; }

      
    }
}
