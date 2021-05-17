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

            // Every phase has a tranistion timer that will set things up before going to the next phase
            switch (phases)
            {
                case Phases.Dream:
                    if (transitioning) // Transitioning timer check
                        transitionTimer.CheckTimer(gameTime);
                    else
                    {
                        TheDream(gameTime); // Update the Dream phase
                    }
                    if (transitionTimer.TimerOn)
                    {
                        // Clear projectiles from previous phase
                        Game1.WindowManager.GetGameplayWindow().CurrentLevel.Projectiles.Clear();

                        // Change background
                        Game1.WindowManager.GetGameplayWindow().CurrentLevel.Background = dreamBackground;

                        // Set up the world boundries
                        Game1.WindowManager.GetGameplayWindow().CurrentLevel.Boundries = new Rectangle(0, 0, 1280, 900);

                        // Turn off boss nighmare mode
                        ((DragonLord)Creature).NightmareMode = false;

                        // Reset timer
                        transitionTimer.Reset();

                        // Remove the bloddballs
                        foreach (Bloodball bloodball in bloodballs)
                            Game1.WindowManager.GetGameplayWindow().CurrentLevel.Projectiles.Remove(bloodball);

                        // Remove the player creature that is Dragon Lord
                        Game1.WindowManager.GetGameplayWindow().CurrentLevel.Creatures.Remove(DragonLordDream);

                        // Change player creature to the normal creature
                        Game1.WindowManager.GetGameplayWindow().Player.ChangeCreature(PlayerCreature);

                        // Add the previous player Creature to the player list
                        Game1.WindowManager.GetGameplayWindow().CurrentLevel.Creatures.Add(PlayerCreature);

                        // Revert to the player's normal inventory items
                        Game1.WindowManager.GetGameplayWindow().Player.Inventory.LoadInventoryFromData(PlayerInventory);

                        // Reset the boss sprite scale
                        Creature.Sprite.Scale = 7;

                        // Change the fireball timer based on nightmare level
                        fireballTimer.NewTime(2000 - 200 * nightmareLevel);

                        // Increase the nightmare level
                        nightmareLevel++;

                        // Change transition timer
                        transitionTimer.NewTime(800);

                        // Transition over
                        transitioning = false;
                    }
                    break;
                case Phases.Maw:
                    if (transitioning) // Check transition timer
                        transitionTimer.CheckTimer(gameTime);
                    else
                    {
                        TheMaw(gameTime); // Update Maw
                    }
                    if (transitionTimer.TimerOn)
                    {
                        // Remove the boss from creature list
                        Game1.WindowManager.GetGameplayWindow().CurrentLevel.Creatures.Remove(Creature);

                        // Change background
                        Game1.WindowManager.GetGameplayWindow().CurrentLevel.Background = mawBackground;
                        
                        // Change boundries
                        Game1.WindowManager.GetGameplayWindow().CurrentLevel.Boundries = new Rectangle(0, 0, 1280, 800);

                        // Reset transition timer
                        transitionTimer.Reset();

                        // Change timers based on nightmare level
                        nightmareOrbTimer.NewTime(10000 + 1500 * nightmareLevel);
                        flameTimer.NewTime(2000 - 200 * nightmareLevel);
                        plagueTimer.NewTime(5000 - 400 * nightmareLevel);

                        // Increase nightmare level
                        nightmareLevel++;

                        // New transition timer
                        transitionTimer.NewTime(500);

                        // End transition
                        transitioning = false;
                    }
                    break;
                case Phases.Nightmare:
                    if (transitioning) // Check transition timer
                        transitionTimer.CheckTimer(gameTime);
                    else
                    {
                        TheNightmare(gameTime); // Update Nightmare phase
                    }
                    if (transitionTimer.TimerOn)
                    {
                        // Add the boss creature back
                        Game1.WindowManager.GetGameplayWindow().CurrentLevel.Creatures.Add(Creature);

                        // Change background
                        Game1.WindowManager.GetGameplayWindow().CurrentLevel.Background = nightmareBackground;

                        // Change boundries
                        Game1.WindowManager.GetGameplayWindow().CurrentLevel.Boundries = new Rectangle(0, 0, 1280, 800);

                        // Boss nightmare mode is on
                        ((DragonLord)Creature).NightmareMode = true;

                        // Transition reset
                        transitionTimer.Reset();

                        // Player's health set to max
                        DragonLordDream.SetHealthToMax();

                        // Put player to start position
                        DragonLordDream.ChangePosition(new Vector2(200, 900));

                        // Save the inventory
                        Game1.WindowManager.GetGameplayWindow().Player.Inventory.Inventory.CopyTo(PlayerInventory, 0);

                        // Remove player creature
                        Game1.WindowManager.GetGameplayWindow().CurrentLevel.Creatures.Remove(PlayerCreature);

                        // Change player creature to dragon lord
                        Game1.WindowManager.GetGameplayWindow().Player.ChangeCreature(DragonLordDream);

                        // Add the new player creature to the creature list
                        Game1.WindowManager.GetGameplayWindow().CurrentLevel.Creatures.Add(DragonLordDream);

                        // Empty the inventory
                        Game1.WindowManager.GetGameplayWindow().Player.Inventory.EmptyInventory();

                        // Change scale of sprite
                        Creature.Sprite.Scale = 10;

                        // Change timer based on nightmare level
                        bloodballTimer.NewTime(2000 - 100 * nightmareLevel);

                        // Increase nightmare level
                        nightmareLevel++;

                        // Change transition timer
                        transitionTimer.NewTime(200);
                        
                        // Reset dream timers
                        consumeTimer.Reset();
                        consumingTimer.Reset();

                        // Put player in start position
                        PlayerCreature.ChangePosition(new Vector2(300, 900));


                        // End Transition
                        transitioning = false;
                    }
                    break;
                default:
                    break;
            }

            // Update lightnings if there exists any
            for (int i = lightnings.Count - 1; i >= 0; --i)
            {
                lightnings[i].Update(gameTime);
                if (lightnings[i].Done)
                {
                    lightnings[i] = null;
                    lightnings.RemoveAt(i);
                }
            }

            // Move boss position in a sinusodial wave pattern (up and down)
            Creature.ChangePosition(new Vector2(Creature.Position.X, 700 + 50 * (float)Math.Sin(0.5 * gameTime.TotalGameTime.TotalSeconds)));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            // Draw lightnings
            foreach (Lightning lightning in lightnings)
                lightning.Draw(spriteBatch);
        }

        private void TheDream(GameTime gameTime)
        {
            // Check timers
            fireballTimer.CheckTimer(gameTime);
            lightningTimer.CheckTimer(gameTime);
            dreamAwakeningTimer.CheckTimer(gameTime);

            // Check timer if boss is not consuming the player
            if (!consuming)
                consumeTimer.CheckTimer(gameTime);

            // If time to start consuming put bool to true
            if (consumeTimer.TimerOn)
                consuming = true;

            // During dream phase, decrease nightmare level by 1 and not below 0.
            if (dreamAwakeningTimer.TimerOn)
                nightmareLevel -= nightmareLevel > 0 ? 1 : 0;

            
            if (consuming)
            {
                // If consuming draw player towards the boss and
                Game1.WindowManager.GetGameplayWindow().Player.Creature.AddToPosition(new Vector2(100 * (float)gameTime.ElapsedGameTime.TotalSeconds * consumePower++, 0));
                if (Vector2.Distance(Creature.Position, Game1.WindowManager.GetGameplayWindow().Player.Creature.Position) < 100) // If close to boss while consuming
                {
                    phases = Phases.Maw; // Get swallowed and change to maw phase
                    Creature.Sprite.OneTimeAnimation(2, 10); // Animate the boss
                    transitioning = true; // Transition on
                }

                // Check consuming up time
                consumingTimer.CheckTimer(gameTime);
                if (consumingTimer.TimerOn)
                    consuming = false;
            }

            // Check if it's time to shoot a fireball
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

            // Add a lightning strike on the player when it's time
            if (lightningTimer.TimerOn)
                lightnings.Add(new Lightning(Game1.WindowManager.GetGameplayWindow().Player.Creature.Position.X));
        }

        private void TheMaw(GameTime gameTime)
        {
            // Check timers
            plagueTimer.CheckTimer(gameTime);
            flameTimer.CheckTimer(gameTime);
            nightmareOrbTimer.CheckTimer(gameTime);
            mawNightmareTimer.CheckTimer(gameTime);

            // Increase nightmare level periodically
            if (mawNightmareTimer.TimerOn)
                nightmareLevel++;

            // If plague timer on add a plague that follows the player
            if (plagueTimer.TimerOn)
            {
                BellyPlague bellyPlague = new BellyPlague();
                bellyPlague.ChangePosition(new Vector2(1300, r.Next(0, 800)));
                bellyPlagues.Add(bellyPlague);
                Game1.WindowManager.GetGameplayWindow().CurrentLevel.AddCreature(bellyPlague);
            }

            // If flame timer on add a flame ball that follows the player
            if (flameTimer.TimerOn)
            {
                BellyFlame bellyFlame = new BellyFlame();
                bellyFlame.ChangePosition(new Vector2(1300, r.Next(0, 800)));
                bellyFlames.Add(bellyFlame);
                Game1.WindowManager.GetGameplayWindow().CurrentLevel.AddCreature(bellyFlame);
            }

            // If nightmare time on add a nightmare ball that follows the player
            if (nightmareOrbTimer.TimerOn)
            {
                NightmareOrb nightmareOrb = new NightmareOrb();
                nightmareOrb.ChangePosition(new Vector2(1300, r.Next(0, 800)));
                nightmareOrbs.Add(nightmareOrb);
                Game1.WindowManager.GetGameplayWindow().CurrentLevel.AddCreature(nightmareOrb);
            }

            // Update the plagues
            for (int i = bellyPlagues.Count - 1; i >= 0; --i)
            {
                if (bellyPlagues[i].CurrentHealth <= 0) // If they die increase the damage done to the boss at the end of the phase
                {
                    bellyPlagues.RemoveAt(i); // Remove the creature
                    mawDamage += 1000; // Add 1000 to the damage
                    continue;
                }

                // Update movement
                if (bellyPlagues[i].MoveTo(gameTime, Game1.WindowManager.GetGameplayWindow().Player.Creature.Position, new Vector2(5000, 0), 100)) // If plague close to player, attack
                    bellyPlagues[i].Attack();
            }

            // Update flames
            for (int i = bellyFlames.Count - 1; i >= 0; --i)
            {
                if (bellyFlames[i].CurrentHealth <= 0) // If they die increase the damage done to the boss at the end of the phase
                {
                    bellyFlames.RemoveAt(i); // Remove
                    mawDamage += 800; // More damage
                    continue;
                }

                // Update movement
                if (bellyFlames[i].MoveTo(gameTime, Game1.WindowManager.GetGameplayWindow().Player.Creature.Position, new Vector2(5000, 0), 100)) // If plague close to player, attack
                    bellyFlames[i].Attack();
            }

            // Update nightmares
            for (int i = nightmareOrbs.Count - 1; i >= 0; --i)
            {
                if (nightmareOrbs[i].CurrentHealth <= 0) // If they die increase the damage done to the boss at the end of the phase
                {
                    nightmareOrbs.RemoveAt(i); // Remove
                    mawDamage += 3000; // Damage
                    continue;
                }

                // Update movement, if they get close to the player switch phase
                if (nightmareOrbs[i].MoveTo(gameTime, Game1.WindowManager.GetGameplayWindow().Player.Creature.Position, new Vector2(5000, 0), 100)) // If orb close to player, attack
                {
                    transitioning = true;
                    phases = Phases.Nightmare;
                    KillAllOrbs(); // Kill all remaining orbs
                    break;
                }
            }
        }


        private void TheNightmare(GameTime gameTime) // Nightmare phase
        {
            // Check timer
            bloodballTimer.CheckTimer(gameTime);

            // The boss shoots a bloodball if timer on
            if (bloodballTimer.TimerOn)
            {
                Bloodball bloodball = new Bloodball(Creature, Creature.Position,
                    Vector2.Normalize(Game1.WindowManager.GetGameplayWindow().Player.Creature.Position - Creature.Position));
                bloodballs.Add(bloodball);
                Game1.WindowManager.GetGameplayWindow().CurrentLevel.AddProjectile(bloodball);
                Creature.Attack();
            }

            // If player left clicks then the player (now a dragon) will shoot three different attacks
            if (Game1.InputManager.LeftMousePressed())
            {
                // Chosen randomly each tick
                switch (r.Next(0, 3))
                {
                    // An orb that deals heavy damage
                    case 0:
                        Game1.WindowManager.GetGameplayWindow().CurrentLevel.Projectiles.Add(new LightOrb(DragonLordDream, DragonLordDream.Position,
                    Vector2.Normalize(Game1.InputManager.GetCursor() - DragonLordDream.Position + new Vector2(r.Next(-30 - nightmareLevel * 10, 30 + nightmareLevel * 10), r.Next(-30 - nightmareLevel * 10, 30 + nightmareLevel * 10)))));
                        break;

                    // An orb that deals burning damage (Damage over time)
                    case 1:
                        Game1.WindowManager.GetGameplayWindow().CurrentLevel.Projectiles.Add(new FireOrb(DragonLordDream, DragonLordDream.Position,
                    Vector2.Normalize(Game1.InputManager.GetCursor() - DragonLordDream.Position + new Vector2(r.Next(-30 - nightmareLevel * 10, 30 + nightmareLevel * 10), r.Next(-30 - nightmareLevel * 10, 30 + nightmareLevel * 10)))));
                        break;

                    // An orb that deals damage and heals the player
                    case 2:
                        Game1.WindowManager.GetGameplayWindow().CurrentLevel.Projectiles.Add(new DreamOrb(DragonLordDream, DragonLordDream.Position,
                    Vector2.Normalize(Game1.InputManager.GetCursor() - DragonLordDream.Position + new Vector2(r.Next(-30 - nightmareLevel * 10, 30 + nightmareLevel * 10), r.Next(-30 - nightmareLevel * 10, 30 + nightmareLevel * 10)))));
                        break;
                    default:
                        break;
                }
            }

            // If the player (now dragon) becomes under 50% health switch back to dream phase.
            if (DragonLordDream.GetHealthRatio < 0.5)
            {
                phases = Phases.Dream;
                transitioning = true;
            }
        }

        public override void OnDeath()
        {
            base.OnDeath();

            // Remove all bloodballs on death
            foreach (Bloodball bloodball in bloodballs)
                Game1.WindowManager.GetGameplayWindow().CurrentLevel.Projectiles.Remove(bloodball);

            // Switch back to dream phase
            Game1.WindowManager.GetGameplayWindow().CurrentLevel.Background = dreamBackground;

            // Fix boundries
            Game1.WindowManager.GetGameplayWindow().CurrentLevel.Boundries = new Rectangle(0, 0, 1280, 900);

            // Return the player inventory if the player was in nightmare phase
            if (phases != Phases.Dream)
            {
                Game1.WindowManager.GetGameplayWindow().CurrentLevel.Creatures.Remove(DragonLordDream);
                Game1.WindowManager.GetGameplayWindow().Player.ChangeCreature(PlayerCreature);
                Game1.WindowManager.GetGameplayWindow().CurrentLevel.Creatures.Add(PlayerCreature);
            }
            Game1.WindowManager.GetGameplayWindow().Player.Inventory.LoadInventoryFromData(PlayerInventory);

            // Clear projectiles
            Game1.WindowManager.GetGameplayWindow().CurrentLevel.Projectiles.Clear();

            // Send to connected players if it was a local game that the current player has defeated all bosses
            if (Managers.Networking.GameplayNetworkHandler.InLocalGame)
                Managers.Networking.GameplayNetworkHandler.Send("PDA" + Managers.Networking.GameplayNetworkHandler.Name);
        }

        private void BackgroundSetup()
        {
            // Dream phase background
            dreamBackground.FilePath = "dragonrise";
            dreamBackground.LoadContent();
            dreamBackground.Layers = 3;
            dreamBackground.LayerFrames = new List<float>() { 0, 0, 0 };
            dreamBackground.LayerSpeeds = new List<float>() { 0, 5, 10 };

            // Maw phase background
            mawBackground.FilePath = "theMaw";
            mawBackground.LoadContent();
            mawBackground.Layers = 2;
            mawBackground.LayerFrames = new List<float>() { 0, 0 };
            mawBackground.LayerSpeeds = new List<float>() { 10, 200 };

            // Nightmare phase background
            nightmareBackground.FilePath = "dragonightmare";
            nightmareBackground.LoadContent();
            nightmareBackground.Layers = 5;
            nightmareBackground.LayerFrames = new List<float>() { 0, 0, 0, 0, 0 };
            nightmareBackground.LayerSpeeds = new List<float>() { 0, 10, 15, 0, 0 };
        }

        private void KillAllOrbs()
        {
            // The player takes damage based on dead nightmare, plague and flames
            Creature.TakeDamage(nightmareOrbs.Count * 3000 + bellyPlagues.Count * 1000 + bellyFlames.Count * 800 + mawDamage - 500 * nightmareLevel * nightmareLevel);

            // Kill all orbs
            foreach (NightmareOrb nightmareOrb in nightmareOrbs)
                nightmareOrb.Kill();

            foreach (BellyFlame bellyFlame in bellyFlames)
                bellyFlame.Kill();

            foreach (BellyPlague bellyPlague in bellyPlagues)
                bellyPlague.Kill();

            // Clear the lists
            nightmareOrbs.Clear();
            bellyFlames.Clear();
            bellyPlagues.Clear();
        }
    }
}
