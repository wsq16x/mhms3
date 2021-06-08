using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using mhms3.Data;
using mhms3.Models;

namespace mhms3.Pages.Appointments
{
    public class DeleteModel : PageModel
    {
        private readonly mhms3.Data.ApplicationDbContext _context;

        public DeleteModel(mhms3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Appointment Appointment { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Appointment = await _context.Appointment
                .Include(a => a.Client).FirstOrDefaultAsync(m => m.AppointmentId == id);

            if (Appointment == null)
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

            Appointment = await _context.Appointment.FindAsync(id);

            if (Appointment != null)
            {
                _context.Appointment.Remove(Appointment);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
