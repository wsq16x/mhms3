using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace mhms3.Pages
{
    [AllowAnonymous]
    public class CallJoinModel : PageModel
    {
        public void OnGet()
        {

        }
        [BindProperty]
        public string Key { get; set; }

        public IActionResult OnPost()
        {   
            Console.WriteLine(Key);
            return RedirectToPage("/Call/CallClient", new {key = Key});
        }
    }
}
