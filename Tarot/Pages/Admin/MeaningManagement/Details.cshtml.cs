using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Tarot.Data;
using Tarot.Service;

namespace Tarot.Pages.Admin.MeaningManagement
{
    public class DetailsModel : PageModel
    {
        private readonly Tarot.Data.TarotOnlineContext _context;
        public int? currentUserId { get; set; }
        private AccountService _accountService;
        public DetailsModel(Tarot.Data.TarotOnlineContext context, AccountService accountService)
        {
            _context = context;
            _accountService = accountService;
        }

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
            else 
            {
                Meaning = meaning;
            }
            return Page();
        }
    }
}
