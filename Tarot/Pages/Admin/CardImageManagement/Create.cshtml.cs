using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tarot.Data;
using Tarot.Service;

namespace Tarot.Pages.Admin.CardImageManagement
{
    public class CreateModel : PageModel
    {
        public AccountService accountService;
        public ReadingService readingService;
        [BindProperty]
        public int? currentUserId { get; set; }
        private readonly TarotOnlineContext _context;

        public CreateModel(TarotOnlineContext context)
        {
            _context = context;
            accountService = new AccountService();
            readingService = new ReadingService();
        }

        public IActionResult OnGet()
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
