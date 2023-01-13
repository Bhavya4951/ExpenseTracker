using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Data;
using ExpenseTracker.Models;

namespace ExpenseTracker.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TotalExpenseLimitsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TotalExpenseLimitsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/TotalExpenseLimits
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TotalExpenseLimit>>> GetTotalExpenseLimit()
        {
            return await _context.TotalExpenseLimit.ToListAsync();
        }

        // GET: api/TotalExpenseLimits/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TotalExpenseLimit>> GetTotalExpenseLimit(int id)
        {
            var totalExpenseLimit = await _context.TotalExpenseLimit.FindAsync(id);

            if (totalExpenseLimit == null)
            {
                return NotFound();
            }

            return totalExpenseLimit;
        }

        // PUT: api/TotalExpenseLimits/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTotalExpenseLimit(int id, TotalExpenseLimit totalExpenseLimit)
        {
            if (id != totalExpenseLimit.Total_ExpenseLimit_Id)
            {
                return BadRequest();
            }

            _context.Entry(totalExpenseLimit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TotalExpenseLimitExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TotalExpenseLimits
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TotalExpenseLimit>> PostTotalExpenseLimit(TotalExpenseLimit totalExpenseLimit)
        {
            _context.TotalExpenseLimit.Add(totalExpenseLimit);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTotalExpenseLimit", new { id = totalExpenseLimit.Total_ExpenseLimit_Id }, totalExpenseLimit);
        }

        // DELETE: api/TotalExpenseLimits/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTotalExpenseLimit(int id)
        {
            var totalExpenseLimit = await _context.TotalExpenseLimit.FindAsync(id);
            if (totalExpenseLimit == null)
            {
                return NotFound();
            }

            _context.TotalExpenseLimit.Remove(totalExpenseLimit);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TotalExpenseLimitExists(int id)
        {
            return _context.TotalExpenseLimit.Any(e => e.Total_ExpenseLimit_Id == id);
        }
    }
}
