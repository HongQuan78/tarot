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
    public class IndexModel : PageModel
    {
        private readonly Tarot.Data.TarotOnlineContext _context;

        public IndexModel(Tarot.Data.TarotOnlineContext context)
        {
            _context = context;
        }

        public IList<Deck> Deck { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Decks != null)
            {
                Deck = await _context.Decks.ToListAsync();
            }
        }
    }
}
