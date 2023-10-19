using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Tarot.Data;
using Tarot.Service;

namespace Tarot.Pages.Admin.UserManagement
{
    public class IndexModel : PageModel
    {
        public AccountService accountService;
        public ReadingService readingService;
        [BindProperty]
        public int? currentUserId { get; set; }
        private readonly Tarot.Data.TarotOnlineContext _context;

        public IndexModel(Tarot.Data.TarotOnlineContext context)
        {
            _context = context;
            accountService = new AccountService();
            readingService = new ReadingService();
        }

        public IList<User> User { get;set; } = default!;

        public async Task OnGetAsync()
        {
            currentUserId = HttpContext.Session.GetInt32("userId");
            if (currentUserId == null)
            {
                Response.Redirect("/Index");
            }

            if (accountService.getRole(currentUserId) != "admin")
            {
                Response.Redirect("/Index");
            }
            if (_context.Users != null)
            {
                User = await _context.Users.ToListAsync();
            }
        }
    }
}
