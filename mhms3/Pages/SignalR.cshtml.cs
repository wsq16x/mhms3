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
    public class SignalRModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
