using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace mhms3.Pages.Admin.ManageRoles
{
    public class EditRoleModel : PageModel
    {
        private readonly mhms3.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _UserManager;


        public EditRoleModel(mhms3.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _UserManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roleList = await _context.Roles
                .Where(d => d.Name != "Admin")
                .Select(a => a.Name).ToListAsync();

            ViewData["Role"] = new SelectList(roleList);


            UID = id;

            return Page();
        }

        [BindProperty]
        public string Role { get; set; }

        [BindProperty]
        public string UID { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {

            var user = await _UserManager.FindByIdAsync(UID);
            var oldRoleNames = await _UserManager.GetRolesAsync(user);

            Console.WriteLine(UID);
            Console.WriteLine(Role);

            await _UserManager.RemoveFromRolesAsync(user, oldRoleNames);

            await _UserManager.AddToRoleAsync(user, Role);

            return RedirectToPage("./Index");
        }
    }
}
