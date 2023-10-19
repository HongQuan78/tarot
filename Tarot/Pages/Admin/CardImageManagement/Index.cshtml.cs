using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Tarot.Data;
using Tarot.Service;

namespace Tarot.Pages.Admin.CardImageManagement
{
    public class IndexModel : PageModel
    {
        public AccountService accountService;
        public ReadingService readingService;
        private readonly Tarot.Data.TarotOnlineContext _context;
        [BindProperty]
        public int? currentUserId { get; set; }

        public IndexModel(Tarot.Data.TarotOnlineContext context)
        {
            _context = context;
            accountService = new AccountService();
            readingService = new ReadingService();
        }

        public IList<CardImage> CardImage { get;set; } = default!;

        public async Task<ActionResult> OnGetAsync()
        {
            currentUserId = HttpContext.Session.GetInt32("userId");
            if (currentUserId == null)
            {
                return Redirect("/Index");
            }

            if (accountService.getRole(currentUserId) != "admin")
            {
                return Redirect("/Index");
            }
            if (_context.CardImages != null)
            {
                CardImage = await _context.CardImages
                .Include(c => c.Card)
                .Include(c => c.Deck).ToListAsync();
            }
            return Page();
        }
    }
}
