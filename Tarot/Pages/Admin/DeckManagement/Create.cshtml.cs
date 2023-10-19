using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tarot.Data;

namespace Tarot.Pages.Admin.DeckManagement
{
    public class CreateModel : PageModel
    {
        private readonly Tarot.Data.TarotOnlineContext _context;

        public CreateModel(Tarot.Data.TarotOnlineContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
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
