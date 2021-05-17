using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Some_Knights_and_a_Dragon.Entities;
using Some_Knights_and_a_Dragon.Entities.Projectiles;
using Some_Knights_and_a_Dragon.Entities.Other;
using Some_Knights_and_a_Dragon.Managers;
using Microsoft.Xna.Framework.Graphics;

namespace Some_Knights_and_a_Dragon.Entities.Creatures
{
    /* Dragon Lord Yume is boss fight with three phases; The Dream, The Maw, The Nightmare where there is a nightmare counter that increases periodically and makes the fight more difficult
     The Dream: Yume will shoot fireballs at the player and sometimes strike them with lightning.
                Yume will occasionally pull the players closer and try to consume them. When consumed they enter The Maw

     The Maw: In The Maw the players fight the plagues and flames inside the Dragon Lord. When they get close they explode dealing area damage.
              There will be one nightmare monster in the maw. When defeated, the boss will be in the final phase, The Nightmare.

     The Nightmare: The player becomes The Dragon Lord and attacks its nightmare counterpart
     
     */

    public class DragonLordBoss : Boss
    {

        private enum Phases { Dream, Maw, Nightmare};
        Phases phases = Phases.Dream;
        Random r = new Random();


        // List of used creatures
        List<BellyPlague> bellyPlagues = new List<BellyPlague>();
        List<BellyFlame> bellyFlames = new List<BellyFlame>();
        List<NightmareOrb> nightmareOrbs = new List<NightmareOrb>();

        // Projectiles and lightning
        List<Bloodball> bloodballs = new List<Bloodball>();
        List<Lightning> lightnings = new List<Lightning>();

        // Timers
        Timer fireballTimer = new Timer(2000);
        Timer bloodballTimer = new Timer(2000);
        Timer lightningTimer = new Timer(3000);
        Timer consumeTimer = new Timer(5000);
        Timer consumingTimer = new Timer(3000);
        Timer transitionTimer = new Timer(1500);
        Timer plagueTimer = new Timer(5000);
        Timer flameTimer = new Timer(3000);
        Timer nightmareOrbTimer = new Timer(10000);
        Timer mawNightmareTimer = new Timer(10000);
        Timer dreamAwakeningTimer = new Timer(8000);

        // Backgrounds
        Background dreamBackground = new Background();
        Background mawBackground = new Background();
        Background nightmareBackground = new Background();

        // Values
        float consumePower = 1;
        bool consuming = false;
        bool transitioning = false;
        int nightmareLevel = 1;
        int mawDamage = 0;

        // Player changing values to store player data when changing phases
        Creature PlayerCreature = Game1.WindowManager.GetGameplayWindow().Player.Creature;
        Managers.PlayerManagement.InventoryItem[] PlayerInventory = new Managers.PlayerManagement.InventoryItem[5];
        Creature DragonLordDream = new DragonLord();

