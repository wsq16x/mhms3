using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mhms3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace mhms3.Pages.Manager.Counselors
{
    public class ViewClientsModel : PageModel
    {
        private readonly mhms3.Data.ApplicationDbContext _context;

        public ViewClientsModel(mhms3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public IList<Client> Clients { get; set; }

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            Clients = await _context.Client
                .Where(d => d.CounselorID == id).ToListAsync();

            Console.WriteLine(id);

            return Page();
        }

    }
}
