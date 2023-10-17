using System;
using System.Collections.Generic;

namespace Tarot.Data;

public partial class CardImage
{
    public int ImageId { get; set; }

    public int? CardId { get; set; }

    public int? DeckId { get; set; }

    public string? ImageUrl { get; set; }

    public virtual TarotCard? Card { get; set; }

    public virtual Deck? Deck { get; set; }
}
