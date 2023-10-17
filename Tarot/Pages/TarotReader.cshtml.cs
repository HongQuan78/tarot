using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Tarot.Data;

namespace Tarot.Pages
{
    public class TarotReader : PageModel
    {
        private readonly TarotOnlineContext _context;
        public List<Reader> readers = new List<Reader>();
        public TarotReader(TarotOnlineContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            readers = _context.Readers.Include(x => x.ReaderNavigation).ToList();
        }
    }
}