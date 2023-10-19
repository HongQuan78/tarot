using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tarot.Data;
using Tarot.Service;

namespace Tarot.Pages.Admin.MeaningManagement
{
    public class EditModel : PageModel
    {
        private readonly Tarot.Data.TarotOnlineContext _context; 
        public int? currentUserId { get; set; }
        private AccountService _accountService;
        public SelectList TypeList { get; } = new SelectList(new[]
{
        new { Id = "light", Name = "Light" },
        new { Id = "shadow", Name = "Shadow" },
    }, "Id", "Name");

        public EditModel(Tarot.Data.TarotOnlineContext context, AccountService accountService)
        {
            _context = context;
            _accountService = accountService;
        }

        [BindProperty]
        public Meaning Meaning { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            currentUserId = HttpContext.Session.GetInt32("userId");
            if (currentUserId == null)
            {
                return Redirect("/Index");
            }

            if (_accountService.getRole(currentUserId) != "admin")
            {
                return Redirect("/Index");
            }
            if (id == null || _context.Meanings == null)
            {
                return NotFound();
            }

            var meaning = await _context.Meanings.FirstOrDefaultAsync(m => m.MeaningId == id);
            if (meaning == null)
            {
                return NotFound();
            }
            Meaning = meaning;
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

            _context.Attach(Meaning).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MeaningExists(Meaning.MeaningId))
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

        private bool MeaningExists(int id)
        {
            return (_context.Meanings?.Any(e => e.MeaningId == id)).GetValueOrDefault();
        }
    }
}
