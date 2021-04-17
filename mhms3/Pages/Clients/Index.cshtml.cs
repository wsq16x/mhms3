using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using mhms3.Data;
using mhms3.Models;
using Microsoft.AspNetCore.Authorization;

namespace mhms3.Pages.Clients
{
    
    public class IndexModel : PageModel
    {
        private readonly mhms3.Data.ApplicationDbContext _context;

        public IndexModel(mhms3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Client> Client { get;set; }

        public async Task OnGetAsync()
        {
            Client = await _context.Client.ToListAsync();
        }
    }
}
