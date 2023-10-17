namespace Tarot.Data;

public partial class Deck
{
    public int DeckId { get; set; }

    public string? Type { get; set; }

    public virtual ICollection<ReadingHistory> ReadingHistories { get; set; } = new List<ReadingHistory>();

    public virtual ICollection<TarotCard> Cards { get; set; } = new List<TarotCard>();
}
