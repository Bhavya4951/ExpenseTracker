using ExpenseTracker.Data;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;

namespace ExpenseTracker.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly AppDbContext _context;

        public ExpenseController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var data = _context.Expenses.Include(n=> n.Category).ToList(); // gate all data from Expense table

            return View(data);
        }
       
		
		public IActionResult Create()
        {
           var categoryList = _context.Categories.Select(x => new Category()
            {

               C_Id = x.C_Id,
               C_Name = x.C_Name
           }).ToList(); // which is use to get c_id and c_name as list form

           ViewBag.Category_List = new SelectList(categoryList,"C_Id","C_Name");

           return View();

          }

        [HttpPost]
        public IActionResult Create(Expense exp)
        {
            
            var expenID = exp.C_id;
            var filteredResult = (from s in _context.Categories
                                  where s.C_Id == expenID 
                                  select new {s.C_Expense_Limit }).Single();
            var cateExpense = filteredResult.C_Expense_Limit; // total sum of particular category column 

            
            var total = _context.Expenses.Where(c => c.C_id == expenID)
                     .Select(x => x.E_Amount).Sum();

            var TotalExpenseCategory = total + exp.E_Amount;

            if (ModelState.IsValid)
            {
                if(TotalExpenseCategory > cateExpense)
                {
                    var categoryList = _context.Categories.Select(x => new Category()
                    {

                        C_Id = x.C_Id,
                        C_Name = x.C_Name
                    }).ToList(); // which is use to get c_id and c_name as list form

                    ViewBag.Category_List = new SelectList(categoryList, "C_Id", "C_Name");

                    TempData["AlertMsg"] = $"Your Expense Amount should be less then  {cateExpense} (Category Limit) !";
                    return View(exp);
                }
                else
                {

                    _context.Expenses.Add(exp);
                    _context.SaveChanges();
                    TempData["ResultOk"] = "New Expense Added Successfully !";

                    return RedirectToAction("Index", "Home");
                }   
				
			}
           
			return View(exp);
		}

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var editrecord = _context.Expenses.Find(id);
            var categoryList = _context.Categories.Select(x => new Category()
            {

                C_Id = x.C_Id,
                C_Name = x.C_Name
            }).ToList(); // which is use to get c_id and c_name as list form

            ViewBag.Category_List = new SelectList(categoryList, "C_Id", "C_Name");
            if (editrecord == null)
            {
                return NotFound();
            }
            return View(editrecord);
        }

        [HttpPost]
        public IActionResult Edit(Expense exp, int id)
        {

            var expenID = exp.C_id;
            var filteredResult = (from s in _context.Categories
                                  where s.C_Id == expenID
                                  select new { s.C_Expense_Limit }).Single();
            var cateExpense = filteredResult.C_Expense_Limit; // total sum of particular category column 


            var total = _context.Expenses.Where(c => c.C_id == expenID).Select(x => x.E_Amount).Sum();  // total sum of Expense Amount in Expense table 

            var rslt_row_Pre_E_Amt = _context.Expenses.FirstOrDefault(s => s.E_Id.Equals(id));
            var new_E_Value =rslt_row_Pre_E_Amt.E_Amount;

            total = total - new_E_Value;

            var TotalExpenseCategory = total + exp.E_Amount;

            if (ModelState.IsValid)
            {
                if (TotalExpenseCategory > cateExpense)

                {
                    var categoryList = _context.Categories.Select(x => new Category()
                    {

                        C_Id = x.C_Id,
                        C_Name = x.C_Name
                    }).ToList(); // which is use to get c_id and c_name as list form

                    ViewBag.Category_List = new SelectList(categoryList, "C_Id", "C_Name");

                    TempData["AlertMsg"] = $"Your Expense Amount should be less then  {cateExpense} (Category Limit) !";
                    return View(exp);

                   
                }
                else {
                
                     var dbexp = _context.Expenses.FirstOrDefault(s => s.E_Id.Equals(id));
                    dbexp.E_Title = exp.E_Title;
                    dbexp.E_Description = exp.E_Description;
                    dbexp.E_Amount = exp.E_Amount;
                    dbexp.C_id = exp.C_id;
                        _context.SaveChanges();
                    TempData["ResultOk"] = "Expense Updated Successfully !";
                    return RedirectToAction("Index", "Home");
                }
               
            }


            return View(exp);
        }

        public IActionResult Delete(int id)
        {
            var deleterecord = _context.Expenses.Find(id);

            _context.Expenses.Remove(deleterecord);
            _context.SaveChanges();
            TempData["ResultOk"] = "Data Deleted Successfully !";
            return RedirectToAction("Index", "Home");
        }
    }
}
