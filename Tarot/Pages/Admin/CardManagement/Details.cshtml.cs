using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Tarot.Data;

namespace Tarot.Pages.Admin.CardManagement
{
    public class DetailsModel : PageModel
    {
        private readonly Tarot.Data.TarotOnlineContext _context;

        public DetailsModel(Tarot.Data.TarotOnlineContext context)
        {
            _context = context;
        }

      public TarotCard TarotCard { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.TarotCards == null)
            {
                return NotFound();
            }

            var tarotcard = await _context.TarotCards.FirstOrDefaultAsync(m => m.CardId == id);
            if (tarotcard == null)
            {
                return NotFound();
            }
            else 
            {
                TarotCard = tarotcard;
            }
            return Page();
        }
    }
}
