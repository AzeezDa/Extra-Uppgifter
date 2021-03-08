using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Windows.Menus;
using Some_Knights_and_a_Dragon.Managers;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Some_Knights_and_a_Dragon.Windows
{
    public class HighScoreWindow : MenuWindow
    {
        List<HighScoreItem> highScoreItems; // The highscore items
        private int scrollValue; // How much the user has scrolled
        private Sprite textBackground; // Background for each row 
        public HighScoreWindow() : base("HighScoreWindow")
        {
            // Menu items, one return to menu button and 3 sorting clickable texts (similiar to anchor)
            MenuItems.Add("Main Menu", new Button(new Vector2(640, 900), "Main Menu", MainMenuButton));
            MenuItems.Add("Bosses Defeated", new ClickableText(new Vector2(600, 150), "Bosses Defeated", "Bosses Defeated", SortBosses));
            MenuItems.Add("Time", new ClickableText(new Vector2(790, 150), "Time", "Time", SortTime));
            MenuItems.Add("Bosses/min", new ClickableText(new Vector2(990, 150), "Bosses/min", "Bosses/min", SortBossesPerTime));

            // Set the scroll value to zero, default
            scrollValue = 0;

            // Load the texture
            textBackground = new Sprite("Menus/textBackground");
        }

        public override void Draw(ref SpriteBatch _spriteBatch)
        {
            base.Draw(ref _spriteBatch);

            // Write names of columns
            Game1.FontManager.WriteText(_spriteBatch, "Rank", new Vector2(250, 150));
            Game1.FontManager.WriteText(_spriteBatch, "Name", new Vector2(410, 150));

            // Repeat for 10 rows and display the rows based on scroll value
            for (int i = 0; i < 10 && scrollValue + i < highScoreItems.Count; i++)
            {
                // Draw row texture
                textBackground.DrawOnArea(ref _spriteBatch, new Rectangle(200, 175 + i * 60, 900, 50), 0, 0); 

                // Draw the columns of each row
                Game1.FontManager.WriteText(_spriteBatch, $"{scrollValue + 1 + i}", new Vector2(250, 200 + i * 60));
                Game1.FontManager.WriteText(_spriteBatch, highScoreItems[scrollValue + i].Name, new Vector2(410, 200 + i * 60));
                Game1.FontManager.WriteText(_spriteBatch, highScoreItems[scrollValue + i].BossesDefeated.ToString(), new Vector2(600, 200 + i * 60));
                Game1.FontManager.WriteText(_spriteBatch, highScoreItems[scrollValue + i].Time.ToString(@"hh\:mm\:ss\.fff"), new Vector2(790, 200 + i * 60));
                Game1.FontManager.WriteText(_spriteBatch,
                    $"{Math.Round(highScoreItems[scrollValue + i].BossesDefeated / highScoreItems[scrollValue + i].Time.TotalMinutes, 5)}", 
                    new Vector2(990, 200 + i * 60));
            }
        }

        public override void Update(ref GameTime gameTime)
        {
            base.Update(ref gameTime);

            // Get the scroll value and add it to this class' scroll value
            scrollValue += Game1.InputManager.ScrollValue();

            // Checks if the scroll value is greater than the amount of items in order to display the correct amount (up to 10 per page)
            if (scrollValue + 10 >= highScoreItems.Count)
                scrollValue = highScoreItems.Count - 10;
            if (scrollValue <= 0)
                scrollValue = 0;

        }

        public override void LoadContent()
        {
            // Load the high score and checks for errors
            try
            {
                highScoreItems = HighScoreManager.LoadHighScores();
            }
            catch (FileNotFoundException e) // File not found
            {
                Game1.WindowManager.DisplayError(e, "The High score file could not be found.");
            }
            catch (System.Xml.XmlException e) // Error in the XML parsing
            {
                Game1.WindowManager.DisplayError(e, "There was an error in the high score file.");
            }
            catch (Exception e) // Other exceptions
            {
                Game1.WindowManager.DisplayError(e);
            }
        }

        // Return to main menu
        private void MainMenuButton()
        {
            Game1.WindowManager.GameState = GameState.MainMenu;
        }

        // Sort by time
        private void SortTime()
        {
            highScoreItems.Sort((x, y) => x.Time.CompareTo(y.Time));
        }

        // Sort by amount of bosses defeated
        private void SortBosses()
        {
            highScoreItems.Sort((y, x) => x.BossesDefeated.CompareTo(y.BossesDefeated));
        }

        // Sort by bosses/time
        private void SortBossesPerTime()
        {
            highScoreItems.Sort((y, x) => (x.BossesDefeated / x.Time.TotalSeconds).CompareTo(y.BossesDefeated / y.Time.TotalSeconds));
        }
    }
}
