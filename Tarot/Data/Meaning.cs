using System;
using System.Collections.Generic;

namespace Tarot.Data;

public partial class Meaning
{
    public int MeaningId { get; set; }

    public string? Meaning1 { get; set; }

    public string? Type { get; set; }

    public virtual ICollection<TarotCard> Cards { get; set; } = new List<TarotCard>();
}