        public DragonLordBoss() : base("Dragon Lord Yume")
        {
            Creature = new DragonLord();
            BackgroundSetup();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            switch (phases)
            {
                case Phases.Dream:
                    if (transitioning)
                        transitionTimer.CheckTimer(gameTime);
                    else
                    {
                        TheDream(gameTime);
                    }
                    if (transitionTimer.TimerOn)
                    {
                        Game1.WindowManager.GetGameplayWindow().CurrentLevel.Projectiles.Clear();
                        Game1.WindowManager.GetGameplayWindow().CurrentLevel.Background = dreamBackground;
                        Game1.WindowManager.GetGameplayWindow().CurrentLevel.Boundries = new Rectangle(0, 0, 1280, 900);
                        ((DragonLord)Creature).NightmareMode = false;
                        transitionTimer.Reset();
                        foreach (Bloodball bloodball in bloodballs)
                            Game1.WindowManager.GetGameplayWindow().CurrentLevel.Projectiles.Remove(bloodball);
                        transitioning = false;
                        Game1.WindowManager.GetGameplayWindow().CurrentLevel.Creatures.Remove(DragonLordDream);
                        Game1.WindowManager.GetGameplayWindow().Player.ChangeCreature(PlayerCreature);
                        Game1.WindowManager.GetGameplayWindow().CurrentLevel.Creatures.Add(PlayerCreature);
                        Game1.WindowManager.GetGameplayWindow().Player.Inventory.LoadInventoryFromData(PlayerInventory);
                        Creature.Sprite.Scale = 7;

                        fireballTimer.NewTime(2000 - 200 * nightmareLevel);

                        nightmareLevel++;
                        transitionTimer.NewTime(800);
                    }
                    break;
                case Phases.Maw:
                    if (transitioning)
                        transitionTimer.CheckTimer(gameTime);
                    else
                    {
                        TheMaw(gameTime);
                    }
                    if (transitionTimer.TimerOn)
                    {
                        transitioning = false;
                        Game1.WindowManager.GetGameplayWindow().CurrentLevel.Creatures.Remove(Creature);
                        Game1.WindowManager.GetGameplayWindow().CurrentLevel.Background = mawBackground;
                        Game1.WindowManager.GetGameplayWindow().CurrentLevel.Boundries = new Rectangle(0, 0, 1280, 800);
                        transitionTimer.Reset();

                        nightmareOrbTimer.NewTime(10000 + 1500 * nightmareLevel);
                        flameTimer.NewTime(2000 - 200 * nightmareLevel);
                        plagueTimer.NewTime(5000 - 400 * nightmareLevel);
                        nightmareLevel++;
                        transitionTimer.NewTime(500);
                    }
                    break;
                case Phases.Nightmare:
                    if (transitioning)
                        transitionTimer.CheckTimer(gameTime);
                    else
                    {
                        TheNightmare(gameTime);
                    }
                    if (transitionTimer.TimerOn)
                    {
                        transitioning = false;
                        Game1.WindowManager.GetGameplayWindow().CurrentLevel.Creatures.Add(Creature);
                        Game1.WindowManager.GetGameplayWindow().CurrentLevel.Background = nightmareBackground;
                        Game1.WindowManager.GetGameplayWindow().CurrentLevel.Boundries = new Rectangle(0, 0, 1280, 800);
                        ((DragonLord)Creature).NightmareMode = true;
                        transitionTimer.Reset();
                        DragonLordDream.SetHealthToMax();
                        DragonLordDream.ChangePosition(new Vector2(200, 900));


                        Game1.WindowManager.GetGameplayWindow().Player.Inventory.Inventory.CopyTo(PlayerInventory, 0);
                        Game1.WindowManager.GetGameplayWindow().CurrentLevel.Creatures.Remove(PlayerCreature);
                        Game1.WindowManager.GetGameplayWindow().Player.ChangeCreature(DragonLordDream);
                        Game1.WindowManager.GetGameplayWindow().CurrentLevel.Creatures.Add(DragonLordDream);
                        Game1.WindowManager.GetGameplayWindow().Player.Inventory.EmptyInventory();
                        Creature.Sprite.Scale = 10;

                        bloodballTimer.NewTime(2000 - 100 * nightmareLevel);

                        nightmareLevel++;
                        transitionTimer.NewTime(200);
                        consumeTimer.Reset();
                        consumingTimer.Reset();
                        PlayerCreature.ChangePosition(new Vector2(300, 900));
                    }
                    break;
                default:
                    break;
            }

            for (int i = lightnings.Count - 1; i >= 0; --i)
            {
                lightnings[i].Update(gameTime);
                if (lightnings[i].Done)
                {
                    lightnings[i] = null;
                    lightnings.RemoveAt(i);
                }
            }

            Creature.ChangePosition(new Vector2(Creature.Position.X, 700 + 50 * (float)Math.Sin(0.5 * gameTime.TotalGameTime.TotalSeconds)));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            foreach (Lightning lightning in lightnings)
            {
                lightning.Draw(spriteBatch);
            }
        }

        private void TheDream(GameTime gameTime)
        {
            fireballTimer.CheckTimer(gameTime);
            lightningTimer.CheckTimer(gameTime);
            dreamAwakeningTimer.CheckTimer(gameTime);

            if (!consuming)
                consumeTimer.CheckTimer(gameTime);

            if (consumeTimer.TimerOn)
                consuming = true;

            if (dreamAwakeningTimer.TimerOn)
                nightmareLevel -= nightmareLevel > 0 ? 1 : 0;

            if (consuming)
            {
                Game1.WindowManager.GetGameplayWindow().Player.Creature.AddToPosition(new Vector2(100 * (float)gameTime.ElapsedGameTime.TotalSeconds * consumePower, 0));
                if (Vector2.Distance(Creature.Position, Game1.WindowManager.GetGameplayWindow().Player.Creature.Position) < 100)
                {
                    phases = Phases.Maw;
                    Creature.Sprite.OneTimeAnimation(2, 10);
                    transitioning = true;
                }
                consumingTimer.CheckTimer(gameTime);
                if (consumingTimer.TimerOn)
                    consuming = false;
            }

            if (fireballTimer.TimerOn)
            {
                foreach (Creature creature in Game1.WindowManager.GetGameplayWindow().CurrentLevel.Creatures)
                {
                    if (creature == Creature)
                        continue;

                    // Shoots the the all other creatures with a fireball
                    Game1.WindowManager.GetGameplayWindow().CurrentLevel.AddProjectile(new Fireball(Creature, Creature.Position,
                    Vector2.Normalize(creature.Position - Creature.Position), 10));
                    Creature.Attack();
                }
            }

            if (lightningTimer.TimerOn)
                lightnings.Add(new Lightning(Game1.WindowManager.GetGameplayWindow().Player.Creature.Position.X));
        }

