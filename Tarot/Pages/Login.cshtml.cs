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

        [BindProperty]
        public bool IsRemember { get; set;}
        private AccountService _accountService;

        public LoginModel()
        {
            _accountService = new AccountService();
        }
        public void OnGet()
        {
            if (HttpContext.Request.Cookies.ContainsKey("username"))
            {
                username = HttpContext.Request.Cookies["username"];
                password = HttpContext.Request.Cookies["password"];
            }
        }
        public void OnPost() 
        {
            try
            {
                User currentUser = _accountService.Login(username, password);
                string fullname = currentUser.Fullname;
                int userId = currentUser.UserId;
                if (currentUser == null)
                {
                    TempData["Fail"] = "Đăng nhập thất bại!.";
                    Redirect("/Login");
                }
                else
                {
                    if (IsRemember)
                    {
                        // Lưu username vào cookie
                        HttpContext.Response.Cookies.Append("username", username, new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(14) });

                        // Lưu một token vào cookie (ví dụ: bạn có thể lưu mật khẩu đã mã hóa)
                        HttpContext.Response.Cookies.Append("password", password, new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(14) });
                    }
                    else
                    {
                        // Xóa cookies nếu người dùng không chọn "Nhớ mật khẩu"
                        HttpContext.Response.Cookies.Delete("username");
                        HttpContext.Response.Cookies.Delete("token");
                    }
                    HttpContext.Session.SetString("fullname", fullname);
                    HttpContext.Session.SetInt32("userId", userId);
                    HttpContext.Session.SetString("role", currentUser.Role);
                    Response.Redirect("/Index");
                }
            }
            catch (Exception)
            {
                TempData["Fail"] = "Đăng nhập thất bại!.";
                Redirect("/Login");
            }
            
        }
    }
}
