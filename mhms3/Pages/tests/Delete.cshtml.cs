using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using mhms3.Data;
using mhms3.Models;

namespace mhms3.Pages.tests
{
    public class DeleteModel : PageModel
    {
        private readonly mhms3.Data.ApplicationDbContext _context;

        public DeleteModel(mhms3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public test test { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            test = await _context.test
                .Include(t => t.Client).FirstOrDefaultAsync(m => m.testId == id);

            if (test == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            test = await _context.test.FindAsync(id);

            if (test != null)
            {
                _context.test.Remove(test);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
