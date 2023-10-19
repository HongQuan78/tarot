﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tarot.Data;

namespace Tarot.Pages.Admin.UserManagement
{
    public class CreateModel : PageModel
    {
        private readonly Tarot.Data.TarotOnlineContext _context;
        public SelectList UserRoles { get; } = new SelectList(new[]
    {
        new { Id = "reader", Name = "Reader" },
        new { Id = "user", Name = "User" },
    }, "Id", "Name");

        public CreateModel(Tarot.Data.TarotOnlineContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {

            return Page();
        }

        [BindProperty]
        public User User { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Users == null || User == null)
            {
                return Page();
            }

            _context.Users.Add(User);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
