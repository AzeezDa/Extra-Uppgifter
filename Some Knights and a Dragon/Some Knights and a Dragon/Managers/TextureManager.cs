using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Managers
{
    struct TextureAndID
    {
        public Texture2D Texture;
        public string Filepath;

        public TextureAndID(string filepath)
        {
            Filepath = filepath;
            Texture = Game1.ContentManager.Load<Texture2D>(filepath);
        }
    }
    public class TextureManager
    {

        private List<TextureAndID> textures;

        public TextureManager()
        {
            textures = new List<TextureAndID>();
        }

        public Texture2D GetTexture(string filepath)
        {
            foreach (TextureAndID texture in textures)
            {
                if (texture.Filepath == filepath)
                    return texture.Texture;
            }
            textures.Add(new TextureAndID(filepath));
            return GetTexture(filepath);
        }
    }
}
