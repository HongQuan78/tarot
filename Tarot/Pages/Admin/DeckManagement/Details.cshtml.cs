using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Tarot.Data;

namespace Tarot.Pages.Admin.DeckManagement
{
    public class DetailsModel : PageModel
    {
        private readonly Tarot.Data.TarotOnlineContext _context;

        public DetailsModel(Tarot.Data.TarotOnlineContext context)
        {
            _context = context;
        }

      public Deck Deck { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Decks == null)
            {
                return NotFound();
            }

            var deck = await _context.Decks.FirstOrDefaultAsync(m => m.DeckId == id);
            if (deck == null)
            {
                return NotFound();
            }
            else 
            {
                Deck = deck;
            }
            return Page();
        }
    }
}
