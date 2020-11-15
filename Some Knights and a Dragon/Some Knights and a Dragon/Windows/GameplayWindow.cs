using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Managers;
using Some_Knights_and_a_Dragon.Creatures;
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
            CurrentGameArea = new GameArea("volcano_bg");

            CurrentGameArea.AddCreature(new Dragon());
            CurrentGameArea.AddCreature(new Knight());
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