        private void TheMaw(GameTime gameTime)
        {
            plagueTimer.CheckTimer(gameTime);
            flameTimer.CheckTimer(gameTime);
            nightmareOrbTimer.CheckTimer(gameTime);
            mawNightmareTimer.CheckTimer(gameTime);

            if (mawNightmareTimer.TimerOn)
                nightmareLevel++;

            if (plagueTimer.TimerOn)
            {
                BellyPlague bellyPlague = new BellyPlague();
                bellyPlague.ChangePosition(new Vector2(1300, r.Next(0, 800)));
                bellyPlagues.Add(bellyPlague);
                Game1.WindowManager.GetGameplayWindow().CurrentLevel.AddCreature(bellyPlague);
            }

            if (flameTimer.TimerOn)
            {
                BellyFlame bellyFlame = new BellyFlame();
                bellyFlame.ChangePosition(new Vector2(1300, r.Next(0, 800)));
                bellyFlames.Add(bellyFlame);
                Game1.WindowManager.GetGameplayWindow().CurrentLevel.AddCreature(bellyFlame);
            }

            if (nightmareOrbTimer.TimerOn)
            {
                NightmareOrb nightmareOrb = new NightmareOrb();
                nightmareOrb.ChangePosition(new Vector2(1300, r.Next(0, 800)));
                nightmareOrbs.Add(nightmareOrb);
                Game1.WindowManager.GetGameplayWindow().CurrentLevel.AddCreature(nightmareOrb);
            }

            for (int i = bellyPlagues.Count - 1; i >= 0; --i)
            {
                if (bellyPlagues[i].CurrentHealth <= 0)
                {
                    bellyPlagues.RemoveAt(i);
                    mawDamage += 1000;
                    continue;
                }

                if (bellyPlagues[i].MoveTo(gameTime, Game1.WindowManager.GetGameplayWindow().Player.Creature.Position, new Vector2(5000, 0), 100)) // If plague close to player, attack
                    bellyPlagues[i].Attack();
            }

            for (int i = bellyFlames.Count - 1; i >= 0; --i)
            {
                if (bellyFlames[i].CurrentHealth <= 0)
                {
                    bellyFlames.RemoveAt(i);
                    mawDamage += 800;
                    continue;
                }

                if (bellyFlames[i].MoveTo(gameTime, Game1.WindowManager.GetGameplayWindow().Player.Creature.Position, new Vector2(5000, 0), 100)) // If plague close to player, attack
                    bellyFlames[i].Attack();
            }

            for (int i = nightmareOrbs.Count - 1; i >= 0; --i)
            {
                if (nightmareOrbs[i].CurrentHealth <= 0)
                {
                    nightmareOrbs.RemoveAt(i);
                    mawDamage += 3000;
                    continue;
                }

                if (nightmareOrbs[i].MoveTo(gameTime, Game1.WindowManager.GetGameplayWindow().Player.Creature.Position, new Vector2(5000, 0), 100)) // If orb close to player, attack
                {
                    transitioning = true;
                    phases = Phases.Nightmare;
                    KillAllOrbs();
                    break;
                }
            }
        }
        private void TheNightmare(GameTime gameTime)
        {
            bloodballTimer.CheckTimer(gameTime);
            if (bloodballTimer.TimerOn)
            {
                Bloodball bloodball = new Bloodball(Creature, Creature.Position,
                    Vector2.Normalize(Game1.WindowManager.GetGameplayWindow().Player.Creature.Position - Creature.Position));
                bloodballs.Add(bloodball);
                Game1.WindowManager.GetGameplayWindow().CurrentLevel.AddProjectile(bloodball);
                Creature.Attack();
            }
            if (Game1.InputManager.LeftMousePressed())
            {
                switch (r.Next(0, 3))
                {
                    case 0:
                        Game1.WindowManager.GetGameplayWindow().CurrentLevel.Projectiles.Add(new LightOrb(DragonLordDream, DragonLordDream.Position,
                    Vector2.Normalize(Game1.InputManager.GetCursor() - DragonLordDream.Position + new Vector2(r.Next(-30 - nightmareLevel * 10, 30 + nightmareLevel * 10), r.Next(-30 - nightmareLevel * 10, 30 + nightmareLevel * 10)))));
                        break;
                    case 1:
                        Game1.WindowManager.GetGameplayWindow().CurrentLevel.Projectiles.Add(new FireOrb(DragonLordDream, DragonLordDream.Position,
                    Vector2.Normalize(Game1.InputManager.GetCursor() - DragonLordDream.Position + new Vector2(r.Next(-30 - nightmareLevel * 10, 30 + nightmareLevel * 10), r.Next(-30 - nightmareLevel * 10, 30 + nightmareLevel * 10)))));
                        break;
                    case 2:
                        Game1.WindowManager.GetGameplayWindow().CurrentLevel.Projectiles.Add(new DreamOrb(DragonLordDream, DragonLordDream.Position,
                    Vector2.Normalize(Game1.InputManager.GetCursor() - DragonLordDream.Position + new Vector2(r.Next(-30 - nightmareLevel * 10, 30 + nightmareLevel * 10), r.Next(-30 - nightmareLevel * 10, 30 + nightmareLevel * 10)))));
                        break;
                    default:
                        break;
                }
            }

            if (DragonLordDream.GetHealthRatio < 0.5)
            {
                phases = Phases.Dream;
                transitioning = true;
            }
        }

