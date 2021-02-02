using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Managers;
using Some_Knights_and_a_Dragon.Entities;
using Some_Knights_and_a_Dragon.Entities.Creatures;
using Some_Knights_and_a_Dragon.Levels;
using System;
using System.Collections.Generic;
using System.Text;
using Some_Knights_and_a_Dragon.Managers.PlayerManagement;

namespace Some_Knights_and_a_Dragon.Windows
{
    public class GameplayWindow : GameWindow
    {
        public Level CurrentLevel { get; private set; } // The Current level of the game
        public Player Player { get; private set; } // The player object

        public XMLManager<Level> LevelLoader { get; private set; } // Manages the loading of XML files
        public GameplayWindow() : base("Gameplay Window")
        {
            LevelLoader = new XMLManager<Level>();
        }

        public override void LoadContent() // Loads the content of the level
        {
            base.LoadContent();
            // CODE IS FOR DEBUG WILL BE CHANGED TO BE MORE DYNAMIC AND DEPENDANT ON THE XML LOADERS
            Player = new Player(new Elf());
            NewLevel("Dragonfall.xml");

            CurrentLevel.DroppedItems.Add(new DroppedItem(new Vector2(400, 400), new Items.Weapons.Sword()));
            CurrentLevel.DroppedItems.Add(new DroppedItem(new Vector2(500, 400), new Items.Weapons.ElvenBow()));

            for (int i = 0; i < 32; i++)
            {
                CurrentLevel.DroppedItems.Add(new DroppedItem(new Vector2(550 + i, 400), new Items.Other.Arrow()));
            }
            Loaded = true;
        }

        public override void Draw(ref SpriteBatch _spriteBatch)
        {
            base.Draw(ref _spriteBatch);
            CurrentLevel.Draw(ref _spriteBatch);
            Player.Draw(ref _spriteBatch);
        }

        public override void Update(ref GameTime gameTime)
        {
            base.Update(ref gameTime);
            CurrentLevel.Update(ref gameTime);
            Player.Update(ref gameTime);
            if (!Player.IsAlive)
                Game1.WindowManager.GameState = GameState.Dead;
        }

        // Loads a new level and calls the Garbage collector to more 
        public void NewLevel(string levelName)
        {
            CurrentLevel = null;
            GC.Collect();
            CurrentLevel = LevelLoader.Get("Levels/" + levelName);
            CurrentLevel.LoadContent();
            CurrentLevel.Creatures.Add(Player.Creature);
        }

        public void ResetLevel()
        {
            Player.Creature.SetHealthToMax();
            NewLevel(CurrentLevel.Name + ".xml");
        }
    }
}
