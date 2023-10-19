using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tarot.Data;
using Tarot.Service;

namespace Tarot.Pages.Admin.MeaningManagement
{
    public class CreateModel : PageModel
    {
        private readonly Tarot.Data.TarotOnlineContext _context;
        public int? currentUserId { get; set; }
        public AccountService accountService;
        public SelectList TypeList { get; } = new SelectList(new[]
       {
        new { Id = "light", Name = "Light" },
        new { Id = "shadow", Name = "Shadow" },
    }, "Id", "Name");
        public CreateModel(Tarot.Data.TarotOnlineContext context, AccountService accountService)
        {
            _context = context;
            this.accountService = accountService;
        }

        public IActionResult OnGet()
        {
            currentUserId = HttpContext.Session.GetInt32("userId");
            if (currentUserId == null)
            {
                return Redirect("/Index");
            }

            if (accountService.getRole(currentUserId) != "admin")
            {
                return Redirect("/Index");
            }
            return Page();
        }

            [BindProperty]
        public Meaning Meaning { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Meanings == null || Meaning == null)
            {
                return Page();
            }

            _context.Meanings.Add(Meaning);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
