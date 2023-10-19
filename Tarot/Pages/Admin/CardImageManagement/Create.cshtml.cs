using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tarot.Data;

namespace Tarot.Pages.Admin.CardImageManagement
{
    public class CreateModel : PageModel
    {
        private readonly TarotOnlineContext _context;

        public CreateModel(TarotOnlineContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CardId"] = new SelectList(_context.TarotCards, "CardId", "Name");
        ViewData["DeckId"] = new SelectList(_context.Decks, "DeckId", "Type");
            return Page();
        }

        [BindProperty]
        public CardImage CardImage { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.CardImages == null || CardImage == null)
            {
                return Page();
            }

            _context.CardImages.Add(CardImage);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
