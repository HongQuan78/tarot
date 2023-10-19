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
    public class DetailsModel : PageModel
    {
        private readonly Tarot.Data.TarotOnlineContext _context;

        public DetailsModel(Tarot.Data.TarotOnlineContext context)
        {
            _context = context;
        }

      public CardImage CardImage { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.CardImages == null)
            {
                return NotFound();
            }

            var cardimage = await _context.CardImages.FirstOrDefaultAsync(m => m.ImageId == id);
            if (cardimage == null)
            {
                return NotFound();
            }
            else 
            {
                CardImage = cardimage;
            }
            return Page();
        }
    }
}
