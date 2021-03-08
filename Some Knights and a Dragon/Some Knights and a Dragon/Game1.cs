using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Some_Knights_and_a_Dragon.Managers;

namespace Some_Knights_and_a_Dragon
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static bool Quit { get; set; }
        
        public static WindowManager WindowManager { get; private set; } // Manages the windows/screens displayed
        public static ContentManager ContentManager { get; private set; } // Static to be accessed by everywhere without being passed into the objects.
        public static TextureManager TextureManager { get; private set; } // Static to be accessed by everywhere without being passed into the objects.
        public static InputManager InputManager { get; private set; } // Static to be accessed by everywhere without being passed into objects.
        public static FontManager FontManager { get; private set; } // Static to be accessed by everywhere without being passed into the objects.
        public static SongManager SongManager { get; private set; } // Static to be accessed by everywhere without being passed into the objects.

        public static Microsoft.Xna.Framework.GameWindow GameWindow; // Static to be used from everywhere, this is used to handle text input events
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
            FontManager = new FontManager(); // Manages text draws
            InputManager = new InputManager(); // Manages player's input (mouse and keyboard)
            GameWindow = Window;
            TextureManager = new TextureManager(GraphicsDevice); // Mananges textures such that no duplicates are made
            SongManager = new SongManager(); // Manages background music
            WindowManager = new WindowManager(); // Manages displayed windows
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            HealthBar.Setup(ref _spriteBatch); // Health bar manager for creatures

        }

        protected override void Update(GameTime gameTime)
        {
            if (Quit)
                Exit();

            // TODO: Add your update logic here
            InputManager.Update();
            WindowManager.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // TODO: Add your drawing code here
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp); // Sampler is used to make scaled sprites not blurry but keep their pixely look.
            WindowManager.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
