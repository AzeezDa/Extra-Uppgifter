using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Managers
{
    public class InputManager
    {
        private KeyboardState currentKeyboardState;
        private MouseState currentMouseState;

        private KeyboardState previousKeyboardState;
        private MouseState previousMouseState;

        public InputManager()
        {
        }
        public bool KeyPressed(Keys key)
        {
            currentKeyboardState = Keyboard.GetState();
            return currentKeyboardState.IsKeyDown(key);
        }

        public bool KeyClicked(Keys key)
        {

            currentKeyboardState = Keyboard.GetState();
            return currentKeyboardState.IsKeyDown(key) && previousKeyboardState.IsKeyUp(key);
        }

        public bool LeftMousePressed()
        {   
            currentMouseState = Mouse.GetState();
            return currentMouseState.LeftButton == ButtonState.Pressed;
        }

        public bool RightMousePressed()
        {
            currentMouseState = Mouse.GetState();
            return currentMouseState.RightButton == ButtonState.Pressed;
        }

        public bool MiddleMousePressed()
        {
            currentMouseState = Mouse.GetState();
            return currentMouseState.MiddleButton == ButtonState.Pressed;
        }

        public bool LeftMouseClicked()
        {
            currentMouseState = Mouse.GetState();
            return currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released;
        }

        public bool RightMouseClicked()
        {
            currentMouseState = Mouse.GetState();
            return currentMouseState.RightButton == ButtonState.Pressed && previousMouseState.RightButton == ButtonState.Released;
        }

        public bool MiddleMouseClicked()
        {
            currentMouseState = Mouse.GetState();
            return currentMouseState.MiddleButton == ButtonState.Pressed && previousMouseState.MiddleButton == ButtonState.Released;
        }


        public void Update()
        {
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
        }
    }
}
