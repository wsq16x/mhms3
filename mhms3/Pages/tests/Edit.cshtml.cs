﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mhms3.Data;
using mhms3.Models;

namespace mhms3.Pages.tests
{
    public class EditModel : PageModel
    {
        private readonly mhms3.Data.ApplicationDbContext _context;

        public EditModel(mhms3.Data.ApplicationDbContext context)
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
           ViewData["ClientID"] = new SelectList(_context.Client, "ClientId", "ClientId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(test).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!testExists(test.testId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool testExists(int id)
        {
            return _context.test.Any(e => e.testId == id);
        }
    }
}
