using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Managers;
using Some_Knights_and_a_Dragon.Entities.Creatures;
using Some_Knights_and_a_Dragon.Levels;
using System;
using Some_Knights_and_a_Dragon.Managers.PlayerManagement;
using Some_Knights_and_a_Dragon.Entities;
using System.Collections.Generic;

namespace Some_Knights_and_a_Dragon.Windows
{
    public class GameplayWindow : MenuWindow
    {
        public Level CurrentLevel { get; private set; } // The Current level of the game
        public Player Player { get; private set; } // The player object
        public string PlayerName { get; private set; } // The name of the player
        private SaveData saveData; // Save data to load
        public HighScoreRecorder HighScoreRecorder { get; private set; } // Records the time and bosses defeated to later save in the high score list
        public XMLManager<Level> LevelLoader { get; private set; } // Manages the loading of XML files

        public Dictionary<string, Player> Players { get; private set; }

        public bool Online { get; set; }

        public GameplayWindow() : base("Gameplay Window")
        {
            // Load the level
            LevelLoader = new XMLManager<Level>();
            Players = new Dictionary<string, Player>();
            Online = false;
        }

        public override void LoadContent() // Loads the content of the level
        {
            base.LoadContent();
            // CODE IS FOR DEBUG WILL BE CHANGED TO BE MORE DYNAMIC AND DEPENDANT ON THE XML LOADERS

            // After the saved data is loaded the data from the save file is placed into their corresponding places (fields)

            // Player name
            PlayerName = saveData.DataSaveValues["PlayerName"];

            // Load the player and its creature from the file
            Player = new Player((Creature)Activator.CreateInstance(null, saveData.DataSaveValues["Character"]).Unwrap());

            // Load the level
            NewLevel(saveData.DataSaveValues["Level"]);
            CurrentLevel.AddCreature(Player.Creature);

            // Load the health data
            Player.Creature.ChangeMaxHealth(int.Parse(saveData.DataSaveValues["MaxHealth"]));
            Player.Creature.ChangeCurrentHealth(int.Parse(saveData.DataSaveValues["CurrentHealth"]));

            // Load the inventory
            Player.Inventory.LoadInventoryFromData(saveData.InventoryItems);

            // Set player position to start
            Player.Creature.ChangePosition(CurrentLevel.PlayerStartingPosition);

            // Initiate the high score recorder and start recording
            HighScoreRecorder = new HighScoreRecorder(PlayerName);
            HighScoreRecorder.Start();
        }

        // Load a save from a file path in the saves file
        public void LoadFromSave(string path)
        {
            saveData = new SaveData("../../../Saves/" + path);
            LoadContent();
        }

        // Load a save from a save data object
        public void LoadFromSave(SaveData saveData)
        {
            this.saveData = saveData;
            LoadContent();
        }

        public void LoadFromRequest(string request)
        {
            string[] requests = request.Split('|');

            Dictionary<int, Creature> creaturesToAdd = new Dictionary<int, Creature>();
            Dictionary<int, string> playersToAdd = new Dictionary<int, string>(); 

            foreach (string startRequest in requests)
            {
                string[] requestData = startRequest.Split(':');

                switch (requestData[0])
                {
                    case "L":
                        saveData = new SaveData("../../../Saves/" + requestData[1]);
                        break;

                    case "P":
                        Creature playerCreature = (Creature)Activator.CreateInstance(null, requestData[2]).Unwrap();
                        int id = int.Parse(requestData[3]);
                        creaturesToAdd.Add(id, playerCreature);
                        if (requestData[1] == ((OnlinePrePlayWindow)Game1.WindowManager.Windows["Online Pre Play"]).LocalPlayerName)
                        {
                            Player = new Player(playerCreature);
                            Player.Inventory.LoadInventoryFromData(saveData.InventoryItems);
                            NewLevel(saveData.DataSaveValues["Level"]);
                            Player.Creature.ChangePosition(CurrentLevel.PlayerStartingPosition);
                        }
                        else
                            playersToAdd.Add(id, requestData[1]);
                        break;

                    default:
                        break;
                }
            }

            foreach (KeyValuePair<int, Creature> creature in creaturesToAdd)
                CurrentLevel.AddCreature(creature.Value, creature.Key);

            foreach (KeyValuePair<int, string> player in playersToAdd)
                ConnectNewPlayer(player.Value, player.Key);

            Game1.WindowManager.GameState = GameState.Playing;
            Online = true;
        }

        // Save the game
        public void SaveGame()
        {
            // Save the game
            saveData.Save();

            // Stop the recorder and save the high score
            HighScoreRecorder.Stop();
            HighScoreRecorder.SaveHighScore();
        }

        public override void Draw(ref SpriteBatch _spriteBatch)
        {
            // Draw the level and the player
            base.Draw(ref _spriteBatch);
            CurrentLevel.Draw(ref _spriteBatch);
            Player.Draw(ref _spriteBatch);
        }

        public override void Update(ref GameTime gameTime)
        {
            // Update the player and the game. If the player is dead
            base.Update(ref gameTime);
            CurrentLevel.Update(ref gameTime);
            Player.Update(ref gameTime);
        }

        // Loads a new level and calls the Garbage collector to more 
        public void NewLevel(string levelName)
        {
            try // If there is error, catch it and send it to the ErrorWindow
            {
                CurrentLevel = null;
                GC.Collect();
                CurrentLevel = LevelLoader.Get("Levels/" + levelName);
                CurrentLevel.LoadContent();
            }
            catch (Exception e)
            {
                Game1.WindowManager.DisplayError(e, "The XML file to load this level could not be found.");
            }

        }

        // Resets the current level, this is used when the player dies
        public void ResetLevel()
        {
            Player.Creature.SetHealthToMax();
            NewLevel(CurrentLevel.Name + ".xml");
        }

        public void ConnectNewPlayer(string name, int id)
        {
            Players.Add(name, new Player(CurrentLevel.Creatures[id]));
            CurrentLevel.Creatures[id].ChangePosition(CurrentLevel.PlayerStartingPosition);
        }
    }
}
