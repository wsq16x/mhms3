using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace mhms3.Pages.Manager.Counselors
{
    public class IndexModel : PageModel
    {
        private readonly mhms3.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _UserManager;

        public IndexModel(mhms3.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _UserManager = userManager;
        }

        public class ViewModel
        {
            public string UserID { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
        }

        public IEnumerable<ViewModel> ListCounselor;

        public void OnGet()
        {
            var counselors = _context.Roles
                .Where(w => w.Name == "User")
                .Join(_context.UserRoles,
                a => a.Id,
                b => b.RoleId,
                (a, b) => new { a, b })
                .Join(_context.Users,
                combined => combined.b.UserId,
                n => n.Id,
                (combined, n) => new ViewModel
                {
                    UserID = n.Id,
                    UserName = n.UserName,
                    Email = n.Email,
                    Phone = n.PhoneNumber
                }
                );

            ListCounselor = counselors;

        }
    }
}
