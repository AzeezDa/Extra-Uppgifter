using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Managers
{
    public class Timer // Handles times for certain things such as cooldowns or timer for attacks
    {
        private float previousTime = 0;
        private float interval = 0;
        private float duration;
        public bool TimerOn { get; private set; } // Returns true if the interval has passed.

        public Timer(float interval, float duration = 0)
        {
            this.interval = interval < 0 ? 0 : interval;
            this.duration = duration < 0 ? 0 : duration;
        }

        // Change the timer's interval
        public void NewTime(float interval)
        {
            this.interval = interval < 0 ? 0 : interval;
        }

        // Change the timer's interval and duration
        public void NewTime(float interval, float duration)
        {
            this.interval = interval < 0 ? 0 : interval;
            this.duration = duration < 0 ? 0 : duration;
        }

        // Check if timer has passed, changes the TimerOn field.
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
