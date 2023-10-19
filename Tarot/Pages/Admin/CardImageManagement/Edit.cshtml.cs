using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tarot.Data;
using Tarot.Service;

namespace Tarot.Pages.Admin.CardImageManagement
{
    public class EditModel : PageModel
    {
        public AccountService accountService;
        public ReadingService readingService;
        [BindProperty]
        public int? currentUserId { get; set; }
        private readonly Tarot.Data.TarotOnlineContext _context;

        public EditModel(Tarot.Data.TarotOnlineContext context)
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
                return Redirect("/Index");
            }

            if (accountService.getRole(currentUserId) != "admin")
            {
                return Redirect("/Index");
            }

            if (id == null || _context.CardImages == null)
            {
                return NotFound();
            }

            var cardimage =  await _context.CardImages.FirstOrDefaultAsync(m => m.ImageId == id);
            if (cardimage == null)
            {
                return NotFound();
            }
            CardImage = cardimage;
           ViewData["CardId"] = new SelectList(_context.TarotCards, "CardId", "Name");
           ViewData["DeckId"] = new SelectList(_context.Decks, "DeckId", "Type");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(CardImage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardImageExists(CardImage.ImageId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CardImageExists(int id)
        {
          return (_context.CardImages?.Any(e => e.ImageId == id)).GetValueOrDefault();
        }
    }
}
