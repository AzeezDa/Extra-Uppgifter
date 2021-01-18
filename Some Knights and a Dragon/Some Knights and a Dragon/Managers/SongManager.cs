using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace Some_Knights_and_a_Dragon.Managers
{
    public class SongManager // Manages song played
    {
        public SongManager()
        {
            MediaPlayer.IsRepeating = true;
        }

        public void Play(string songName) // Play the song with given file name
        {
            Song song = Game1.ContentManager.Load<Song>("Music/" + songName);
            MediaPlayer.Play(song);
        }
    }
}
