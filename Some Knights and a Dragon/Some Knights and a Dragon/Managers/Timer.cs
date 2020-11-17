using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Managers
{
    public class Timer
    {
        private float previousTime = 0;
        private float interval = 0;
        private float duration;
        public bool TimerOn { get; private set; }

        public Timer(float interval, float duration = 0)
        {
            this.interval = interval < 0 ? 0 : interval;
            this.duration = duration < 0 ? 0 : duration;
        }

        public void NewTime(float interval)
        {
            this.interval = interval < 0 ? 0 : interval;
        }

        public void NewTime(float interval, float duration)
        {
            this.interval = interval < 0 ? 0 : interval;
            this.duration = duration < 0 ? 0 : duration;
        }

        public void CheckTimer(ref GameTime gameTime)
        {
            previousTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (TimerOn)
            {
                TimerOn = previousTime <= duration;
            }
            else
            {
                TimerOn = false;
                if (previousTime >= interval)
                {
                    previousTime = 0;
                    TimerOn = true;
                }
            }
        }
    }
}
