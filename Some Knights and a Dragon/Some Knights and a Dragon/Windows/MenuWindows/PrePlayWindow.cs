using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Managers;
using Some_Knights_and_a_Dragon.Windows.Menus;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Some_Knights_and_a_Dragon.Windows
{
    public class PrePlayWindow : MenuWindow
    {
        public List<ClickableText> SaveFilePaths; // Stores the path to the save data as clickable text to load in to the different saves
        private int scrollValue; // How much the user has scrolled
        Sprite textBackground; // Background for the texts
        public PrePlayWindow() : base("Pre Play Window")
        {
            // Add the buttons
            MenuItems.Add("New Game", new Button(new Vector2(340, 900), "New Game", NewGameButton));
            MenuItems.Add("Main Menu", new Button(new Vector2(940, 900), "Main Menu", MainMenuButton));

            // Get the background sprite
            textBackground = new Sprite("Menus/textBackground");

            // Initiate the values
            SaveFilePaths = new List<ClickableText>();
            scrollValue = 0;
        }

        public override void Draw(ref SpriteBatch _spriteBatch)
        {
            base.Draw(ref _spriteBatch);

            // Write the title of the window
            Game1.FontManager.WriteTitle(_spriteBatch, "Saves", new Vector2(640, 100));

            for (int i = 0; i < 10 && scrollValue + i < SaveFilePaths.Count; i++)
            {
                // Draw row texture
                textBackground.DrawOnArea(ref _spriteBatch, new Rectangle(540, 175 + i * 60, 200, 50), 0, 0);
                SaveFilePaths[scrollValue + i].ChangePosition(new Vector2(640, 200 + i * 60));
                SaveFilePaths[scrollValue + i].Draw(_spriteBatch);
            }

        }

        public override void Update(ref GameTime gameTime)
        {
            base.Update(ref gameTime);

            // Update each clickable text
            foreach (ClickableText file in SaveFilePaths)
                file.Update();

            // Get the scroll value and add it to this class' scroll value
            scrollValue = scrollValue + Game1.InputManager.ScrollValue();

            // Checks if the scroll value is greater than the amount of items in order to display the correct amount (up to 10 per page)
            if (scrollValue + 10 >= SaveFilePaths.Count)
                scrollValue = SaveFilePaths.Count - 10;
            if (scrollValue <= 0)
                scrollValue = 0;
        }

        private void NewGameButton()
        {
            // Change gamestate to new game
            Game1.WindowManager.GameState = GameState.NewGame;
        }

        private void MainMenuButton()
        {
            // Change gamestate to main menu
            Game1.WindowManager.GameState = GameState.MainMenu;
        }

        public void GetSaves()
        {
            // Get the saves from the saves folder

            // Clear the current list
            SaveFilePaths.Clear();

            // Get all the paths in the directory/folder "Saves"
            string[] filePaths = Directory.GetFiles("../../../Saves/", "*.save").Select(Path.GetFileName).ToArray();

            // For every path, add the name of the folder without the extension to the clickable text list
            foreach (string path in filePaths)
            {
                SaveFilePaths.Add(new ClickableText(Vector2.Zero, "fileSave", path.Substring(0, path.Length - 5),
                    
                    // Anonymous function for every clickable text where it loads the save data for the corresponding save file
                    () => { Game1.WindowManager.LoadGameplay(); 
                        LoadGame(path); 
                         }));
            }
        }

        public void LoadGame(string path)
        {
            // Load the game from GameplayWindow LoadFromSave method
            Game1.WindowManager.GetGameplayWindow().LoadFromSave(path);
        }
    }
}
