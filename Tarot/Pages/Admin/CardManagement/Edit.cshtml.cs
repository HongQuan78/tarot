using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tarot.Data;

namespace Tarot.Pages.Admin.CardManagement
{
    public class EditModel : PageModel
    {
        private readonly Tarot.Data.TarotOnlineContext _context;

        public EditModel(Tarot.Data.TarotOnlineContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TarotCard TarotCard { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.TarotCards == null)
            {
                return NotFound();
            }

            var tarotcard =  await _context.TarotCards.FirstOrDefaultAsync(m => m.CardId == id);
            if (tarotcard == null)
            {
                return NotFound();
            }
            TarotCard = tarotcard;
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

            _context.Attach(TarotCard).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TarotCardExists(TarotCard.CardId))
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

        private bool TarotCardExists(int id)
        {
          return (_context.TarotCards?.Any(e => e.CardId == id)).GetValueOrDefault();
        }
    }
}
