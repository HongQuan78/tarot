using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tarot.Data;

namespace Tarot.Pages
{
    public class UserProfileModel : PageModel
    {
        public User user { get; set; }
        public TarotOnlineContext _context;

        public UserProfileModel(TarotOnlineContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("userId") == null)
            {
                return Redirect("/Index");
            }
            user = _context.Users.Find(HttpContext.Session.GetInt32("userId"));
            return Page();
        }
    }
}
