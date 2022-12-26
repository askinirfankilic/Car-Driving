using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Core
{
    public abstract class ILevelManager : IInitializable
    {
        protected int _currentLevelIndex = 0;
        protected int _currentLevelPrefix;
        protected string _levelPrefix = "Level"; //Add current level prefix next to that.

        private int _levelCount;

        public void Initialize()
        {
            _currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
            _levelCount = SceneManager.sceneCountInBuildSettings;
        }

        public virtual void RestartLevel()
        {
            _currentLevelPrefix = _currentLevelIndex + 1;
            LoadLevel(_currentLevelPrefix);
        }

        public virtual void NextLevel()
        {
            //If last level finished reload first level. This can be later changed to random pool.
            if (_currentLevelIndex == _levelCount - 1)
            {
                _currentLevelIndex = 0;
            }
            else
            {
                _currentLevelIndex++;
            }

            _currentLevelPrefix = _currentLevelIndex + 1;
            LoadLevel(_currentLevelPrefix);
        }

        private void LoadLevel(int index)
        {
            SceneManager.LoadScene(string.Format("{0}{1}", _levelPrefix, index.ToString()));
        }
    }
}