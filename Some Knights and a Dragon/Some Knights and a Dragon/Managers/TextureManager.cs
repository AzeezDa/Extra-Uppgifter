using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Managers
{
    struct TextureAndID // Struct to store a single texture and its name
    {
        public Texture2D Texture;
        public string Filepath;

        public TextureAndID(string filepath)
        {
            Filepath = filepath;
            Texture = Game1.ContentManager.Load<Texture2D>(filepath);
        }
    }
    public class TextureManager // Manages texture such that they are loaded once to be used multiple times
    {

        private List<TextureAndID> textures; // List of the textures and the their nams
        public Texture2D BlankTexture; // Blank texture used for debug or other effects

        public TextureManager(GraphicsDevice graphicsDevice)
        {
            textures = new List<TextureAndID>();

            // The blank texture is a 1x1 texture that is white, can be scaled from the spritebatch
            BlankTexture = new Texture2D(graphicsDevice, 1, 1);
            BlankTexture.SetData(new Color[] { Color.White });
        }

        public Texture2D GetTexture(string filepath) // Get a texture from the list.
        {
            foreach (TextureAndID texture in textures) // Checks if the texture is in the list already, return that
            {
                if (texture.Filepath == filepath)
                    return texture.Texture;
            }

            // If not in the list, make a new, add it to the list and return the new texture through recursion.
            textures.Add(new TextureAndID(filepath));
            return textures[textures.Count - 1].Texture;
        }
    }
}
