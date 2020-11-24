using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Some_Knights_and_a_Dragon.Entities;
using Some_Knights_and_a_Dragon.Managers;
using Some_Knights_and_a_Dragon.Windows;
using System.Runtime.CompilerServices;

namespace Some_Knights_and_a_Dragon
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static GameWindow CurrentWindow { get; private set; }
        public static ContentManager ContentManager { get; private set; }
        public static InputManager InputManager { get; set; } // Static to be accessed by everywhere without being passed into objects.
        public static FontManager FontManager { get; set; } // Static to be accessed by everywhere without being passed into the objects.

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 960;
            Content.RootDirectory = "Content";
            ContentManager = Content;
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            CurrentWindow = new GameplayWindow(); // Draws current window which is gameplay, to be changed for loading screens and menu

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            HealthBar.Setup(ref _spriteBatch); // Healthbar manager for creatures
            FontManager = new FontManager(); // Manages text draws
            InputManager = new InputManager(); // Manages player's input (mouse and keyboard)

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            InputManager.Update();
            CurrentWindow.Update(ref gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            CurrentWindow.Draw(ref _spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