        public override void OnDeath()
        {
            base.OnDeath();
            foreach (Bloodball bloodball in bloodballs)
                Game1.WindowManager.GetGameplayWindow().CurrentLevel.Projectiles.Remove(bloodball);
            Game1.WindowManager.GetGameplayWindow().CurrentLevel.Background = dreamBackground;
            Game1.WindowManager.GetGameplayWindow().CurrentLevel.Boundries = new Rectangle(0, 0, 1280, 900);

            if (phases != Phases.Dream)
            {
                Game1.WindowManager.GetGameplayWindow().CurrentLevel.Creatures.Remove(DragonLordDream);
                Game1.WindowManager.GetGameplayWindow().Player.ChangeCreature(PlayerCreature);
                Game1.WindowManager.GetGameplayWindow().CurrentLevel.Creatures.Add(PlayerCreature);
            }

            
            Game1.WindowManager.GetGameplayWindow().Player.Inventory.LoadInventoryFromData(PlayerInventory);
            Game1.WindowManager.GetGameplayWindow().CurrentLevel.Projectiles.Clear();

            if (Managers.Networking.GameplayNetworkHandler.InLocalGame)
                Managers.Networking.GameplayNetworkHandler.Send("PDA" + Managers.Networking.GameplayNetworkHandler.Name);
        }

        private void BackgroundSetup()
        {
            dreamBackground.FilePath = "dragonrise";
            dreamBackground.LoadContent();
            dreamBackground.Layers = 3;
            dreamBackground.LayerFrames = new List<float>() { 0, 0, 0 };
            dreamBackground.LayerSpeeds = new List<float>() { 0, 5, 10 };

            mawBackground.FilePath = "theMaw";
            mawBackground.LoadContent();
            mawBackground.Layers = 2;
            mawBackground.LayerFrames = new List<float>() { 0, 0 };
            mawBackground.LayerSpeeds = new List<float>() { 10, 200 };

            nightmareBackground.FilePath = "dragonightmare";
            nightmareBackground.LoadContent();
            nightmareBackground.Layers = 5;
            nightmareBackground.LayerFrames = new List<float>() { 0, 0, 0, 0, 0 };
            nightmareBackground.LayerSpeeds = new List<float>() { 0, 10, 15, 0, 0 };
        }

        private void KillAllOrbs()
        {
            Creature.TakeDamage(nightmareOrbs.Count * 3000 + bellyPlagues.Count * 1000 + bellyFlames.Count * 800 + mawDamage - 500 * nightmareLevel * nightmareLevel);
            foreach (NightmareOrb nightmareOrb in nightmareOrbs)
                nightmareOrb.Kill();

            foreach (BellyFlame bellyFlame in bellyFlames)
                bellyFlame.Kill();

            foreach (BellyPlague bellyPlague in bellyPlagues)
                bellyPlague.Kill();

            nightmareOrbs.Clear();
            bellyFlames.Clear();
            bellyPlagues.Clear();
        }
    }
}
