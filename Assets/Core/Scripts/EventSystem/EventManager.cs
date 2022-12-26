using System;

namespace Core
{
    /// <summary>
    /// This event manager is for in-game only events.
    /// </summary>
    public class EventManager
    {
        public event Action<GameState> GameStateChanged;

        public void InvokeGameStateChanged(GameState state)
        {
            GameStateChanged?.Invoke(state);
        }
    }
}