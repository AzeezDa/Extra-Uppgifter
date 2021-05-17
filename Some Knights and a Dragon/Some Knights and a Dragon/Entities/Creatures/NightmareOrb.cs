using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Some_Knights_and_a_Dragon.Entities.Creatures
{
    public class NightmareOrb : Creature
    {
        public NightmareOrb()
        {
            LoadSprite("nightmareOrb");
            Position = new Vector2(900, 400);
            Speed = new Vector2(250, 8000);
            MaxHealth = 200;
            CurrentHealth = MaxHealth;
            Sprite.Scale = 3;
        }
    }
}
