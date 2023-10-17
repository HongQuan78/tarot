using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tarot.Data;
using Tarot.Service;

namespace Tarot.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string username { get; set; }
        [BindProperty]
        public string password { get; set; }
        private AccountService _accountService;

        public LoginModel()
        {
            _accountService = new AccountService();
        }
        public void OnGet()
        {

        }
        public void OnPost() 
        {
            User currentUser = _accountService.Login(username, password);
            string fullname = currentUser.Fullname;
            int userId = currentUser.UserId;
            if (currentUser == null)
            {
                Response.Redirect("/Login");
            }
            else
            {
                HttpContext.Session.SetString("fullname", fullname);
                HttpContext.Session.SetInt32("userId", userId);
                Response.Redirect("/Index");
            }
        }
    }
}
