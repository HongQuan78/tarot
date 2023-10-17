using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections;
using Tarot.Data;
using Tarot.Service;

namespace Tarot.Pages
{
    public class IndexModel : PageModel
    {
        private CardService cardService;
        [BindProperty]
        public List<TarotCard> cardList { get; set; }

        [BindProperty]
        public string cardBack { get; set; }

        [BindProperty]
        public List<Deck> deckList { get; set; }

        private readonly ILogger<IndexModel> _logger;
        
        public IndexModel()
        {
            cardService = new CardService();
        }

        public void OnGet()
        {
            // Get all cards
            List<TarotCard> allCards = cardService.GetCardList();

            // Shuffle the list using OrderBy and a random number
            Random rng = new Random();
            IEnumerable<TarotCard> shuffledCards = allCards.OrderBy(card => rng.Next());

            // Take the first 20 cards from the shuffled list
            cardList = shuffledCards.Take(20).ToList();
            deckList = cardService.GetDeckList();
        }
    }
}