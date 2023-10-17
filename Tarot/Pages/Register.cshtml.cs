using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tarot.Model;
using Tarot.Service;

namespace Tarot.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public RegisterDTO UserInput { get; set; }
        private readonly AccountService _accountService;
        public RegisterModel(AccountService accountService)
        {
            _accountService = accountService;
        }
        public void OnGet()
        {

        }

        public void OnPost()
        {
            try
            {
                bool registrationSuccess = _accountService.Register(UserInput);
                if (registrationSuccess)
                {
                    TempData["Message"] = "Registration successful!";
                }
                else
                {
                    TempData["Message"] = "Registration failed. Please try again.";
                }
            }
            catch (Exception)
            {
                TempData["Message"] = "Registration failed. Please try again.";
                Redirect("/");
            }
            
        }
    }
}
