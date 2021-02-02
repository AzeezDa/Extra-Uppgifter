using Some_Knights_and_a_Dragon.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Items
{
    public abstract class Weapon : Item
    {
        public int Damage { get; protected set; } // The damage that the weapons deals
        public Weapon()
        {

        }

        protected override void LoadSprite(string filePath, int width, int height) // Overriden method to load from the right path
        {
            Sprite = new Sprite("Items/Weapons/" + filePath, width, height, 12);
        }

        protected override void LoadSprite(string filePath) // Overriden method to load from the right path
        {
            Sprite = new Sprite("Items/Weapons/" + filePath);
        }
    }
}
