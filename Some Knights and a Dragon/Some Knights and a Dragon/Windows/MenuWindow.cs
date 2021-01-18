using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Managers;
using Some_Knights_and_a_Dragon.Windows.Menus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Windows
{
    class MenuWindow : GameWindow // The Main Menu (WORK IN PROGRESS)
    {
        List<Button> buttons; // List of the buttons
        Sprite logo; // The logo of the game

        public MenuWindow()
        {
            buttons = new List<Button>();
            buttons.Add(new Button("playButton", new Vector2(640, 500)));
            buttons.Add(new Button("quitButton", new Vector2(640, 600)));
            logo = new Sprite("Menus/logo");

            buttons[0].OnClick = delegate () { Game1.WindowManager.NewWindow(new GameplayWindow()); }; // PLAY button makes the current window a GameplayWindow
            buttons[1].OnClick = delegate () { Game1.Quit = true; };
            Game1.SongManager.Play("intro");
        }
        public override void Draw(ref SpriteBatch _spriteBatch)
        {
            base.Draw(ref _spriteBatch);

            foreach (Button button in buttons)
            {
                button.Draw(ref _spriteBatch);
            }
            logo.DrawFrame(ref _spriteBatch, new Vector2(640, 250), 0, 0);
        }

        public override void Update(ref GameTime gameTime)
        {
            base.Update(ref gameTime);
            foreach (Button button in buttons)
            {
                button.Update(ref gameTime);
            }
        }
    }
}
