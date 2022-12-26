using System;
using UnityEngine;
using Zenject;

namespace Core
{
    public enum GameState
    {
        Start = 0,
        Gameplay = 1,
        Finish = 2,
        Fail = 3,
    }

    public class GameManager : IInitializable, ILateDisposable
    {
        private ILevelManager _levelManager;
        private EventManager _eventManager;

        public GameManager(ILevelManager levelManager, EventManager eventManager)
        {
            _levelManager = levelManager;
            _eventManager = eventManager;

            _eventManager.GameStateChanged += OnGameStateChanged;
        }

        public void Initialize()
        {
            _eventManager.InvokeGameStateChanged(GameState.Start);
        }

        private void OnGameStateChanged(GameState state)
        {
            switch (state)
            {
                case GameState.Start:
#if UNITY_EDITOR
                    Debug.Log("Gamestate Start");
#endif
                    break;
                case GameState.Gameplay:
#if UNITY_EDITOR
                    Debug.Log("Gamestate Gameplay");
#endif
                    break;
                case GameState.Finish:
#if UNITY_EDITOR
                    Debug.Log("Gamestate Finish");
#endif
                    Next();
                    break;
                case GameState.Fail:
#if UNITY_EDITOR
                    Debug.Log("Gamestate Fail");
#endif
                    Restart();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private void Restart()
        {
            _levelManager.RestartLevel();
        }

        private void Next()
        {
            _levelManager.NextLevel();
        }

        public void LateDispose()
        {
            _eventManager.GameStateChanged -= OnGameStateChanged;
        }
    }
}

}