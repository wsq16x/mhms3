using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using mhms3.Models;
using mhms3.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace mhms3.Pages.Call
{
    public class CallCounselorModel : PageModel
    {
        private readonly mhms3.Data.ApplicationDbContext _context;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<IdentityUser> _userManager;

        public CallCounselorModel(mhms3.Data.ApplicationDbContext context, IAuthorizationService authorizationService, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _authorizationService = authorizationService;
            _userManager = userManager;
        }
        
        [BindProperty]
        public Appointment Appointment { get; set; }

        [BindProperty]
        public string Key { get; set; }
        [BindProperty]
        public int appId { get; set; }

        public async Task <IActionResult> OnGetAsync(int ?AppId)
        {
            if (AppId == null)
            {
                Console.WriteLine("Appointment ID is Invalid");
                return NotFound();
            }

            Appointment = await _context.Appointment
               .FirstOrDefaultAsync(m => m.AppointmentId == AppId);

            appId = Appointment.AppointmentId;

            Console.WriteLine(Appointment.AppointmentId);

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, Appointment, CounselorOperations.Read);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            //set key to cookie
            //HttpContext.Session.SetString(SessionKey, key);

            return Page();
        }

      

        public async Task<IActionResult> OnPostCompleteAsync(int? id)
        {
            Console.WriteLine("Reached handler");
            Console.WriteLine(Appointment.AppointmentId);

            //var Appid = Appointment.AppointmentId;
            Appointment = await _context.Appointment
                .FirstOrDefaultAsync(m => m.AppointmentId == Appointment.AppointmentId);

            Console.WriteLine(Appointment.Status);
            Appointment.Status = "Completed";

            //_context.Attach(Appointment).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }

    }
}
