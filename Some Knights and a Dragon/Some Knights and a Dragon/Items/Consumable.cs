using Some_Knights_and_a_Dragon.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Items
{
    public abstract class Consumable : Item
    {
        public Consumable()
        {

        }

        protected override void LoadSprite(string filePath, int width, int height) // Overriden method to load from the right path
        {
            Sprite = new Sprite("Items/Consumables/" + filePath, width, height, 12);
        }

        protected override void LoadSprite(string filePath) // Overriden method to load from the right path
        {
            Sprite = new Sprite("Items/Consumables/" + filePath);
        }

    }
}
