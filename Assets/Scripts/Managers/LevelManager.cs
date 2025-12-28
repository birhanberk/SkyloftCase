using System.Collections.Generic;
using Levels;
using UnityEngine;
using VContainer;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private List<Level> levelPrefabs;
        [SerializeField] private Transform levelRoot;
        
        [Inject] private IObjectResolver _objectResolver;

        private int _currentLevelIndex;
        private Level _currentLevelInstance;

        public Level CurrentLevel => _currentLevelInstance;
        public int CurrentLevelIndex => _currentLevelIndex;

        public void OnLevelSuccessful()
        {
            _currentLevelIndex++;

            if (_currentLevelIndex >= levelPrefabs.Count)
                _currentLevelIndex = 0;
        }

        public void DestroyLevel()
        {
            if (_currentLevelInstance)
            {
                Destroy(_currentLevelInstance.gameObject);
            }
        }

        public bool HasNextLevel()
        {
            return _currentLevelIndex + 1 < levelPrefabs.Count;
        }

        public void LoadLevel()
        {
            DestroyLevel();

            _currentLevelInstance = Instantiate(levelPrefabs[_currentLevelIndex], levelRoot);
            _objectResolver.Inject(_currentLevelInstance);
        }
    }
}
