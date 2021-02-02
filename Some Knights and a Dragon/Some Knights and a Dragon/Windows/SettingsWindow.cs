using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Some_Knights_and_a_Dragon.Windows.Menus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Some_Knights_and_a_Dragon.Windows
{
    class SettingsWindow : GameWindow
    {
        List<MenuItem> menuItems;
        public SettingsWindow() : base("Settings Window")
        {
            GetSettings();
            menuItems = new List<MenuItem>();
            menuItems.Add(new Slider(new Vector2(640, 400), "Music Volume", MediaPlayer.Volume));
            menuItems.Add(new Button(new Vector2(640, 500), "Back", ReturnButton));
        }

        public override void Draw(ref SpriteBatch _spriteBatch)
        {
            base.Draw(ref _spriteBatch);
            _spriteBatch.Draw(Game1.TextureManager.BlankTexture, new Rectangle(0, 0, 1280, 960), Color.Gray * 0.5f); // Draws a faint foreground on the screen
            Game1.FontManager.WriteTitle(_spriteBatch, "SETTINGS", new Vector2(640, 300));

            foreach (MenuItem menuItem in menuItems)
            {
                menuItem.Draw(_spriteBatch);
            }
        }

        public override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(ref GameTime gameTime)
        {
            base.Update(ref gameTime);
            foreach (MenuItem menuItem in menuItems)
            {
                menuItem.Update();
                if (menuItem.Name == "Music Volume") // If menu item is the volume slider name then update the volume
                    MediaPlayer.Volume = ((Slider)menuItem).SliderValue;
            }
        }

        public void ReturnButton()
        {
            switch (Game1.WindowManager.GameState)
            {
                case Managers.GameState.SettingsMainMenu:
                    Game1.WindowManager.GameState = Managers.GameState.MainMenu;
                    break;
                case Managers.GameState.SettingsInGame:
                    Game1.WindowManager.GameState = Managers.GameState.Paused;
                    break;
                default:
                    break;
            }

            SaveSettings();
        }

        public void GetSettings()
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(Environment.CurrentDirectory + "/../../../Data/Settings.xml");
            foreach (XmlNode node in xmlDocument.SelectSingleNode("Settings").ChildNodes)
            {
                switch (node.Name)
                {
                    case "MusicVolume":
                        MediaPlayer.Volume = float.Parse(node.InnerText) / 100;
                        break;
                    default:
                        break;
                }
            }
        }

        public void SaveSettings()
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(Environment.CurrentDirectory + "/../../../Data/Settings.xml");

            foreach (XmlNode node in xmlDocument.SelectSingleNode("Settings").ChildNodes)
            {
                switch (node.Name)
                {
                    case "MusicVolume":
                        node.InnerText = (MediaPlayer.Volume * 100).ToString();
                        break;
                    default:
                        break;
                }
            }

            xmlDocument.Save(Environment.CurrentDirectory + "/../../../Data/Settings.xml");
        }
    }
}
