using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Items.Other
{
    public class Coin : Item
    {
        // ITEM: Coins are used to trade with the mysterious man
        public Coin()
        {
            Name = "Coin";
            Description = "Shiny! Used for trading";
            LoadSprite("Items/Other/coin");
            Sprite.Scale = 2;
        }
    }
}
