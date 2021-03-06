using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace mhms3.Pages.Call
{
    [AllowAnonymous]
    public class CallClientModel : PageModel
    {
        //public const string SessionKey = "_Key";
        public IActionResult OnGet(string? key)
        {
            if (key == null)
            {
                Console.WriteLine("Session Key Invalid");
                return RedirectToPage("/Error");
            }
            Console.WriteLine("Your key is: ");
            Console.WriteLine(key);

            //set key to cookie
            //HttpContext.Session.SetString(SessionKey, key);

            Key = key;

            return Page();

        }

        [BindProperty]
        public string Key { get; set; }
    }
}
