using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tarot.Data;

namespace Tarot.Pages.Admin.DeckManagement
{
    public class EditModel : PageModel
    {
        private readonly Tarot.Data.TarotOnlineContext _context;

        public EditModel(Tarot.Data.TarotOnlineContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Deck Deck { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Decks == null)
            {
                return NotFound();
            }

            var deck =  await _context.Decks.FirstOrDefaultAsync(m => m.DeckId == id);
            if (deck == null)
            {
                return NotFound();
            }
            Deck = deck;
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

            _context.Attach(Deck).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeckExists(Deck.DeckId))
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

        private bool DeckExists(int id)
        {
          return (_context.Decks?.Any(e => e.DeckId == id)).GetValueOrDefault();
        }
    }
}
