using System;

namespace Core
{
    public class EventManager
    {
        public event Action<GameState> GameStateChanged;

        public void InvokeGameStateChanged(GameState state)
        {
            GameStateChanged?.Invoke(state);
        }
    }
}