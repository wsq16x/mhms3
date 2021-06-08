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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace mhms3.Pages.Clients
{
    public class DetailsModel : PageModel
    {
        private readonly mhms3.Data.ApplicationDbContext _context;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<IdentityUser> _userManager;

        public DetailsModel(mhms3.Data.ApplicationDbContext context, IAuthorizationService authorizationService, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _authorizationService = authorizationService;
            _userManager = userManager;
        }

        public Client Client { get; set; }
        public IList<Appointment> AppointmentList { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Client = await _context.Client.FirstOrDefaultAsync(m => m.ClientId == id);

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, Client, CounselorOperations.Read);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            AppointmentList = await _context.Appointment
            .Where(u => u.ClientID == Client.ClientId)
            .Include(a => a.Client).ToListAsync();

            var missedCount = 0;
            var CompletedCount = 0;
            var rescheduledCount = 0;
            var totalCount = 0;

            foreach(var item in AppointmentList)
            {
                totalCount++;

                if(item.Status == "Completed")
                {
                    CompletedCount++;
                }
                else if(item.Status == "Missed")
                {
                    missedCount++;
                }
                else if(item.Status == "Rescheduled")
                {
                    rescheduledCount++;
                }
                
            }

            ViewData["completed"] = CompletedCount;
            ViewData["missed"] = missedCount;
            ViewData["reschedule"] = rescheduledCount;
            ViewData["total"] = totalCount;

            if (Client == null)
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

            Client = await _context.Client.FindAsync(id);

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, Client, CounselorOperations.Delete);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            if (Client != null)
            {
                _context.Client.Remove(Client);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
