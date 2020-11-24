using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Managers;
using Some_Knights_and_a_Dragon.Entities;
using Some_Knights_and_a_Dragon.Entities.Creatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Windows
{
    class GameplayWindow : GameWindow
    {
        public GameArea CurrentGameArea { get; private set; }

        public GameplayWindow()
        {

            // THE CODE BELOW IS PREMILINARY, TO BE CHANGED FOR ALL LEVELS AND CHARACTERS. THIS ONLY EXISTS FOR DEBUG PURPOSES

            CurrentGameArea = new GameArea("volcano_bg");

            CurrentGameArea.Gravity = new Vector2(0, 9.8f);
            CurrentGameArea.Boundries = new Rectangle(0, 0, 1280, 800);

            CurrentGameArea.AddCreature(new Dragon());
            CurrentGameArea.AddCreature(new Elf());
        }
        
        public override void Draw(ref SpriteBatch _spriteBatch)
        {
            base.Draw(ref _spriteBatch);
            CurrentGameArea.Draw(ref _spriteBatch);
        }

        public override void Update(ref GameTime gameTime)
        {
            base.Update(ref gameTime);
            CurrentGameArea.Update(ref gameTime);
        }
    }
}
