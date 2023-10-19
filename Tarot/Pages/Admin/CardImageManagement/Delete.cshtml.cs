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
    public class DeleteModel : PageModel
    {
        public AccountService accountService;
        public ReadingService readingService;
        [BindProperty]
        public int? currentUserId { get; set; }
        private readonly Tarot.Data.TarotOnlineContext _context;

        public DeleteModel(Tarot.Data.TarotOnlineContext context)
        {
            _context = context;
            accountService = new AccountService();
            readingService = new ReadingService();
        }

        [BindProperty]
      public CardImage CardImage { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
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
            if (id == null || _context.CardImages == null)
            {
                return NotFound();
            }

            var cardimage = await _context.CardImages.FirstOrDefaultAsync(m => m.ImageId == id);

            if (cardimage == null)
            {
                return NotFound();
            }
            else 
            {
                CardImage = cardimage;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.CardImages == null)
            {
                return NotFound();
            }
            var cardimage = await _context.CardImages.FindAsync(id);

            if (cardimage != null)
            {
                CardImage = cardimage;
                _context.CardImages.Remove(CardImage);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
