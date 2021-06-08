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
    public class IndexModel : PageModel
    {
        private readonly mhms3.Data.ApplicationDbContext _context;

        public IndexModel(mhms3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Session> Session { get;set; }

        public async Task OnGetAsync()
        {
            Session = await _context.Session
                .Include(s => s.Appointment)
                .ThenInclude(a => a.Client)
                .ToListAsync();
        }
    }
}
