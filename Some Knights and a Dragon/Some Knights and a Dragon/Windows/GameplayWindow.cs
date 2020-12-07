using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Managers;
using Some_Knights_and_a_Dragon.Entities;
using Some_Knights_and_a_Dragon.Entities.Creatures;
using System;
using System.Collections.Generic;
using System.Text;
using Some_Knights_and_a_Dragon.Managers.PlayerManagement;

namespace Some_Knights_and_a_Dragon.Windows
{
    class GameplayWindow : GameWindow
    {
        public GameArea CurrentGameArea { get; private set; }
        public Player Player { get; private set; }
        public GameplayWindow()
        {

            // THE CODE BELOW IS PREMILINARY, TO BE CHANGED FOR ALL LEVELS AND CHARACTERS. THIS ONLY EXISTS FOR DEBUG PURPOSES

            Player = new Player(new Elf());

            CurrentGameArea = new GameArea("Backgrounds/volcano_bg");

            CurrentGameArea.Gravity = new Vector2(0, 5f);
            CurrentGameArea.Boundries = new Rectangle(0, 0, 1280, 800);

            CurrentGameArea.AddCreature(Player.Creature);

            CurrentGameArea.DroppedItems.Add(new Entities.Other.DroppedItem(new Vector2(900, 400), new Items.Weapons.ElvenBow()));
            CurrentGameArea.DroppedItems.Add(new Entities.Other.DroppedItem(new Vector2(600, 400), new Items.Other.Arrow()));
            CurrentGameArea.DroppedItems.Add(new Entities.Other.DroppedItem(new Vector2(700, 400), new Items.Other.Arrow()));
            CurrentGameArea.DroppedItems.Add(new Entities.Other.DroppedItem(new Vector2(800, 400), new Items.Other.Arrow()));
            CurrentGameArea.DroppedItems.Add(new Entities.Other.DroppedItem(new Vector2(500, 400), new Items.Weapons.Sword()));
        }
        
        public override void Draw(ref SpriteBatch _spriteBatch)
        {
            base.Draw(ref _spriteBatch);
            CurrentGameArea.Draw(ref _spriteBatch);
            Player.Draw(ref _spriteBatch);
        }

        public override void Update(ref GameTime gameTime)
        {
            base.Update(ref gameTime);
            CurrentGameArea.Update(ref gameTime);
            Player.Update(ref gameTime);
        }
    }
}
