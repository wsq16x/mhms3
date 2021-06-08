using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using mhms3.Data;
using mhms3.Models;
using mhms3.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace mhms3.Pages.Appointments
{
    public class DetailsModel : PageModel
    {
        private readonly mhms3.Data.ApplicationDbContext _context;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<IdentityUser> _userManager;

        public DetailsModel(mhms3.Data.ApplicationDbContext context, IAuthorizationService authorizationService, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _authorizationService = authorizationService;
        }

        public Appointment Appointment { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Appointment = await _context.Appointment
                .Include(a => a.Client).FirstOrDefaultAsync(m => m.AppointmentId == id);

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, Appointment, CounselorOperations.Read);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }


            if (Appointment == null)
            {
                return NotFound();
            }
            return Page();
       

        }

        public async Task<IActionResult> OnPostDeleteAsync(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            Appointment = await _context.Appointment.FindAsync(id);

/*            var isAuthorized = await _authorizationService.AuthorizeAsync(User, Appointment, CounselorOperations.Delete);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }*/

            if (Appointment != null)
            {
                _context.Appointment.Remove(Appointment);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
