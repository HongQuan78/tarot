using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Tarot.Data;

namespace Tarot.Pages.Admin.CardImageManagement
{
    public class IndexModel : PageModel
    {
        private readonly Tarot.Data.TarotOnlineContext _context;

        public IndexModel(Tarot.Data.TarotOnlineContext context)
        {
            _context = context;
        }

        public IList<CardImage> CardImage { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.CardImages != null)
            {
                CardImage = await _context.CardImages
                .Include(c => c.Card)
                .Include(c => c.Deck).ToListAsync();
            }
        }
    }
}
