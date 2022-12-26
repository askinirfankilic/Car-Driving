using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public abstract class ILevelManager
    {
        public int CurrentLevelIndex = 0;
        public string LevelPrefix = "Level"; //Add current level index next to that.

        public virtual void RestartLevel()
        {
#if UNITY_EDITOR
            Debug.Log($"Restart Level {CurrentLevelIndex.ToString()}");
#endif
            LoadLevel(CurrentLevelIndex);
        }

        public virtual void NextLevel()
        {
            CurrentLevelIndex++;
#if UNITY_EDITOR
            Debug.Log($"Next Level {CurrentLevelIndex.ToString()}");
#endif
            LoadLevel(CurrentLevelIndex);
        }

        private void LoadLevel(int index)
        {
            SceneManager.LoadScene(string.Format("{0}{1}", LevelPrefix, index.ToString()));
        }
    }
}