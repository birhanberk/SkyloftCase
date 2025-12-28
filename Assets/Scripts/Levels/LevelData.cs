using System.Collections.Generic;
using UnityEngine;

namespace Levels
{
    [CreateAssetMenu(menuName = "Data / Level")]
    public class LevelData : ScriptableObject
    {
        [SerializeField] private int levelTime;
        [SerializeField] private List<LevelSpawnData> levelSpawnData;
        [SerializeField] private int maxActiveEnemyCount = 10;
        [SerializeField] private float despawnDistance = 15f;
        
        public int LevelTime => levelTime;
        public List<LevelSpawnData> LevelSpawnData => levelSpawnData;
        public int MaxActiveEnemyCount => maxActiveEnemyCount;
        public float DespawnDistance => despawnDistance;
    }
}