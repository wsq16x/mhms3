using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using mhms3.Data;
using mhms3.Models;

namespace mhms3.Pages.Sessions
{
    public class DeleteModel : PageModel
    {
        private readonly mhms3.Data.ApplicationDbContext _context;

        public DeleteModel(mhms3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Session Session { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Session = await _context.Session
                .Include(s => s.Appointment).FirstOrDefaultAsync(m => m.SessionId == id);

            if (Session == null)
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

            Session = await _context.Session.FindAsync(id);

            if (Session != null)
            {
                _context.Session.Remove(Session);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
