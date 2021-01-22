using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Items.Consumables;
using Some_Knights_and_a_Dragon.Items.Other;
using Some_Knights_and_a_Dragon.Items.Weapons;
using Some_Knights_and_a_Dragon.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Entities.Creatures
{
    public class StoneQueenBoss : Boss
    {
        List<Snake> snakes;
        List<Bird> birds;
        StoneGuardian guardian;
        Timer snakeTimer;
        Timer birdTimer;
        Timer snakeAttackTimer;
        Timer guardianRespawn;
        Timer birdAttackTimer;
        int guardianLevel;

        int brokenStatus;
        public StoneQueenBoss()
        {
            // Creatures
            Creature = new StoneQueen();
            snakes = new List<Snake>();
            birds = new List<Bird>();
            guardian = new StoneGuardian();
            Game1.WindowManager.GetGameplayWindow().CurrentLevel.AddCreature(guardian);

            // Timers
            snakeTimer = new Timer(5000);
            birdTimer = new Timer(10000);
            guardianRespawn = new Timer(15000);
            snakeAttackTimer = new Timer(500, 0);
            birdAttackTimer = new Timer(400, 0);

            // Ints to store levels and progress of fight
            brokenStatus = 0;
            guardianLevel = 1;

            // Loot
            AddLoot(new Coin(), 50);
            AddLoot(new QueensBlade(), 1);
            AddLoot(new DiskOfFortune(), 1);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void OnDeath()
        {
            AddLoot(new Coin(), Game1.WindowManager.GetGameplayWindow().Player.Inventory.AmountOf("Queenleaf") * 5);
            Game1.WindowManager.GetGameplayWindow().CurrentLevel.RemoveAllOfDroppedItem("Queenleaf");
            Game1.WindowManager.GetGameplayWindow().Player.Inventory.RemoveAll("Queenleaf");
            base.OnDeath();
            guardian.Kill();
            foreach (Snake snake in snakes)
            {
                snake.Kill();
            }
            foreach (Bird bird in birds)
            {
                bird.Kill();
            }
            Game1.WindowManager.GetGameplayWindow().Player.Creature.SetHealthToMax();
            Creature.Sprite.OneTimeAnimation(1, 11);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            brokenStatus = 9 - (int)(Creature.GetHealthRatio * 9);
            Creature.Sprite.Freeze(0, brokenStatus);

            GuardianPhase(gameTime);
            Phase1(gameTime);
            if (brokenStatus == 8)
            {
                IsAlive = false;
                OnDeath();
            }
        }

        private void GuardianPhase(GameTime gameTime)
        {
            if (Game1.WindowManager.GetGameplayWindow().Player.Creature.Position.X > 850 && guardian.CurrentHealth > 0)
            {
                Game1.WindowManager.GetGameplayWindow().Player.Creature.ChangePosition(new Vector2(850, Game1.WindowManager.GetGameplayWindow().Player.Creature.Position.Y));
            }
            if (guardian.CurrentHealth <= 0)
            {
                birdAttackTimer.CheckTimer(gameTime);
                if (birdAttackTimer.TimerOn)
                {
                    Creature.TakeDamage(birds.Count * 50);
                    if (Vector2.Distance(Game1.WindowManager.GetGameplayWindow().Player.Creature.Position, Creature.Position) < 50 &&
                        Game1.WindowManager.GetGameplayWindow().Player.Inventory.ItemInInventory("Queenleaf"))
                    {
                        Game1.WindowManager.GetGameplayWindow().Player.Inventory.RemoveItem("Queenleaf");
                        Game1.WindowManager.GetGameplayWindow().Player.Creature.AddHealth(10);
                        Creature.TakeDamage(50);
                    }
                }
                guardianRespawn.CheckTimer(gameTime);
                if (guardianRespawn.TimerOn)
                {
                    guardian.ChangeMaxHealth(guardian.MaxHealth * 2);
                    guardian.SetHealthToMax();
                    guardian.Sprite.Scale++;
                    Game1.WindowManager.GetGameplayWindow().CurrentLevel.AddCreature(guardian);
                    if (Game1.WindowManager.GetGameplayWindow().Player.Creature.Position.X > 900)
                    {
                        Game1.WindowManager.GetGameplayWindow().Player.Creature.TakeDamage(50);
                    }
                }
            }
        }

        private void Phase1(GameTime gameTime)
        {
            snakeTimer.CheckTimer(gameTime);
            birdTimer.CheckTimer(gameTime);
            snakeAttackTimer.CheckTimer(gameTime);

            if (snakeTimer.TimerOn)
                AddSnake();
            if (birdTimer.TimerOn)
                AddBird();

            for (int i = snakes.Count - 1; i >= 0; --i)
            {
                if (snakes[i].CurrentHealth < 0)
                {
                    snakes.RemoveAt(i);
                    continue;
                }
                if (snakes[i].MoveTo(gameTime, Game1.WindowManager.GetGameplayWindow().Player.Creature.Position, new Vector2(5000, 0), 100))
                {
                    if (snakeAttackTimer.TimerOn)
                    {
                        Game1.WindowManager.GetGameplayWindow().Player.Creature.TakeDamage(guardianLevel * 3);
                        snakes[i].Attack();
                    }
                }
            }

            foreach (Bird bird in birds)
            {
                if (guardian.CurrentHealth > 0)
                {
                    bird.MoveTo(gameTime, new Vector2(Game1.WindowManager.GetGameplayWindow().Player.Creature.Position.X, 0), new Vector2(10000, 10000), 100);
                    if (snakeTimer.TimerOn)
                        Game1.WindowManager.GetGameplayWindow().CurrentLevel.AddDroppedItem(bird.Position, new Queenleaf());
                }
                else
                {
                    bird.MoveTo(gameTime, new Vector2(Creature.Position.X, Creature.Position.Y - 300), new Vector2(10000, 10000), 10);
                }
            }
        }

        private void AddSnake()
        {
            Snake snake = new Snake();
            snakes.Add(snake);
            Game1.WindowManager.GetGameplayWindow().CurrentLevel.AddCreature(snake);
        }
        private void AddBird()
        {
            Bird bird = new Bird();
            birds.Add(bird);
            Game1.WindowManager.GetGameplayWindow().CurrentLevel.AddCreature(bird);
        }
    }
}
