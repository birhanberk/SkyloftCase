using UnityEngine;

namespace Level
{
    [CreateAssetMenu(menuName = "Game/Level Data")]
    public class LevelData : ScriptableObject
    {
        public GameObject levelPrefab;
    }
}