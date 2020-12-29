using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Managers
{
    public class Background
    {
        public Texture2D Texture { get; private set; }
        private List<float> layerSpeeds;
        private List<float> layerFrames;
        private int layers;
        public Background(string filepath, int layers)
        {
            layerSpeeds = new List<float>(layers);
            layerFrames = new List<float>(layers);
            this.layers = layers;
            Texture = Game1.TextureManager.GetTexture("Backgrounds/" + filepath);
            for (int i = 0; i < layers; i++)
            {
                layerSpeeds.Add(0);
                layerFrames.Add(0);
            }
        }

        public void ChangeSpeed(int layer, float speed)
        {
            layerSpeeds[layer % layerSpeeds.Count] = speed;
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < layerSpeeds.Count; i++)
            {

                layerFrames[i] = (layerFrames[i] + layerSpeeds[i] * (float)gameTime.ElapsedGameTime.TotalSeconds) % 1280;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < layerSpeeds.Count; i++)
            {
                spriteBatch.Draw(Texture, new Rectangle((int)layerFrames[i], 0, 1280, 1080),
                                new Rectangle(0, i * Texture.Height / layers, Texture.Width, Texture.Height / layers),
                                Color.White);
                if (layerSpeeds[i] != 0)
                {
                    spriteBatch.Draw(Texture,
                                new Rectangle((int)layerFrames[i] - 1280, 0, 1280, 1080),
                                new Rectangle(0, i * Texture.Height / layers, Texture.Width, Texture.Height / layers),
                                Color.White);
                }
            }
        }
    }
}
