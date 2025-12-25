using System.Collections.Generic;
using Level;
using UnityEngine;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private List<LevelData> levels;
        [SerializeField] private Transform levelRoot;

        private int _currentLevelIndex = -1;
        private GameObject _currentLevelInstance;

        public void LoadNextLevel()
        {
            _currentLevelIndex++;

            if (_currentLevelIndex >= levels.Count)
                _currentLevelIndex = 0;

            LoadLevel(_currentLevelIndex);
        }

        public bool HasNextLevel()
        {
            return _currentLevelIndex + 1 < levels.Count;
        }

        private void LoadLevel(int index)
        {
            if (_currentLevelInstance)
                Destroy(_currentLevelInstance);

            _currentLevelInstance = Instantiate(levels[index].levelPrefab, levelRoot);
        }
    }
}
