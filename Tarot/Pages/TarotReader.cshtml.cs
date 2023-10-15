using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Tarot.Pages
{
    public class TarotReader : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public TarotReader(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}