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

        protected override void LoadSprite(string filePath, int width, int height)
        {
            Sprite = new Sprite("Items/Consumable/" + filePath, width, height, 12);
        }

        protected override void LoadSprite(string filePath)
        {
            Sprite = new Sprite("Items/Consumable/" + filePath);
        }

    }
}
