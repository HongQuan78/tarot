using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tarot.Data;

namespace Tarot.Pages
{
    public class TarotReader : PageModel
    {
        private readonly TarotOnlineContext _context;
        [BindProperty]
        public List<Reader> readers { get; set; } = null;
        public TarotReader(TarotOnlineContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            readers = _context.Readers.ToList();
        }
    }
}