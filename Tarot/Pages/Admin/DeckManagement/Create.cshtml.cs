using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tarot.Data;
using Tarot.Service;

namespace Tarot.Pages.Admin.DeckManagement
{
    public class CreateModel : PageModel
    {
        public AccountService accountService;
        public ReadingService readingService;
        [BindProperty]
        public int? currentUserId { get; set; }
        private readonly Tarot.Data.TarotOnlineContext _context;

        public CreateModel(Tarot.Data.TarotOnlineContext context)
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
            return Page();
        }

        [BindProperty]
        public Deck Deck { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Decks == null || Deck == null)
            {
                return Page();
            }

            _context.Decks.Add(Deck);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
