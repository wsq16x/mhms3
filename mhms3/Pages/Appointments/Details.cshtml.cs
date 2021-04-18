﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using mhms3.Data;
using mhms3.Models;

namespace mhms3.Pages.Appointments
{
    public class DetailsModel : PageModel
    {
        private readonly mhms3.Data.ApplicationDbContext _context;

        public DetailsModel(mhms3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Appointment Appointment { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Appointment = await _context.Appointment
                .Include(a => a.Client).FirstOrDefaultAsync(m => m.ID == id);

            if (Appointment == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
