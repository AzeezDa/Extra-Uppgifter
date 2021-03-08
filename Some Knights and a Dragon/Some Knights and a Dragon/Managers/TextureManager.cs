using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Managers
{
    public class TextureManager // Manages texture such that they are loaded once to be used multiple times
    {
        private Dictionary<string, Texture2D> textureIDs;
        public Texture2D BlankTexture; // Blank texture used for debug or other effects

        public TextureManager(GraphicsDevice graphicsDevice)
        {
            textureIDs = new Dictionary<string, Texture2D>();

            // The blank texture is a 1x1 texture that is white, can be scaled from the spritebatch
            BlankTexture = new Texture2D(graphicsDevice, 1, 1);
            BlankTexture.SetData(new Color[] { Color.White });

        }

        public Texture2D GetTexture(string filepath) // Get a texture from the dictionary.
        {
            if (textureIDs.ContainsKey(filepath)) // If key already 
                return textureIDs[filepath];

            // If not in the list, make a new, add it to the list and return the new texture through recursion.
            textureIDs.Add(filepath, Game1.ContentManager.Load<Texture2D>(filepath));
            return textureIDs[filepath];
        }

        public void Reload(List<string> texturesToLoad)
        {
            foreach (string textureFilePath in texturesToLoad) // Adds the new textures to the dictionary and loads them
            {
                if (!textureIDs.ContainsKey(textureFilePath))
                    textureIDs.Add(textureFilePath, Game1.ContentManager.Load<Texture2D>(textureFilePath));
            }

            GC.Collect();
        }
    }
}
