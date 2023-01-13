using ExpenseTracker.Data;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ExpenseTracker.Controllers
{
    public class TotalExpenseLimitController : Controller
    {

        private readonly AppDbContext _context;

        public TotalExpenseLimitController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var data = _context.TotalExpenseLimit.ToList();

            return View(data);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]

        public IActionResult Create(TotalExpenseLimit totalExpnsLmt)
        {

            var data = _context.TotalExpenseLimit.ToList();
            int countRow = data.Count();

            if (ModelState.IsValid)
            {
                          
                if(countRow == 0 )
                {
                    
                    _context.TotalExpenseLimit.Add(totalExpnsLmt);
                
                    _context.SaveChanges();
                    TempData["ResultOk"] = "New Expense Limit Added Successfully !";
                    return RedirectToAction("Index", "Home");

                }
                else
                {

                    TempData["AlertMsg"] ="You can Insert Only one record into database";
                    return View();
                }
                                    
                
            }

            return View(totalExpnsLmt);

        }


        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var editrecord = _context.TotalExpenseLimit.Find(id);

            if (editrecord == null)
            {
                return NotFound();
            }
            return View(editrecord);
        }

        [HttpPost]

        public IActionResult Edit(TotalExpenseLimit totalExpenseLimit, int id)
        {
            if (ModelState.IsValid)
            {
                var dbTotal_ExpenseLimit = _context.TotalExpenseLimit.FirstOrDefault(s => s.Total_ExpenseLimit_Id.Equals(id));
                dbTotal_ExpenseLimit.Total_ExpenseLimit = totalExpenseLimit.Total_ExpenseLimit;
               
                _context.SaveChanges();
                TempData["ResultOk"] = "Total Expense Limit Updated Successfully !";
                return RedirectToAction("Index", "Home");
            }


            return View(totalExpenseLimit);
        }
    }
}
