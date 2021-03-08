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
    class SettingsWindow : MenuWindow
    {
        public SettingsWindow() : base("Settings Window")
        {
            // Get the settings values from the file
            GetSettings();

            // Add the menu items
            MenuItems.Add("Music Volume", new Slider(new Vector2(640, 400), "Music Volume", MediaPlayer.Volume));
            MenuItems.Add("BackButton", new Button(new Vector2(640, 500), "Back", ReturnButton));
        }

        public override void Draw(ref SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(Game1.TextureManager.BlankTexture, new Rectangle(0, 0, 1280, 960), Color.Gray * 0.5f); // Draws a faint foreground on the screen
            base.Draw(ref _spriteBatch);

            // Write the window title on the screen
            Game1.FontManager.WriteTitle(_spriteBatch, "SETTINGS", new Vector2(640, 300));
        }

        public override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(ref GameTime gameTime)
        {
            base.Update(ref gameTime);

            // Update the music volume based on the volume slider
            MediaPlayer.Volume = ((Slider)MenuItems["Music Volume"]).SliderValue;
        }

        public void ReturnButton()
        {

            // Returns to the correct location based on what previously what the player was doing
            switch (Game1.WindowManager.GameState)
            {
                case Managers.GameState.SettingsMainMenu: // If was in main menu, then return to main menu
                    Game1.WindowManager.GameState = Managers.GameState.MainMenu;
                    break;
                case Managers.GameState.SettingsInGame: // If was in game, then return to game
                    Game1.WindowManager.GameState = Managers.GameState.Paused;
                    break;
                default:
                    break;
            }

            // Save the settings back to the file
            SaveSettings();
        }

        public void GetSettings()
        {
            // Create a xmldocument object 
            XmlDocument xmlDocument = new XmlDocument();

            // Load the xml file from the Settings.xml file
            xmlDocument.Load(Environment.CurrentDirectory + "/../../../Data/Settings.xml");

            // For every node in the settings, get the correct value and apply it in game
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
            // Create a xmldocument object
            XmlDocument xmlDocument = new XmlDocument();

            // Load the settings file
            xmlDocument.Load(Environment.CurrentDirectory + "/../../../Data/Settings.xml");

            // Change the values in the file
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

            // Save the file
            xmlDocument.Save(Environment.CurrentDirectory + "/../../../Data/Settings.xml");
        }
    }
}
