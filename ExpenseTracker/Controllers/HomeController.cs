using ExpenseTracker.Data;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;


namespace ExpenseTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? id)
        {


            GrpModels grpmodels = new GrpModels();

            grpmodels.cats = _context.Categories.ToList();
          
            grpmodels.totalexplims = _context.TotalExpenseLimit.ToList();

            if (id == null)
            {
                grpmodels.exps = _context.Expenses.ToList();
            }
            else
            {
                grpmodels.exps = _context.Expenses.Where(z => z.C_id == id).ToList();
            }

            //return View(md);

            return View(grpmodels);
        }

       
    }
}