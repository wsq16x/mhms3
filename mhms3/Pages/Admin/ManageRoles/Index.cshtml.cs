using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace mhms3.Pages.Admin.ManageRoles
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {

        private readonly mhms3.Data.ApplicationDbContext _context;

        public IndexModel(mhms3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ViewModel> _ListOfUser;

        public class ViewModel
        {
            public string UserID { get; set;}
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Role { get; set; }
        }

        public void OnGet()
        {

            var ListOfUser = _context.Roles
                .Where(r => r.Name != "Admin")
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
                    Role = combined.a.Name
                }
                );

            _ListOfUser = ListOfUser;
        }
    }
}
