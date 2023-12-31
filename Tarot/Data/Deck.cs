﻿using System;
using System.Collections.Generic;

namespace Tarot.Data;

public partial class Deck
{
    public int DeckId { get; set; }

    public string? Type { get; set; }

    public string? ImageUrl { get; set; }

    public virtual ICollection<CardImage> CardImages { get; set; } = new List<CardImage>();
}
