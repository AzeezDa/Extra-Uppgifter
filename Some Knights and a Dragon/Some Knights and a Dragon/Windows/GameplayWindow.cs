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

            

            CurrentGameArea = new GameArea("volcano");
            CurrentGameArea.Background.ChangeSpeed(1, 15);
            CurrentGameArea.Background.ChangeSpeed(0, 5);

            CurrentGameArea.Gravity = new Vector2(0, 5f);
            CurrentGameArea.Boundries = new Rectangle(0, 0, 1280, 850);

            CurrentGameArea.Boss = new DragonBoss();
            CurrentGameArea.AddCreature(CurrentGameArea.Boss.Creature);

            Player = new Player(new Elf());
            CurrentGameArea.AddCreature(Player.Creature);
            CurrentGameArea.DroppedItems.Add(new DroppedItem(new Vector2(400, 400), new Items.Weapons.ElvenBow()));
            CurrentGameArea.DroppedItems.Add(new DroppedItem(new Vector2(500, 400), new Items.Weapons.Sword()));

            for (int i = 0; i < 32; i++)
            {
                CurrentGameArea.DroppedItems.Add(new DroppedItem(new Vector2(550 + i, 400), new Items.Other.Arrow()));
            }
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
