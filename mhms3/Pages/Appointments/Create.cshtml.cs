﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using mhms3.Data;
using mhms3.Models;
using System.Security.Claims;

namespace mhms3.Pages.Appointments
{
    public class CreateModel : PageModel
    {
        private readonly mhms3.Data.ApplicationDbContext _context;

        public CreateModel(mhms3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ClientID"] = new SelectList(_context.Client, "ID", "ID");
            return Page();
        }

        [BindProperty]
        public Appointment Appointment { get; set; }
        //   .ToListAsync();

        public int err { get; set; }
        



        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD

        public async Task<IActionResult> OnPostAsync()
        {
            var ClientID = HttpContext.Request.Form["ClientID"];
            DateTime AppDate = Convert.ToDateTime(HttpContext.Request.Form["AppDate"]);
            DateTime TimeStart = Convert.ToDateTime(HttpContext.Request.Form["TimeStart"]);
            DateTime TimeEnd = Convert.ToDateTime(HttpContext.Request.Form["TimeEnd"]);
            Console.WriteLine("=======================================");
            Console.WriteLine(ClientID);
            Console.WriteLine(AppDate);
            Console.WriteLine(TimeStart);
            Console.WriteLine(TimeEnd);
            Console.WriteLine("=======================================");
            if (!ModelState.IsValid)
            {
                return Page();
            }
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            Appointment.CounselorID = currentUserID;

            //var appointments = _context.Appointment
            //   .Select(Appointment => new SelectListItem { Value = AppDate, Appointment.TimeStart = TimeStart, Appointment.TimeEnd = TimeEnd })
            //same conunselor
            //same date
            // requested start time >= existing end time in db
            var queryApointments = _context.Appointment
                .Where(appointment => appointment.AppDate == AppDate &&
                    appointment.CounselorID == currentUserID &&
                    TimeStart < appointment.TimeEnd &&
                    TimeEnd > appointment.TimeStart)
                .ToList();

            if (queryApointments.Any())
            {
                Console.WriteLine("This line is printed");
                Console.WriteLine(queryApointments);
                foreach (Appointment apointment in queryApointments)
                {
                    Console.WriteLine("HI");
                    Console.WriteLine("=======================================");
                    Console.WriteLine("     {0}",apointment.CounselorID);
                    Console.WriteLine("     {0}", apointment.AppDate);
                    Console.WriteLine("     {0}", apointment.TimeStart);
                    Console.WriteLine("     {0}", apointment.TimeEnd);
                    Console.WriteLine("=======================================");

                    err = 1;

                    return Page();
                }


            }
            else
            {
                _context.Appointment.Add(Appointment);
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