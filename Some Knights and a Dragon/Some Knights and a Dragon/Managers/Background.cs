using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Some_Knights_and_a_Dragon.Managers
{
    public class Background
    {
        [XmlIgnore]
        public Texture2D Texture { get; private set; }

        // LOADED FROM XML
        public string FilePath { get; set; } // Path of the background
        public int Layers { get; set; } // Amount of layers

        public List<float> LayerSpeeds; // Speed of each layer
        public List<float> LayerFrames; // Used for scrolling layers

        public Background()
        {

        }

        public void LoadContent()
        {
            Texture = Game1.TextureManager.GetTexture("Backgrounds/" + FilePath);
        }

        public void ChangeSpeed(int layer, float speed) // Changes speed of a layer
        {
            LayerSpeeds[layer % LayerSpeeds.Count] = speed;
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < LayerSpeeds.Count; i++)
            {
                // Adds the effect of infinite scrolling backgrounds
                LayerFrames[i] = (LayerFrames[i] + LayerSpeeds[i] * (float)gameTime.ElapsedGameTime.TotalSeconds) % 1280;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            // Draws the layers in the correct order and in their position if they have a scroll value
            for (int i = 0; i < LayerSpeeds.Count; i++)
            {
                spriteBatch.Draw(Texture, new Rectangle((int)LayerFrames[i], 0, 1280, 1080),
                                new Rectangle(0, i * Texture.Height / Layers, Texture.Width, Texture.Height / Layers),
                                Color.White);
                if (LayerSpeeds[i] != 0)
                {
                    spriteBatch.Draw(Texture,
                                new Rectangle((int)LayerFrames[i] - 1280, 0, 1280, 1080),
                                new Rectangle(0, i * Texture.Height / Layers, Texture.Width, Texture.Height / Layers),
                                Color.White);
                }
            }
        }
    }
}
