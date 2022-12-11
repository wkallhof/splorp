using Splorp.Core.Input;
using Splorp.Core.Primitives;
using static Splorp.Sdl2.Interops.SDL;

namespace Splorp.Sdl2
{
    public class Sdl2InputManager : IInputManager
    {
        public Action? OnExit { get; set; }
        public Action<int, int>? OnResize { get; set; }

        public void ProcessInputs(KeyboardState keyboard, MouseState mouse)
        {
            ProcessKeyboardInputs(keyboard);
            ProcessMouseInputs(mouse);
        }

        private void ProcessKeyboardInputs(KeyboardState keyboard)
        {
            var pollResult = SDL_PollEvent(out var keyboardEvent);
            if(pollResult == 0)
                return;

            if(keyboardEvent.type is SDL_EventType.SDL_QUIT && OnExit != null)
            {
                OnExit.Invoke();
                return;
            }

            if(keyboardEvent.type is SDL_EventType.SDL_WINDOWEVENT && keyboardEvent.window.windowEvent == SDL_WindowEventID.SDL_WINDOWEVENT_RESIZED && OnResize != null)
            {
                OnResize.Invoke(keyboardEvent.window.data1, keyboardEvent.window.data2);
            }

            if(keyboardEvent.type is not SDL_EventType.SDL_KEYDOWN and not SDL_EventType.SDL_KEYUP)
                return;
            
            switch(keyboardEvent.key.keysym.sym)
            {
                case SDL_Keycode.SDLK_LEFT : UpdateKey(keyboard.Left, keyboardEvent.type); break;
                case SDL_Keycode.SDLK_RIGHT : UpdateKey(keyboard.Right, keyboardEvent.type); break;
                case SDL_Keycode.SDLK_UP : UpdateKey(keyboard.Up, keyboardEvent.type); break;
                case SDL_Keycode.SDLK_DOWN : UpdateKey(keyboard.Down, keyboardEvent.type); break;
                case SDL_Keycode.SDLK_SPACE : UpdateKey(keyboard.Spacebar, keyboardEvent.type); break;
                case SDL_Keycode.SDLK_LSHIFT: UpdateKey(keyboard.LeftShift, keyboardEvent.type); break;
                case SDL_Keycode.SDLK_w : UpdateKey(keyboard.W, keyboardEvent.type); break;
                case SDL_Keycode.SDLK_a : UpdateKey(keyboard.A, keyboardEvent.type); break;
                case SDL_Keycode.SDLK_s : UpdateKey(keyboard.S, keyboardEvent.type); break;
                case SDL_Keycode.SDLK_d : UpdateKey(keyboard.D, keyboardEvent.type); break;
                case SDL_Keycode.SDLK_r : UpdateKey(keyboard.R, keyboardEvent.type); break;
            }

            void UpdateKey(KeyboardState.Key key, SDL_EventType eventType)
            {
                if(key.Up && eventType == SDL_EventType.SDL_KEYDOWN)
                    key.Down = true;

                if(key.Down && eventType == SDL_EventType.SDL_KEYUP)
                {
                    key.Down = false;
                    key.Pressed = true;
                    key.StateChecked = false;
                }
            }
        }

        private static void ProcessMouseInputs(MouseState mouse)
        {
            SDL_PumpEvents();
            var sdlMouse = SDL_GetMouseState(out var mouseX, out var mouseY);

            mouse.Position = new Vector2(mouseX, mouseY);
            mouse.LeftButtonDown = sdlMouse == 1;
            mouse.RightButtonDown = sdlMouse == 4;
        }
    }
}