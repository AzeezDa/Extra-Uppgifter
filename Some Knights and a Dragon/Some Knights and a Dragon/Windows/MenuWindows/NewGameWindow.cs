using System;
using System.Collections.Generic;
using System.Text;
using Some_Knights_and_a_Dragon.Windows.Menus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Managers.PlayerManagement;

namespace Some_Knights_and_a_Dragon.Windows
{
    public class NewGameWindow : MenuWindow
    {
        // Used to display if the name chosen by the player already exists in the system
        bool nameExistsError;
        public NewGameWindow() : base("New Game Window")
        {
            // Adds the menu items
            MenuItems.Add("Name Box", new TextBox(new Vector2(640, 500), "PlayerName", backgroundText: "Your Name")); // Used to enter name of player
            MenuItems.Add("Start", new Button(new Vector2(640, 600), "Start", NewGameButton));
            MenuItems.Add("Return", new Button(new Vector2(640, 700), "Return", ReturnToPrePlay));

            // Default value
            nameExistsError = false;
        }

        public override void Draw(ref SpriteBatch _spriteBatch)
        {
            base.Draw(ref _spriteBatch);
            
            // Draws title of the screen
            Game1.FontManager.WriteTitle(_spriteBatch, "New Game", new Vector2(640, 100));

            // If the error occured, display a text on the screen
            if (nameExistsError)
                Game1.FontManager.WriteText(_spriteBatch, "Name Already Exists", new Vector2(640, 400), Color.Red);
        }

        public override void Update(ref GameTime gameTime)
        {
            base.Update(ref gameTime);
        }

        private void NewGameButton()
        {
            // Check if the name exists in the saves file

            try
            {
                nameExistsError = true;
                foreach (ClickableText text in ((PrePlayWindow)Game1.WindowManager.Windows["Pre Play"]).SaveFilePaths)
                {
                    if (text.Text == ((TextBox)MenuItems["Name Box"]).Text)
                    {
                        nameExistsError = true;
                        return;
                    }
                }

                // A fresh new set up from an xml file similar to the save data, just with initial values for the data
                SaveData saveData = new SaveData("../../../Data/fresh.start");

                // Change the name of the save data to the player name
                saveData.ChangeValue("PlayerName", ((TextBox)MenuItems["Name Box"]).Text);

                // Load the gameplay
                Game1.WindowManager.LoadGameplay();

                // Load the data into the game using through the save data object
                Game1.WindowManager.GetGameplayWindow().LoadFromSave(saveData);

                // Set the gamestate to playing
                Game1.WindowManager.GameState = Managers.GameState.Playing;

                // Save the data with the new player name
                saveData.Save();
            }
            catch (Exception e)
            {
                Game1.WindowManager.DisplayError(e);
            }
            
        }

        private void ReturnToPrePlay()
        {
            // Return to the previous window, preplay
            Game1.WindowManager.GameState = Managers.GameState.PrePlay;
        }
    }
}
