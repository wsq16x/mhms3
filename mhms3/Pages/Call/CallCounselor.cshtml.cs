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
        public Session Session { get; set; }


        public async Task <IActionResult> OnGetAsync(int ?AppId)
        {
            if (AppId == null)
            {
                Console.WriteLine("Appointment ID is Invalid");
                return NotFound();
            }

            Appointment = await _context.Appointment
               .FirstOrDefaultAsync(m => m.AppointmentId == AppId);

            if(Appointment.Status != "Pending" && Appointment.Status != "Rescheduled")
            {
                return RedirectToPage("/Error");
            }

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
            Session = await _context.Session
                .Include(a => a.Appointment)
                .FirstOrDefaultAsync(m => m.AppointmentID == Appointment.AppointmentId);

            Console.WriteLine(Session.Appointment.Status);
            Session.Appointment.Status = "Completed";

            //_context.Attach(Appointment).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Redirect("~/Sessions/Details?id="+Session.SessionId);
        }

    }
}
