using System;
using System.Collections.Generic;

namespace Tarot.Data;

public partial class TarotCard
{
    public int CardId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Arcana { get; set; }

    public virtual ICollection<CardImage> CardImages { get; set; } = new List<CardImage>();

    public virtual ICollection<Meaning> Meanings { get; set; } = new List<Meaning>();
}
