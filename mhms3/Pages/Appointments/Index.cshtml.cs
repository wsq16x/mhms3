using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using mhms3.Data;
using mhms3.Models;
using System.Security.Claims;

namespace mhms3.Pages.Appointments
{
    public class IndexModel : PageModel
    {
        private readonly mhms3.Data.ApplicationDbContext _context;

        public IndexModel(mhms3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Appointment> Appointment { get;set; }

        public async Task OnGetAsync()
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            Appointment = await _context.Appointment
                .Where(u => u.CounselorID == currentUserID)
                .Include(a => a.Client).ToListAsync();
        }
    }
}
