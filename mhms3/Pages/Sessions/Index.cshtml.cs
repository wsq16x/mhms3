using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using mhms3.Data;
using mhms3.Models;
using Microsoft.AspNetCore.Identity;

namespace mhms3.Pages.Sessions
{
    public class IndexModel : PageModel
    {
        private readonly mhms3.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(mhms3.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Session> Session { get;set; }

        public async Task OnGetAsync()
        {
            Session = await _context.Session
                .Where(s=>s.CounselorID == _userManager.GetUserId(User))
                .Include(s => s.Appointment)
                .ThenInclude(a => a.Client)
                .ToListAsync();
        }
    }
}
