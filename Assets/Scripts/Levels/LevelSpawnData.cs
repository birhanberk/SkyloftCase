using System;
using Character.Enemy;
using UnityEngine;

namespace Levels
{
    [Serializable]
    public class LevelSpawnData
    {
        [SerializeField] private EnemyData enemyData;
        [SerializeField] private float spawnRatio;

        public EnemyData EnemyData => enemyData;
        public float SpawnRatio => spawnRatio;
    }
}
