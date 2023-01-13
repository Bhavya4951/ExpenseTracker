using ExpenseTracker.Data;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Numerics;

namespace ExpenseTracker.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

       
        public IActionResult Index()
        {
            var data = _context.Categories.ToList();

            return View(data);

           
        }

        //Get request
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
       
        public IActionResult Create(Category category)
        {
          
            if (ModelState.IsValid)
            {
                if (_context.Categories.Any(k => k.C_Name == category.C_Name))
                {
                   ModelState.AddModelError("Category_Name", "Category Name already exists");
                    return View();
                }
                else {

                   

                    var total = _context.TotalExpenseLimit.Take(1).FirstOrDefault();
                    var totalExpenseLimit = total.Total_ExpenseLimit;
                    var categorysum = _context.Categories.Select(x => x.C_Expense_Limit).Sum();
                   
                    var totalCategorysum = categorysum + category.C_Expense_Limit;

                    if ( totalExpenseLimit < totalCategorysum )
                    {

                        TempData["AlertMsg"] = $"Your Category Limit should be less then  {totalExpenseLimit} (Total Expense Limit) !";
                        return View();
                    }
                    else
                    {
                        _context.Categories.Add(category);
                        _context.SaveChanges();
                        TempData["ResultOk"] = "New Category Added Successfully !";
                        return RedirectToAction("Index", "Home");
                    }             
                }
                      
            }
            
            return View(category);

        }

        

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var editrecord = _context.Categories.Find(id);

            if (editrecord == null)
            {
                return NotFound();
            }
            return View(editrecord);
        }



        [HttpPost]

        public IActionResult Edit(Category category,int id)
        {
            if (ModelState.IsValid)
            {


                var total = _context.TotalExpenseLimit.Take(1).FirstOrDefault();
                var totalExpenseLimit = total.Total_ExpenseLimit;
                var categorysum = _context.Categories.Select(x => x.C_Expense_Limit).Sum();

                var rslt_Pre_C_Amt = _context.Categories.FirstOrDefault(s => s.C_Id.Equals(id));
                categorysum = categorysum - rslt_Pre_C_Amt.C_Expense_Limit; // New Sum of Category

                var totalCategorysum = categorysum + category.C_Expense_Limit;
                if (totalExpenseLimit < totalCategorysum)
                {

                    TempData["AlertMsg"] = $"Your Category Limit should be less then  {totalExpenseLimit} (Total Expense Limit) !";
                    return View();
                }
                else
                {
                    var dbcategory = _context.Categories.FirstOrDefault(s => s.C_Id.Equals(id));
                    dbcategory.C_Name = category.C_Name;
                    dbcategory.C_Expense_Limit = category.C_Expense_Limit;
                    _context.SaveChanges();
                    TempData["ResultOk"] = "Category Updated Successfully !";
                    return RedirectToAction("Index", "Home");

                }
               
               
            }

            return View(category);
        }

        public IActionResult Delete(int id)
        {
            var deleterecord = _context.Categories.Find(id);

            _context.Categories.Remove(deleterecord);
            _context.SaveChanges();
            TempData["ResultOk"] = "Category Deleted Successfully !";
            return RedirectToAction("Index", "Home");
        }
    }
}
 