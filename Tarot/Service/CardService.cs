using Microsoft.EntityFrameworkCore;
using Tarot.Data;

namespace Tarot.Service
{
    public class CardService
    {
        private TarotOnlineContext _tarotOnlineContext;

        public CardService()
        {
            _tarotOnlineContext = new TarotOnlineContext();
        }

        public List<Deck> GetDeckList()
        {
            return _tarotOnlineContext.Decks.ToList();
        }

        public List<TarotCard> GetCardList()
        {
            return _tarotOnlineContext.TarotCards.Include(x => x.CardImages).Include(y => y.Meanings).ThenInclude(y => y.Cards).ToList();
        }
    }
}
