﻿using Core.Entities.Common;

namespace Entity.Entities;

public class CardType : Entity<Guid>
{
    public string Name { get; set; } // Mifare, Mifare 4k

    public virtual ICollection<Card> Cards { get; set; }

    public CardType()
    {
        Cards = [];
    }
}
