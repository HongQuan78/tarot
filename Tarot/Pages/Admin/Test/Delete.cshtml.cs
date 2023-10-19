﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Tarot.Data;

namespace Tarot.Pages.Admin.Test
{
    public class DeleteModel : PageModel
    {
        private readonly Tarot.Data.TarotOnlineContext _context;

        public DeleteModel(Tarot.Data.TarotOnlineContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Meaning Meaning { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Meanings == null)
            {
                return NotFound();
            }

            var meaning = await _context.Meanings.FirstOrDefaultAsync(m => m.MeaningId == id);

            if (meaning == null)
            {
                return NotFound();
            }
            else 
            {
                Meaning = meaning;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Meanings == null)
            {
                return NotFound();
            }
            var meaning = await _context.Meanings.FindAsync(id);

            if (meaning != null)
            {
                Meaning = meaning;
                _context.Meanings.Remove(Meaning);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
