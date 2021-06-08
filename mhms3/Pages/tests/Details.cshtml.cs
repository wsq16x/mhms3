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
    public class DetailsModel : PageModel
    {
        private readonly mhms3.Data.ApplicationDbContext _context;

        public DetailsModel(mhms3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
