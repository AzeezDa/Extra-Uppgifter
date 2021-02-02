using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Items.Other
{
    public class Queenleaf : Item
    {
        // ITEM: Queenleaf is used in the fight to help The Stone Queen
        public Queenleaf()
        {
            Name = "Queenleaf";
            Description = "Firm and soft. Used to mend the Queen's wounds.";
            LoadSprite("Items/Other/queenleaf");
            Sprite.Scale = 3;
        }
    }
}
