using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using mhms3.Data;
using mhms3.Models;
using System.Security.Claims;
using mhms3.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace mhms3.Pages.Appointments
{
    public class CreateModel : PageModel
    {
        private readonly mhms3.Data.ApplicationDbContext _context;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<IdentityUser> _userManager;

        public CreateModel(mhms3.Data.ApplicationDbContext context, IAuthorizationService authorizationService, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _authorizationService = authorizationService;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            //ClaimsPrincipal currentUser = this.User;
            var currentUserID = _userManager.GetUserId(User);
            var clients = _context.Client.Where(u => u.CounselorID == currentUserID);
            ViewData["ClientID"] = new SelectList(clients, "ClientId", "ClientId");
            return Page();


        }

        [BindProperty]
        public Appointment Appointment { get; set; }
        //   .ToListAsync();

        public int Err { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD

        public async Task<IActionResult> OnPostAsync()
        {
            
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var currentUserID = _userManager.GetUserId(User);

            //get list of conflicting appointments
            var queryApointments = _context.Appointment
                .Where(appointment => appointment.AppDate == Appointment.AppDate &&
                    appointment.CounselorID == currentUserID &&
                    Appointment.TimeStart <= appointment.TimeEnd &&
                    Appointment.TimeEnd >= appointment.TimeStart)
                .ToList();

            foreach(var item in queryApointments)
            {
                Console.WriteLine(item.AppointmentId);
            }

            var SessionKey = RNGCrypto.RandomString(12);
         

            if (queryApointments.Any())
            {
                    Err = 1;
                    var clients = _context.Client.Where(u => u.CounselorID == currentUserID);
                    ViewData["ClientID"] = new SelectList(clients, "ClientId", "ClientId");
                    return Page();
            }
            else
            {
                _context.Appointment.Add(Appointment);
                Appointment.CounselorID = currentUserID;
                Appointment.SessionKey = SessionKey;

                Console.WriteLine(Appointment.ClientID);
                Console.WriteLine(Appointment.AppDate);

                await _context.SaveChangesAsync();
            }


            //bool isEmpty = !list.Any();
            //if (isEmpty)
            //{
            //    // error message
            //}
            //else
            //{
            //    // show grid
            //}

            return RedirectToPage("./Index");
        }
    }
}
