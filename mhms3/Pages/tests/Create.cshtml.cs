using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using mhms3.Data;
using mhms3.Models;

namespace mhms3.Pages.tests
{
    public class CreateModel : PageModel
    {
        private readonly mhms3.Data.ApplicationDbContext _context;

        public CreateModel(mhms3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ClientID"] = new SelectList(_context.Client, "ClientId", "ClientId");
            return Page();
        }

        [BindProperty]
        public test test { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.test.Add(test);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
