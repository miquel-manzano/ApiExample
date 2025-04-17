using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages
{
    public class LogoutModel : PageModel
    {
        //No sera void, ara retorn IActionResult per poder fer una redireccio.
        public IActionResult OnGet()
        {
            HttpContext.Session.Clear(); //Elimina tota la sessio
            return RedirectToPage("/Index");
        }
    }
}
