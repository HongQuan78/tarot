using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Tarot.Pages
{
    public class LogoutModel : PageModel
    {
        public void OnGet()
        {
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }
            HttpContext.Session.Clear();
            Response.Redirect("/Index"); //
        }
    }
}
