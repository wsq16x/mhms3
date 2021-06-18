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

namespace mhms3.Pages.Sessions
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

        [BindProperty]
        public Session Session { get; set; }
        [BindProperty]
        public TimeSpan duration {get; set;}

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Session = await _context.Session
                .Include(s => s.Appointment)
                .ThenInclude(a => a.Client)
                .FirstOrDefaultAsync(m => m.SessionId == id);

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, Session, CounselorOperations.Read);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            //calculating duration
            duration = Session.TimeEnd - Session.TimeStart;

            if (Session == null)
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
